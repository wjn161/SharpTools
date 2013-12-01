using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTools.Logging
{
    public class ConsoleLog
    {
        private const string CONSLE_LOG_FACTORY_CACHE_KEY = "SharpTools.Logging.ConsoleFactory.CacheKey";
        private readonly LoggerCache<string, ConsoleLoggerFactory> factoryCache;
        private LoggerCache<string, ConsoleLogger> loggerCache;
        private ConsoleLog()
        {
            factoryCache = new LoggerCache<string, ConsoleLoggerFactory>(key => new Task<ConsoleLoggerFactory>(() => new ConsoleLoggerFactory()));
            var logger = factoryCache.GetValue(CONSLE_LOG_FACTORY_CACHE_KEY);
        }
        public static readonly ConsoleLog Instance = new ConsoleLog();

        public void Debug()
        {

        }
    }

    public class LoggerFactory
    {
    }
}
