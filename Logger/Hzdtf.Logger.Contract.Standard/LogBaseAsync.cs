using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 日志基类
    /// @ 黄振东
    /// </summary>
    public partial class LogBase
    {
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
            return Task.Factory.StartNew(() => Debug(msg, ex, source, tags));
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
            return Task.Factory.StartNew(() => Info(msg, ex, source, tags));
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
            return Task.Factory.StartNew(() => Wran(msg, ex, source, tags));
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
            return Task.Factory.StartNew(() => Error(msg, ex, source, tags));
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
            return Task.Factory.StartNew(() => Fatal(msg, ex, source, tags));
        }
    }
}
