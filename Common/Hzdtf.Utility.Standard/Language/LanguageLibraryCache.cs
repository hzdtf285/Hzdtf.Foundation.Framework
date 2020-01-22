using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Cache;
using Hzdtf.Utility.Standard.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Hzdtf.Utility.Standard.Utils;

namespace Hzdtf.Utility.Standard.Language
{
    /// <summary>
    /// 语系库缓存
    /// @ 黄振东
    /// </summary>
    [Inject]
    public class LanguageLibraryCache : SingleTypeLocalMemoryBase<string, LanguageInfo>, IReaderAll<LanguageInfo>, ILanguageLibrary
    {
        #region 属性与字段

        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly static IDictionary<string, LanguageInfo> dicCache = new Dictionary<string, LanguageInfo>();

        /// <summary>
        /// 编码映射ID字典
        /// </summary>
        private readonly static IDictionary<string, int> dicCodeMapId = new Dictionary<string, int>();

        /// <summary>
        /// 同步缓存字典
        /// </summary>
        private readonly static object syncDicCache = new object();

        /// <summary>
        /// 同步映射ID字典
        /// </summary>
        private readonly static object syncDicCodeMapId = new object();

        /// <summary>
        /// 原生语系库读取
        /// </summary>
        public IReaderAll<LanguageInfo> ProtoLanguageLibraryReader
        {
            get;
            set;
        }

        #endregion

        #region IReaderAll<LanguageInfo> 接口

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns>数据</returns>
        public IList<LanguageInfo> ReaderAll()
        {
            if (dicCache.Count == 0)
            {
                var list = ProtoLanguageLibraryReader.ReaderAll();
                if (list.IsNullOrCount0())
                {
                    return null;
                }

                lock (syncDicCache)
                {
                    foreach (var m in list)
                    {
                        dicCache.Add(m.Key, m);
                    }
                }

                return list;
            }

            return dicCache.Values.ToList();
        }

        #endregion

        #region ILanguageLibrary 接口

        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override LanguageInfo Get(string key)
        {
            if (Exists(key))
            {
                return dicCache[key];
            }
            else
            {
                ReaderAll();

                if (Exists(key))
                {
                    return dicCache[key];
                }

                return null;
            }
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns>缓存</returns>
        protected override IDictionary<string, LanguageInfo> GetCache() => dicCache;

        /// <summary>
        /// 获取同步的缓存对象，是为了线程安全
        /// </summary>
        /// <returns>同步的缓存对象</returns>
        protected override object GetSyncCache() => syncDicCache;

        #endregion
    }
}
