using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 网络辅助类
    /// @ 黄振东
    /// </summary>
    public static class NetworkUtil
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        /// <summary>
        /// 本地IP
        /// </summary>
        private static string localIP;

        /// <summary>
        /// 本地IP
        /// </summary>
        public static string LocalIP
        {
            get
            {
                if (localIP == null)
                {
                    IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                    for (int i = 0; i < ipEntry.AddressList.Length; i++)
                    {
                        if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIP = ipEntry.AddressList[i].ToString();
                            break;
                        }
                    }
                }

                return localIP;
            }
        }

        /// <summary>
        /// 判断网络是否已连接
        /// </summary>
        /// <returns>网络是否已连接</returns>
        public static bool IsNetConnected()
        {
            int i = 0;
            return InternetGetConnectedState(out i, 0);
        }
    }
}
