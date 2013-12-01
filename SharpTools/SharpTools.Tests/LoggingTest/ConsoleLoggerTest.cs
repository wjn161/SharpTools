using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTools.Logging;

namespace SharpTools.Tests.LoggingTest
{
    [TestFixture]
    public class ConsoleLoggerTest
    {
        private static ConsoleLoggerFactory factory;
        private ILogger logger;
        [SetUp]
        public void Init()
        {
            logger = LoggerManager.CreateLogger<ConsoleLoggerFactory>("consoleLogger");
        }
        [Test]
        public void Console_Logger_Print_HelloWorld()
        {
            logger.Error("Hello World");
            Assert.AreEqual(1, 1);
        }
        [Test]
        public void Console_Logger_Print_HelloWorld_Too()
        {
            logger.Debug("Hello World Too");
            Assert.AreEqual(1, 1);
        }
    }
}
