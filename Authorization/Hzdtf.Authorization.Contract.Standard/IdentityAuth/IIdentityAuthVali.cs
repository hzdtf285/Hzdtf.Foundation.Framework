using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard.IdentityAuth
{
    /// <summary>
    /// 身份授权验证接口
    /// @ 黄振东
    /// </summary>
    public interface IIdentityAuthVali
    {
        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> IsAuthed();
    }
}
