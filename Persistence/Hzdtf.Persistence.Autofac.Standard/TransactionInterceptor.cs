using Castle.DynamicProxy;
using Hzdtf.Persistence.Contract.Standard.Basic;
using Hzdtf.Utility.Standard.Attr;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Hzdtf.Utility.Standard.Data;
using Hzdtf.Autofac.Extend.Standard;
using Hzdtf.Utility.Standard.Model.Return;
using Hzdtf.Autofac.Extend.Standard.Intercepteds;

namespace Hzdtf.Persistence.Autofac.Standard
{
    /// <summary>
    /// 事务拦截器
    /// connectionId是关键点，引用方法必须要指定该参数的索引位置
    /// 此拦截器会根据索引位置获取到connectionId，如果之前有设置值，则在本拦截器里不会开启新的事务
    /// 开启了新事务后，会执行业务方法会把新创建的connectionId传入到业务方法对应参数里
    /// 如果业务方法里有抛出异常或返回值为ReturnInfo.Code失败，则会回滚
    /// @ 黄振东
    /// </summary>
    public class TransactionInterceptor : AttrInterceptorBase<TransactionAttribute>
    {
        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="basicReturn">基本返回</param>
        /// <param name="invocation">拦截参数</param>
        /// <param name="attr">特性</param>
        /// <param name="isExecProceeded">是否已执行</param>
        protected override void Intercept(BasicReturnInfo basicReturn, IInvocation invocation, TransactionAttribute attr, out bool isExecProceeded)
        {
            isExecProceeded = true;
            BasicReturnInfo returnInfo = new BasicReturnInfo();
            object connId = null;
            if (attr.ConnectionIdIndex == -1)
            {
                connId = invocation.GetArgumentValue(attr.ConnectionIdIndex);
            }
            IGetObject<IPersistenceConnection> getPerConn = AutofacTool.Resolve(invocation.TargetType) as IGetObject<IPersistenceConnection>;
            if (getPerConn == null)
            {
                basicReturn.SetFailureMsg("未实现IGetObject<IPersistenceConnection>接口");
                return;
            }

            IPersistenceConnection perConn = getPerConn.Get();
            string connectionId = null;
            // 当有连接ID传过来，判断是否存在该连接事务，存在则不开启新事务
            if (connId != null)
            {
                string connIdStr = connId.ToString();
                if (perConn.GetDbTransaction(connIdStr) != null)
                {
                    invocation.Proceed();
                    return;
                }

                connectionId = connIdStr;
            }
            else
            {
                connectionId = perConn.NewConnectionId();
            }
            IDbTransaction dbTransaction = null;
            try
            {
                dbTransaction = perConn.BeginTransaction(connectionId, attr.Level);
                
                invocation.SetArgumentValue(attr.ConnectionIdIndex, connectionId);

                invocation.Proceed();

                // 如果返回值为失败标识，也回滚
                Type returnType = invocation.Method.ReturnType;
                if (invocation.Method.ReturnType.IsReturnType())
                {
                    BasicReturnInfo basicReturnInfo = invocation.ReturnValue as BasicReturnInfo;
                    if (basicReturnInfo.Failure())
                    {
                        dbTransaction.Rollback();

                        return;
                    }
                }

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
                perConn.Release(connectionId);
            }
        }
    }
}
