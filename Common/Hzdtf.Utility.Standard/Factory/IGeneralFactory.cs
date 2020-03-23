using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Factory
{
    /// <summary>
    /// 普通工厂接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ProductT">产品类型</typeparam>
    public interface IGeneralFactory<ProductT>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns>产品</returns>
        ProductT Create();
    }
}
