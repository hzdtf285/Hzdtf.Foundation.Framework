﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 账单结算持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class BillSettlePersistence : MySqlDapperBase<BillSettleInfo>, IBillSettlePersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "bill_settle";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "account_leader",
            "account_leader_id",
            "auto_builder",
            "bill_month",
            "business_leader",
            "business_leader_id",
            "contract_customer_id",
            "cost_amount",
            "create_time",
            "creater",
            "creater_id",
            "discount_amount",
            "invoice",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "payee_amount",
            "payee_leader",
            "payee_leader_id",
            "reconciliation_desc",
            "settle_date",
            "settle_status",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "account_leader",
            "account_leader_id",
            "auto_builder",
            "bill_month",
            "business_leader",
            "business_leader_id",
            "contract_customer_id",
            "cost_amount",
            "discount_amount",
            "invoice",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "payee_amount",
            "payee_leader",
            "payee_leader_id",
            "reconciliation_desc",
            "settle_date",
            "settle_status",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "account_leader AccountLeader",
            "account_leader_id AccountLeaderId",
            "auto_builder AutoBuilder",
            "bill_month BillMonth",
            "business_leader BusinessLeader",
            "business_leader_id BusinessLeaderId",
            "contract_customer_id ContractCustomerId",
            "cost_amount CostAmount",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "discount_amount DiscountAmount",
            "id Id",
            "invoice Invoice",
            "memo Memo",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "payee_amount PayeeAmount",
            "payee_leader PayeeLeader",
            "payee_leader_id PayeeLeaderId",
            "reconciliation_desc ReconciliationDesc",
            "settle_date SettleDate",
            "settle_status SettleStatus",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(BillSettleInfo model, string field)
        {
            switch (field)
            {
﻿                case "account_leader":
                    return model.AccountLeader;

﻿                case "account_leader_id":
                    return model.AccountLeaderId;

﻿                case "auto_builder":
                    return model.AutoBuilder;

﻿                case "bill_month":
                    return model.BillMonth;

﻿                case "business_leader":
                    return model.BusinessLeader;

﻿                case "business_leader_id":
                    return model.BusinessLeaderId;

﻿                case "contract_customer_id":
                    return model.ContractCustomerId;

﻿                case "cost_amount":
                    return model.CostAmount;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "discount_amount":
                    return model.DiscountAmount;

﻿                case "id":
                    return model.Id;

﻿                case "invoice":
                    return model.Invoice;

﻿                case "memo":
                    return model.Memo;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "payee_amount":
                    return model.PayeeAmount;

﻿                case "payee_leader":
                    return model.PayeeLeader;

﻿                case "payee_leader_id":
                    return model.PayeeLeaderId;

﻿                case "reconciliation_desc":
                    return model.ReconciliationDesc;

﻿                case "settle_date":
                    return model.SettleDate;

﻿                case "settle_status":
                    return model.SettleStatus;

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
