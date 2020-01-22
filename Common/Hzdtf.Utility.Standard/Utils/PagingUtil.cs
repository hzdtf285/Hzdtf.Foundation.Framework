using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 分页辅助类
    /// @ 黄振东
    /// </summary>
    public static class PagingUtil
    {
        /// <summary>
        /// 计算分页总数
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总数</param>
        /// <returns>分页总数</returns>
        public static int PageCount(int pageSize, int total)
        {
            if (pageSize <= 0)
            {
                return 0;
            }
            int pageCount = total / pageSize;
            if (total % pageSize != 0)
            {
                pageCount++;
            }

            return pageCount;
        }

        /// <summary>
        /// 计算分页开始结束数
        /// </summary>
        /// <param name="pageIndex">页码，从0开始</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="baseStartNum">基层开始数</param>
        /// <returns>分页开始结束数</returns>
        public static int[] PageStartEnd(int pageIndex, int pageSize, int baseStartNum = 0)
        {
            int[] result = new int[2];
            result[0] = pageIndex * pageSize;
            result[1] = result[0] + pageSize;
            result[0] += baseStartNum;

            return result;
        }

        /// <summary>
        /// 按指定数据类型执行分页函数
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="countFunc">统计数函数</param>
        /// <param name="selectPageFunc">查询分页函数</param>
        /// <returns>分页信息</returns>
        public static PagingInfo<DataT> ExecPage<DataT>(int pageIndex, int pageSize, Func<int> countFunc, Func<IList<DataT>> selectPageFunc)
        {
            PagingInfo<DataT> pagingInfo = new PagingInfo<DataT>();
            // 先执行统计，如果为0则不用再往下查询，提高性能
            int count = countFunc();
            if (count == 0)
            {
                return pagingInfo;
            }

            pagingInfo.PageIndex = pageIndex;
            pagingInfo.PageSize = pageSize;
            pagingInfo.Records = count;
            pagingInfo.Rows = selectPageFunc();

            return pagingInfo;
        }
    }
}
