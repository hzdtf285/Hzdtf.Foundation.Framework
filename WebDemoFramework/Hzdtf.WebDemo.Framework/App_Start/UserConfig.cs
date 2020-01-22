using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Authorization.Web.Framework;
using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Autofac.Extend.Standard;

namespace Hzdtf.WebDemo.Framework
{
    /// <summary>
    /// 用户配置
    /// @ 黄振东
    /// </summary>
    public static class UserConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            UserTool.GetCurrUserFunc = () =>
            {
                ReturnInfo<BasicUserInfo> returnInfo = AutofacTool.Resolve<IdentityHttpFormAuth>().Reader();
                if (returnInfo.Success() && returnInfo.Data != null)
                {
                    return returnInfo.Data;
                }

                if (ConfigurationManager.AppSettings["User:AllowTest"] != null && Convert.ToBoolean(ConfigurationManager.AppSettings["User:AllowTest"]))
                {
                    return UserTool.TestUser;
                }

                return null;
            };
        }
    }
}