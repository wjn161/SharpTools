using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpTools.Web.Common
{
    public enum GenerateType
    {
        /// <summary>
        /// 远程模式
        /// </summary>
        Remote,
        /// <summary>
        /// 本地模式
        /// </summary>
        Local
    }

    public enum GenerateMode
    {
        /// <summary>
        /// 主动生成
        /// </summary>
        Positive = 1,
        /// <summary>
        /// 被动生成
        /// </summary>
        Nagetive=2
    }
}
