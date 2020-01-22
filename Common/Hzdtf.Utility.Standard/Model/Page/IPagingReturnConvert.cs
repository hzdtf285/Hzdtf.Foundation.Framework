using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Page
{
    /// <summary>
    /// 分页返回转换接口
    /// @ 黄振东
    /// </summary>
    public interface IPagingReturnConvert
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="RowT">行类型</typeparam>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>转换后的数据</returns>
        object Convert<RowT>(ReturnInfo<PagingInfo<RowT>> returnInfo);
    }
}
