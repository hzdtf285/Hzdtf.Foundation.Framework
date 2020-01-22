using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Resources
{
    /// <summary>
    /// 数据库池基类
    /// @ 黄振东
    /// </summary>
    public abstract class DatabasePoolBase : ResourcePoolBase<string, object>, IDatabasePool
    {
        #region 重写父类的方法

        /// <summary>
        /// 创建一个新的键
        /// </summary>
        /// <returns>一个新的键</returns>
        protected override string CreateKey() => StringUtil.NewShortGuid();

        #endregion
    }
}
