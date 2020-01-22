using Autofac;
using System;
using System.Web.Http;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using Hzdtf.Autofac.Web.Extend.Framework;
using Hzdtf.Authorization.Web.Framework;
using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.BasicFunction.Service.Impl.Standard;
using Hzdtf.BasicFunction.Service.Impl.Standard.Expand.Attachment;
using Hzdtf.Autofac.Extend.Standard;

namespace Hzdtf.WebDemo.Framework
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
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigXml();
            AssemblyConfigInfo assemblyConfig = assemblyConfigLocalMember.Reader();
           
            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();
            IContainer container = builder.UnifiedRegisterAssemblysForWebApi2(GlobalConfiguration.Configuration, new WebBuilderParam()
            {
                AssemblyControllers = assemblyConfig.Entrances,
                AssemblyServices = assemblyConfig.Services,
                RegisteringServiceAction = () =>
                {
                    builder.RegisterType<IdentityHttpFormAuth>().As<IIdentityAuthVali>().AsSelf().PropertiesAutowired();
                    builder.RegisterType<IdentityHttpFormAuth>().As<IIdentityExit>().AsSelf().PropertiesAutowired();
                    builder.RegisterType<IdentityHttpFormAuth>().AsSelf().PropertiesAutowired();
                }
            });

            AttachmentService attachmentService = AutofacTool.Resolve<AttachmentService>();
            AttachmentOwnerLocalMember attachmentOwnerLocalMember = AutofacTool.Resolve<AttachmentOwnerLocalMember>();
            attachmentOwnerLocalMember.ProtoAttachmentOwnerReader = AutofacTool.Resolve<AttachmentOwnerXml>();

            attachmentService.AttachmentOwnerReader = attachmentOwnerLocalMember;
        }
    }
}