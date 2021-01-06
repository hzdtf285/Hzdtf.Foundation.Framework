using Hzdtf.Utility.Standard.Attr;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Model.Identitys
{
    /// <summary>
    /// 雪花算法ID
    /// 分布式全局唯一ID
    /// </summary>
    [Inject]
    public class SnowflakeId : IIdentity<long>
    {
        /// <summary>
        /// ID工作
        /// </summary>
        private static IdWorker worker;

        /// <summary>
        /// 同步ID工作
        /// </summary>
        private readonly static object syncWorker = new object();

        /// <summary>
        /// 初始化，如果ID工作对象已初始化过，则会忽略
        /// </summary>
        /// <param name="workerId">工作ID</param>
        /// <param name="datacenterId">数据中心ID</param>
        /// <param name="sequence">序列</param>
        public static void Init(long workerId, long datacenterId, long sequence = 0)
        {
            if (worker != null)
            {
                return;
            }

            lock (syncWorker)
            {
                if (worker == null)
                {
                    worker = new IdWorker(workerId, datacenterId, sequence);
                }
            }
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="idStr">ID字符串</param>
        /// <returns>ID值</returns>
        public long ConvertTo(string idStr) => Convert.ToInt64(idStr);

        /// <summary>
        /// 新建
        /// </summary>
        /// <returns>ID值</returns>
        public long New()
        {
            Init(1, 1);

            return worker.NextId();
        }

        /// <summary>
        /// 默认
        /// </summary>
        /// <returns>ID值</returns>
        public long Default() => 0;

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>是否为空</returns>
        public bool IsEmpty(long id) => id == Default();

        /// <summary>
        /// 获取值SQL
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>值SQL</returns>
        public string GetValueSql(long id) => id.ToString();
    }
}
