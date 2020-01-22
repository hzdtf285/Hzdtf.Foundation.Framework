﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.SqlServer.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 菜单持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class MenuPersistence : SqlServerDapperBase<MenuInfo>, IMenuPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "menu";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "link",
            "icon",
            "parent_id",
            "sort",
            "code",
            "name",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "link",
            "icon",
            "parent_id",
            "sort",
            "code",
            "name",
            "modifier_id",
            "modifier",
            "modify_time",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "link Link",
            "icon Icon",
            "parent_id ParentId",
            "sort Sort",
            "code Code",
            "name Name",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(MenuInfo model, string field)
        {
            switch (field)
            {
﻿                case "id":
                    return model.Id;

﻿                case "link":
                    return model.Link;

﻿                case "icon":
                    return model.Icon;

﻿                case "parent_id":
                    return model.ParentId;

﻿                case "sort":
                    return model.Sort;

﻿                case "code":
                    return model.Code;

﻿                case "name":
                    return model.Name;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "creater":
                    return model.Creater;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modify_time":
                    return model.ModifyTime;

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
