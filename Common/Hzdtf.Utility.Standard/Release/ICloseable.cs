using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Release
{
    /// <summary>
    /// 关闭的接口
    /// @ 黄振东
    /// </summary>
    public interface IClose
    {
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }

    /// <summary>
    /// 可关闭的接口
    /// @ 黄振东
    /// </summary>
    public interface ICloseable : IClose
    {
        /// <summary>
        /// 关闭后事件
        /// </summary>
        event DataHandler Closed;
    }
}
