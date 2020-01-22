using System;
using System.Collections.Generic;

namespace Hzdtf.Utility.Standard.Cache
{
    /// <summary>
    /// 列表本地内存基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public abstract class ListLocalMemoryBase<T> : IListCache<T>
    {
        #region IListCache<T> 接口

        /// <summary>
        /// 缓存选项数量
        /// </summary>
        public int Count { get => GetCache().Count; }

        /// <summary>
        /// 判断选项是否存在
        /// </summary>
        /// <param name="item">选项</param>
        /// <returns>选项是否存在</returns>
        public virtual bool Exists(T item) => GetCache().Contains(item);

        #endregion

        #region ICacheWrite<itemT, ValueT> 接口

        /// <summary>
        /// 添加
        /// 如果存在则不添加，返回false
        /// </summary>
        /// <param name="item">选项</param>
        /// <returns>是否添加成功</returns>
        public virtual bool Add(T item)
        {
            if (Exists(item))
            {
                return false;
            }
            lock(GetSyncCache())
            {
                GetCache().Add(item);

                return true;
            }
        }

        /// <summary>
        /// 移除
        /// 如果存在则删除并返回true，否则返回false
        /// </summary>
        /// <param name="item">选项</param>
        /// <returns>是否移除成功</returns>
        public virtual bool Remove(T item)
        {
            if (Exists(item))
            {
                lock (GetSyncCache())
                {
                    return GetCache().Remove(item);
                }
            }

            return false;
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

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public virtual IList<T> ReaderAll() => GetCache();

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected abstract IList<T> GetCache();

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
        /// <param name="list">列表</param>
        protected void SetAll(IList<T> list)
        {
            lock (GetSyncCache())
            {
                var dic = GetCache();
                dic = list;
            }
        }

        #endregion
    }
}
