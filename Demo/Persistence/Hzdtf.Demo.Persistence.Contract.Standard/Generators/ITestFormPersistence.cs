using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Demo.Model.Standard;

namespace Hzdtf.Demo.Persistence.Contract.Standard
{
    /// <summary>
    /// 测试表单持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface ITestFormPersistence : IPersistence<int, TestFormInfo>
    {
    }
}
