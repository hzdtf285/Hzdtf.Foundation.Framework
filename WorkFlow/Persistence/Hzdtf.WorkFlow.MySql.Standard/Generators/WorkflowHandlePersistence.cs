﻿using Hzdtf.WorkFlow.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using Hzdtf.WorkFlow.Persistence.Contract.Standard;

namespace Hzdtf.WorkFlow.MySql.Standard
{
    /// <summary>
    /// 工作流处理持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class WorkflowHandlePersistence : MySqlDapperBase<int, WorkflowHandleInfo>, IWorkflowHandlePersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "workflow_handle";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "id",
            "concrete_concrete",
            "concrete_concrete_id",
            "create_time",
            "creater",
            "creater_id",
            "flow_censorship_id",
            "handle_status",
            "handle_time",
            "handle_type",
            "handler",
            "handler_id",
            "idea",
            "is_readed",
            "modifier",
            "modifier_id",
            "modify_time",
            "workflow_id",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "concrete_concrete",
            "concrete_concrete_id",
            "flow_censorship_id",
            "handle_status",
            "handle_time",
            "handle_type",
            "handler",
            "handler_id",
            "idea",
            "is_readed",
            "modifier",
            "modifier_id",
            "modify_time",
            "workflow_id",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "concrete_concrete ConcreteConcrete",
            "concrete_concrete_id ConcreteConcreteId",
            "create_time CreateTime",
            "creater Creater",
            "creater_id CreaterId",
            "flow_censorship_id FlowCensorshipId",
            "handle_status HandleStatus",
            "handle_time HandleTime",
            "handle_type HandleType",
            "handler Handler",
            "handler_id HandlerId",
            "id Id",
            "idea Idea",
            "is_readed IsReaded",
            "modifier Modifier",
            "modifier_id ModifierId",
            "modify_time ModifyTime",
            "workflow_id WorkflowId",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(WorkflowHandleInfo model, string field)
        {
            switch (field)
            {
﻿                case "concrete_concrete":
                    return model.ConcreteConcrete;

﻿                case "concrete_concrete_id":
                    return model.ConcreteConcreteId;

﻿                case "create_time":
                    return model.CreateTime;

﻿                case "creater":
                    return model.Creater;

﻿                case "creater_id":
                    return model.CreaterId;

﻿                case "flow_censorship_id":
                    return model.FlowCensorshipId;

﻿                case "handle_status":
                    return model.HandleStatus;

﻿                case "handle_time":
                    return model.HandleTime;

﻿                case "handle_type":
                    return model.HandleType;

﻿                case "handler":
                    return model.Handler;

﻿                case "handler_id":
                    return model.HandlerId;

﻿                case "id":
                    return model.Id;

﻿                case "idea":
                    return model.Idea;

﻿                case "is_readed":
                    return model.IsReaded;

﻿                case "modifier":
                    return model.Modifier;

﻿                case "modifier_id":
                    return model.ModifierId;

﻿                case "modify_time":
                    return model.ModifyTime;

﻿                case "workflow_id":
                    return model.WorkflowId;

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
