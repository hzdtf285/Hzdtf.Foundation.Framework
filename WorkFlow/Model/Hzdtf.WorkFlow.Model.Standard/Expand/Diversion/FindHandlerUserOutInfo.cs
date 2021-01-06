using Hzdtf.Utility.Standard.Model;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.WorkFlow.Model.Standard.Expand.Diversion
{
    /// <summary>
    /// 查找处理者用户输出信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class FindHandlerUserOutInfo
    {
        /// <summary>
        /// 具体关卡
        /// </summary>
        [JsonProperty("concreteCensorship")]
        [MessagePack.Key("concreteCensorship")]
        public CodeNameInfo<int> ConcreteCensorship
        {
            get;
            set;
        }

        /// <summary>
        /// 用户信息数组
        /// </summary>
        [JsonProperty("users")]
        [MessagePack.Key("users")]
        public BasicUserInfo<int>[] Users
        {
            get;
            set;
        }
    }
}
