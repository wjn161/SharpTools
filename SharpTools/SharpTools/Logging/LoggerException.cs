using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    [Serializable]
    public class LoggerException : Exception
    {
        public LoggerException()
        {
        }

        public LoggerException(String message)
            : base(message)
        {
        }

        public LoggerException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
