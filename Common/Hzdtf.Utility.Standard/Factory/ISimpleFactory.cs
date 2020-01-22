using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Factory
{
    /// <summary>
    /// 简单工厂接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="TypeT">类型</typeparam>
    /// <typeparam name="ProductT">产品类型</typeparam>
    public interface ISimpleFactory<TypeT, ProductT>
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        ProductT Create(TypeT type);
    }
}
