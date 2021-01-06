using Hzdtf.Authorization.Contract.Standard.IdentityAuth;
using Hzdtf.Authorization.Contract.Standard.User;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Authorization.Web.Framework
{
    /// <summary>
    /// 身份Http表单授权
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IdentityHttpFormAuth : IdentityAuthBase<int, BasicUserInfo<int>>, IIdentityExit
    {
        #region 初始化

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userVali">用户验证</param>
        public IdentityHttpFormAuth(IUserVali<int, BasicUserInfo<int>> userVali)
            : base(userVali)
        {
        }

        #endregion

        #region IIdentityExit 接口

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> Exit()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            returnInfo.Data = HttpFormsAuthorizationUtil.RemoveAuthenticate();
            if (returnInfo.Data)
            {
                returnInfo.SetSuccessMsg("退出成功");
            }
            else
            {
                returnInfo.SetFailureMsg("退出失败");
            }

            return returnInfo;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="basicUser">基本用户</param>
        protected override void SaveUserInfo(BasicUserInfo<int> basicUser) => HttpFormsAuthorizationUtil.SetAuthenticationCookie<BasicUserInfo<int>>(basicUser);

        #endregion
    }
}
