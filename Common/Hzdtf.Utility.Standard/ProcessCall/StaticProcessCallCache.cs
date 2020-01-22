using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Cache;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// 静态过程调用缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class StaticProcessCallCache : SingleTypeLocalMemoryBase<string, MethodInfo>, IProcessCall, ISetObject<IReader<IDictionary<string ,string>>>
    {
        /// <summary>
        /// 读取配置
        /// </summary>
        private IReader<IDictionary<string, string>> readerConfig;

        /// <summary>
        /// 缓存键
        /// </summary>
        private static readonly IDictionary<string, MethodInfo> dicCaches = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// 同步缓存键
        /// </summary>
        private static readonly object syncDicCaches = new object();

        /// <summary>
        /// 类配置字典
        /// </summary>
        private static IDictionary<string, string> dicClasses;

        /// <summary>
        /// 同步类配置字典
        /// </summary>
        private static readonly object syncDicClasses = new object();

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        public virtual object Invoke(string key, params object[] parames)
        {
            if (dicCaches.ContainsKey(key))
            {
                return Get(key).Invoke(null, parames);
            }
            else
            {
                string fullName = null;
                if (dicClasses == null)
                {
                    var tempDicClasses = readerConfig.Reader();
                    if (tempDicClasses != null)
                    {
                        lock (syncDicCaches)
                        {
                            dicClasses = tempDicClasses;
                        }

                        fullName = dicClasses[key];
                    }
                }
                else
                {
                    fullName = dicClasses[key];
                }

                MethodInfo method;
                var re = ReflectUtil.InvokeStaticMethod(fullName, out method, parames);

                Set(key, method);

                return re;
            }
        }

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="obj">对象</param>
        public void Set(IReader<IDictionary<string, string>> obj) => this.readerConfig = obj;

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns>缓存对象</returns>
        protected override IDictionary<string, MethodInfo> GetCache()
        {
            return dicCaches;
        }

        /// <summary>
        /// 获取同步缓存对象
        /// </summary>
        /// <returns>同步缓存对象</returns>
        protected override object GetSyncCache()
        {
            return syncDicCaches;
        }
    }
}
