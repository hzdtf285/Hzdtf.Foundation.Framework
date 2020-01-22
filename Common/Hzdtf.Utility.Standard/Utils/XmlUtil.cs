using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// XML辅助类
    /// @ 黄振东
    /// </summary>
    public static class XmlUtil
    {
        /// <summary>
        /// 判断XML节点列表是否为null或数量是否为0
        /// </summary>
        /// <param name="xmlNodeList">XML节点列表</param>
        /// <returns>XML节点列表是否为null或数量是否为0</returns>
        public static bool IsNullOrCount0(this XmlNodeList xmlNodeList) => xmlNodeList == null || xmlNodeList.Count == 0;

        /// <summary>
        /// 判断XML节点是否为null或子节点数是否为0
        /// </summary>
        /// <param name="xmlNode">XML节点</param>
        /// <returns>XML节点是否为null或子节点数是否为0</returns>
        public static bool IsNullOrChildrenCount0(this XmlNode xmlNode) => xmlNode == null || IsNullOrCount0(xmlNode.ChildNodes) ? true : false;
       
        /// <summary>
        /// 获取子节点文本
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="childNodeName">子节点名称</param>
        /// <returns>子节点文本</returns>
        public static string GetChildNodeInnerTextByNode(this XmlNode node, string childNodeName) => node.SelectSingleNode(childNodeName) != null ? node.SelectSingleNode(childNodeName).InnerText : null;
   
        /// <summary>
        /// 循环XML节点列表
        /// </summary>
        /// <param name="xmlNodeList">XML节点列表</param>
        /// <param name="callback">回调方法</param>
        public static void ForEach(this XmlNodeList xmlNodeList, Action<XmlNode> callback)
        {
            if (xmlNodeList.Count == 0 || callback == null)
            {
                return;
            }

            foreach (XmlNode node in xmlNodeList)
            {
                callback(node);
            }
        }

        /// <summary>
        /// 根据节点列表路径查找节点列表
        /// </summary>
        /// <param name="xmlDocument">XML文档对象</param>
        /// <param name="nodeListPath">节点列表路径</param>
        /// <param name="eurekaNumberAction">找到了数量动作</param>
        /// <param name="eurekaNodeFunc">找到了节点函数</param>
        public static void FindNodeList(this XmlDocument xmlDocument, string nodeListPath,
            Action<int> eurekaNumberAction, Func<XmlNode, int, bool> eurekaNodeFunc)
        {
            if (string.IsNullOrWhiteSpace(nodeListPath) || eurekaNumberAction == null || eurekaNodeFunc == null)
            {
                return;
            }

            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(nodeListPath);
            if (xmlNodeList.IsNullOrCount0())
            {
                return;
            }

            eurekaNumberAction(xmlNodeList.Count);
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                if (eurekaNodeFunc(xmlNodeList[i], i))
                {
                    continue;
                }

                return;
            }
        }
    }
}
