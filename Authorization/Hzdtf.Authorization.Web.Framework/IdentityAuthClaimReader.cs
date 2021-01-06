using Hzdtf.Authorization.Contract.Standard.IdentityAuth;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Web.Framework
{
    /// <summary>
    /// 身份认证证件单元读取
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IdentityAuthClaimReader : IIdentityAuthReader<int, BasicUserInfo<int>>
    {
        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> IsAuthed()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            returnInfo.Data = HttpFormsAuthorizationUtil.IsAuthenticated<BasicUserInfo<int>>();

            return returnInfo;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ReturnInfo<BasicUserInfo<int>> Reader()
        {
            ReturnInfo<BasicUserInfo<int>> returnInfo = new ReturnInfo<BasicUserInfo<int>>();
            returnInfo.Data = HttpFormsAuthorizationUtil.ParseUserData<BasicUserInfo<int>>();

            return returnInfo;
        }
    }
}
