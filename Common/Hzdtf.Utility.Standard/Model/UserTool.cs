using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 用户工具类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public static class UserTool<IdT>
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        private static BasicUserInfo<IdT> CurrUser
        {
            get => GetCurrUserFunc != null ? GetCurrUserFunc() : null;
        }

        /// <summary>
        /// 获取当前用户方法
        /// </summary>
        public static Func<BasicUserInfo<IdT>> GetCurrUserFunc;

        /// <summary>
        /// 测试用户
        /// </summary>
        public readonly static BasicUserInfo<IdT> TestUser = new BasicUserInfo<IdT>()
        {
            Code = "000000",
            Name = "测试用户"
        };

        /// <summary>
        /// 获取当前用户，如果传入的用户不为null，则取传入的用户。否则调GetCurrUserFunc委托
        /// </summary>
        /// <param name="currUser">当前用户</param>
        /// <param name="notExistsIsOutTestUser">如果不存在是否输出测试用户</param>
        /// <returns>当前用户</returns>
        public static BasicUserInfo<IdT> GetCurrUser(BasicUserInfo<IdT> currUser = null, bool notExistsIsOutTestUser = false)
        {
            var result = currUser == null ? CurrUser : currUser;
            if (result == null && notExistsIsOutTestUser)
            {
                result = TestUser;
            }

            return result;
        }

        /// <summary>
        /// 获取当前用户，如果传入的用户不为null，则取传入的用户。否则调GetCurrUserFunc委托
        /// </summary>
        /// <typeparam name="UserT">用户类型</typeparam>
        /// <param name="currUser">当前用户</param>
        /// <param name="notExistsIsOutTestUser">如果不存在是否输出测试用户</param>
        /// <returns>当前用户</returns>
        public static UserT GetCurrUser<UserT>(BasicUserInfo<IdT> currUser = null, bool notExistsIsOutTestUser = false) where UserT : BasicUserInfo<IdT>
        {
            var result = currUser == null ? CurrUser : currUser;
            if (result == null && notExistsIsOutTestUser)
            {
                result = TestUser;
            }

            return result as UserT;
        }
    }
}
