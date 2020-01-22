using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Release;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Cache
{
    /// <summary>
    /// 单类型缓存接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    public interface ISingleTypeCache<KeyT, ValueT> : IGetable<KeyT, ValueT>, IClearable, IReader<IDictionary<KeyT, ValueT>>
    {
        /// <summary>
        /// 缓存键数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 添加
        /// 如果存在则不添加，返回false
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        bool Add(KeyT key, ValueT value);

        /// <summary>
        /// 更新
        /// 如果不存在则不更新，返回false
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        bool Update(KeyT key, ValueT value);

        /// <summary>
        /// 设置
        /// 如果存在则更新，不存在则添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否设置成功</returns>
        bool Set(KeyT key, ValueT value);

        /// <summary>
        /// 根据键移除
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        bool Remove(KeyT key);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>是否移除成功</returns>
        bool Remove(KeyT[] keys);

        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>键是否存在</returns>
        bool Exists(KeyT key);
    }
}
