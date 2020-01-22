using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Hzdtf.Rabbit.Impl.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit消息队列JSON
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RabbitMessageQueueJson : RabbitMessageQueueBase
    {
        #region 属性与字段

        /// <summary>
        /// JSON文件名
        /// </summary>
        private readonly string jsonFileName;

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

        #region 重写父类的方法

        /// <summary>
        /// 从源头查询交换机信息列表
        /// </summary>
        /// <returns>交换机信息列表</returns>
        protected override IList<RabbitExchangeInfo> QueryExchangeInfosFromSource()
        {
            return new JsonConvert().Deserialize<IList<RabbitExchangeInfo>>(File.ReadAllText(jsonFileName));
        }

        #endregion
    }
}