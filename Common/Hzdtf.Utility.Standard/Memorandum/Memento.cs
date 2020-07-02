using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Memorandum
{
    /// <summary>
    /// 备注录存储
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="StateT">状态类型</typeparam>
    public class Memento<StateT> : IMemento<StateT>
    {
        /// <summary>
        /// 状态
        /// </summary>
        private readonly StateT state;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="state">状态</param>
        public Memento(StateT state)
        {
            this.state = state;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns>状态</returns>
        public StateT GetState() => state;
    }
}
