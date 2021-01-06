using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.Standard.Data
{
    /// <summary>
    /// 持久化异步接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public interface IPersistenceAsync<IdT, ModelT> : IPersistenceConnection where ModelT : SimpleInfo<IdT>
    {
        #region 读取方法

        /// <summary>
        /// 异步根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        Task<ModelT> SelectAsync(IdT id, ref string connectionId);

        /// <summary>
        /// 异步根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        Task<ModelT> SelectAsync(IdT id, string[] propertyNames, ref string connectionId);

        /// <summary>
        /// 异步根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        Task<IList<ModelT>> SelectAsync(IdT[] ids, ref string connectionId);

        /// <summary>
        /// 异步根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        Task<IList<ModelT>> SelectAsync(IdT[] ids, string[] propertyNames, ref string connectionId);

        /// <summary>
        /// 异步根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数任务</returns>
        Task<int> CountAsync(IdT id, ref string connectionId);

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数任务</returns>
        Task<int> CountAsync(ref string connectionId);

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表任务</returns>
        Task<IList<ModelT>> SelectAsync(ref string connectionId);

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表任务</returns>
        Task<IList<ModelT>> SelectAsync(string[] propertyNames, ref string connectionId);

        /// <summary>
        /// 异步查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <returns>分页信息任务</returns>
        Task<PagingInfo<ModelT>> SelectPageAsync(int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null);

        /// <summary>
        /// 异步查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <returns>分页信息任务</returns>
        Task<PagingInfo<ModelT>> SelectPageAsync(int pageIndex, int pageSize, string[] propertyNames, ref string connectionId, FilterInfo filter = null);

        #endregion

        #region 写入方法

        /// <summary>
        /// 异步插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> InsertAsync(ModelT model, ref string connectionId);

        /// <summary>
        /// 异步插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> InsertAsync(IList<ModelT> models, ref string connectionId);

        /// <summary>
        /// 异步根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> UpdateByIdAsync(ModelT model, ref string connectionId);

        /// <summary>
        /// 异步根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> UpdateByIdAsync(ModelT model, string[] propertyNames, ref string connectionId);

        /// <summary>
        /// 异步根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> DeleteByIdAsync(IdT id, ref string connectionId);

        /// <summary>
        /// 异步根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> DeleteByIdsAsync(IdT[] ids, ref string connectionId);

        /// <summary>
        /// 异步删除所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        Task<int> DeleteAsync(ref string connectionId);

        #endregion
    }
}
