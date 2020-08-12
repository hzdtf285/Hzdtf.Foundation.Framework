using Hzdtf.Demo.Model.Standard;
using Hzdtf.Demo.Persistence.Contract.Standard;
using Hzdtf.Demo.Service.Contract.Standard;
using Hzdtf.Service.Impl.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Demo.Service.Impl.Standard
{
    /// <summary>
    /// 测试表单服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class TestFormService : ServiceBase<TestFormInfo, ITestFormPersistence>, ITestFormService
    {
    }
}
