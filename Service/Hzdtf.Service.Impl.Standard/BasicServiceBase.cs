using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Service.Impl.Standard
{
    /// <summary>
    /// 基本服务基类
    /// @ 黄振东
    /// </summary>
    public abstract class BasicServiceBase
    {
        #region 属性与字段

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = LogTool.DefaultLog;

        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        #endregion
    }
}
