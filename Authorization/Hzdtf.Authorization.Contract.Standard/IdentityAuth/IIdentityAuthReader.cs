using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard.IdentityAuth
{
    /// <summary>
    /// 身份认证读取接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="UserT">用户类型</typeparam>
    public interface IIdentityAuthReader<IdT, UserT> : IReader<ReturnInfo<UserT>>
        where UserT : BasicUserInfo<IdT>
    {
        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsAuthed();
    }
}
