﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Persistence.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.Persistence.MySql.Standard
{
    /// <summary>
    /// 数据字典子项持久化
    /// </summary>
    [Inject]
    public partial class DataDictionaryItemPersistence : MySqlDapperBase<DataDictionaryItemInfo>, IDataDictionaryItemPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected override string Table => "data_dictionary_item";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "create_time",
            "creater",
            "creater_id",
            "data_dictionary_id",
            "key",
            "modifier",
            "modifier_id",
            "modify_time",
            "system_inlay",
            "value",

        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "data_dictionary_id",
            "key",
            "modifier",
            "modifier_id",
            "modify_time",
            "system_inlay",
            "value",

        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "data_dictionary_id DataDictionaryId",
            "id Id",
            "key Key",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "system_inlay SystemInlay",
            "value Value",

        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(DataDictionaryItemInfo model, string field)
        {
            switch (field)
            {
﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "data_dictionary_id":
                    return model.DataDictionaryId;

﻿                case "id":
                    return model.Id;

﻿                case "key":
                    return model.Key;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "system_inlay":
                    return model.SystemInlay;

﻿                case "value":
                    return model.Value;


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
