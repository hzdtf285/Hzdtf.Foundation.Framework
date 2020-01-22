﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.Persistence.MySql.Standard
{
    /// <summary>
    /// 用户角色持久化
    /// </summary>
    [Inject]
    public partial class UserRolePersistence : MySqlDapperBase<UserRoleInfo>, IUserRolePersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected override string Table => "user_role";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "create_time",
            "creater",
            "creater_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "role_id",
            "system_inlay",
            "user_id",

        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "modifier",
            "modifier_id",
            "modify_time",
            "role_id",
            "system_inlay",
            "user_id",

        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "id Id",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "role_id RoleId",
            "system_inlay SystemInlay",
            "user_id UserId",

        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(UserRoleInfo model, string field)
        {
            switch (field)
            {
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

﻿                case "role_id":
                    return model.RoleId;

﻿                case "system_inlay":
                    return model.SystemInlay;

﻿                case "user_id":
                    return model.UserId;


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
