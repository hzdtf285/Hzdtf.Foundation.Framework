using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Vali.Model.Prop;

namespace Hzdtf.Utility.Standard.Vali.Model
{
    /// <summary>
    /// 模型属性集合验证
    /// @ 黄振东
    /// </summary>
    public class ModelPropsVali : IModelVali
    {
        /// <summary>
        /// 属性验证集合
        /// </summary>
        private readonly static IPropVali[] propValis = new IPropVali[]
        {
            new RequiredVali(),
            new MinLengthVali(),
            new MaxLengthVali(),
            new RangeVali(),
            new CompareVali(),
        };

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        public BasicReturnInfo Vali(object model)
        {
            BasicReturnInfo returnInfo = new BasicReturnInfo();
            Type type = model.GetType();
            PropertyInfo[] properties = type.GetProperties();
            if (properties.IsNullOrLength0())
            {
                return returnInfo;
            }

            foreach (PropertyInfo pf in properties)
            {
                if (pf.CanRead)
                {
                    foreach (IPropVali pv in propValis)
                    {
                        returnInfo = pv.Vali(model, pf, pf.GetValue(model));
                        if (returnInfo.Failure())
                        {
                            return returnInfo;
                        }
                    }
                }
            }

            return returnInfo;
        }
    }
}
