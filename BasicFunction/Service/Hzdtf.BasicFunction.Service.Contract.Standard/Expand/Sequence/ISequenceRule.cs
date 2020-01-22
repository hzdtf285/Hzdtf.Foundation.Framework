using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Sequence
{
    /// <summary>
    /// 序列规则接口
    /// @ 黄振东
    /// </summary>
    public interface ISequenceRule
    {
        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="seqType">序列类型</param>
        /// <param name="noLength">序列号长度</param>
        /// <param name="increment">增量</param>
        /// <returns>序列号</returns>
        string BuilderNo(string seqType, byte noLength, int increment);
    }
}
