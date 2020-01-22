using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 字符串辅助类
    /// @ 黄振东
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 从URL参数转换为字典
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>字典</returns>
        public static IDictionary<string, string> ToDictionaryFromUrlParams(this string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url.IndexOf("?") == -1)
            {
                return null;
            }
           
            int index = url.IndexOf("?") + 1;
            url =  HttpUtility.UrlDecode(url.Substring(index, url.Length - index));
            string[] array = url.Split('&');
            IDictionary<string, string> keyValues = new Dictionary<string, string>(array.Length);
            foreach (string arr in array)
            {
                if (string.IsNullOrWhiteSpace(arr))
                {
                    continue;
                }

                string[] temp = arr.Split('=');
                if (temp.Length != 2 || string.IsNullOrWhiteSpace(temp[0]) || keyValues.ContainsKey(temp[0]))
                {
                    continue;
                }

                
                keyValues.Add(temp[0], temp[1]);
            }

            return keyValues;
        }

        /// <summary>
        /// 判断字符串是否在数组里
        /// </summary>
        /// <param name="strs">字符串数组</param>
        /// <param name="str">字符串</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns>字符串是否在数组里</returns>
        public static bool Contains(this string[] strs, string str, bool ignoreCase = false)
        {
            if (strs.IsNullOrLength0() || str == null)
            {
                return false;
            }

            foreach (string s in strs)
            {
                if (string.Compare(s, str, ignoreCase) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 将字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>对象</returns>
        public static T DeserializeFromJson<T>(this string str) => JsonUtil.Deserialize<T>(str);

        /// <summary>
        /// 新建简短的GUID
        /// </summary>
        /// <returns>简短的GUID</returns>
        public static string NewShortGuid() => Guid.NewGuid().ToString().Replace("-", null);

        /// <summary>
        /// 首位转换为大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>首位转换为大写</returns>
        public static string FristUpper(this string str) => FristFunc(str, x => x.ToUpper());

        /// <summary>
        /// 首位转换为小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>首位转换为小写</returns>
        public static string FristLower(this string str) => FristFunc(str, x => x.ToLower());

        /// <summary>
        /// 首位回调
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fristFun">首位回调</param>
        /// <returns>字符串</returns>
        private static string FristFunc(string str, Func<string, string> fristFun)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            StringBuilder result = new StringBuilder();
            string[] temp = str.Split('_');
            for (int i = 0; i < temp.Length; i++)
            {
                string s = i == 0 ? fristFun(temp[i][0].ToString()) : temp[i][0].ToString().ToUpper();
                
                result.AppendFormat("{0}{1}", s, temp[i].Substring(1, temp[i].Length - 1));
            }

            return result.ToString();
        }

        /// <summary>
        /// 解析GUID
        /// </summary>
        /// <param name="guidStr">GUID字符串</param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="name">名称</param>
        /// <returns>GUID</returns>
        public static Guid ParseGuid(this string guidStr, out string errMsg, string name = null)
        {
            if (string.IsNullOrWhiteSpace(guidStr))
            {
                errMsg = $"{name}Guid不能为空";

                return Guid.Empty;
            }
            if (guidStr.Length == 32 || guidStr.Length == 36)
            {
                Guid guid;
                if (Guid.TryParse(guidStr, out guid))
                {
                    errMsg = null;
                    return guid;
                }
                else
                {
                    errMsg = $"{name}Guid格式错误";
                }
            }
            else
            {
                errMsg = $"{name}Guid长度必须是32或36个字符";
            }

            return Guid.Empty;
        }

        /// <summary>  
        /// 新建一个Guid唯一的长整型
        /// </summary>  
        /// <returns>Guid唯一的长整型</returns>  
        public static long NewGuidLong()
        {
            return ToLong(Guid.NewGuid());
        }

        /// <summary>  
        /// 将GUID转换为唯一的长整型
        /// </summary>  
        /// <param name="guid">GUID</param>
        /// <returns>唯一的长整型</returns>  
        public static long ToLong(this Guid guid)
        {
            return BitConverter.ToInt64(guid.ToByteArray(), 0);
        }

        /// <summary>
        /// JSON反序列化对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonStr">JSON字符串</param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="name">名称</param>
        /// <returns>反序列化后的对象</returns>
        public static T JsonDeserialize<T>(this string jsonStr, out string errMsg, string name = null)
        {
            if (string.IsNullOrWhiteSpace(jsonStr))
            {
                errMsg = $"{name}Json字符串不能为空";

                return default(T);
            }

            errMsg = null;
            try
            {
                return JsonUtil.Deserialize<T>(jsonStr);
            }
            catch
            {
                errMsg = $"{name}Json字符串格式错误";

                return default(T);
            }
        }
    }
}
