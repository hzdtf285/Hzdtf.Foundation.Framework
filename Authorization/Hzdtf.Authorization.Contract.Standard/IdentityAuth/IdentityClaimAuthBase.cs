using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Model.Return;
using System.Security.Claims;
using Hzdtf.Utility.Standard.Model;
using System.Collections.Generic;
using System;
using Hzdtf.Authorization.Contract.Standard.User;

namespace Hzdtf.Authorization.Contract.Standard.IdentityAuth
{
    /// <summary>
    /// 身份证件授权
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="UserT">用户类型</typeparam>
    public abstract class IdentityClaimAuthBase<UserT> : IdentityAuthBase<UserT>, IIdentityAuthVali, IReader<ReturnInfo<UserT>>
        where UserT : BasicUserInfo
    {
        #region 属性与字段

        /// <summary>
        /// 用户
        /// </summary>
        public IAuthUserData<UserT> AuthUserData
        {
            get;
            set;
        }

        #endregion

        #region IIdentityAuthVali 接口

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public abstract ReturnInfo<bool> IsAuthed();

        #endregion

        #region IReader<IdentityInfoT> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ReturnInfo<UserT> Reader()
        {
            ReturnInfo<UserT> returnInfo = new ReturnInfo<UserT>();
            ReturnInfo<bool> isAuthReturnInfo = IsAuthed();
            if (isAuthReturnInfo.Success() && isAuthReturnInfo.Data)
            {
                var claims = GetClaims();
                if (claims == null)
                {
                    return returnInfo;
                }

                returnInfo.Data = IdentityAuthUtil.GetUserDataFromClaims<UserT>(claims, AuthUserData);
            }

            return returnInfo;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">用户</param>
        protected override void SaveUserInfo(UserT user)
        {
            var claims = IdentityAuthUtil.SaveUserInfoGetClaims(user, AuthUserData);
            var identity = new ClaimsIdentity(claims, GetAuthenticationScheme());

            SignIn(new ClaimsPrincipal(identity));
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 获取证件单元集合
        /// </summary>
        /// <returns>证件单元集合</returns>
        protected abstract IEnumerable<Claim> GetClaims();

        /// <summary>
        /// 获取身份认证方案
        /// </summary>
        /// <returns>身份认证方案</returns>
        protected abstract string GetAuthenticationScheme();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="principal">当事人</param>
        protected abstract void SignIn(ClaimsPrincipal principal);

        #endregion
    }
}
