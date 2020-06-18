using AndroidXml;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Hzdtf.Utility.Standard.Android
{
    /// <summary>
    /// Apk辅助类
    /// @ 黄振东
    /// </summary>
    public static class ApkUtil
    {
        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <param name="apkFileName">apk文件名</param>
        /// <returns>应用信息</returns>
        public static AndroidAppInfo GetAppInfo(string apkFileName)
        {
            return ToAppInfo(GetManifestInfo(apkFileName));
        }

        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <param name="apkStream">apk流</param>
        /// <returns>应用信息</returns>
        public static AndroidAppInfo GetAppInfo(Stream apkStream)
        {
            return ToAppInfo(GetManifestInfo(apkStream));
        }

        /// <summary>
        /// 获取主配置文件信息
        /// </summary>
        /// <param name="apkFileName">apk文件名</param>
        /// <returns>安卓信息列表</returns>
        public static IList<AndroidInfo> GetManifestInfo(string apkFileName)
        {
            if (string.IsNullOrWhiteSpace(apkFileName))
            {
                return null;
            }

            using (var stream  = new FileStream(apkFileName, FileMode.Open))
            {
                return GetManifestInfo(stream);
            }
        }

        /// <summary>
        /// 获取主配置文件信息
        /// </summary>
        /// <param name="apkStream">apk流</param>
        /// <returns>安卓信息列表</returns>
        public static IList<AndroidInfo> GetManifestInfo(Stream apkStream)
        {
            var androidInfos = new List<AndroidInfo>();
            byte[] manifestData = null;

            using (var zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(apkStream))
            {
                foreach (ICSharpCode.SharpZipLib.Zip.ZipEntry item in zipfile)
                {
                    if (item.Name.ToLower() == "androidmanifest.xml")
                    {
                        using (Stream strm = zipfile.GetInputStream(item))
                        {
                            using (BinaryReader s = new BinaryReader(strm))
                            {
                                manifestData = s.ReadBytes((int)item.Size);
                            }
                        }

                        break;
                    }
                }

                zipfile.Close();
            }

            if (manifestData.IsNullOrLength0())
            {
                return null;
            }

            #region 读取文件内容

            using (var stream = new MemoryStream(manifestData))
            {
                using (var reader = new AndroidXmlReader(stream))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                AndroidInfo info = new AndroidInfo();
                                androidInfos.Add(info);
                                info.Name = reader.Name;
                                info.Settings = new List<KeyValueInfo<string, string>>();
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);

                                    var setting = new KeyValueInfo<string, string>() { Key = reader.Name, Value = reader.Value };
                                    info.Settings.Add(setting);
                                }
                                reader.MoveToElement();

                                break;
                        }
                    }

                    reader.Close();
                }
            }

            #endregion

            return androidInfos;
        }

        /// <summary>
        /// 将安卓信息列表转换为安卓应用信息
        /// </summary>
        /// <param name="androids">安卓信息列表</param>
        /// <returns>安卓应用信息</returns>
        private static AndroidAppInfo ToAppInfo(IList<AndroidInfo> androids)
        {
            if (androids.IsNullOrCount0())
            {
                return null;
            }

            var mainFest = androids.Where(p => p.Name == "manifest").FirstOrDefault();
            if (mainFest == null || mainFest.Settings.IsNullOrCount0())
            {
                return null;
            }

            var appInfo = new AndroidAppInfo();
            foreach (var subItem in mainFest.Settings)
            {
                switch (subItem.Key)
                {
                    case "package":
                        appInfo.PackageName = subItem.Value;

                        break;

                    case "android:versionCode":
                        int code;
                        if (int.TryParse(subItem.Value, out code))
                        {
                            appInfo.VersionCode = code;
                        }

                        break;

                    case "android:versionName":
                        appInfo.VersionName = subItem.Value;

                        break;
                }
            }

            return appInfo;
        }
    }
}
