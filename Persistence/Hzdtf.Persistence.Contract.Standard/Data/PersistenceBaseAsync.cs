using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hzdtf.Persistence.Contract.Standard.Data
{
    /// <summary>
    /// 异步持久化基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class PersistenceBase<ModelT> where ModelT : SimpleInfo
    {
        #region 读取方法

        /// <summary>
        /// 异步根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        public virtual Task<ModelT> SelectAsync(int id, ref string connectionId)
        {
            Task<ModelT> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<ModelT>(connId, isClose, dbConn, () =>
                {
                    return Select(id, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        public virtual Task<ModelT> SelectAsync(int id, string[] propertyNames, ref string connectionId)
        {
            Task<ModelT> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<ModelT>(connId, isClose, dbConn, () =>
                {
                    return Select(id, dbConn, propertyNames: propertyNames);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(int[] ids, ref string connectionId)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, () =>
                {
                    return Select(ids, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(int[] ids, string[] propertyNames, ref string connectionId)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, () =>
                {
                    return Select(ids, dbConn, propertyNames: propertyNames);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数任务</returns>
        public virtual Task<int> CountAsync(int id, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return Count(id, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数任务</returns>
        public virtual Task<int> CountAsync(ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return Count(dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(ref string connectionId)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, () =>
                {
                    return Select(dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表任务</returns>
        public virtual Task<IList<ModelT>> SelectAsync(string[] propertyNames, ref string connectionId)
        {
            Task<IList<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<IList<ModelT>>(connId, isClose, dbConn, () =>
                {
                    return Select(dbConn, propertyNames: propertyNames);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <returns>分页信息任务</returns>
        public virtual Task<PagingInfo<ModelT>> SelectPageAsync(int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null)
        {
            Task<PagingInfo<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<PagingInfo<ModelT>>(connId, isClose, dbConn, () =>
                {
                    return SelectPage(pageIndex, pageSize, dbConn, filter);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <returns>分页信息任务</returns>
        public virtual Task<PagingInfo<ModelT>> SelectPageAsync(int pageIndex, int pageSize, string[] propertyNames, ref string connectionId, FilterInfo filter = null)
        {
            Task<PagingInfo<ModelT>> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<PagingInfo<ModelT>>(connId, isClose, dbConn, () =>
                {
                    return SelectPage(pageIndex, pageSize, dbConn, filter, propertyNames: propertyNames);
                });
            });

            return task;
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 异步插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> InsertAsync(ModelT model, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return Insert(model, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> InsertAsync(IList<ModelT> models, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return Insert(models, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> UpdateByIdAsync(ModelT model, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return UpdateById(model, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> UpdateByIdAsync(ModelT model, string[] propertyNames, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return UpdateById(model, dbConn, propertyNames: propertyNames);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> DeleteByIdAsync(int id, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return DeleteById(id, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> DeleteByIdsAsync(int[] ids, ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return DeleteByIds(ids, dbConn);
                });
            });

            return task;
        }

        /// <summary>
        /// 异步删除所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数任务</returns>
        public virtual Task<int> DeleteAsync(ref string connectionId)
        {
            Task<int> task = null;
            DbConnectionManager.BrainpowerExecuteAsync(ref connectionId, this, (connId, isClose, dbConn) =>
            {
                task = ExecAsync<int>(connId, isClose, dbConn, () =>
                {
                    return Delete(dbConn);
                });
            });

            return task;
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="connectionId">连接ID</param>
        /// <param name="isClose">是否关闭</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="func">函数</param>
        /// <returns>结果任务</returns>
        protected async Task<TResult> ExecAsync<TResult>(string connectionId, bool isClose, IDbConnection dbConnection, Func<TResult> func)
        {
            return await Task.Run<TResult>(() =>
            {
                TResult result = func();
                if (isClose)
                {
                    DbConnectionManager.Release(connectionId, dbConnection);
                }

                return result;
            });
        }

        #endregion
    }
}
