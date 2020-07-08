using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.ProcessCall
{
    /// <summary>
    /// Rpc客户端方法接口
    /// @ 黄振东
    /// </summary>
    public interface IRpcClientMethod
    {
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="message">消息</param>
        /// <returns>返回数据</returns>
        object Call(MethodInfo method, object message);
    }
}
