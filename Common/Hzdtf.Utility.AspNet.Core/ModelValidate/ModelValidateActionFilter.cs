using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.ModelValidate
{
    /// <summary>
    /// 模型验证动作过滤器
    /// @ 黄振东
    /// </summary>
    public class ModelValidateActionFilter : IActionFilter
    {
        /// <summary>
        /// 动作执行中
        /// </summary>
        /// <param name="context">上下文</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var result = new ReturnInfo<string>();
            var msg = new StringBuilder();
            foreach (var item in context.ModelState.Values)
            {
                foreach (var error in item.Errors)
                {
                    msg.Append(error.ErrorMessage + "|");
                }
            }
            if (msg.Length == 0)
            {
                return;
            }

            result.SetCodeMsg(400, msg.ToString());

            context.HttpContext.Response.StatusCode = 400;
            context.Result = new JsonResult(result);
        }

        /// <summary>
        /// 动作执行完
        /// </summary>
        /// <param name="context">上下文</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
