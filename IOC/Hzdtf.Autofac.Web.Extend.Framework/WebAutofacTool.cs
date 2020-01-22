using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Hzdtf.Autofac.Web.Extend.Framework
{
    /// <summary>
    /// Web Autofac工具
    /// @ 黄振东
    /// </summary>
    public static class WebAutofacTool
    {
        /// <summary>
        /// Http注入解析器
        /// </summary>
        public static IDependencyResolver HttpDependencyResolver { get; set; }

        /// <summary>
        /// Mvc注入解析器
        /// </summary>
        public static System.Web.Mvc.IDependencyResolver MvcDependencyResolver { get; set; }

        /// <summary>
        /// 获取Http服务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>服务</returns>
        public static object GetHttpService(Type type) => HttpDependencyResolver.GetService(type);

        /// <summary>
        /// 获取Mvc服务
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>服务</returns>
        public static object GetMvcService(Type type) => MvcDependencyResolver.GetService(type);
    }
}
