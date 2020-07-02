using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data.Config
{
    /// <summary>
    /// Json文件配置基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public abstract class JsonFileConfigurationBase<T> : FileConfigurationBase<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="isExecWrite">是否执行写入</param>
        public JsonFileConfigurationBase(string file, bool isExecWrite = true)
            : base(file, isExecWrite)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isExecWrite">是否执行写入</param>
        public JsonFileConfigurationBase(T data, bool isExecWrite = true)
            : base(data, isExecWrite)
        {
        }

        /// <summary>
        /// 从文件里转换为数据
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>数据</returns>
        protected override T ConvertFromFile(string file) => JsonUtil.DeserializeFromFile<T>(file);
    }
}
