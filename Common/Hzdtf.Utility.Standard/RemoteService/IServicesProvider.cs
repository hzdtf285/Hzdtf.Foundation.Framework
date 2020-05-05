using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Utility.Standard.RemoteService
{
    /// <summary>
    /// 服务提供者接口
    /// @ 黄振东
    /// </summary>
    public interface IServicesProvider : IDisposable
    {
        /// <summary>
        /// 异步根据服务名获取地址数组
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="tag">标签</param>
        /// <returns>地址数组任务</returns>
        Task<string[]> GetAddresses(string serviceName, string tag = null);
    }
}
