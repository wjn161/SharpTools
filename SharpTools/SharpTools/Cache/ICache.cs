using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Cache
{
    public interface ICache<in TKey, TValue> where TValue : class
    {
        TValue Get(TKey key);
        void Add(TKey key, TValue value, TimeSpan expired);
        void Remove(TKey key);
        void Retrieve(TKey key, TimeSpan expired, Func<TValue> func);
        void RemoveAll();
    }
}
