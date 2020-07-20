using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand
{
    /// <summary>
    /// 流程初始信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="FormT">表单类型</typeparam>
    [MessagePackObject]
    public class FlowInitInfo<FormT>
        where FormT : PersonTimeInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [DisplayName("ID")]
        [Display(Name = "ID")]
        [MessagePack.Key("id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流编码
        /// </summary>
        [JsonProperty("workflowCode")]
        [DisplayName("工作流编码")]
        [Display(Name = "工作流编码")]
        [MaxLength(2)]
        [MessagePack.Key("workflowCode")]
        public string WorkflowCode
        {
            get;
            set;
        }

        /// <summary>
        /// 申请单号
        /// </summary>
        [JsonProperty("applyNo")]
        [DisplayName("申请单号")]
        [Display(Name = "申请单号")]
        [MaxLength(20)]
        [MessagePack.Key("applyNo")]
        public string ApplyNo
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        [DisplayName("标题")]
        [Display(Name = "标题")]
        [MaxLength(50)]
        [MessagePack.Key("title")]
        public string Title
        {
            get;
            set;
        }
        
        /// <summary>
        /// 意见
        /// </summary>
        [JsonProperty("idea")]
        [MaxLength(200)]
        [DisplayName("意见")]
        [Display(Name = "意见")]
        [MessagePack.Key("idea")]
        public string Idea
        {
            get;
            set;
        }

        /// <summary>
        /// 表单数据
        /// </summary>
        [JsonProperty("formData")]
        [MessagePack.Key("formData")]
        public FormT FormData
        {
            get;
            set;
        }

        /// <summary>
        /// 动作类型
        /// </summary>
        [JsonProperty("actionType")]
        [MessagePack.Key("actionType")]
        public ActionType ActionType
        {
            get;
            set;
        } = ActionType.SEND;

        /// <summary>
        /// 转换为流程输入
        /// </summary>
        /// <returns>流程输入</returns>
        public FlowInInfo<FlowInitInfo<PersonTimeInfo>> ToFlowIn()
        {
            return new FlowInInfo<FlowInitInfo<PersonTimeInfo>>()
            {
                Flow = new FlowInitInfo<PersonTimeInfo>()
                {
                    Id = this.Id,
                    WorkflowCode = this.WorkflowCode,
                    Title = this.Title,
                    ApplyNo = this.ApplyNo,
                    Idea = this.Idea,
                    ActionType = this.ActionType
                },
                Form = FormData
            };
        }
    }
}
