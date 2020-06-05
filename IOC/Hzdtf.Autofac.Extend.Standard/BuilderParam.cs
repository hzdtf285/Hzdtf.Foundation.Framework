using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Autofac.Extend.Standard
{
    /// <summary>
    /// 生成参数
    /// @ 黄振东
    /// </summary>
    public class BuilderParam
    {
        /// <summary>
        /// 程序集服务集合
        /// </summary>
        public AssemblyExpandInfo[] AssemblyServices
        {
            get;
            set;
        }

        /// <summary>
        /// 注册中服务动作
        /// </summary>
        public Action RegisteringServiceAction
        {
            get;
            set;
        }

        /// <summary>
        /// 是否加载自动映射配置
        /// </summary>
        public bool IsLoadAutoMapperConfig
        {
            get;
            set;
        }
    }
}
