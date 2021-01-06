using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.BasicFunction.Model.Standard;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 用户持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IUserPersistence : IPersistence<int, UserInfo>
    {
    }
}
