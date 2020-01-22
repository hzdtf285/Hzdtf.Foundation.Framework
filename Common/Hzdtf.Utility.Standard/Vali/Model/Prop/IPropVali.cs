using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali.Model.Prop
{
    /// <summary>
    /// 属性验证接口
    /// @ 黄振东
    /// </summary>
    public interface IPropVali
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>返回信息</returns>
        BasicReturnInfo Vali(object model, PropertyInfo property, object value);
    }
}
