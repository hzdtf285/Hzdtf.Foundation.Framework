using Hzdtf.Utility.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Basic
{
    /// <summary>
    /// 默认连接字符串
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DefaultConnectionString : IDefaultConnectionString
    {
        /// <summary>
        /// 连接环境工厂
        /// </summary>
        public ISimpleFactory<EnvironmentType, string[]> ConnEnvironmentFactory
        {
            get;
            set;
        }

        /// <summary>
        /// 连接字符串集合
        /// </summary>
        public string[] Connections
        {
            get => ConnEnvironmentFactory.Create(UtilTool.CurrEnvironmentType);
        }
    }
}
