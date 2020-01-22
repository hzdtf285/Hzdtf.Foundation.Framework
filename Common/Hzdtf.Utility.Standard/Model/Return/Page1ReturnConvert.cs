using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Return
{
    /// <summary>
    /// 分页从1开始的返回转换
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class Page1ReturnConvert : IPagingReturnConvert
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="RowT">行类型</typeparam>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>转换后的数据</returns>
        public object Convert<RowT>(ReturnInfo<PagingInfo<RowT>> returnInfo) => Page1ReturnInfo<RowT>.From(returnInfo);
    }
}
