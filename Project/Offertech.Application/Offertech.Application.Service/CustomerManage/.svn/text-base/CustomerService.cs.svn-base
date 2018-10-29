using Offertech.Application.Code;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.IService.SystemManage;
using Offertech.Application.Service.SystemManage;
using Offertech.Data.Repository;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using Offertech.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.Common;
using Offertech.Data;
using Offertech.Application.IService.AuthorizeManage;
using Offertech.Application.Service.AuthorizeManage;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class CustomerService : RepositoryFactory<CustomerEntity>, ICustomerService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private IAuthorizeService<CustomerEntity> iauthorizeservice = new AuthorizeService<CustomerEntity>();


        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList()
        {
            return this.BaseRepository().IQueryable().Where(s => s.DeleteMark == 0 && s.EnabledMark > 0).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetMyList()
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            return this.BaseRepository().IQueryable().Where(s => s.TraceUserId == userId && s.DeleteMark == 0 && s.EnabledMark > 0).OrderBy(t => t.FullName).ToList();
        }
        /// <summary>
        /// 获取客户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList(string userId)
        {
            return this.BaseRepository().IQueryable().Where(s => s.TraceUserId == userId && s.DeleteMark == 0 && s.EnabledMark > 0).OrderBy(t => t.FullName).ToList();
        }

        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMyData(string queryJson)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("select * from (SELECT c.*,'0' isShare FROM dbo.Client_Customer c WHERE TraceUserId=@UserId AND c.DeleteMark=0 AND c.EnabledMark>0 ");
                strSql.Append(" UNION ");
                strSql.Append("SELECT c.*,'1' isShare FROM dbo.Client_Customer c WHERE c.DeleteMark=0 AND c.EnabledMark>0  AND EXISTS(SELECT * FROM Client_Share WHERE  DeleteMark=0 AND EnabledMark=1  AND c.CustomerId=ObjectId  AND ToUserID=@UserId ) ) tbl where 1=1 ");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //客户编号
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //跟进人员
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                #region 高级查询条件
                if (!queryParam["EnCode"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@EnCode,EnCode)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@EnCode", queryParam["EnCode"].ToString()));
                }
                if (!queryParam["FullName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@FullName,FullName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@FullName", queryParam["FullName"].ToString()));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@Contact,Contact)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@Contact", queryParam["Contact"].ToString()));
                }
                if (!queryParam["TraceUserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@TraceUserName,TraceUserName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@TraceUserName", queryParam["TraceUserName"].ToString()));
                }
                if (!queryParam["SaleStageId"].IsEmpty())
                {//销售阶段
                    strSql.Append(" AND EXISTS(SELECT CustomerFollowId FROM Client_CustomerFollow WHERE  DeleteMark=0 AND EnabledMark=1  AND tbl.CustomerId=CustomerId AND SaleStageId=@SaleStageId  AND CreateUserId=@UserId )");
                    parameter.Add(DbParameters.CreateDbParameter("@SaleStageId", queryParam["SaleStageId"].ToString()));
                }
                if (!queryParam["CustLevelId"].IsEmpty())
                {
                    strSql.Append(" AND CustLevelId=@CustLevelId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustLevelId", queryParam["CustLevelId"].ToString()));
                }
                if (!queryParam["NatureCode"].IsEmpty())
                {
                    strSql.Append(" AND NatureCode=@NatureCode");
                    parameter.Add(DbParameters.CreateDbParameter("@NatureCode", queryParam["NatureCode"].ToString()));
                }
                if (!queryParam["RegisterCapital"].IsEmpty())
                {
                    strSql.Append(" AND RegisterCapital=@RegisterCapital");
                    parameter.Add(DbParameters.CreateDbParameter("@RegisterCapital", queryParam["RegisterCapital"].ToString()));
                }
                if (!queryParam["SalesRevenue"].IsEmpty())
                {
                    strSql.Append(" AND SalesRevenue=@SalesRevenue");
                    parameter.Add(DbParameters.CreateDbParameter("@SalesRevenue", queryParam["SalesRevenue"].ToString()));
                }
                if (!queryParam["CustIndustryId"].IsEmpty())
                {
                    strSql.Append(" AND CustIndustryId=@CustIndustryId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustIndustryId", queryParam["CustIndustryId"].ToString()));
                }
                if (!queryParam["CompanySize"].IsEmpty())
                {
                    strSql.Append(" AND CompanySize=@CompanySize");
                    parameter.Add(DbParameters.CreateDbParameter("@CompanySize", queryParam["CompanySize"].ToString()));
                }
                if (!queryParam["CityId"].IsEmpty())
                {
                    strSql.Append(" AND CityId=@CityId");
                    parameter.Add(DbParameters.CreateDbParameter("@CityId", queryParam["CityId"].ToString()));
                }
                #endregion
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMyPageData(Pagination pagination, string queryJson)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();

                strSql.Append("select * from (SELECT c.*,'0' isShare FROM dbo.Client_Customer c WHERE TraceUserId=@UserId AND c.DeleteMark=0 AND c.EnabledMark>0 ");
                strSql.Append(" UNION ");
                strSql.Append("SELECT c.*,'1' isShare FROM dbo.Client_Customer c WHERE c.DeleteMark=0 AND c.EnabledMark>0  AND EXISTS(SELECT * FROM Client_Share WHERE  DeleteMark=0 AND EnabledMark=1  AND c.CustomerId=ObjectId  AND ToUserID=@UserId ) ) tbl where 1=1 ");

                //当前用户和对应的下级用户
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //客户编号
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //跟进人员
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                #region 高级查询条件
                if (!queryParam["EnCode"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@EnCode,EnCode)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@EnCode", queryParam["EnCode"].ToString()));
                }
                if (!queryParam["FullName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@FullName,FullName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@FullName", queryParam["FullName"].ToString()));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@Contact,Contact)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@Contact", queryParam["Contact"].ToString()));
                }
                if (!queryParam["TraceUserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@TraceUserName,TraceUserName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@TraceUserName", queryParam["TraceUserName"].ToString()));
                }
                if (!queryParam["SaleStageId"].IsEmpty())
                {//销售阶段
                    strSql.Append(" AND EXISTS(SELECT CustomerFollowId FROM Client_CustomerFollow WHERE  DeleteMark=0 AND EnabledMark=1  AND tbl.CustomerId=CustomerId AND SaleStageId=@SaleStageId  AND CreateUserId=@UserId )");
                    parameter.Add(DbParameters.CreateDbParameter("@SaleStageId", queryParam["SaleStageId"].ToString()));
                }
                if (!queryParam["CustLevelId"].IsEmpty())
                {
                    strSql.Append(" AND CustLevelId=@CustLevelId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustLevelId", queryParam["CustLevelId"].ToString()));
                }
                if (!queryParam["NatureCode"].IsEmpty())
                {
                    strSql.Append(" AND NatureCode=@NatureCode");
                    parameter.Add(DbParameters.CreateDbParameter("@NatureCode", queryParam["NatureCode"].ToString()));
                }
                if (!queryParam["RegisterCapital"].IsEmpty())
                {
                    strSql.Append(" AND RegisterCapital=@RegisterCapital");
                    parameter.Add(DbParameters.CreateDbParameter("@RegisterCapital", queryParam["RegisterCapital"].ToString()));
                }
                if (!queryParam["SalesRevenue"].IsEmpty())
                {
                    strSql.Append(" AND SalesRevenue=@SalesRevenue");
                    parameter.Add(DbParameters.CreateDbParameter("@SalesRevenue", queryParam["SalesRevenue"].ToString()));
                }
                if (!queryParam["CustIndustryId"].IsEmpty())
                {
                    strSql.Append(" AND CustIndustryId=@CustIndustryId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustIndustryId", queryParam["CustIndustryId"].ToString()));
                }
                if (!queryParam["CompanySize"].IsEmpty())
                {
                    strSql.Append(" AND CompanySize=@CompanySize");
                    parameter.Add(DbParameters.CreateDbParameter("@CompanySize", queryParam["CompanySize"].ToString()));
                }
                if (!queryParam["CityId"].IsEmpty())
                {
                    strSql.Append(" AND CityId=@CityId");
                    parameter.Add(DbParameters.CreateDbParameter("@CityId", queryParam["CityId"].ToString()));
                }
                #endregion
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetMyPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            expression = expression.And(t => t.TraceUserId == userId && t.DeleteMark == 0 && t.EnabledMark > 0);
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //客户编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //客户名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //联系人
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //跟进人员
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取当前用户和下级用户的列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetChildList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM ( SELECT CustomerId,EnCode,FullName,ShortName,CustIndustryId,CustTypeId,CustLevelId,CustDegreeId,ProvinceId,Province,CityId,City,CountyId,County,CompanyAddress,Contact,PostId,Mobile,Tel,Fax ,QQ,Email,Wechat,Hobby,LegalPerson ,CompanySite,CompanyDesc,TraceUserId,TraceUserName,AlertDateTime ,EstablishDate,NatureCode,CompanySize,CustomerSource,RegisterCapital,SalesRevenue,AlertState,RatingScore,SortCode,DeleteMark ,EnabledMark,Description,CreateDate,TraceUserId AS CreateUserId,CreateUserName,ModifyDate,ModifyUserId,ModifyUserName  FROM dbo.Client_Customer ) tbl   WHERE  EnabledMark>0 AND DeleteMark=0 ");

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //客户编号
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //跟进人员
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }

                #region 高级查询条件
                if (!queryParam["EnCode"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@EnCode,EnCode)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@EnCode", queryParam["EnCode"].ToString()));
                }
                if (!queryParam["FullName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@FullName,FullName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@FullName", queryParam["FullName"].ToString()));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@Contact,Contact)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@Contact", queryParam["Contact"].ToString()));
                }
                if (!queryParam["TraceUserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@TraceUserName,TraceUserName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@TraceUserName", queryParam["TraceUserName"].ToString()));
                }
                if (!queryParam["SaleStageId"].IsEmpty())
                {//销售阶段
                    strSql.Append(" AND EXISTS(SELECT CustomerFollowId FROM Client_CustomerFollow WHERE  DeleteMark=0 AND EnabledMark=1  AND tbl.CustomerId=CustomerId AND SaleStageId=@SaleStageId )");
                    parameter.Add(DbParameters.CreateDbParameter("@SaleStageId", queryParam["SaleStageId"].ToString()));
                }
                if (!queryParam["CustLevelId"].IsEmpty())
                {
                    strSql.Append(" AND CustLevelId=@CustLevelId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustLevelId", queryParam["CustLevelId"].ToString()));
                }
                if (!queryParam["NatureCode"].IsEmpty())
                {
                    strSql.Append(" AND NatureCode=@NatureCode");
                    parameter.Add(DbParameters.CreateDbParameter("@NatureCode", queryParam["NatureCode"].ToString()));
                }
                if (!queryParam["RegisterCapital"].IsEmpty())
                {
                    strSql.Append(" AND RegisterCapital=@RegisterCapital");
                    parameter.Add(DbParameters.CreateDbParameter("@RegisterCapital", queryParam["RegisterCapital"].ToString()));
                }
                if (!queryParam["SalesRevenue"].IsEmpty())
                {
                    strSql.Append(" AND SalesRevenue=@SalesRevenue");
                    parameter.Add(DbParameters.CreateDbParameter("@SalesRevenue", queryParam["SalesRevenue"].ToString()));
                }
                if (!queryParam["CustIndustryId"].IsEmpty())
                {
                    strSql.Append(" AND CustIndustryId=@CustIndustryId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustIndustryId", queryParam["CustIndustryId"].ToString()));
                }
                if (!queryParam["CompanySize"].IsEmpty())
                {
                    strSql.Append(" AND CompanySize=@CompanySize");
                    parameter.Add(DbParameters.CreateDbParameter("@CompanySize", queryParam["CompanySize"].ToString()));
                }
                if (!queryParam["CityId"].IsEmpty())
                {
                    strSql.Append(" AND CityId=@CityId");
                    parameter.Add(DbParameters.CreateDbParameter("@CityId", queryParam["CityId"].ToString()));
                }
                #endregion

                return iauthorizeservice.FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取所有数据的列表
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
                strSql.Append(@" SELECT * FROM (SELECT c.* FROM dbo.Client_Customer c INNER JOIN Base_User u ON c.TraceUserId=u.UserId ) tbl WHERE  EnabledMark>0 AND DeleteMark=0 ");

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //客户编号
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //跟进人员
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }

                #region 高级查询条件
                if (!queryParam["EnCode"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@EnCode,EnCode)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@EnCode", queryParam["EnCode"].ToString()));
                }
                if (!queryParam["FullName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@FullName,FullName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@FullName", queryParam["FullName"].ToString()));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@Contact,Contact)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@Contact", queryParam["Contact"].ToString()));
                }
                if (!queryParam["TraceUserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@TraceUserName,TraceUserName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@TraceUserName", queryParam["TraceUserName"].ToString()));
                }
                if (!queryParam["SaleStageId"].IsEmpty())
                {//销售阶段
                    strSql.Append(" AND EXISTS(SELECT CustomerFollowId FROM Client_CustomerFollow WHERE  DeleteMark=0 AND EnabledMark=1  AND tbl.CustomerId=CustomerId AND SaleStageId=@SaleStageId )");
                    parameter.Add(DbParameters.CreateDbParameter("@SaleStageId", queryParam["SaleStageId"].ToString()));
                }
                if (!queryParam["CustLevelId"].IsEmpty())
                {
                    strSql.Append(" AND CustLevelId=@CustLevelId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustLevelId", queryParam["CustLevelId"].ToString()));
                }
                if (!queryParam["NatureCode"].IsEmpty())
                {
                    strSql.Append(" AND NatureCode=@NatureCode");
                    parameter.Add(DbParameters.CreateDbParameter("@NatureCode", queryParam["NatureCode"].ToString()));
                }
                if (!queryParam["RegisterCapital"].IsEmpty())
                {
                    strSql.Append(" AND RegisterCapital=@RegisterCapital");
                    parameter.Add(DbParameters.CreateDbParameter("@RegisterCapital", queryParam["RegisterCapital"].ToString()));
                }
                if (!queryParam["SalesRevenue"].IsEmpty())
                {
                    strSql.Append(" AND SalesRevenue=@SalesRevenue");
                    parameter.Add(DbParameters.CreateDbParameter("@SalesRevenue", queryParam["SalesRevenue"].ToString()));
                }
                if (!queryParam["CustIndustryId"].IsEmpty())
                {
                    strSql.Append(" AND CustIndustryId=@CustIndustryId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustIndustryId", queryParam["CustIndustryId"].ToString()));
                }
                if (!queryParam["CompanySize"].IsEmpty())
                {
                    strSql.Append(" AND CompanySize=@CompanySize");
                    parameter.Add(DbParameters.CreateDbParameter("@CompanySize", queryParam["CompanySize"].ToString()));
                }
                if (!queryParam["CityId"].IsEmpty())
                {
                    strSql.Append(" AND CityId=@CityId");
                    parameter.Add(DbParameters.CreateDbParameter("@CityId", queryParam["CityId"].ToString()));
                }
                #endregion

                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //客户编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //客户名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //联系人
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //跟进人员
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 公共客户池列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetPublicList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            expression = expression.And(t => t.DeleteMark == 0 && t.EnabledMark == 0);
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //客户编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //客户名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //联系人
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //跟进人员
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 关键字搜索客户列表
        /// </summary>
        /// <param name="keyword">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetSearchList(string keyword)
        {
            var expression = LinqExtensions.True<CustomerEntity>();

            expression = expression.And(t => t.DeleteMark == 0);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                expression = expression.And(t => t.FullName.Contains(keyword) || t.EnCode.Contains(keyword));
            }
            else
            {
                return new List<CustomerEntity>();
            }
            return this.BaseRepository().IQueryable(expression).Take(16);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="fullName">主键值</param>
        /// <returns></returns>
        public CustomerEntity GetEntityByName(string fullName)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            expression = expression.And(t => t.FullName == fullName);
            return this.BaseRepository().FindEntity(expression);
        }
        #region 移动端

        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetAppMyList(string userId)
        {
            return this.BaseRepository().IQueryable().Where(s => s.TraceUserId == userId && s.DeleteMark == 0 && s.EnabledMark > 0).OrderBy(t => t.FullName).ToList();
        }
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAppMyPageData(string userId, Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();

                strSql.Append("select * from (SELECT c.*,'0' isShare FROM dbo.Client_Customer c WHERE TraceUserId=@UserId AND c.DeleteMark=0 AND c.EnabledMark>0 ");
                strSql.Append(" UNION ");
                strSql.Append("SELECT c.*,'1' isShare FROM dbo.Client_Customer c WHERE c.DeleteMark=0 AND c.EnabledMark>0  AND EXISTS(SELECT * FROM Client_Share WHERE  DeleteMark=0 AND EnabledMark=1  AND c.CustomerId=ObjectId  AND ToUserID=@UserId ) ) tbl where 1=1 ");

                //当前用户和对应的下级用户
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //客户编号
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //联系人
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //跟进人员
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                #region 高级查询条件
                if (!queryParam["EnCode"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@EnCode,EnCode)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@EnCode", queryParam["EnCode"].ToString()));
                }
                if (!queryParam["FullName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@FullName,FullName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@FullName", queryParam["FullName"].ToString()));
                }
                if (!queryParam["Contact"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@Contact,Contact)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@Contact", queryParam["Contact"].ToString()));
                }
                if (!queryParam["TraceUserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@TraceUserName,TraceUserName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@TraceUserName", queryParam["TraceUserName"].ToString()));
                }
                if (!queryParam["CustLevelId"].IsEmpty())
                {
                    strSql.Append(" AND CustLevelId=@CustLevelId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustLevelId", queryParam["CustLevelId"].ToString()));
                }
                if (!queryParam["NatureCode"].IsEmpty())
                {
                    strSql.Append(" AND NatureCode=@NatureCode");
                    parameter.Add(DbParameters.CreateDbParameter("@NatureCode", queryParam["NatureCode"].ToString()));
                }
                if (!queryParam["RegisterCapital"].IsEmpty())
                {
                    strSql.Append(" AND RegisterCapital=@RegisterCapital");
                    parameter.Add(DbParameters.CreateDbParameter("@RegisterCapital", queryParam["RegisterCapital"].ToString()));
                }
                if (!queryParam["SalesRevenue"].IsEmpty())
                {
                    strSql.Append(" AND SalesRevenue=@SalesRevenue");
                    parameter.Add(DbParameters.CreateDbParameter("@SalesRevenue", queryParam["SalesRevenue"].ToString()));
                }
                if (!queryParam["CustIndustryId"].IsEmpty())
                {
                    strSql.Append(" AND CustIndustryId=@CustIndustryId");
                    parameter.Add(DbParameters.CreateDbParameter("@CustIndustryId", queryParam["CustIndustryId"].ToString()));
                }
                if (!queryParam["CompanySize"].IsEmpty())
                {
                    strSql.Append(" AND CompanySize=@CompanySize");
                    parameter.Add(DbParameters.CreateDbParameter("@CompanySize", queryParam["CompanySize"].ToString()));
                }
                if (!queryParam["CityId"].IsEmpty())
                {
                    strSql.Append(" AND CityId=@CityId");
                    parameter.Add(DbParameters.CreateDbParameter("@CityId", queryParam["CityId"].ToString()));
                }
                #endregion
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.CustomerId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<CustomerEntity>(keyValue);
                db.Delete<TrailRecordEntity>(t => t.ObjectId.Equals(keyValue));
                db.Delete<CustomerContactEntity>(t => t.CustomerId.Equals(keyValue));
                db.Delete<OwnershipChangeEntity>(t => t.ObjectId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CustomerEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //归属记录
                OwnershipChangeEntity changeEntiy = new OwnershipChangeEntity();
                changeEntiy.Create();
                changeEntiy.ObjectId = entity.CustomerId;
                changeEntiy.UserID = entity.TraceUserId;
                changeEntiy.UserName = entity.TraceUserName;
                changeEntiy.StartDate = DateTime.Now;
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    //插入归属记录
                    db.Insert(changeEntiy);
                    entity.Create();
                    //获得指定模块或者编号的单据号
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, SystemInfo.CurrentModuleId, "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        public void SaveForm(string keyValue, CustomerEntity entity, string moduleId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                //归属记录
                OwnershipChangeEntity changeEntiy = new OwnershipChangeEntity();
                changeEntiy.Create();
                changeEntiy.ObjectId = entity.CustomerId;
                changeEntiy.UserID = entity.TraceUserId;
                changeEntiy.UserName = entity.TraceUserName;
                changeEntiy.StartDate = DateTime.Now;
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    //插入归属记录
                    db.Insert(changeEntiy);

                    //获得指定模块或者编号的单据号
                    entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, moduleId, "", db);
                    db.Insert(entity);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 修改客户信用等级
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="RatingScore">客户信用等级</param>
        public void UpdateRatingScore(string keyValue, string RatingScore)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.RatingScore = RatingScore;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：（-2审核不通过， -1带审核， 0 被释放的客户， 1无需 审核，2审核通过）</param>
        public void UpdateState(string keyValue, int State)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.EnabledMark = 0;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 释放客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void GetRelease(string keyValue)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            CustomerEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                if (entity.TraceUserId != userId)
                {
                    throw new Exception("只能释放自己的客户");
                }
                entity.Modify(keyValue);
                //
                IRepository db = new RepositoryFactory().BaseRepository();
                //找到第一分享
                //var shareEntity = db.FindList<ShareEntity>(s => s.ObjectId == keyValue && s.DeleteMark == 0).OrderByDescending(s => s.Authority).ThenBy(s => s.CreateDate).FirstOrDefault();
                var shareEntity = db.FindList<ShareEntity>(s => s.ObjectId == keyValue && s.DeleteMark == 0).OrderByDescending(s => s.CreateDate).FirstOrDefault();
                if (shareEntity != null)
                {
                    entity.TraceUserId = shareEntity.ToUserID;
                    entity.TraceUserName = shareEntity.ToUserName;
                }
                else
                {
                    entity.EnabledMark = 0;
                }
                //归属记录
                OwnershipChangeEntity changeEntity = db.FindEntity<OwnershipChangeEntity>(s => s.ObjectId == keyValue && s.DeleteMark == 0 && !s.EndDate.HasValue);
                if (changeEntity != null)
                {
                    changeEntity.EndDate = DateTime.Now;
                    changeEntity.Modify(changeEntity.OwnershipChangeId);

                    db.BeginTrans();
                    try
                    {
                        if (shareEntity != null)
                        {
                            //插入第一个分享人为新归属人
                            OwnershipChangeEntity newChangeEntiy = new OwnershipChangeEntity();
                            newChangeEntiy.Create();
                            newChangeEntiy.ObjectId = entity.CustomerId;
                            newChangeEntiy.UserID = entity.TraceUserId;
                            newChangeEntiy.UserName = entity.TraceUserName;
                            newChangeEntiy.StartDate = DateTime.Now;
                            db.Insert(newChangeEntiy);
                            shareEntity.DeleteMark = 1;
                            db.Update(shareEntity);
                        }
                        else
                        {
                            //插入自己为归属人
                            OwnershipChangeEntity newChangeEntiy = new OwnershipChangeEntity();
                            newChangeEntiy.Create();
                            newChangeEntiy.ObjectId = entity.CustomerId;
                            newChangeEntiy.UserID = entity.TraceUserId;
                            newChangeEntiy.UserName = entity.TraceUserName;
                            newChangeEntiy.StartDate = DateTime.Now;
                            db.Insert(newChangeEntiy);
                        }
                        //归属更新
                        db.Update(changeEntity);
                        db.Update(entity);
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }
            }


        }
        /// <summary>
        /// 提取客户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetExtract(string keyValue)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.TraceUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            entity.TraceUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
            entity.EnabledMark = 1;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="FullName">客户名称</param>
        public void UpdateFullName(string keyValue, string FullName)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.FullName = FullName;
            this.BaseRepository().Update(entity);
        }

        #region 移动端
        /// <summary>
        /// 移动端新增客户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        public void AppInsert(CustomerEntity entity, string moduleId)
        {
            entity.DeleteMark = 0;
            entity.CustomerId = Guid.NewGuid().ToString();
            entity.CreateDate = DateTime.Now;
            //归属记录
            OwnershipChangeEntity changeEntiy = new OwnershipChangeEntity();
            changeEntiy.DeleteMark = 0;
            changeEntiy.EnabledMark = 1;
            changeEntiy.OwnershipChangeId = Guid.NewGuid().ToString();
            changeEntiy.CreateDate = DateTime.Now;
            changeEntiy.ObjectId = entity.CustomerId;
            changeEntiy.UserID = entity.TraceUserId;
            changeEntiy.UserName = entity.TraceUserName;
            changeEntiy.StartDate = DateTime.Now;
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //插入归属记录
                db.Insert(changeEntiy);

                //获得指定模块或者编号的单据号
                entity.EnCode = coderuleService.SetBillCode(entity.CreateUserId, moduleId, "", db);
                db.Insert(entity);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }

        }
        /// <summary>
        /// 移动端更新客户
        /// </summary>
        /// <param name="entity"></param>
        public void AppUpdate(CustomerEntity entity)
        {
            this.BaseRepository().Update(entity);
        }
        #endregion 
        #endregion
    }
}
