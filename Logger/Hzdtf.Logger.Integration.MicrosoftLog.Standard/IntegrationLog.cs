using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Hzdtf.Logger.Text.Integration.MicrosoftLog.Standard
{
    /// <summary>
    /// 集成日志
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IntegrationLog : ILogable, ILogger, ISetObject<ILogable>
    {
        #region 属性与字段

        /// <summary>
        /// 分类名称
        /// </summary>
        private readonly string categoryName;

        /// <summary>
        /// 原生日志
        /// </summary>
        private ILogable protoLog;

        /// <summary>
        /// 原生日志
        /// </summary>
        protected ILogable ProtoLog
        {
            get
            {
                if (protoLog == null)
                {
                    protoLog = LogTool.DefaultLog;
                }

                return protoLog;
            }
        }

        /// <summary>
        /// 日志记录级别
        /// </summary>
        public ILogRecordLevel LogRecordLevel
        {
            get;
            set;
        } = new DefaultLogRecordLevel();

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public IntegrationLog()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="categoryName">分类名称</param>
        /// <param name="protoLog">原生日志</param>
        /// <param name="logRecordLevel">日志记录等级</param>
        public IntegrationLog(string categoryName, ILogable protoLog, ILogRecordLevel logRecordLevel)
        {
            this.categoryName = categoryName;
            this.protoLog = protoLog;
            this.LogRecordLevel = logRecordLevel;
        }

        #region ILogable 接口

        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        public void Trace(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            ProtoLog.Trace(msg, ex, source, tags);
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
            HandleMsgAndSource(ref msg, ref source);
            ProtoLog.Debug(msg, ex, source, tags);
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
            HandleMsgAndSource(ref msg, ref source);
            ProtoLog.Info(msg, ex, source, tags);
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
            HandleMsgAndSource(ref msg, ref source);
            ProtoLog.Wran(msg, ex, source, tags);
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
            HandleMsgAndSource(ref msg, ref source);
            ProtoLog.Error(msg, ex, source, tags);
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
            HandleMsgAndSource(ref msg, ref source);
            ProtoLog.Fatal(msg, ex, source, tags);
        }

        /// <summary>
        /// 异步跟踪
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task TraceAsync(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            return ProtoLog.TraceAsync(msg, ex, source, tags);
        }

        /// <summary>
        /// 异步调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task DebugAsync(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            return ProtoLog.DebugAsync(msg, ex, source, tags);
        }

        /// <summary>
        /// 异步信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task InfoAsync(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            return ProtoLog.InfoAsync(msg, ex, source, tags);
        }

        /// <summary>
        /// 异步警告
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task WranAsync(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            return ProtoLog.WranAsync(msg, ex, source, tags);
        }

        /// <summary>
        /// 异步错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task ErrorAsync(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            return ProtoLog.ErrorAsync(msg, ex, source, tags);
        }

        /// <summary>
        /// 异步致命
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        public Task FatalAsync(string msg, Exception ex = null, string source = null, params string[] tags)
        {
            HandleMsgAndSource(ref msg, ref source);
            return ProtoLog.FatalAsync(msg, ex, source, tags);
        }

        #endregion

        #region ILogger 接口

        /// <summary>
        /// 开启范围
        /// </summary>
        /// <typeparam name="TState">状态类型</typeparam>
        /// <param name="state">状态</param>
        /// <returns>释放对象</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// 日志等级是否开启
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <returns>日志等级是否开启</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return LogLevelHelper.IsNeedWriteLog(logLevel.ToString(), LogRecordLevel.GetRecordLevel());
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <typeparam name="TState">状态类型</typeparam>
        /// <param name="logLevel">日志等级</param>
        /// <param name="eventId">事件ID</param>
        /// <param name="state">状态</param>
        /// <param name="exception">异常</param>
        /// <param name="formatter">格式</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.None)
            {
                return;
            }

            string msg = $"eventId:{eventId},{state}";
            switch (logLevel)
            {
                case LogLevel.Trace:
                    TraceAsync(msg, exception);

                    break;

                case LogLevel.Debug:
                    DebugAsync(msg, exception);

                    break;

                case LogLevel.Information:
                    InfoAsync(msg, exception);

                    break;

                case LogLevel.Warning:
                    WranAsync(msg, exception);

                    break;

                case LogLevel.Error:
                    ErrorAsync(msg, exception);

                    break;

                case LogLevel.Critical:
                    FatalAsync(msg, exception);

                    break;
            }
        }

        #endregion

        #region ISetObject<ILogable>

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="obj">对象</param>
        public void Set(ILogable obj)
        {
            this.protoLog = obj;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 处理消息和来源
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="source">来源</param>
        private void HandleMsgAndSource(ref string msg, ref string source)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                source = categoryName;
            }
            else
            {
                msg = $"{categoryName}:{msg}";
            }
        }

        #endregion
    }
}
