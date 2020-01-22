using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Cache
{
    /// <summary>
    /// 列表缓存接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IListCache<T> : IReaderAll<T>, IClearable
    {
        /// <summary>
        /// 缓存键数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item">选项</param>
        /// <returns>是否添加成功</returns>
        bool Add(T item);

        /// <summary>
        /// 根据选项移除
        /// </summary>
        /// <param name="item">选项</param>
        /// <returns>是否移除成功</returns>
        bool Remove(T item);

        /// <summary>
        /// 判断选项是否存在
        /// </summary>
        /// <param name="item">选项</param>
        /// <returns>选项是否存在</returns>
        bool Exists(T item);
    }
}
