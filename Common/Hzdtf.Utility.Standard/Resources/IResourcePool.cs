using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 资源池接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    public interface IResourcePool<KeyT, ValueT>
    {
        /// <summary>
        /// 资源数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取新键
        /// 1、如果键不存在，则新创建一个键与默认值，返回新的键
        /// 2、如果存在，则返回已经存在的键
        /// </summary>
        /// <param name="isExists">是否已存在</param>
        /// <param name="key">键</param>
        /// <returns>键</returns>
        KeyT GetNewKey(out bool isExists, KeyT key = default(KeyT));

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        ValueT GetValue(KeyT key);

        /// <summary>
        /// 根据键获取新值
        /// 1、如果键不存在，则返回默认值
        /// 2、如果键存在，值不存在，则自动创建值返回
        /// 3、如果键存在，值存在，则返回已存在的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        ValueT GetNewValue(KeyT key);

        /// <summary>
        /// 根据键释放资源
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isThisCreateKey">是否本身创建的键，如果为是，则释放，否则不释放，默认为是。此处应与GetNewKey中的out isExists对应</param>
        void Release(KeyT key, bool isThisCreateKey = true);

        /// <summary>
        /// 释放所有资源
        /// </summary>
        void ReleaseAll();
    }
}
