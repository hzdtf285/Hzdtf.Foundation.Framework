using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Return
{
    /// <summary>
    /// 分页从1开始的返回信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="RowT">行类型</typeparam>
    [MessagePackObject]
    public class Page1ReturnInfo<RowT> : BasicReturnInfo
    {
        /// <summary>
        /// 基本全称
        /// 除了泛型的组合
        /// </summary>
        public new const string BASIC_FULL_NAME = "Hzdtf.Utility.Standard.Model.Return.Page1ReturnInfo";

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
        /// 页码（从1开始）
        /// </summary>
        [JsonProperty("page")]
        [Key("page")]
        public int Page
        {
            get;
            set;
        } = 1;

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
        [JsonProperty("total")]
        [Key("total")]
        public int Total
        {
            get => PagingUtil.PageCount(PageSize, Records);
        }

        /// <summary>
        /// 从分页信息转换为分页从1开始的返回信息
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>分页从1开始的返回信息</returns>
        public static Page1ReturnInfo<RowT> From(ReturnInfo<PagingInfo<RowT>> returnInfo)
        {
            if (returnInfo == null)
            {
                return null;
            }

            Page1ReturnInfo<RowT> page1Return = new Page1ReturnInfo<RowT>();
            page1Return.SetCodeMsg(returnInfo.Code, returnInfo.Msg, returnInfo.Desc);

            if (returnInfo.Data != null)
            {
                page1Return.Page = returnInfo.Data.PageIndex + 1;
                page1Return.PageSize = returnInfo.Data.PageSize;
                page1Return.Records = returnInfo.Data.Records;
                page1Return.Rows = returnInfo.Data.Rows;
            }

            return page1Return;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => JsonUtil.SerializeIgnoreNull(this);

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
