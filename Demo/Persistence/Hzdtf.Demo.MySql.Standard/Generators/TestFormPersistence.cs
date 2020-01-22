﻿using Hzdtf.Demo.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.Demo.Persistence.Contract.Standard;

namespace Hzdtf.Demo.MySql.Standard
{
    /// <summary>
    /// 测试表单持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class TestFormPersistence : MySqlDapperBase<TestFormInfo>, ITestFormPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "test_form";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "flow_status",
            "workflow_id",
            "code",
            "name",
            "creater_id",
            "creater",
            "create_time",
            "modifier_id",
            "modifier",
            "modify_time",
            "apply_no",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "flow_status",
            "workflow_id",
            "code",
            "name",
            "modifier_id",
            "modifier",
            "modify_time",
            "apply_no",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "flow_status FlowStatus",
            "workflow_id WorkflowId",
            "code Code",
            "name Name",
            "creater_id CreaterId",
            "creater Creater",
            "create_time CreateTime",
            "modifier_id ModifierId",
            "modifier Modifier",
            "modify_time ModifyTime",
            "id Id",
            "apply_no ApplyNo",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(TestFormInfo model, string field)
        {
            switch (field)
            {
﻿                case "flow_status":
                    return model.FlowStatus;

﻿                case "workflow_id":
                    return model.WorkflowId;

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

﻿                case "id":
                    return model.Id;

﻿                case "apply_no":
                    return model.ApplyNo;

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
