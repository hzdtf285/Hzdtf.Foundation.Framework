using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 序列持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface ISequencePersistence
    {
        /// <summary>
        /// 根据序列类型查询序列信息
        /// </summary>
        /// <param name="seqType">序列类型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>序列信息</returns>
        SequenceInfo SelectBySeqType(string seqType, string connectionId = null);

        /// <summary>
        /// 根据ID更新增量
        /// </summary>
        /// <param name="sequenceInfo">序列信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateIncrementById(SequenceInfo sequenceInfo, string connectionId = null);
    }
}
