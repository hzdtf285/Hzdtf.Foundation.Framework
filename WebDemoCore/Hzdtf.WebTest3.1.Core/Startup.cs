using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Hzdtf.Utility.Standard.AutoMapperExtensions;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Hzdtf.Platform.Impl.Core;
using Microsoft.OpenApi.Models;
using System.IO;
using Newtonsoft.Json;
using Hzdtf.Polly.AspNet.Extensions.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                });

            services.AddSession();

            // 添加Cookies验证：
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/login.html");
                });

            //添加jwt验证：
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options => {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,//是否验证Issuer
            //            ValidateAudience = true,//是否验证Audience
            //            ValidateLifetime = true,//是否验证失效时间
            //            ClockSkew = TimeSpan.FromSeconds(30),
            //            ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //            ValidAudience = Configuration["Jwt:Domain"],//Audience
            //            ValidIssuer = Configuration["Jwt:Domain"],//Issuer，这两项和前面签发jwt的设置一致
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]))//拿到SecurityKey
            //        };
            //    });


            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 0;
                options.ValidateComplexTypesIfChildValidationFails = false;
            });
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Warning);
                builder.AddConsole();
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
                        Title = "web demo接口",
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

            //services.AddHttpClientForBreakerWrapPolicy<Exception>(); // 使用断路器时才需要
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {         
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

            app.UseApiExceptionHandle();
            //app.UseHttpClientForBreakerWrapPolicy(); // 使用断路器时才需要

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

            UserConfig.Init();
            OtherConfig.Init();

            AutoMapperUtil.Builder();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            DependencyInjection.RegisterComponents(builder);
        }
    }
}
