using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Hzdtf.Utility.Standard.Cache.TimerRefresh
{
    /// <summary>
    /// 定时刷新缓存基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ValueT">值类型</typeparam>
    public abstract class TimerRefreshCacheBase<ValueT> : IReader<ValueT>, IDisposable
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private Timer timer;

        /// <summary>
        /// 缓存
        /// </summary>
        private ValueT caches;

        /// <summary>
        /// 是否已刷新
        /// </summary>
        private bool isRefreshed;

        /// <summary>
        /// 状态
        /// </summary>
        private readonly object state;

        /// <summary>
        /// 间隔时间，单位：毫秒
        /// </summary>
        private readonly int intervalMillseconds;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="intervalMillseconds">间隔时间，单位：毫秒</param>
        /// <param name="isInitTimer">是否初始化定时器，默认为是</param>
        /// <param name="state">状态</param>
        public TimerRefreshCacheBase(int intervalMillseconds, object state = null, bool isInitTimer = true)
        {
            this.intervalMillseconds = intervalMillseconds;
            this.state = state;

            if (isInitTimer)
            {
                InitTimer();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        [ProcTrackLog(ExecProc = false)]
        public virtual void Dispose()
        {
            timer.Dispose();
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ValueT Reader()
        {
            if (isRefreshed || caches != null)
            {
                return caches;
            }

            caches = Refresh(state);
            isRefreshed = true;

            return caches;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns>值</returns>
        protected abstract ValueT Refresh(object state);

        /// <summary>
        /// 析构方法
        /// </summary>
        ~TimerRefreshCacheBase()
        {
            Dispose();
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        protected void InitTimer()
        {
            timer = new Timer(st =>
            {
                caches = Refresh(st);
                isRefreshed = true;
            }, state, intervalMillseconds, intervalMillseconds);
        }
    }
}
