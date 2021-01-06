﻿using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;

namespace Hzdtf.WorkFlow.MySql.Standard
{
    /// <summary>
    /// 工作流持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowPersistence : MySqlDapperBase<int, WorkflowInfo>, IWorkflowPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "workflow";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "apply_no",
            "create_time",
            "creater",
            "creater_id",
            "curr_concrete_censorship_ids",
            "curr_concrete_censorships",
            "curr_flow_censorship_ids",
            "curr_handler_ids",
            "curr_handlers",
            "flow_status",
            "modifier",
            "modifier_id",
            "modify_time",
            "title",
            "workflow_define_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "apply_no",
            "curr_concrete_censorship_ids",
            "curr_concrete_censorships",
            "curr_flow_censorship_ids",
            "curr_handler_ids",
            "curr_handlers",
            "flow_status",
            "modifier",
            "modifier_id",
            "modify_time",
            "title",
            "workflow_define_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "apply_no ApplyNo",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "curr_concrete_censorship_ids CurrConcreteCensorshipIds",
            "curr_concrete_censorships CurrConcreteCensorships",
            "curr_flow_censorship_ids CurrFlowCensorshipIds",
            "curr_handler_ids CurrHandlerIds",
            "curr_handlers CurrHandlers",
            "flow_status FlowStatus",
            "id Id",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "title Title",
            "workflow_define_id WorkflowDefineId",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(WorkflowInfo model, string field)
        {
            switch (field)
            {
﻿                case "apply_no":
                    return model.ApplyNo;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "curr_concrete_censorship_ids":
                    return model.CurrConcreteCensorshipIds;

﻿                case "curr_concrete_censorships":
                    return model.CurrConcreteCensorships;

﻿                case "curr_flow_censorship_ids":
                    return model.CurrFlowCensorshipIds;

﻿                case "curr_handler_ids":
                    return model.CurrHandlerIds;

﻿                case "curr_handlers":
                    return model.CurrHandlers;

﻿                case "flow_status":
                    return model.FlowStatus;

﻿                case "id":
                    return model.Id;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "title":
                    return model.Title;

﻿                case "workflow_define_id":
                    return model.WorkflowDefineId;

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
