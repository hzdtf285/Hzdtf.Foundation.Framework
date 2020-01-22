using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;

namespace Hzdtf.Service.Contract.Standard
{
    /// <summary>
    /// 服务接口
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public partial interface IService<ModelT> : IServiceAsync<ModelT> where ModelT : SimpleInfo
    {
        #region 读取 

        /// <summary>
        /// 根据ID查找模型前事件
        /// </summary>
        event Action<ReturnInfo<ModelT>, int, string> Finding;

        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<ModelT> Find(int id, string connectionId = null);

        /// <summary>
        /// 根据ID查找模型后事件
        /// </summary>
        event Action<ReturnInfo<ModelT>, int, string> Finded;

        /// <summary>
        /// 根据ID查找模型列表前事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, int[], string> Findsing;

        /// <summary>
        /// 根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ModelT>> Find(int[] ids, string connectionId = null);

        /// <summary>
        /// 根据ID查找模型列表后事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, int[], string> Findsed;

        /// <summary>
        /// 根据ID判断模型是否存在前事件
        /// </summary>
        event Action<ReturnInfo<bool>, int, string> Existsing;

        /// <summary>
        /// 根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Exists(int id, string connectionId = null);

        /// <summary>
        /// 根据ID判断模型是否存在后事件
        /// </summary>
        event Action<ReturnInfo<bool>, int, string> Existsed;

        /// <summary>
        /// 统计模型数前事件
        /// </summary>
        event Action<ReturnInfo<int>, string> Counting;

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<int> Count(string connectionId = null);

        /// <summary>
        /// 统计模型数后事件
        /// </summary>
        event Action<ReturnInfo<int>, string> Counted;

        /// <summary>
        /// 查询模型列表前事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, string> Querying;

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<IList<ModelT>> Query(string connectionId = null);

        /// <summary>
        /// 查询模型列表后事件
        /// </summary>
        event Action<ReturnInfo<IList<ModelT>>, string> Queryed;

        /// <summary>
        /// 执行查询模型列表并分页前事件
        /// </summary>
        event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, string> QueryPaging;

        /// <summary>
        /// 执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<PagingInfo<ModelT>> QueryPage(int pageIndex, int pageSize, FilterInfo filter = null, string connectionId = null);
        
        /// <summary>
        /// 执行查询模型列表并分页后事件
        /// </summary>
        event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, string> QueryPaged;

        #endregion

        #region 写入

        /// <summary>
        /// 添加模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, string> Adding;

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Add(ModelT model, string connectionId = null);

        /// <summary>
        /// 添加模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, string> Added;

        /// <summary>
        /// 添加模型列表前事件
        /// </summary>
        event Action<ReturnInfo<bool>, IList<ModelT>, string> Addsing;

        /// <summary>
        /// 添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Add(IList<ModelT> models, string connectionId = null);

        /// <summary>
        /// 添加模型列表后事件
        /// </summary>
        event Action<ReturnInfo<bool>, IList<ModelT>, string> Addsed;

        /// <summary>
        /// 设置模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, string> Seting;

        /// <summary>
        /// 设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Set(ModelT model, string connectionId = null);

        /// <summary>
        /// 设置模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, string> Seted;

        /// <summary>
        /// 根据ID修改模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, string> ModifyByIding;

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> ModifyById(ModelT model, string connectionId = null);

        /// <summary>
        /// 根据ID修改模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, ModelT, string> ModifyByIded;

        /// <summary>
        /// 根据ID移除模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, int, string> RemoveByIding;

        /// <summary>
        /// 根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> RemoveById(int id, string connectionId = null);

        /// <summary>
        /// 根据ID移除模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, int, string> RemoveByIded;

        /// <summary>
        /// 根据ID数组移除模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, int[], string> RemoveByIdsing;

        /// <summary>
        /// 根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> RemoveByIds(int[] ids, string connectionId = null);

        /// <summary>
        /// 根据ID数组移除模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, int[], string> RemoveByIdsed;

        /// <summary>
        /// 清空所有模型前事件
        /// </summary>
        event Action<ReturnInfo<bool>, string> Clearing;

        /// <summary>
        /// 清空所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        ReturnInfo<bool> Clear(string connectionId = null);

        /// <summary>
        /// 清空所有模型后事件
        /// </summary>
        event Action<ReturnInfo<bool>, string> Cleared;

        #endregion
    }
}
