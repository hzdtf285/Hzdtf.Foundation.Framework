using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Cache;
using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using System.Linq;

namespace Hzdtf.Utility.Standard.InterfaceImpl
{
    /// <summary>
    /// 接口映射实现类缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class InterfaceMapImplCache : SingleTypeLocalMemoryBase<string, string>, IInterfaceMapImpl, ISetObject<IReader<IDictionary<string, string>>>
    {
        /// <summary>
        /// 缓存键，接口名映射实现类名
        /// </summary>
        private static readonly IDictionary<string, string> dicCaches = new Dictionary<string, string>();

        /// <summary>
        /// 同步缓存键
        /// </summary>
        private static readonly object syncDicCaches = new object();

        /// <summary>
        /// 读取配置
        /// </summary>
        private IReader<IDictionary<string, string>> readerConfig;

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="key">键，接口全路径，包含接口的程序集名，比如：Hzdtf.Contract,Hzdtf.Contract.ITestable</param>
        /// <returns>值，接口对应的实现类名，比如：Hzdtf.Impl,Hzdtf.Impl.Test</returns>
        public string Reader(string key)
        {
            if (dicCaches.ContainsKey(key))
            {
                return dicCaches[key];
            }
            else
            {
                var dicMap = readerConfig.Reader();
                if (dicMap.IsNullOrCount0())
                {
                    throw new Exception("找不到任何程序集的配置");
                }

                // 获取接口程序集
                string interfaceAssemblyName;
                var interfaceClassFullName = ReflectUtil.GetClassAndAssemblyFullName(key, out interfaceAssemblyName);
                if (string.IsNullOrWhiteSpace(interfaceAssemblyName))
                {
                    throw new Exception($"[{key}]的接口程序集不能为空");
                }

                var interfaceAssembly = ReflectUtil.GetAssembly(interfaceAssemblyName);
                if (interfaceAssembly == null)
                {
                    throw new Exception($"找不到[{interfaceAssemblyName}]的接口程序集");
                }

                Type interfaceType = interfaceAssembly.GetType(interfaceClassFullName);
                if (interfaceType == null)
                {
                    throw new Exception($"找不到[{interfaceClassFullName}]的接口类型");
                }

                // 找到对应的实现程序集
                if (dicMap.ContainsKey(interfaceAssemblyName))
                {
                    var implAssemblyName = dicMap[interfaceAssemblyName];
                    var implAssembly = ReflectUtil.GetAssembly(implAssemblyName);
                    if (implAssembly == null)
                    {
                        throw new Exception($"[{interfaceAssemblyName}]的对应的实现程序集");
                    }

                    var implType = implAssembly.GetTypes().Where(t => t.IsClass && t.IsPublic && interfaceType.IsAssignableFrom(t)).FirstOrDefault();
                    if (implType == null)
                    {
                        throw new Exception($"[{interfaceClassFullName}]的对应的实现类");
                    }

                    var implFullName = $"{implAssemblyName},{implType.FullName}";

                    Set(key, implFullName);

                    return implFullName;
                }
                else
                {
                    throw new Exception($"找不到[{interfaceAssemblyName}]的接口程序集的配置");
                }
            }

            throw new Exception($"找不到[{key}]对应的实现类");
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
        protected override IDictionary<string, string> GetCache()
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
