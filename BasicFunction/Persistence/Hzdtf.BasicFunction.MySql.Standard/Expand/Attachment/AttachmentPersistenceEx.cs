using Dapper;
using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 附件持久化
    /// @ 黄振东
    /// </summary>
    public partial class AttachmentPersistence
    {
        /// <summary>
        /// 根据归属查询附件列表
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>附件列表</returns>
        public IList<AttachmentInfo> SelectByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null)
        {
            IList<AttachmentInfo> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{SelectSql()} WHERE owner_type=@OwnerType AND owner_id=@OwnerId";
                if (!string.IsNullOrWhiteSpace(blurTitle))
                {
                    sql += string.Format(" AND title LIKE '%{0}%'", blurTitle.FillSqlValue());
                }

                Log.TraceAsync(sql, source: this.GetType().Name, tags: "SelectByOwner");
                result = dbConn.Query<AttachmentInfo>(sql, new { OwnerType = ownerType, OwnerId = ownerId }, GetDbTransaction(connId, AccessMode.SLAVE)).AsList();
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据归属查询统计附件个数
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="blurTitle">模糊标题</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>附件个数</returns>
        public int CountByOwner(short ownerType, int ownerId, string blurTitle = null, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{CountSql()} WHERE owner_type=@OwnerType AND owner_id=@OwnerId";
                if (!string.IsNullOrWhiteSpace(blurTitle))
                {
                    sql += string.Format(" AND title LIKE '%{0}%'", blurTitle.FillSqlValue());
                }

                Log.TraceAsync(sql, source: this.GetType().Name, tags: "CountByOwner");
                result = dbConn.ExecuteScalar<int>(sql, new { OwnerType = ownerType, OwnerId = ownerId }, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据归属删除
        /// </summary>
        /// <param name="ownerType">归属类型</param>
        /// <param name="ownerId">归属ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public int DeleteByOwner(short ownerType, int ownerId, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                string sql = $"{DeleteSql()} WHERE owner_type=@OwnerType AND owner_id=@OwnerId";
                Log.TraceAsync(sql, source: this.GetType().Name, tags: "DeleteByOwner");
                result = dbConn.Execute(sql, new { OwnerType = ownerType, OwnerId = ownerId }, GetDbTransaction(connId));
            });

            return result;
        }

        #region 重写父类的方法

        /// <summary>
        /// 追加查询分页SQL
        /// </summary>
        /// <param name="whereSql">where语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        protected override void AppendSelectPageWhereSql(StringBuilder whereSql, DynamicParameters parameters, FilterInfo filter = null)
        {
            if (filter is AttachmentFilterInfo)
            {
                AttachmentFilterInfo attachFilter = filter as AttachmentFilterInfo;
                whereSql.AppendFormat(" AND `{0}`=@OwnerType", GetFieldByProp("OwnerType"));
                parameters.Add("@OwnerType", attachFilter.OwnerType);

                if (attachFilter.OwnerId != null)
                {
                    whereSql.AppendFormat(" AND `{0}`=@OwnerId", GetFieldByProp("OwnerId"));
                    parameters.Add("@OwnerId", attachFilter.OwnerId);
                }
                if (!string.IsNullOrWhiteSpace(attachFilter.BlurTitle))
                {
                    whereSql.AppendFormat(" AND `{0}` LIKE '%{1}%'", GetFieldByProp("Title"), attachFilter.BlurTitle.FillSqlValue());
                }
            }
        }

        #endregion
    }
}
