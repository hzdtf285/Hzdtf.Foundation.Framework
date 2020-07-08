using Hzdtf.Utility.Standard.Connection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.MessageQueue.Contract.Standard.Connection
{
    /// <summary>
    /// 消息队列连接工厂接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionWrapInfoT">连接包装信息类型</typeparam>
    public interface IMessageQueueConnectionFactory<ConnectionWrapInfoT> : IConnectionFactory<IMessageQueueConnection, ConnectionInfo, ConnectionWrapInfoT>
        where ConnectionWrapInfoT : ConnectionWrapInfo
    {
    }

    /// <summary>
    /// 消息队列连接工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IMessageQueueConnectionFactory : IConnectionFactory<IMessageQueueConnection, ConnectionInfo, ConnectionWrapInfo>
    {
    }
}
