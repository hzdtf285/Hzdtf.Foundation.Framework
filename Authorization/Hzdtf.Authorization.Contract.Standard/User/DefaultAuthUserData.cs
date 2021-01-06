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
    /// <typeparam name="IdT">ID类型</typeparam>
    [Inject]
    public class DefaultAuthUserData<IdT> : AuthUserDataBase<IdT, BasicUserInfo<IdT>>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns>用户</returns>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public override BasicUserInfo<IdT> CreateUser() => new BasicUserInfo<IdT>();
    }

    /// <summary>
    /// 默认身份认证用户数据
    /// @ 黄振东
    /// </summary>
    [Inject]
    public sealed class DefaultAuthUserData : DefaultAuthUserData<int>
    {
    }
}
