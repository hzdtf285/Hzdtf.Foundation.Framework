﻿using Hzdtf.BasicController.Core;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.Menu;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.MvcController.Core.PermissionSet
{
    /// <summary>
    /// 角色权限控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [Menu("RolePermission")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RolePermissionController : PagingControllerBase<PageInfo<int>, RoleInfo, IRoleService, KeywordFilterInfo>
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
        [HttpGet("MenuTrees")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual IList<MenuTreeInfo> MenuTrees() => MenuService.QueryMenuTrees().Data;

        /// <summary>
        /// 获取角色拥有的功能菜单信息列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("HaveMenuFunctions")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual ReturnInfo<IList<MenuFunctionInfo>> HaveMenuFunctions(int roleId) => RoleMenuFunctionService.QueryMenuFunctionsByRoleId(roleId);

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuFunctionIds">菜单功能ID列表</param>
        /// <returns>返回信息</returns>
        [HttpPut("SavePermission")]
        [Function(FunCodeDefine.SAVE_CODE)]
        public virtual ReturnInfo<bool> SavePermission(int roleId, IList<int> menuFunctionIds) => RoleMenuFunctionService.SaveRoleMenuFunctions(roleId, menuFunctionIds);
        
        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected override string MenuCode() => "RolePermission";
    }
}
