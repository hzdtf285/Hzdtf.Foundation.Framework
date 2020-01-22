using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 用户工具类
    /// @ 黄振东
    /// </summary>
    public static class UserTool
    {
        /// <summary>
        /// 当前用户
        /// @ 黄振东
        /// </summary>
        public static BasicUserInfo CurrUser
        {
            get => GetCurrUserFunc != null ? GetCurrUserFunc() : null;
        }

        /// <summary>
        /// 获取当前用户方法
        /// </summary>
        public static Func<BasicUserInfo> GetCurrUserFunc;

        /// <summary>
        /// 测试用户
        /// </summary>
        public readonly static BasicUserInfo TestUser = new BasicUserInfo()
        {
            Code = "000000",
            Name = "测试用户"
        };
    }
}
