using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Factory;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Hzdtf.CodeGenerator.Impl.Standard
{
    /// <summary>
    /// 连接工厂
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ConnectionFactory : ISimpleFactory<string, IDbConnection>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public IDbConnection Create(string type)
        {
            switch(type)
            {
                case "SqlServer":
                    return new SqlConnection();

                case "MySql":
                    return new MySqlConnection();

                default:
                    return null;
            }
        }
    }
}
