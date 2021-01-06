﻿﻿using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;

namespace Hzdtf.WorkFlow.MySql.Standard
{
    /// <summary>
    /// 表单持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class FormPersistence : MySqlDapperBase<int, FormInfo>, IFormPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "form";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "create_time",
            "creater",
            "creater_id",
            "form_url",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "form_get_detail_url"
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "form_url",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "form_get_detail_url"
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "form_url FormUrl",
            "id Id",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "name Name",
            "form_get_detail_url FormGetDetailUrl",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(FormInfo model, string field)
        {
            switch (field)
            {
﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "form_url":
                    return model.FormUrl;

﻿                case "id":
                    return model.Id;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "name":
                    return model.Name;

                case "form_get_detail_url":
                    return model.FormGetDetailUrl;

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
