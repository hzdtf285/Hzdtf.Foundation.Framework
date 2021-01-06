﻿using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Persistence.Contract.Standard.Management;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using System.Data;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Model.Identitys;
using System.Linq;

namespace Hzdtf.Persistence.Contract.Standard.Data
{
    /// <summary>
    /// 持久化基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class PersistenceBase<IdT, ModelT> : PersistenceConnectionBase, IPersistence<IdT, ModelT> where ModelT : SimpleInfo<IdT>
    {
        #region 属性与字段

        /// <summary>
        /// ID
        /// </summary>
        public IIdentity<IdT> Identity
        {
            get;
            set;
        }

        #endregion

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        public virtual ModelT Select(IdT id, string connectionId = null)
        {
            ModelT result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(id, dbConn, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型</returns>
        public virtual ModelT Select(IdT id, string[] propertyNames, string connectionId = null)
        {
            ModelT result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(id, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID集合查询模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(IdT[] ids, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(ids, dbConn, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID集合查询模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(IdT[] ids, string[] propertyNames, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(ids, dbConn, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        public virtual int Count(IdT id, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Count(id, dbConn, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型数</returns>
        public virtual int Count(string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Count(dbConn, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(dbConn, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>模型列表</returns>
        public virtual IList<ModelT> Select(string[] propertyNames, string connectionId = null)
        {
            IList<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Select(dbConn, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames);
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public virtual PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, FilterInfo filter = null, string connectionId = null)
        {
            BeforeFilterInfo(filter);
            PagingInfo<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = SelectPage(pageIndex, pageSize, dbConn, filter, GetDbTransaction(connId, AccessMode.SLAVE));
            }, AccessMode.SLAVE);

            return result;
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>分页信息</returns>
        public virtual PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, string[] propertyNames, FilterInfo filter = null, string connectionId = null)
        {
            BeforeFilterInfo(filter);
            PagingInfo<ModelT> result = null;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = SelectPage(pageIndex, pageSize, dbConn, filter, GetDbTransaction(connId, AccessMode.SLAVE), propertyNames);
            }, AccessMode.SLAVE);

            return result;
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int Insert(ModelT model, string connectionId = null)
        {
            int result = 0;

            IdT tenantId;
            if (IsExistsTenantId(out tenantId))
            {
                SetTenantId(model, tenantId);
            }

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Insert(model, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int Insert(IList<ModelT> models, string connectionId = null)
        {
            int result = 0;

            IdT tenantId;
            if (IsExistsTenantId(out tenantId))
            {
                foreach (var m in models)
                {
                    SetTenantId(m, tenantId);
                }
            }

            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = Insert(models, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int UpdateById(ModelT model, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = UpdateById(model, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        public virtual int UpdateById(ModelT model, string[] propertyNames, string connectionId = null)
        {
            int result = 0;
            DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
            {
                result = UpdateById(model, dbConn, GetDbTransaction(connId, AccessMode.MASTER), propertyNames);
            }, AccessMode.MASTER);

            return result;
        }

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        [Transaction(ConnectionIdIndex = 1)]
        public virtual int DeleteById(IdT id, string connectionId = null)
        {
            int result = 0;
            IDictionary<string, string> slaveTables = SlaveTables();
            if (slaveTables.IsNullOrCount0())
            {
                DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
                {
                    result = DeleteById(id, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
                }, AccessMode.MASTER);

                return result;
            }

            ExecTransaction((cId) =>
            {
                DbConnectionManager.BrainpowerExecute(cId, this, (connId, dbConn) =>
                {
                    result = DeleteById(id, dbConn, GetDbTransaction(connId, AccessMode.MASTER));

                    foreach (KeyValuePair<string, string> slTable in slaveTables)
                    {
                        DeleteSlaveTableByForeignKeys(slTable.Key, slTable.Value, new IdT[] { id }, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
                    }
                }, AccessMode.MASTER);
            }, AccessMode.MASTER, connectionId: connectionId);           

            return result;
        }

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        [Transaction(ConnectionIdIndex = 1)]
        public virtual int DeleteByIds(IdT[] ids, string connectionId = null)
        {
            int result = 0;
            IDictionary<string, string> slaveTables = SlaveTables();
            if (slaveTables.IsNullOrCount0())
            {
                DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
                {
                    result = DeleteByIds(ids, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
                }, AccessMode.MASTER);

                return result;
            }

            ExecTransaction((cId) =>
            {
                DbConnectionManager.BrainpowerExecute(cId, this, (connId, dbConn) =>
                {
                    result = DeleteByIds(ids, dbConn, GetDbTransaction(connId, AccessMode.MASTER));

                    foreach (KeyValuePair<string, string> slTable in slaveTables)
                    {
                        DeleteSlaveTableByForeignKeys(slTable.Key, slTable.Value, ids, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
                    }
                }, AccessMode.MASTER);
            }, AccessMode.MASTER, connectionId: connectionId);

            return result;
        }

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>影响行数</returns>
        [Transaction(ConnectionIdIndex = 0)]
        public virtual int Delete(string connectionId = null)
        {
            int result = 0;
            IDictionary<string, string> slaveTables = SlaveTables();
            if (slaveTables.IsNullOrCount0())
            {
                DbConnectionManager.BrainpowerExecute(connectionId, this, (connId, dbConn) =>
                {
                    result = Delete(dbConn, GetDbTransaction(connId, AccessMode.MASTER));
                }, AccessMode.MASTER);

                return result;
            }

            ExecTransaction((cId) =>
            {
                DbConnectionManager.BrainpowerExecute(cId, this, (connId, dbConn) =>
                {
                    result = Delete(dbConn, GetDbTransaction(connId, AccessMode.MASTER));

                    foreach (KeyValuePair<string, string> slTable in slaveTables)
                    {
                        DeleteSlaveTable(slTable.Key, dbConn, GetDbTransaction(connId, AccessMode.MASTER));
                    }
                }, AccessMode.MASTER);
            }, AccessMode.MASTER, connectionId: connectionId);

            return result;
        }

        #endregion

        #region 需要子类重写的方法

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>模型</returns>
        protected abstract ModelT Select(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null);

        /// <summary>
        /// 根据ID集合查询模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>模型列表</returns>
        protected abstract IList<ModelT> Select(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null);

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>模型数</returns>
        protected abstract int Count(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <returns>模型数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        protected abstract int Count(IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <returns>模型列表</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        protected abstract IList<ModelT> Select(IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null);

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="filter">筛选</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>分页信息</returns>
        protected abstract PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, IDbConnection dbConnection, FilterInfo filter = null, IDbTransaction dbTransaction = null, string[] propertyNames = null);
        
        /// <summary>
        /// 所有字段映射集合
        /// </summary>
        /// <returns>所有字段映射集合</returns>
        public abstract string[] AllFieldMapProps();

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected abstract int Insert(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected abstract int Insert(IList<ModelT> models, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>影响行数</returns>
        protected abstract int UpdateById(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null);

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected abstract int DeleteById(IdT id, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected abstract int DeleteByIds(IdT[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected abstract int Delete(IDbConnection dbConnection, IDbTransaction dbTransaction = null);

        #endregion

        #endregion

        #region 虚方法

        /// <summary>
        /// 从表集合
        /// Key:表名;Value:外键字段
        /// </summary>
        /// <returns>从表集合</returns>
        protected virtual IDictionary<string, string> SlaveTables() => null;

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected virtual int DeleteSlaveTable(string table, IDbConnection dbConnection, IDbTransaction dbTransaction = null) => 0;

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected virtual int DeleteSlaveTableByForeignKeys(string table, string foreignKeyName, IdT[] foreignKeyValues, IDbConnection dbConnection, IDbTransaction dbTransaction = null) => 0;
        
        /// <summary>
        /// 过滤信息前
        /// </summary>
        /// <param name="filter">过滤</param>
        protected virtual void BeforeFilterInfo(FilterInfo filter) { }

        /// <summary>
        /// 主键是否自增
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>主键是否自增</returns>
        protected virtual bool PrimaryKeyIncr(IdT id) => Identity.IsEmpty(id);

        /// <summary>
        /// 是否存在租赁ID
        /// </summary>
        /// <returns>查询时是否需要追加租赁ID为过滤条件</returns>
        protected virtual bool IsExistsTenantId()
        {
            IdT currUserTenantId;
            return IsExistsTenantId(out currUserTenantId);
        }

        /// <summary>
        /// 是否存在租赁ID
        /// </summary>
        /// <param name="currUserTenantId">当前用户租户ID</param>
        /// <returns>是否存在租赁ID</returns>
        protected virtual bool IsExistsTenantId(out IdT currUserTenantId)
        {
            currUserTenantId = default(IdT);
            
            if (ModelContainerTenantId())
            {
                var currUser = UserTool<IdT>.GetCurrUser();
                if (currUser == null)
                {
                    throw new ArgumentNullException("当前用户为空");
                }
                if (Identity.IsEmpty(currUser.TenantId))
                {
                    throw new ArgumentException("当前用户的租户ID为空");
                }
                currUserTenantId = currUser.TenantId;

                return true;
            }

            return false;
        }

        /// <summary>
        /// 模型是否包含租户ID
        /// </summary>
        /// <returns>模型是否包含租户ID</returns>
        protected virtual bool ModelContainerTenantId() => false;

        /// <summary>
        /// 设置模型的租赁ID
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="tenantId">租赁ID</param>
        protected virtual void SetTenantId(ModelT model, IdT tenantId)
        {
            if (model == null)
            {
                return;
            }

            if (model is PersonTimeTenantInfo<ModelT>)
            {
                var temp = model as PersonTimeTenantInfo<IdT>;
                temp.TenantId = tenantId;
            }
            if (model is PersonTimeTenantInfo<IdT>)
            {
                var temp = model as PersonTimeTenantInfo<IdT>;
                temp.TenantId = tenantId;
            }
            else if (model is BasicUserInfo<IdT>)
            {
                var temp = model as BasicUserInfo<IdT>;
                temp.TenantId = tenantId;
            }
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="action">动作回调</param>
        /// <param name="accessMode">访问模式</param>
        /// <param name="level">事务等级</param>
        /// <param name="connectionId">连接ID</param>
        protected void ExecTransaction(Action<string> action, AccessMode accessMode = AccessMode.MASTER, IsolationLevel level = IsolationLevel.ReadCommitted, string connectionId = null)
        {
            if (string.IsNullOrWhiteSpace(connectionId) || GetDbTransaction(connectionId, accessMode) == null)
            {
                IDbTransaction dbTransaction = null;
                try
                {
                    if (string.IsNullOrWhiteSpace(connectionId))
                    {
                        connectionId = NewConnectionId(accessMode);
                    }
                    dbTransaction = BeginTransaction(connectionId, level);

                    action(connectionId);

                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (dbTransaction != null)
                    {
                        dbTransaction.Rollback();
                    }
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    Release(connectionId);
                }
            }
            else
            {
                action(connectionId);
            }
        }

        #endregion
    }
}
