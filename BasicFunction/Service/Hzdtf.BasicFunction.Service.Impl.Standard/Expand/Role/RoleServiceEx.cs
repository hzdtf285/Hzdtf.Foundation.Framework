using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Model;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 角色服务
    /// @ 黄振东
    /// </summary>
    public partial class RoleService
    {
        #region IRoleService 接口

        /// <summary>
        /// 查询角色列表并去掉系统隐藏
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<RoleInfo>> QueryAndNotSystemHide(string connectionId = null)
        {
            return ExecReturnFunc<IList<RoleInfo>>((reInfo) =>
            {
                return Persistence.SelectAndNotSystemHide(connectionId);
            });
        }

        /// <summary>
        /// 根据筛选条件查询角色列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<RoleInfo>> QueryByFilter(KeywordFilterInfo filter, string connectionId = null)
        {
            return ExecReturnFunc<IList<RoleInfo>>((reInfo) =>
            {
                return Persistence.SelectByFilter(filter, connectionId);
            });
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 添加模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected override void BeforeAdd(ReturnInfo<bool> returnInfo, RoleInfo model, ref string connectionId)
        {
            bool idClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                idClose = true;
                connectionId = Persistence.NewConnectionId();
            }
            try
            {
                ValiExistsParam(returnInfo, model, Persistence.SelelctByCodeOrName(model.Code, model.Name, connectionId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (idClose && returnInfo.Failure())
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID修改模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected override void BeforeModifyById(ReturnInfo<bool> returnInfo, RoleInfo model, ref string connectionId)
        {
            bool idClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                idClose = true;
                connectionId = Persistence.NewConnectionId();
            }
            try
            {
                ValiExistsParam(returnInfo, model, Persistence.SelelctByCodeOrNameNotId(model.Code, model.Name, model.Id, connectionId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (idClose && returnInfo.Failure())
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 添加模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>        
        /// <param name="connectionId">连接ID</param>
        protected override void BeforeAdd(ReturnInfo<bool> returnInfo, IList<RoleInfo> models, ref string connectionId)
        {
            for (var i = 0; i < models.Count; i++)
            {
                BeforeAdd(returnInfo, models[i], ref connectionId);
                if (returnInfo.Failure())
                {
                    returnInfo.SetFailureMsg($"第{i + 1}行:{returnInfo.Msg}");
                    return;
                }
            }
        }

        /// <summary>
        /// 根据ID移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected override void BeforeRemoveById(ReturnInfo<bool> returnInfo, int id, ref string connectionId)
        {
            ValiCanRemove(returnInfo, Persistence.Select(id, connectionId));
        }

        /// <summary>
        /// 根据ID集合移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected override void BeforeRemoveByIds(ReturnInfo<bool> returnInfo, int[] ids, ref string connectionId)
        {
            IList<RoleInfo> roles = Persistence.Select(ids, connectionId);
            if (roles.IsNullOrCount0())
            {
                return;
            }

            foreach (RoleInfo role in roles)
            {
                ValiCanRemove(returnInfo, role);
                if (returnInfo.Failure())
                {
                    return;
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 验证存在的参数
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="existsRoles">存在的角色列表</param>
        private void ValiExistsParam(ReturnInfo<bool> returnInfo, RoleInfo model, IList<RoleInfo> existsRoles)
        {
            if (existsRoles.IsNullOrCount0())
            {
                return;
            }

            foreach (var r in existsRoles)
            {
                if (string.Compare(model.Code, r.Code, true) == 0)
                {
                    returnInfo.SetFailureMsg($"编码:{model.Code}已存在");
                    return;
                }
                if (string.Compare(model.Name, r.Name, true) == 0)
                {
                    returnInfo.SetFailureMsg($"名称:{model.Name}已存在");
                    return;
                }
            }
        }

        /// <summary>
        /// 验证是否能移除
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="role">角色</param>
        private void ValiCanRemove(ReturnInfo<bool> returnInfo, RoleInfo role)
        {
            if (role == null)
            {
                return;
            }

            if (role.SystemInlay)
            {
                returnInfo.SetFailureMsg($"角色[{role.Name}]是系统内置不能删除");
                return;
            }

            if (role.SystemHide)
            {
                returnInfo.SetFailureMsg($"角色[{role.Name}]是系统隐藏不能删除");
                return;
            }
        }

        #endregion
    }
}
