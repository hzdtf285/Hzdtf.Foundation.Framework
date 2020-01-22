using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Release
{
    /// <summary>
    /// 可关闭的接口
    /// @ 黄振东
    /// </summary>
    public interface ICloseable
    {
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();

        /// <summary>
        /// 关闭后事件
        /// </summary>
        event DataHandler Closed;
    }
}
