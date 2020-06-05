using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using Hzdtf.Utility.Standard.Utils;
using NPOI.SS.Formula.Functions;

namespace Hzdtf.Utility.Standard.AutoMapperExtensions
{
    /// <summary>
    /// 自动映射辅助类
    /// @ 黄振东
    /// </summary>
    public static class AutoMapperUtil
    {
        /// <summary>
        /// 自动映射配置
        /// </summary>
        private static MapperConfiguration mapperConfiguration;

        /// <summary>
        /// 同步映射
        /// </summary>
        private readonly static object syncMapper = new object();

        /// <summary>
        /// 映射
        /// </summary>
        private static Mapper mapper;

        /// <summary>
        /// 映射
        /// </summary>
        public static Mapper Mapper
        {
            get
            {
                if (mapper == null)
                {
                    lock (syncMapper)
                    {
                        if (mapper == null)
                        {
                            mapper = new Mapper(mapperConfiguration);
                        }
                    }
                }

                return mapper;
            }
        }

        /// <summary>
        /// 配置列表
        /// </summary>
        private readonly static IList<Action<IMapperConfigurationExpression>> configs = new List<Action<IMapperConfigurationExpression>>();

        /// <summary>
        /// 自动映射配置列表
        /// </summary>
        private readonly static IList<IAutoMapperConfig> mapperConfigs = new List<IAutoMapperConfig>();

        /// <summary>
        /// 注册配置
        /// </summary>
        /// <param name="config">配置回调</param>
        public static void RegisterConfig(Action<IMapperConfigurationExpression> config)
        {
            configs.Add(config);
        }

        /// <summary>
        /// 自动找出实现IAutoMapperConfig接口的配置
        /// </summary>
        /// <param name="assemblyNames">程序集名数组</param>
        public static void AutoRegisterConfig(string[] assemblyNames)
        {
            if (assemblyNames.IsNullOrLength0())
            {
                return;
            }

            var assemblies = new Assembly[assemblyNames.Length];
            for (var i = 0; i < assemblies.Length; i++)
            {
                assemblies[i] = Assembly.Load(assemblyNames[i]);
            }

            AutoRegisterConfig(assemblies);
        }

        /// <summary>
        /// 自动找出实现IAutoMapperConfig接口的配置
        /// </summary>
        /// <param name="assemblies">程序集数组</param>
        public static void AutoRegisterConfig(Assembly[] assemblies)
        {
            var types = ReflectUtil.GetImplClassType(assemblies, typeof(IAutoMapperConfig));
            if (types.IsNullOrLength0())
            {
                return;
            }

            foreach (var t in types)
            {
                var config = t.Assembly.CreateInstance(t.FullName) as IAutoMapperConfig;
                mapperConfigs.Add(config);
            }
        }

        /// <summary>
        /// 生成映射配置
        /// 会循环生成注册的配置
        /// 程序启动时执行，只需执行一次
        /// 如果已经生成过，则会忽略
        /// </summary>
        public static void Builder()
        {
            if (mapperConfiguration != null)
            {
                return;
            }

            mapperConfiguration = new MapperConfiguration(config =>
            {
                if (!mapperConfigs.IsNullOrCount0())
                {
                    foreach (var m in mapperConfigs)
                    {
                        m.Register(config);
                    }
                }

                foreach (var c in configs)
                {
                    c(config);
                }
            });
        }
    }
}
