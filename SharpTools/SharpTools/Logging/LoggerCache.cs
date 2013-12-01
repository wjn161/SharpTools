using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SharpTools.Logging
{
    internal static class TaskExtentions
    {
        /// <summary>Creates a Task that has completed in the RanToCompletion state with the specified result.</summary>
        /// <typeparam name="TResult">Specifies the type of payload for the new Task.</typeparam>
        /// <param name="factory">The target TaskFactory.</param>
        /// <param name="result">The result with which the Task should complete.</param>
        /// <returns>The completed Task.</returns>
        public static Task<TResult> FromResult<TResult>(this TaskFactory factory, TResult result)
        {
            var tcs = new TaskCompletionSource<TResult>(factory.CreationOptions);
            tcs.SetResult(result);
            return tcs.Task;
        }
    }

    /// <summary>Extension methods for Lazy.</summary>
    internal static class LazyExtensions
    {
        /// <summary>Forces value creation of a Lazy instance.</summary>
        /// <typeparam name="T">Specifies the type of the value being lazily initialized.</typeparam>
        /// <param name="lazy">The Lazy instance.</param>
        /// <returns>The initialized Lazy instance.</returns>
        public static Lazy<T> Force<T>(this Lazy<T> lazy)
        {
            return lazy;
        }

        /// <summary>Retrieves the value of a Lazy asynchronously.</summary>
        /// <typeparam name="T">Specifies the type of the value being lazily initialized.</typeparam>
        /// <param name="lazy">The Lazy instance.</param>
        /// <returns>A Task representing the Lazy's value.</returns>
        public static Task<T> GetValueAsync<T>(this Lazy<T> lazy)
        {
            return Task.Factory.StartNew(() => lazy.Value);
        }

        /// <summary>Creates a Lazy that's already been initialized to a specified value.</summary>
        /// <typeparam name="T">The type of the data to be initialized.</typeparam>
        /// <param name="value">The value with which to initialize the Lazy instance.</param>
        /// <returns>The initialized Lazy.</returns>
        public static Lazy<T> Create<T>(T value)
        {
            return new Lazy<T>(() => value, false).Force();
        }
    }

    /// <summary>Debugger type proxy for AsyncCache.</summary>
    /// <typeparam name="TKey">Specifies the type of the cache's keys.</typeparam>
    /// <typeparam name="TValue">Specifies the type of the cache's values.</typeparam>
    internal class LoggerCache_DebugView<TKey, TValue>
    {
        private readonly LoggerCache<TKey, TValue> asyncCache;

        internal LoggerCache_DebugView(LoggerCache<TKey, TValue> asyncCache)
        {
            this.asyncCache = asyncCache;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        internal KeyValuePair<TKey, Task<TValue>>[] Values
        {
            get { return asyncCache.ToArray(); }
        }
    }

    /// <summary>Caches asynchronously retrieved data.</summary>
    /// <typeparam name="TKey">Specifies the type of the cache's keys.</typeparam>
    /// <typeparam name="TValue">Specifies the type of the cache's values.</typeparam>
    [DebuggerTypeProxy(typeof(LoggerCache_DebugView<,>))]
    [DebuggerDisplay("Count={Count}")]
    internal class LoggerCache<TKey, TValue> : ICollection<KeyValuePair<TKey, Task<TValue>>>
    {
        /// <summary>The factory to use to create tasks.</summary>
        private readonly Func<TKey, Task<TValue>> valueFactory;
        /// <summary>The dictionary to store all of the tasks.</summary>
        private readonly ConcurrentDictionary<TKey, Lazy<Task<TValue>>> map;

        /// <summary>Initializes the cache.</summary>
        /// <param name="valueFactory">A factory for producing the cache's values.</param>
        public LoggerCache(Func<TKey, Task<TValue>> valueFactory)
        {
            if (valueFactory == null) throw new ArgumentNullException("valueFactory");
            this.valueFactory = valueFactory;
            map = new ConcurrentDictionary<TKey, Lazy<Task<TValue>>>();
        }

        /// <summary>Gets a Task to retrieve the value for the specified key.</summary>
        /// <param name="key">The key whose value should be retrieved.</param>
        /// <returns>A Task for the value of the specified key.</returns>
        public Task<TValue> GetValue(TKey key)
        {
            if (key == null) throw new ArgumentNullException("key");
            var value = new Lazy<Task<TValue>>(() => valueFactory(key));
            return map.GetOrAdd(key, value).Value;
        }

        /// <summary>Sets the value for the specified key.</summary>
        /// <param name="key">The key whose value should be set.</param>
        /// <param name="value">The value to which the key should be set.</param>
        public void SetValue(TKey key, TValue value)
        {
            SetValue(key, Task.Factory.FromResult(value));
        }

        /// <summary>Sets the value for the specified key.</summary>
        /// <param name="key">The key whose value should be set.</param>
        /// <param name="value">The value to which the key should be set.</param>
        public void SetValue(TKey key, Task<TValue> value)
        {
            if (key == null) throw new ArgumentNullException("key");
            map[key] = LazyExtensions.Create(value);
        }

        /// <summary>Gets a Task to retrieve the value for the specified key.</summary>
        /// <param name="key">The key whose value should be retrieved.</param>
        /// <returns>A Task for the value of the specified key.</returns>
        public Task<TValue> this[TKey key]
        {
            get { return GetValue(key); }
            set { SetValue(key, value); }
        }

        /// <summary>Empties the cache.</summary>
        public void Clear() { map.Clear(); }

        /// <summary>Gets the number of items in the cache.</summary>
        public int Count { get { return map.Count; } }

        /// <summary>Gets an enumerator for the contents of the cache.</summary>
        /// <returns>An enumerator for the contents of the cache.</returns>
        public IEnumerator<KeyValuePair<TKey, Task<TValue>>> GetEnumerator()
        {
            return map.Select(p => new KeyValuePair<TKey, Task<TValue>>(p.Key, p.Value.Value)).GetEnumerator();
        }

        /// <summary>Gets an enumerator for the contents of the cache.</summary>
        /// <returns>An enumerator for the contents of the cache.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        /// <summary>Adds or overwrites the specified entry in the cache.</summary>
        /// <param name="item">The item to be added.</param>
        void ICollection<KeyValuePair<TKey, Task<TValue>>>.Add(KeyValuePair<TKey, Task<TValue>> item)
        {
            this[item.Key] = item.Value;
        }

        /// <summary>Determines whether the cache contains the specified key.</summary>
        /// <param name="item">The item contained the key to be searched for.</param>
        /// <returns>True if the cache contains the key; otherwise, false.</returns>
        bool ICollection<KeyValuePair<TKey, Task<TValue>>>.Contains(KeyValuePair<TKey, Task<TValue>> item)
        {
            return map.ContainsKey(item.Key);
        }

        /// <summary>
        /// Copies the elements of the System.Collections.Generic.ICollection<T> to an
        /// System.Array, starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional System.Array that is the destination of the elements
        /// copied from System.Collections.Generic.ICollection<T>. The System.Array must
        /// have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        void ICollection<KeyValuePair<TKey, Task<TValue>>>.CopyTo(KeyValuePair<TKey, Task<TValue>>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, Task<TValue>>>)map).CopyTo(array, arrayIndex);
        }

        /// <summary>Gets whether the cache is read-only.</summary>
        bool ICollection<KeyValuePair<TKey, Task<TValue>>>.IsReadOnly { get { return false; } }

        /// <summary>Removes the specified key from the cache.</summary>
        /// <param name="item">The item containing the key to be removed.</param>
        /// <returns>True if the item could be removed; otherwise, false.</returns>
        bool ICollection<KeyValuePair<TKey, Task<TValue>>>.Remove(KeyValuePair<TKey, Task<TValue>> item)
        {
            Lazy<Task<TValue>> value;
            return map.TryRemove(item.Key, out value);
        }
    }
}