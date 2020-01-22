using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard.User;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hzdtf.BasicFunction.MvcController.Framework.Home
{
    /// <summary>
    /// 主页控制器
    /// @ 黄振东
    /// </summary>
    [Authorize]
    [Inject]
    public class HomeController : Controller
    {
        /// <summary>
        /// 用户菜单服务
        /// </summary>
        public IUserMenuService UserMenuService
        {
            get;
            set;
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>动作结果</returns>
        public ActionResult Index()
        {
            return View(UserMenuService.CanAccessMenus(UserTool.CurrUser.Id));
        }
    }
}
