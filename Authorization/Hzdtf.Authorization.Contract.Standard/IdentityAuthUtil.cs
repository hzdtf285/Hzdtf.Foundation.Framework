using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Authorization.Contract.Standard
{
    /// <summary>
    /// 身份授权辅助类
    /// @ 黄振东
    /// </summary>
    public static class IdentityAuthUtil
    {
        /// <summary>
        /// 验证参数
        /// </summary>
        /// <typeparam name="IdentityInfoT">身份模型类型</typeparam>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <param name="userAlias">用户别名</param>
        /// <returns>是否验证通过</returns>
        public static bool ValiParams<IdentityInfoT>(ReturnInfo<IdentityInfoT> returnInfo, string user, string password, string userAlias = null)
        where IdentityInfoT : BasicUserInfo
        {            
            if (string.IsNullOrWhiteSpace(user))
            {
                if (string.IsNullOrWhiteSpace(userAlias))
                {
                    userAlias = "用户";
                }
                returnInfo.SetFailureMsg($"请输入{userAlias}");

                return false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                returnInfo.SetFailureMsg("请输入密码");
                return false;
            }

            return true;
        }
    }
}
