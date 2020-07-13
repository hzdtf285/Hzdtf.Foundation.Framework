using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Cache;
using Hzdtf.Utility.Standard.Model.Return;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Polly.Extensions.Standard
{
    /// <summary>
    /// 策略返回数据缓缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class PolicyReturnDataCache : SingleTypeLocalMemoryBase<string, IAsyncPolicy<ReturnInfo<object>>>, IPolicyReturnData
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        private static readonly IDictionary<string, IAsyncPolicy<ReturnInfo<object>>> dicCaches = new Dictionary<string, IAsyncPolicy<ReturnInfo<object>>>();

        /// <summary>
        /// 同步缓存键
        /// </summary>
        private static readonly object syncDicCaches = new object();

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 构造方法
        /// </summary>
        public PolicyReturnDataCache() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="log">日志</param>
        public PolicyReturnDataCache(ILogable log)
        {
            if (log == null)
            {
                return;
            }

            this.Log = log;
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns>缓存对象</returns>
        protected override IDictionary<string, IAsyncPolicy<ReturnInfo<object>>> GetCache()
        {
            return dicCaches;
        }

        /// <summary>
        /// 获取同步缓存对象
        /// </summary>
        /// <returns>同步缓存对象</returns>
        protected override object GetSyncCache()
        {
            return syncDicCaches;
        }

        /// <summary>
        /// 设置，如果存在则忽略，不存在则添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="options">选项配置</param>
        /// <returns>异步策略</returns>
        public IAsyncPolicy<ReturnInfo<object>> SetIgnoreExistskey(string key, Action<BreakerWrapPolicyOptions<ReturnInfo<object>>> options = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("键不能为空");
            }

            if (dicCaches.ContainsKey(key))
            {
                return dicCaches[key];
            }

            var asyncPolicy = PolicyUtil.BuilderBreakerWrapPollicyReturnInfoAsync<Exception, object>(options);
            Set(key, asyncPolicy);

            return asyncPolicy;
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="execFunc">执行回调</param>
        /// <param name="options">选项配置</param>
        /// <returns>返回信息任务</returns>
        public Task<ReturnInfo<object>> ExecuteAsync(string key, Func<Task<ReturnInfo<object>>> execFunc, Action<BreakerWrapPolicyOptions<ReturnInfo<object>>> options = null)
        {
            return SetIgnoreExistskey(key, options).ExecuteAsync(execFunc);
        }
    }
}
