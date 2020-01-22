using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hzdtf.BasicFunction.Model.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件流信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class AttachmentStreamInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("fileName")]
        [MessagePack.Key("fileName")]
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// 流
        /// </summary>
        [JsonProperty("stream")]
        [MessagePack.Key("stream")]
        public Stream Stream
        {
            get;
            set;
        }
    }
}
