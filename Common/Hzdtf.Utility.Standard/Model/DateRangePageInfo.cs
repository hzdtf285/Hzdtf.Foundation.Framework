using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 日期范围页信息，默认为1个月内
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class DateRangePageInfo : PageInfo
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [JsonProperty("startDate")]
        [MessagePack.Key("startDate")]
        public DateTime? StartDate
        {
            get;
            set;
        } = DateTime.Now.AddMonths(-1);

        /// <summary>
        /// 结束日期
        /// </summary>
        [JsonProperty("endDate")]
        [MessagePack.Key("endDate")]
        public DateTime? EndDate
        {
            get;
            set;
        } = DateTime.Now;
    }
}
