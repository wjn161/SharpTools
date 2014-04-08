using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    /// <summary>
    /// Levels of logger
    /// </summary>
    public enum LoggerLevel
    {
        /// <summary>
        ///   Logging will be off
        /// </summary>
        Off = 0,
        /// <summary>
        /// Logging errors
        /// </summary>
        Error = 1,
        /// <summary>
        /// Logging infomation
        /// </summary>
        Info = 2
    }
}
