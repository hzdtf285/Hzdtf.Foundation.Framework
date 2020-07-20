using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件筛选信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class AttachmentFilterInfo : FilterInfo
    {
        /// <summary>
        /// 归属类型
        /// </summary>
        [JsonProperty("ownerType")]
        [MessagePack.Key("ownerType")]
        public short OwnerType
        {
            get;
            set;
        }

        /// <summary>
        /// 归属ID
        /// </summary>
        [JsonProperty("ownerId")]
        [MessagePack.Key("ownerId")]
        public int? OwnerId
        {
            get;
            set;
        }

        /// <summary>
        /// 模糊标题
        /// </summary>
        [JsonProperty("blurTitle")]
        [MessagePack.Key("blurTitle")]
        public string BlurTitle
        {
            get;
            set;
        }
    }
}
