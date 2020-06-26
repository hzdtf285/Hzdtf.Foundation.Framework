using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Event
{
    /// <summary>
    /// 事件数据
    /// @ 黄振东
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        } = DateTime.Now;

        /// <summary>
        /// 源
        /// </summary>
        public object Source
        {
            get;
            set;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data
        {
            get;
            set;
        }
    }
}
