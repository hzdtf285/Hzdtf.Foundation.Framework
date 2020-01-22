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
        /// 转换
        /// </summary>
        private readonly IConvertable convert;

        /// <summary>
        /// 转换类型
        /// </summary>
        public IConvertable Convert
        {
            get => convert;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="convertType">转换类型</param>
        public DisplayValueConvertAttribute(Type convertType)
        {
            object instance = convertType.Assembly.CreateInstance(convertType.FullName);
            if (instance is IConvertable)
            {
                convert = instance as IConvertable;
                return;
            }

            throw new NotImplementedException("类型未实现IConvertable接口");
        }
    }
}
