using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;

namespace Hzdtf.CodeGenerator.Contract.Standard
{
    /// <summary>
    /// 数据库信息服务接口
    /// @ 黄振东
    /// </summary>
    public interface IDbInfoService
    {
        /// <summary>
        /// 查询所有表信息列表
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dataSourceType">数据源类型</param>
        /// <returns>所有表信息列表</returns>
        ReturnInfo<IList<TableInfo>> Query(string dataBase, string connectionString, string dataSourceType);
    }
}
