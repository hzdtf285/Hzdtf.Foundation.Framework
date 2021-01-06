using Hzdtf.CodeGenerator.Contract.Standard;
using Hzdtf.CodeGenerator.Contract.Standard.Function;
using Hzdtf.CodeGenerator.Impl.Standard.Function;
using Hzdtf.CodeGenerator.Model.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.CodeGenerator.Impl.Standard
{
    /// <summary>
    /// 代码生成服务
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class CodeGeneratorService : ICodeGeneratorService
    {
        /// <summary>
        /// 属性生成服务
        /// </summary>
        public ModelGeneratorService ModelGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 持久生成服务
        /// </summary>
        public PersistenceGeneratorService PersistenceGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 服务生成服务
        /// </summary>
        public ServiceGeneratorService ServiceGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// Framework控制生成服务
        /// </summary>
        public FrameworkControllerGeneratorService FrameworkControllerGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// Core控制生成服务
        /// </summary>
        public CoreControllerGeneratorService CoreControllerGeneratorService
        {
            get;
            set;
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Generator(CodeGeneratorParamInfo param)
        {
            Util.FOLDER_ROOT.DeleteDirectory();

            ReturnInfo<bool> returnInfo = null;
            IList<IFunctionGeneratorService> services = GetFunctionServices(param.FunctionTypes);

            foreach (IFunctionGeneratorService s in services)
            {
                returnInfo = s.Generator(param);
            }

            return returnInfo;
        }

        /// <summary>
        /// 获取功能生成服务列表
        /// </summary>
        /// <param name="functionTypes">功能类型集合</param>
        /// <returns>功能生成服务列表</returns>
        private IList<IFunctionGeneratorService> GetFunctionServices(FunctionType[] functionTypes)
        {
            IList<IFunctionGeneratorService> services = new List<IFunctionGeneratorService>();
            foreach (FunctionType f in functionTypes)
            {
                switch (f)
                {
                    case FunctionType.MODEL:
                        services.Add(ModelGeneratorService);

                        break;

                    case FunctionType.PERSISTENCE:
                        services.Add(PersistenceGeneratorService);

                        break;

                    case FunctionType.SERVICE:
                        services.Add(ServiceGeneratorService);

                        break;

                    case FunctionType.FRAMEWORK_CONTROLLER:
                        services.Add(FrameworkControllerGeneratorService);

                        break;

                    case FunctionType.CORE_CONTROLLER:
                        services.Add(CoreControllerGeneratorService);

                        break;
                }
            }

            return services;
        }
    }
}
