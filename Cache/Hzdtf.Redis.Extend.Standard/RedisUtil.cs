using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Redis.Extend.Standard
{
    /// <summary>
    /// Redis辅助类
    /// @ 黄振东
    /// </summary>
    public static class RedisUtil
    {
        /// <summary>
        /// 将对象属性转换为哈希键值对数组
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>哈希键值对数组</returns>
        public static HashEntry[] ToHashEntrys(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            if (properties.IsNullOrLength0())
            {
                return null;
            }

            IList<HashEntry> list = new List<HashEntry>(properties.Length);
            foreach (PropertyInfo p in properties)
            {
                if (p.CanRead)
                {
                    object v = p.GetValue(obj);
                    if (v == null)
                    {
                        continue;
                    }

                    list.Add(new HashEntry(p.Name, v.ToString()));
                }
            }
            if (list.Count == 0)
            {
                return null;
            }

            return list.ToArray();
        }

        /// <summary>
        /// 从哈希键值对数组转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="hashEntries">哈希键值对数组</param>
        /// <returns>对象</returns>
        public static T FromHashEntrys<T>(HashEntry[] hashEntries)
            where T : class
        {
            if (hashEntries.IsNullOrLength0())
            {
                return null;
            }

            T obj = typeof(T).CreateInstance<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (properties.IsNullOrLength0())
            {
                return obj;
            }

            foreach (HashEntry he in hashEntries)
            {
                foreach (PropertyInfo p in properties)
                {
                    if (p.CanWrite && p.Name.Equals(he.Name) && he.Value.HasValue)
                    {
                        p.SetPropertyValue(obj, he.Value.ToString());
                    }
                }
            }

            return obj;
        }
    }
}
