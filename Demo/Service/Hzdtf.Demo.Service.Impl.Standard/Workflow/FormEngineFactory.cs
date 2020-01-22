using Hzdtf.Demo.Model.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Demo.Service.Impl.Standard.Workflow
{
    /// <summary>
    /// 表单引擎工厂
    /// 黄振东
    /// </summary>
    [Inject]
    public class FormEngineFactory : IFormEngineFactory
    {
        /// <summary>
        /// 测试表单引擎
        /// </summary>
        public TestFormEngine TestFormEngine
        {
            get;
            set;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>产品</returns>
        public IFormEngine Create(string type)
        {
            switch (type)
            {
                case WorkflowDefine.TEST_FORM:

                    return TestFormEngine;

                default:

                    return null;
            }
        }
    }
}
