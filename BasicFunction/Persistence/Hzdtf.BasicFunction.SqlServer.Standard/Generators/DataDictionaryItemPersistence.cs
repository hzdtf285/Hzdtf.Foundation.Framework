﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.SqlServer.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 数据字典子项持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class DataDictionaryItemPersistence : SqlServerDapperBase<DataDictionaryItemInfo>, IDataDictionaryItemPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "data_dictionary_item";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "code",
            "data_dictionary_id",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
            "text",
            "system_inlay",
            "expand_table",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "code",
            "data_dictionary_id",
            "modifier_id",
            "modifier",
            "modify_time",
            "text",
            "system_inlay",
            "expand_table",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "id Id",
            "data_dictionary_id DataDictionaryId",
            "code Code",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
            "text Text",
            "system_inlay SystemInlay",
            "expand_table ExpandTable",
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
﻿                case "id":
                    return model.Id;

                case "code":
                    return model.Code;

                case "data_dictionary_id":
                    return model.DataDictionaryId;

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

﻿                case "Text":
                    return model.Text;

﻿                case "system_inlay":
                    return model.SystemInlay;

﻿                case "expand_table":
                    return model.ExpandTable;

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
