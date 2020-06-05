using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig
{
    /// <summary>
    /// 程序集配置信息
    /// @ 黄振东
    /// </summary>
    public class AssemblyConfigInfo
    {
        /// <summary>
        /// 入口集合
        /// </summary>
        public BasicAssemblyInfo[] Entrances
        {
            get;
            set;
        }

        /// <summary>
        /// 服务集合
        /// </summary>
        public AssemblyExpandInfo[] Services
        {
            get;
            set;
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        private string[] assemblyNames;

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string[] AssemblyNames
        {
            get
            {
                if (assemblyNames == null)
                {
                    var list = new List<string>();
                    if (!Services.IsNullOrLength0())
                    {
                        foreach (var item in Services)
                        {
                            list.AddRange(item.Names);
                        }
                    }
                    if (!Entrances.IsNullOrLength0())
                    {
                        foreach (var item in Entrances)
                        {
                            list.AddRange(item.Names);
                        }
                    }

                    assemblyNames = list.ToArray();
                }

                return assemblyNames;
            }
        }

        /// <summary>
        /// 是否加载自动映射配置
        /// </summary>
        public bool IsLoadAutoMapperConfig
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 基本程序集信息
    /// </summary>
    public class BasicAssemblyInfo
    {
        /// <summary>
        /// 名称集合
        /// </summary>
        public string[] Names
        {
            get;
            set;
        }

        /// <summary>
        /// 拦截器类型集合
        /// </summary>
        private Type[] interceptedTypes;

        /// <summary>
        /// 拦截器类型集合
        /// </summary>
        public Type[] InterceptedTypes
        {
            get
            {
                if (interceptedTypes == null && !Intercepteds.IsNullOrCount0())
                {
                    interceptedTypes = new Type[Intercepteds.Length];
                    for (int i = 0; i < Intercepteds.Length; i++)
                    {
                        interceptedTypes[i] = AssemblyUtil.LoadType(Intercepteds[i]);
                    }
                }

                return interceptedTypes;
            }
        }

        /// <summary>
        /// 拦截器集合
        /// </summary>
        public string[] Intercepteds
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 程序集扩展参数
    /// </summary>
    public class AssemblyExpandInfo : BasicAssemblyInfo
    {
        /// <summary>
        /// 生命周期，默认为DEPENDENCY
        /// </summary>
        public LifecycleType Lifecycle
        {
            get;
            set;
        } = LifecycleType.DEPENDENCY;

        /// <summary>
        /// 匹配标签名称数组
        /// </summary>
        public string[] MatchTagNames
        {
            get;
            set;
        }
    }
}
