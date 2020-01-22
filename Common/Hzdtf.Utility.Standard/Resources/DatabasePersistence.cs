using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 数据库持久化基类
    /// @ 黄振东
    /// </summary>
    public abstract class DatabasePersistenceBase
    {
        /// <summary>
        /// 数据库池
        /// </summary>
        public IDatabasePool DatabasePool
        {
            get;
            set;
        }
    }
}
