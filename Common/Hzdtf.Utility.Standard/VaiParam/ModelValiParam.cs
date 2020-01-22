using Hzdtf.Utility.Standard.Attr.ParamAttr;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Vali;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.VaiParam
{
    /// <summary>
    /// 模型验证参数
    /// @ 黄振东
    /// </summary>
    public class ModelValiParam : ValiParamBase<ModelAttribute>
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
        protected override string ExecOper(object value, ModelAttribute valiAttr, string displayName)
        {
            if (value == null)
            {
                return null;
            }

            BasicReturnInfo basicReturn = modelCom.Exec(new object[] { value });
            if (basicReturn.Failure())
            {
                return basicReturn.Msg;
            }

            return null;
        }
    }
}
