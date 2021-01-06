﻿using Hzdtf.BasicFunction.Model.Standard;
using Hzdtf.BasicFunction.Persistence.Contract.Standard;
using Hzdtf.BasicFunction.Service.Contract.Standard;
using Hzdtf.Service.Impl.Standard;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Service.Impl.Standard
{
    /// <summary>
    /// 序列服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class SequenceService : ServiceBase<int, SequenceInfo, ISequencePersistence>, ISequenceService
    {
    }
}
