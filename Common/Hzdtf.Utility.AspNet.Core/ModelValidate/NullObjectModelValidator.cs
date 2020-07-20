using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.ModelValidate
{
    /// <summary>
    /// 对象可为null的模型验证
    /// @ 黄振东
    /// </summary>
    public class NullObjectModelValidator : IObjectModelValidator
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="actionContext">动作上下文</param>
        /// <param name="validationState">验证状态</param>
        /// <param name="prefix">前辍</param>
        /// <param name="model">模型</param>
        public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
        {
        }
    }
}
