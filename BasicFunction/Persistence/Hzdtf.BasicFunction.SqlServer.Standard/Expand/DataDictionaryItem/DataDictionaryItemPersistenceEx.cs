using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.BasicFunction.Model.Standard.Expand.DataDictionaryItem;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 数据字典子项持久化
    /// @ 黄振东
    /// </summary>
    public partial class DataDictionaryItemPersistence
    {
        /// <summary>
        /// 根据数据字典ID和文本统计个数
        /// </summary>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="text">文本</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        public int CountByDataItemIdAndText(int dataDictionaryId, string text, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{CountSql()} WHERE {GetFieldByProp("DataDictionaryId")}=@DataDictionaryId AND [{GetFieldByProp("Text")}]=@Text";
                result = dbConn.ExecuteScalar<int>(sql, new { DataDictionaryId = dataDictionaryId, Text = text });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据数据字典ID和文本统计个数，但排除ID
        /// </summary>
        /// <param name="notId">排除ID</param>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="text">文本</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        public int CountByDataItemIdAndTextNotId(int notId, int dataDictionaryId, string text, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{CountSql()} WHERE [{GetFieldByProp("Id")}]!=@Id AND {GetFieldByProp("DataDictionaryId")}=@DataDictionaryId AND [{GetFieldByProp("Text")}]=@Text";
                result = dbConn.ExecuteScalar<int>(sql, new { Id = notId, DataDictionaryId = dataDictionaryId, Text = text });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据数据字典ID查询数据字典子项列表
        /// </summary>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>数据字典子项列表</returns>
        public IList<DataDictionaryItemInfo> SelectByDataDictionaryId(int dataDictionaryId, string connectionId = null)
        {
            IList<DataDictionaryItemInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE {GetFieldByProp("DataDictionaryId")}=@DataDictionaryId";
                result = dbConn.Query<DataDictionaryItemInfo>(sql, new { DataDictionaryId = dataDictionaryId }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据数据字典编码查询数据字典子项列表
        /// </summary>
        /// <param name="dataDictionaryCode">数据字典编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>数据字典子项列表</returns>
        public IList<DataDictionaryItemInfo> SelectByDataDictionaryCode(string dataDictionaryCode, string connectionId = null)
        {
            IList<DataDictionaryItemInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql("DDI.")} INNER JOIN data_dictionary DD" +
                $" ON DD.id=DDI.data_dictionary_id AND DD.code=@DataDictionaryCode";
                result = dbConn.Query<DataDictionaryItemInfo>(sql, new { DataDictionaryCode = dataDictionaryCode }).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        #region 重写父类的方法

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected override IDictionary<string, string> SlaveTables()
        {
            return new Dictionary<string, string>()
            {
                { "data_dictionary_item_expand", "data_dictionary_item_id" }
            };
        }

        /// <summary>
        /// 追加查询分页SQL
        /// </summary>
        /// <param name="whereSql">where语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        protected override void AppendSelectPageWhereSql(StringBuilder whereSql, DynamicParameters parameters, FilterInfo filter = null)
        {
            if (filter is DataDictionaryItemFilterInfo)
            {
                DataDictionaryItemFilterInfo dataFilter = filter as DataDictionaryItemFilterInfo;
                whereSql.AppendFormat(" AND [{0}]=@DataDictionaryId", GetFieldByProp("DataDictionaryId"));
                parameters.Add("@DataDictionaryId", dataFilter.DataDictionaryId);
            }
        }

        #endregion
    }
}
