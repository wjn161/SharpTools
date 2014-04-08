using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTools.Logging
{

    public class ConsoleLoggerFactory
    {
        public static ILogger Create(string loggerName, LoggerLevel level)
        {
            return new ConsoleLogger(loggerName, level);
        }
        public static ILogger Create(string loggerName)
        {
            return new ConsoleLogger(loggerName);
        }
    }

    public class FileLoggerFactory
    {
        public static ILogger Create(string loggerName, LoggerLevel level)
        {
            return new FileLogger(loggerName, level);
        }
        public static ILogger Create(string loggerName)
        {
            return new FileLogger(loggerName);
        }
    }
}
