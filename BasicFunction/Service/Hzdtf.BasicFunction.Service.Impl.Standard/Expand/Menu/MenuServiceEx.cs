using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Model.Standard.Expand.Menu;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 菜单服务
    /// @ 黄振东
    /// </summary>
    public partial class MenuService
    {
        /// <summary>
        /// 查询菜单树列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<MenuTreeInfo>> QueryMenuTrees(string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return ExecReturnFunc<IList<MenuTreeInfo>>((reInfo) =>
            {
                IList<MenuInfo> menus = Persistence.SelectContainsFunctions(connectionId);
                return menus.ToOrigAndSort().ToMenuTrees();
            });
        }
    }
}
