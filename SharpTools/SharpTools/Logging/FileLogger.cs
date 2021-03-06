﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SharpTools.Exceptions;

namespace SharpTools.Logging
{
    public class FileLogger : BaseLogger
    {
        static readonly ConcurrentQueue<string> loggerQueue = new ConcurrentQueue<string>();
        /// <summary>
        ///   Creates a new ConsoleLogger with the <c>Level</c>
        ///   set to <c>LoggerLevel.Info</c> and the <c>Name</c>
        ///   set to <c>String.Empty</c>.
        /// </summary>
        public FileLogger()
            : this(String.Empty, LoggerLevel.Info)
        {
        }

        /// <summary>
        ///   Creates a new ConsoleLogger with the <c>Name</c>
        ///   set to <c>String.Empty</c>.
        /// </summary>
        /// <param name = "logLevel">The logs Level.</param>
        public FileLogger(LoggerLevel logLevel)
            : this(String.Empty, logLevel)
        {
        }

        /// <summary>
        ///   Creates a new ConsoleLogger with the <c>Level</c>
        ///   set to <c>LoggerLevel.Info</c>.
        /// </summary>
        /// <param name = "name">The logs Name.</param>
        public FileLogger(String name)
            : this(name, LoggerLevel.Info)
        {
        }

        /// <summary>
        ///   Creates a new ConsoleLogger.
        /// </summary>
        /// <param name = "name">The logs Name.</param>
        /// <param name = "logLevel">The logs Level.</param>
        public FileLogger(String name, LoggerLevel logLevel)
            : base(name, logLevel)
        {
        }
        

        private static string GetBaseDir(string logPath)
        {
            var config = ConfigurationManager.AppSettings["LoggerBaseDir"];
            return string.IsNullOrEmpty(config) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logPath) : config;
        }

        protected override void Log(LoggerLevel loggerLevel, string loggerName, string message, Exception exception)
        {
            string defaultPath;
            string log, fileName;
            if (exception != null)
            {
                defaultPath = GetBaseDir("ErrorLogs");
                if (!Directory.Exists(defaultPath))
                {
                    Directory.CreateDirectory(defaultPath);
                }
                fileName = Path.Combine(defaultPath, string.Format("{0}.log", ExceptionHelper.GetExcepitonName(exception)));//文件名称
                log = string.Format("[{0}][{1}] =>{3}UserMessage:{2}{3}{4}:{5}{3}{6}{7}{3}",
                   loggerName,
                   DateTime.Now,
                   message,
                   Environment.NewLine,
                   exception.GetType().FullName,
                   ExceptionHelper.GetExcepitonMessage(exception),
                   Environment.NewLine,
                   string.Empty.PadRight(200, '-'));
            }
            else
            {
                defaultPath = GetBaseDir("Logs");//日志默认路径
                if (!Directory.Exists(defaultPath))
                {
                    Directory.CreateDirectory(defaultPath);
                }
                fileName = Path.Combine(defaultPath, string.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd")));//文件名称
                log = string.Format("[{0}][{1}] => {2}{3}{4}{3}",
                    loggerName,
                    DateTime.Now,
                    message,
                    Environment.NewLine,
                    string.Empty.PadRight(150, '-'));
            }
            lock (loggerQueue)
            {
                loggerQueue.Enqueue(log);
            }
            new Action<string>(Log).BeginInvoke(fileName, null, null);
        }

        private static void Log(string fileName)
        {
            while (!loggerQueue.IsEmpty)
            {
                lock (loggerQueue)
                {
                    string msg;
                    if (loggerQueue.TryDequeue(out msg))
                    {
                        File.AppendAllText(fileName, msg, Encoding.UTF8);
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}
