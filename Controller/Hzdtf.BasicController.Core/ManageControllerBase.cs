using Hzdtf.Service.Contract.Standard;
using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Model.Return;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hzdtf.BasicController.Core
{
    /// <summary>
    /// 管理控制器基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="PageInfoT">页面信息类型</typeparam>
    /// <typeparam name="ModelT">模型类型</typeparam>
    /// <typeparam name="ServiceT">服务类型</typeparam>
    /// <typeparam name="PageFilterT">分页筛选类型</typeparam>
    public abstract partial class ManageControllerBase<PageInfoT, ModelT, ServiceT, PageFilterT> : PagingControllerBase<PageInfoT, ModelT, ServiceT, PageFilterT>
        where PageInfoT : PageInfo
        where ModelT : SimpleInfo
        where ServiceT : IService<ModelT>
        where PageFilterT : FilterInfo
    {
        /// <summary>
        /// 根据ID查找模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpGet("{id}")]
        [Function(FunCodeDefine.QUERY_CODE)]
        public virtual async Task<ReturnInfo<ModelT>> Get(int id) => await Service.FindAsync(id);

        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPost]
        [Function(FunCodeDefine.ADD_CODE)]
        public virtual async Task<ReturnInfo<bool>> Post(ModelT model) => await Service.AddAsync(model);

        /// <summary>
        /// 修改模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="model">模型</param>
        /// <returns>返回信息</returns>
        [HttpPut]
        [Function(FunCodeDefine.EDIT_CODE)]
        public virtual async Task<ReturnInfo<bool>> Put(int id, ModelT model)
        {
            if (id != 0)
            {
                model.Id = id;
            }

            return await Service.ModifyByIdAsync(model);
        }

        /// <summary>
        /// 移除模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回信息</returns>
        [HttpDelete]
        [Function(FunCodeDefine.REMOVE_CODE)]
        public virtual async Task<ReturnInfo<bool>> Delete(int id) => await Service.RemoveByIdAsync(id);

        /// <summary>
        /// 批量添加模型列表
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <returns>返回信息</returns>
        [HttpPost("BatchAdd")]
        [Function(FunCodeDefine.ADD_CODE)]
        public virtual async Task<ReturnInfo<bool>> BatchAdd(IList<ModelT> models) => await Service.AddAsync(models);

        /// <summary>
        /// 根据ID集合批量移除模型
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <returns>返回信息</returns>
        [HttpDelete("BatchRemove")]
        [Function(FunCodeDefine.REMOVE_CODE)]
        public virtual async Task<ReturnInfo<bool>> BatchRemove(int[] ids) => await Service.RemoveByIdsAsync(ids);
    }
}
