using Hzdtf.Utility.Standard.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard
{
    /// <summary>
    /// 辅助工具类
    /// @ 黄振东
    /// </summary>
    public static class UtilTool
    {
        /// <summary>
        /// 最大每页记录数
        /// -1表示不限制，默认为-1
        /// </summary>
        public static int MaxPageSize
        {
            get;
            set;
        } = -1;

        /// <summary>
        /// 同步当前应用程序名称
        /// </summary>
        private static readonly object syncCurrApplicationName = new object();

        /// <summary>
        /// 当前应用程序名称
        /// </summary>
        private static string currApplicationName = null;

        /// <summary>
        /// 当前应用程序名称
        /// </summary>
        public static string CurrApplicationName
        {
            get => currApplicationName;
            set
            {
                lock (syncCurrApplicationName)
                {
                    currApplicationName = value;
                }
            }
        }

        /// <summary>
        /// 当前环境类型
        /// </summary>
        public static EnvironmentType CurrEnvironmentType
        {
            get => GetCurrEnvironmentTypeFunc == null ? EnvironmentType.PRODUCTION : GetCurrEnvironmentTypeFunc();
        }

        /// <summary>
        /// 获取当前环境类型回调
        /// </summary>
        public static Func<EnvironmentType> GetCurrEnvironmentTypeFunc;
    }
}
