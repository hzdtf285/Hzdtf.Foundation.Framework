﻿using hzdtd.Model.Standard;
using Hzdtf.MySql.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using hzdtd.Persistence.Contract.Standard;

namespace hzdtd.MySql.Standard
{
    /// <summary>
    /// 班级持久化
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class ClassesPersistence : MySqlDapperBase<ClassesInfo>, IClassesPersistence
    {
        /// <summary>
        /// 表名
        /// </summary>
        public override string Table => "classes";

        /// <summary>
        /// 插入字段名称集合
        /// </summary>
        private readonly static string[] INSERT_FIELD_NAMES = new string[]
        {
            "curr_serial_no",
            "last_send_time",
        };

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        private readonly static string[] UPDATE_FIELD_NAMES = new string[]
        {
            "curr_serial_no",
            "last_send_time",
        };

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        private readonly static string[] FIELD_MAP_PROPS = new string[]
        {
            "curr_serial_no CurrSerialNo",
            "id Id",
            "last_send_time LastSendTime",
        };

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected override object GetValueByFieldName(ClassesInfo model, string field)
        {
            switch (field)
            {
﻿                case "curr_serial_no":
                    return model.CurrSerialNo;

﻿                case "id":
                    return model.Id;

﻿                case "last_send_time":
                    return model.LastSendTime;

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
