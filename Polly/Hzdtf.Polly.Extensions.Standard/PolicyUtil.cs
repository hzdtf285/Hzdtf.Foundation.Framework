using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using Polly;

namespace Hzdtf.Polly.Extensions.Standard
{
    /// <summary>
    /// 策略辅助类
    /// @ 黄振东
    /// </summary>
    public static class PolicyUtil
    {
        /// <summary>
        /// 本类名称
        /// </summary>
        private static readonly string thisName = typeof(PolicyUtil).Name;

        /// <summary>
        /// 默认降级编码
        /// </summary>
        public const int DEFAULT_FALLBACK_CODE = 500;

        /// <summary>
        /// 默认降级文本
        /// </summary>
        public const string DEFAULT_FALLBACK_MSG = "系统繁忙，请稍候再试";

        /// <summary>
        /// 默认降级返回字符串
        /// </summary>
        public readonly static string DEFAULT_FALLBACK_RETURN_STRING = CreateDefaultFallbackReturnInfo<object>().ToString();


        /// <summary>
        /// 异步生成断路器包装策略
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="options">断路器包装策略选项配置</param>
        /// <param name="log">日志</param>
        /// <returns>策略任务</returns>
        public static IAsyncPolicy<TResult> BuilderBreakerWrapPollicyAsync<TResult, TException>(Action<BreakerWrapPolicyOptions<TResult>> options = null, ILogable log = null)
            where TException : Exception
        {
            var config = new BreakerWrapPolicyOptions<TResult>();
            if (options != null)
            {
                options(config);
            }

            var policys = new List<IAsyncPolicy<TResult>>();

            // 降级
            var fallback = Policy<TResult>.Handle<TException>().FallbackAsync(token =>
            {
                return Task<TResult>.Run(() =>
                {
                    if (config.GetResult == null)
                    {
                        return config.Result;
                    }
                    else
                    {
                        return config.GetResult();
                    }
                });
            });
            policys.Add(fallback);

            // 断路
            if (config.Breaker != null)
            {
                var breaker = Policy<TResult>.Handle<TException>()
                .CircuitBreakerAsync(
                   handledEventsAllowedBeforeBreaking: config.Breaker.HandledEventsAllowedBeforeBreaking,
                   durationOfBreak: TimeSpan.FromSeconds(config.Breaker.DurationOfBreak),
                   onBreak: (ex, state, ts, context) =>
                   {
                       log.AvailableLog().WranAsync($"(onBreak)进入断路器,状态:{state},timespan:{ts}", ex.Exception, thisName, tags: config.Id);
                   },
                   onReset: (context) =>
                   {
                       log.AvailableLog().InfoAsync(msg: "(onReset)关闭断路器", source: thisName, tags: config.Id);
                   },
                 onHalfOpen: () =>
                 {
                     log.AvailableLog().InfoAsync(msg: "(onHalfOpen)半打开断路器", source: thisName, tags: config.Id);
                 });

                policys.Add(breaker);
            }

            // 重试
            if (config.RetryNumber != 0)
            {
                var retry = Policy<TResult>.Handle<TException>()
                    .RetryAsync(config.RetryNumber, (ex, num) =>
                    {
                        log.AvailableLog().WranAsync(msg: $"(retrt)重试第{num}次", ex.Exception, source: thisName, tags: config.Id);
                    });
                policys.Add(retry);
            }

            // 超时
            if (config.Timeout != 0)
            {
                var timeout = Policy.TimeoutAsync<TResult>(config.Timeout);
                policys.Add(timeout);
            }

            return Policy.WrapAsync<TResult>(policys.ToArray());
        }

        /// <summary>
        /// 异步执行断路器包装策略
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="execFunc">执行回调</param>
        /// <param name="options">断路器包装策略选项配置</param>
        /// <param name="log">日志</param>
        /// <returns>结果任务</returns>
        public static Task<TResult> ExecuteBreakerWrapPollicyAsync<TResult, TException>(Func<Task<TResult>> execFunc, Action<BreakerWrapPolicyOptions<TResult>> options = null, ILogable log = null)
            where TException : Exception
        {
            if (execFunc == null)
            {
                throw new NullReferenceException("执行回调不能为null");
            }

            var polly = BuilderBreakerWrapPollicyAsync<TResult, TException>(options);

            return polly.ExecuteAsync(execFunc);
        }

        /// <summary>
        /// 异步生成断路器包装策略（带有返回信息）
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <typeparam name="ReturnDataT">返回数据类型</typeparam>
        /// <param name="options">断路器包装策略选项配置</param>
        /// <param name="log">日志</param>
        /// <returns>策略任务</returns>
        public static IAsyncPolicy<ReturnInfo<ReturnDataT>> BuilderBreakerWrapPollicyReturnInfoAsync<TException, ReturnDataT>(Action<BreakerWrapPolicyOptions<ReturnInfo<ReturnDataT>>> options = null, ILogable log = null)
            where TException : Exception
        {
            return BuilderBreakerWrapPollicyAsync<ReturnInfo<ReturnDataT>, TException>(op =>
            {
                op.Result = CreateDefaultFallbackReturnInfo<ReturnDataT>();

                if (options != null)
                {
                    options(op);
                }
            });
        }

        /// <summary>
        /// 异步执行断路器包装策略（带有返回信息）
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <typeparam name="ReturnDataT">返回数据类型</typeparam>
        /// <param name="execFunc">执行回调</param>
        /// <param name="options">断路器包装策略选项配置</param>
        /// <param name="log">日志</param>
        /// <returns>结果任务</returns>
        public static Task<ReturnInfo<ReturnDataT>> ExecuteBreakerWrapPollicyReturnInfoAsync<TException, ReturnDataT>(Func<Task<ReturnInfo<ReturnDataT>>> execFunc, Action<BreakerWrapPolicyOptions<ReturnInfo<ReturnDataT>>> options = null, ILogable log = null)
            where TException : Exception
        {
            if (execFunc == null)
            {
                throw new NullReferenceException("执行回调不能为null");
            }

            var polly = BuilderBreakerWrapPollicyReturnInfoAsync<TException, ReturnDataT>(options);

            return polly.ExecuteAsync(execFunc);
        }

        /// <summary>
        /// 创建默认的降级返回信息
        /// </summary>
        /// <typeparam name="DataT">数据类型</typeparam>
        /// <returns>降级返回信息</returns>
        public static ReturnInfo<DataT> CreateDefaultFallbackReturnInfo<DataT>()
        {
            return new ReturnInfo<DataT>()
            {
                Code = PolicyUtil.DEFAULT_FALLBACK_CODE,
                Msg = PolicyUtil.DEFAULT_FALLBACK_MSG
            };
        }
    }
}
