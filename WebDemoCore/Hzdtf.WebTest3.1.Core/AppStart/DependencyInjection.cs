using System;
using Autofac;
using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using Hzdtf.Utility.Standard.Data;

namespace Hzdtf.WebTest3_1.Core.AppStart
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 注册组件
        /// </summary>
        /// <param name="builder">内容生成</param>
        public static void RegisterComponents(ContainerBuilder builder)
        {
            AssemblyConfigLocalMember assemblyConfigLocalMember = new AssemblyConfigLocalMember();
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigJson();
            AssemblyConfigInfo assemblyConfig = assemblyConfigLocalMember.Reader();

            builder.UnifiedRegisterAssemblysV3_0(new BuilderParam()
            {
                AssemblyServices = assemblyConfig.Services,
                RegisteringServiceAction = () =>
                {
                    builder.RegisterType<AutofacInstance>().As<IInstance>().AsSelf().PropertiesAutowired().SingleInstance();
                }
            });
            builder.RegisterBuildCallback(container =>
            {
                PlatformTool.AppConfig = container.Resolve<IAppConfiguration>();
            });
        }
    }
}