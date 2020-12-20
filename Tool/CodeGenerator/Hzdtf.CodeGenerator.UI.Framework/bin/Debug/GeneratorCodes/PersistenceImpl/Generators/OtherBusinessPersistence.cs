﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 其他业务持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class OtherBusinessPersistence : MySqlDapperBase<OtherBusinessInfo>, IOtherBusinessPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "other_business";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "account_leader",
            "account_leader_id",
            "busine_desc_claim",
            "business_leader",
            "business_leader_id",
            "business_type",
            "claim",
            "contract_amount",
            "create_time",
            "creater",
            "creater_id",
            "customer_id",
            "discount_amount",
            "is_end",
            "is_exists_contract",
            "legwork_leader",
            "legwork_leader_id",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "money_peceipt_amount",
            "money_receipt_desc",
            "pack_data",
            "progress",
            "sign_bill_date",
            "status",
            "status_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "account_leader",
            "account_leader_id",
            "busine_desc_claim",
            "business_leader",
            "business_leader_id",
            "business_type",
            "claim",
            "contract_amount",
            "customer_id",
            "discount_amount",
            "is_end",
            "is_exists_contract",
            "legwork_leader",
            "legwork_leader_id",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "money_peceipt_amount",
            "money_receipt_desc",
            "pack_data",
            "progress",
            "sign_bill_date",
            "status",
            "status_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "account_leader AccountLeader",
            "account_leader_id AccountLeaderId",
            "busine_desc_claim BusineDescClaim",
            "business_leader BusinessLeader",
            "business_leader_id BusinessLeaderId",
            "business_type BusinessType",
            "claim Claim",
            "contract_amount ContractAmount",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "customer_id CustomerId",
            "discount_amount DiscountAmount",
            "id Id",
            "is_end IsEnd",
            "is_exists_contract IsExistsContract",
            "legwork_leader LegworkLeader",
            "legwork_leader_id LegworkLeaderId",
            "memo Memo",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "money_peceipt_amount MoneyPeceiptAmount",
            "money_receipt_desc MoneyReceiptDesc",
            "pack_data PackData",
            "progress Progress",
            "sign_bill_date SignBillDate",
            "status Status",
            "status_id StatusId",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(OtherBusinessInfo model, string field)
        {
            switch (field)
            {
﻿                case "account_leader":
                    return model.AccountLeader;

﻿                case "account_leader_id":
                    return model.AccountLeaderId;

﻿                case "busine_desc_claim":
                    return model.BusineDescClaim;

﻿                case "business_leader":
                    return model.BusinessLeader;

﻿                case "business_leader_id":
                    return model.BusinessLeaderId;

﻿                case "business_type":
                    return model.BusinessType;

﻿                case "claim":
                    return model.Claim;

﻿                case "contract_amount":
                    return model.ContractAmount;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "customer_id":
                    return model.CustomerId;

﻿                case "discount_amount":
                    return model.DiscountAmount;

﻿                case "id":
                    return model.Id;

﻿                case "is_end":
                    return model.IsEnd;

﻿                case "is_exists_contract":
                    return model.IsExistsContract;

﻿                case "legwork_leader":
                    return model.LegworkLeader;

﻿                case "legwork_leader_id":
                    return model.LegworkLeaderId;

﻿                case "memo":
                    return model.Memo;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "money_peceipt_amount":
                    return model.MoneyPeceiptAmount;

﻿                case "money_receipt_desc":
                    return model.MoneyReceiptDesc;

﻿                case "pack_data":
                    return model.PackData;

﻿                case "progress":
                    return model.Progress;

﻿                case "sign_bill_date":
                    return model.SignBillDate;

﻿                case "status":
                    return model.Status;

﻿                case "status_id":
                    return model.StatusId;

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
