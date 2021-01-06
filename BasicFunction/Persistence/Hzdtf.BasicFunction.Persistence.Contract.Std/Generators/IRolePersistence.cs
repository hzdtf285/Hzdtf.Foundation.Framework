using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.BasicFunction.Model.Standard;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 角色持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IRolePersistence : IPersistence<int, RoleInfo>
    {
    }
}
