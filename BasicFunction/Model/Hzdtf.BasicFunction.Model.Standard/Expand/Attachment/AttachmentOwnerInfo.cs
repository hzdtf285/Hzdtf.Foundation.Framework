using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件归属信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public partial class AttachmentOwnerInfo
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
        /// 允许扩展名
        /// 如果有多个则以,分隔
        /// 不限制则用*
        /// </summary>
        [JsonProperty("allowExpands")]
        [MessagePack.Key("allowExpands")]
        public string AllowExpands
        {
            get;
            set;
        }

        /// <summary>
        /// 最大文件大小（单位：KB）
        /// 如果不限制，则为-1
        [JsonProperty("maxSize")]
        /// </summary>
        [MessagePack.Key("maxSize")]
        public float MaxSize
        {
            get;
            set;
        } = -1;
    }
}
