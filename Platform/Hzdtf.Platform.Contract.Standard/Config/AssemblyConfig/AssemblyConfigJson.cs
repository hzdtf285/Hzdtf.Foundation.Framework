using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig
{
    /// <summary>
    /// 程序集配置JSON
    /// @ 黄振东
    /// </summary>
    public class AssemblyConfigJson : IReader<AssemblyConfigInfo>
    {
        #region 属性与字段

        /// <summary>
        /// xmlFileName
        /// </summary>
        private readonly string jsonFileName;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public AssemblyConfigJson()
            : this($"{AppContext.BaseDirectory}Config/assemblyConfig.json")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFileName">json文件名</param>
        public AssemblyConfigJson(string jsonFileName) => this.jsonFileName = jsonFileName;

        #endregion

        #region IReader<AssemblyConfigInfo> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public AssemblyConfigInfo Reader() => JsonUtil.Deserialize<AssemblyConfigInfo>(jsonFileName.ReaderFileContent());

        #endregion
    }
}
