using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpTools.Logging;

namespace SharpTools.Tests.LoggingTest
{
    [TestFixture]
    public class SimpleLoggerTest
    {
        private ILogger logger;
        [SetUp]
        public void Init()
        {
            logger = LoggerManager.Create<SimpleLoggerFactory>("TestLogger", LoggerLevel.Error);

        }

        [Test]
        public void File_Should_Be_Create_And_Logging()
        {
            logger.Error("Hello");
        }
        [Test]
        public void File_Should_Be_Create_And_Exception()
        {
            var b = 0;
            try
            {
                var a = 1 / b;
            }
            catch (Exception ex)
            {
                logger.Error("exception", ex);
            }
        }
        [Test]
        public void Multi_Thread_File_Exception()
        {
            while (true)
            {
                Parallel.For(1, 1000, (i) =>
                {
                    var b = 0;
                    try
                    {
                        var a = 1 / b;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(String.Format("Thread #{0},Exception{1}:{2},Message:{3}", Thread.CurrentThread.ManagedThreadId, ex.GetType().Name, i, ex.Message), ex);
                    }
                });
                Thread.Sleep(1);
            }
           

        }
    }
}
