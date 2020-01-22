using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 事务特性
    /// @ 黄振东
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : Attribute
    {
        /// <summary>
        /// 连接ID位置索引
        /// 默认为-1，为-1时，表示方法没有连接ID的参数
        /// </summary>
        public sbyte ConnectionIdIndex
        {
            get;
            set;
        } = -1;

        /// <summary>
        /// 事务等级
        /// </summary>
        public IsolationLevel Level
        {
            get;
            set;
        } = IsolationLevel.ReadCommitted;
    }
}
