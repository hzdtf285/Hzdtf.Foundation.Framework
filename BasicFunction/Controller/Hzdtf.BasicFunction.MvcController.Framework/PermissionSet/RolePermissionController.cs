using Hzdtf.BasicController.Framework;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.Menu;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace Hzdtf.BasicFunction.MvcController.Core.PermissionSet
{
    /// <summary>
    /// 角色权限控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("RolePermission")]
    [RoutePrefix("api/RolePermission")]
    [System.Web.Mvc.Authorize]
    public class RolePermissionController : PagingControllerBase<PageInfo, RoleInfo, IRoleService, KeywordFilterInfo>
    {
        /// <summary>
        /// 菜单服务
        /// </summary>
        public IMenuService MenuService
        {
            get;
            set;
        }

        /// <summary>
        /// 角色菜单功能服务
        /// </summary>
        public IRoleMenuFunctionService RoleMenuFunctionService
        {
            get;
            set;
        }

        /// <summary>
        /// 获取菜单树列表（包含菜单及所拥有的功能列表）
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet()]
        [Route("MenuTrees")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual IList<MenuTreeInfo> MenuTrees() => MenuService.QueryMenuTrees().Data;

        /// <summary>
        /// 获取角色拥有的功能菜单信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回信息</returns>
        [HttpGet()]
        [Route("HaveMenuFunctions")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual ReturnInfo<IList<MenuFunctionInfo>> HaveMenuFunctions(int roleId) => RoleMenuFunctionService.QueryMenuFunctionsByRoleId(roleId);

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuFunctionIds">菜单功能ID列表</param>
        /// <returns>返回信息</returns>
        [HttpPut()]
        [Route("SavePermission")]
        [Function(FunCodeDefine.SAVE_CODE)]
        public virtual ReturnInfo<bool> SavePermission(int roleId, IList<int> menuFunctionIds) => RoleMenuFunctionService.SaveRoleMenuFunctions(roleId, menuFunctionIds);

        /// <summary>
        /// 获取页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet]
        [Function(FunCodeDefine.QUERY_CODE)]
        [Route("PageData")]
        public virtual ReturnInfo<PageInfo> PageData() => ExecPageData("RolePermission");
        
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="page">页码，从1开始</param>
        /// <param name="rows">每页记录数</param>
        /// <returns>分页返回信息</returns>
        [HttpGet]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual Page1ReturnInfo<RoleInfo> Page(int page, int rows) => ExecPage(page, rows);
    }
}
