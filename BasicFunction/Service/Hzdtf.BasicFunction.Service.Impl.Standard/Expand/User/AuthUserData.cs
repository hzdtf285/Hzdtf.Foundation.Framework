using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Authorization.Contract.Standard.User;

namespace Hzdtf.BasicFunction.Service.Impl.Standard.Expand.User
{
    /// <summary>
    /// 身份认证用户数据
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class AuthUserData : AuthUserDataBase<int, UserInfo>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns>用户</returns>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public override UserInfo CreateUser() => new UserInfo();

        /// <summary>
        /// 设置额外的用户数据
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="claims">证件单元集合</param>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public override void SetExtraToUserData(UserInfo user, IEnumerable<Claim> claims) 
        {
            user.Mail = claims.Get(ClaimTypes.Email);
            user.Mobile = claims.Get(ClaimTypes.MobilePhone);

            var sexStr = claims.Get("sex");
            if (string.IsNullOrWhiteSpace(sexStr))
            {
                return;   
            }

            bool sex;
            if (bool.TryParse(sexStr, out sex))
            {
                user.Sex = sex;
            }
        }

        /// <summary>
        /// 设置额外的证件单元集合
        /// </summary>
        /// <param name="claims">用户</param>
        /// <param name="user">证件单元集合</param>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public override void SetExtraToClaimsData(IList<Claim> claims, UserInfo user)
        {
            claims.Add(ClaimTypes.Email, user.Mail);
            claims.Add(ClaimTypes.Email, user.Mail);
            claims.Add(ClaimTypes.MobilePhone, user.Mobile);
            claims.Add("sex", user.Sex.ToString());
        }
    }
}
