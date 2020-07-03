using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列XML
    /// @ 黄振东
    /// </summary>
    public class RabbitMessageQueueXml : RabbitMessageQueueBase
    {
        #region 属性与字段

        /// <summary>
        /// XML文档
        /// </summary>
        private readonly XmlDocument xmlDoc;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitMessageQueueXml()
            : this($"{AppContext.BaseDirectory}Config/rabbitMessageQueue.xml")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="xmlFileName">XML文件名</param>
        public RabbitMessageQueueXml(string xmlFileName)
        {
            ValidateUtil.ValidateNullOrEmpty(xmlFileName, "XML文件名");

            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 从源头查询交换机信息列表
        /// </summary>
        /// <returns>交换机信息列表</returns>
        protected override IList<RabbitExchangeInfo> QueryExchangeInfosFromSource()
        {
            XmlNodeList nodeList = xmlDoc.SelectNodes("exchanges/exchange");
            if (nodeList.IsNullOrCount0())
            {
                return null;
            }

            IList<RabbitExchangeInfo> exchanges = new List<RabbitExchangeInfo>();
            nodeList.ForEach(x =>
            {
                RabbitExchangeInfo exchange = new RabbitExchangeInfo();
                if (x.Attributes["name"] != null)
                {
                    exchange.Name = x.Attributes["name"].Value;
                }
                if (x.SelectSingleNode("type") != null)
                {
                    exchange.Type = x.SelectSingleNode("type").InnerText;
                }
                if (x.SelectSingleNode("persistent") != null)
                {
                    exchange.Persistent = Convert.ToBoolean(x.SelectSingleNode("persistent").InnerText);
                }

                XmlNodeList xmlQueues = x.SelectNodes("queues/queue");
                if (!xmlQueues.IsNullOrCount0())
                {
                    exchange.Queues = new List<RabbitQueueModel>(xmlQueues.Count);

                    xmlQueues.ForEach(y =>
                    {
                        RabbitQueueModel rabbitQueue = new RabbitQueueModel();
                        if (y.Attributes["name"] != null)
                        {
                            rabbitQueue.Name = y.Attributes["name"].Value;
                        }
                        if (y.SelectSingleNode("routingKeys") != null)
                        {
                            rabbitQueue.RoutingKeys = y.SelectSingleNode("routingKeys").InnerText.Split(',');
                        }
                        if (y.SelectSingleNode("autoDelQueue") != null)
                        {
                            rabbitQueue.AutoDelQueue = Convert.ToBoolean(y.SelectSingleNode("autoDelQueue").InnerText);
                        }
                        if (y.SelectSingleNode("qos") != null)
                        {
                            rabbitQueue.Qos = Convert.ToUInt16(y.SelectSingleNode("qos").InnerText);
                        }

                        exchange.Queues.Add(rabbitQueue);
                    });
                }

                exchanges.Add(exchange);
            });

            return exchanges;
        }

        #endregion
    }
}