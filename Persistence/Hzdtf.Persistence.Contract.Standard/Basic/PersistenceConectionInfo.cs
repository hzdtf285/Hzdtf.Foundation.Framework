using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Basic
{
    /// <summary>
    /// 持久化连接信息
    /// @ 黄振东
    /// </summary>
    public class PersistenceConectionInfo
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// 访问模式
        /// </summary>
        public AccessMode AccessMode
        {
            get;
            set;
        } = AccessMode.MASTER;
    }
}
