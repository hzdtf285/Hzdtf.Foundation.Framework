using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.CodeGenerator.Persistence.Contract.Std;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hzdtf.CodeGenerator.MySql.Standard
{
    /// <summary>
    /// MySql信息持久化
    /// @ 黄振东
    /// </summary>
    public class MySqlInfoPersistence : IDbInfoPersistence
    {
        /// <summary>
        /// 查询所有表信息列表
        /// </summary>
        /// <param name="dataBase">数据库</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>所有表信息列表</returns>
        public IList<TableInfo> SelectTables(string dataBase, string connectionString)
        {
            string sql = "SELECT table_name Name,TABLE_COMMENT Description FROM INFORMATION_SCHEMA.TABLES WHERE table_type IN('BASE TABLE','VIEW')"
                    + " AND table_schema = @DataBase";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            IList<TableInfo> tables = dbConnection.Query<TableInfo>(sql, new { DataBase = dataBase }).AsList();
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
            string sql = "SELECT column_name NAME, DATA_TYPE DataType, column_comment Description, IF (IS_NULLABLE = 'YES', TRUE, FALSE) `IsNull`, CHARACTER_MAXIMUM_LENGTH `Length`, COLUMN_TYPE as ColumnType"
                        + " FROM information_schema.COLUMNS WHERE table_schema = @DataBase AND TABLE_NAME = @Table";
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            IList<ColumnInfo> columns = dbConnection.Query<ColumnInfo>(sql, new { DataBase = dataBase, Table = table }).AsList();
            dbConnection.Close();
            dbConnection.Dispose();

            return columns;
        }
    }
}
