using Offertech.Application.Code;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Application.IService.AuthorizeManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.IService.SystemManage;
using Offertech.Application.Service.AuthorizeManage;
using Offertech.Application.Service.SystemManage;
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
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:51
    /// 描 述：合同管理
    /// </summary>
    public class ContractService : RepositoryFactory<ContractEntity>, IContractService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private IAuthorizeService<ContractEntity> iauthorizeservice = new AuthorizeService<ContractEntity>();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ContractEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ContractEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }


        /// <summary>
        /// 获取合同到期数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ContractEntity> GetDeadlineList(string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM  dbo.Client_Contract WHERE CHARINDEX('OS', ContractCode)>0 AND  Deadline=dateadd( mm,1,@Today) AND  DeleteMark=0 AND EnabledMark=1 ");
                parameter.Add(DbParameters.CreateDbParameter("@Today", DateTime.Now.ToString("yyyy-MM-dd")));

                return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ContractEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }


        /// <summary>
        /// 获取欧孚销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetOfferDataBySale(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();

                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;

                strSql.Append(@"SELECT * FROM dbo.Client_Contract c WHERE DeleteMark=0 AND EnabledMark=1 AND ContractType='11' ");


                strSql.AppendFormat(@"  AND EXISTS
                                          (SELECT *
                                             FROM Base_User
                                            WHERE     UserId = c.CreateUserId
                                                  AND (ManagerId = '{0}' OR UserId = '{0}')) ", userId);

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "ContractCode":              //合同编号
                            strSql.Append(" AND CHARINDEX( @keyword,ContractCode)>0 ");
                            break;
                        case "CustomerName":            //客户名称
                        case "customername":
                            strSql.Append(" AND CHARINDEX( @keyword,CustomerName)>0 ");
                            break;
                        case "ProjectLeader":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ProjectLeader)>0 ");
                            break;
                        case "ServiceType":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ServiceType)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                if (!queryParam["ServiceType"].IsEmpty())
                {
                    var serviceType = queryParam["ServiceType"].ToString();
                    strSql.AppendFormat(" AND SERVICETYPE = '{0}'", serviceType);
                }
                if (!queryParam["SignType"].IsEmpty())
                {
                    var signType = queryParam["SignType"].ToString();
                    strSql.AppendFormat(" AND SignType = '{0}'", signType);

                }
                return iauthorizeservice.FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetBridgeDataBySale(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM dbo.Client_Contract c WHERE DeleteMark=0 AND EnabledMark=1 AND ContractType='12' ");


                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                strSql.AppendFormat(@"  AND EXISTS
                                          (SELECT *
                                             FROM Base_User
                                            WHERE     UserId = c.CreateUserId
                                                  AND (ManagerId = '{0}' OR UserId = '{0}')) ", userId);

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "ContractCode":              //合同编号
                            strSql.Append(" AND CHARINDEX( @keyword,ContractCode)>0 ");
                            break;
                        case "CustomerName":            //客户名称
                        case "customername":
                            strSql.Append(" AND CHARINDEX( @keyword,CustomerName)>0 ");
                            break;
                        case "ProjectLeader":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ProjectLeader)>0 ");
                            break;
                        case "ServiceType":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ServiceType)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                if(!queryParam["ServiceType"].IsEmpty())
                {
                    var serviceType = queryParam["ServiceType"].ToString();
                    strSql.AppendFormat(" AND SERVICETYPE = '{0}'", serviceType); 
                }
                if(!queryParam["SignType"].IsEmpty())
                {
                    var signType = queryParam["SignType"].ToString();
                    strSql.AppendFormat(" AND SignType = '{0}'", signType);

                }
                return iauthorizeservice.FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取欧孚客服合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetOfferDataByKeFu(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM dbo.Client_Contract c WHERE DeleteMark=0 AND EnabledMark=1 AND ContractType='21' ");


                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                strSql.AppendFormat(@"  AND EXISTS
                                          (SELECT *
                                             FROM Base_User
                                            WHERE     UserId = c.CreateUserId
                                                  AND (ManagerId = '{0}' OR UserId = '{0}')) ", userId);


                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "ContractCode":              //合同编号
                            strSql.Append(" AND CHARINDEX( @keyword,ContractCode)>0 ");
                            break;
                        case "CustomerName":            //客户名称
                        case "customername":
                            strSql.Append(" AND CHARINDEX( @keyword,CustomerName)>0 ");
                            break;
                        case "ProjectLeader":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ProjectLeader)>0 ");
                            break;
                        case "ServiceType":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ServiceType)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                return iauthorizeservice.FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetBridgeDataByKeFu(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM dbo.Client_Contract c WHERE DeleteMark=0 AND EnabledMark=1 AND ContractType='22' ");


                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                strSql.AppendFormat(@"  AND EXISTS
                                          (SELECT *
                                             FROM Base_User
                                            WHERE     UserId = c.CreateUserId
                                                  AND (ManagerId = '{0}' OR UserId = '{0}')) ", userId);

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "ContractCode":              //合同编号
                            strSql.Append(" AND CHARINDEX( @keyword,ContractCode)>0 ");
                            break;
                        case "CustomerName":            //客户名称
                        case "customername":
                            strSql.Append(" AND CHARINDEX( @keyword,CustomerName)>0 ");
                            break;
                        case "ProjectLeader":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ProjectLeader)>0 ");
                            break;
                        case "ServiceType":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ServiceType)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                return iauthorizeservice.FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM dbo.Client_Contract WHERE DeleteMark=0 AND EnabledMark=1  ");

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "ContractCode":              //合同编号
                            strSql.Append(" AND CHARINDEX( @keyword,ContractCode)>0 ");
                            break;
                        case "CustomerName":            //客户名称
                        case "customername":
                            strSql.Append(" AND CHARINDEX( @keyword,CustomerName)>0 ");
                            break;
                        case "ProjectLeader":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ProjectLeader)>0 ");
                            break;
                        case "ServiceType":            //项目负责人
                            strSql.Append(" AND CHARINDEX( @keyword,ServiceType)>0 ");
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

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            ContractEntity entity = new ContractEntity();
            entity.Modify(keyValue);
            entity.EnabledMark = 0;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ContractModel model)
        {
            ContractEntity entity = new ContractEntity();
            entity.ContractId = model.ContractId;
            entity.ContractMoney = model.ContractMoney;
            entity.ContractType = model.ContractType;
            entity.CustomerId = model.CustomerId;
            entity.CustomerName = model.CustomerName;
            entity.Deadline = model.Deadline;
            entity.Description = model.Description;
            entity.EffectiveDate = model.EffectiveDate;
            entity.PaymentMethod = model.PaymentMethod;
            entity.PhoneNumber = model.PhoneNumber;
            entity.ProjectLeader = model.ProjectLeader;
            entity.ServiceType = model.ServiceType.TrimEnd(new char[] { ',', ';' });
            entity.SignSubject = model.SignSubject;
            entity.SignSubjectId = model.SignSubjectId;
            entity.SignType = model.SignType;
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
            }
            else
            {
                entity.Create();
            }
            List<ContractServiceEntity> list = new List<ContractServiceEntity>();
            if (!string.IsNullOrWhiteSpace(model.ServiceTypeId))
            {
                var arrIds = model.ServiceTypeId.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                var arrNames = model.ServiceType.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arrIds.Length; i++)
                {
                    ContractServiceEntity serviceEntity = new ContractServiceEntity();
                    serviceEntity.ServiceTypeId = arrIds[i];
                    serviceEntity.ServiceType = arrNames[i];
                    serviceEntity.ContractId = entity.ContractId;
                    serviceEntity.Create();
                    list.Add(serviceEntity);
                }
            }
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(entity);
                }
                else
                {
                    //获得指定模块或者编号的单据号
                    entity.ContractCode += coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);
                    db.Insert(entity);
                    db.Insert(list);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="entity">合同主体对象</param>
        /// <param name="serviceEntityList">服务类型</param>
        public void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList)
        {
            entity.Create();
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //获得指定模块或者编号的单据号
                entity.ContractCode += coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);
                db.Insert(entity);
                db.Insert(serviceEntityList);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="entity">合同主体对象</param>
        /// <param name="serviceEntityList">服务类型</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="moduleCode">模板编码</param>
        public void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList, string moduleId, string moduleCode)
        {
            entity.Create();
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //获得指定模块或者编号的单据号
                entity.ContractCode += coderuleService.SetBillCode(entity.CreateUserId, moduleId, moduleCode, db);
                db.Insert(entity);
                db.Insert(serviceEntityList);
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
