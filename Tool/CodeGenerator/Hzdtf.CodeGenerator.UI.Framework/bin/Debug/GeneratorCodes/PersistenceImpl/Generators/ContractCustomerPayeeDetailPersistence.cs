﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 合约客户_收款明细持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class ContractCustomerPayeeDetailPersistence : MySqlDapperBase<ContractCustomerPayeeDetailInfo>, IContractCustomerPayeeDetailPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "contract_customer_payee_detail";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "apply_no",
            "contract_customer_id",
            "create_time",
            "creater",
            "creater_id",
            "discount_amount",
            "flow_status",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "payee_amount",
            "payee_date",
            "payee_leader",
            "payee_leader_id",
            "settle_end_month",
            "settle_start_month",
            "workflow_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "apply_no",
            "contract_customer_id",
            "discount_amount",
            "flow_status",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "payee_amount",
            "payee_date",
            "payee_leader",
            "payee_leader_id",
            "settle_end_month",
            "settle_start_month",
            "workflow_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "apply_no ApplyNo",
            "contract_customer_id ContractCustomerId",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "discount_amount DiscountAmount",
            "flow_status FlowStatus",
            "id Id",
            "memo Memo",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "payee_amount PayeeAmount",
            "payee_date PayeeDate",
            "payee_leader PayeeLeader",
            "payee_leader_id PayeeLeaderId",
            "settle_end_month SettleEndMonth",
            "settle_start_month SettleStartMonth",
            "workflow_id WorkflowId",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(ContractCustomerPayeeDetailInfo model, string field)
        {
            switch (field)
            {
﻿                case "apply_no":
                    return model.ApplyNo;

﻿                case "contract_customer_id":
                    return model.ContractCustomerId;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "discount_amount":
                    return model.DiscountAmount;

﻿                case "flow_status":
                    return model.FlowStatus;

﻿                case "id":
                    return model.Id;

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

﻿                case "payee_date":
                    return model.PayeeDate;

﻿                case "payee_leader":
                    return model.PayeeLeader;

﻿                case "payee_leader_id":
                    return model.PayeeLeaderId;

﻿                case "settle_end_month":
                    return model.SettleEndMonth;

﻿                case "settle_start_month":
                    return model.SettleStartMonth;

﻿                case "workflow_id":
                    return model.WorkflowId;

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
