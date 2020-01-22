using Hzdtf.Utility.Standard.Language;
using Hzdtf.Utility.Standard.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yahoo.Yui.Compressor;

namespace Hzdtf.Compressor.Impl.Standard
{
    /// <summary>
    /// JS压缩辅助类
    /// @ 黄振东
    /// </summary>
    public static class JSCompressorUtil
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void CompressorFile(string fileName)
        {
            string folder = Path.GetDirectoryName(fileName) + "/";
            string shortFileName = Path.GetFileNameWithoutExtension(fileName);

            JavaScriptCompressor compressor = new JavaScriptCompressor();
            //使用utf-8 编码文件
            compressor.Encoding = Encoding.UTF8;
            string newfilename = folder + shortFileName + ".min.js";

            string source = File.ReadAllText(fileName);
            source = compressor.Compress(source);
            File.WriteAllText(newfilename, source);
        }

        /// <summary>
        /// 合并并压缩Json文件组
        /// </summary>
        /// <param name="fileNames">文件名组</param>
        /// <param name="newFilePre">新的文件前辍</param>
        public static void MergeAndCompressorJsonFiles(string[] fileNames, string newFilePre)
        {
            if (fileNames.IsNullOrLength0())
            {
                return;
            }

            var list = ReaderFilesToLanguages(fileNames);
            var jsonContent = JsonUtil.SerializeIgnoreNull(list);

            JavaScriptCompressor compressor = new JavaScriptCompressor();
            //使用utf-8 编码文件
            compressor.Encoding = Encoding.UTF8;
            jsonContent = compressor.Compress(jsonContent);

            string folder = Path.GetDirectoryName(fileNames[0]) + "/";
            string newfilename = folder + newFilePre + ".min.json";

            File.WriteAllText(newfilename, jsonContent);
        }

        /// <summary>
        /// 读取文件内容并转换为语系信息列表
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>语系信息列表</returns>
        private static IList<LanguageInfo> ReaderFileToLanguages(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            string content = File.ReadAllText(fileName);
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            var list = JsonUtil.Deserialize<IList<LanguageInfo>>(content);
            IList<string> keys = new List<string>(list.Count);
            foreach (var k in list)
            {
                if (keys.Contains(k.Key))
                {
                    throw new Exception($"{fileName}中的key:{k.Key}已存在");
                }

                keys.Add(k.Key);
            }

            return list;
        }

        /// <summary>
        /// 读取文件组内容并转换为语系信息列表
        /// </summary>
        /// <param name="fileName">文件名组</param>
        /// <returns>语系信息列表</returns>
        private static IList<LanguageInfo> ReaderFilesToLanguages(string[] fileNames)
        {
            if (fileNames.IsNullOrLength0())
            {
                return null;
            }

            List<LanguageInfo> list = new List<LanguageInfo>();
            foreach (var file in fileNames)
            {
                var tempList = ReaderFileToLanguages(file);
                if (tempList.IsNullOrCount0())
                {
                    continue;
                }

                list.ForEach(x =>
                {
                    foreach (var t in tempList)
                    {
                        if(x.Key.Equals(t.Key))
                        {
                            throw new Exception($"{file}中的key:{t.Key}已存在");
                        }
                    }
                });

                list.AddRange(tempList);
            }

            return list;
        }
    }
}
