using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data
{
    /// <summary>
    /// 数据处理委托
    /// @ 黄振东
    /// </summary>
    /// <param name="o">引发对象</param>
    /// <param name="e">数据事件参数</param>
    public delegate void DataHandler(object o, DataEventArgs e);

    /// <summary>
    /// 数据事件参数
    /// </summary>
    public class DataEventArgs : EventArgs
    {
        /// <summary>
        /// 数据
        /// </summary>
        private readonly object data;

        /// <summary>
        /// 数据
        /// </summary>
        public object Data
        {
            get { return data; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="data"></param>
        public DataEventArgs(object data = null)
        {
            this.data = data;
        }
    }
}
