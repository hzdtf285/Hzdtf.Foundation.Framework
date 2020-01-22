using Hzdtf.CodeGenerator.Model.Standard;
using System;
using System.Collections.Generic;

namespace Hzdtf.CodeGenerator.Persistence.Contract.Std
{
    /// <summary>
    /// 数据库信息持久化
    /// @ 黄振东
    /// </summary>
    public interface IDbInfoPersistence
    {
        /// <summary>
        /// 查询所有表信息列表
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>所有表信息列表</returns>
        IList<TableInfo> SelectTables(string dataBase, string connectionString);

        /// <summary>
        /// 查询所有列信息列表
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="table">表</param>
        /// <returns>所有列信息列表</returns>
        IList<ColumnInfo> SelectColumns(string dataBase, string connectionString, string table);
    }
}
