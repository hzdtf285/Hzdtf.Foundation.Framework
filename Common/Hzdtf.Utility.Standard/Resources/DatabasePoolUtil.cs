using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 数据库池辅助类
    /// @ 黄振东
    /// </summary>
    public static class DatabasePoolUtil
    {
        /// <summary>
        /// 开始执行核心业务，此方法是为了封装统一的数据库池操作
        /// 1、如果传入的键存在，则在此方法里不会释放此键的资源，由外面释放
        /// 2、如果传入的键不存在，则创建新的键，并在此方法里释放资源
        /// </summary>
        /// <param name="databasePool">数据库池</param>
        /// <param name="func">处理业务回调</param>
        /// <param name="key">键</param>
        /// <param name="isHaveUpdate">是否有更新，如果为true，且key不存在，业务回调又返回true，则会调用databasePool.Submit()</param>
        /// <returns>影响行数</returns>
        public static int StartExecCore(this IDatabasePool databasePool, Func<string, object, bool> func, string key = null, bool isHaveUpdate = false)
        {
            bool keyExists;
            key = databasePool.GetNewKey(out keyExists, key);
            try
            {
                var value = databasePool.GetNewValue(key);
                if (func(key, value) && isHaveUpdate && !keyExists)
                {
                    return databasePool.Submit(value);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {                
                databasePool.Release(key, keyExists);
            }
        }
    }
}
