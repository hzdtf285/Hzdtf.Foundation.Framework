using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.Service.Contract.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Contract.Standard
{
    /// <summary>
    /// 序列服务接口
    /// @ 黄振东
    /// </summary>
    public partial interface ISequenceService : IService<int, SequenceInfo>
    {
    }
}
