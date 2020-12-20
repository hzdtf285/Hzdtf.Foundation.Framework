﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 客户资料持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class CustomerDataPersistence : MySqlDapperBase<CustomerDataInfo>, ICustomerDataPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "customer_data";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "address",
            "code",
            "company_tel",
            "contact",
            "contact_tel",
            "create_time",
            "creater",
            "creater_id",
            "customer_name",
            "customer_short_name",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "status",
            "status_id",
            "uscc",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "address",
            "code",
            "company_tel",
            "contact",
            "contact_tel",
            "customer_name",
            "customer_short_name",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "status",
            "status_id",
            "uscc",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "address Address",
            "code Code",
            "company_tel CompanyTel",
            "contact Contact",
            "contact_tel ContactTel",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "customer_name CustomerName",
            "customer_short_name CustomerShortName",
            "id Id",
            "memo Memo",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "status Status",
            "status_id StatusId",
            "uscc Uscc",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(CustomerDataInfo model, string field)
        {
            switch (field)
            {
﻿                case "address":
                    return model.Address;

﻿                case "code":
                    return model.Code;

﻿                case "company_tel":
                    return model.CompanyTel;

﻿                case "contact":
                    return model.Contact;

﻿                case "contact_tel":
                    return model.ContactTel;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "customer_name":
                    return model.CustomerName;

﻿                case "customer_short_name":
                    return model.CustomerShortName;

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

﻿                case "status":
                    return model.Status;

﻿                case "status_id":
                    return model.StatusId;

﻿                case "uscc":
                    return model.Uscc;

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
