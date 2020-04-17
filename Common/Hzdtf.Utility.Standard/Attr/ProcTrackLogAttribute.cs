using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 过程轨迹日志特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ProcTrackLogAttribute : Attribute
    {
        /// <summary>
        /// 记录执行过程，默认为是
        /// </summary>
        public bool ExecProc
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 忽略参数值
        /// </summary>
        public bool IgnoreParamValues
        {
            get;
            set;
        }

        /// <summary>
        /// 忽略返回值
        /// </summary>
        public bool IgnoreParamReturn
        {
            get;
            set;
        }
    }
}
