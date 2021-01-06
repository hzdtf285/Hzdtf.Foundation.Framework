﻿using Hzdtf.Persistence.Contract.Standard.Data;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Service.Impl.Standard
{
    /// <summary>
    /// 服务基类
    /// @ 黄振东
    /// </summary>
    public abstract partial class ServiceBase<IdT, ModelT, PersistenceT> 
        where ModelT : SimpleInfo<IdT>
        where PersistenceT : IPersistence<IdT, ModelT>
    {
        #region 读取

        /// <summary>
        /// 异步根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<ModelT>> FindAsync(IdT id, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<ModelT>>(() =>
            {
                return Find(id, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步根据ID集合查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<IList<ModelT>>> FindAsync(IdT[] ids, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<IList<ModelT>>>(() =>
            {
                return Find(ids, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> ExistsAsync(IdT id, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Exists(id, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<int>> CountAsync(string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<int>>(() =>
            {
                return Count(connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<IList<ModelT>>> QueryAsync(string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<IList<ModelT>>>(() =>
            {
                return Query(connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步执行查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<PagingInfo<ModelT>>> QueryPageAsync(int pageIndex, int pageSize, FilterInfo filter = null, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<PagingInfo<ModelT>>>(() =>
            {
                return QueryPage(pageIndex, pageSize, filter, connectionId, currUser);
            });
        }

        #endregion

        #region 写入

        /// <summary>
        /// 异步添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> AddAsync(ModelT model, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Add(model, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> AddAsync(IList<ModelT> models, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Add(models, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> SetAsync(ModelT model, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Set(model, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> ModifyByIdAsync(ModelT model, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return ModifyById(model, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> RemoveByIdAsync(IdT id, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return RemoveById(id, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步根据ID集合移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> RemoveByIdsAsync(IdT[] ids, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return RemoveByIds(ids, connectionId, currUser);
            });
        }

        /// <summary>
        /// 异步清空所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息任务</returns>
        public virtual async Task<ReturnInfo<bool>> ClearAsync(string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            return await Task.Run<ReturnInfo<bool>>(() =>
            {
                return Clear(connectionId, currUser);
            });
        }

        #endregion
    }
}
