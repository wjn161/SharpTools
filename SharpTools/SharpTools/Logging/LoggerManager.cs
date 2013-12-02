using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTools.Logging
{

    public class LoggerManager : MarshalByRefObject
    {
        public class LoggerCache<TK, TV>
        {
            private static readonly IDictionary<TK, TV> innerDict = new Dictionary<TK, TV>();
            private readonly object mudex = new object();
            public TV Get(TK key)
            {
                return innerDict.ContainsKey(key) ? innerDict[key] : default(TV);
            }
            public void Add(TK key, TV value)
            {
                lock (mudex)
                {
                    innerDict[key] = value;
                }
            }
            public void Remove(TK key)
            {
                lock (mudex)
                {
                    innerDict.Remove(key);
                }
            }
        }
        public static readonly LoggerCache<string, ILoggerFactory> factoryCache = new LoggerCache<string, ILoggerFactory>();
        private const string LOGGER_FACTORY_CACHE_KEY = "SharpTools.Logging.LOGGERFACTORY.CACHE.KEY";
        public static ILogger Create<TLoggerFactory>(string loggerName) where TLoggerFactory : class,ILoggerFactory
        {
            var factory = factoryCache.Get(LOGGER_FACTORY_CACHE_KEY);
            if (factory != null) return factory.Create(loggerName);
            factory = Activator.CreateInstance<TLoggerFactory>();
            factoryCache.Add(LOGGER_FACTORY_CACHE_KEY, factory);
            return factory.Create(loggerName);
        }

        public static ILogger Create<TLoggerFactory>(string loggerName, LoggerLevel level)
         where TLoggerFactory : class,ILoggerFactory
        {
            var factory = factoryCache.Get(LOGGER_FACTORY_CACHE_KEY);
            if (factory != null) return factory.Create(loggerName, level);
            factory = Activator.CreateInstance<TLoggerFactory>();
            factoryCache.Add(LOGGER_FACTORY_CACHE_KEY, factory);
            return factory.Create(loggerName, level);
        }
    }
}
