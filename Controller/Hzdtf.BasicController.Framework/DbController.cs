using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace Hzdtf.BasicController.Framework
{
    /// <summary>
    /// 数据库控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [RoutePrefix("api/Db")]
    public class DbController : ApiController
    {
        /// <summary>
        /// 获取当前连接数
        /// </summary>
        /// <returns>当前连接数</returns>
        [HttpGet]
        [Route("CurrConnCount")]
        public int CurrConnCount() => DbConnectionManager.CurrDbConnCount;
    }
}
