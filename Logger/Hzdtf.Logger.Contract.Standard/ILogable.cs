using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 可记录日志的接口
    /// @ 黄振东
    /// </summary>
    public interface ILogable : ILogableAsync
    {
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        void Debug(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        void Info(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        void Wran(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        void Error(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 致命
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        void Fatal(string msg, Exception ex = null, string source = null, params string[] tags);
    }
}
