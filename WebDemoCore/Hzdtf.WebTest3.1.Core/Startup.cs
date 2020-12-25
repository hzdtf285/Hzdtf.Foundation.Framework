using System;
using Autofac;
using Hzdtf.WebTest3_1.Core.AppStart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hzdtf.Utility.AspNet.Core.ExceptionHandle;
using Hzdtf.Logger.Integration.MicrosoftLog.Core;
using Hzdtf.Logger.Integration.ENLog.Standard;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Hzdtf.Platform.Impl.Core;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text;
using Hzdtf.Authorization.Web.Core;
using Hzdtf.Utility.AspNet.Core;
using Hzdtf.Utility.AspNet.Core.ModelValidate;

namespace Hzdtf.WebTest3._1.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            PlatformCodeTool.Config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers()
                .AddControllersAsServices()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true; // ����api�ӿڵĴ���ģ��״̬
                });

            services.AddSession();
            
            //services.AddDistributedRedisCache();// ��ӷֲ�ʽ����ΪRedis���罫session�洢��redis����ִ�д˾�

            services.AddIdentityAuth(options =>
            {
                options.LocalAuth.LoginPath = "/login.html";
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<ModelValidateActionFilter>();
            });
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddLogging(builder =>
            {
                //builder.SetMinimumLevel(LogLevel.Warning);
                //builder.AddConsole();
                builder.AddHzdtf(options =>
                {
                    options.ProtoLog = new IntegrationNLog();
                    //options.LogRecordLevel.SetRecordLevel(LogLevel.Warning.ToString());
                });
            });


            services.AddApiExceptionHandle();

            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Title = "web demo�ӿ�",
                        Version = "v1"
                    });

                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.Utility.Standard.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicController.Core.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicFunction.Model.Standard.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicFunction.MvcController.Core.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.WorkFlow.MvcController.Core.xml"));
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.WorkFlow.Model.Standard.xml"));
                });
            }

            //services.AddHttpClientForBreakerWrapPolicy<Exception>(); // ʹ�ö�·��ʱ����Ҫ
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceProviderTool.Instance = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityAuth();

            app.UseApiExceptionHandle();
            //app.UseHttpClientForBreakerWrapPolicy(); // ʹ�ö�·��ʱ����Ҫ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            if (Configuration.GetValue<bool>("Swagger:Enabled"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebDemo API");
                });
            }

            OtherConfig.Init();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            DependencyInjection.RegisterComponents(builder);
        }
    }
}
