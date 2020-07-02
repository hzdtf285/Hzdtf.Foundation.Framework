using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// JSON辅助类
    /// @ 黄振东
    /// </summary>
    public static class JsonUtil
    {
        /// <summary>
        /// 序列化忽略null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON字符串</returns>
        public static string SerializeIgnoreNull(this object obj)
        {
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jSetting);
        }

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        public static object Deserialize(string str, Type type)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(str, type);
        }

        /// <summary>
        /// 从文件里反序列为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <returns>对象</returns>
        public static T DeserializeFromFile<T>(string fileName) => ReaderFileContent(fileName, str => Deserialize<T>(str));

        /// <summary>
        /// 从文件里反序列为对象
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="type">类型</param>
        /// <returns>对象</returns>
        public static object DeserializeFromFile(string fileName, Type type) => ReaderFileContent(fileName, str => Deserialize(str, type));

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <param name="action">读取完内容后回调</param>
        /// <returns>对象</returns>
        private static T ReaderFileContent<T>(string fileName, Func<string, T> action)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return default(T);
            }

            return action(File.ReadAllText(fileName));
        }
    }
}
