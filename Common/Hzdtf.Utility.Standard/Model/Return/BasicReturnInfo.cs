using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Return
{
    /// <summary>
    /// 基本返回信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BasicReturnInfo
    {
        /// <summary>
        /// 基本全称
        /// </summary>
        public const string BASIC_FULL_NAME = "Hzdtf.Utility.Standard.Model.Return.BasicReturnInfo";

        /// <summary>
        /// 成功编码
        /// </summary>
        public const int SUCCESS_CODE = 0;

        /// <summary>
        /// 默认失败编码
        /// </summary>
        public const int DEFAULT_FAILURE_CODE = -1;

        /// <summary>
        /// 是否转换为JSON
        /// </summary>
        private readonly bool isToJsonString;

        /// <summary>
        /// 构造方法
        /// </summary>
        public BasicReturnInfo() : this(true) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isToJsonString">是否转换为JSON</param>
        public BasicReturnInfo(bool isToJsonString) { this.isToJsonString = isToJsonString; }

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty("code")]
        [Key("code")]
        public int Code
        {
            get;
            set;
        } = SUCCESS_CODE;

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("msg")]
        [Key("msg")]
        public string Msg
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("desc")]
        [Key("desc")]
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// 异常
        /// </summary>
        [JsonIgnore]
        [IgnoreMember]
        [JsonProperty("ex")]
        [Key("ex")]
        public Exception Ex
        {
            get;
            set;
        }       

        /// <summary>
        /// 是否成功
        /// </summary>
        /// <returns>是否成功</returns>
        public bool Success() => Code == SUCCESS_CODE;

        /// <summary>
        /// 是否失败
        /// </summary>
        /// <returns>是否失败</returns>
        public bool Failure() => Code != SUCCESS_CODE;

        /// <summary>
        /// 设置成功消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="desc">描述</param>
        public void SetSuccessMsg(string msg, string desc = null) => SetCodeMsg(SUCCESS_CODE, msg, desc);

        /// <summary>
        /// 设置失败消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="desc">描述</param>
        /// <param name="ex">异常</param>
        public void SetFailureMsg(string msg, string desc = null, Exception ex = null) => SetFailureMsg(DEFAULT_FAILURE_CODE, msg, desc, ex);

        /// <summary>
        /// 设置失败消息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="msg">消息</param>
        /// <param name="desc">描述</param>
        /// <param name="ex">异常</param>
        public void SetFailureMsg(int code, string msg, string desc = null, Exception ex = null) => SetCodeMsg(code, msg, desc, ex);

        /// <summary>
        /// 设置编码和消息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="msg">消息</param>
        /// <param name="desc">描述</param>
        /// <param name="ex">异常</param>
        public void SetCodeMsg(int code, string msg, string desc = null, Exception ex = null)
        {
            Code = code;
            SetMsg(msg, desc);
            Ex = ex;
        }

        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="desc">描述</param>
        public void SetMsg(string msg, string desc = null)
        {
            Msg = msg;
            Desc = desc;
        }

        /// <summary>
        /// 设置来自另外一个基本返回对象
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        public void FromBasic(BasicReturnInfo basicReturn) => SetCodeMsg(basicReturn.Code, basicReturn.Msg, basicReturn.Desc, basicReturn.Ex);

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return isToJsonString ? JsonUtil.SerializeIgnoreNull(this) : base.ToString();
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        public void ThrowException()
        {
            throw new BasicReturnException(this);
        }

        /// <summary>
        /// 是否本身类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否本身类型</returns>
        public static bool IsThisType(Type type)
        {
            return type != null ? BASIC_FULL_NAME.Equals(type.FullName) : false;
        }
    }

    /// <summary>
    /// 基本返回异常
    /// @ 黄振东
    /// </summary>
    public class BasicReturnException : Exception
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code
        {
            get;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc
        {
            get;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        public BasicReturnException(BasicReturnInfo basicReturn)
            : this(basicReturn.Code, basicReturn.Msg, basicReturn.Desc)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="message">消息</param>
        /// <param name="desc">描述</param>
        public BasicReturnException(int code = -1, string message = null, string desc = null)
            : base(message)
        {
            this.Code = code;
            this.Desc = desc;
        }
    }
}