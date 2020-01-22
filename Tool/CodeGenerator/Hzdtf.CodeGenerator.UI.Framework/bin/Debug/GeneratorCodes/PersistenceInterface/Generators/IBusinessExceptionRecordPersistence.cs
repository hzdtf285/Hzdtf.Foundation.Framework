using Hzdtf.Persistence.Contract.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using hzdtd.Model.Standard;

namespace hzdtd.Persistence.Contract.Standard
{
    /// <summary>
    /// 业务异常记录持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IBusinessExceptionRecordPersistence : IPersistence<BusinessExceptionRecordInfo>
    {
    }
}
