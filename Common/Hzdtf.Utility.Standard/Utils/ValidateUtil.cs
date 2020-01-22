using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 验证辅助类
    /// @ 黄振东
    /// </summary>
    public static class ValidateUtil
    {
        /// <summary>
        /// 验证字符串是否为null或为空，如果是则抛出异常
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="name">名称</param>
        public static void ValidateNullOrEmpty(this string str, string name) => ValidateNullOrEmpty2(str, name + "不能为null或空");

        /// <summary>
        /// 验证字符串是否为null或为空，如果是则抛出异常
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="message">消息</param>
        public static void ValidateNullOrEmpty2(this string str, string message)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException(message);
            }
        }

        /// <summary>
        /// 验证对象是否为null，如果是则抛出异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">名称</param>
        public static void ValidateNull(this object obj, string name) => ValidateNull2(obj, name + "不能为null");

        /// <summary>
        /// 验证对象是否为null，如果是则抛出异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="message">消息</param>
        public static void ValidateNull2(this object obj, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}
