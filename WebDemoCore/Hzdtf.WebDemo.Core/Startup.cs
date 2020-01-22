using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Hzdtf.Platform.Contract.Standard.Config.ProgramStart;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.WebDemo.Core.AppStart;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Hzdtf.WebDemo.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            PlatformCodeTool.Config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 所有控制器将由Autofac来替换创建
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/login.html");
                });
            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 0;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "基础框架系统-API"
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.Utility.Standard.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicFunction.Model.Standard.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicController.Core.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Hzdtf.BasicFunction.MvcController.Core.xml"));
            });

            return DependencyInjection.RegisterComponents(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            if (PlatformCodeTool.Config.GetValue<bool>("Swagger:Enabled"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebDemo API");
                });
            }

            UserConfig.Init();
            OtherConfig.Init();

            PrgramConfigManager.ProgramConfigReader = new ProgramStartConfigJson();
            PrgramConfigManager.Start();
        }
    }
}
