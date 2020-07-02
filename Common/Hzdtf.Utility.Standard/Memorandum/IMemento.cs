using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Memorandum
{
    /// <summary>
    /// 备注录存储接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="StateT">状态类型</typeparam>
    public interface IMemento<StateT>
    {
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns>状态</returns>
        StateT GetState();
    }
}
