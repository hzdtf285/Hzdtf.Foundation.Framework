using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Return;
using System.Reflection;
using Hzdtf.Utility.Standard.VaiParam;
using System.ComponentModel.DataAnnotations;
using Hzdtf.Utility.Standard.Attr.ParamAttr;

namespace Hzdtf.Autofac.Extend.Standard.Intercepteds
{
    /// <summary>
    /// 参数验证拦截器
    /// @ 黄振东
    /// </summary>
    public class ParamValiInterceptor : InterceptorBase
    {
        /// <summary>
        /// 同步验证参数字典
        /// </summary>
        private readonly static object syncDicValiParam = new object();

        /// <summary>
        /// 验证参数字典
        /// </summary>
        private readonly static IDictionary<Type, IValiParam> dicValiParams = new Dictionary<Type, IValiParam>()
        {
            { typeof(RequiredAttribute), new RequiredValiParam() },
            { typeof(MaxLengthAttribute), new MaxLengthValiParam() },
            { typeof(MinLengthAttribute), new MinLengthValiParam() },
            { typeof(RangeAttribute), new RangeValiParam() },
            { typeof(ModelAttribute), new ModelValiParam() },
            { typeof(MultiModelAttribute), new MultiModelValiParam() },
            { typeof(IdAttribute), new IdValiParam() },
            { typeof(ArrayNotEmptyAttribute), new ArrayNotEmptyValiParam() },
            { typeof(PageIndexAttribute), new PageIndexValiParam() },
            { typeof(PageSizeAttribute), new PageSizeValiParam() },
        };

        /// <summary>
        /// 拦截操作
        /// </summary>
        /// <param name="invocation">拦截参数</param>
        /// <param name="isExecProceeded">是否已执行</param>
        /// <returns>基本返回信息</returns>
        protected override BasicReturnInfo InterceptOperation(IInvocation invocation, out bool isExecProceeded)
        {
            isExecProceeded = false;
            BasicReturnInfo basicReturn = new BasicReturnInfo();
            if (invocation.Arguments.IsNullOrLength0())
            {
                return basicReturn;
            }

            ParameterInfo[] parameters = invocation.Method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                ParameterInfo p = parameters[i];
                IEnumerable<Attribute> atts = p.GetCustomAttributes();
                if (atts == null)
                {
                    continue;
                }

                DisplayName2Attribute paramDisplayNameAttr = p.GetCustomAttribute<DisplayName2Attribute>();
                string displayName = paramDisplayNameAttr == null ? p.Name : paramDisplayNameAttr.Name;

                foreach (Attribute a in atts)
                {
                    if (a is ValidationAttribute)
                    {
                        ValidationAttribute v = a as ValidationAttribute;
                        Type type = v.GetType();
                        if (dicValiParams.ContainsKey(type))
                        {
                            string errMsg = dicValiParams[type].Exec(invocation.GetArgumentValue(i), v, displayName);
                            if (string.IsNullOrWhiteSpace(errMsg))
                            {
                                continue;
                            }

                            basicReturn.SetFailureMsg(errMsg);

                            return basicReturn;
                        }
                    }                    
                }
            }

            return basicReturn;
        }

        /// <summary>
        /// 追加属性映射验证处理
        /// attrType必须是特性，且是继承于ValidationAttribute
        /// </summary>
        /// <param name="attrType">特性类型</param>
        /// <param name="valiParam">验证参数</param>
        public static void AppendAttrMapValiHandler(Type attrType, IValiParam valiParam)
        {
            lock (syncDicValiParam)
            {
                dicValiParams.Add(attrType, valiParam);
            }
        }
    }
}
