using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.User;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace Hzdtf.BasicFunction.MvcController.Framework
{
    /// <summary>
    /// 用户控制器
    /// @ 黄振东
    /// </summary>
    public partial class UserController
    {
        /// <summary>
        /// 角色服务
        /// </summary>
        public IRoleService RoleService
        {
            get;
            set;
        }

        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="currUserModifyPassword">当前用户修改密码</param>
        /// <returns>返回信息</returns>
        [HttpPut()]
        [Route("ModifyCurrUserPassword")]
        public virtual ReturnInfo<bool> ModifyCurrUserPassword(CurrUserModifyPasswordInfo currUserModifyPassword)
        {
            currUserModifyPassword.LoginId = UserTool.CurrUser.LoginId;
            return Service.ModifyPasswordByLoginId(currUserModifyPassword);
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="modifyPassword">修改密码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [HttpPut()]
        [Route("ResetUserPassword")]
        [Function(FunCodeDefine.RESET_PASSWORD_CODE)]
        public virtual ReturnInfo<bool> ResetUserPassword(ModifyPasswordInfo modifyPassword, string connectionId = null) => Service.ResetUserPassword(modifyPassword);
        
        /// <summary>
        /// 追加页面数据
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected override void AppendPageData(ReturnInfo<UserPageInfo> returnInfo)
        {
            ReturnInfo<IList<RoleInfo>> roleReturnInfo = RoleService.QueryAndNotSystemHide();
            if (roleReturnInfo.Success())
            {
                returnInfo.Data.Roles = roleReturnInfo.Data;
            }
            else
            {
                returnInfo.FromBasic(roleReturnInfo);
            }
        }
    }
}
