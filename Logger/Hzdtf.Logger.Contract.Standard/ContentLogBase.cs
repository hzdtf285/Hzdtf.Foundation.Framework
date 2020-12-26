using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 内容日志基类
    /// @ 黄振东
    /// </summary>
    public abstract class ContentLogBase : LogBase
    {
        #region 重写父类的方法

        /// <summary>
        /// 将消息与异常写入到存储设备里
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        protected override void WriteStorage(string level, string msg, Exception ex = null, string source = null, params string[] tags)
        {
            WriteStorage(GetLogContent(level, msg, ex, source, tags), level);
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 将日志内容写入到存储设备里
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="level">等级</param>
        protected abstract void WriteStorage(string logContent, string level);

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取日志内容
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        /// <param name="source">来源</param>
        /// <param name="tags">标签</param>
        /// <returns>日志内容</returns>
        private string GetLogContent(string level, string msg, Exception ex = null, string source = null, params string[] tags)
        {
            string exMsg = ex == null ? null : string.Format("{0}异常:Message:{1}.StackTrace:{2}", SectionPartitionSymbol(), ex.Message, ex.StackTrace);
            string tagMsg = tags == null || tags.Length == 0 ? null : string.Format("{0}标签:{1}", SectionPartitionSymbol(), string.Join(",", AppendLocalIdTags(tags)));
            if (string.IsNullOrWhiteSpace(source) && ex != null)
            {
                source = ex.Source;
            }

            return string.Format("时间:{0}{1}级别:{2}{1}来源:{6}{1}消息:{3}{4}{5}{7}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                SectionPartitionSymbol(),
                level,
                msg,
                tagMsg,
                exMsg,
                source,
                Environment.NewLine);
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 分段分隔符
        /// </summary>
        /// <returns>分段分隔符</returns>
        protected virtual string SectionPartitionSymbol() => " ";

        #endregion
    }
}
