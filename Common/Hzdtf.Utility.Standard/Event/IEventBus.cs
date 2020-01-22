using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Event
{
    /// <summary>
    /// 事件总线接口
    /// @ 黄振东
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 绑定事件源与事件处理的关系
        /// </summary>
        /// <param name="eventSourceType">事件源类型</param>
        /// <param name="eventHandlerType">事件处理类型</param>
        void Bind(Type eventSourceType, Type eventHandlerType);

        /// <summary>
        /// 解绑事件源与事件处理的关系
        /// </summary>
        /// <param name="eventSourceType">事件源类型</param>
        /// <param name="eventHandlerType">事件处理类型</param>
        void UnBind(Type eventSourceType, Type eventHandlerType);

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventSourceType">事件源类型</param>
        /// <param name="eventData">事件数据</param>
        void Publish(Type eventSourceType, EventData eventData);
    }
}
