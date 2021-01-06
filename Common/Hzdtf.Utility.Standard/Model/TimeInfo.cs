using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 带有时间的信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    [MessagePackObject]
    public class TimeInfo<IdT> : SimpleInfo<IdT>
    {
        /// <summary>
        /// 创建时间_名称
        /// </summary>
        public const string CreateTime_Name = "CreateTime";

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("createTime")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("createTime")]
        public DateTime CreateTime
        {
            get;
            set;
        } = DateTime.Now;

        /// <summary>
        /// 修改时间_名称
        /// </summary>
        public const string ModifyTime_Name = "ModifyTime";

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonProperty("modifyTime")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("modifyTime")]
        public DateTime ModifyTime
        {
            get;
            set;
        } = DateTime.Now;
    }

    /// <summary>
    /// 带有时间的信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class TimeInfo : TimeInfo<int>
    {
    }
}
