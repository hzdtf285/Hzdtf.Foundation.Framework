using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hzdtf.Utility.Standard.Data.Config
{
    /// <summary>
    /// 文件配置基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public abstract class FileConfigurationBase<T> : IConfigurationData<T>
    {
        /// <summary>
        /// 文件
        /// </summary>
        protected readonly string file;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="isExecWrite">是否执行写入</param>
        public FileConfigurationBase(string file, bool isExecWrite = true)
        {
            this.file = file;
            if (isExecWrite)
            {
                InitFile(file);
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isExecWrite">是否执行写入</param>
        public FileConfigurationBase(T data, bool isExecWrite = true)
        {
            if (isExecWrite)
            {
                Write(data);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public abstract T Reader();

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="data">数据</param>
        public void Write(T data)
        {
            WriteToStorage(data);
        }

        /// <summary>
        /// 初始化文件
        /// </summary>
        /// <param name="file">文件</param>
        protected void InitFile(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
            {
                return;
            }

            if (File.Exists(file))
            {
                Write(ConvertFromFile(file));
            }
        }

        /// <summary>
        /// 写入到存储里
        /// </summary>
        /// <param name="data">数据</param>
        protected abstract void WriteToStorage(T data);

        /// <summary>
        /// 从文件里转换为数据
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>数据</returns>
        protected abstract T ConvertFromFile(string file);
    }
}
