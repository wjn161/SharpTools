using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    [Serializable]
    public class ConsoleLoggerFactory : MarshalByRefObject, ILoggerFactory
    {
        private LoggerLevel? level;

        public ConsoleLoggerFactory()
        {
        }

        public ConsoleLoggerFactory(LoggerLevel level)
        {
            this.level = level;
        }

        public ILogger Create(Type type)
        {
            return Create(type.FullName);
        }

        public ILogger Create(String name)
        {
            if (level.HasValue)
            {
                return Create(name, level.Value);
            }
            return new ConsoleLogger(name);
        }

        public ILogger Create(Type type, LoggerLevel level)
        {
            return new ConsoleLogger(type.Name, level);
        }

        public ILogger Create(String name, LoggerLevel level)
        {
            return new ConsoleLogger(name, level);
        }
    }
}
