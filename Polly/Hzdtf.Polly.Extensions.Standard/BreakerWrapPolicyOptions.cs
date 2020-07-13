using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Polly.Extensions.Standard
{
    /// <summary>
    /// 断路器包装策略选项配置
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    public class BreakerWrapPolicyOptions<TResult>
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id
        {
            get;
            set;
        } = StringUtil.NewShortGuid();

        /// <summary>
        /// 重试次数，默认为3次，如果不需要重设，请设为0
        /// </summary>
        public int RetryNumber
        {
            get;
            set;
        } = 3;

        /// <summary>
        /// 超时，默认为20秒，如果不需要超时，请设为0
        /// </summary>
        public int Timeout
        {
            get;
            set;
        } = 20;

        /// <summary>
        /// 断路器，默认是开启，如果不需要断路器，请设为null
        /// </summary>
        public BreakerOptions Breaker
        {
            get;
            set;
        } = new BreakerOptions();

        /// <summary>
        /// 结果
        /// </summary>
        public TResult Result
        {
            get;
            set;
        }

        /// <summary>
        /// 获取结果回调
        /// </summary>
        public Func<TResult> GetResult
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 断路器选项配置
    /// @ 黄振东
    /// </summary>
    public class BreakerOptions
    {
        /// <summary>
        /// 连续出现故障的次数，默认为3次
        /// </summary>
        public int HandledEventsAllowedBeforeBreaking
        {
            get;
            set;
        } = 3;

        /// <summary>
        /// 进入断路状态的秒数，默认为10秒
        /// </summary>
        public int DurationOfBreak
        {
            get;
            set;
        } = 10;
    }
}
