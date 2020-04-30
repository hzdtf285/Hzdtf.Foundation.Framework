using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 数据库服务基类
    /// @ 黄振东
    /// </summary>
    public abstract class DatabasePoolServiceBase
    {
        /// <summary>
        /// 数据库池
        /// </summary>
        public IDatabasePool DatabasePool
        {
            get;
            set;
        }

        /// <summary>
        /// 从数据库池
        /// </summary>
        public ISlaveDatabasePool SlaveDatabasePool
        {
            get;
            set;
        }

        /// <summary>
        /// 获取数据库池
        /// 如果输入是从，且没找到从，自动返回主
        /// </summary>
        /// <param name="mode">访问模式</param>
        /// <returns>数据库池</returns>
        public virtual IDatabasePool GetDatabasePool(AccessMode mode = AccessMode.MASTER)
        {
            return mode == AccessMode.MASTER
                || SlaveDatabasePool == null
                || !SlaveDatabasePool.ExistsSlave
                ? DatabasePool : SlaveDatabasePool;
        }
    }
}
