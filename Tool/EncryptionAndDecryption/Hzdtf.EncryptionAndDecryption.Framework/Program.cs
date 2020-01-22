using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hzdtf.EncryptionAndDecryption.Framework
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// @ 黄振东
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
