using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    [Serializable]
    public class SimpleLoggerFactory : MarshalByRefObject, ILoggerFactory
    {
        private LoggerLevel? level;

        public SimpleLoggerFactory()
        {
        }

        public SimpleLoggerFactory(LoggerLevel level)
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
            return new SimpleLogger(name);
        }

        public ILogger Create(Type type, LoggerLevel level)
        {
            return new SimpleLogger(type.Name, level);
        }

        public ILogger Create(String name, LoggerLevel level)
        {
            return new SimpleLogger(name, level);
        }
    }
}
