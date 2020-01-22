using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali.Model.Prop
{
    /// <summary>
    /// 属性验证基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ValiAttrT">验证特性类型</typeparam>
    public abstract class PropValiBase<ValiAttrT> : IPropVali
        where ValiAttrT : ValidationAttribute
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>返回信息</returns>
        public BasicReturnInfo Vali(object model, PropertyInfo property, object value)
        {
            BasicReturnInfo returnInfo = new BasicReturnInfo();
            ValiAttrT valiAttr = property.GetCustomAttribute<ValiAttrT>();
            if (valiAttr == null)
            {
                return returnInfo;
            }

            DisplayNameAttribute displayName = property.GetCustomAttribute<DisplayNameAttribute>();
            string name = displayName != null ? displayName.DisplayName : property.Name;

            string errMsg= ExecVali(model, value, name, valiAttr);
            if (string.IsNullOrWhiteSpace(errMsg))
            {
                return returnInfo;
            }

            returnInfo.SetFailureMsg(errMsg);

            return returnInfo;
        }

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="value">值</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="valiAttr">验证特性</param>
        /// <returns>基本错误消息</returns>
        protected abstract string ExecVali(object model, object value, string displayName, ValiAttrT valiAttr);
    }
}
