using System;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Platform.Contract.Standard.Config.ProgramStart;
using Hzdtf.Utility.Standard.Data;

namespace Hzdtf.Platform.Contract.Standard.Config.ProgramStart
{
    /// <summary>
    /// 程序开始配置Json
    /// @ 黄振东
    /// </summary>
    public class ProgramStartConfigJson : IReader<ProgramStartInfo[]>
    {
        #region 属性与字段

        /// <summary>
        /// JSON文件
        /// </summary>
        private readonly string jsonFile;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public ProgramStartConfigJson()
            : this($"{AppContext.BaseDirectory}Config/programStartConfig.json")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFile">JSON文件</param>
        public ProgramStartConfigJson(string jsonFile) => this.jsonFile = jsonFile;

        #endregion

        #region IProgramConfigReader 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ProgramStartInfo[] Reader() => JsonUtil.Deserialize<ProgramStartInfo[]>(jsonFile.ReaderFileContent());

        #endregion
    }
}
