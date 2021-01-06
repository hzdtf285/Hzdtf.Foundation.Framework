﻿using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard.User
{
    /// <summary>
    /// 身份认证用户数据基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public abstract class AuthUserDataBase<IdT, UserT> : IAuthUserData<IdT, UserT>
        where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns>用户</returns>
        public abstract UserT CreateUser();

        /// <summary>
        /// 设置额外的用户数据
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="claims">证件单元集合</param>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public virtual void SetExtraToUserData(UserT user, IEnumerable<Claim> claims) { }

        /// <summary>
        /// 设置额外的证件单元集合
        /// </summary>
        /// <param name="claims">用户</param>
        /// <param name="user">证件单元集合</param>
        [ProcTrackLog(ExecProc = false, IgnoreParamReturn = true, IgnoreParamValues = true)]
        public virtual void SetExtraToClaimsData(IList<Claim> claims, UserT user) { }
    }
}
