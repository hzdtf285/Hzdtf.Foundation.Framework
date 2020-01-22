using Hzdtf.Utility.Standard.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Service.Contract.Standard.Engine.Form
{
    /// <summary>
    /// 表单移除工厂接口
    /// @ 黄振东
    /// </summary>
    public interface IFormRemoveFactory : ISimpleFactory<string, IFormRemove>
    {
    }
}
