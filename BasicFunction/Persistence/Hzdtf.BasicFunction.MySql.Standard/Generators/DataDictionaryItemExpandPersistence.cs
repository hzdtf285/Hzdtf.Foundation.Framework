﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.MySql.Standard
{
    /// <summary>
    /// 数据字典子项扩展持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class DataDictionaryItemExpandPersistence : MySqlDapperBase<DataDictionaryItemExpandInfo>, IDataDictionaryItemExpandPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "data_dictionary_item_expand";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "create_time",
            "creater",
            "creater_id",
            "data_dictionary_item_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "text",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "data_dictionary_item_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "name",
            "text",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "data_dictionary_item_id DataDictionaryItemId",
            "id Id",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "name Name",
            "text Text",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(DataDictionaryItemExpandInfo model, string field)
        {
            switch (field)
            {
﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "data_dictionary_item_id":
                    return model.DataDictionaryItemId;

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

﻿                case "text":
                    return model.Text;

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
