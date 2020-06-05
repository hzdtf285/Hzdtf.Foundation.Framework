using Autofac;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using Hzdtf.Autofac.Extend.Standard;
using System;
using Microsoft.Extensions.DependencyInjection;
using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.Authorization.Web.Core;
using Microsoft.AspNetCore.Http;
using Hzdtf.BasicFunction.Service.Impl.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Impl.Standard;
using Hzdtf.WorkFlow.Service.Impl.Standard.Engine;
using Hzdtf.WorkFlow.Service.Impl.Standard;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine;
using Hzdtf.Utility.Standard.Language;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Platform.Contract.Standard.Config.Language;
using Hzdtf.BasicFunction.WorkFlow.Standard;

namespace Hzdtf.WebDemo.Core.AppStart
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
        /// <param name="services">服务</param>
        /// <returns>服务提供者</returns>
        public static IServiceProvider RegisterComponents(IServiceCollection services)
        {
            AssemblyConfigLocalMember assemblyConfigLocalMember = new AssemblyConfigLocalMember();
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigJson();
            AssemblyConfigInfo assemblyConfig = assemblyConfigLocalMember.Reader();

            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();

            IServiceProvider serviceProvider;
            builder.UnifiedRegisterAssemblys(services, new BuilderParam()
            {
                AssemblyServices = assemblyConfig.Services,
                IsLoadAutoMapperConfig = assemblyConfig.IsLoadAutoMapperConfig,
                RegisteringServiceAction = () =>
                {
                    builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().AsSelf().PropertiesAutowired();

                    builder.RegisterType<IdentityCookieAuth>().As<IIdentityAuthVali>().AsSelf().PropertiesAutowired();
                    builder.RegisterType<IdentityCookieAuth>().As<IIdentityExit>().AsSelf().PropertiesAutowired();

                    builder.RegisterType<WorkflowConfigCache>().As<IWorkflowConfigReader>().AsSelf().PropertiesAutowired();
                    builder.RegisterType<WorkflowInitSequenceService>().As<IWorkflowFormService>().AsSelf().PropertiesAutowired();

                    builder.RegisterType<LanguageLibraryCache>().As<ILanguageLibrary>().AsSelf().PropertiesAutowired();
                    builder.RegisterType<LanguageLibraryConfigJson>().As<IReaderAll<LanguageInfo>>().AsSelf().PropertiesAutowired();
                }
            }, out serviceProvider); 

            AttachmentService attachmentService = AutofacTool.Resolve<AttachmentService>();
            AttachmentOwnerLocalMember attachmentOwnerLocalMember = AutofacTool.Resolve<AttachmentOwnerLocalMember>();
            attachmentOwnerLocalMember.ProtoAttachmentOwnerReader = AutofacTool.Resolve<AttachmentOwnerJson>();

            attachmentService.AttachmentOwnerReader = attachmentOwnerLocalMember;

            var languageCache = AutofacTool.Resolve<LanguageLibraryCache>();
           // var languageJson = AutofacTool.Resolve<LanguageLibraryJson>();
            //languageJson.JsonFile = @"F:\MyWorks\net\Hzdtf.FoundationFramework\WebDemoCore\Hzdtf.WebDemo.Core\Config\LanguageLibrary\test.min.json";
           /// languageCache.ProtoLanguageLibraryReader = languageJson;
            LanguageUtil.LanguageLibrary = languageCache;

            return serviceProvider;
        }
    }
}