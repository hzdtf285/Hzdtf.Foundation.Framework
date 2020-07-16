using Hzdtf.Authorization.Contract.Standard.User;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using System;

namespace Hzdtf.Authorization.Contract.Standard.IdentityAuth
{
    /// <summary>
    /// 身份授权基类
    /// 先执行原生验证，原生通过后再把授权信息保存到存储器里
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="UserT">用户顾炎武</typeparam>
    public abstract class IdentityAuthBase<UserT> : IIdentityAuth<UserT>
        where UserT : BasicUserInfo
    {
        #region 属性与字段

        /// <summary>
        /// 用户验证
        /// </summary>
        public IUserVali<UserT> UserVali
        {
            get;
            set;
        }

        #endregion

        #region IIdentityAuth 接口

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="password">密码</param>
        /// <returns>返回信息</returns>
        public ReturnInfo<UserT> Accredit(string user, string password)
        {
            if (UserVali == null)
            {
                throw new NullReferenceException("用户验证不能为null");
            }

            ReturnInfo<UserT> returnInfo = UserVali.Vali(user, password);
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
        /// <param name="user">用户</param>
        protected abstract void SaveUserInfo(UserT user);

        #endregion
    }
}
