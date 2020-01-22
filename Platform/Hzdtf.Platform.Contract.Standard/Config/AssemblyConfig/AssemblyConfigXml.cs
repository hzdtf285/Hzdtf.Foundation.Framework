using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig
{
    /// <summary>
    /// 程序集配置XML
    /// @ 黄振东
    /// </summary>
    public class AssemblyConfigXml : IReader<AssemblyConfigInfo>
    {
        #region 属性与字段

        /// <summary>
        /// xmlFileName
        /// </summary>
        private readonly string xmlFileName;

        #endregion

        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        public AssemblyConfigXml()
            : this($"{AppContext.BaseDirectory}Config/assemblyConfig.xml")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFileName">XML文件名</param>
        public AssemblyConfigXml(string xmlFileName) => this.xmlFileName = xmlFileName;

        #endregion

        #region IReader<AssemblyConfigInfo> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public AssemblyConfigInfo Reader()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            AssemblyConfigInfo result = new AssemblyConfigInfo();

            xmlDoc.FindNodeList("Assemblys/Entrances/Assembly", 
            (num) =>
            {
                result.Entrances = new BasicAssemblyInfo[num];
            },
            (node, index) =>
            {
                result.Entrances[index] = ParseBasicAssembly(node);
                return true;
            });

            xmlDoc.FindNodeList("Assemblys/Services/Assembly",
            (num) =>
            {
                result.Services = new AssemblyExpandInfo[num];
            },
            (node, index) =>
            {
                result.Services[index] = ParseExpandAssembly(node);
                return true;
            });

            return result;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 解析程序集基本信息
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>程序集基本信息</returns>
        private AssemblyExpandInfo ParseBasicAssembly(XmlNode node)
        {
            AssemblyExpandInfo assembly = new AssemblyExpandInfo();
            XmlNodeList nameNodeList = node.SelectNodes("Names/Name");
            assembly.Names = new string[nameNodeList.Count];
            for (int i = 0; i < assembly.Names.Length; i++)
            {
                assembly.Names[i] = nameNodeList[i].InnerText;
            }

            XmlNodeList interceptedNodeList = node.SelectNodes("Intercepteds/Intercepted");
            if (!interceptedNodeList.IsNullOrCount0())
            {
                assembly.Intercepteds = new string[interceptedNodeList.Count];
                for (int i = 0; i < interceptedNodeList.Count; i++)
                {
                    assembly.Intercepteds[i] = interceptedNodeList[i].InnerText;
                }
            }

            return assembly;
        }

        /// <summary>
        /// 解析程序集扩展信息
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>程序集扩展信息</returns>
        private AssemblyExpandInfo ParseExpandAssembly(XmlNode node)
        {
            AssemblyExpandInfo assembly = new AssemblyExpandInfo();
            XmlNodeList nameNodeList = node.SelectNodes("Names/Name");
            assembly.Names = new string[nameNodeList.Count];
            for (int i = 0; i < assembly.Names.Length; i++)
            {
                assembly.Names[i] = nameNodeList[i].InnerText;
            }

            string lifecycleText = node.GetChildNodeInnerTextByNode("Lifecycle");
            if (!string.IsNullOrWhiteSpace(lifecycleText))
            {
                assembly.Lifecycle = (LifecycleType)Enum.Parse(typeof(LifecycleType), lifecycleText);
            }

            XmlNodeList interceptedNodeList = node.SelectNodes("Intercepteds/Intercepted");
            if (!interceptedNodeList.IsNullOrCount0())
            {
                assembly.Intercepteds = new string[interceptedNodeList.Count];
                for (int i = 0; i < interceptedNodeList.Count; i++)
                {
                    assembly.Intercepteds[i] = interceptedNodeList[i].InnerText;
                }
            }

            return assembly;
        }

        #endregion
    }
}
