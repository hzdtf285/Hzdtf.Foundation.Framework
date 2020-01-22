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
    /// CSS压缩辅助类
    /// @ 黄振东
    /// </summary>
    public static class CssCompressorUtil
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void CompressorFile(string fileName)
        {
            string folder = Path.GetDirectoryName(fileName) + "/";
            string shortFileName = Path.GetFileNameWithoutExtension(fileName);

            CssCompressor compressor = new CssCompressor();
            string newfilename = folder + shortFileName + ".min.css";

            string source = File.ReadAllText(fileName);
            source = compressor.Compress(source);
            File.WriteAllText(newfilename, source);
        }
    }
}
