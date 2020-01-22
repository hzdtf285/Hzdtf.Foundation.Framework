using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Cache;
using Hzdtf.Utility.Standard.Conversion;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// 方法调用缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class MethodCallCache : SingleTypeLocalMemoryBase<string, InstanceMapMethodsInfo>, IMethodCall
    {
        /// <summary>
        /// 缓存键
        /// key:类全路径名(不包含方法名)
        /// </summary>
        private static readonly IDictionary<string, InstanceMapMethodsInfo> dicCaches = new Dictionary<string, InstanceMapMethodsInfo>();

        /// <summary>
        /// 同步缓存键
        /// </summary>
        private static readonly object syncDicCaches = new object();

        /// <summary>
        /// 参数值转换
        /// </summary>
        public IConvertTypeValue ParamValueConvert
        {
            get;
            set;
        }

        /// <summary>
        /// 实例
        /// </summary>
        public IInstance Instance
        {
            get;
            set;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        public virtual object Invoke(string fullPath, params object[] parames)
        {
            MethodInfo method;

            return Invoke(fullPath, out method, parames);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <param name="method">方法</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        public virtual object Invoke(string fullPath, out MethodInfo method, params object[] parames)
        {
            string classFullPath;
            string methodName = ReflectUtil.GetMethodName(fullPath, out classFullPath);

            if (dicCaches.ContainsKey(classFullPath))
            {
                var insMapMethod = dicCaches[classFullPath];
                method = insMapMethod.GetMethodByName(methodName);
                if (method == null)
                {
                    method = insMapMethod.Instance.GetType().GetMethod(methodName);
                    insMapMethod.Methods.Add(method);
                }

                AutoEqualMethodParams(method, parames);

                return method.Invoke(insMapMethod.Instance, parames);
            }
            else
            {
                var insMapMethod = new InstanceMapMethodsInfo()
                {
                    Instance = Instance.CreateInstance(classFullPath)
                };
                method = insMapMethod.Instance.GetType().GetMethod(methodName);
                insMapMethod.Methods.Add(method);

                Set(classFullPath, insMapMethod);

                AutoEqualMethodParams(method, parames);

                return method.Invoke(insMapMethod.Instance, parames);
            }
        }

        /// <summary>
        /// 自动匹配参数类型
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="parames">参数数组</param>
        private void AutoEqualMethodParams(MethodInfo method, params object[] parames)
        {
            // 传过来的参数，如果与方法对应的参数类型不一致，则进行转换
            if (parames.IsNullOrLength0())
            {
                return;
            }

            var methodParams = method.GetParameters();
            for (var i = 0; i < parames.Length; i++)
            {
                if (parames[i] == null)
                {
                    continue;
                }

                var inType = parames[i].GetType();
                var methodType = methodParams[i].ParameterType;
                if (methodType == inType)
                {
                    continue;
                }

                parames[i] = ParamValueConvert.To(parames[i], methodType);
            }
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <returns>缓存对象</returns>
        protected override IDictionary<string, InstanceMapMethodsInfo> GetCache()
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
