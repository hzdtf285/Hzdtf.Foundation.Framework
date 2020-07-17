using Hzdtf.BasicFunction.WorkFlow.Standard;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard;
using Hzdtf.Utility.Standard.AutoMapperExtensions;
using System;

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
            UserWorkflowUtil.InitValiUserHandleVali();

            if (PlatformTool.AppConfig["Page:MaxPageSize"] != null)
            {
                UtilTool.MaxPageSize = Convert.ToInt32(PlatformTool.AppConfig["Page:MaxPageSize"]);
            }

            AutoMapperUtil.Builder();
        }
    }
}
