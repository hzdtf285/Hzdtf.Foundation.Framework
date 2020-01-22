using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 数据库池接口
    /// @ 黄振东
    /// </summary>
    public interface IDatabasePool : IResourcePool<string, object>
    {
    }
}
