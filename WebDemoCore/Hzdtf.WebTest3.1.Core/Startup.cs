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
using Hzdtf.Logger.Integration.MicrosoftLog.Standard;
using Hzdtf.Logger.Text.Impl.Standard;
using NPOI.SS.Formula.Functions;
using Autofac.Core;
using Hzdtf.Logger.Integration.MicrosoftLog.Core;
using Hzdtf.Logger.Integration.ENLog.Standard;

namespace Hzdtf.WebTest3._1.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            var log = factory.CreateLogger("test");
            log.LogInformation("bbaaa{0},{1}", "a1", "a2");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseApiExceptionHandle();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            DependencyInjection.RegisterComponents(builder);
        }
    }
}
