using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Service.Contract.Standard
{
    /// <summary>
    /// 异步服务接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public partial interface IServiceAsync<ModelT> where ModelT : SimpleInfo
    {
        #region 读取

        /// <summary>
        /// 异步根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<ModelT>> FindAsync(int id, string connectionId = null);

        /// <summary>
        /// 异步根据ID集合查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<IList<ModelT>>> FindAsync(int[] ids, string connectionId = null);

        /// <summary>
        /// 异步根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ExistsAsync(int id, string connectionId = null);

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<int>> CountAsync(string connectionId = null);

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<IList<ModelT>>> QueryAsync(string connectionId = null);

        /// <summary>
        /// 异步执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<PagingInfo<ModelT>>> QueryPageAsync(int pageIndex, int pageSize, FilterInfo filter = null, string connectionId = null);

        #endregion

        #region 写入

        /// <summary>
        /// 异步添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> AddAsync(ModelT model, string connectionId = null);

        /// <summary>
        /// 异步添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> AddAsync(IList<ModelT> models, string connectionId = null);

        /// <summary>
        /// 异步设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> SetAsync(ModelT model, string connectionId = null);

        /// <summary>
        /// 异步根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ModifyByIdAsync(ModelT model, string connectionId = null);

        /// <summary>
        /// 异步根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> RemoveByIdAsync(int id, string connectionId = null);

        /// <summary>
        /// 异步清空所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息任务</returns>
        Task<ReturnInfo<bool>> ClearAsync(string connectionId = null);

        #endregion
    }
}
