﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.SqlServer.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;

namespace Hzdtf.BasicFunction.SqlServer.Standard
{
    /// <summary>
    /// 附件持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class AttachmentPersistence : SqlServerDapperBase<int, AttachmentInfo>, IAttachmentPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "attachment";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "create_time",
            "creater",
            "creater_id",
            "expand_name",
            "file_address",
            "file_name",
            "file_size",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "owner_id",
            "owner_type",
            "title",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "expand_name",
            "file_address",
            "file_name",
            "file_size",
            "memo",
            "modifier",
            "modifier_id",
            "modify_time",
            "owner_id",
            "owner_type",
            "title",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "expand_name ExpandName",
            "file_address FileAddress",
            "file_name FileName",
            "file_size FileSize",
            "id Id",
            "memo Memo",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "owner_id OwnerId",
            "owner_type OwnerType",
            "title Title",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(AttachmentInfo model, string field)
        {
            switch (field)
            {
﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "expand_name":
                    return model.ExpandName;

﻿                case "file_address":
                    return model.FileAddress;

﻿                case "file_name":
                    return model.FileName;

﻿                case "file_size":
                    return model.FileSize;

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

﻿                case "owner_id":
                    return model.OwnerId;

﻿                case "owner_type":
                    return model.OwnerType;

﻿                case "title":
                    return model.Title;

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
