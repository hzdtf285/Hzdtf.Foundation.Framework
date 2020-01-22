using Hzdtf.Utility.Standard.Attr;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// 消息包转换类型值
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class MessagePackConvertTypeValue : ConvertTypeValueBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value, Type targetType) => MessagePackSerializer.NonGeneric.Deserialize(targetType, MessagePackSerializer.Serialize(value));
    }
}
