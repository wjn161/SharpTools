using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    [Serializable]
    public abstract class AbstractLoggerFactory : ILoggerFactory
    {

        public virtual ILogger Create(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return Create(type.FullName);
        }

        public virtual ILogger Create(Type type, LoggerLevel level)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return Create(type.FullName, level);
        }

        public abstract ILogger Create(String name);

        public abstract ILogger Create(String name, LoggerLevel level);

    }
}
