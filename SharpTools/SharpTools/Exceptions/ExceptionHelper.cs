using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Exceptions
{
    public class ExceptionHelper
    {
        public  static string GetExcepitonName(Exception ex)
        {
            var name = ex.GetType().Name;
            if (ex.InnerException == null) return name;
            var innerException = GetInnerException(ex);
            name = innerException.GetType().Name;
            return name;
        }
        public static Exception GetInnerException(Exception ex)
        {
            if (ex == null)
            {
                return null;
            }
            return ex.InnerException != null ? GetInnerException(ex.InnerException) : ex;
        }

        public static string GetExcepitonMessage(Exception ex)
        {
            string result;
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("\r\nType:" + ex.GetType().FullName);
                stringBuilder.Append("\r\nMessage:" + GetErrorMessage(ex));
                stringBuilder.Append("\r\nSource:" + ex.Source);
                stringBuilder.Append("\r\nTargetSite:" + ((ex.TargetSite == null) ? null : ex.TargetSite.ToString()));
                stringBuilder.Append("\r\nStackTrace:" + ex.StackTrace);
                stringBuilder.Append("\r\n");
                if (ex.InnerException != null)
                {
                    stringBuilder.AppendLine(string.Empty.PadRight(150, '-'));
                    stringBuilder.Append(GetExcepitonMessage(ex.InnerException));
                }
                result = stringBuilder.ToString();
            }
            catch (Exception)
            {
                result = ex.ToString();
            }
            return result;
        }

        private static string GetErrorMessage(Exception ex)
        {
            if (ex == null)
            {
                return null;
            }
            return ex.Data.Contains("ErrorHeader") ? string.Format("{0}\r\n\r\n{1}", ex.Data["ErrorHeader"], ex.Message) : ex.Message;
        }
    }
}
