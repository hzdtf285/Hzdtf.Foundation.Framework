using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Persistence.Contract.Standard.Data;

namespace Hzdtf.Persistence.Dapper.Standard
{
    /// <summary>
    /// Dapper持久化基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class DapperPersistenceBase<ModelT> : PersistenceBase<ModelT> where ModelT : SimpleInfo
    {
        #region 属性与字段

        /// <summary>
        /// 表
        /// </summary>
        public abstract string Table { get; }

        #endregion

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>模型</returns>
        protected override ModelT Select(int id, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            var sql = SelectSql(id, propertyNames);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Select");
            return dbConnection.QueryFirstOrDefault<ModelT>(sql, new SimpleInfo() { Id = id }, dbTransaction);
        }

        /// <summary>
        /// 根据ID集合查询模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>模型列表</returns>
        protected override IList<ModelT> Select(int[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            DynamicParameters parameters;
            var sql = SelectSql(ids, out parameters, propertyNames);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Select");
            return dbConnection.Query<ModelT>(sql, parameters, dbTransaction).AsList();
        }

        /// <summary>
        /// 根据ID统计模型数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>模型数</returns>
        protected override int Count(int id, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = CountSql(id);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Count");
            return dbConnection.ExecuteScalar<int>(sql, new SimpleInfo() { Id = id }, dbTransaction);
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <returns>模型数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        protected override int Count(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = CountSql();
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Count");
            return dbConnection.ExecuteScalar<int>(sql, dbTransaction);
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <returns>模型列表</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        protected override IList<ModelT> Select(IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            var sql = SelectSql(propertyNames: propertyNames);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Select");
            return dbConnection.Query<ModelT>(sql, dbTransaction).AsList();
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>分页信息</returns>
        protected override PagingInfo<ModelT> SelectPage(int pageIndex, int pageSize, IDbConnection dbConnection, FilterInfo filter = null, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            BeforeFilterInfo(filter);
            var source = this.GetType().Name;
            return PagingUtil.ExecPage<ModelT>(pageIndex, pageSize, () =>
            {
                DynamicParameters parameters;
                var countSql = CountByFilterSql(filter, out parameters);
                Log.TraceAsync(countSql, source: source, tags: "SelectPage");
                return dbConnection.ExecuteScalar<int>(countSql, parameters, dbTransaction);
            }, () =>
            {
                DynamicParameters parameters;
                var pageSql = SelectPageSql(pageIndex, pageSize, out parameters, filter, propertyNames);
                Log.TraceAsync(pageSql, source: source, tags: "SelectPage");
                return dbConnection.Query<ModelT>(pageSql, parameters, dbTransaction).AsList();
            });
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int Insert(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = InsertSql(model, true);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Insert");
            model.Id = dbConnection.ExecuteScalar<int>(sql, model, dbTransaction);

            return 1;
        }

        /// <summary>
        /// 插入模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int Insert(IList<ModelT> models, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = InsertSql(models, out parameters);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Insert");
            return dbConnection.Execute(sql, parameters, dbTransaction);
        }

        /// <summary>
        /// 根据ID更新模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>影响行数</returns>
        protected override int UpdateById(ModelT model, IDbConnection dbConnection, IDbTransaction dbTransaction = null, string[] propertyNames = null)
        {
            var sql = UpdateByIdSql(model, propertyNames);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "UpdateById");
            return dbConnection.Execute(sql, model, dbTransaction);
        }

        /// <summary>
        /// 根据ID删除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteById(int id, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = DeleteByIdSql(id);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "DeleteById");
            return dbConnection.Execute(sql, new SimpleInfo() { Id = id }, dbTransaction);
        }

        /// <summary>
        /// 根据ID数组删除模型
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteByIds(int[] ids, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = DeleteByIdsSql(ids, out parameters);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "DeleteByIds");
            return dbConnection.Execute(sql, parameters, dbTransaction);
        }

        /// <summary>
        /// 删除所有模型
        /// </summary>
        /// <returns>影响行数</returns>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        protected override int Delete(IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = DeleteSql();
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "Delete");
            return dbConnection.Execute(sql, dbTransaction);
        }

        #endregion

        #region 重写父类的方法

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteSlaveTable(string table, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            var sql = DeleteByTableSql(table);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "DeleteSlaveTable");
            return dbConnection.Execute(sql, dbTransaction);
        }

        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="table">从表</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <param name="dbTransaction">数据库事务</param>
        /// <returns>影响行数</returns>
        protected override int DeleteSlaveTableByForeignKeys(string table, string foreignKeyName, int[] foreignKeyValues, IDbConnection dbConnection, IDbTransaction dbTransaction = null)
        {
            DynamicParameters parameters;
            var sql = DeleteByTableAndForignKeySql(table, foreignKeyName, foreignKeyValues, out parameters);
            Log.TraceAsync(sql, source: this.GetType().Name, tags: "DeleteSlaveTableByForeignKeys");
            return dbConnection.Execute(sql, parameters, dbTransaction);
        }

        #endregion

        #region 需要子类重写的方法

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(int id, string[] propertyNames = null);

        /// <summary>
        /// 根据ID集合查询模型列表SQL语句
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="parameters">参数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(int[] ids, out DynamicParameters parameters, string[] propertyNames = null);

        /// <summary>
        /// 根据ID统计模型数SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountSql(int id);

        /// <summary>
        /// 统计模型数SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountSql(string pfx = null);

        /// <summary>
        /// 根据筛选信息统计模型数SQL语句
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected abstract string CountByFilterSql(FilterInfo filter, out DynamicParameters parameters);

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="appendFieldSqls">追加字段SQL，包含前面的,</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectSql(string pfx = null, string appendFieldSqls = null, string[] propertyNames = null);

        /// <summary>
        /// 查询模型列表并分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string SelectPageSql(int pageIndex, int pageSize, out DynamicParameters parameters, FilterInfo filter = null, string[] propertyNames = null);
        
        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="isGetInsertId">是否获取自增ID</param>
        /// <returns>SQL语句</returns>
        protected abstract string InsertSql(ModelT model, bool isGetInsertId = false);

        /// <summary>
        /// 插入模型列表SQL语句
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="parameters">动态参数</param>
        /// <returns>SQL语句</returns>
        protected abstract string InsertSql(IList<ModelT> models, out DynamicParameters parameters);

        /// <summary>
        /// 根据ID更新模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected abstract string UpdateByIdSql(ModelT model, string[] propertyNames = null);

        /// <summary>
        /// 根据ID删除模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteByIdSql(int id);

        /// <summary>
        /// 根据ID数组删除模型SQL语句
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteByIdsSql(int[] ids, out DynamicParameters parameters);

        /// <summary>
        /// 删除所有模型SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        protected abstract string DeleteSql();

        #endregion

        #endregion

        #region 虚方法

        /// <summary>
        /// 根据表名删除所有模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>SQL语句</returns>
        protected virtual string DeleteByTableSql(string table) => null;

        /// <summary>
        /// 根据表名、外键字段和外键值删除模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected virtual string DeleteByTableAndForignKeySql(string table, string foreignKeyName, int[] foreignKeyValues, out DynamicParameters parameters)
        {
            parameters = null;
            return null;
        }

        /// <summary>
        /// 获取查询的排序名称前辍，如果是主表，可以为null或空
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns>查询分页的排序名称前辍</returns>
        protected virtual string GetSelectSortNamePfx(FilterInfo filter) => filter != null ? GetSelectSortNamePfx(filter.SortName) : null;

        /// <summary>
        /// 获取查询的排序名称前辍，如果是主表，可以为null或空
        /// </summary>
        /// <param name="sortName">排序名称</param>
        /// <returns>查询分页的排序名称前辍</returns>
        protected virtual string GetSelectSortNamePfx(string sortName) => null;

        #endregion
    }
}
