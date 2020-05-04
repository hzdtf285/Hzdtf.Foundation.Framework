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

        /// <summary>
        /// 从域名获取端口，如果是合格的域名，没有指定端口，默认http返回80，https返回443
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>端口</returns>
        public static int GetPortFromDomain(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                return 0;
            }

            domain = domain.ToLower();
            if (!domain.StartsWith("http://") && !domain.StartsWith("https://"))
            {
                domain = "http://" + domain;
            }

            return new Uri(domain).Port;
        }

        /// <summary>
        /// 过滤URL，将带有*或[::]替换为本地IP
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>过滤后的URL</returns>
        public static string FilterUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return url;
            }

            if (url.Contains("*"))
            {
                url = url.Replace("*", NetworkUtil.LocalIP);
            }
            else if (url.Contains("[::]"))
            {
                url = url.Replace("[::]", NetworkUtil.LocalIP);
            }

            return url;
        }
    }
}
