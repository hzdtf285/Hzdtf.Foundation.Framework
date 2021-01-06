using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 序列持久化
    /// @ 黄振东
    /// </summary>
    public partial class SequencePersistence
    {
        /// <summary>
        /// 根据序列类型查询序列信息
        /// </summary>
        /// <param name="seqType">序列类型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>序列信息</returns>
        public SequenceInfo SelectBySeqType(string seqType, string connectionId = null)
        {
            SequenceInfo result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{BasicSelectSql()} WHERE {GetFieldByProp("SeqType")}=@SeqType";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectBySeqType");
                result = dbConn.QueryFirstOrDefault<SequenceInfo>(sql, new { SeqType = seqType });
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID更新增量
        /// </summary>
        /// <param name="sequenceInfo">序列信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int UpdateIncrementById(SequenceInfo sequenceInfo, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"UPDATE `{Table}` SET `{GetFieldByProp("Increment")}`=@Increment,`{GetFieldByProp("UpdateDate")}`=@UpdateDate{GetModifyInfoSql(sequenceInfo)} WHERE {GetFieldByProp("Id") }=@Id";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "UpdateIncrementById");
                result = dbConn.Execute(sql, sequenceInfo, GetDbTransaction(connId));
            });

            return result;
        }
    }
}
