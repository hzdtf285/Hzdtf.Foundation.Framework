using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.CodeGenerator.Model.Standard
{
    /// <summary>
    /// 功能类型枚举
    /// @ 黄振东
    /// </summary>
    public enum FunctionType : byte
    {
        /// <summary>
        /// 模型
        /// </summary>
        MODEL = 1,

        /// <summary>
        /// 持久
        /// </summary>
        PERSISTENCE = 2,

        /// <summary>
        /// 服务
        /// </summary>
        SERVICE = 3,

        /// <summary>
        /// Framework控制
        /// </summary>
        FRAMEWORK_CONTROLLER = 4,

        /// <summary>
        /// Core控制
        /// </summary>
        CORE_CONTROLLER = 5,
    }
}
