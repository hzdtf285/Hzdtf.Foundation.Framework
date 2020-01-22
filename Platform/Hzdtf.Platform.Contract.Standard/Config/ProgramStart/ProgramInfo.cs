using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.ProgramStart
{
    /// <summary>
    /// 程序启动信息
    /// @ 黄振东
    /// </summary>
    public class ProgramStartInfo
    {
        /// <summary>
        /// 全类名（如果不是本程序集，则要包含所在程序集，用,分隔）
        /// </summary>
        public string FullClass
        {
            get;
            set;
        }

        /// <summary>
        /// 参数集合
        /// </summary>
        public string[] Args
        {
            get;
            set;
        }
    }
}
