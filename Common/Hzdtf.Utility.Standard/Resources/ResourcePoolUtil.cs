using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 资源池辅助类
    /// @ 黄振东
    /// </summary>
    public static class ResourcePoolUtil
    {
        /// <summary>
        /// 开始执行核心业务，此方法是为了封装统一的资源池操作
        /// 1、如果传入的键存在，则在此方法里不会释放此键的资源，由外面释放
        /// 2、如果传入的键不存在，则创建新的键，并在此方法里释放资源
        /// </summary>
        /// <typeparam name="KeyT">键类型</typeparam>
        /// <typeparam name="ValueT">值类型</typeparam>
        /// <param name="resourcePool">资源池</param>
        /// <param name="action">处理业务回调</param>
        /// <param name="key">键</param>
        public static void StartExecCore<KeyT, ValueT>(this IResourcePool<KeyT, ValueT> resourcePool, Action<KeyT, ValueT> action, KeyT key = default(KeyT))
        {
            bool keyExists;
            key = resourcePool.GetNewKey(out keyExists, key);
            try
            {
                var value = resourcePool.GetNewValue(key);
                action(key, value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {                
                resourcePool.Release(key, keyExists);
            }
        }
    }
}
