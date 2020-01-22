﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.Persistence.MySql.Standard
{
    /// <summary>
    /// 角色持久化
    /// </summary>
    [Inject]
    public partial class RolePersistence : MySqlDapperBase<RoleInfo>, IRolePersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected override string Table => "role";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "code",
            "create_time",
            "creater",
            "creater_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "remark",
            "system_hide",
            "system_inlay",

        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "code",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "remark",
            "system_hide",
            "system_inlay",

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
            "id Id",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "name Name",
            "remark Remark",
            "system_hide SystemHide",
            "system_inlay SystemInlay",

        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(RoleInfo model, string field)
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

﻿                case "remark":
                    return model.Remark;

﻿                case "system_hide":
                    return model.SystemHide;

﻿                case "system_inlay":
                    return model.SystemInlay;


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
        protected override string[] AllFieldMapProps() => FIELD_MAP_PROPS;
    }
}
