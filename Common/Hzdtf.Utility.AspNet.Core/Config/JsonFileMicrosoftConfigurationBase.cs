using Hzdtf.Utility.Standard.Data.Config;
using Hzdtf.Utility.Standard.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.Config
{
    /// <summary>
    /// Json文件微软配置基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public abstract class JsonFileMicrosoftConfigurationBase<T> : JsonFileConfigurationBase<T>
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected IConfiguration configuration;

        /// <summary>
        /// 配置生成前回调
        /// </summary>
        private readonly Action<IConfigurationBuilder> beforeConfigurationBuilder;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">json文件</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public JsonFileMicrosoftConfigurationBase(string jsonFile, Action<IConfigurationBuilder> beforeConfigurationBuilder = null) 
            : base(jsonFile, false)
        {
            this.beforeConfigurationBuilder = beforeConfigurationBuilder;
            InitFile(jsonFile);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="beforeConfigurationBuilder">配置生成前回调</param>
        public JsonFileMicrosoftConfigurationBase(T data, Action<IConfigurationBuilder> beforeConfigurationBuilder = null)
            : base(data, false)
        {
            this.beforeConfigurationBuilder = beforeConfigurationBuilder;
            Write(data);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public override T Reader()
        {
            if (configuration == null)
            {
                return default(T);
            }

            return configuration.Get<T>();
        }

        /// <summary>
        /// 写入到存储里
        /// </summary>
        /// <param name="data">数据</param>
        protected override void WriteToStorage(T data)
        {
            var jsonStr = JsonUtil.SerializeIgnoreNull(data);
            using (var stream = StreamUtil.WriteStream(jsonStr))
            {
                var builder = new ConfigurationBuilder().AddJsonStream(stream);
                if (beforeConfigurationBuilder != null)
                {
                    beforeConfigurationBuilder(builder);
                }
                configuration = builder.Build();
            }
        }
    }
}
