using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 带有人时间信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class PersonTimeInfo : TimeInfo
    {
        /// <summary>
        /// 创建人ID_名称
        /// </summary>
        public const string CreaterId_Name = "CreaterId";

        /// <summary>
        /// 创建人ID
        /// </summary>
        [JsonProperty("createrId")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("createrId")]
        public int CreaterId
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人_名称
        /// </summary>
        public const string Creater_Name = "Creater";

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty("creater")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("creater")]
        public string Creater
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人ID_名称
        /// </summary>
        public const string ModifierId_Name = "ModifierId";

        /// <summary>
        /// 修改人ID
        /// </summary>
        [JsonProperty("modifierId")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("modifierId")]
        public int ModifierId
        {
            get;
            set;
        }

        /// <summary>
        /// 修改人_名称
        /// </summary>
        public const string Modifier_Name = "Modifier";

        /// <summary>
        /// 修改人
        /// </summary>
        [JsonProperty("modifier")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("modifier")]
        public string Modifier
        {
            get;
            set;
        }
    }    
}
