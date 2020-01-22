using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Attr;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicController.Core
{
    /// <summary>
    /// 数据库控制器
    /// @ 黄振东
    /// </summary>
    [Inject]
    [ApiController]
    [Route("api/[controller]")]
    public class DbController : ControllerBase
    {
        /// <summary>
        /// 获取当前连接数
        /// </summary>
        /// <returns>当前连接数</returns>
        [HttpGet("CurrConnCount")]
        public int CurrConnCount() => DbConnectionManager.CurrDbConnCount;
    }
}
