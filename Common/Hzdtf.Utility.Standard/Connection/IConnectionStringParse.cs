namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接字符串解析接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ConnectionInfoT">连接信息类型</typeparam>
    public interface IConnectionStringParse<ConnectionInfoT>
        where ConnectionInfoT : ConnectionInfo
    {
        /// <summary>
        /// 将连接字符串解析为连接模型
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接信息</returns>
        ConnectionInfoT Parse(string connectionString);
    }

    /// <summary>
    /// 连接字符串解析接口
    /// @ 黄振东
    /// </summary>
    public interface IConnectionStringParse : IConnectionStringParse<ConnectionInfo>
    {
    }
}
