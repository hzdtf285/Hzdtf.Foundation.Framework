using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Diversion;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr.ParamAttr;

namespace Hzdtf.BasicFunction.WorkFlow.Standard
{
    /// <summary>
    /// 查找处理者角色用户
    /// @黄振东
    /// </summary>
    [Inject]
    public partial class FindHandlerRoleUser : IFindHandlerRoleUser
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
        /// 角色用户服务
        /// </summary>
        public IUserRoleService UserRoleService
        {
            get;
            set;
        }

        /// <summary>
        /// 根据ID查找用户信息数组
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<FindHandlerUserOutInfo> FindById([DisplayName2("ID"), Id] int id, int userId, string connectionId = null)
        {
            ReturnInfo<FindHandlerUserOutInfo> returnInfo = new ReturnInfo<FindHandlerUserOutInfo>();

            ReturnInfo<RoleInfo> reRole = RoleService.Find(id, connectionId);
            if (reRole.Failure())
            {
                returnInfo.FromBasic(reRole);
                return returnInfo;
            }
            if (reRole.Data == null || reRole.Data.SystemHide)
            {
                return returnInfo;
            }

            ReturnInfo<IList<UserInfo>> reUser = UserRoleService.OwnUsersByRoleId(id, connectionId);
            if (reUser.Failure())
            {
                returnInfo.FromBasic(reRole);
                return returnInfo;
            }

            returnInfo.Data = new FindHandlerUserOutInfo()
            {
                ConcreteCensorship = reRole.Data,
                Users = reUser.Data.ToArray()
            };

            return returnInfo;
        }
    }
}
