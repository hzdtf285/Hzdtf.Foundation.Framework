using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 可记录日志的异步接口
    /// @ 黄振东
    /// </summary>
    public interface ILogableAsync
    {
        /// <summary>
        /// 异步调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        Task DebugAsync(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 异步信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        Task InfoAsync(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 异步警告
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        Task WranAsync(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 异步错误
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        Task ErrorAsync(string msg, Exception ex = null, string source = null, params string[] tags);

        /// <summary>
        /// 异步致命
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>任务</returns>
        Task FatalAsync(string msg, Exception ex = null, string source = null, params string[] tags);
    }
}
