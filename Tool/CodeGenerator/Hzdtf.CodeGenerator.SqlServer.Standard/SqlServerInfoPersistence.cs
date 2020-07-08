using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.CodeGenerator.Persistence.Contract.Std;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Hzdtf.CodeGenerator.SqlServer.Standard
{
    /// <summary>
    /// Sql Server信息持久化
    /// @ 黄振东
    /// </summary>
    public class SqlServerInfoPersistence : IDbInfoPersistence
    {
        /// <summary>
        /// 查询所有表信息列表
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>所有表信息列表</returns>
        public IList<TableInfo> SelectTables(string dataBase, string connectionString)
        {
            string sql = "SELECT tbs.name Name,ds.value Description "
                    + " FROM sys.extended_properties ds"
                    + " LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id"
                    + " WHERE ds.minor_id=0";
            IDbConnection dbConnection = null;// new SqlConnection(connectionString);
            IList<TableInfo> tables = dbConnection.Query<TableInfo>(sql).AsList();
            dbConnection.Close();
            dbConnection.Dispose();

            return tables;
        }

        /// <summary>
        /// 查询所有列信息列表
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="table">表</param>
        /// <returns>所有列信息列表</returns>
        public IList<ColumnInfo> SelectColumns(string dataBase, string connectionString, string table)
        {
            string sql = "SELECT c.name Name,ep.value Description,st.name DataType, c.max_length Length, is_nullable [IsNull], st.name ColumnType"
                    + " FROM sys.tables t"
                    + " INNER JOIN sys.columns c ON t.object_id = c.object_id"
                    + " INNER JOIN dbo.systypes st ON c.system_type_id = st.xusertype"
                    + " INNER JOIN sys.extended_properties ep"
                    + " ON ep.major_id = c.object_id AND ep.minor_id = c.column_id WHERE ep.class=1"
                    + " AND t.name=@Table";
            IDbConnection dbConnection = new SqlConnection(connectionString);
            IList<ColumnInfo> columns = dbConnection.Query<ColumnInfo>(sql, new { Table = table }).AsList();
            dbConnection.Close();
            dbConnection.Dispose();

            return columns;
        }
    }
}
