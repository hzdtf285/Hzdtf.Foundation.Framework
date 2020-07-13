using Hzdtf.Utility.Standard.Model.Return;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Polly.Extensions.Standard
{
    /// <summary>
    /// 带有返回数据的策略接口
    /// @ 黄振东
    /// </summary>
    public interface IPolicyReturnData
    {
        /// <summary>
        /// 设置，如果存在则忽略，不存在则添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="options">选项配置</param>
        /// <returns>异步策略</returns>
        IAsyncPolicy<ReturnInfo<object>> SetIgnoreExistskey(string key, Action<BreakerWrapPolicyOptions<ReturnInfo<object>>> options = null);

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="execFunc">执行回调</param>
        /// <param name="options">选项配置</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<object>> ExecuteAsync(string key, Func<Task<ReturnInfo<object>>> execFunc, Action<BreakerWrapPolicyOptions<ReturnInfo<object>>> options = null);
    }
}
