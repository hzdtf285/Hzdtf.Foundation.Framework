using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 日志记录级别接口
    /// @ 黄振东
    /// </summary>
    public interface ILogRecordLevel
    {
        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        string GetRecordLevel();
    }

    /// <summary>
    /// 默认日志记录级别
    /// </summary>
    public class DefaultLogRecordLevel : ILogRecordLevel
    {
        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        public string GetRecordLevel() => "debug";
    }
}
