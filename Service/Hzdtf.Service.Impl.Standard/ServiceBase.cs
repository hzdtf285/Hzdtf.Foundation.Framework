using Hzdtf.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Page;
using Hzdtf.Utility.Standard.Model.Return;
using System;
using System.Collections.Generic;
using Hzdtf.Persistence.Contract.Standard.Data;
using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Utility.Standard.Enums;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using System.ComponentModel.DataAnnotations;

namespace Hzdtf.Service.Impl.Standard
{
    /// <summary>
    /// 服务基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="PersistenceT">持久化类型</typeparam>
    public abstract partial class ServiceBase<ModelT, PersistenceT> : BasicServiceBase, IService<ModelT>, IGetObject<IPersistenceConnection> 
        where ModelT : SimpleInfo
        where PersistenceT : IPersistence<ModelT>
    {
        #region 属性与字段

        /// <summary>
        /// 持久化
        /// </summary>
        public PersistenceT Persistence
        {
            get;
            set;
        }

        #endregion

        #region IGetObject<IPersistenceConnection>

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns>对象</returns>
        public IPersistenceConnection Get() => Persistence;

        #endregion

        #region IService<ModelT> 接口

        #region 读取

        /// <summary>
        /// 根据ID查找模型前事件
        /// </summary>
        public event Action<ReturnInfo<ModelT>, int, string> Finding;

        /// <summary>
        /// 执行根据ID查找模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFinding(ReturnInfo<ModelT> returnInfo, int id, string connectionId)
        {
            if (Finding != null)
            {
                Finding(returnInfo, id, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型后事件
        /// </summary>
        public event Action<ReturnInfo<ModelT>, int, string> Finded;

        /// <summary>
        /// 执行根据ID查找模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFinded(ReturnInfo<ModelT> returnInfo, int id, string connectionId)
        {
            if (Finded != null)
            {
                Finded(returnInfo, id, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<ModelT> Find([Id] int id, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<ModelT> returnInfo = new ReturnInfo<ModelT>();
                BeforeFind(returnInfo, id, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFinding(returnInfo, id, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<ModelT>((reInfo) =>
                {
                    return Persistence.Select(id, connectionId);
                }, returnInfo);

                AfterFind(returnInfo, id, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFinded(returnInfo, id, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID查找模型列表前事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, int[], string> Findsing;

        /// <summary>
        /// 执行根据ID查找模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFindsing(ReturnInfo<IList<ModelT>> returnInfo, int[] ids, string connectionId)
        {
            if (Findsing != null)
            {
                Findsing(returnInfo, ids, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, int[], string> Findsed;

        /// <summary>
        /// 执行根据ID查找模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnFindsed(ReturnInfo<IList<ModelT>> returnInfo, int[] ids, string connectionId)
        {
            if (Findsed != null)
            {
                Findsed(returnInfo, ids, connectionId);
            }
        }

        /// <summary>
        /// 根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<ModelT>> Find([DisplayName2("Id集合"), ArrayNotEmpty] int[] ids, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<IList<ModelT>> returnInfo = new ReturnInfo<IList<ModelT>>();
                BeforeFind(returnInfo, ids, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFindsing(returnInfo, ids, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<IList<ModelT>>((reInfo) =>
                {
                    return Persistence.Select(ids, connectionId);
                }, returnInfo);

                AfterFind(returnInfo, ids, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFindsed(returnInfo, ids, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, int, string> Existsing;

        /// <summary>
        /// 执行根据ID判断模型是否存在前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnExistsing(ReturnInfo<bool> returnInfo, int id, string connectionId)
        {
            if (Existsing != null)
            {
                Existsing(returnInfo, id, connectionId);
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, int, string> Existsed;

        /// <summary>
        /// 执行根据ID判断模型是否存在后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnExistsed(ReturnInfo<bool> returnInfo, int id, string connectionId)
        {
            if (Existsed != null)
            {
                Existsed(returnInfo, id, connectionId);
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> Exists([Id] int id, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeExists(returnInfo, id, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnExistsing(returnInfo, id, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Count(id, connectionId) > 0;
                }, returnInfo);

                AfterExists(returnInfo, id, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnExistsed(returnInfo, id, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 统计模型数前事件
        /// </summary>
        public event Action<ReturnInfo<int>, string> Counting;

        /// <summary>
        /// 执行统计模型数前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnCounting(ReturnInfo<int> returnInfo, string connectionId)
        {
            if (Counting != null)
            {
                Counting(returnInfo, connectionId);
            }
        }

        /// <summary>
        /// 统计模型数后事件
        /// </summary>
        public event Action<ReturnInfo<int>, string> Counted;

        /// <summary>
        /// 执行统计模型数后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnCounted(ReturnInfo<int> returnInfo, string connectionId)
        {
            if (Counted != null)
            {
                Counted(returnInfo, connectionId);
            }
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<int> Count(string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<int> returnInfo = new ReturnInfo<int>();
                BeforeCount(returnInfo, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCounting(returnInfo, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<int>((reInfo) =>
                {
                    return Persistence.Count(connectionId);
                }, returnInfo);

                AfterCount(returnInfo, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCounted(returnInfo, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 查询模型列表前事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, string> Querying;

        /// <summary>
        /// 执行查询模型列表前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQuerying(ReturnInfo<IList<ModelT>> returnInfo, string connectionId)
        {
            if (Querying != null)
            {
                Querying(returnInfo, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, string> Queryed;

        /// <summary>
        /// 执行查询模型列表后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQueryed(ReturnInfo<IList<ModelT>> returnInfo, string connectionId)
        {
            if (Queryed != null)
            {
                Queryed(returnInfo, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<IList<ModelT>> Query(string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<IList<ModelT>> returnInfo = new ReturnInfo<IList<ModelT>>();
                BeforeQuery(returnInfo, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQuerying(returnInfo, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<IList<ModelT>>((reInfo) =>
                {
                    return Persistence.Select(connectionId);
                }, returnInfo);

                AfterQuery(returnInfo, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryed(returnInfo, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 查询模型列表并分页前事件
        /// </summary>
        public event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, string> QueryPaging;

        /// <summary>
        /// 执行查询模型列表并分页前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQueryPaging(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, FilterInfo filter, string connectionId)
        {
            if (QueryPaging != null)
            {
                QueryPaging(returnInfo, pageIndex, pageSize, filter, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表并分页后事件
        /// </summary>
        public event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, string> QueryPaged;

        /// <summary>
        /// 执行查询模型列表并分页后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnQueryPaged(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, FilterInfo filter, string connectionId)
        {
            if (QueryPaged != null)
            {
                QueryPaged(returnInfo, pageIndex, pageSize, filter, connectionId);
            }
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<PagingInfo<ModelT>> QueryPage([PageIndex] int pageIndex, [PageSize] int pageSize, FilterInfo filter = null, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            if (filter != null)
            {
                filter.EndCreateTime = filter.EndCreateTime.AddThisDayLastTime();
            }

            try
            {
                ReturnInfo<PagingInfo<ModelT>> returnInfo = new ReturnInfo<PagingInfo<ModelT>>();
                BeforeQueryPage(returnInfo, pageIndex, pageSize, ref connectionId, filter);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryPaging(returnInfo, pageIndex, pageSize, filter, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<PagingInfo<ModelT>>((reInfo) =>
                {
                    return Persistence.SelectPage(pageIndex, pageSize, filter, connectionId);
                }, returnInfo);

                AfterQueryPage(returnInfo, pageIndex, pageSize, ref connectionId, filter);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryPaged(returnInfo, pageIndex, pageSize, filter, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        #endregion

        #region 写入

        /// <summary>
        /// 添加模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string> Adding;

        /// <summary>
        /// 执行模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAdding(ReturnInfo<bool> returnInfo, ModelT model, string connectionId)
        {
            if (Adding != null)
            {
                Adding(returnInfo, model, connectionId);
            }
        }

        /// <summary>
        /// 添加模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string> Added;

        /// <summary>
        /// 执行模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAdded(ReturnInfo<bool> returnInfo, ModelT model, string connectionId)
        {
            if (Added != null)
            {
                Added(returnInfo, model, connectionId);
            }
        }

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> Add([DisplayName2("模型"), Required, Model] ModelT model, string connectionId = null)
        {
            if (model is PersonTimeInfo)
            {
                SetCreateInfo(model);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeAdd(returnInfo, model, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAdding(returnInfo, model, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Insert(model, connectionId) > 0;
                }, returnInfo);

                AfterAdd(returnInfo, model, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAdded(returnInfo, model, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 添加模型列表前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IList<ModelT>, string> Addsing;

        /// <summary>
        /// 执行模型列表前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAddsing(ReturnInfo<bool> returnInfo, IList<ModelT> models, string connectionId)
        {
            if (Addsing != null)
            {
                Addsing(returnInfo, models, connectionId);
            }
        }

        /// <summary>
        /// 添加模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IList<ModelT>, string> Addsed;

        /// <summary>
        /// 执行模型列表后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnAddsed(ReturnInfo<bool> returnInfo, IList<ModelT> models, string connectionId)
        {
            if (Addsed != null)
            {
                Addsed(returnInfo, models, connectionId);
            }
        }

        /// <summary>
        /// 添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> Add([DisplayName2("模型列表"), MultiModel] IList<ModelT> models, string connectionId = null)
        {
            foreach (ModelT model in models)
            {
                SetCreateInfo(model);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeAdd(returnInfo, models, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAddsing(returnInfo, models, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Insert(models, connectionId) > 0;
                }, returnInfo);

                AfterAdd(returnInfo, models, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAddsed(returnInfo, models, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 设置模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string> Seting;

        /// <summary>
        /// 执行设置模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnSeting(ReturnInfo<bool> returnInfo, ModelT model, string connectionId)
        {
            if (Seting != null)
            {
                Seting(returnInfo, model, connectionId);
            }
        }

        /// <summary>
        /// 设置模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string> Seted;

        /// <summary>
        /// 执行设置模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnSeted(ReturnInfo<bool> returnInfo, ModelT model, string connectionId)
        {
            if (Seted != null)
            {
                Seted(returnInfo, model, connectionId);
            }
        }

        /// <summary>
        /// 设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> Set([DisplayName2("模型"), Required, Model] ModelT model, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeSet(returnInfo, model, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnSeting(returnInfo, model, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
                {
                    bool exists = false;
                    if (model.Id > 0)
                    {
                        ReturnInfo<bool> existsReturnInfo = Exists(model.Id, connId);
                        if (existsReturnInfo.Failure())
                        {
                            reInfo.FromBasic(existsReturnInfo);
                            return false;
                        }
                        exists = existsReturnInfo.Data;
                    }

                    ReturnInfo<bool> re = exists ? ModifyById(model, connId) : Add(model, connId);
                    reInfo.FromBasic(re);

                    return re.Data;
                }, returnInfo, connectionId);

                AfterSet(returnInfo, model, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnSeted(returnInfo, model, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID修改模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string> ModifyByIding;

        /// <summary>
        /// 执行根据ID修改模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnModifyByIding(ReturnInfo<bool> returnInfo, ModelT model, string connectionId)
        {
            if (ModifyByIding != null)
            {
                ModifyByIding(returnInfo, model, connectionId);
            }
        }

        /// <summary>
        /// 根据ID修改模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string> ModifyByIded;

        /// <summary>
        /// 执行根据ID修改模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnModifyByIded(ReturnInfo<bool> returnInfo, ModelT model, string connectionId)
        {
            if (ModifyByIded != null)
            {
                ModifyByIded(returnInfo, model, connectionId);
            }
        }

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> ModifyById([DisplayName2("模型"), Required, Model] ModelT model, string connectionId = null)
        {
            SetModifyInfo(model);

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeModifyById(returnInfo, model, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnModifyByIding(returnInfo, model, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.UpdateById(model, connectionId) > 0;
                }, returnInfo);

                AfterModifyById(returnInfo, model, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnModifyByIded(returnInfo, model, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID移除模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, int, string> RemoveByIding;

        /// <summary>
        /// 执行根据ID移除模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIding(ReturnInfo<bool> returnInfo, int id, string connectionId)
        {
            if (RemoveByIding != null)
            {
                RemoveByIding(returnInfo, id, connectionId);
            }
        }

        /// <summary>
        /// 根据ID移除模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, int, string> RemoveByIded;

        /// <summary>
        /// 执行根据ID移除模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIded(ReturnInfo<bool> returnInfo, int id, string connectionId)
        {
            if (RemoveByIded != null)
            {
                RemoveByIded(returnInfo, id, connectionId);
            }
        }

        /// <summary>
        /// 根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> RemoveById([Id] int id, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeRemoveById(returnInfo, id, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIding(returnInfo, id, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.DeleteById(id, connectionId) > 0;
                }, returnInfo);

                AfterRemoveById(returnInfo, id, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIded(returnInfo, id, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 根据ID数组移除模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, int[], string> RemoveByIdsing;

        /// <summary>
        /// 执行根据ID数组移除模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIdsing(ReturnInfo<bool> returnInfo, int[] ids, string connectionId)
        {
            if (RemoveByIdsing != null)
            {
                RemoveByIdsing(returnInfo, ids, connectionId);
            }
        }

        /// <summary>
        /// 根据ID数组移除模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, int[], string> RemoveByIdsed;

        /// <summary>
        /// 执行根据ID数组移除模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnRemoveByIdsed(ReturnInfo<bool> returnInfo, int[] ids, string connectionId)
        {
            if (RemoveByIdsed != null)
            {
                RemoveByIdsed(returnInfo, ids, connectionId);
            }
        }

        /// <summary>
        /// 根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> RemoveByIds([DisplayName2("ID集合"), ArrayNotEmpty] int[] ids, string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeRemoveByIds(returnInfo, ids, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIdsing(returnInfo, ids, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.DeleteByIds(ids, connectionId) > 0;
                }, returnInfo);

                AfterRemoveByIds(returnInfo, ids, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIdsed(returnInfo, ids, connectionId);

                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        /// <summary>
        /// 清空所有模型前事件
        /// </summary>
        public event Action<ReturnInfo<bool>, string> Clearing;

        /// <summary>
        /// 执行清空所有模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnClearing(ReturnInfo<bool> returnInfo, string connectionId)
        {
            if (Clearing != null)
            {
                Clearing(returnInfo, connectionId);
            }
        }

        /// <summary>
        /// 清空所有模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, string> Cleared;

        /// <summary>
        /// 执行清空所有模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected void OnCleared(ReturnInfo<bool> returnInfo, string connectionId)
        {
            if (Cleared != null)
            {
                Cleared(returnInfo, connectionId);
            }
        }

        /// <summary>
        /// 清空所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        [Auth]
        public virtual ReturnInfo<bool> Clear(string connectionId = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeClear(returnInfo, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnClearing(returnInfo, connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Delete(connectionId) > 0;
                });

                AfterClear(returnInfo, ref connectionId);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCleared(returnInfo, connectionId);                
                
                return returnInfo;
            }
            catch (Exception ex)
            {
                isClose = true;
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (isClose)
                {
                    Persistence.Release(connectionId);
                }
            }
        }

        #endregion

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 执行返回函数且带有连接ID
        /// </summary>
        /// <typeparam name="OutT">输出类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<OutT> ExecReturnFuncAndConnectionId<OutT>(Func<ReturnInfo<OutT>, string, OutT> func, ReturnInfo<OutT> returnInfo = null, string connectionId = null, AccessMode accessMode = AccessMode.MASTER)
        {
            return ExecReturnFunc<OutT>((reInfo) =>
            {
                OutT result = default(OutT);
                ExecProcConnectionId((connId) =>
                {
                    result = func(reInfo, connId);
                }, connectionId, accessMode);

                return result;
            }, returnInfo);
        }

        /// <summary>
        /// 执行返回函数
        /// </summary>
        /// <typeparam name="OutT">输出类型</typeparam>
        /// <param name="func">函数</param>
        /// <param name="returnInfo">返回信息</param>
        /// <returns>返回信息</returns>
        protected ReturnInfo<OutT> ExecReturnFunc<OutT>(Func<ReturnInfo<OutT>, OutT> func, ReturnInfo<OutT> returnInfo = null)
        {
            if (returnInfo == null)
            {
                returnInfo = new ReturnInfo<OutT>();
            }

            returnInfo.Data = func(returnInfo);

            return returnInfo;
        }

        /// <summary>
        /// 执行连接ID过程
        /// 如果传过来的连接ID为空，则会创建新的连接ID，结束后会自动注释连接ID，否则不会
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="accessMode">访问模式</param>
        protected void ExecProcConnectionId(Action<string> action, string connectionId = null, AccessMode accessMode = AccessMode.MASTER)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                connectionId = Persistence.NewConnectionId(accessMode);

                try
                {
                    action(connectionId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    Persistence.Release(connectionId);
                }
            }
            else
            {
                action(connectionId);
                return;
            }
        }

        /// <summary>
        /// 设置创建信息
        /// </summary>
        /// <param name="model">模型</param>
        protected void SetCreateInfo(ModelT model)
        {
            if (model is PersonTimeInfo)
            {
                PersonTimeInfo p = model as PersonTimeInfo;
                p.SetCreateInfo();
            }
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <param name="model">模型</param>
        protected void SetModifyInfo(ModelT model)
        {
            if (model is PersonTimeInfo)
            {
                PersonTimeInfo p = model as PersonTimeInfo;
                p.SetModifyInfo();
            }
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 根据ID查找模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeFind(ReturnInfo<ModelT> returnInfo, int id, ref string connectionId) { }

        /// <summary>
        /// 根据ID查找模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterFind(ReturnInfo<ModelT> returnInfo, int id, ref string connectionId) { }

        /// <summary>
        /// 根据ID集合查找模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeFind(ReturnInfo<IList<ModelT>> returnInfo, int[] ids, ref string connectionId) { }

        /// <summary>
        /// 根据ID集合查找模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合<param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterFind(ReturnInfo<IList<ModelT>> returnInfo, int[] ids, ref string connectionId) { }

        /// <summary>
        /// 根据ID判断模型是否存在前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeExists(ReturnInfo<bool> returnInfo, int id, ref string connectionId) { }

        /// <summary>
        /// 根据ID判断模型是否存在后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterExists(ReturnInfo<bool> returnInfo, int id, ref string connectionId) { }

        /// <summary>
        /// 统计模型数前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeCount(ReturnInfo<int> returnInfo, ref string connectionId) { }

        /// <summary>
        /// 统计模型数后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterCount(ReturnInfo<int> returnInfo, ref string connectionId) { }

        /// <summary>
        /// 查询模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeQuery(ReturnInfo<IList<ModelT>> returnInfo, ref string connectionId) { }

        /// <summary>
        /// 查询模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterQuery(ReturnInfo<IList<ModelT>> returnInfo, ref string connectionId) { }

        /// <summary>
        /// 执行查询模型列表并分页前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        protected virtual void BeforeQueryPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null) { }

        /// <summary>
        /// 执行查询模型列表并分页后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        protected virtual void AfterQueryPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null) { }

        /// <summary>
        /// 添加模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeAdd(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId) { }

        /// <summary>
        /// 添加模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterAdd(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId) { }

        /// <summary>
        /// 添加模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeAdd(ReturnInfo<bool> returnInfo, IList<ModelT> models, ref string connectionId) { }

        /// <summary>
        /// 添加模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterAdd(ReturnInfo<bool> returnInfo, IList<ModelT> models, ref string connectionId) { }

        /// <summary>
        /// 设置模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeSet(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId) { }

        /// <summary>
        /// 设置模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterSet(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId) { }

        /// <summary>
        /// 根据ID修改模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeModifyById(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId) { }

        /// <summary>
        /// 根据ID修改模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterModifyById(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId) { }

        /// <summary>
        /// 根据ID移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeRemoveById(ReturnInfo<bool> returnInfo, int id, ref string connectionId) { }

        /// <summary>
        /// 根据ID移除模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterRemoveById(ReturnInfo<bool> returnInfo, int id, ref string connectionId) { }

        /// <summary>
        /// 根据ID集合移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeRemoveByIds(ReturnInfo<bool> returnInfo, int[] ids, ref string connectionId) { }

        /// <summary>
        /// 根据ID集合移除模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterRemoveByIds(ReturnInfo<bool> returnInfo, int[] ids, ref string connectionId) { }

        /// <summary>
        /// 清空所有模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void BeforeClear(ReturnInfo<bool> returnInfo, ref string connectionId) { }

        /// <summary>
        /// 清空所有模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        protected virtual void AfterClear(ReturnInfo<bool> returnInfo, ref string connectionId) { }

        #endregion
    }
}
