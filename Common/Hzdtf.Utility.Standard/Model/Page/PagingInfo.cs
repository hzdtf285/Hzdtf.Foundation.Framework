using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Page
{
    /// <summary>
    /// 分页信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="RowT">行类型</typeparam>
    [MessagePackObject]
    public class PagingInfo<RowT>
    {
        /// <summary>
        /// 列表行
        /// </summary>
        [JsonProperty("rows")]
        [Key("rows")]
        public IList<RowT> Rows
        {
            get;
            set;
        }

        /// <summary>
        /// 页码（从0开始）
        /// </summary>
        [JsonProperty("pageIndex")]
        [Key("pageIndex")]
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 每页记录数
        /// </summary>
        [JsonProperty("pageSize")]
        [Key("pageSize")]
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("records")]
        [Key("records")]
        public int Records
        {
            get;
            set;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("pageCount")]
        [Key("pageCount")]
        public int PageCount
        {
            get => PagingUtil.PageCount(PageSize, Records);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => JsonUtil.SerializeIgnoreNull(this);
    }
}
