using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Platform.Config.Contract.Standard.Config.App;
using Hzdtf.Platform.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.IO;
using System.Text;

namespace Hzdtf.Logger.Text.Impl.Standard
{
    /// <summary>
    /// 文本文件日志
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class TxtFileLog : ContentLogBase
    {
        #region 属性与字段

        /// <summary>
        /// 日志根目录
        /// </summary>
        private string logRootDirectory;

        /// <summary>
        /// 日志根目录
        /// </summary>
        private string LogRootDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(logRootDirectory))
                {
                    if (string.IsNullOrWhiteSpace(AppConfig["Logging:LogRoot"]))
                    {
                        logRootDirectory = AppContext.BaseDirectory + "/logs/";
                    }
                    else
                    {
                        logRootDirectory = AppConfig["Logging:LogRoot"];
                        if (logRootDirectory.Contains("{LocalApplicationData}"))
                        {
                            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                            logRootDirectory = logRootDirectory.Replace("{LocalApplicationData}", appData);
                        }
                    }
                }

                return logRootDirectory;
            }
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        public IAppConfiguration AppConfig
        {
            get;
            set;
        } = PlatformTool.AppConfig;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public TxtFileLog()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logRootDirectory">日志根目录</param>
        public TxtFileLog(string logRootDirectory) => this.logRootDirectory = logRootDirectory;

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 将日志内容写入到存储设备里
        /// </summary>
        /// <param name="logContent">日志内容</param>
        protected override void WriteStorage(string logContent) => GetFileFullName().WriteFileContent(logContent, true);

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取文件全路径名
        /// </summary>
        /// <returns>文件全路径名</returns>
        private string GetFileFullName()
        {
            string directory = string.Format("{0}/{1}/{2}", LogRootDirectory, DateTime.Now.Year, DateTime.Now.Month);
            if (!Directory.Exists(directory))
            {
                DirectoryInfo dirInfo = Directory.CreateDirectory(directory);
                if (dirInfo == null)
                {
                    throw new Exception(string.Format("创建文件夹[{0}]失败，请检查是否有权限", directory));
                }
            }

            return string.Format("{0}/{1}.log", directory, DateTime.Now.ToString("yyyy-MM-dd"));
        }

        #endregion
    }
}
