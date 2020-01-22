using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.Data.Dic
{
    /// <summary>
    /// 字典XML
    /// @ 黄振东
    /// </summary>
    public class DictionaryXml : IReader<IDictionary<string, string>>
    {
        /// <summary>
        /// XML文件
        /// </summary>
        private readonly string xmlFile;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFile">XML文件</param>
        public DictionaryXml(string xmlFile) => this.xmlFile = xmlFile;

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public IDictionary<string, string> Reader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            XmlNodeList nodeList = xmlDoc.SelectNodes("Dictionarys/Dictionary");
            if (nodeList.IsNullOrCount0())
            {
                return null;
            }

            IDictionary<string, string> result = new Dictionary<string, string>(nodeList.Count);
            nodeList.ForEach(x =>
            {
                result.Add(x.Attributes["Key"].Value, x.InnerText);
            });

            return result;
        }
    }
}
