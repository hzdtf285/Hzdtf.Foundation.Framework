using Hzdtf.Authorization.Contract.Standard.IdentityAuth;
using Hzdtf.Authorization.Contract.Standard.IdentityAuth.Token;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Platform.Impl.Core;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Authorization.Web.Core
{
    /// <summary>
    /// 身份认证扩展类
    /// @ 黄振东
    /// </summary>
    public static class IdentityAuthExtensionscs
    {
        /// <summary>
        /// 添加身份认证
        /// </summary>
        /// <param name="services">服务收藏</param>
        /// <param name="options">选项配置</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddIdentityAuth(this IServiceCollection services, Action<IdentityAuthOptions> options = null)
        {
            return services.AddIdentityAuth<BasicUserInfo>(options);
        }

        /// <summary>
        /// 添加身份认证
        /// </summary>
        /// <typeparam name="UserT">用户类型</typeparam>
        /// <param name="services">服务收藏</param>
        /// <param name="options">选项配置</param>
        /// <returns>服务收藏</returns>
        public static IServiceCollection AddIdentityAuth<UserT>(this IServiceCollection services, Action<IdentityAuthOptions> options = null)
            where UserT : BasicUserInfo
        {
            var config = new IdentityAuthOptions();
            if (options != null)
            {
                options(config);
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IIdentityAuthReader<UserT>, IdentityAuthClaimReader<UserT>>();

            var localOption = config.LocalAuth;
            switch (config.AuthType)
            {
                case IdentityAuthType.COOKIES:
                    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                    {
                        if (!string.IsNullOrWhiteSpace(localOption.LoginPath))
                        {
                            if (localOption.IsRedirectToLogin)
                            {
                                o.Events.OnRedirectToLogin = (context) =>
                                {
                                    return Task.Run(() =>
                                    {
                                        context.Response.Redirect(localOption.LoginPath);
                                    });
                                };
                            }
                            else
                            {
                                o.LoginPath = new PathString(localOption.LoginPath);
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(localOption.LogoutPath))
                        {
                            if (localOption.IsRedirectToLogout)
                            {
                                o.Events.OnRedirectToLogout = (context) =>
                                {
                                    return Task.Run(() =>
                                    {
                                        context.Response.Redirect(localOption.LogoutPath);
                                    });
                                };
                            }
                            else
                            {
                                o.LogoutPath = new PathString(localOption.LogoutPath);
                            }
                        }
                    });

                    services.AddSingleton<IIdentityAuth<UserT>, IdentityCookieAuth<UserT>>();
                    services.AddSingleton<IIdentityExit, IdentityCookieAuth<UserT>>();

                    break;

                case IdentityAuthType.JWT:
                    if (config.Config == null)
                    {
                        throw new NullReferenceException("配置属性不能为null");
                    }

                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,//是否验证Issuer
                                ValidateAudience = true,//是否验证Audience
                                ValidateLifetime = true,//是否验证失效时间
                                ClockSkew = TimeSpan.FromSeconds(30),
                                ValidateIssuerSigningKey = true,//是否验证SecurityKey
                                ValidAudience = config.Config["Jwt:Domain"],//Audience
                                ValidIssuer = config.Config["Jwt:Domain"],//Issuer，这两项和前面签发jwt的设置一致
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Config["Jwt:SecurityKey"]))//拿到SecurityKey
                            };
                        });

                    services.AddSingleton<IIdentityTokenAuth, IdentityJwtAuth<UserT>>();

                    break;
            }

            return services;
        }

        /// <summary>
        /// 使用身份认证
        /// </summary>
        /// <param name="app">应用生成器</param>
        /// <returns>应用生成器</returns>
        public static IApplicationBuilder UseIdentityAuth(this IApplicationBuilder app) => app.UseIdentityAuth<BasicUserInfo>();

        /// <summary>
        /// 使用身份认证
        /// </summary>
        /// <typeparam name="UserT">用户类型</typeparam>
        /// <param name="app">应用生成器</param>
        /// <returns>应用生成器</returns>
        public static IApplicationBuilder UseIdentityAuth<UserT>(this IApplicationBuilder app) where UserT : BasicUserInfo
        {
            UserTool.GetCurrUserFunc = () =>
            {
                var reader = app.ApplicationServices.GetService<IIdentityAuthReader<UserT>>();
                if (reader == null)
                {
                    return null;
                }
                var returnInfo = reader.Reader();
                if (returnInfo.Success() && returnInfo.Data != null)
                {
                    return returnInfo.Data;
                }

                if (PlatformTool.AppConfig["User:AllowTest"] != null && Convert.ToBoolean(PlatformTool.AppConfig["User:AllowTest"]))
                {
                    return UserTool.TestUser;
                }

                return null;
            };

            return app;
        }
    }

    /// <summary>
    /// 身份认证选项配置
    /// @ 黄振东
    /// </summary>
    public class IdentityAuthOptions
    {
        /// <summary>
        /// 身份认证类型，默认为Cookies
        /// </summary>
        public IdentityAuthType AuthType
        {
            get;
            set;
        } = IdentityAuthType.COOKIES;

        /// <summary>
        /// 本地认证配置
        /// </summary>
        public LocalAuthOptions LocalAuth
        {
            get;
            set;
        } = new LocalAuthOptions();

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Config
        {
            get;
            set;
        } = PlatformCodeTool.Config;
    }

    /// <summary>
    /// 本地认证选项配置
    /// @ 黄振东
    /// </summary>
    public class LocalAuthOptions
    {
        /// <summary>
        /// 登录路径
        /// </summary>
        public string LoginPath
        {
            get;
            set;
        }

        /// <summary>
        /// 登出路径
        /// </summary>
        public string LogoutPath
        {
            get;
            set;
        }

        /// <summary>
        /// 是否重定向登录
        /// </summary>
        public bool IsRedirectToLogin
        {
            get;
            set;
        }

        /// <summary>
        /// 是否重定向登出
        /// </summary>
        public bool IsRedirectToLogout
        {
            get;
            set;
        }
    }
}
