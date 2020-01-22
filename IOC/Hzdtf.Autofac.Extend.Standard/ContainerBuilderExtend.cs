using Autofac;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Reflection;
using Autofac.Extras.DynamicProxy;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Hzdtf.Utility.Standard.Enums;

namespace Hzdtf.Autofac.Extend.Standard
{
    /// <summary>
    /// 容器生成器扩展类
    /// @ 黄振东
    /// </summary>
    public static class ContainerBuilderExtend
    {
        /// <summary>
        /// 统一注册服务程序集
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="param">参数</param>
        /// <param name="isExecBuilderContainer">是否执行生成容器，如果为false，则返回值为null</param>
        /// <returns>容器</returns>
        public static IContainer UnifiedRegisterAssemblys(this ContainerBuilder containerBuilder, BuilderParam param, bool isExecBuilderContainer = true)
        {
            foreach (AssemblyExpandInfo assembly in param.AssemblyServices)
            {
                Assembly[] assemblies = AssemblyUtil.Load(assembly.Names);
                if (assemblies.IsNullOrLength0())
                {
                    return null;
                }

                var registerBuilder = containerBuilder.RegisterAssemblyTypes(assemblies)
                       .PropertiesAutowired()
                       .AsImplementedInterfaces()
                       .Where(AutofacUtil.CanInject)
                       .AsSelf();

                if (!assembly.InterceptedTypes.IsNullOrLength0())
                {
                    foreach (Type type in assembly.InterceptedTypes)
                    {
                        containerBuilder.RegisterType(type);
                    }

                    registerBuilder.InterceptedBy(assembly.InterceptedTypes).EnableClassInterceptors();
                }

                switch (assembly.Lifecycle)
                {
                    case LifecycleType.DEPENDENCY:
                        registerBuilder.InstancePerDependency();

                        break;

                    case LifecycleType.LIFETIME_SCOPE:
                        registerBuilder.InstancePerLifetimeScope();

                        break;

                    case LifecycleType.MATCH_LIFETIME_SCOPE:
                        registerBuilder.InstancePerMatchingLifetimeScope(assembly.MatchTagNames);

                        break;

                    case LifecycleType.REQUEST:
                        registerBuilder.InstancePerRequest();

                        break;

                    case LifecycleType.SIGNLETON:
                        registerBuilder.SingleInstance();

                        break;
                }
            }

            if (param.RegisteringServiceAction != null)
            {
                param.RegisteringServiceAction();
            }

            if (isExecBuilderContainer)
            {
                AutofacTool.Container = containerBuilder.Build();
            }
            else
            {
                containerBuilder.RegisterBuildCallback(container => AutofacTool.Container = container);
            }

            return AutofacTool.Container;
        }

        /// <summary>
        /// 统一注册服务程序集
        /// 专门为.NET Core提供
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="services">服务</param>
        /// <param name="param">参数</param>
        /// <param name="serviceProvider">服务提供者</param>
        /// <returns>容器</returns>
        public static IContainer UnifiedRegisterAssemblys(this ContainerBuilder containerBuilder, IServiceCollection services, BuilderParam param, out IServiceProvider serviceProvider)
        {
            IContainer container = UnifiedRegisterAssemblys(containerBuilder, new BuilderParam()
            {
                AssemblyServices = param.AssemblyServices,
                RegisteringServiceAction = () =>
                {
                    if (param.RegisteringServiceAction != null)
                    {
                        param.RegisteringServiceAction();
                    }

                    containerBuilder.Populate(services);
                }
            });

            serviceProvider = new AutofacServiceProvider(container);
            
            return container;
        }

        /// <summary>
        /// 统一注册服务程序集
        /// 专门为.NET Core 3.0以上提供
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="param">参数</param>
        public static void UnifiedRegisterAssemblysV3_0(this ContainerBuilder containerBuilder, BuilderParam param)
        {
            UnifiedRegisterAssemblys(containerBuilder, new BuilderParam()
            {
                AssemblyServices = param.AssemblyServices,
                RegisteringServiceAction = () =>
                {
                    if (param.RegisteringServiceAction != null)
                    {
                        param.RegisteringServiceAction();
                    }
                }
            }, false);
        }
    }
}
