using AutoMapper;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.Platform.Contract.Standard.Config.ProgramStart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 程序启动映射配置
    /// @ 黄振东
    /// </summary>
    public class ProgramStartMapperConfig : IProgramStart
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args">参数</param>
        public void Start(params string[] args)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<UserMenuInfoProFile>();
            });
        }
    }

    /// <summary>
    /// 用户菜单树信息配置文件
    /// </summary>
    class UserMenuInfoProFile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public UserMenuInfoProFile()
        {
            CreateMap<UserInfo, UserMenuInfo>();
        }
    }
}
