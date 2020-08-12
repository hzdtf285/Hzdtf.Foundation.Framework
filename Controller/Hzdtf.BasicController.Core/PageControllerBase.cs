using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Logger.Contract.Standard;
using Hzdtf.Utility.Standard.Model.Page;
using System.Threading.Tasks;

namespace Hzdtf.BasicController.Core
{
    /// <summary>
    /// 页面控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="PageInfoT">页面信息类型</typeparam>
    public abstract class PageControllerBase<PageInfoT> : ControllerBase
        where PageInfoT : PageInfo
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get;
            set;
        }

        /// <summary>
        /// 日志
        /// </summary>
        public ILogable Log
        {
            get;
            set;
        } = new ConsoleLog();

        /// <summary>
        /// 分页解析筛选
        /// </summary>
        public IPagingParseFilter PagingParseFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 分页返回转换
        /// </summary>
        public IPagingReturnConvert PagingReturnConvert
        {
            get;
            set;
        }

        /// <summary>
        /// 获取页面数据，包含当前用户所拥有的权限功能列表
        /// </summary>
        /// <returns>返回信息</returns>
        [HttpGet("PageData")]
        public virtual async Task<ReturnInfo<PageInfoT>> PageData()
        {
            return await Task<ReturnInfo<PageInfoT>>.Run(() =>
            {
                ReturnInfo<PageInfoT> returnInfo = new ReturnInfo<PageInfoT>();
                returnInfo.Data = CreatePageInfo();
                if (string.IsNullOrWhiteSpace(MenuCode()))
                {
                    AppendPageData(returnInfo);
                    return returnInfo;
                }

                ReturnInfo<IList<FunctionInfo>> reFunInfo = UserService.QueryCurrUserOwnFunctionsByMenuCode(MenuCode());
                if (reFunInfo.Success())
                {
                    if (reFunInfo.Data.IsNullOrCount0())
                    {
                        return returnInfo;
                    }

                    returnInfo.Data.Functions = new List<CodeNameInfo>(reFunInfo.Data.Count);
                    foreach (var f in reFunInfo.Data)
                    {
                        returnInfo.Data.Functions.Add(new CodeNameInfo()
                        {
                            Code = f.Code,
                            Name = f.Name
                        });
                    }

                    AppendPageData(returnInfo);
                }
                else
                {
                    returnInfo.FromBasic(reFunInfo);
                }

                return returnInfo;
            });
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        /// <returns>菜单编码</returns>
        protected virtual string MenuCode() => null;

        /// <summary>
        /// 创建页面信息
        /// </summary>
        /// <returns>页面信息</returns>
        private PageInfoT CreatePageInfo()
        {
            if (typeof(PageInfoT) == typeof(PageInfo))
            {
                return (PageInfoT)new PageInfo();
            }

            return typeof(PageInfoT).CreateInstance<PageInfoT>();
        }

        /// <summary>
        /// 追加页面数据
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        protected virtual void AppendPageData(ReturnInfo<PageInfoT> returnInfo) { }
    }
}
