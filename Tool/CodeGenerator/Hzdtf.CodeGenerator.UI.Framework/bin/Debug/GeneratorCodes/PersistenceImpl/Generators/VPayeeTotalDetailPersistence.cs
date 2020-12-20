﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 收款统计明细持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class VPayeeTotalDetailPersistence : MySqlDapperBase<VPayeeTotalDetailInfo>, IVPayeeTotalDetailPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "v_payee_total_detail";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "business_type",
            "customer_short_name",
            "payee_amount",
            "payee_date",
            "payee_leader",
            "payee_leader_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "business_type",
            "customer_short_name",
            "payee_amount",
            "payee_date",
            "payee_leader",
            "payee_leader_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "business_type BusinessType",
            "customer_short_name CustomerShortName",
            "payee_amount PayeeAmount",
            "payee_date PayeeDate",
            "payee_leader PayeeLeader",
            "payee_leader_id PayeeLeaderId",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(VPayeeTotalDetailInfo model, string field)
        {
            switch (field)
            {
﻿                case "business_type":
                    return model.BusinessType;

﻿                case "customer_short_name":
                    return model.CustomerShortName;

﻿                case "payee_amount":
                    return model.PayeeAmount;

﻿                case "payee_date":
                    return model.PayeeDate;

﻿                case "payee_leader":
                    return model.PayeeLeader;

﻿                case "payee_leader_id":
                    return model.PayeeLeaderId;

                default:
                    return null;
            }
        }

        /// <summary>
        /// 插入字段名集合
        /// </summary>
        /// <returns>插入字段名集合</returns>
        protected override string[] InsertFieldNames() => INSERT_FIELD_NAMES;

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        /// <returns>更新字段名称集合</returns>
        protected override string[] UpdateFieldNames() => UPDATE_FIELD_NAMES;

		/// <summary>
        /// 所有字段映射集合
        /// </summary>
        /// <returns>所有字段映射集合</returns>
        public override string[] AllFieldMapProps() => FIELD_MAP_PROPS;
    }
}
