using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 实例映射方法信息
    /// @ 黄振东
    /// </summary>
    public class InstanceMapMethodsInfo
    {
        /// <summary>
        /// 实例
        /// </summary>
        public object Instance
        {
            get;
            set;
        }

        /// <summary>
        /// 方法
        /// </summary>
        public IList<MethodInfo> Methods
        {
            get;
            set;
        } = new List<MethodInfo>();

        /// <summary>
        /// 根据方法名获取方法
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <returns>方法</returns>
        public MethodInfo GetMethodByName(string methodName)
        {
            foreach (var m in Methods)
            {
                if (m.Name.Equals(methodName))
                {
                    return m;
                }
            }

            return null;
        }
    }
}
