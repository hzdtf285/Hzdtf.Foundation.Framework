﻿using Hzdtf.AAG.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.AAG.Persistence.Contract.Standard;

namespace Hzdtf.AAG.MySql.Standard
{
    /// <summary>
    /// 其他业务_操作记录持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class OtherBusinessOperRecordPersistence : MySqlDapperBase<OtherBusinessOperRecordInfo>, IOtherBusinessOperRecordPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "other_business_oper_record";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "content",
            "create_time",
            "creater",
            "creater_id",
            "modifier",
            "modifier_id",
            "modify_time",
            "other_business_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "content",
            "modifier",
            "modifier_id",
            "modify_time",
            "other_business_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "content Content",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "id Id",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "other_business_id OtherBusinessId",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(OtherBusinessOperRecordInfo model, string field)
        {
            switch (field)
            {
﻿                case "content":
                    return model.Content;

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

﻿                case "other_business_id":
                    return model.OtherBusinessId;

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
