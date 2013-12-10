using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace SharpTools.Mail
{
    public class SmtpMailSendArgs : EventArgs
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class SmtpMail
    {
        private readonly SmtpClient mailClient = new SmtpClient();
        public event EventHandler<SmtpMailSendArgs> OnSendCompleted;
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public uint Port { get; set; }
        public bool EnableSSL { get; set; }
        public SmtpMail(string smtpServer, string userName, string displayName, string password, uint port, bool enableSSL)
        {
            SmtpServer = smtpServer;
            UserName = userName;
            Password = password;
            Port = port;
            EnableSSL = enableSSL;
            DisplayName = displayName;
            mailClient.Host = smtpServer;
            mailClient.Port = (int)Port;
            mailClient.Credentials = new NetworkCredential(userName, password);
            mailClient.EnableSsl = EnableSSL;
        }

        public SmtpMail(string smtpServer, string userName, string password)
            : this(smtpServer, userName, string.Empty, password, 25, false)
        {

        }
        public SmtpMail(string smtpServer, string userName, string displayName, string password)
            : this(smtpServer, userName, displayName, password, 25, false)
        {

        }

        #region 同步发送
        public void Send(string title, string body, string to)
        {
            Send(title, body, false, new[] { to }, null, null);
        }

        public void Send(string title, string body, string to, Attachment attachment)
        {
            Send(title, body, false, new[] { to }, null, attachment);
        }

        public void Send(string title, string body, bool bodyIsHtml, string to)
        {
            Send(title, body, bodyIsHtml, new[] { to }, null, null);
        }

        public void Send(string title, string body, string[] to, string[] cc, Attachment attachment)
        {
            Send(title, body, false, to, cc, attachment);
        }
        public void Send(string title, string body, bool bodyIsHtml, string[] to, string[] cc)
        {
            Send(title, body, bodyIsHtml, to, cc, null);
        }
        public void Send(string title, string body, bool isHtml, string[] to, string[] cc, Attachment attachment)
        {
            if (to == null || to.Length <= 0)
            {
                throw new ArgumentNullException("to");
            }
            try
            {
                var message = new MailMessage
                {
                    Subject = title,
                    SubjectEncoding = Encoding.UTF8,
                    Body = body,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = isHtml,
                    From = new MailAddress(UserName, DisplayName)
                };
                if (to.Length > 0)
                {
                    foreach (var t in to.Where(t => !string.IsNullOrEmpty(t)))
                    {
                        message.To.Add(t);
                    }
                }

                if (cc != null && cc.Length > 0)
                {
                    foreach (var c in cc.Where(c => !string.IsNullOrEmpty(c)))
                    {
                        message.CC.Add(c);
                    }
                }
                if (attachment != null)
                {
                    message.Attachments.Add(attachment);
                }
                mailClient.Send(message);
                SendSuccess();
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }

        public void SendWithMultiAttachment(string title, string body, bool isHtml, string[] to, string[] cc, AttachmentCollection attachmentCollection)
        {
            if (to == null || to.Length <= 0)
            {
                throw new ArgumentNullException("to");
            }
            try
            {
                var message = new MailMessage
                {
                    Subject = title,
                    SubjectEncoding = Encoding.UTF8,
                    Body = body,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = isHtml,
                    From = new MailAddress(UserName, DisplayName)
                };
                if (to.Length > 0)
                {
                    foreach (var t in to.Where(t => !string.IsNullOrEmpty(t)))
                    {
                        message.To.Add(t);
                    }
                }

                if (cc != null && cc.Length > 0)
                {
                    foreach (var c in cc.Where(c => !string.IsNullOrEmpty(c)))
                    {
                        message.CC.Add(c);
                    }
                }
                if (attachmentCollection != null)
                {
                    message.Attachments.AddRange(attachmentCollection);
                }
                mailClient.Send(message);
                SendSuccess();
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }
        #endregion

        #region 异步发送
        public void SendAsync(string title, string body, string[] to, string[] cc, Attachment attachment)
        {

        }

        public void SendAsync(string title, string body, string to)
        {

        }
        public void SendAsync(string title, string body, string to, string[] cc)
        {

        }
        public void SendAsync(string title, string body, string to, Attachment attachment)
        {

        }
        public void SendAsync(string title, string body, string[] to)
        {

        }
        public void SendAsync(string title, string body, string[] to, string[] cc, AttachmentCollection attachmentCollection)
        {

        }
        #endregion

        private void SendError(Exception ex)
        {
            if (ex != null && OnSendCompleted != null)
            {
                OnSendCompleted(this, new SmtpMailSendArgs
               {
                   IsSuccess = false,
                   Message = string.Format("发送失败，错误:{0}\n{1}", ex.Message, ex.StackTrace)
               });
            }
        }

        private void SendSuccess()
        {
            if (OnSendCompleted != null)
            {
                OnSendCompleted(this, new SmtpMailSendArgs
                {
                    IsSuccess = true,
                    Message = "发送成功"
                });
            }
        }
    }
}
