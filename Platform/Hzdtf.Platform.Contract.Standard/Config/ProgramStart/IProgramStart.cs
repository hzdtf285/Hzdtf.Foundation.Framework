using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.ProgramStart
{
    /// <summary>
    /// 程序启动接口
    /// @ 黄振东
    /// </summary>
    public interface IProgramStart
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args">参数</param>
        void Start(params string[] args);
    }
}
