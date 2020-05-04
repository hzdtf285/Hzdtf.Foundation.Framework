using Hzdtf.Utility.Standard.LoadBalance;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.RemoteService
{
    /// <summary>
    /// 服务基本选项配置
    /// @ 黄振东
    /// </summary>
    public class ServicesBasicOptions
    {
        /// <summary>
        /// 负载均衡模式
        /// </summary>
        public LoadBalanceMode? LoadBalanceMode
        {
            get;
            set;
        }

        /// <summary>
        /// 方案
        /// </summary>
        public string Sheme
        {
            get;
            set;
        }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 服务选项配置
    /// @ 黄振东
    /// </summary>
    public class ServicesOptions : ServicesBasicOptions
    {
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 负载均衡
        /// </summary>
        public ILoadBalance LoadBalance
        {
            get;
            set;
        }

        /// <summary>
        /// 服务生成器
        /// </summary>
        public IServicesBuilder ServicesBuilder
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 全局服务配置选项
    /// @ 黄振东
    /// </summary>
    public class GlobalServicesOptions : ServicesBasicOptions
    {
    }

    /// <summary>
    /// 统一服务选项配置
    /// @ 黄振东
    /// </summary>
    public class UnityServicesOptions
    {
        /// <summary>
        /// 服务配置数组
        /// </summary>
        public ServicesOptions[] Services
        {
            get;
            set;
        }

        /// <summary>
        /// 全局配置
        /// </summary>
        public GlobalServicesOptions GlobalConfiguration
        {
            get;
            set;
        } = new GlobalServicesOptions();

        /// <summary>
        /// 重置，如果Services没有配置而GlobalConfiguration有配置，则用GlobalConfiguration代替。并设置默认值
        /// </summary>
        public void Reset()
        {
            if (Services.IsNullOrLength0())
            {
                return;
            }

            foreach (var ser in Services)
            {
                if (ser.LoadBalanceMode == null && GlobalConfiguration.LoadBalanceMode != null)
                {
                    ser.LoadBalanceMode = GlobalConfiguration.LoadBalanceMode;
                }
                if (ser.Sheme == null && GlobalConfiguration.Sheme != null)
                {
                    ser.Sheme = GlobalConfiguration.Sheme;
                }
                if (ser.Tag == null && GlobalConfiguration.Tag != null)
                {
                    ser.Tag = GlobalConfiguration.Tag;
                }
                
                if (ser.LoadBalanceMode == null)
                {
                    ser.LoadBalanceMode = LoadBalanceMode.RANDOM;
                }
                if (string.IsNullOrWhiteSpace(ser.Sheme))
                {
                    ser.Sheme = Uri.UriSchemeHttp;
                }

                switch ((Standard.LoadBalance.LoadBalanceMode)ser.LoadBalanceMode)
                {
                    case Standard.LoadBalance.LoadBalanceMode.RANDOM:
                        ser.LoadBalance = new RandomLoadBalance();

                        break;

                    case Standard.LoadBalance.LoadBalanceMode.ROUND_ROBIN:
                        ser.LoadBalance = new RoundRobinLoadBalance();

                        break;

                    case Standard.LoadBalance.LoadBalanceMode.HASH_IP_PORT:
                        ser.LoadBalance = new HashIpPortLoadBalance();

                        break;
                }
            }
        }
    }
}
