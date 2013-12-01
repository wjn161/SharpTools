using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    public class LoggerManager : MarshalByRefObject
    {
        public static ILogger Create<TLoggerFactory>(string loggerName)
         where TLoggerFactory : class,ILoggerFactory
        {
            var factory = Activator.CreateInstance<TLoggerFactory>();
            return factory.Create(loggerName);
        }

        public static ILogger Create<TLoggerFactory>(string loggerName, LoggerLevel level)
         where TLoggerFactory : class,ILoggerFactory
        {
            var factory = Activator.CreateInstance<TLoggerFactory>();
            return factory.Create(loggerName, level);
        }
    }
}
