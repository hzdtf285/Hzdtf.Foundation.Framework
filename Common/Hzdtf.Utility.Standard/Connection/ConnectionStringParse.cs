using System;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接字符串解析器
    /// @ 黄振东
    /// </summary>
    public class ConnectionStringParse : IConnectionStringParse
    {
        #region IConnectionStringParse 接口

        /// <summary>
        /// 将连接字符串解析为连接模型
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接模型</returns>
        public ConnectionInfo Parse(string connectionString)
        {
            ValidateUtil.ValidateNullOrEmpty(connectionString, "连接字符串");

            string[] conns = connectionString.Split(';');
            ConnectionInfo connectionInfo = CreateConnectionInfo();

            char spliStr = '=';
            foreach (string item in conns)
            {
                if (string.IsNullOrWhiteSpace(item) || !item.Contains(spliStr.ToString()))
                {
                    continue;
                }

                string[] nameValues = item.Split(spliStr);
                if (string.IsNullOrWhiteSpace(nameValues[0]) || string.IsNullOrWhiteSpace(nameValues[1]))
                {
                    continue;
                }

                switch (nameValues[0])
                {
                    case "host":
                        connectionInfo.Host = nameValues[1];

                        break;

                    case "password":
                        connectionInfo.Password = nameValues[1];

                        break;

                    case "port":
                        connectionInfo.Port = Convert.ToInt32(nameValues[1]);

                        break;

                    case "user":
                        connectionInfo.User = nameValues[1];

                        break;

                    default:
                        SetOnlyHaveValue(connectionInfo, nameValues[0], nameValues[1]);

                        break;
                }
            }

            ValidateUtil.ValidateNullOrEmpty(connectionInfo.Host, "主机名");
            ValidateUtil.ValidateNullOrEmpty(connectionInfo.User, "用户");
            if (connectionInfo.Port == 0)
            {
                throw new ArgumentException("端口不能为0");
            }

            ValidateOnlyHaveParams(connectionInfo);

            return connectionInfo;
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 设置独特的值
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        protected virtual void SetOnlyHaveValue(ConnectionInfo connectionInfo, string name, string value)
        {
        }

        /// <summary>
        /// 创建连接信息
        /// </summary>
        /// <returns>连接信息</returns>
        protected virtual ConnectionInfo CreateConnectionInfo() => new ConnectionInfo();

        /// <summary>
        /// 验证独特的参数集合，如果不通过则抛出对应异常
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        protected virtual void ValidateOnlyHaveParams(ConnectionInfo connectionInfo)
        {
        }

        #endregion
    }
}
