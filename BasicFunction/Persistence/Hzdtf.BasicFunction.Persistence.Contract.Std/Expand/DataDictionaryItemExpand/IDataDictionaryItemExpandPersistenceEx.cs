using Hzdtf.BasicFunction.Model.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.BasicFunction.Persistence.Contract.Standard
{
    /// <summary>
    /// 数据字典子项扩展持久化接口
    /// @ 黄振东
    /// </summary>
    public partial interface IDataDictionaryItemExpandPersistence
    {
        /// <summary>
        /// 根据数据字典子项ID查询数据字典子项扩展列表
        /// </summary>
        /// <param name="dataDictionaryItemId">数据字典子项ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>数据字典子项扩展列表</returns>
        IList<DataDictionaryItemExpandInfo> SelectByDataDictionaryItemId(int dataDictionaryItemId, string connectionId = null);
    }
}
