using Autofac;
using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Platform.Contract.Standard.Config.AssemblyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hzdtf.CodeGenerator.UI.Framework
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AssemblyConfigLocalMember assemblyConfigLocalMember = new AssemblyConfigLocalMember();
            assemblyConfigLocalMember.ProtoAssemblyConfigReader = new AssemblyConfigXml();
            AssemblyConfigInfo assemblyConfig = assemblyConfigLocalMember.Reader();

            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();
            IContainer container = builder.UnifiedRegisterAssemblys(new BuilderParam()
            {
                AssemblyServices = assemblyConfig.Services
            });


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
