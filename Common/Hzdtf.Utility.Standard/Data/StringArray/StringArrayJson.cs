using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hzdtf.Utility.Standard.Data.StringArray
{
    /// <summary>
    /// 字节串数组JSON
    /// @ 黄振东
    /// </summary>
    public class StringArrayJson : IReader<string[]>
    {
        /// <summary>
        /// Json文件
        /// </summary>
        private readonly string jsonFile;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">Json文件</param>
        public StringArrayJson(string jsonFile) => this.jsonFile = jsonFile;

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public string[] Reader()
        {
            return JsonUtil.Deserialize<string[]>(File.ReadAllText(this.jsonFile));
        }
    }
}
