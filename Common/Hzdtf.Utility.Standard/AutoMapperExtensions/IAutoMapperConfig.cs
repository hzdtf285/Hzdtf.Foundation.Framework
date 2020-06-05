using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.AutoMapperExtensions
{
    /// <summary>
    /// 自动映射配置接口
    /// @ 黄振东
    /// </summary>
    public interface IAutoMapperConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="config">配置</param>
        void Register(IMapperConfigurationExpression config);
    }
}
