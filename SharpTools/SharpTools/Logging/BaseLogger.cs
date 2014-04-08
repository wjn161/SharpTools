using System;
using System.Globalization;
using System.Security;
namespace SharpTools.Logging
{

    /// <summary>
    ///	The Level Filtered Logger class.  This is a base clase which
    ///	provides a LogLevel attribute and reroutes all functions into
    ///	one Log method.
    /// </summary>
    [Serializable]
    public abstract class BaseLogger : ILogger
    {
        private LoggerLevel level = LoggerLevel.Off;
        private String name = "unnamed";

        /// <summary>
        ///   Creates a new <c>LevelFilteredLogger</c>.
        /// </summary>
        protected BaseLogger()
        {
        }

        protected BaseLogger(String name)
        {
            ChangeName(name);
        }

        protected BaseLogger(LoggerLevel loggerLevel)
        {
            level = loggerLevel;
        }

        protected BaseLogger(String loggerName, LoggerLevel loggerLevel)
            : this(loggerLevel)
        {
            ChangeName(loggerName);
        }
        /// <value>
        ///   The <c>LoggerLevel</c> that this logger
        ///   will be using. Defaults to <c>LoggerLevel.Off</c>
        /// </value>
        public LoggerLevel Level
        {
            get { return level; }
            set { level = value; }
        }

        /// <value>
        ///   The name that this logger will be using. 
        ///   Defaults to <c>String.Empty</c>
        /// </value>
        public String Name
        {
            get { return name; }
        }

        #region ILogger implementation


        #region Info

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "message">The message to log</param>
        public void Info(string message)
        {
            Log(LoggerLevel.Info, message, null);
        }

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "message">The message to log</param>
        public void Info(string message, Exception exception)
        {
            Log(LoggerLevel.Info, message, exception);
        }

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Info(string format, params object[] args)
        {
            Log(LoggerLevel.Info, String.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Info(Exception exception, string format, params object[] args)
        {
            Log(LoggerLevel.Info, String.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Info(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log(LoggerLevel.Info, String.Format(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Info(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Log(LoggerLevel.Info, String.Format(formatProvider, format, args), exception);
        }

        #endregion

        #region Error

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "message">The message to log</param>
        public void Error(string message)
        {
          
            Log(LoggerLevel.Error, message, null);
        }

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "message">The message to log</param>
        public void Error(string message, Exception exception)
        {
            Log(LoggerLevel.Error, message, exception);
        }

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Error(string format, params object[] args)
        {
            Log(LoggerLevel.Error, String.Format(CultureInfo.CurrentCulture, format, args), null);
        }

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Error(Exception exception, string format, params object[] args)
        {
            Log(LoggerLevel.Error, String.Format(CultureInfo.CurrentCulture, format, args), exception);
        }

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Error(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log(LoggerLevel.Error, String.Format(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        public void Error(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Log(LoggerLevel.Error, String.Format(formatProvider, format, args), exception);
        }

        #endregion

        #endregion

        /// <summary>
        ///   Implementors output the log content by implementing this method only.
        ///   Note that exception can be null
        /// </summary>
        /// <param name = "loggerLevel"></param>
        /// <param name = "loggerName"></param>
        /// <param name = "message"></param>
        /// <param name = "exception"></param>
        protected abstract void Log(LoggerLevel loggerLevel, String loggerName, String message, Exception exception);

        protected void ChangeName(String newName)
        {
            if (newName == null)
            {
                throw new ArgumentNullException("newName");
            }

            name = newName;
        }

        private void Log(LoggerLevel loggerLevel, String message, Exception exception)
        {
            Log(loggerLevel, Name, message, exception);
        }
    }
}
