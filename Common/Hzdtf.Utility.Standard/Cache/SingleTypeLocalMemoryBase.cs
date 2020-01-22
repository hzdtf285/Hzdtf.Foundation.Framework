using System;
using System.Collections.Generic;

namespace Hzdtf.Utility.Standard.Cache
{
    /// <summary>
    /// 单类型的本地内存基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="KeyT">键类型</typeparam>
    /// <typeparam name="ValueT">值类型</typeparam>
    public abstract class SingleTypeLocalMemoryBase<KeyT, ValueT> : ISingleTypeCache<KeyT, ValueT>
    {
        #region ICacheWrite<KeyT, ValueT> 接口

        /// <summary>
        /// 缓存键数量
        /// </summary>
        public int Count { get => GetCache().Count; }

        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>键是否存在</returns>
        public virtual bool Exists(KeyT key) => GetCache().ContainsKey(key);

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public virtual ValueT Get(KeyT key)
        {
            if (Exists(key))
            {
                return GetCache()[key];
            }
            else
            {
                return default(ValueT);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public virtual IDictionary<KeyT, ValueT> Reader() => GetCache();

        /// <summary>
        /// 添加
        /// 如果存在则不添加，返回false
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        public virtual bool Add(KeyT key, ValueT value)
        {
            if (Exists(key))
            {
                return false;
            }
            lock(GetSyncCache())
            {
                try
                {
                    GetCache().Add(key, value);
                }
                catch (ArgumentException) { }// 忽略添加相同的键异常，为了预防密集的线程过来

                return true;
            }
        }

        /// <summary>
        /// 更新
        /// 如果不存在则不更新，返回false
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否添加成功</returns>
        public virtual bool Update(KeyT key, ValueT value)
        {
            if (Exists(key))
            {
                lock (GetSyncCache())
                {
                    GetCache()[key] = value;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置
        /// 如果存在则更新，不存在则添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否设置成功</returns>
        public virtual bool Set(KeyT key, ValueT value)
        {
            if (Exists(key))
            {
                return Update(key, value);
            }
            else
            {
                return Add(key, value);
            }
        }

        /// <summary>
        /// 移除
        /// 如果存在则删除并返回true，否则返回false
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否移除成功</returns>
        public virtual bool Remove(KeyT key)
        {
            if (Exists(key))
            {
                lock (GetSyncCache())
                {
                    return GetCache().Remove(key);
                }
            }

            return false;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="keys">键数组</param>
        /// <returns>是否移除成功</returns>
        public virtual bool Remove(KeyT[] keys)
        {
            foreach (KeyT key in keys)
            {
                Remove(key);
            }

            return true;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            lock (GetSyncCache())
            {
                GetCache().Clear();
            }
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

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 设置全部
        /// </summary>
        /// <param name="keyValues">键值对</param>
        protected void SetAll(IDictionary<KeyT, ValueT> keyValues)
        {
            lock (GetSyncCache())
            {
                var dic = GetCache();
                dic = keyValues;
            }
        }

        #endregion
    }
}
