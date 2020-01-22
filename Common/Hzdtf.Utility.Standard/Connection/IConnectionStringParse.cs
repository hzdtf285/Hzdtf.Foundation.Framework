namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接字符串解析接口
    /// @ 黄振东
    /// </summary>
    public interface IConnectionStringParse
    {
        /// <summary>
        /// 将连接字符串解析为连接模型
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接信息</returns>
        ConnectionInfo Parse(string connectionString);
    }
}
