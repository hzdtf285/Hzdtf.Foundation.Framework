using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Return
{
    /// <summary>
    /// 默认返回信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class DefaultReturnInfo : ReturnInfo<string>
    {
        /// <summary>
        /// 基本全称
        /// </summary>
        public new const string BASIC_FULL_NAME = "Hzdtf.Utility.Standard.Model.Return.DefaultReturnInfo";

        /// <summary>
        /// 是否本身类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否本身类型</returns>
        public new static bool IsThisType(Type type)
        {
            return type != null ? BASIC_FULL_NAME.Equals(type.FullName) : false;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public DefaultReturnInfo() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isToJsonString">是否转换为JSON</param>
        public DefaultReturnInfo(bool isToJsonString) : base(isToJsonString) { }
    }
}
