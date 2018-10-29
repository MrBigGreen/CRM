using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:53
    /// 描 述：客户变更记录
    /// </summary>
    public class OwnershipChangeService : RepositoryFactory<OwnershipChangeEntity>, IOwnershipChangeService
    {
        private ICustomerService customerService = new CustomerService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OwnershipChangeEntity> GetPageList(string keyValue, Pagination pagination, string queryJson)
        {
            //return this.BaseRepository().FindList(pagination);
            var expression = LinqExtensions.True<OwnershipChangeEntity>();
            var queryParam = queryJson.ToJObject();
            expression = expression.And(s => s.ObjectId == keyValue);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OwnershipChangeEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OwnershipChangeEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, OwnershipChangeEntity entity)
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
        /// <summary>
        /// 保存变更归属
        /// </summary>
        /// <param name="keyValue">客户编号</param>
        /// <param name="newUserId">新</param>
        /// <param name="newUserName"></param>
        public void SaveChanae(string keyValue, string newUserId, string newUserName)
        {
            var entity = customerService.GetEntity(keyValue);
            if (entity.TraceUserId.Equals(newUserId))
            {
                throw new System.Exception("并未修改归属人");
            }
            entity.TraceUserId = newUserId;
            entity.TraceUserName = newUserName;
            customerService.SaveForm(keyValue, entity);

            OwnershipChangeEntity changeEntity = this.BaseRepository().FindEntity(s => s.ObjectId == keyValue && s.DeleteMark == 0 && !s.EndDate.HasValue);
            if (changeEntity != null)
            {
                changeEntity.EndDate = DateTime.Now;
                changeEntity.Modify(changeEntity.OwnershipChangeId);
                this.BaseRepository().Update(changeEntity);
            }
            OwnershipChangeEntity newChangeEntiy = new OwnershipChangeEntity();
            newChangeEntiy.Create();
            newChangeEntiy.ObjectId = entity.CustomerId;
            newChangeEntiy.UserID = entity.TraceUserId;
            newChangeEntiy.UserName = entity.TraceUserName;
            newChangeEntiy.StartDate = DateTime.Now;
            this.BaseRepository().Insert(newChangeEntiy);

        }


        /// <summary>
        /// 批量变更归属
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="newUserId"></param>
        /// <param name="newUserName"></param>
        /// <returns>返回错误字符串</returns>
        public string BatchChanae(string UserId, string newUserId, string newUserName)
        {
            List<string> errorList = new List<string>();
            var list = customerService.GetList(UserId).ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                var entity = list[i];
                if (entity.TraceUserId.Equals(newUserId))
                {
                    errorList.Add(" “" + entity.FullName + "”并未修改归属人");
                }
                try
                {
                    entity.TraceUserId = newUserId;
                    entity.TraceUserName = newUserName;
                    customerService.SaveForm(entity.CustomerId, entity);

                    OwnershipChangeEntity changeEntity = this.BaseRepository().FindEntity(s => s.ObjectId == entity.CustomerId && s.DeleteMark == 0 && !s.EndDate.HasValue);
                    if (changeEntity != null)
                    {
                        changeEntity.EndDate = DateTime.Now;
                        changeEntity.Modify(changeEntity.OwnershipChangeId);
                        this.BaseRepository().Update(changeEntity);
                    }
                    OwnershipChangeEntity newChangeEntiy = new OwnershipChangeEntity();
                    newChangeEntiy.Create();
                    newChangeEntiy.ObjectId = entity.CustomerId;
                    newChangeEntiy.UserID = entity.TraceUserId;
                    newChangeEntiy.UserName = entity.TraceUserName;
                    newChangeEntiy.StartDate = DateTime.Now;
                    this.BaseRepository().Insert(newChangeEntiy);
                }
                catch (Exception ex)
                {
                    errorList.Add(" “" + entity.FullName + "”" + ex.Message);
                }
                if (errorList.Count > 10)
                {//错误大于10条结束
                    break;
                }
            }

            return string.Join(",", errorList);

        }
        #endregion
    }
}
