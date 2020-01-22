using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Hzdtf.Utility.Standard.Data.StringArray
{
    /// <summary>
    /// 字节串数组Xml
    /// @ 黄振东
    /// </summary>
    public class StringArrayXml : IReader<string[]>
    {
        /// <summary>
        /// XML文件
        /// </summary>
        private readonly string xmlFile;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFile">XML文件</param>
        public StringArrayXml(string xmlFile) => this.xmlFile = xmlFile;

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public string[] Reader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);

            XmlNodeList nodeList = xmlDoc.SelectNodes("Strings/String");
            if (nodeList.IsNullOrCount0())
            {
                return null;
            }

            string[] result = new string[nodeList.Count];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = nodeList[i].InnerText;
            }

            return result;
        }
    }
}
