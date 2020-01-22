using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// 过程调用接口
    /// @ 黄振东
    /// </summary>
    public interface IProcessCall
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="parames">参数数组</param>
        /// <returns>返回值</returns>
        object Invoke(string key, params object[] parames);
    }
}
