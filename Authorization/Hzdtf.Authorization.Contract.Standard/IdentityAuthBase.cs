using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;

namespace Hzdtf.Authorization.Contract.Standard
{
    /// <summary>
    /// 身份授权基类
    /// 先执行原生验证，原生通过后再把授权信息保存到存储器里
    /// @ 黄振东
    /// </summary>
    public abstract class IdentityAuthBase : IIdentityAuth
    {
        #region 属性与字段

        /// <summary>
        /// 原生身份授权
        /// </summary>
        private IIdentityAuth protoIdentityAuth;

        /// <summary>
        /// 原生身份授权
        /// </summary>
        public IIdentityAuth ProtoIdentityAuth
        {
            get => protoIdentityAuth;
            set
            {
                if (value == this || value.GetType() == this.GetType())
                {
                    throw new Exception("原生身份授权不能为本对象");
                }

                protoIdentityAuth = value;
            }
        }

        #endregion

        #region IIdentityAuth 接口

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<BasicUserInfo> Accredit(string user, string password)
        {
            if (ProtoIdentityAuth == null)
            {
                throw new NullReferenceException("原生身份授权不能为null");
            }

            ReturnInfo<BasicUserInfo> returnInfo = ProtoIdentityAuth.Accredit(user, password);
            if (returnInfo.Failure())
            {
                return returnInfo;
            }

            SaveUserInfo(returnInfo.Data);

            return returnInfo;
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="basicUser">基本用户</param>
        protected abstract void SaveUserInfo(BasicUserInfo basicUser);

        #endregion
    }
}
