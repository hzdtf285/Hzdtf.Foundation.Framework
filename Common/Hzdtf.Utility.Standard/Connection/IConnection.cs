using Hzdtf.Utility.Standard.Release;

namespace Hzdtf.Utility.Standard.Connection
{
    /// <summary>
    /// 连接接口
    /// @ 黄振东
    /// </summary>
    public interface IConnection : ICloseable
    {
        #region 属性

        /// <summary>
        /// 状态
        /// </summary>
        ConnectionStatusType Status
        {
            get;
        }

        #endregion

        #region 打开关闭

        /// <summary>
        /// 打开
        /// </summary>
        void Open();

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        void Open(string connectionString);

        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="connectionInfo">连接信息</param>
        void Open(ConnectionInfo connectionInfo);

        #endregion
    }
}
