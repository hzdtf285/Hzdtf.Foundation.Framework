using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.WorkFlow.Model.Standard;

namespace Hzdtf.WorkFlow.Persistence.Contract.Standard
{
    /// <summary>
    /// 表单持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IFormPersistence : IPersistence<int, FormInfo>
    {
    }
}
