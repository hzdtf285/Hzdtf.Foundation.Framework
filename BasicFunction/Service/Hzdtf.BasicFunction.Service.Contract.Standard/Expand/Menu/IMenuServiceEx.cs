using Hzdtf.BasicFunction.Model.Standard.Expand.Menu;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard
{
    /// <summary>
    /// 菜单服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface IMenuService
    {
        /// <summary>
        /// 查询菜单树列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<MenuTreeInfo>> QueryMenuTrees(string connectionId = null);
    }
}
