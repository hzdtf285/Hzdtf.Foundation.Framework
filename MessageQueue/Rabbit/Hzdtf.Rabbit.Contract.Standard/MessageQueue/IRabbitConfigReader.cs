using Hzdtf.Rabbit.Model.Standard.MessageQueue;
using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Contract.Standard.MessageQueue
{
    /// <summary>
    /// Rabbit配置读取接口
    /// @ 黄振东
    /// </summary>
    public interface IRabbitConfigReader : IReader<RabbitConfigInfo>
    {
    }
}
