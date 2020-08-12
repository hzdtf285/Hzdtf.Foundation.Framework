using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Sequence;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 序列服务
    /// @ 黄振东
    /// </summary>
    public partial class SequenceService
    {
        /// <summary>
        /// 序列规则
        /// </summary>
        public ISequenceRule SequenceRule
        {
            get;
            set;
        }

        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="seqType">序列类型</param>
        /// <param name="noLength">序列号长度</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<string> BuildNo([DisplayName2("序列类型"), Required, MinLength(2), MaxLength(2)] string seqType, byte noLength = 13, string connectionId = null, BasicUserInfo currUser = null)
        {
            return ExecReturnFuncAndConnectionId<string>((reInfo, connId) =>
            {
                DateTime currTime = DateTime.Now;
                int incr = 0;
                SequenceInfo sequenceInfo = Persistence.SelectBySeqType(seqType, connId);
                if (sequenceInfo == null)
                {
                    sequenceInfo = new SequenceInfo()
                    {
                        SeqType = seqType,
                        UpdateDate = currTime,
                        Increment = 1
                    };
                    SetCreateInfo(sequenceInfo, currUser);

                    Persistence.Insert(sequenceInfo, connId);                    
                }
                else
                {
                    if (currTime.Year == sequenceInfo.UpdateDate.Year
                    && currTime.Month == sequenceInfo.UpdateDate.Month
                    && currTime.Day == sequenceInfo.UpdateDate.Day)
                    {
                        incr = sequenceInfo.Increment;
                        sequenceInfo.Increment++;
                    }
                    else
                    {
                        sequenceInfo.Increment = incr + 1;
                    }

                    sequenceInfo.UpdateDate = currTime;
                    SetModifyInfo(sequenceInfo, currUser);

                    Persistence.UpdateIncrementById(sequenceInfo, connId);
                }

                return SequenceRule.BuilderNo(seqType, noLength, incr, currUser);
            }, null, connectionId);
        }
    }
}
