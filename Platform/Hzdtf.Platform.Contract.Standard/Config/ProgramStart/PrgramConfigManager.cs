using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Platform.Contract.Standard.Config.ProgramStart
{
    /// <summary>
    /// 程序配置管理器
    /// @ 黄振东
    /// </summary>
    public sealed class PrgramConfigManager
    {
        /// <summary>
        /// 程序配置读取
        /// </summary>
        public static IReader<ProgramStartInfo[]> ProgramConfigReader
        {
            get;
            set;
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="programStartInfos">程序启动信息集合</param>
        public static void Start(ProgramStartInfo[] programStartInfos = null)
        {
            if (programStartInfos == null)
            {
                if (ProgramConfigReader == null)
                {
                    return;
                }

                programStartInfos = ProgramConfigReader.Reader();
            }

            if (programStartInfos.IsNullOrLength0())
            {
                return;
            }

            IProgramStart[] programStarts = new IProgramStart[programStartInfos.Length];
            for (int i = 0; i < programStarts.Length; i++)
            {
                programStarts[i] = ReflectUtil.CreateInstance<IProgramStart>(programStartInfos[i].FullClass);
            }

            for (int i = 0; i < programStarts.Length; i++)
            {
                programStarts[i].Start(programStartInfos[i].Args);
            }
        }
    }
}
