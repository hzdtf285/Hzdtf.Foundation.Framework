using Hzdtf.Authorization.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hzdtf.Authorization.Web.Framework
{
    /// <summary>
    /// 身份Http表单授权
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class IdentityHttpFormAuth : IdentityAuthBase, IIdentityAuthVali, IIdentityExit, IReader<ReturnInfo<BasicUserInfo>>
    {
        #region IIdentityAuthVali 接口

        /// <summary>
        /// 判断是否已授权
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> IsAuthed()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            returnInfo.Data = HttpFormsAuthorizationUtil.IsAuthenticated<BasicUserInfo>();

            return returnInfo;
        }

        #endregion

        #region IIdentityExit 接口

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns>返回信息</returns>
        public ReturnInfo<bool> Exit()
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            returnInfo.Data = HttpFormsAuthorizationUtil.RemoveAuthenticate();
            if (returnInfo.Data)
            {
                returnInfo.SetSuccessMsg("退出成功");
            }
            else
            {
                returnInfo.SetFailureMsg("退出失败");
            }

            return returnInfo;
        }

        #endregion

        #region IReader<IdentityInfoT> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public ReturnInfo<BasicUserInfo> Reader()
        {
            ReturnInfo<BasicUserInfo> returnInfo = new ReturnInfo<BasicUserInfo>();
            returnInfo.Data = HttpFormsAuthorizationUtil.ParseUserData<BasicUserInfo>();

            return returnInfo;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="basicUser">基本用户</param>
        protected override void SaveUserInfo(BasicUserInfo basicUser) => HttpFormsAuthorizationUtil.SetAuthenticationCookie<BasicUserInfo>(basicUser);

        #endregion
    }
}
