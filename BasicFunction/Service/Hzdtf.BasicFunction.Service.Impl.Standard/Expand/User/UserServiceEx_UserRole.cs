using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Model.Page;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 用户服务
    /// @ 黄振东
    /// </summary>
    public partial class UserService
    {
        #region 属性与字段

        /// <summary>
        /// 用户角色持久化
        /// </summary>
        public IUserRolePersistence UserRolePersistence
        {
            get;
            set;
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> Add([Model] UserInfo model, string connectionId = null, BasicUserInfo currUser = null)
        {
            if (model is PersonTimeInfo)
            {
                SetCreateInfo(model, currUser);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            BeforeAdd(returnInfo, model, ref connectionId);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            ExecAdd(returnInfo, model, connectionId, currUser);

            AfterAdd(returnInfo, model, ref connectionId);

            if (isClose)
            {
                Persistence.Release(connectionId);
            }

            return returnInfo;
        }

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public override ReturnInfo<bool> ModifyById([Model] UserInfo model, string connectionId = null, BasicUserInfo currUser = null)
        {
            SetModifyInfo(model, currUser);

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            BeforeModifyById(returnInfo, model, ref connectionId);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            ExecModifyById(returnInfo, model, connectionId, currUser);

            AfterModifyById(returnInfo, model, ref connectionId);

            if (isClose)
            {
                Persistence.Release(connectionId);
            }

            return returnInfo;
        }
        
         /// <summary>
         /// 执行查询模型列表并分页后
         /// </summary>
         /// <param name="returnInfo">返回信息</param>
         /// <param name="pageIndex">页码</param>
         /// <param name="pageSize">每页记录数</param>
         /// <param name="connectionId">连接ID</param>
         /// <param name="filter">筛选</param>
         /// <param name="currUser">当前用户</param>
        protected override void AfterQueryPage(ReturnInfo<PagingInfo<UserInfo>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null, BasicUserInfo currUser = null)
        {
            // 查找每个用户所属的角色
            if (returnInfo.Data.Rows.IsNullOrCount0())
            {
                return;
            }

            int[] userIds = new int[returnInfo.Data.Rows.Count];
            for (var i = 0; i < userIds.Length; i++)
            {
                userIds[i] = returnInfo.Data.Rows[i].Id;
            }
            IList<UserRoleInfo> userRoles = UserRolePersistence.SelectContainsRoleByUserIds(userIds, connectionId);
            if (userRoles.IsNullOrCount0())
            {
                return;
            }

            foreach (UserRoleInfo ur in userRoles)
            {
                foreach (UserInfo u in returnInfo.Data.Rows)
                {
                    if (u.Id == ur.UserId)
                    {
                        if (u.OwnRoles == null)
                        {
                            u.OwnRoles = new List<RoleInfo>();
                        }
                        u.OwnRoles.Add(ur.Role);
                    }
                }
            }
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 执行添加
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        [Transaction(ConnectionIdIndex = 2)]
        protected virtual void ExecAdd(ReturnInfo<bool> returnInfo, UserInfo model, string connectionId = null, BasicUserInfo currUser = null)
        {
            returnInfo.Data = Persistence.Insert(model, connectionId) > 0;
            if (returnInfo.Data)
            {
                AddUserRoles(model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 执行根据ID修改
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        [Transaction(ConnectionIdIndex = 2)]
        protected virtual void ExecModifyById(ReturnInfo<bool> returnInfo, UserInfo model, string connectionId = null, BasicUserInfo currUser = null)
        {
            returnInfo.Data = Persistence.UpdateById(model, connectionId) > 0;
            if (returnInfo.Data)
            {
                UserRolePersistence.DeleteByUserId(model.Id, connectionId);
                AddUserRoles(model, connectionId, currUser);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>影响行数</returns>
        private int AddUserRoles(UserInfo model, string connectionId = null, BasicUserInfo currUser = null)
        {
            if (model.OwnRoles.IsNullOrCount0())
            {
                return 0;
            }

            IList<UserRoleInfo> userRoles = new List<UserRoleInfo>(model.OwnRoles.Count);
            foreach (RoleInfo r in model.OwnRoles)
            {
                UserRoleInfo ur = new UserRoleInfo()
                {
                    UserId = model.Id,
                    RoleId = r.Id
                };
                ur.SetCreateInfo(currUser);

                userRoles.Add(ur);
            }
            if (userRoles.IsNullOrCount0())
            {
                return 0;
            }

            return UserRolePersistence.Insert(userRoles, connectionId);
        }

        #endregion
    }
}
