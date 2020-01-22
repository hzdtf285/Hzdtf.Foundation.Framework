﻿using hzdtd.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using hzdtd.Persistence.Contract.Standard;

namespace hzdtd.MySql.Standard
{
    /// <summary>
    /// 业务异常记录持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class BusinessExceptionRecordPersistence : MySqlDapperBase<BusinessExceptionRecordInfo>, IBusinessExceptionRecordPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "business_exception_record";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "create_time",
            "desc",
            "exception_message",
            "exception_string",
            "exchange",
            "queue",
            "queue_message",
            "server_ip",
            "server_machine_name",
            "service_name",
            "time",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "desc",
            "exception_message",
            "exception_string",
            "exchange",
            "queue",
            "queue_message",
            "server_ip",
            "server_machine_name",
            "service_name",
            "time",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "create_time CreateTime",
            "desc Desc",
            "exception_message ExceptionMessage",
            "exception_string ExceptionString",
            "exchange Exchange",
            "id Id",
            "queue Queue",
            "queue_message QueueMessage",
            "server_ip ServerIp",
            "server_machine_name ServerMachineName",
            "service_name ServiceName",
            "time Time",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(BusinessExceptionRecordInfo model, string field)
        {
            switch (field)
            {
﻿                case "create_time":
                    return model.CreateTime;

﻿                case "desc":
                    return model.Desc;

﻿                case "exception_message":
                    return model.ExceptionMessage;

﻿                case "exception_string":
                    return model.ExceptionString;

﻿                case "exchange":
                    return model.Exchange;

﻿                case "id":
                    return model.Id;

﻿                case "queue":
                    return model.Queue;

﻿                case "queue_message":
                    return model.QueueMessage;

﻿                case "server_ip":
                    return model.ServerIp;

﻿                case "server_machine_name":
                    return model.ServerMachineName;

﻿                case "service_name":
                    return model.ServiceName;

﻿                case "time":
                    return model.Time;

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
