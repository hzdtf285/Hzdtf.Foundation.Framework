using Hzdtf.Utility.Standard.Conversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Attr
{
    /// <summary>
    /// 显示值转换特性
    /// @ 黄振东
    /// </summary>
    public class DisplayValueConvertAttribute : Attribute
    {
        /// <summary>
        /// 值到文本转换
        /// </summary>
        private readonly IConvertable valueToTextConvert;

        /// <summary>
        /// 值到文本转换
        /// </summary>
        public IConvertable ValueToTextConvert
        {
            get => valueToTextConvert;
        }

        /// <summary>
        /// 文本到值转换
        /// </summary>
        private readonly IConvertable textToValueConvert;

        /// <summary>
        /// 文本到值转换
        /// </summary>
        public IConvertable TextToValueConvert
        {
            get => textToValueConvert;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="valueToTextConvert">值到文本转换类型</param>
        /// <param name="textToValueConvert">文本到值转换类型</param>
        public DisplayValueConvertAttribute(Type valueToTextConvert = null, Type textToValueConvert = null)
        {
            if (valueToTextConvert != null)
            {
                object t1 = valueToTextConvert.Assembly.CreateInstance(valueToTextConvert.FullName);
                if (t1 is IConvertable)
                {
                    this.valueToTextConvert = t1 as IConvertable;
                }
                else
                {
                    throw new NotImplementedException("值到文本转换类型未实现IConvertable接口");
                }
            }

            if (textToValueConvert != null)
            {
                object t2 = textToValueConvert.Assembly.CreateInstance(textToValueConvert.FullName);
                if (t2 is IConvertable)
                {
                    this.textToValueConvert = t2 as IConvertable;
                }
                else
                {
                    throw new NotImplementedException("文本到值转换类型未实现IConvertable接口");
                }
            }
        }
    }
}
