using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Model.Standard.Utils
{
    /// <summary>
    /// Rabbit辅助类
    /// @ 黄振东
    /// </summary>
    public static class RabbitUtil
    {
        /// <summary>
        /// 获取发布键
        /// </summary>
        /// <param name="publishKey">发布键</param>
        /// <param name="protoKeys">原生键集合</param>
        /// <returns>发布键</returns>
        public static string GetPublishKey(string publishKey, string[] protoKeys)
        {
            return string.IsNullOrWhiteSpace(publishKey) ? protoKeys[0] : publishKey;
        }

        /// <summary>
        /// 判断两个交换机名是否匹配
        /// </summary>
        /// <param name="exchange1">交换机1</param>
        /// <param name="exchange2">交换机2</param>
        /// <returns>两个交换机名是否匹配</returns>
        public static bool IsTwoExchangeEqual(string exchange1, string exchange2)
        {
            if (string.IsNullOrWhiteSpace(exchange1) && string.IsNullOrWhiteSpace(exchange2))
            {
                return true;
            }

            return exchange1 != null && exchange1.Equals(exchange2);
        }
    }
}
