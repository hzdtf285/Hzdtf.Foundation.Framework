using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class FilterInfo
    {
        /// <summary>
        /// 开始创建时间
        /// </summary>
        [JsonProperty("startCreateTime")]
        [MessagePack.Key("startCreateTime")]
        public DateTime? StartCreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束创建时间
        /// </summary>
        [JsonProperty("endCreateTime")]
        [MessagePack.Key("endCreateTime")]
        public DateTime? EndCreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("sort")]
        [MessagePack.Key("sort")]
        public SortEnum Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 排序名称
        /// </summary>
        [JsonProperty("sortName")]
        [MessagePack.Key("sortName")]
        public string SortName
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 排序枚举
    /// </summary>
    public enum SortEnum : byte
    {
        /// <summary>
        /// 升序
        /// </summary>
        ASC = 0,

        /// <summary>
        /// 降序
        /// </summary>
        DESC = 1
    }
}
