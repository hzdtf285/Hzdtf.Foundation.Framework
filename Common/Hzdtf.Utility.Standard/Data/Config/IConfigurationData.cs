using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Data.Config
{
    /// <summary>
    /// 配置数据接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public interface IConfigurationData<T> : IReader<T>, IWrite<T>
    {
    }
}
