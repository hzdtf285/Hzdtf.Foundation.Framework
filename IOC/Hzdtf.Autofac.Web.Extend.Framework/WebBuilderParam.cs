using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Autofac.Web.Extend.Framework
{
    /// <summary>
    /// Web生成参数
    /// @ 黄振东
    /// </summary>
    public class WebBuilderParam : BuilderParam
    {
        /// <summary>
        /// 程序集控制器集合
        /// </summary>
        public BasicAssemblyInfo[] AssemblyControllers
        {
            get;
            set;
        }

        /// <summary>
        /// 注册中服务动作
        /// </summary>
        public Action RegisteringControllerAction
        {
            get;
            set;
        }
    }
}
