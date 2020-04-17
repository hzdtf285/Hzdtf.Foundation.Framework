using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Return
{
    /// <summary>
    /// 返回信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="DataT">数据类型</typeparam>
    [MessagePackObject]
    public class ReturnInfo<DataT> : BasicReturnInfo
    {
        /// <summary>
        /// 基本全称
        /// 除了泛型的组合
        /// </summary>
        public new const string BASIC_FULL_NAME = "Hzdtf.Utility.Standard.Model.Return.ReturnInfo";

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty("data")]
        [Key("data")]
        public DataT Data
        {
            get;
            set;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ReturnInfo() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isToJsonString">是否转换为JSON</param>
        public ReturnInfo(bool isToJsonString) : base(isToJsonString) { }

        /// <summary>
        /// 是否本身类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否本身类型</returns>
        public new static bool IsThisType(Type type)
        {
            return type != null ? BASIC_FULL_NAME.Equals(type.FullName) : false;
        }
    }
}
