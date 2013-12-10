using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTools.Mail;

namespace SharpTools.Tests.MailTest
{
    [TestFixture]
    public class SmtpMailTest
    {
        [Test]
        public void Test()
        {
            var smtpMail = new SmtpMail("smtp.163.com","wjn-161@163.com","wjn161","a2263268");
            smtpMail.OnSendCompleted += smtpMail_OnSendSuccess;
            smtpMail.Send("hello", "hello", "345734678@qq.com"); 
        }

        static void smtpMail_OnSendSuccess(object sender, SmtpMailSendArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
