using Offertech.Application.Code;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.IService.AuthorizeManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.AuthorizeManage;
using Offertech.Data;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-21 16:10
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordService : RepositoryFactory<TrailRecordEntity>, ITrailRecordService
    {
        private IAuthorizeService<TrailRecordEntity> iauthorizeservice = new AuthorizeService<TrailRecordEntity>();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TrailRecordEntity> GetList(string objectId)
        {
            return this.BaseRepository().IQueryable(t => t.ObjectId.Equals(objectId)).OrderByDescending(t => t.CreateDate).ToList();
        }

        /// <summary>
        /// 获取跟进记录
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
        public IEnumerable<TrailRecordEntity> GetPageList(string objectId, Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<TrailRecordEntity>();
            var queryParam = queryJson.ToJObject();
            expression = expression.And(t => t.ObjectId == objectId && t.DeleteMark == 0 && t.EnabledMark > 0);
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "TrackContent":              //跟进内容
                        expression = expression.And(t => t.TrackContent.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TrailRecordEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">筛选条件</param>
        /// <returns></returns>
        public DataTable GetPlanData(Pagination pagination, string queryJson)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT r.*,c.FullName AS CustomerName FROM Client_TrailRecord r INNER JOIN Client_Customer c ON r.ObjectId=c.CustomerId AND r.ObjectSort=2 WHERE r.DeleteMark=0 AND r.EnabledMark=1 AND r.TrailType=1 ");
                //当前用户
                strSql.Append(" AND r.CreateUserId=@UserId ");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "CustomerName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,c.FullName)>0");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,r.Contact)>0");
                            break;
                        case "SaleStageName":       //销售阶段
                            strSql.Append(" AND CHARINDEX(@keyword,r.SaleStageName)>0");
                            break;
                        case "TrackContent":       //跟进内容
                            strSql.Append(" AND CHARINDEX(@keyword,r.TrackContent)>0");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }

                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取跟进历史列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">筛选条件</param>
        /// <returns></returns>
        public DataTable GetHistoryData(Pagination pagination, string queryJson)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;

            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM ( SELECT r.*,c.FullName AS CustomerName FROM Client_TrailRecord r INNER JOIN Client_Customer c ON r.ObjectId=c.CustomerId AND r.ObjectSort=2 ) tbl   WHERE DeleteMark=0 AND EnabledMark=1 AND TrailType=2 ");
                //当前用户
                //strSql.Append(" AND CreateUserId=@UserId ");
                //parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                strSql.AppendFormat(@"  AND EXISTS
                                          (SELECT *
                                             FROM Base_User
                                            WHERE     UserId = tbl.CreateUserId
                                                  AND (ManagerId = '{0}' OR UserId = '{0}')) ", userId);

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "CustomerName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,CustomerName)>0");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0");
                            break;
                        case "SaleStageName":       //销售阶段
                            strSql.Append(" AND CHARINDEX(@keyword,SaleStageName)>0");
                            break;
                        case "TrackContent":       //跟进内容
                            strSql.Append(" AND CHARINDEX(@keyword,TrackContent)>0");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                if (!queryParam["UserId"].IsEmpty())
                {
                    strSql.Append(" AND CreateUserId=@UserId ");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", queryParam["UserId"].ToString()));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CreateUserName=@UserName ");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                if (!queryParam["SaleStageId"].IsEmpty())
                {
                    strSql.Append(" AND SaleStageId=@SaleStageId ");
                    parameter.Add(DbParameters.CreateDbParameter("@SaleStageId", queryParam["SaleStageId"].ToString()));
                }
                if (!queryParam["FollowUpMode"].IsEmpty())
                {
                    strSql.Append(" AND FollowUpMode=@FollowUpMode ");
                    parameter.Add(DbParameters.CreateDbParameter("@FollowUpMode", queryParam["FollowUpMode"].ToString()));
                }
                if (!queryParam["Description"].IsEmpty())
                {
                    strSql.AppendFormat(" AND Description LIKE '%{0}%' ", queryParam["Description"].ToString());
                }
                return iauthorizeservice.FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        public DataTable GetAppPlanData(string userId, Pagination pagination)
        {
            try
            {
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT r.*,c.FullName AS CustomerName FROM Client_TrailRecord r INNER JOIN Client_Customer c ON r.ObjectId=c.CustomerId AND r.ObjectSort=2 WHERE r.DeleteMark=0 AND r.EnabledMark=1 AND r.TrailType=1 ");
                //当前用户
                strSql.Append(" AND r.CreateUserId=@UserId ");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));


                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
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
        public void SaveForm(string keyValue, TrailRecordEntity entity)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
                entity = this.GetEntity(entity.TrailId);
                #region 商机
                if (entity.ObjectSort == 1)
                {
                    IRepository db = new RepositoryFactory().BaseRepository();
                    db.BeginTrans();
                    try
                    {
                        ChanceEntity chanceEntity = new ChanceEntity();
                        chanceEntity.Modify(entity.ObjectId);
                        db.Update<ChanceEntity>(chanceEntity);
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }

                #endregion

                #region 客户
                else if (entity.ObjectSort == 2)
                {
                    string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                    //销售阶段
                    CustomerFollowEntity followEntity = new CustomerFollowEntity();
                    followEntity.Create();
                    followEntity.CustomerId = entity.ObjectId;
                    followEntity.SaleStageId = entity.SaleStageId;
                    followEntity.SaleStageName = entity.SaleStageName;
                    followEntity.CustomerFollowId = entity.TrailId;


                    IRepository db = new RepositoryFactory().BaseRepository();
                    db.BeginTrans();
                    try
                    {
                        CustomerEntity customerEntity = new CustomerEntity();
                        customerEntity.Modify(entity.ObjectId);
                        db.Update<CustomerEntity>(customerEntity);


                        //更新日程
                        ScheduleEntity scheduleEntity = new ScheduleEntity();
                        scheduleEntity.ScheduleState = 1;
                        scheduleEntity.Modify(entity.TrailId);
                        db.Update(scheduleEntity);

                        //删除已有的客户销售阶段
                        db.Delete<CustomerFollowEntity>(t => t.CustomerId == entity.ObjectId && t.CreateUserId == userId);
                        //创建新销售阶段
                        db.Insert<CustomerFollowEntity>(followEntity);
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }
                #endregion
            }
            else
            {
                entity.Create();

                #region 商机
                if (entity.ObjectSort == 1)
                {
                    IRepository db = new RepositoryFactory().BaseRepository();
                    db.BeginTrans();
                    try
                    {
                        ChanceEntity chanceEntity = new ChanceEntity();
                        chanceEntity.Modify(entity.ObjectId);
                        db.Update<ChanceEntity>(chanceEntity);
                        db.Insert(entity);
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }

                #endregion

                #region 客户
                else if (entity.ObjectSort == 2)
                {
                    string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                    //销售阶段
                    CustomerFollowEntity followEntity = new CustomerFollowEntity();
                    followEntity.Create();
                    followEntity.CustomerId = entity.ObjectId;
                    followEntity.SaleStageId = entity.SaleStageId;
                    followEntity.SaleStageName = entity.SaleStageName;

                    followEntity.CustomerFollowId = entity.TrailId;
                    IRepository db = new RepositoryFactory().BaseRepository();
                    db.BeginTrans();
                    try
                    {
                        CustomerEntity customerEntity = new CustomerEntity();
                        customerEntity.Modify(entity.ObjectId);
                        db.Update<CustomerEntity>(customerEntity);


                        db.Insert(entity);
                        #region 计划任务增加到日程
                        if (entity.TrailType == 1)
                        {
                            ScheduleEntity scheduleEntity = new ScheduleEntity();
                            scheduleEntity.ScheduleContent = entity.ObjectName + "-" + entity.SaleStageName + "-" + entity.TrackContent;
                            scheduleEntity.ScheduleState = 0;
                            scheduleEntity.StartDate = entity.StartTime.ToDate();
                            scheduleEntity.StartTime = Convert.ToDateTime(entity.StartTime).ToString("HHmm");
                            scheduleEntity.EndDate = entity.EndTime.ToDate();
                            scheduleEntity.EndTime = Convert.ToDateTime(entity.EndTime).ToString("HHmm");
                            scheduleEntity.IsMailAlert = 0;
                            scheduleEntity.IsMobileAlert = 0;
                            scheduleEntity.IsWeChatAlert = 0;
                            scheduleEntity.Create();
                            scheduleEntity.ScheduleId = entity.TrailId;
                            db.Insert(scheduleEntity);
                        }
                        else
                        {
                            //删除已有的客户销售阶段
                            db.Delete<CustomerFollowEntity>(t => t.CustomerId == entity.ObjectId && t.CreateUserId == userId);
                            //创建新销售阶段
                            db.Insert<CustomerFollowEntity>(followEntity);
                        }
                        #endregion
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }
                #endregion

            }
        }

        /// <summary>
        /// 移动端保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void AppSaveForm(string keyValue, TrailRecordEntity entity)
        {
            #region 修改
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                this.BaseRepository().Update(entity);
                entity = this.GetEntity(entity.TrailId);

                #region 客户
                string userId = entity.ModifyUserId;
                //销售阶段
                CustomerFollowEntity followEntity = new CustomerFollowEntity();
                followEntity.CustomerFollowId = Guid.NewGuid().ToString();
                followEntity.CreateDate = DateTime.Now;
                followEntity.CreateUserId = entity.ModifyUserId;
                followEntity.CreateUserName = entity.ModifyUserName;
                followEntity.CustomerId = entity.ObjectId;
                followEntity.SaleStageId = entity.SaleStageId;
                followEntity.SaleStageName = entity.SaleStageName;
                followEntity.CustomerFollowId = entity.TrailId;


                IRepository db = new RepositoryFactory().BaseRepository();
                db.BeginTrans();
                try
                {
                    CustomerEntity customerEntity = new CustomerEntity();
                    customerEntity.CustomerId = entity.ObjectId;
                    customerEntity.ModifyDate = DateTime.Now;
                    customerEntity.ModifyUserId = entity.ModifyUserId;
                    customerEntity.ModifyUserName = entity.ModifyUserName;
                    db.Update<CustomerEntity>(customerEntity);


                    //更新日程
                    ScheduleEntity scheduleEntity = new ScheduleEntity();
                    scheduleEntity.ScheduleState = 1;
                    scheduleEntity.Modify(entity.TrailId);
                    scheduleEntity.ScheduleId = entity.TrailId;
                    scheduleEntity.ModifyDate = DateTime.Now;
                    scheduleEntity.ModifyUserId = entity.ModifyUserId;
                    scheduleEntity.ModifyUserName = entity.ModifyUserName;
                    db.Update(scheduleEntity);

                    //删除已有的客户销售阶段
                    db.Delete<CustomerFollowEntity>(t => t.CustomerId == entity.ObjectId && t.CreateUserId == userId);
                    //创建新销售阶段
                    db.Insert<CustomerFollowEntity>(followEntity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }

                #endregion


            }
            #endregion

            #region 创建
            else
            {
                string userId = entity.CreateUserId;
                //销售阶段
                CustomerFollowEntity followEntity = new CustomerFollowEntity();
                followEntity.CustomerFollowId = Guid.NewGuid().ToString();
                followEntity.CreateDate = DateTime.Now;
                followEntity.CreateUserId = entity.CreateUserId;
                followEntity.CreateUserName = entity.CreateUserName;
                followEntity.CustomerId = entity.ObjectId;
                followEntity.SaleStageId = entity.SaleStageId;
                followEntity.SaleStageName = entity.SaleStageName;

                followEntity.CustomerFollowId = entity.TrailId;
                IRepository db = new RepositoryFactory().BaseRepository();
                db.BeginTrans();
                try
                {
                    CustomerEntity customerEntity = new CustomerEntity();
                    customerEntity.CustomerId = entity.ObjectId;
                    customerEntity.ModifyDate = DateTime.Now;
                    customerEntity.ModifyUserId = entity.CreateUserId;
                    customerEntity.ModifyUserName = entity.CreateUserName;
                    db.Update<CustomerEntity>(customerEntity);


                    db.Insert(entity);
                    #region 计划任务增加到日程
                    if (entity.TrailType == 1)
                    {
                        ScheduleEntity scheduleEntity = new ScheduleEntity();
                        scheduleEntity.ScheduleContent = entity.ObjectName + "-" + entity.SaleStageName + "-" + entity.TrackContent;
                        scheduleEntity.ScheduleState = 0;
                        scheduleEntity.StartDate = entity.StartTime.ToDate();
                        scheduleEntity.StartTime = Convert.ToDateTime(entity.StartTime).ToString("HHmm");
                        scheduleEntity.EndDate = entity.EndTime.ToDate();
                        scheduleEntity.EndTime = Convert.ToDateTime(entity.EndTime).ToString("HHmm");
                        scheduleEntity.IsMailAlert = 0;
                        scheduleEntity.IsMobileAlert = 0;
                        scheduleEntity.IsWeChatAlert = 0;
                        scheduleEntity.ScheduleId = entity.TrailId;
                        scheduleEntity.ScheduleId = Guid.NewGuid().ToString();
                        scheduleEntity.ScheduleState = 0;
                        scheduleEntity.EnabledMark = 1;
                        scheduleEntity.DeleteMark = 0;
                        scheduleEntity.CreateDate = DateTime.Now;
                        scheduleEntity.CreateUserId = entity.CreateUserId;
                        scheduleEntity.CreateUserName = entity.CreateUserId;
                        db.Insert(scheduleEntity);
                    }
                    else
                    {
                        //删除已有的客户销售阶段
                        db.Delete<CustomerFollowEntity>(t => t.CustomerId == entity.ObjectId && t.CreateUserId == userId);
                        //创建新销售阶段
                        db.Insert<CustomerFollowEntity>(followEntity);
                    }
                    #endregion
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }


            }
            #endregion
        }
        #endregion
    }
}