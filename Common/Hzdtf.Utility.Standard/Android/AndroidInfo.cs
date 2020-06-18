using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Hzdtf.Utility.Standard.Android
{
    /// <summary>
    /// 安卓信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class AndroidInfo
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        [JsonProperty("name")]
        [MessagePack.Key("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 节点属性集合
        /// </summary>
        [JsonProperty("settings")]
        [MessagePack.Key("settings")]
        public IList<KeyValueInfo<string, string>> Settings 
        { 
            get; 
            set; 
        }
    }

    /// <summary>
    /// 安卓应用信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class AndroidAppInfo
    {
        /// <summary>
        /// 包名
        /// </summary>
        [JsonProperty("packageName")]
        [MessagePack.Key("packageName")]
        public string PackageName
        {
            get;
            set;
        }

        /// <summary>
        /// 版本号
        /// </summary>
        [JsonProperty("versionCode")]
        [MessagePack.Key("versionCode")]
        public int VersionCode
        {
            get;
            set;
        }

        /// <summary>
        /// 版本
        /// </summary>
        [JsonProperty("versionName")]
        [MessagePack.Key("versionName")]
        public string VersionName
        {
            get;
            set;
        }
    }
}
