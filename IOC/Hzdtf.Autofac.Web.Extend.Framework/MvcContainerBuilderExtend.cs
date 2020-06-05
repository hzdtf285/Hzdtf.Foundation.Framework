using Autofac;
using System;
using Hzdtf.Autofac.Extend.Standard;
using Autofac.Integration.Mvc;
using Hzdtf.Utility.Standard.Utils;
using System.Reflection;
using System.Web.Mvc;
using Autofac.Extras.DynamicProxy;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using System.Collections.Generic;
using Hzdtf.Utility.Standard.AutoMapperExtensions;

namespace Hzdtf.Autofac.Web.Extend.Framework
{
    /// <summary>
    /// MVC容器生成器扩展类
    /// 引用MVC5
    /// @ 黄振东
    /// </summary>
    public static class MvcContainerBuilderExtend
    {
        /// <summary>
        /// 统一注册MVC及服务程序集
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="param">参数</param>
        /// <returns>容器</returns>
        public static IContainer UnifiedRegisterAssemblysForMvc5(this ContainerBuilder containerBuilder, WebBuilderParam param)
        {
            var assemblyList = new List<Assembly>();
            foreach (BasicAssemblyInfo assembly in param.AssemblyControllers)
            {
                Assembly[] assemblies = AssemblyUtil.Load(assembly.Names);
                if (assemblies.IsNullOrLength0())
                {
                    return null;
                }
                assemblyList.AddRange(assemblies);

                if (!assembly.InterceptedTypes.IsNullOrLength0())
                {
                    foreach (Type type in assembly.InterceptedTypes)
                    {
                        containerBuilder.RegisterType(type);
                    }
                }

                if (assembly.Intercepteds.IsNullOrLength0())
                {
                    containerBuilder.RegisterControllers(assemblies)
                       .PropertiesAutowired()
                       .AsImplementedInterfaces()
                       .Where(AutofacUtil.CanInject)
                       .AsSelf();
                }
                else
                {
                    containerBuilder.RegisterControllers(assemblies)
                        .PropertiesAutowired()
                        .AsImplementedInterfaces()
                        .AsSelf()
                        .InterceptedBy(assembly.Intercepteds)
                        .Where(AutofacUtil.CanInject)
                        .EnableClassInterceptors();
                }
            }

            if (param.RegisteringControllerAction != null)
            {
                param.RegisteringControllerAction();
            }

            IContainer container = containerBuilder.UnifiedRegisterAssemblys(param);
            //将MVC的控制器对象实例 交由autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            WebAutofacTool.MvcDependencyResolver = DependencyResolver.Current;
            Hzdtf.Autofac.Extend.Standard.AutofacTool.ResolveFunc = WebAutofacTool.GetMvcService;

            if (param.IsLoadAutoMapperConfig)
            {
                AutoMapperUtil.AutoRegisterConfig(assemblyList.ToArray());
            }

            return container;
        }
    }
}
