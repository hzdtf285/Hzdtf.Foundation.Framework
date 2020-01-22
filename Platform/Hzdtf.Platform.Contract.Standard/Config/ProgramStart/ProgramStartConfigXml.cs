using System;
using System.Xml;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Platform.Contract.Standard.Config.ProgramStart;
using Hzdtf.Utility.Standard.Data;

namespace Hzdtf.Platform.Contract.Standard.Config.ProgramStart
{
    /// <summary>
    /// 程序开始配置XML
    /// @ 黄振东
    /// </summary>
    public class ProgramStartConfigXml : IReader<ProgramStartInfo[]>
    {
        #region 属性与字段

        /// <summary>
        /// XML文件
        /// </summary>
        private readonly string xmlFile;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public ProgramStartConfigXml()
            : this($"{AppContext.BaseDirectory}Config/programStartConfig.xml")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFile">XML文件</param>
        public ProgramStartConfigXml(string xmlFile) => this.xmlFile = xmlFile;

        #endregion

        #region IProgramConfigReader 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ProgramStartInfo[] Reader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("ProgramStarts/ProgramStart");
            if (xmlNodeList.IsNullOrCount0())
            {
                return null;
            }

            ProgramStartInfo[] result = new ProgramStartInfo[xmlNodeList.Count];
            int i = 0;
            xmlNodeList.ForEach(x =>
            {
                result[i] = new ProgramStartInfo()
                {
                    FullClass = x.SelectSingleNode("FullClass").InnerText
                };
                XmlNodeList argsNode = x.SelectNodes("Args/Arg");
                if (argsNode.IsNullOrCount0())
                {
                    i++;
                    return;
                }

                int j = 0;
                result[i].Args = new string[argsNode.Count];
                argsNode.ForEach(y =>
                {
                    result[i].Args[j] = y.InnerText;
                    j++;
                });

                i++;
            });

            return result;
        }

        #endregion
    }
}
