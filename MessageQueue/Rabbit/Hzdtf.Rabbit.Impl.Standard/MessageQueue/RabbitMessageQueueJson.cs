using Hzdtf.Rabbit.Contract.Standard.MessageQueue;
using Hzdtf.Rabbit.Model.Standard.Connection;
using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Utils;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列JSON
    /// @ 黄振东
    /// </summary>
    public class RabbitMessageQueueJson : RabbitMessageQueueBase, IRabbitConfigReader
    {
        #region 属性与字段

        /// <summary>
        /// JSON文件名
        /// </summary>
        private readonly string jsonFileName;

        /// <summary>
        /// 文件映射配置字典，key：虚拟路径；value：Rabbit交换机列表
        /// </summary>
        private readonly static IDictionary<string, IList<RabbitExchangeInfo>> dicVirtPathMapConfig = new Dictionary<string, IList<RabbitExchangeInfo>>();

        /// <summary>
        /// 同步文件映射配置字典
        /// </summary>
        private readonly static object syncDicFileMapConfig = new object();

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public RabbitMessageQueueJson()
            : this($"{AppContext.BaseDirectory}Config/rabbitMessageQueue.json")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="jsonFileName">JSON文件名</param>
        public RabbitMessageQueueJson(string jsonFileName)
        {
            ValidateUtil.ValidateNullOrEmpty(jsonFileName, "Json文件名");

            this.jsonFileName = jsonFileName;
        }

        #endregion

        #region IRabbitConfigReader 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public RabbitConfigInfo Reader()
        {
            return JsonUtil.DeserializeFromFile<RabbitConfigInfo>(jsonFileName);
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 从源头查询交换机信息列表
        /// </summary>
        /// <returns>交换机信息列表</returns>
        protected override IList<RabbitExchangeInfo> QueryExchangeInfosFromSource(string virtualPath = RabbitConnectionInfo.DEFAULT_VIRTUAL_PATH)
        {
            if (dicVirtPathMapConfig.Count == 0)
            {
                var config = Reader();
                if (config == null)
                {
                    return null;
                }
                lock (syncDicFileMapConfig)
                {
                    try
                    {
                        foreach (var item in config.Rabbit)
                        {
                            dicVirtPathMapConfig.Add(item.VirtualPath, item.Exchanges);
                        }
                    }
                    catch (ArgumentException) { }
                }
            }

            if (dicVirtPathMapConfig.ContainsKey(virtualPath))
            {
                return dicVirtPathMapConfig[virtualPath];
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}