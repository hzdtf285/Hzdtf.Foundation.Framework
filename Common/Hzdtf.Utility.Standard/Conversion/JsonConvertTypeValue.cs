using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Conversion
{
    /// <summary>
    /// JSON转换类型值
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class JsonConvertTypeValue : ConvertTypeValueBase
    {
        /// <summary>
        /// 转换新值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>新值</returns>
        protected override object ToNew(object value, Type targetType) => JsonUtil.Deserialize(JsonUtil.SerializeIgnoreNull(value), targetType);
    }
}
