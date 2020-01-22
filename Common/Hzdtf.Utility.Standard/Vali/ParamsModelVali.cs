using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Vali
{
    /// <summary>
    /// 可变参数模型验证
    /// @ 黄振东
    /// </summary>
    public class ParamsModelVali : IValiHandler
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

            int length = param.Length - paramIndex;
            for (byte i = paramIndex, row = 1; i < length; i++, row++)
            {
                object model = param[i];
                if (model == null)
                {
                    returnInfo.SetFailureMsg($"第{row}行：模型不能为null");
                    return returnInfo;
                }

                returnInfo = modelComVali.Exec(param, i);
                if (returnInfo.Failure())
                {
                    returnInfo.SetMsg($"第{row}行：{returnInfo.Msg}", $"第{row}行：{returnInfo.Desc}");
                    return returnInfo;
                }
            }

            return returnInfo;
        }
    }
}
