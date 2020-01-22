using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Hzdtf.Utility.Standard;

namespace Hzdtf.WebDemo.Framework
{
    /// <summary>
    /// 其他配置
    /// @ 黄振东
    /// </summary>
    public static class OtherConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if (ConfigurationManager.AppSettings["Page:MaxPageSize"] != null)
            {
                UtilTool.MaxPageSize = Convert.ToInt32(ConfigurationManager.AppSettings["Page:MaxPageSize"]);
            }
        }
    }
}