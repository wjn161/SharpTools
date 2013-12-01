using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    public class LoggerManager : MarshalByRefObject
    {
        public static ILogger CreateLogger<TFactory>(string name)
         where TFactory : class, ILoggerFactory
        {
            ILoggerFactory factory = Activator.CreateInstance<TFactory>();
            return factory.Create(name);
        }
    }
}
