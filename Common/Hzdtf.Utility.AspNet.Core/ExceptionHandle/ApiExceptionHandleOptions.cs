using Hzdtf.Utility.Standard.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.AspNet.Core.ExceptionHandle
{
    /// <summary>
    /// Api异常处理选项配置
    /// @ 黄振东
    /// </summary>
    public class ApiExceptionHandleOptions
    {
        /// <summary>
        /// 是否开发环境
        /// </summary>
        public bool IsDevelopment
        {
            get;
            set;
        }

        /// <summary>
        /// Api路径前辍，默认是/api/
        /// </summary>
        public string PfxApiPath
        {
            get;
            set;
        } = "/api/";

        /// <summary>
        /// 序列化，默认为Json
        /// </summary>
        public ISerialization Serialization
        {
            get;
            set;
        } = new JsonConvert();

        /// <summary>
        /// 异常编码，默认为500
        /// </summary>
        public int ExceptionCode
        {
            get;
            set;
        } = 500;

        /// <summary>
        /// 异常消息，默认为“操作失败”
        /// </summary>
        public string ExceptionMsg
        {
            get;
            set;
        } = "操作失败";

        /// <summary>
        /// Http状态码，默认返回200
        /// </summary>
        public int HttpStatusCode
        {
            get;
            set;
        } = StatusCodes.Status200OK;
    }
}
