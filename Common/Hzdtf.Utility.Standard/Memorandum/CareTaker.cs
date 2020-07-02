using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hzdtf.Utility.Standard.Memorandum
{
    /// <summary>
    /// 守护者
    /// </summary>
    /// <typeparam name="StateT">状态类型</typeparam>
    public class CareTaker<StateT> : ICareTaker<StateT>
    {
        /// <summary>
        /// 备忘录字典
        /// </summary>
        private readonly IDictionary<string, IMemento<StateT>> dicMementos = new Dictionary<string, IMemento<StateT>>();

        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="memento">备忘录</param>
        public void Add(string key, IMemento<StateT> memento)
        {
            if (dicMementos.ContainsKey(key))
            {
                dicMementos[key] = memento;
            }
            else
            {
                dicMementos.Add(key, memento);
            }
        }

        /// <summary>
        /// 获取备忘录
        /// </summary>
        /// <param name="key">键</param>
        public IMemento<StateT> Get(string key) => dicMementos.ContainsKey(key) ? dicMementos[key] : null;

        /// <summary>
        /// 移除备忘录
        /// </summary>
        /// <param name="key">键</param>
        public void Remove(string key)
        {
            if (dicMementos.ContainsKey(key))
            {
                dicMementos.Remove(key);
            }
        }
    }
}
