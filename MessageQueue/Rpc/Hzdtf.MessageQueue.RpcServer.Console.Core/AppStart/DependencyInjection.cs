using System;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using Hzdtf.Autofac.Extend.Standard;
using Autofac;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Utility.Standard.InterfaceImpl;
using Hzdtf.Utility.Standard.Data.Dic;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Conversion;

namespace Hzdtf.MessageQueue.RpcServer.Console.Core.AppStart
{
    /// <summary>
    /// 依赖注入
    /// @ 黄振东
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 注册组件
        /// </summary>
        public static void RegisterComponents()
        {
            AssemblyConfigLocalMember assemblyConfigLocalMember = new AssemblyConfigLocalMember();
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigJson();
            AssemblyConfigInfo assemblyConfig = assemblyConfigLocalMember.Reader();

            var builder = new ContainerBuilder();
            builder.UnifiedRegisterAssemblys(new BuilderParam()
            {
                AssemblyServices = assemblyConfig.Services,
                RegisteringServiceAction = () =>
                {
                    builder.RegisterType<AutofacInstance>().As<IInstance>().AsSelf().PropertiesAutowired().SingleInstance();
                    builder.RegisterType<MessagePackBytesSerialization>().As<IBytesSerialization>().AsSelf().PropertiesAutowired().SingleInstance();
                    builder.RegisterType<MessagePackConvertTypeValue>().As<IConvertTypeValue>().AsSelf().PropertiesAutowired().SingleInstance();
                }
            });
            PlatformTool.AppConfig = AutofacTool.Resolve<IAppConfiguration>();

            var implCache = AutofacTool.Resolve<InterfaceMapImplCache>();
            implCache.Set(new DictionaryJson($"{AppContext.BaseDirectory}/Config/interfaceAssemblyMapImplAssemblyConfig.json"));
        }
    }
}