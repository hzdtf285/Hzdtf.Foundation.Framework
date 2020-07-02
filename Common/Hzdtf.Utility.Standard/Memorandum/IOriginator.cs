using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Memorandum
{
    /// <summary>
    /// 发起者接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="StateT">状态类型</typeparam>
    public interface IOriginator<StateT>
    {
        /// <summary>
        /// 保存状态到备忘录
        /// </summary>
        /// <returns>备忘录</returns>
        IMemento<StateT> SaveStateToMemento();

        /// <summary>
        /// 从备忘录里还原状态
        /// </summary>
        /// <param name="memento">备忘录</param>
        void RestoreStateFromMemento(IMemento<StateT> memento);
    }
}
