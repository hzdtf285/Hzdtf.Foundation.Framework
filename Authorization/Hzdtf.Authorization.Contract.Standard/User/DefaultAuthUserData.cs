using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard.User
{
    /// <summary>
    /// 默认身份认证用户数据
    /// @ 黄振东
    /// </summary>
    [Inject]
    public sealed class DefaultAuthUserData : AuthUserDataBase<BasicUserInfo>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns>用户</returns>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public override BasicUserInfo CreateUser() => new BasicUserInfo();
    }
}
