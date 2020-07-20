using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.Attachment
{
    /// <summary>
    /// 简单附件信息
    /// @ 黄振东
    /// </summary>
    public class SimpleAttachmentInfo
    {        
        /// <summary>
        /// 归属ID
        /// </summary>
        [JsonProperty("ownerId")]
        [DisplayName("归属ID")]
        [Display(Name = "归属ID", Order = 0, AutoGenerateField = true)]
        [MessagePack.Key("ownerId")]
        public int OwnerId
        {
            get;
            set;
        }

        /// <summary>
        /// 归属类型
        /// </summary>
        [JsonProperty("ownerType")]
        [Required]
        [DisplayName("归属类型")]
        [Display(Name = "归属类型", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("ownerType")]
        public short OwnerType
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        [MaxLength(50)]
        [DisplayName("标题")]
        [Display(Name = "标题", Order = 2, AutoGenerateField = true)]
        [MessagePack.Key("title")]
        public string Title
        {
            get;
            set;
        }
        
        /// <summary>
         /// 备注
         /// </summary>
        [JsonProperty("memo")]
        [MaxLength(500)]
        [DisplayName("备注")]
        [Display(Name = "备注", Order = 9, AutoGenerateField = true)]
        [MessagePack.Key("memo")]
        public string Memo
        {
            get;
            set;
        }
    }
}
