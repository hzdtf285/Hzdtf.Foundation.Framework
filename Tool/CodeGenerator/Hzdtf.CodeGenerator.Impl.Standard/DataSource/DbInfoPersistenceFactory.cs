using Hzdtf.CodeGenerator.MySql.Standard;
using Hzdtf.CodeGenerator.Persistence.Contract.Std;
using Hzdtf.CodeGenerator.SqlServer.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Impl.Standard.DataSource
{
    /// <summary>
    /// 数据库信息持久化工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class DbInfoPersistenceFactory : ISimpleFactory<string, IDbInfoPersistence>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public IDbInfoPersistence Create(string type)
        {
            switch (type)
            {
                case "SqlServer":
                    return new SqlServerInfoPersistence();

                case "MySql":
                    return new MySqlInfoPersistence();

                default:
                    return null;
            }
        }
    }
}
