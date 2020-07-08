using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.Utility.Standard;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Language;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hzdtf.WebTest3_1.Core.AppStart
{
    /// <summary>
    /// 其他配置
    /// </summary>
    public static class OtherConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if (PlatformTool.AppConfig["Page:MaxPageSize"] != null)
            {
                UtilTool.MaxPageSize = Convert.ToInt32(PlatformTool.AppConfig["Page:MaxPageSize"]);
            }

            IHttpContextAccessor httpContext = AutofacTool.Resolve<IHttpContextAccessor>();

            LanguageUtil.GetCurrentCultureName = () =>
            {
                return httpContext != null ? httpContext.HttpContext.Session.GetString("cultureName") : null;
            };

            UtilTool.GetCurrEnvironmentTypeFunc = () =>
            {               
                if (httpContext != null)
                {
                    int? type = httpContext.HttpContext.Session.GetInt32("CurrEnvironmentType");
                    if (type == null)
                    {
                        return EnvironmentType.PRODUCTION;
                    }

                    return (EnvironmentType)type;
                }

                return EnvironmentType.TEST;
            };            
        }
    }
}
