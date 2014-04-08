using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Logging
{
    /// <summary>
    ///   Manages logging.
    /// </summary>
    /// <remarks>
    ///   This is a facade for the different logging subsystems.
    ///   It offers a simplified interface that follows IOC patterns
    ///   and a simplified priority/level/severity abstraction.
    /// </remarks>
    public interface ILogger
    {
        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "message">The message to log</param>
        void Error(string message);
        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "message">The message to log</param>
        void Error(string message, Exception exception);

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Error(string format, params object[] args);

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Error(Exception exception, string format, params object[] args);

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Error(IFormatProvider formatProvider, string format, params object[] args);

        /// <summary>
        ///   Logs an error message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Error(Exception exception, IFormatProvider formatProvider, string format, params object[] args);

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "message">The message to log</param>
        void Info(string message);

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "message">The message to log</param>
        void Info(string message, Exception exception);

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Info(string format, params object[] args);

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Info(Exception exception, string format, params object[] args);

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Info(IFormatProvider formatProvider, string format, params object[] args);

        /// <summary>
        ///   Logs an info message.
        /// </summary>
        /// <param name = "exception">The exception to log</param>
        /// <param name = "formatProvider">The format provider to use</param>
        /// <param name = "format">Format string for the message to log</param>
        /// <param name = "args">Format arguments for the message to log</param>
        void Info(Exception exception, IFormatProvider formatProvider, string format, params object[] args);

    }
}
