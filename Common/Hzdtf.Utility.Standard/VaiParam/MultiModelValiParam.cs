using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Vali;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 多个模型验证参数
    /// @ 黄振东
    /// </summary>
    public class MultiModelValiParam : ValiParamBase<MultiModelAttribute>
    {
        /// <summary>
        /// 模型组合验证
        /// </summary>
        private readonly static ModelComVali modelCom = new ModelComVali();

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="valiAttr">验证特性</param>
        /// <param name="displayName">显示名</param>
        /// <returns>错误消息</returns>
        protected override string ExecOper(object value, MultiModelAttribute valiAttr, string displayName)
        {
            if (value == null)
            {
                return null;
            }

            System.Collections.IEnumerable list = value as System.Collections.IEnumerable;
            int row = 1;
            foreach (var model in list)
            {
                if (model == null)
                {
                    return $"第{row}行：模型不能为null";
                }

                BasicReturnInfo returnInfo = modelCom.Exec(new object[] { model }, 0);
                if (returnInfo.Failure())
                {
                    return $"第{row}行：{returnInfo.Msg}";
                }

                row++;
            }

            return null;
        }
    }
}
