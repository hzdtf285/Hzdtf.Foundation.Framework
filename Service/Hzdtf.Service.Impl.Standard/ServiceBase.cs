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
using Hzdtf.Utility.Standard.Utils;
using Hzdtf.Utility.Standard.Attr.ParamAttr;
using System.ComponentModel.DataAnnotations;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model.Identitys;

namespace Hzdtf.Service.Impl.Standard
{
    /// <summary>
    /// 服务基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="IdT">ID类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="PersistenceT">持久化类型</typeparam>
    public abstract partial class ServiceBase<IdT, ModelT, PersistenceT> : BasicServiceBase, IService<IdT, ModelT>, IGetObject<IPersistenceConnection> 
        where ModelT : SimpleInfo<IdT>
        where PersistenceT : IPersistence<IdT, ModelT>
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

        /// <summary>
        /// ID
        /// </summary>
        public IIdentity<IdT> Identity
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
        public event Action<ReturnInfo<ModelT>, IdT, string, BasicUserInfo<IdT>> Finding;

        /// <summary>
        /// 执行根据ID查找模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnFinding(ReturnInfo<ModelT> returnInfo, IdT id, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Finding != null)
            {
                Finding(returnInfo, id, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID查找模型后事件
        /// </summary>
        public event Action<ReturnInfo<ModelT>, IdT, string, BasicUserInfo<IdT>> Finded;

        /// <summary>
        /// 执行根据ID查找模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnFinded(ReturnInfo<ModelT> returnInfo, IdT id, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Finded != null)
            {
                Finded(returnInfo, id, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<ModelT> Find([Id] IdT id, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<ModelT> returnInfo = new ReturnInfo<ModelT>();
                BeforeFind(returnInfo, id, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFinding(returnInfo, id, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<ModelT>((reInfo) =>
                {
                    return Persistence.Select(id, connectionId);
                }, returnInfo);

                AfterFind(returnInfo, id, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFinded(returnInfo, id, connectionId, currUser);

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
        public event Action<ReturnInfo<IList<ModelT>>, IdT[], string, BasicUserInfo<IdT>> Findsing;

        /// <summary>
        /// 执行根据ID查找模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnFindsing(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Findsing != null)
            {
                Findsing(returnInfo, ids, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID查找模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, IdT[], string, BasicUserInfo<IdT>> Findsed;

        /// <summary>
        /// 执行根据ID查找模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnFindsed(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Findsed != null)
            {
                Findsed(returnInfo, ids, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID查找模型列表
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<ModelT>> Find([DisplayName2("Id集合"), ArrayNotEmpty] IdT[] ids, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<IList<ModelT>> returnInfo = new ReturnInfo<IList<ModelT>>();
                BeforeFind(returnInfo, ids, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFindsing(returnInfo, ids, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<IList<ModelT>>((reInfo) =>
                {
                    return Persistence.Select(ids, connectionId);
                }, returnInfo);

                AfterFind(returnInfo, ids, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnFindsed(returnInfo, ids, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, IdT, string, BasicUserInfo<IdT>> Existsing;

        /// <summary>
        /// 执行根据ID判断模型是否存在前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnExistsing(ReturnInfo<bool> returnInfo, IdT id, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Existsing != null)
            {
                Existsing(returnInfo, id, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT, string, BasicUserInfo<IdT>> Existsed;

        /// <summary>
        /// 执行根据ID判断模型是否存在后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnExistsed(ReturnInfo<bool> returnInfo, IdT id, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Existsed != null)
            {
                Existsed(returnInfo, id, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID判断模型是否存在
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Exists([Id] IdT id, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeExists(returnInfo, id, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnExistsing(returnInfo, id, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Count(id, connectionId) > 0;
                }, returnInfo);

                AfterExists(returnInfo, id, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnExistsed(returnInfo, id, connectionId, currUser);

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
        public event Action<ReturnInfo<int>, string, BasicUserInfo<IdT>> Counting;

        /// <summary>
        /// 执行统计模型数前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnCounting(ReturnInfo<int> returnInfo, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Counting != null)
            {
                Counting(returnInfo, connectionId, currUser);
            }
        }

        /// <summary>
        /// 统计模型数后事件
        /// </summary>
        public event Action<ReturnInfo<int>, string, BasicUserInfo<IdT>> Counted;

        /// <summary>
        /// 执行统计模型数后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnCounted(ReturnInfo<int> returnInfo, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Counted != null)
            {
                Counted(returnInfo, connectionId, currUser);
            }
        }

        /// <summary>
        /// 统计模型数
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<int> Count(string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<int> returnInfo = new ReturnInfo<int>();
                BeforeCount(returnInfo, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCounting(returnInfo, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<int>((reInfo) =>
                {
                    return Persistence.Count(connectionId);
                }, returnInfo);

                AfterCount(returnInfo, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCounted(returnInfo, connectionId, currUser);

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
        public event Action<ReturnInfo<IList<ModelT>>, string, BasicUserInfo<IdT>> Querying;

        /// <summary>
        /// 执行查询模型列表前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnQuerying(ReturnInfo<IList<ModelT>> returnInfo, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Querying != null)
            {
                Querying(returnInfo, connectionId, currUser);
            }
        }

        /// <summary>
        /// 查询模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<IList<ModelT>>, string, BasicUserInfo<IdT>> Queryed;

        /// <summary>
        /// 执行查询模型列表后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnQueryed(ReturnInfo<IList<ModelT>> returnInfo, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Queryed != null)
            {
                Queryed(returnInfo, connectionId, currUser);
            }
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<IList<ModelT>> Query(string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<IList<ModelT>> returnInfo = new ReturnInfo<IList<ModelT>>();
                BeforeQuery(returnInfo, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQuerying(returnInfo, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<IList<ModelT>>((reInfo) =>
                {
                    return Persistence.Select(connectionId);
                }, returnInfo);

                AfterQuery(returnInfo, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryed(returnInfo, connectionId, currUser);

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
        public event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, string, BasicUserInfo<IdT>> QueryPaging;

        /// <summary>
        /// 执行查询模型列表并分页前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnQueryPaging(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, FilterInfo filter, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (QueryPaging != null)
            {
                QueryPaging(returnInfo, pageIndex, pageSize, filter, connectionId, currUser);
            }
        }

        /// <summary>
        /// 查询模型列表并分页后事件
        /// </summary>
        public event Action<ReturnInfo<PagingInfo<ModelT>>, int, int, FilterInfo, string, BasicUserInfo<IdT>> QueryPaged;

        /// <summary>
        /// 执行查询模型列表并分页后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnQueryPaged(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, FilterInfo filter, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (QueryPaged != null)
            {
                QueryPaged(returnInfo, pageIndex, pageSize, filter, connectionId, currUser);
            }
        }

        /// <summary>
        /// 查询模型列表并分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="filter">筛选</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<PagingInfo<ModelT>> QueryPage([PageIndex] int pageIndex, [PageSize] int pageSize, FilterInfo filter = null, string connectionId = null, BasicUserInfo<IdT> currUser = null)
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
                BeforeQueryPage(returnInfo, pageIndex, pageSize, ref connectionId, filter, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryPaging(returnInfo, pageIndex, pageSize, filter, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<PagingInfo<ModelT>>((reInfo) =>
                {
                    return Persistence.SelectPage(pageIndex, pageSize, filter, connectionId);
                }, returnInfo);

                AfterQueryPage(returnInfo, pageIndex, pageSize, ref connectionId, filter, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnQueryPaged(returnInfo, pageIndex, pageSize, filter, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, ModelT, string, BasicUserInfo<IdT>> Adding;

        /// <summary>
        /// 执行模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnAdding(ReturnInfo<bool> returnInfo, ModelT model, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Adding != null)
            {
                Adding(returnInfo, model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 添加模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string, BasicUserInfo<IdT>> Added;

        /// <summary>
        /// 执行模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnAdded(ReturnInfo<bool> returnInfo, ModelT model, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Added != null)
            {
                Added(returnInfo, model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> Add([DisplayName2("模型"), Required, Model] ModelT model, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                SetCreateInfo(model, currUser);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeAdd(returnInfo, model, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAdding(returnInfo, model, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Insert(model, connectionId) > 0;
                }, returnInfo);

                AfterAdd(returnInfo, model, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAdded(returnInfo, model, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, IList<ModelT>, string, BasicUserInfo<IdT>> Addsing;

        /// <summary>
        /// 执行模型列表前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnAddsing(ReturnInfo<bool> returnInfo, IList<ModelT> models, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Addsing != null)
            {
                Addsing(returnInfo, models, connectionId, currUser);
            }
        }

        /// <summary>
        /// 添加模型列表后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IList<ModelT>, string, BasicUserInfo<IdT>> Addsed;

        /// <summary>
        /// 执行模型列表后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnAddsed(ReturnInfo<bool> returnInfo, IList<ModelT> models, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Addsed != null)
            {
                Addsed(returnInfo, models, connectionId, currUser);
            }
        }

        /// <summary>
        /// 添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> Add([DisplayName2("模型列表"), MultiModel] IList<ModelT> models, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            foreach (ModelT model in models)
            {
                SetCreateInfo(model, currUser);
            }

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeAdd(returnInfo, models, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAddsing(returnInfo, models, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Insert(models, connectionId) > 0;
                }, returnInfo);

                AfterAdd(returnInfo, models, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnAddsed(returnInfo, models, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, ModelT, string, BasicUserInfo<IdT>> Seting;

        /// <summary>
        /// 执行设置模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnSeting(ReturnInfo<bool> returnInfo, ModelT model, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Seting != null)
            {
                Seting(returnInfo, model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 设置模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string, BasicUserInfo<IdT>> Seted;

        /// <summary>
        /// 执行设置模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnSeted(ReturnInfo<bool> returnInfo, ModelT model, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Seted != null)
            {
                Seted(returnInfo, model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 设置模型
        /// 如果ID存在则修改，否则添加
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> Set([DisplayName2("模型"), Required, Model] ModelT model, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeSet(returnInfo, model, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnSeting(returnInfo, model, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFuncAndConnectionId<bool>((reInfo, connId) =>
                {
                    bool exists = false;
                    if (!Identity.IsEmpty(model.Id))
                    {
                        ReturnInfo<bool> existsReturnInfo = Exists(model.Id, connId, currUser);
                        if (existsReturnInfo.Failure())
                        {
                            reInfo.FromBasic(existsReturnInfo);
                            return false;
                        }
                        exists = existsReturnInfo.Data;
                    }

                    ReturnInfo<bool> re = exists ? ModifyById(model, connId, currUser) : Add(model, connId, currUser);
                    reInfo.FromBasic(re);

                    return re.Data;
                }, returnInfo, connectionId);

                AfterSet(returnInfo, model, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnSeted(returnInfo, model, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, ModelT, string, BasicUserInfo<IdT>> ModifyByIding;

        /// <summary>
        /// 执行根据ID修改模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnModifyByIding(ReturnInfo<bool> returnInfo, ModelT model, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (ModifyByIding != null)
            {
                ModifyByIding(returnInfo, model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID修改模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, ModelT, string, BasicUserInfo<IdT>> ModifyByIded;

        /// <summary>
        /// 执行根据ID修改模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnModifyByIded(ReturnInfo<bool> returnInfo, ModelT model, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (ModifyByIded != null)
            {
                ModifyByIded(returnInfo, model, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID修改模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> ModifyById([DisplayName2("模型"), Required, Model] ModelT model, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            SetModifyInfo(model, currUser);

            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeModifyById(returnInfo, model, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnModifyByIding(returnInfo, model, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.UpdateById(model, connectionId) > 0;
                }, returnInfo);

                AfterModifyById(returnInfo, model, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnModifyByIded(returnInfo, model, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, IdT, string, BasicUserInfo<IdT>> RemoveByIding;

        /// <summary>
        /// 执行根据ID移除模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnRemoveByIding(ReturnInfo<bool> returnInfo, IdT id, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (RemoveByIding != null)
            {
                RemoveByIding(returnInfo, id, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID移除模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT, string, BasicUserInfo<IdT>> RemoveByIded;

        /// <summary>
        /// 执行根据ID移除模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnRemoveByIded(ReturnInfo<bool> returnInfo, IdT id, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (RemoveByIded != null)
            {
                RemoveByIded(returnInfo, id, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> RemoveById([Id] IdT id, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeRemoveById(returnInfo, id, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIding(returnInfo, id, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.DeleteById(id, connectionId) > 0;
                }, returnInfo);

                AfterRemoveById(returnInfo, id, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIded(returnInfo, id, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, IdT[], string, BasicUserInfo<IdT>> RemoveByIdsing;

        /// <summary>
        /// 执行根据ID数组移除模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnRemoveByIdsing(ReturnInfo<bool> returnInfo, IdT[] ids, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (RemoveByIdsing != null)
            {
                RemoveByIdsing(returnInfo, ids, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID数组移除模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, IdT[], string, BasicUserInfo<IdT>> RemoveByIdsed;

        /// <summary>
        /// 执行根据ID数组移除模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnRemoveByIdsed(ReturnInfo<bool> returnInfo, IdT[] ids, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (RemoveByIdsed != null)
            {
                RemoveByIdsed(returnInfo, ids, connectionId, currUser);
            }
        }

        /// <summary>
        /// 根据ID数组移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        [Auth(CurrUserParamIndex = 2)]
        public virtual ReturnInfo<bool> RemoveByIds([DisplayName2("ID集合"), ArrayNotEmpty] IdT[] ids, string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeRemoveByIds(returnInfo, ids, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIdsing(returnInfo, ids, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.DeleteByIds(ids, connectionId) > 0;
                }, returnInfo);

                AfterRemoveByIds(returnInfo, ids, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnRemoveByIdsed(returnInfo, ids, connectionId, currUser);

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
        public event Action<ReturnInfo<bool>, string, BasicUserInfo<IdT>> Clearing;

        /// <summary>
        /// 执行清空所有模型前事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnClearing(ReturnInfo<bool> returnInfo, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Clearing != null)
            {
                Clearing(returnInfo, connectionId, currUser);
            }
        }

        /// <summary>
        /// 清空所有模型后事件
        /// </summary>
        public event Action<ReturnInfo<bool>, string, BasicUserInfo<IdT>> Cleared;

        /// <summary>
        /// 执行清空所有模型后事件
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected void OnCleared(ReturnInfo<bool> returnInfo, string connectionId, BasicUserInfo<IdT> currUser = null)
        {
            if (Cleared != null)
            {
                Cleared(returnInfo, connectionId, currUser);
            }
        }

        /// <summary>
        /// 清空所有模型
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        public virtual ReturnInfo<bool> Clear(string connectionId = null, BasicUserInfo<IdT> currUser = null)
        {
            bool isClose = false;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                isClose = true;
            }

            try
            {
                ReturnInfo<bool> returnInfo = new ReturnInfo<bool>();
                BeforeClear(returnInfo, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnClearing(returnInfo, connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                returnInfo = ExecReturnFunc<bool>((reInfo) =>
                {
                    return Persistence.Delete(connectionId) > 0;
                });

                AfterClear(returnInfo, ref connectionId, currUser);
                if (returnInfo.Failure())
                {
                    return returnInfo;
                }

                OnCleared(returnInfo, connectionId, currUser);                
                
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
            }
        }

        /// <summary>
        /// 设置创建信息
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="currUser">当前用户</param>
        protected void SetCreateInfo(ModelT model, BasicUserInfo<IdT> currUser = null)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                PersonTimeInfo<IdT> p = model as PersonTimeInfo<IdT>;
                p.SetCreateInfo(currUser);
            }
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="currUser">当前用户</param>
        protected void SetModifyInfo(ModelT model, BasicUserInfo<IdT> currUser = null)
        {
            if (model is PersonTimeInfo<IdT>)
            {
                PersonTimeInfo<IdT> p = model as PersonTimeInfo<IdT>;
                p.SetModifyInfo(currUser);
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
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeFind(ReturnInfo<ModelT> returnInfo, IdT id, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID查找模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterFind(ReturnInfo<ModelT> returnInfo, IdT id, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID集合查找模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeFind(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID集合查找模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterFind(ReturnInfo<IList<ModelT>> returnInfo, IdT[] ids, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID判断模型是否存在前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeExists(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID判断模型是否存在后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterExists(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 统计模型数前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeCount(ReturnInfo<int> returnInfo, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 统计模型数后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterCount(ReturnInfo<int> returnInfo, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 查询模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeQuery(ReturnInfo<IList<ModelT>> returnInfo, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 查询模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterQuery(ReturnInfo<IList<ModelT>> returnInfo, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 执行查询模型列表并分页前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeQueryPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 执行查询模型列表并分页后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="filter">筛选</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterQueryPage(ReturnInfo<PagingInfo<ModelT>> returnInfo, int pageIndex, int pageSize, ref string connectionId, FilterInfo filter = null, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 添加模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeAdd(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 添加模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterAdd(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 添加模型列表前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeAdd(ReturnInfo<bool> returnInfo, IList<ModelT> models, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 添加模型列表后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="models">模型列表</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterAdd(ReturnInfo<bool> returnInfo, IList<ModelT> models, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 设置模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeSet(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 设置模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterSet(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID修改模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeModifyById(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID修改模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="model">模型</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterModifyById(ReturnInfo<bool> returnInfo, ModelT model, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeRemoveById(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID移除模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="id">ID</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterRemoveById(ReturnInfo<bool> returnInfo, IdT id, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID集合移除模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        protected virtual void BeforeRemoveByIds(ReturnInfo<bool> returnInfo, IdT[] ids, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 根据ID集合移除模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="ids">ID集合</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        /// <returns>返回信息</returns>
        protected virtual void AfterRemoveByIds(ReturnInfo<bool> returnInfo, IdT[] ids, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 清空所有模型前
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void BeforeClear(ReturnInfo<bool> returnInfo, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        /// <summary>
        /// 清空所有模型后
        /// </summary>
        /// <param name="returnInfo">返回信息</param>
        /// <param name="connectionId">连接ID</param>
        /// <param name="currUser">当前用户</param>
        protected virtual void AfterClear(ReturnInfo<bool> returnInfo, ref string connectionId, BasicUserInfo<IdT> currUser = null) { }

        #endregion
    }
}
