using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
        /// <summary>
        /// 网络连接状态
        /// </summary>
        /// <param name="connectionDescription">连接描述</param>
        /// <param name="reservedValue">返回值</param>
        /// <returns>是否连接</returns>
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        /// <summary>
        /// 同步本地IP
        /// </summary>
        private static readonly object syncLocalIP = new object();

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
                    string ip = null;
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        var ipList = NetworkInterface.GetAllNetworkInterfaces()
                            .Select(p => p.GetIPProperties())
                            .SelectMany(p => p.UnicastAddresses)
                            .Where(p => p.Address.AddressFamily == AddressFamily.InterNetwork && p.Address.ToString() != "127.0.0.1").ToList();
                        if (!ipList.IsNullOrCount0())
                        {
                            var states = new DuplicateAddressDetectionState[] { DuplicateAddressDetectionState.Preferred, DuplicateAddressDetectionState.Duplicate };
                            foreach (var state in states)
                            {
                                var temp = ipList.Where(p => p.DuplicateAddressDetectionState == state).OrderByDescending(p => p.DhcpLeaseLifetime).Select(p => p.Address).FirstOrDefault();
                                if (temp == null)
                                {
                                    continue;
                                }

                                ip = temp.ToString();

                                break;
                            }
                        }
                    }
                    else
                    {
                        ip = NetworkInterface.GetAllNetworkInterfaces()
                                .Select(p => p.GetIPProperties())
                                .SelectMany(p => p.UnicastAddresses)
                                .FirstOrDefault(p => p.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(p.Address))?.Address.ToString();
                    }

                    SetLocalIP(string.IsNullOrWhiteSpace(ip) ? "127.0.0.1" : ip);
                }

                return localIP;
            }
            set => SetLocalIP(value);
        }

        /// <summary>
        /// 设置本地IP
        /// </summary>
        /// <param name="localIP">本地IP</param>
        private static void SetLocalIP(string localIP)
        {
            lock(syncLocalIP)
            {
                NetworkUtil.localIP = localIP;
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

        /// <summary>
        /// 是否能ping通
        /// </summary>
        /// <param name="hostNameOrAddress">主机名或地址</param>
        /// <returns>是否能ping通</returns>
        public static bool IsPingSuccess(string hostNameOrAddress)
        {
            if (string.IsNullOrWhiteSpace(hostNameOrAddress))
            {
                return false;
            }

            var result = new Ping().SendPingAsync(hostNameOrAddress).Result;
            return result.Status == IPStatus.Success;
        }

        /// <summary>
        /// 默认HTTP客户端名称
        /// </summary>
        public const string DEFAULT_HTTP_CLIENT_NAME = "default";
    }
}
