using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 文件辅助类
    /// @ 黄振东
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件扩展名</returns>
        public static string FileExpandName(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }
            
            int lastPointIndex = fileName.LastIndexOf(".");
            if (lastPointIndex == -1 || lastPointIndex == fileName.Length - 1)
            {
                return null;
            }

            return fileName.Substring(lastPointIndex, fileName.Length - lastPointIndex).ToLower();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void DeleteFile(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// 转换为URL文件路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="domainUrl">域名URL</param>
        /// <param name="folder">文件夹</param>
        /// <returns>URL文件路径</returns>
        public static string ToUrlFilePath(this string fileName, string domainUrl, string folder)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return fileName;
            }

            return (domainUrl + folder + fileName).Replace("\\", "/");
        }

        /// <summary>
        /// 转换为物理文件路径
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <param name="domainUrl">域名URL</param>
        /// <param name="isReplaceBackslash">是否替换反斜杠</param>
        /// <returns>物理文件路径</returns>
        public static string ToPhysicsFilePath(this string fileUrl, string domainUrl, bool isReplaceBackslash = true)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                return fileUrl;
            }

            string filePath = fileUrl.Replace(domainUrl, AppContext.BaseDirectory);
            if (isReplaceBackslash)
            {
                filePath = filePath.Replace("/", "\\");
            }

            return filePath;
        }

        /// <summary>
        /// 创建文件夹，如果文件夹存在则忽略
        /// </summary>
        /// <param name="path">路径</param>
        public static void CreateNotExistsDirectory(this string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">路径</param>
        public static void DeleteDirectory(this string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            if (Directory.Exists(path))
            {
                DirectoryInfo dicInfo = new DirectoryInfo(path);
                FileSystemInfo[] fileInfos = dicInfo.GetFileSystemInfos();

                if (!fileInfos.IsNullOrLength0())
                {
                    foreach (FileSystemInfo fileInfo in fileInfos)
                    {
                        if (fileInfo is DirectoryInfo)
                        {
                            DirectoryInfo subdir = new DirectoryInfo(fileInfo.FullName);
                            subdir.Delete(true);
                        }
                        else
                        {
                            fileInfo.Delete();
                        }
                    }
                }

                Directory.Delete(path, true);
            }
        }
    }
}
