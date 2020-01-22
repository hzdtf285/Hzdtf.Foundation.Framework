using Hzdtf.BasicFunction.Model.Standard.Expand.Attachment;
using Hzdtf.BasicFunction.Service.Contract.Standard.Expand.Attachment;
using Hzdtf.Service.Impl.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.BasicFunction.Service.Impl.Standard.Expand.Attachment
{
    /// <summary>
    /// 附件存储
    /// @ 黄振东
    /// </summary>
    [Inject]
    public partial class AttachmentStore : BasicServiceBase, IAttachmentStore
    {
        /// <summary>
        /// 文件根路径
        /// </summary>
        private static string fileRoot;

        /// <summary>
        /// 同步文件根路径
        /// </summary>
        private static readonly object syncFileRoot = new object();

        /// <summary>
        /// 同步创建文件夹
        /// </summary>
        private static readonly object syncCreateRoot = new object();

        /// <summary>
        /// 文件根路径
        /// </summary>
        public string FileRoot
        {
            get
            {
                if (fileRoot == null)
                {
                    string rootType = AppConfig["Attachment:UploadRootType"];
                    switch (rootType)
                    {
                        case "virtual":
                            lock (syncFileRoot)
                            {
                                fileRoot = $"{AppContext.BaseDirectory}{AppConfig["Attachment:UploadRoot"]}";
                            }

                            break;

                        default:
                            lock (syncFileRoot)
                            {
                                fileRoot = AppConfig["Attachment:UploadRoot"];
                            }

                            break;
                    }
                }


                return fileRoot;
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="attachmentStream">附件流</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<string>> Upload(params AttachmentStreamInfo[] attachmentStream)
        {
            // 以当前年月为目录
            string yearMonthDic = $"{DateTime.Now.ToCompactShortYM()}/";
            string dic = $"{FileRoot}{yearMonthDic}";
            lock (syncCreateRoot)
            {
                dic.CreateNotExistsDirectory();
            }

            ReturnInfo<IList<string>> returnInfo = new ReturnInfo<IList<string>>();
            returnInfo.Data = new List<string>(attachmentStream.Length);

            try
            {
                foreach (var attStream in attachmentStream)
                {
                    string expandName = attStream.FileName.FileExpandName();
                    string newFileName = $"{StringUtil.NewShortGuid()}{expandName}";

                    $"{dic}{newFileName}".WriteFile(attStream.Stream);
                    returnInfo.Data.Add($"{AppConfig["Attachment:DownloadRoot"]}{yearMonthDic}{newFileName}");
                }
            }
            catch (Exception ex)
            {
                Log.ErrorAsync(ex.Message, ex, this.GetType().FullName);
                returnInfo.SetFailureMsg(ex.Message);
            }

            return returnInfo;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="fileAddress">文件地址</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Remove(params string[] fileAddress)
        {
            ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
            try
            {
                foreach (string f in fileAddress)
                {
                    // 替换虚拟路径
                    string newF = f.Replace(AppConfig["Attachment:DownloadRoot"], null);
                    $"{FileRoot}{newF}".DeleteFile();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorAsync(ex.Message, ex, this.GetType().FullName);
                returnInfo.SetFailureMsg(ex.Message);
            }

            return returnInfo;
        }
    }
}
