using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Contract.Standard.Connection
{
    /// <summary>
    /// Rabbit渠道接口
    /// @ 黄振东
    /// </summary>
    public interface IRabbitChannel
    {
        /// <summary>
        /// 获取渠道数
        /// </summary>
        /// <returns>渠道数</returns>
        int GetChannelCount();

        /// <summary>
        /// 关闭渠道集合
        /// </summary>
        /// <param name="topCount">前几个要关闭的，如果为-1则表示全部</param>
        void CloseChannels(int topCount = -1);
    }
}
