using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Event
{
    /// <summary>
    /// 事件处理接口
    /// @ 黄振东
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="eventData">事件数据</param>
        void Execute(EventData eventData);
    }
}
