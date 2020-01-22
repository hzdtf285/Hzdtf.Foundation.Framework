using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 收藏辅助类
    /// @ 黄振东
    /// </summary>
    public static class CollectionUtil
    {
        /// <summary>
        /// 判断收藏是否为null或长度是否为0
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collection">收藏</param>
        /// <returns>列表是否为null或长度是否为0</returns>
        public static bool IsNullOrCount0<T>(this ICollection<T> collection) => collection == null || collection.Count == 0 ? true : false;
       
        /// <summary>
        /// 判断数组是否为null或长度是否为0
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>列表是否为null或长度是否为0</returns>
        public static bool IsNullOrLength0<T>(this T[] array) => array == null || array.Length == 0 ? true : false;

        /// <summary>
        /// 判断字典是否为null或长度是否为0
        /// </summary>
        /// <typeparam name="KeyT">键类型</typeparam>
        /// <typeparam name="ValueT">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <returns>字典是否为null或长度是否为0</returns>
        public static bool IsNullOrCount0<KeyT, ValueT>(this IDictionary<KeyT, ValueT> dic) => dic == null || dic.Count == 0 ? true : false;

        /// <summary>
        /// 将列表转换为数组
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">列表</param>
        /// <returns>数组</returns>
        public static T[] ToArray<T>(this IList<T> list)
        {
            if (list.IsNullOrCount0())
            {
                return null;
            }

            T[] result = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                result[i] = list[i];
            }
            
            return result;
        }

        /// <summary>
        /// 将字典值转换为列表
        /// </summary>
        /// <typeparam name="KeyT">键类型</typeparam>
        /// <typeparam name="ValueT">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <returns>列表</returns>
        public static IList<ValueT> ValuesToList<KeyT, ValueT>(this IDictionary<KeyT, ValueT> dic)
        {
            if (dic == null)
            {
                return null;
            }

            IList<ValueT> result = new List<ValueT>(dic.Count);
            foreach (KeyValuePair<KeyT, ValueT> item in dic)
            {
                result.Add(item.Value);
            }

            return result;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="itemAction">子项动作</param>
        /// <param name="comparer">比较</param>
        public static void Sort<T>(this IList<T> list, Comparer<T> comparer, Action<T> itemAction = null)
        {
            if (list.IsNullOrCount0())
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (comparer.Compare(list[i], list[j]) > 0)
                    {
                        T temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }

                if (itemAction != null)
                {
                    itemAction(list[i]);
                }
            }
        }

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <typeparam name="KeyT">键类型</typeparam>
        /// <typeparam name="ValueT">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static ValueT GetValue<KeyT, ValueT>(this IDictionary<KeyT, ValueT> dic, KeyT key)
        {
            if (dic.IsNullOrCount0())
            {
                return default(ValueT);
            }

            return dic.ContainsKey(key) ? dic[key] : default(ValueT);
        }

        /// <summary>
        /// 合并数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="array1">数组1</param>
        /// <param name="array2">数组2</param>
        /// <returns>合并后的数组</returns>
        public static T[] Merge<T>(this T[] array1, T[] array2)
        {
            if (IsNullOrLength0(array1) && IsNullOrLength0(array2))
            {
                return null;
            }

            if (!IsNullOrLength0(array1) && IsNullOrLength0(array2))
            {
                return array1;
            }
            if (IsNullOrLength0(array1) && !IsNullOrLength0(array2))
            {
                return array2;
            }

            T[] newArray = new T[array1.Length + array2.Length];
            for (int i = 0; i < array1.Length; i++)
            {
                newArray[i] = array1[i];
            }
            for (int i = 0,startIndex = array1.Length; i < array2.Length; i++, startIndex++)
            {
                newArray[startIndex] = array2[i];
            }

            return newArray;
        }

        /// <summary>
        /// 字典转换为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="ValueT">字典值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <returns>对象</returns>
        public static T ToObject<T, ValueT>(this IDictionary<string, ValueT> dic)
        {
            if (dic.IsNullOrCount0())
            {
                return default(T);
            }

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            if (properties.IsNullOrLength0())
            {
                return default(T);
            }

            T instance = (T)type.Assembly.CreateInstance(type.FullName);
            foreach (KeyValuePair<string, ValueT> keyValue in dic)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (string.Compare(keyValue.Key, property.Name, true) != 0 || keyValue.Value == null)
                    {
                        continue;
                    }

                    ReflectUtil.SetPropertyValue(property, instance, keyValue.Value);

                    break;
                }
            }

            return instance;
        }

        /// <summary>
        /// 将数组按分隔符组合字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="split">分隔符</param>
        /// <returns>组合后的字符串</returns>
        public static string ToMergeString<T>(this T[] array, string split = null)
        {
            if (array.IsNullOrLength0())
            {
                return null;
            }

            StringBuilder result = new StringBuilder();
            foreach (T ar in array)
            {
                result.AppendFormat("{0}{1}", ar, split);
            }
            if (!string.IsNullOrEmpty(split))
            {
                result.Remove(result.Length - split.Length, split.Length);
            }

            return result.ToString();
        }

        /// <summary>
        /// 移除键
        /// </summary>
        /// <typeparam name="KeyT">键类型</typeparam>
        /// <typeparam name="ValueT">值类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="key">键</param>
        public static void RemoveKey<KeyT, ValueT>(this IDictionary<KeyT, ValueT> dic, KeyT key)
        {
            if (dic.IsNullOrCount0())
            {
                return;
            }

            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
            }
        }

        /// <summary>
        /// 将收藏转换为列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">收藏</param>
        /// <returns>列表</returns>
        public static IList<T> ToList<T>(this ICollection<T> collection)
        {
            if (collection == null)
            {
                return null;
            }

            var list = new List<T>(collection.Count);
            foreach (var c in collection)
            {
                list.Add(c);
            }

            return list;
        }

        /// <summary>
        /// 将收藏的各元素进行反转
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="collection">收藏</param>
        /// <param name="createCollectionFunc">创建收藏回调，如果为null，默认为创建List</param>
        /// <returns>反转后的收藏</returns>
        public static ICollection<T> Reverse<T>(this ICollection<T> collection, Func<int, ICollection<T>> createCollectionFunc = null)
        {
            if (collection.IsNullOrCount0())
            {
                return collection;
            }
            if (collection is List<T>)
            {
                var list = (collection as List<T>);
                list.Reverse();

                return list;
            }

            var newColl = createCollectionFunc == null ? new List<T>(collection.Count) : createCollectionFunc(collection.Count);
            var enumer = collection.GetEnumerator();
            var newArray = new T[collection.Count];
            var i = 0;
            while (enumer.MoveNext())
            {
                newArray[i] = enumer.Current;
                i++;
            }

            for (i--; i > -1; i--)
            {
                newColl.Add(newArray[i]);
            }

            return newColl;
        }

        /// <summary>
        /// 将列表的各元素进行反转
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="createListFunc">创建列表回调，如果为null，默认为创建List</param>
        /// <returns>反转后的列表</returns>
        public static IList<T> Reverse<T>(this IList<T> list, Func<int, IList<T>> createListFunc = null)
        {
            if (list.IsNullOrCount0())
            {
                return list;
            }

            if (list is List<T>)
            {
                var tempList = (list as List<T>);
                tempList.Reverse();

                return tempList;
            }

            IList<T> newList = createListFunc == null ? new List<T>(list.Count) : createListFunc(list.Count);
            for (var i = list.Count - 1; i > -1; i--)
            {
                newList.Add(list[i]);
            }

            return newList;
        }

        /// <summary>
        /// 将数组的各元素进行反转
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>反转后的数组</returns>
        public static T[] Reverse<T>(this T[] array)
        {
            if (array.IsNullOrLength0())
            {
                return array;
            }

            var newArray = new T[array.Length];
            var newIndex = newArray.Length - 1;
            for (var i = 0; i < array.Length; i++, newIndex--)
            {
                newArray[newIndex] = array[i];
            }

            return newArray;
        }
    }
}
