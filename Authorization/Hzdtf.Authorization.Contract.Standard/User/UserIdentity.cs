using System.Security.Principal;

namespace Hzdtf.Authorization.Contract.Standard.User
{
    /// <summary>
    /// 用户标识
    /// @ 黄振东
    /// </summary>
    public class UserIdentity : IIdentity
    {
        /// <summary>
        /// 默认用户标识
        /// </summary>
        public const string DEFAULT_USER_IDENTITY = "userIdentity";

        /// <summary>
        /// 名称
        /// </summary>
        private readonly string name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name => name;

        /// <summary>
        /// 授权类型
        /// </summary>
        private readonly string authenticationType;

        /// <summary>
        /// 授权类型
        /// </summary>
        public string AuthenticationType => authenticationType;

        /// <summary>
        /// 是否已授权
        /// </summary>
        private readonly bool isAuthenticated;

        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsAuthenticated => isAuthenticated;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isAuthenticated">是否已授权</param>
        /// <param name="name">名称</param>
        /// <param name="authenticationType">授权类型</param>
        public UserIdentity(bool isAuthenticated, string name = DEFAULT_USER_IDENTITY, string authenticationType = "form")
        {
            this.isAuthenticated = isAuthenticated;
            this.name = name;
            this.authenticationType = authenticationType;
        }
    }
}
