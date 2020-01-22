using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
