using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Logger.Contract.Standard
{
    /// <summary>
    /// 控制台日志
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class ConsoleLog : ContentLogBase
    {
        #region 重写父类的方法

        /// <summary>
        /// 将日志内容写入到存储设备里
        /// </summary>
        /// <param name="logContent">日志内容</param>
        protected override void WriteStorage(string logContent)
        {
            Console.WriteLine(logContent);
        }

        /// <summary>
        /// 分段分隔符
        /// </summary>
        /// <returns>分段分隔符</returns>
        protected override string SectionPartitionSymbol() => Environment.NewLine;

        #endregion
    }
}
