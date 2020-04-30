using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 从数据库池接口
    /// @ 黄振东
    /// </summary>
    public interface ISlaveDatabasePool : IDatabasePool
    {
        /// <summary>
        /// 是否存在从
        /// </summary>
        bool ExistsSlave
        {
            get;
        }
    }
}
