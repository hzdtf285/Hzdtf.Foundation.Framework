using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Persistence.Contract.Standard.Data
{
    /// <summary>
    /// 持久化接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public interface IPersistence<IdT, ModelT> : IPersistenceAsync<IdT, ModelT> where ModelT : SimpleInfo<IdT>
    {
        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        ModelT Select(IdT id, string connectionId = null);

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        ModelT Select(IdT id, string[] propertyNames, string connectionId = null);

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        IList<ModelT> Select(IdT[] ids, string connectionId = null);

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        IList<ModelT> Select(IdT[] ids, string[] propertyNames, string connectionId = null);

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        int Count(IdT id, string connectionId = null);

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        int Count(string connectionId = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        IList<ModelT> Select(string connectionId = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        IList<ModelT> Select(string[] propertyNames, string connectionId = null);

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, FilterInfo filter = null, string connectionId = null);

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, string[] propertyNames, FilterInfo filter = null, string connectionId = null);

        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        /// <returns>所有字段映射集合</returns>
        string[] AllFieldMapProps();

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Insert(ModelT model, string connectionId = null);

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Insert(IList<ModelT> models, string connectionId = null);

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateById(ModelT model, string connectionId = null);

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int UpdateById(ModelT model, string[] propertyNames, string connectionId = null);

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteById(IdT id, string connectionId = null);

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int DeleteByIds(IdT[] ids, string connectionId = null);

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        int Delete(string connectionId = null);

        #endregion
    }
}
