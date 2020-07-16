using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard.IdentityAuth.Token
{
    /// <summary>
    /// 身份令牌授权接口
    /// @ 黄振东
    /// </summary>
    public interface IIdentityTokenAuth
    {
        /// <summary>
        /// 授权并生成令牌
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <returns>返回信息</returns>
        ReturnInfo<string> AccreditToToken(string user, string password);
    }
}
