using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Hzdtf.Utility.Standard.Language
{
    /// <summary>
    /// 语系辅助类
    /// @ 黄振东
    /// </summary>
    public static class LanguageUtil
    {
        /// <summary>
        /// 语系库
        /// </summary>
        public static ILanguageLibrary LanguageLibrary
        {
            get;
            set;
        }

        /// <summary>
        /// 获取当前语言名称
        /// </summary>
        public static Func<string> GetCurrentCultureName;

        /// <summary>
        /// 转换为语系值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>语系值</returns>
        public static string ToLanguage(this string key)
        {
            if (GetCurrentCultureName != null)
            {
                SetCurrentCulture(GetCurrentCultureName());                
            }

            return ToLanguage(key, Thread.CurrentThread.CurrentUICulture.Name);
        }

        /// <summary>
        /// 转换为语系值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cultureName">语言文化名称</param>
        /// <returns>语系值</returns>
        public static string ToLanguage(this string key, string cultureName)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(cultureName) || LanguageLibrary == null)
            {
                return key;
            }

            var languageInfo = LanguageLibrary.Get(key);
            if (languageInfo == null)
            {
                return key;
            }

            string val = null;
            switch (cultureName)
            {
                case LanguageInfo.zh_CN:
                    val = languageInfo.ZH_CN;

                    break;

                case LanguageInfo.zh_TW:
                    val = languageInfo.ZH_TW;

                    break;

                case LanguageInfo.en_US:
                    val = languageInfo.EN_US;

                    break;

                default:

                    return key;
            }

            return val != null ? val : key;
        }

        /// <summary>
        /// 设置当前语言文化
        /// </summary>
        /// <param name="cultureName">语言文化名称</param>
        public static void SetCurrentCulture(string cultureName)
        {
            if (string.IsNullOrWhiteSpace(cultureName) || Thread.CurrentThread.CurrentUICulture.Name.Equals(cultureName))
            {
                return;
            }

            var c = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;
        }
    }
}
