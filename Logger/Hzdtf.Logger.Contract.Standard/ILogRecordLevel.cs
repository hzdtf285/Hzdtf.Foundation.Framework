using NPOI.SS.Formula.Functions;
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
    /// 日志记录等级基类
    /// @ 黄振东
    /// </summary>
    public abstract class LogRecordLevelBase : ILogRecordLevel
    {
        /// <summary>
        /// 等级
        /// </summary>
        private static string level;

        /// <summary>
        /// 同步等级
        /// </summary>
        private static readonly object syncLevel = new object();

        /// <summary>
        /// 获取记录级别
        /// </summary>
        /// <returns>记录级别</returns>
        public string GetRecordLevel()
        {
            if (string.IsNullOrWhiteSpace(level))
            {
                var temp = GetDefaultRecordLevel();
                SetRecordLevel(temp);
            }

            return level;
        }

        /// <summary>
        /// 设置记录级别
        /// </summary>
        /// <param name="level">记录级别</param>
        public void SetRecordLevel(string level)
        {
            lock (syncLevel)
            {
                LogRecordLevelBase.level = level;
            }
        }

        /// <summary>
        /// 获取默认的记录等级
        /// </summary>
        /// <returns>记录级别</returns>
        protected abstract string GetDefaultRecordLevel();
    }

    /// <summary>
    /// 默认日志记录级别
    /// @ 黄振东
    /// </summary>
    public class DefaultLogRecordLevel : LogRecordLevelBase
    {
        /// <summary>
        /// 获取默认的记录等级
        /// </summary>
        /// <returns>记录级别</returns>
        protected override string GetDefaultRecordLevel() => "info";
    }
}
