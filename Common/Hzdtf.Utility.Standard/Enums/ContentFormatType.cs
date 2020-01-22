using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Enums
{
    /// <summary>
    /// 内容格式类型
    /// @ 黄振东
    /// </summary>
    public enum ContentFormatType : byte
    {
        /// <summary>
        /// 文本
        /// </summary>
        TEXT = 1,

        /// <summary>
        /// 图片
        /// </summary>
        PICTURE = 2,

        /// <summary>
        /// 视频
        /// </summary>
        VIDEO = 3,

        /// <summary>
        /// 音频
        /// </summary>
        AUDIO = 4,

        /// <summary>
        /// 其它文件
        /// </summary>
        OTHER_FILE = 5,

        /// <summary>
        /// 其它
        /// </summary>
        OTHER = 255
    }
}
