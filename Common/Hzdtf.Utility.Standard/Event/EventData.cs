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
        private DateTime _Time;

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

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

        /// <summary>
        /// 构造方法
        /// </summary>
        public EventData() => _Time = DateTime.Now;
    }
}
