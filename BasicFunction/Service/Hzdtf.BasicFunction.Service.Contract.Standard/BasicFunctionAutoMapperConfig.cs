using AutoMapper;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Utility.Standard.AutoMapperExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard
{
    /// <summary>
    /// 基本功能自动映射配置
    /// @ 黄振东
    /// </summary>
    public class BasicFunctionAutoMapperConfig : IAutoMapperConfig
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="config">配置</param>
        public void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<UserInfo, UserMenuInfo>();
            config.CreateMap<UserMenuInfo, UserInfo>();
            config.CreateMap<AttachmentInfo, SimpleAttachmentInfo>();
            config.CreateMap<SimpleAttachmentInfo, AttachmentInfo>();
        }
    }
}
