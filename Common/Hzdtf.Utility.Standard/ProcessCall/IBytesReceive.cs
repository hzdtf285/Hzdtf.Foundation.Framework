using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// 字节数组接收接口
    /// @ 黄振东
    /// </summary>
    public interface IBytesReceive
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="receiveMessageFun">接收消息回调</param>
        void Receive(Func<byte[], byte[]> receiveMessageFun);
    }
}
