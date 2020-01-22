using Hzdtf.Utility.Standard.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine
{
    /// <summary>
    /// 表单数据读取工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IFormDataReaderFactory : ISimpleFactory<string, IFormDataReader>
    {
    }
}
