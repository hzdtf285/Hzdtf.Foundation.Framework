using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 数据字典子项持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IDataDictionaryItemPersistence
    {
        /// <summary>
        /// 根据数据字典ID和文本统计个数
        /// </summary>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="text">文本</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        int CountByDataItemIdAndText(int dataDictionaryId, string text, string connectionId = null);

        /// <summary>
        /// 根据数据字典ID和文本统计个数，但排除ID
        /// </summary>
        /// <param name="notId">排除ID</param>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="text">文本</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>个数</returns>
        int CountByDataItemIdAndTextNotId(int notId, int dataDictionaryId, string text, string connectionId = null);

        /// <summary>
        /// 根据数据字典ID查询数据字典子项列表
        /// </summary>
        /// <param name="dataDictionaryId">数据字典ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>数据字典子项列表</returns>
        IList<DataDictionaryItemInfo> SelectByDataDictionaryId(int dataDictionaryId, string connectionId = null);
        
        /// <summary>
        /// 根据数据字典编码查询数据字典子项列表
        /// </summary>
        /// <param name="dataDictionaryCode">数据字典编码</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>数据字典子项列表</returns>
        IList<DataDictionaryItemInfo> SelectByDataDictionaryCode(string dataDictionaryCode, string connectionId = null);
    }
}
