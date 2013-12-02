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
    }
}
