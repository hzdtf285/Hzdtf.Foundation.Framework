using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Utility.Standard.Vali.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// 模型组合验证
    /// @ 黄振东
    /// </summary>
    public class ModelComVali : IValiHandler
    {
        /// <summary>
        /// 模型验证列表
        /// </summary>
        private static readonly IList<IModelVali> modelValis = new List<IModelVali>()
        {
            new ModelPropsVali()
        };

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="paramIndex">参数索引位置</param>
        /// <returns>返回信息</returns>
        public BasicReturnInfo Exec(object[] param, byte paramIndex = 0)
        {
            BasicReturnInfo returnInfo = new BasicReturnInfo();

            object model = param[paramIndex];
            if (model == null)
            {
                return returnInfo;
            }

            foreach (IModelVali vali in modelValis)
            {
                returnInfo = vali.Vali(model);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }
            }

            return returnInfo;
        }

        /// <summary>
        /// 追加模型验证
        /// </summary>
        /// <param name="modelVali">模型验证</param>
        public static void AppendModelVali(IModelVali modelVali) => modelValis.Add(modelVali);
    }
}
