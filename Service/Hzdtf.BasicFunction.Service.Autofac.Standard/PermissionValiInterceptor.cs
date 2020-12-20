using Castle.DynamicProxy;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Autofac.Extend.Standard.Intercepteds;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.BasicFunction.Service.Autofac.Standard
{
    /// <summary>
    /// 权限验证拦截器
    /// @ 黄振东
    /// </summary>
    public class PermissionValiInterceptor : AttrInterceptorBase<FunctionAttribute>
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        /// <param name="invocation">拦截参数</param>
        /// <param name="attr">特性</param>
        /// <param name="isExecProceeded">是否已执行</param>
        protected override void Intercept(BasicReturnInfo basicReturn, IInvocation invocation, FunctionAttribute attr, out bool isExecProceeded)
        {
            isExecProceeded = false;
            var ignorePerAttr = invocation.Method.GetAttribute<IgnorePermissionAttribute>();
            if (ignorePerAttr != null)
            {
                return;
            }
            if (attr.Codes.IsNullOrCount0())
            {
                basicReturn.SetFailureMsg("功能编码不能为空");
                return;
            }
            MenuAttribute menuAttr = invocation.TargetType.GetAttribute<MenuAttribute>();
            if (menuAttr == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(menuAttr.Code))
            {
                basicReturn.SetFailureMsg("菜单编码不能为空");
                return;
            }

            // 这里执行权限验证
            IUserService userService = AutofacTool.Resolve<IUserService>();
            if (userService == null)
            {
                basicReturn.SetFailureMsg("找不到用户服务");
                return;
            }
            ReturnInfo<bool> perReInfo = userService.IsCurrUserPermission(menuAttr.Code, attr.Codes);
            if (perReInfo.Failure())
            {
                basicReturn.FromBasic(perReInfo);
                return;
            }
            if (perReInfo.Data)
            {
                return;
            }
            else
            {
                basicReturn.SetCodeMsg(ErrCodeDefine.NOT_PERMISSION, "Sorry，您没有访问此功能权限");
            }
        }

    }
}
