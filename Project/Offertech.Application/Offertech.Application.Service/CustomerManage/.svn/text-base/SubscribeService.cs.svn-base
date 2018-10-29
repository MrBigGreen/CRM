using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.AuthorizeManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.AuthorizeManage;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-06-23 17:42
    /// 描 述：客户预约记录
    /// </summary>
    public class SubscribeService : RepositoryFactory<SubscribeEntity>, ISubscribeService
    {
        private IAuthorizeService<SubscribeEntity> iauthorizeservice = new AuthorizeService<SubscribeEntity>();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<SubscribeEntity> GetPageList(string customerId, Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<SubscribeEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "CustomerId":              //客户编号
                        expression = expression.And(t => t.CustomerId.Contains(keyword));
                        break;
                    case "CustomerName":            //客户名称
                        expression = expression.And(t => t.CustomerName.Contains(keyword));
                        break;
                    case "SubscribeName":             //联系人
                        expression = expression.And(t => t.SubscribeName.Contains(keyword));
                        break;
                    case "Intention":       //意向（1来 2 不来 3 待定）
                        int intention = keyword.ToInt();
                        expression = expression.And(t => t.Intention == intention);
                        break;
                    default:
                        break;
                }
            }
            if (!customerId.IsEmpty())
            { 
                expression = expression.And(t => t.CustomerId == customerId);
            }
            if (!queryParam["Intention"].IsEmpty())
            {
                int intention = queryParam["Intention"].ToInt();
                expression = expression.And(t => t.Intention == intention);
            }
            if (!queryParam["IsCome"].IsEmpty())
            {
                int IsCome = queryParam["IsCome"].ToInt();
                expression = expression.And(t => t.IsCome == IsCome);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<SubscribeEntity> GetAuthorizePageList(string customerId, Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<SubscribeEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "CustomerId":              //客户编号
                        expression = expression.And(t => t.CustomerId.Contains(keyword));
                        break;
                    case "CustomerName":            //客户名称
                        expression = expression.And(t => t.CustomerName.Contains(keyword));
                        break;
                    case "SubscribeName":             //联系人
                        expression = expression.And(t => t.SubscribeName.Contains(keyword));
                        break;
                    case "Intention":       //意向（1来 2 不来 3 待定）
                        int intention = keyword.ToInt();
                        expression = expression.And(t => t.Intention == intention);
                        break;
                    default:
                        break;
                }
            }
            if (!customerId.IsEmpty())
            {
                expression = expression.And(t => t.CustomerId == customerId);
            }
            if (!queryParam["Intention"].IsEmpty())
            {
                int intention = queryParam["Intention"].ToInt();
                expression = expression.And(t => t.Intention == intention);
            }
            if (!queryParam["IsCome"].IsEmpty())
            {
                int IsCome = queryParam["IsCome"].ToInt();
                expression = expression.And(t => t.IsCome == IsCome);
            }
            return iauthorizeservice.FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<SubscribeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SubscribeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, SubscribeEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
