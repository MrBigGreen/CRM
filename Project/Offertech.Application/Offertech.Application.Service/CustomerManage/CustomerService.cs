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
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �����ˣ�chand
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public class CustomerService : RepositoryFactory<CustomerEntity>, ICustomerService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private IAuthorizeService<CustomerEntity> iauthorizeservice = new AuthorizeService<CustomerEntity>();


        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList()
        {
            return this.BaseRepository().IQueryable().Where(s => s.DeleteMark == 0 && s.EnabledMark > 0).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetMyList()
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            return this.BaseRepository().IQueryable().Where(s => s.TraceUserId == userId && s.DeleteMark == 0 && s.EnabledMark > 0).OrderBy(t => t.FullName).ToList();
        }
        /// <summary>
        /// ��ȡ�ͻ�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList(string userId)
        {
            return this.BaseRepository().IQueryable().Where(s => s.TraceUserId == userId && s.DeleteMark == 0 && s.EnabledMark > 0).OrderBy(t => t.FullName).ToList();
        }

        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
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

                //��ѯ����
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //�ͻ����
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //�ͻ�����
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //��ϵ��
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //������Ա
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                #region �߼���ѯ����
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
                {//���۽׶�
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
        /// ��ȡ��ǰ�û��Ŀͻ��б�
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

                //��ǰ�û��Ͷ�Ӧ���¼��û�
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //��ѯ����
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //�ͻ����
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //�ͻ�����
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //��ϵ��
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //������Ա
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                #region �߼���ѯ����
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
                {//���۽׶�
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetMyPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            expression = expression.And(t => t.TraceUserId == userId && t.DeleteMark == 0 && t.EnabledMark > 0);
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //�ͻ����
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //�ͻ�����
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //��ϵ��
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //������Ա
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// ��ȡ��ǰ�û����¼��û����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public DataTable GetChildList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM ( SELECT CustomerId,EnCode,FullName,ShortName,CustIndustryId,CustTypeId,CustLevelId,CustDegreeId,ProvinceId,Province,CityId,City,CountyId,County,CompanyAddress,Contact,PostId,Mobile,Tel,Fax ,QQ,Email,Wechat,Hobby,LegalPerson ,CompanySite,CompanyDesc,TraceUserId,TraceUserName,AlertDateTime ,EstablishDate,NatureCode,CompanySize,CustomerSource,RegisterCapital,SalesRevenue,AlertState,RatingScore,SortCode,DeleteMark ,EnabledMark,Description,CreateDate,TraceUserId AS CreateUserId,CreateUserName,ModifyDate,ModifyUserId,ModifyUserName  FROM dbo.Client_Customer ) tbl   WHERE  EnabledMark>0 AND DeleteMark=0 ");

                //��ѯ����
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //�ͻ����
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //�ͻ�����
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //��ϵ��
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //������Ա
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }

                #region �߼���ѯ����
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
                {//���۽׶�
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
        /// ��ȡ�������ݵ��б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public DataTable GetPageData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@" SELECT * FROM (SELECT c.* FROM dbo.Client_Customer c INNER JOIN Base_User u ON c.TraceUserId=u.UserId ) tbl WHERE  EnabledMark>0 AND DeleteMark=0 ");

                //��ѯ����
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //�ͻ����
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //�ͻ�����
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //��ϵ��
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //������Ա
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }

                #region �߼���ѯ����
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
                {//���۽׶�
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //�ͻ����
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //�ͻ�����
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //��ϵ��
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //������Ա
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// �����ͻ����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetPublicList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            var queryParam = queryJson.ToJObject();
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            expression = expression.And(t => t.DeleteMark == 0 && t.EnabledMark == 0);
            //��ѯ����
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //�ͻ����
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //�ͻ�����
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    case "Contact":             //��ϵ��
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "TraceUserName":       //������Ա
                        expression = expression.And(t => t.TraceUserName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// �ؼ��������ͻ��б�
        /// </summary>
        /// <param name="keyword">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
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
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public CustomerEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="fullName">����ֵ</param>
        /// <returns></returns>
        public CustomerEntity GetEntityByName(string fullName)
        {
            var expression = LinqExtensions.True<CustomerEntity>();
            expression = expression.And(t => t.FullName == fullName);
            return this.BaseRepository().FindEntity(expression);
        }
        #region �ƶ���

        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetAppMyList(string userId)
        {
            return this.BaseRepository().IQueryable().Where(s => s.TraceUserId == userId && s.DeleteMark == 0 && s.EnabledMark > 0).OrderBy(t => t.FullName).ToList();
        }
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
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

                //��ǰ�û��Ͷ�Ӧ���¼��û�
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //��ѯ����
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "EnCode":              //�ͻ����
                            strSql.Append(" AND CHARINDEX(@keyword,EnCode)>0 ");
                            break;
                        case "FullName":            //�ͻ�����
                            strSql.Append(" AND CHARINDEX(@keyword,FullName)>0 ");
                            break;
                        case "Contact":             //��ϵ��
                            strSql.Append(" AND CHARINDEX(@keyword,Contact)>0 ");
                            break;
                        case "TraceUserName":       //������Ա
                            strSql.Append(" AND CHARINDEX(@keyword,TraceUserName)>0 ");
                            break;
                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }
                #region �߼���ѯ����
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

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
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

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
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
                //������¼
                OwnershipChangeEntity changeEntiy = new OwnershipChangeEntity();
                changeEntiy.Create();
                changeEntiy.ObjectId = entity.CustomerId;
                changeEntiy.UserID = entity.TraceUserId;
                changeEntiy.UserName = entity.TraceUserName;
                changeEntiy.StartDate = DateTime.Now;
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    //���������¼
                    db.Insert(changeEntiy);
                    entity.Create();
                    //���ָ��ģ����߱�ŵĵ��ݺ�
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
        /// �����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">ģ��</param>
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
                //������¼
                OwnershipChangeEntity changeEntiy = new OwnershipChangeEntity();
                changeEntiy.Create();
                changeEntiy.ObjectId = entity.CustomerId;
                changeEntiy.UserID = entity.TraceUserId;
                changeEntiy.UserName = entity.TraceUserName;
                changeEntiy.StartDate = DateTime.Now;
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    //���������¼
                    db.Insert(changeEntiy);

                    //���ָ��ģ����߱�ŵĵ��ݺ�
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
        /// �޸Ŀͻ����õȼ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="RatingScore">�ͻ����õȼ�</param>
        public void UpdateRatingScore(string keyValue, string RatingScore)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.RatingScore = RatingScore;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// �޸��û�״̬
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="State">״̬����-2��˲�ͨ���� -1����ˣ� 0 ���ͷŵĿͻ��� 1���� ��ˣ�2���ͨ����</param>
        public void UpdateState(string keyValue, int State)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.EnabledMark = 0;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// �ͷſͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        public void GetRelease(string keyValue)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            CustomerEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                if (entity.TraceUserId != userId)
                {
                    throw new Exception("ֻ���ͷ��Լ��Ŀͻ�");
                }
                entity.Modify(keyValue);
                //
                IRepository db = new RepositoryFactory().BaseRepository();
                //�ҵ���һ����
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
                //������¼
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
                            //�����һ��������Ϊ�¹�����
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
                            //�����Լ�Ϊ������
                            OwnershipChangeEntity newChangeEntiy = new OwnershipChangeEntity();
                            newChangeEntiy.Create();
                            newChangeEntiy.ObjectId = entity.CustomerId;
                            newChangeEntiy.UserID = entity.TraceUserId;
                            newChangeEntiy.UserName = entity.TraceUserName;
                            newChangeEntiy.StartDate = DateTime.Now;
                            db.Insert(newChangeEntiy);
                        }
                        //��������
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
        /// ��ȡ�ͻ�
        /// </summary>
        /// <param name="keyValue">����</param>
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
        /// �޸Ŀͻ�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="FullName">�ͻ�����</param>
        public void UpdateFullName(string keyValue, string FullName)
        {
            CustomerEntity entity = new CustomerEntity();
            entity.Modify(keyValue);
            entity.FullName = FullName;
            this.BaseRepository().Update(entity);
        }

        #region �ƶ���
        /// <summary>
        /// �ƶ��������ͻ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="moduleId">ģ��</param>
        public void AppInsert(CustomerEntity entity, string moduleId)
        {
            entity.DeleteMark = 0;
            entity.CustomerId = Guid.NewGuid().ToString();
            entity.CreateDate = DateTime.Now;
            //������¼
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
                //���������¼
                db.Insert(changeEntiy);

                //���ָ��ģ����߱�ŵĵ��ݺ�
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
        /// �ƶ��˸��¿ͻ�
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
