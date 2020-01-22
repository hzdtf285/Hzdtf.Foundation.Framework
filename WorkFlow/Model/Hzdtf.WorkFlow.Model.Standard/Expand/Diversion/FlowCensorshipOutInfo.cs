using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using MessagePack;

namespace Hzdtf.WorkFlow.Model.Standard.Expand.Diversion
{
    /// <summary>
    /// 流程关卡输出信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class FlowCensorshipOutInfo
    {
        /// <summary>
        /// 工作流
        /// 如果是申请状态，则需要客户端插入，然后手工把工作流ID赋于下一个处理数组和当前更新处理
        /// </summary>
        [JsonProperty("workflow")]
        [MessagePack.Key("workflow")]
        public WorkflowInfo Workflow
        {
            get;
            set;
        }

        /// <summary>
        /// 当前具体关卡处理
        /// </summary>
        [JsonProperty("currConcreteCensorship")]
        [MessagePack.Key("currConcreteCensorship")]
        public ConcreteCensorshipInfo CurrConcreteCensorship
        {
            get;
            set;
        }

        /// <summary>
        /// 下一个具体关卡处理数组
        /// </summary>
        [JsonProperty("nextConcreteCensorshipHandles")]
        [MessagePack.Key("nextConcreteCensorshipHandles")]
        public ConcreteCensorshipInfo[] NextConcreteCensorshipHandles
        {
            get;
            set;
        }

        /// <summary>
        /// 动作类型，默认为送件
        /// </summary>
        [JsonProperty("actionType")]
        [MessagePack.Key("actionType")]
        public ActionType ActionType
        {
            get;
            set;
        } = ActionType.SEND;

        /// <summary>
        /// 客户输入数据
        /// </summary>
        [JsonProperty("clientInData")]
        [MessagePack.Key("clientInData")]
        public object ClientInData
        {
            get;
            set;
        }

        /// <summary>
        /// 判断当前关卡是否申请者关卡
        /// </summary>
        /// <returns>当前关卡是否申请者关卡</returns>
        public bool IsCurrApplicantCensorship()
        {
            if (CurrConcreteCensorship == null || CurrConcreteCensorship.FlowCensorship == null)
            {
                return false;
            }

            return CurrConcreteCensorship.FlowCensorship.OwnerCensorshipType == CensorshipTypeEnum.STANDARD
                    && StandardCensorshipDefine.APPLICANT.Equals(CurrConcreteCensorship.Code);
        }

        /// <summary>
        /// 判断下一个关卡是否结束关卡
        /// </summary>
        /// <returns>下一个关卡是否结束关卡</returns>
        public bool IsNextEndCensorship()
        {
            if (NextConcreteCensorshipHandles.IsNullOrLength0())
            {
                return false;
            }

            foreach (var ch in NextConcreteCensorshipHandles)
            {
                if (ch.FlowCensorship == null || ch.FlowCensorship.OwnerCensorshipType != CensorshipTypeEnum.STANDARD)
                {
                    continue;
                }

                if (StandardCensorshipDefine.END.Equals(ch.Code))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断下一个关卡是否申请者关卡
        /// </summary>
        /// <returns>下一个关卡是否申请者关卡</returns>
        public bool IsNextApplicantCensorship()
        {
            if (NextConcreteCensorshipHandles.IsNullOrLength0())
            {
                return false;
            }

            foreach (var ch in NextConcreteCensorshipHandles)
            {
                if (ch.FlowCensorship == null || ch.FlowCensorship.OwnerCensorshipType != CensorshipTypeEnum.STANDARD)
                {
                    continue;
                }

                if (StandardCensorshipDefine.APPLICANT.Equals(ch.Code))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
