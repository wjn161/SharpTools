using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;

namespace SharpTools.Web.Common
{
    /// <summary>
    /// http://mysoftsolution.googlecode.com/svn-history/r165/trunk/MySoft.Web/Static/
    /// </summary>
    public interface IStaicPageGenerator
    {
        void Generate(GenerateType type, string sourcePage, string outputPath);
        event EventHandler<EventArgs> OnError;
        event EventHandler<EventArgs> OnSuccess;
    }
}
