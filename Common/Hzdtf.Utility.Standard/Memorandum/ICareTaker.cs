using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hzdtf.Utility.Standard.Memorandum
{
    /// <summary>
    /// 守护者接口
    /// </summary>
    /// <typeparam name="StateT">状态类型</typeparam>
    public interface ICareTaker<StateT>
    {
        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="memento">备忘录</param>
        void Add(string key, IMemento<StateT> memento);

        /// <summary>
        /// 获取备忘录
        /// </summary>
        /// <param name="key">键</param>
        IMemento<StateT> Get(string key);

        /// <summary>
        /// 移除备忘录
        /// </summary>
        /// <param name="key">键</param>
        void Remove(string key);
    }
}
