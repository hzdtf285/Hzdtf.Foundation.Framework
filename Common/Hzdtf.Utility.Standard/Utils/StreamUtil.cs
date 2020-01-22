using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 流辅助类
    /// @ 黄振东
    /// </summary>
    public static class StreamUtil
    {
        /// <summary>
        /// 同步写入文件
        /// </summary>
        private static readonly object syncWriteFile = new object();

        /// <summary>
        /// 根据文件名读取文件内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>文件内容</returns>
        public static string ReaderFileContent(this string fileName) => ReaderFileContent(fileName, Encoding.UTF8);

        /// <summary>
        /// 根据文件名读取文件内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码</param>
        /// <returns>文件内容</returns>
        public static string ReaderFileContent(this string fileName, Encoding encoding) => ReaderStreamToString(new FileStream(fileName, FileMode.Open));
       
        /// <summary>
        /// 读取流并转换为字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>字符串</returns>
        public static string ReaderStreamToString(this Stream stream) => ReaderStreamToString(stream, Encoding.UTF8);

        /// <summary>
        /// 读取流并转换为字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static string ReaderStreamToString(this Stream stream, Encoding encoding) => encoding.GetString(ReaderStream(stream));

        /// <summary>
        /// 读取流
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>字节数组</returns>
        public static byte[] ReaderStream(this Stream stream)
        {
            try
            {
                byte[] data = new byte[stream.Length];
                int readed = 0;
                while (readed < data.Length)
                {
                    readed += stream.Read(data, readed, data.Length - readed);
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }
        }

        /// <summary>
        /// 写入文件内容
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="content">内容</param>
        /// <param name="append">是否追加</param>
        public static void WriteFileContent(this string fileName, string content, bool append = false)
        {
            byte[] logBytes = Encoding.UTF8.GetBytes(content);
            Stream stream = null;
            lock (syncWriteFile)
            {
                if (File.Exists(fileName))
                {
                    if (append)
                    {
                        stream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        stream = new FileStream(fileName, FileMode.Open, FileAccess.Write);
                    }
                }
                else
                {
                    stream = File.Create(fileName);
                }

                try
                {
                    stream.Write(logBytes, 0, logBytes.Length);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                        stream = null;
                    }
                }
            }
        }

        /// <summary>
        /// 将数据转换为字节流
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns>字节流</returns>
        public static byte[] ConvertBytes<T>(T data)
        {
            if (typeof(T) == typeof(string))
            {
                return Encoding.UTF8.GetBytes(data as string);
            }
            else
            {
                return Encoding.UTF8.GetBytes(JsonUtil.SerializeIgnoreNull(data));
            }
        }

        /// <summary>
        /// 将字节流转换为数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dataBytes">字节流</param>
        /// <returns>数据</returns>
        public static T ConvertData<T>(byte[] dataBytes)
        {
            if (dataBytes.IsNullOrLength0())
            {
                return default(T);
            }
            if (typeof(T) == typeof(string))
            {
                string str = Encoding.UTF8.GetString(dataBytes);
                return (T)Convert.ChangeType(str, typeof(T));
            }
            else
            {
                return JsonUtil.Deserialize<T>(Encoding.UTF8.GetString(dataBytes));
            }
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="stream">流</param>
        public static void WriteFile(this string fileName, Stream stream) => WriteFile(fileName, stream.ReaderStream());

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="data">数据</param>
        public static void WriteFile(this string fileName, byte[] data)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                fileStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                    fileStream = null;
                }
            }
        }
    }
}
