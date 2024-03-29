﻿using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.WorkFlow.Model.Standard.Expand.Diversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form
{
    /// <summary>
    /// 基本表单引擎基类
    /// @ 黄振东
    /// </summary>
    public abstract class BasicFormEngineBase : IFormEngine
    {
        /// <summary>
        /// 执行流程前
        /// </summary>
        /// <param name="flowCensorshipOut">流程关卡输出</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> BeforeExecFlow(FlowCensorshipOutInfo flowCensorshipOut, object flowIn, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return new ReturnInfo<bool>();
        }

        /// <summary>
        /// 执行流程后
        /// </summary>
        /// <param name="flowCensorshipOut">流程关卡输出</param>
        /// <param name="flowIn">流程输入</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> AfterExecFlow(FlowCensorshipOutInfo flowCensorshipOut, object flowIn, bool isSuccess, string connectionId = null, BasicUserInfo<int> currUser = null)
        {
            return new ReturnInfo<bool>();
        }
    }
}
