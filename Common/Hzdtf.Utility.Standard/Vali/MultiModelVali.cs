using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// 多个模型验证
    /// @ 黄振东
    /// </summary>
    public class MultiModelVali : IValiHandler
    {
        /// <summary>
        /// 模型组合验证
        /// </summary>
        private readonly static ModelComVali modelComVali = new ModelComVali();

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="paramIndex">参数索引位置</param>
        /// <returns>返回信息</returns>
        public BasicReturnInfo Exec(object[] param, byte paramIndex = 0)
        {
            BasicReturnInfo returnInfo = new BasicReturnInfo();
            if (param[paramIndex] == null)
            {
                return returnInfo;
            }

            System.Collections.IEnumerable list = param[paramIndex] as System.Collections.IEnumerable;
            int row = 1;
            foreach (var model in list)
            {
                if (model == null)
                {
                    returnInfo.SetFailureMsg($"第{row}行：模型不能为null");
                    return returnInfo;
                }

                returnInfo = modelComVali.Exec(new object[] { model }, 0);
                if (returnInfo.Failure())
                {
                    returnInfo.SetMsg($"第{row}行：{returnInfo.Msg}", $"第{row}行：{returnInfo.Desc}");
                    return returnInfo;
                }

                row++;
            }

            return returnInfo;
        }
    }
}
