using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 日志基类
    /// @ 黄振东
    /// </summary>
    public abstract partial class LogBase : ILogable
    {
        #region 属性与字段

        /// <summary>
        /// 日志记录级别
        /// </summary>
        public ILogRecordLevel LogRecordLevel
        {
            get;
            set;
        } = new DefaultLogRecordLevel();

        /// <summary>
        /// 本地ID标签数组
        /// </summary>
        private static readonly string[] localIdTags = new string[] { NetworkUtil.LocalIP, Environment.MachineName };

        #endregion

        #region ILog 接口

        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Trace(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            BeforeWriteStorage("trace", msg, ex, source, tags);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Debug(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            BeforeWriteStorage("debug", msg, ex, source, tags);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Info(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            BeforeWriteStorage("info", msg, ex, source, tags);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Wran(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            BeforeWriteStorage("wran", msg, ex, source, tags);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Error(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            BeforeWriteStorage("error", msg, ex, source, tags);
        }

        /// <summary>
        /// 致命
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Fatal(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            BeforeWriteStorage("fatal", msg, ex, source, tags);
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 将消息与异常写入到存储设备里前
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="msg">消息</param>
        /// <param name="tags">标签</param>
        /// <param name="source">来源</param>
        /// <param name="ex">异常</param>
        protected virtual void BeforeWriteStorage(string level, string msg, Exception ex = null, string source = null, params string[] tags)
        {
            if (LogLevelHelper.IsNeedWriteLog(level, LogRecordLevel.GetRecordLevel()))
            {
                WriteStorage(level, msg, ex.GetLastInnerException(), source, tags);
            }
        }

        /// <summary>
        /// 将消息与异常写入到存储设备里
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        protected abstract void WriteStorage(string level, string msg, Exception ex = null, string source = null, params string[] tags);

        #endregion

        /// <summary>
        /// 追加本地标识标签
        /// </summary>
        /// <param name="tag">标签</param>
        /// <returns>本地标识标签</returns>
        protected string[] AppendLocalIdTags(params string[] tag) => localIdTags.Merge(tag);
    }

    /// <summary>
    /// 日志级别帮助类
    /// @ 黄振东
    /// </summary>
    public static class LogLevelHelper
    {
        /// <summary>
        /// 解析日志级别枚举
        /// </summary>
        /// <param name="level">级别</param>
        /// <returns>日志级别枚举</returns>
        public static LogLevelEnum Parse(string level)
        {
            switch (level.ToLower())
            {
                case "trace":
                    return LogLevelEnum.TRACE;

                case "debug":
                    return LogLevelEnum.DEBUG;

                case "info":
                case "information":
                    return LogLevelEnum.INFO;

                case "wran":
                case "warning":
                    return LogLevelEnum.WRAN;

                case "error":
                    return LogLevelEnum.ERROR;

                case "fatal":
                case "critical":
                    return LogLevelEnum.FATAL;

                default:
                    return LogLevelEnum.NONE;
            }
        }

        /// <summary>
        /// 判断是否需要写入日志
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="recordLogLevel">记录日志级别</param>
        /// <returns>是否需要写入日志</returns>
        public static bool IsNeedWriteLog(string level, string recordLogLevel)
        {
            LogLevelEnum levelEnum = Parse(level);
            if (levelEnum == LogLevelEnum.NONE)
            {
                return false;
            }

            return levelEnum >= Parse(recordLogLevel);
        }
    }
}
