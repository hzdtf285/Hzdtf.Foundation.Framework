using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 资源池基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    public abstract class ResourcePoolBase<KeyT, ValueT> : IResourcePool<KeyT, ValueT>
    {
        #region IResourcePool<KeyT, ValueT> 接口

        /// <summary>
        /// 资源数量
        /// </summary>
        public int Count { get => GetCache().Count; }

        /// <summary>
        /// 获取新键
        /// 1、如果键不存在，则新创建一个键与默认值，返回新的键
        /// 2、如果存在，则返回已经存在的键
        /// </summary>
        /// <param name="isExists">是否已存在</param>
        /// <param name="key">键</param>
        /// <returns>键</returns>
        public KeyT GetNewKey(out bool isExists, KeyT key = default(KeyT))
        {
            if (key == null || !GetCache().ContainsKey(key))
            {
                isExists = false;

                key = CreateKey();

                lock (GetSyncCache())
                {
                    try
                    {
                        GetCache().Add(key, default(ValueT));
                    }
                    catch(ArgumentException) { }// 忽略添加相同的键异常，为了预防密集的线程过来
                }

                return key;
            }
            else
            {
                isExists = true;

                return key;
            }
        }

        /// <summary>
        /// 根据键获取新值
        /// 1、如果键不存在，则返回默认值
        /// 2、如果键存在，值不存在，则自动创建值返回
        /// 3、如果键存在，值存在，则返回已存在的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public ValueT GetNewValue(KeyT key)
        {
            if (key == null || !GetCache().ContainsKey(key))
            {
                return default(ValueT);
            }

            var value = GetCache()[key];
            if (value == null)
            {
                value = CreateValue();

                lock (GetSyncCache())
                {
                    GetCache()[key] = value;
                }
            }

            return value;
        }

        /// <summary>
        /// 根据键释放资源
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="currContextKeyNotExists">当前上下文的键是否不存在，如果为是，不释放，反之释放，默认为否。此处应与GetNewKey中的out isExists对应</param>
        public void Release(KeyT key, bool currContextKeyNotExists = false)
        {
            if (key == null || currContextKeyNotExists)
            {
                return;
            }

            if (GetCache().ContainsKey(key))
            {
                var value = GetCache()[key];
                if (value != null)
                {
                    ReleaseValue(value);
                }

                lock (GetSyncCache())
                {
                    GetCache().Remove(key);
                }
            }
        }

        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void ReleaseAll()
        {
            if (Count == 0)
            {
                return;
            }

            foreach (var kv in GetCache())
            {
                if (kv.Value == null)
                {
                    continue;
                }

                ReleaseValue(kv.Value);
            }

            GetCache().Clear();
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected abstract IDictionary<KeyT, ValueT> GetCache();

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected abstract object GetSyncCache();

        /// <summary>
        /// 创建一个新的键
        /// </summary>
        /// <returns>一个新的键</returns>
        protected abstract KeyT CreateKey();

        /// <summary>
        /// 创建一个新的值
        /// </summary>
        /// <returns>一个新的值</returns>
        protected abstract ValueT CreateValue();

        #endregion

        #region 虚方法

        /// <summary>
        /// 释放值
        /// </summary>
        /// <param name="value">值</param>
        protected virtual void ReleaseValue(ValueT value) { }

        #endregion
    }
}
