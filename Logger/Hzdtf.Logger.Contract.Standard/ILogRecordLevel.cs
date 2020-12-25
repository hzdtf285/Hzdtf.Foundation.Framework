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

        /// <summary>
        /// 设置记录级别
        /// </summary>
        /// <param name="level">记录级别</param>
        void SetRecordLevel(string level);
    }

    /// <summary>
    /// 默认日志记录级别
    /// @ 黄振东
    /// </summary>
    public class DefaultLogRecordLevel : ILogRecordLevel
    {
        /// <summary>
        /// 等级
        /// </summary>
        private static string level = "info";

        /// <summary>
        /// 同步等级
        /// </summary>
        private static readonly object syncLevel = new object();

        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        public string GetRecordLevel() => level;

        /// <summary>
        /// 设置记录级别
        /// </summary>
        /// <param name="level">记录级别</param>
        public void SetRecordLevel(string level)
        {
            lock (syncLevel)
            {
                DefaultLogRecordLevel.level = level;
            }
        }
    }
}
