using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Conversion;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hzdtf.Utility.Standard.Model
{
    /// <summary>
    /// 基本用户信息
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    [MessagePackObject]
    public class BasicUserInfo<IdT> : CodeNameInfo<IdT>
    {
        /// <summary>
        /// 登录ID_名称
        /// </summary>
        public const string LoginId_Name = "LoginId";

        /// <summary>
        /// 登录ID
        /// </summary>
        [JsonProperty("loginId")]
        [DisplayName("登录ID")]
        [MaxLength(20)]
        [Required]
        [Display(Name = "登录ID", Order = 1, AutoGenerateField = true)]
        [MessagePack.Key("loginId")]
        public string LoginId
        {
            get;
            set;
        }

        /// <summary>
        /// 密码_名称
        /// </summary>
        public const string Password_Name = "Password";

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        [DisplayName("密码")]
        [Required]
        [MaxLength(20)]
        [Display(Name = "密码", Order = 2, AutoGenerateField = false)]
        [MessagePack.Key("password")]
        public string Password
        {
            get;
            set;
        }
        
        /// <summary>
        /// 启用_名称
        /// </summary>
        public const string Enabled_Name = "Enabled";

        /// <summary>
        /// 启用
        /// </summary>
        [JsonProperty("enabled")]
        [DisplayValueConvert(typeof(BoolValueToTextConvert), typeof(BoolTextToValueConvert))]
        [Display(Name = "启用", Order = 50, AutoGenerateField = true)]
        [MessagePack.Key("enabled")]
        public bool Enabled
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 系统内置_名称
        /// </summary>
        public const string SystemInlay_Name = "SystemInlay";

        /// <summary>
        /// 系统内置
        /// </summary>
        [JsonProperty("systemInlay")]
        [DisplayValueConvert(typeof(BoolValueToTextConvert), typeof(BoolTextToValueConvert))]
        [Display(Name = "系统内置", Order = 4, AutoGenerateField = false)]
        [MessagePack.Key("systemInlay")]
        public bool SystemInlay
        {
            get;
            set;
        }

        /// <summary>
        /// 登录时间_名称
        /// </summary>
        public const string LoginTime_Name = "LoginTime";

        /// <summary>
        /// 登录时间
        /// </summary>
        [JsonProperty("loginTime")]
        [DisplayValueConvert(typeof(DateTimeValueToTextConvert))]
        [Display(Name = "登录时间", Order = 5, AutoGenerateField = false)]
        [MessagePack.Key("loginTime")]
        public DateTime? LoginTime
        {
            get;
            set;
        }

        /// <summary>
        /// 租户ID_名称
        /// 一般情况没有，特殊情况下有用，如SAAS中的租户
        /// </summary>
        public const string TenantId_Name = "TenantId";

        /// <summary>
        /// 租户ID
        /// 一般情况没有，特殊情况下有用，如SAAS中的租户
        /// </summary>
        [JsonProperty("tenantId")]
        [Display(Name = "租户ID", Order = 999, AutoGenerateField = false)]
        [MessagePack.Key("tenantId")]
        public IdT TenantId
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 基本用户信息
    /// @ 黄振东
    /// </summary>
    [MessagePackObject]
    public class BasicUserInfo : BasicUserInfo<int>
    {
    }
}
