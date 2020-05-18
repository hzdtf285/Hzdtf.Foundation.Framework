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
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="value">值对象</param>
        /// <returns>影响行数</returns>
        int Submit(object value);
    }
}
