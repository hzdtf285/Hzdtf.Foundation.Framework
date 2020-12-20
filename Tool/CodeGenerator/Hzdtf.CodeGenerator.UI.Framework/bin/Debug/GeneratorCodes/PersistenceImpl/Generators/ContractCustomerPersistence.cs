﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 合约客户持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class ContractCustomerPersistence : MySqlDapperBase<ContractCustomerInfo>, IContractCustomerPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "contract_customer";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "code",
            "create_time",
            "creater",
            "creater_id",
            "customer_id",
            "invoice",
            "is_auto_build_billed",
            "is_exists_contract",
            "last_auto_build_bill_date",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "service_end_month",
            "service_start_month",
            "settle_price",
            "settle_unit",
            "sign_bill_date",
            "status",
            "status_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "code",
            "customer_id",
            "invoice",
            "is_auto_build_billed",
            "is_exists_contract",
            "last_auto_build_bill_date",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "service_end_month",
            "service_start_month",
            "settle_price",
            "settle_unit",
            "sign_bill_date",
            "status",
            "status_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "code Code",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "customer_id CustomerId",
            "id Id",
            "invoice Invoice",
            "is_auto_build_billed IsAutoBuildBilled",
            "is_exists_contract IsExistsContract",
            "last_auto_build_bill_date LastAutoBuildBillDate",
            "memo Memo",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "service_end_month ServiceEndMonth",
            "service_start_month ServiceStartMonth",
            "settle_price SettlePrice",
            "settle_unit SettleUnit",
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
        protected override object GetValueByFieldName(ContractCustomerInfo model, string field)
        {
            switch (field)
            {
﻿                case "code":
                    return model.Code;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "customer_id":
                    return model.CustomerId;

﻿                case "id":
                    return model.Id;

﻿                case "invoice":
                    return model.Invoice;

﻿                case "is_auto_build_billed":
                    return model.IsAutoBuildBilled;

﻿                case "is_exists_contract":
                    return model.IsExistsContract;

﻿                case "last_auto_build_bill_date":
                    return model.LastAutoBuildBillDate;

﻿                case "memo":
                    return model.Memo;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "service_end_month":
                    return model.ServiceEndMonth;

﻿                case "service_start_month":
                    return model.ServiceStartMonth;

﻿                case "settle_price":
                    return model.SettlePrice;

﻿                case "settle_unit":
                    return model.SettleUnit;

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
