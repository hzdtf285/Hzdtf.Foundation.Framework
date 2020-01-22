using Hzdtf.Rabbit.Model.Standard.Connection;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Connection;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Rabbit.Impl.Standard.Connection
{
    /// <summary>
    /// Rabbit连接字符串解析器
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class RabbitConnectionStringParse : ConnectionStringParse
    {
        #region 重写父类的方法

        /// <summary>
        /// 设置独特的值
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        protected override void SetOnlyHaveValue(ConnectionInfo connectionInfo, string name, string value)
        {
            RabbitConnectionInfo rabbitConnectionInfo = connectionInfo as RabbitConnectionInfo;
           
            switch (name)
            {
                case "virtualPath":
                    rabbitConnectionInfo.VirtualPath = value;

                    break;

                case "autoRecovery":
                    rabbitConnectionInfo.AutoRecovery = Convert.ToBoolean(value);

                    break;

                case "heartbeat":
                    rabbitConnectionInfo.Heartbeat = Convert.ToUInt16(value);

                    break;
            }
        }

        /// <summary>
        /// 验证独特的参数集合，如果不通过则抛出对应异常
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        protected override void ValidateOnlyHaveParams(ConnectionInfo connectionInfo)
        {
            RabbitConnectionInfo rabbitConnectionInfo = connectionInfo as RabbitConnectionInfo;
            ValidateUtil.ValidateNullOrEmpty(rabbitConnectionInfo.VirtualPath, "虚拟路径");
        }

        /// <summary>
        /// 创建连接信息
        /// </summary>
        /// <returns>连接信息</returns>
        protected override ConnectionInfo CreateConnectionInfo()
        {
            return new RabbitConnectionInfo();
        }

        #endregion
    }
}
