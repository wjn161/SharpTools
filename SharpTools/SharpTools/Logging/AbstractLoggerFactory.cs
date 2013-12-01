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

        /// <summary>
        ///   Gets the configuration file.
        /// </summary>
        /// <param name = "fileName">i.e. log4net.config</param>
        /// <returns></returns>
        protected static FileInfo GetConfigFile(string fileName)
        {
            return Path.IsPathRooted(fileName) ? new FileInfo(fileName) : new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
        }
    }
}
