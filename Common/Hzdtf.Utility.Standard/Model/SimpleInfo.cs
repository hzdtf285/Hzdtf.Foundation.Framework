using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 简单信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    [MessagePackObject]
    public class SimpleInfo<IdT> : ICloneable
    {
        /// <summary>
        /// ID_名称
        /// </summary>
        public const string Id_Name = "Id";

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        [Display(AutoGenerateField = false)]
        [MessagePack.Key("id")]
        public IdT Id
        {
            get;
            set;
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns>拷贝后的对象</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString() => JsonUtil.SerializeIgnoreNull(this);
    }

    /// <summary>
    /// 简单信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class SimpleInfo : SimpleInfo<int>
    {
    }

    /// <summary>
    /// 简单租户信息
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    public class SimpleTenanInfo<IdT> : SimpleInfo<IdT>
    {
        /// <summary>
        /// 租户ID_名称
        /// </summary>
        public const string TenantId_Name = "TenantId";

        /// <summary>
        /// 租户ID
        /// </summary>
        [JsonProperty("tenantId")]
        [Display(Name = "租户ID", Order = 10, AutoGenerateField = false)]
        [MessagePack.Key("tenantId")]
        public IdT TenantId
        {
            get;
            set;
        }
    }
}
