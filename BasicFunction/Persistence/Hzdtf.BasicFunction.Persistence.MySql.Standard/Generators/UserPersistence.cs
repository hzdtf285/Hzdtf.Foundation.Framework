﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.Persistence.MySql.Standard
{
    /// <summary>
    /// 用户持久化
    /// </summary>
    [Inject]
    public partial class UserPersistence : MySqlDapperBase<UserInfo>, IUserPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected override string Table => "user";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "code",
            "create_time",
            "creater",
            "creater_id",
            "enabled",
            "login_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "password",
            "remark",
            "sex",
            "system_hide",
            "system_inlay",

        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "code",
            "enabled",
            "login_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "password",
            "remark",
            "sex",
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
            "enabled Enabled",
            "id Id",
            "login_id LoginId",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "name Name",
            "password Password",
            "remark Remark",
            "sex Sex",
            "system_hide SystemHide",
            "system_inlay SystemInlay",

        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(UserInfo model, string field)
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

﻿                case "enabled":
                    return model.Enabled;

﻿                case "id":
                    return model.Id;

﻿                case "login_id":
                    return model.LoginId;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "name":
                    return model.Name;

﻿                case "password":
                    return model.Password;

﻿                case "remark":
                    return model.Remark;

﻿                case "sex":
                    return model.Sex;

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
