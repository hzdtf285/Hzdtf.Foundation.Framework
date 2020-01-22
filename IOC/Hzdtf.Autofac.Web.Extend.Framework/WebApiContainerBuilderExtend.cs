using Autofac;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Reflection;
using Hzdtf.Autofac.Extend.Standard;
using Autofac.Integration.WebApi;
using System.Web.Http;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Autofac.Extras.DynamicProxy;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;

namespace Hzdtf.Autofac.Web.Extend.Framework
{
    /// <summary>
    /// WebApi容器生成器扩展类
    /// 引入的是WebApi2
    /// @ 黄振东
    /// </summary>
    public static class WebApiContainerBuilderExtend
    {
        /// <summary>
        /// 为WebApi2统一注册MVC及服务程序集
        /// WebApi2用于MVC5以上
        /// </summary>
        /// <param name="containerBuilder">容器生成器</param>
        /// <param name="param">参数</param>
        /// <returns>容器</returns>
        public static IContainer UnifiedRegisterAssemblysForWebApi2(this ContainerBuilder containerBuilder, HttpConfiguration configuration, WebBuilderParam param)
        {
            foreach (BasicAssemblyInfo assembly in param.AssemblyControllers)
            {
                Assembly[] assemblies = AssemblyUtil.Load(assembly.Names);
                if (assemblies.IsNullOrLength0())
                {
                    return null;
                }

                if (!assembly.InterceptedTypes.IsNullOrLength0())
                {
                    foreach (Type type in assembly.InterceptedTypes)
                    {
                        containerBuilder.RegisterType(type);
                    }
                }

                if (assembly.Intercepteds.IsNullOrLength0())
                {
                    containerBuilder.RegisterApiControllers(assemblies)
                       .PropertiesAutowired()
                       .AsImplementedInterfaces()
                       .Where(AutofacUtil.CanInject)
                       .AsSelf();//注册api容器的实现

                    containerBuilder.RegisterControllers(assemblies)
                        .PropertiesAutowired()
                        .AsImplementedInterfaces()
                        .Where(AutofacUtil.CanInject)
                        .AsSelf();
                }
                else
                {
                    containerBuilder.RegisterApiControllers(assemblies)
                       .PropertiesAutowired()
                       .AsImplementedInterfaces()
                       .AsSelf()
                       .InterceptedBy(assembly.InterceptedTypes)
                       .Where(AutofacUtil.CanInject)
                       .EnableClassInterceptors();//注册api容器的实现

                    containerBuilder.RegisterControllers(assemblies)
                        .PropertiesAutowired()
                        .AsImplementedInterfaces()
                        .AsSelf()
                        .InterceptedBy(assembly.InterceptedTypes)
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
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            WebAutofacTool.HttpDependencyResolver = configuration.DependencyResolver;
            AutofacTool.ResolveFunc = WebAutofacTool.GetHttpService;

            return container;
        }
    }
}
