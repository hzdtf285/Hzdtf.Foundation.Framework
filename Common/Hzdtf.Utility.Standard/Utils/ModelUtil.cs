using Hzdtf.Utility.Standard.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 模型辅助类
    /// @ 黄振东
    /// </summary>
    public static class ModelUtil
    {
        /// <summary>
        /// 设置创建信息
        /// </summary>
        /// <param name="model">模型</param>
        public static void SetCreateInfo(this PersonTimeInfo model)
        {
            if (UserTool.CurrUser != null)
            {
                model.CreaterId = model.ModifierId = UserTool.CurrUser.Id;
                model.Creater = model.Modifier = UserTool.CurrUser.Name;
                model.CreateTime = model.ModifyTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <param name="model">模型</param>
        public static void SetModifyInfo(this PersonTimeInfo model)
        {
            if (UserTool.CurrUser != null)
            {
                model.ModifierId = UserTool.CurrUser.Id;
                model.Modifier = UserTool.CurrUser.Name;
                model.ModifyTime = DateTime.Now;
            }
        }
    }
}
