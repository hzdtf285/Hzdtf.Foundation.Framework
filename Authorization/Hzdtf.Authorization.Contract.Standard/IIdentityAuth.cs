using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;

namespace Hzdtf.Authorization.Contract.Standard
{
    /// <summary>
    /// 身份授权接口
    /// @ 黄振东
    /// </summary>
    public interface IIdentityAuth
    {
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <returns>返回信息</returns>
        ReturnInfo<BasicUserInfo> Accredit(string user, string password);
    }
}
