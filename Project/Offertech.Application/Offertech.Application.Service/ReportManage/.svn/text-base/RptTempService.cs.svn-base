using Offertech.Application.Code;
using Offertech.Application.Entity.AuthorizeManage;
using Offertech.Application.Entity.ReportManage;
using Offertech.Application.IService.AuthorizeManage;
using Offertech.Application.IService.ReportManage;
using Offertech.Data;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Offertech.Application.Service.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class RptTempService : RepositoryFactory<RptTempEntity>, IRptTempService
    {
        private IAuthorizeService<RptTempEntity> iauthorizeservice = new AuthorizeManage.AuthorizeService<RptTempEntity>();
        private IRepository db = new RepositoryFactory().BaseRepository();

        #region 获取数据
        /// <summary>
        /// 报表模板列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RptTempEntity> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  r.TempId,
                                    r.EnCode ,
                                    r.FullName ,
                                    CASE r.TempType
                                      WHEN 'line' THEN '折线图'
                                      WHEN 'bar' THEN '柱形图'
                                      WHEN 'map' THEN '地图'
                                      WHEN 'pie' THEN '饼图'
                                    END AS TempType ,
                                    r.TempCategory ,
                                    r.Description ,
                                    r.CreateDate
                            FROM    Rpt_Temp r
                            WHERE   1 = 1 ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":            //角色编号
                        strSql.Append(" AND r.EnCode LIKE @keyword ");
                        parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyword + '%'));
                        break;
                    case "FullName":          //角色名称
                        strSql.Append(" AND r.FullName LIKE @keyword ");
                        parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyword + '%'));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["reportCode"].IsEmpty())
            {
                strSql.Append(" AND r.TempCategory = @TempCategory ");
                parameter.Add(DbParameters.CreateDbParameter("@TempCategory", queryParam["reportCode"].ToString()));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 报表模板实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RptTempEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="reportId">主键值</param>
        /// <returns></returns>
        public string GetReportData(string reportId)
        {
            RptTempEntity rpttempentity = this.BaseRepository().FindEntity(reportId);
            if (rpttempentity.ParamJson != null)
            {
                dynamic paramJson = rpttempentity.ParamJson.ToJson();
                string strSql = paramJson.sqlString;
                string strListSql = paramJson.listSqlString;
                string picTitle = paramJson.title;
                string title = rpttempentity.FullName;
                string tempType = rpttempentity.TempType;
                List<FieldList> listField = new List<FieldList>();
                DataTable picData = new DataTable();
                if (!string.IsNullOrEmpty(strSql))
                {
                    picData = this.BaseRepository().FindTable(strSql);
                }
                DataTable listData = new DataTable();
                if (!string.IsNullOrEmpty(strListSql))
                {
                    listData = this.BaseRepository().FindTable(strListSql);
                    if (listData.Columns.Count > 0)
                    {
                        for (int i = 0; i < listData.Columns.Count; i++)
                        {
                            listField.Add(new FieldList() { Field = listData.Columns[i].ColumnName });
                        }
                    }
                }
                var jsonData = new
                {
                    title = title,
                    tempType = tempType,
                    listField = listField,
                    picTitle = picTitle,
                    picData = picData,
                    listData = listData
                };
                return jsonData.ToJson();
            }
            return null;
        }

        /// <summary>
        /// 客户汇总报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerSummaryData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM (");
                strSql.Append("SELECT tbl.*,follow.CustomerFunnel1,follow.CustomerFunnel2,follow.CustomerFunnel3,follow.CustomerFunnel4,follow.CustomerFunnel5,follow.CustomerFunnel6,follow.CustomerFunnel7,follow.FaceFollow,follow.TelFollow,tmpN.CustomerNewCount FROM (");
                strSql.Append(" SELECT u.RealName,u.DepartmentName,u.DepartmentId,ctm.TraceUserId AS CreateUserId,ctm.CustomerTotal  FROM v_UserDepartment u ");
                strSql.Append(" INNER JOIN ( SELECT TraceUserId,COUNT(*) AS CustomerTotal FROM  dbo.Client_Customer WHERE DeleteMark=0 AND EnabledMark>0 GROUP BY  TraceUserId) ctm ON u.UserId=ctm.TraceUserId) tbl ");
                strSql.Append(" LEFT JOIN (");
                strSql.Append("	SELECT CreateUserId,SUM(CustomerFunnel1) AS CustomerFunnel1,SUM(CustomerFunnel2) AS CustomerFunnel2,SUM(CustomerFunnel3) AS CustomerFunnel3, 	SUM(CustomerFunnel4) AS ");
                strSql.Append("	CustomerFunnel4,SUM(CustomerFunnel5) AS CustomerFunnel5,SUM(CustomerFunnel6) AS CustomerFunnel6, SUM(CustomerFunnel7) AS CustomerFunnel7,SUM(FaceFollow) AS FaceFollow,SUM(TelFollow) AS TelFollow FROM ( SELECT CreateUserId, CASE WHEN SaleStageId = '62d1f2f6-7796-4c3c-a981-ea1ef820d3f4'  THEN 1 ELSE 0 END AS CustomerFunnel1, CASE  WHEN SaleStageId= '52d29698-93d5-41bc-bc6e-1f6c65665893'   THEN 1  ELSE 0 END AS CustomerFunnel2, CASE  WHEN SaleStageId= 'ad8ff3bf-3b9f-49ff-a1e5-d16f732b4347'  THEN 1  ELSE 0 END AS CustomerFunnel3, CASE  WHEN SaleStageId= '01ba1091-6e0e-4ace-8d57-9458fd33967e'  THEN 1  ELSE 0 END AS CustomerFunnel4, CASE  WHEN SaleStageId= '8d9f6089-8900-46de-85a1-52aeaf1926fa'   THEN 1  ELSE 0 END AS CustomerFunnel5, CASE  WHEN SaleStageId= 'c8355d30-69a1-4d0a-ae60-c88642e5f8a2'  THEN 1  ELSE 0 END AS CustomerFunnel6, CASE  WHEN SaleStageId='95285934-4b59-4ede-8664-853c3ecc938a'  THEN 1  ELSE 0 END AS CustomerFunnel7, CASE  WHEN FollowUpMode=1  THEN 1  ELSE 0 END AS TelFollow,	CASE  WHEN FollowUpMode=2 THEN 1  ELSE 0 END AS FaceFollow  FROM Client_TrailRecord tr WHERE  EXISTS(SELECT CustomerFollowId FROM Client_CustomerFollow WHERE CustomerFollowId= tr.TrailId  AND DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,EndTime,CreateDate)<=0))  f GROUP BY CreateUserId");
                strSql.Append("	) follow ON tbl.CreateUserId=follow.CreateUserId");
                strSql.Append(" LEFT JOIN  ( SELECT CreateUserId,COUNT(*) AS CustomerNewCount FROM  dbo.Client_Customer WHERE DeleteMark=0 AND EnabledMark>0");
                strSql.Append(" AND DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,@EndTime,CreateDate)<=0  GROUP BY CreateUserId");
                strSql.Append(") tmpN ON tbl.CreateUserId=tmpN.CreateUserId ");
                strSql.Append(") newTbl WHERE 1=1  {0}");

                string userID = OperatorProvider.Provider.Current().LoginInfo.UserId;

                if (!string.IsNullOrWhiteSpace(userID))
                {
                    strSql.Replace("{0}", string.Format(@" and exists(select * from Base_User where UserId=newTbl.CreateUserId and (UserId='{0}' or  ManagerId='{0}' or DepartmentId in
         (                                                                                                      select DepartmentId
                                                                                                                from
       Base_Department
                                                                                                                where  ParentId =
         (select DepartmentId
          from   Base_User
          where  UserId = '{0}'))))", userID));
                }

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@UserName,RealName)>0");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                if (!queryParam["DepartmentId"].IsEmpty())
                {
                    strSql.Append(" AND DepartmentId=@DepartmentId");
                    parameter.Add(DbParameters.CreateDbParameter("@DepartmentId", queryParam["DepartmentId"].ToString()));
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
        /// 客户汇总报表-按部门
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerSummaryByDeptData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(" SELECT tbl.DepartmentName ,SUM(CustomerFunnel1) AS CustomerFunnel1,SUM(CustomerFunnel2) AS CustomerFunnel2,SUM(CustomerFunnel3) AS CustomerFunnel3,");
                strSql.Append(" SUM(CustomerFunnel4) AS CustomerFunnel4,SUM(CustomerFunnel5) AS CustomerFunnel5,SUM(CustomerFunnel6) AS CustomerFunnel6,SUM(CustomerFunnel7) AS CustomerFunnel7,");
                strSql.Append(" SUM(FaceFollow) AS FaceFollow,SUM(TelFollow) AS TelFollow,SUM(CustomerNewCount) AS CustomerNewCount,SUM(CustomerTotal) AS CustomerTotal FROM ");
                strSql.Append(" (SELECT * FROM dbo.v_UserDepartment u RIGHT JOIN (SELECT tmpT.CreateUserId,ISNULL(tmpT.CustomerTotal,0) AS CustomerTotal,ISNULL(tmpN.CustomerNewCount,0) CustomerNewCount,");
                strSql.Append(" ISNULL(tmpF.CustomerFunnel1,0) CustomerFunnel1,ISNULL(tmpF.CustomerFunnel2,0)CustomerFunnel2,ISNULL(tmpF.CustomerFunnel3,0) CustomerFunnel3,");
                strSql.Append(" ISNULL(tmpF.CustomerFunnel4,0) CustomerFunnel4,ISNULL(tmpF.CustomerFunnel5,0) CustomerFunnel5,ISNULL(tmpF.CustomerFunnel6,0) CustomerFunnel6,ISNULL(tmpF.CustomerFunnel7,0) CustomerFunnel7,");
                strSql.Append(" ISNULL(tmpF.FaceFollow,0) FaceFollow,ISNULL(tmpF.TelFollow,0) TelFollow FROM (( SELECT CreateUserId,COUNT(*) AS CustomerTotal FROM  dbo.Client_Customer WHERE DeleteMark=0 AND EnabledMark>0 GROUP BY CreateUserId) tmpT ");
                strSql.Append(" LEFT JOIN  ( SELECT CreateUserId,COUNT(*) AS CustomerNewCount FROM  dbo.Client_Customer WHERE DeleteMark=0 ");
                strSql.Append(" AND EnabledMark>0 AND DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,@EndTime,CreateDate)<=0 GROUP BY CreateUserId) tmpN ON tmpT.CreateUserId=tmpN.CreateUserId");
                strSql.Append(" LEFT JOIN  (	SELECT CreateUserId,SUM(CustomerFunnel1) AS CustomerFunnel1,SUM(CustomerFunnel2) AS CustomerFunnel2,");
                strSql.Append(" SUM(CustomerFunnel3) AS CustomerFunnel3,SUM(CustomerFunnel4) AS CustomerFunnel4,SUM(CustomerFunnel5) AS CustomerFunnel5,SUM(CustomerFunnel6) AS CustomerFunnel6,");
                strSql.Append(" SUM(CustomerFunnel7) AS CustomerFunnel7,SUM(FaceFollow) AS FaceFollow,SUM(TelFollow) AS TelFollow FROM (");
                strSql.Append(" SELECT CreateUserId, CASE WHEN SaleStageId = '62d1f2f6-7796-4c3c-a981-ea1ef820d3f4'  THEN 1 ELSE 0 END AS CustomerFunnel1,");
                strSql.Append(" CASE  WHEN SaleStageId= '52d29698-93d5-41bc-bc6e-1f6c65665893'   THEN 1  ELSE 0 END AS CustomerFunnel2,");
                strSql.Append(" CASE  WHEN SaleStageId= 'ad8ff3bf-3b9f-49ff-a1e5-d16f732b4347'  THEN 1  ELSE 0 END AS CustomerFunnel3,");
                strSql.Append(" CASE  WHEN SaleStageId= '01ba1091-6e0e-4ace-8d57-9458fd33967e'  THEN 1  ELSE 0 END AS CustomerFunnel4,");
                strSql.Append(" CASE  WHEN SaleStageId= '8d9f6089-8900-46de-85a1-52aeaf1926fa'   THEN 1  ELSE 0 END AS CustomerFunnel5,");
                strSql.Append(" CASE  WHEN SaleStageId= 'c8355d30-69a1-4d0a-ae60-c88642e5f8a2'  THEN 1  ELSE 0 END AS CustomerFunnel6,");
                strSql.Append(" CASE  WHEN SaleStageId='95285934-4b59-4ede-8664-853c3ecc938a'  THEN 1  ELSE 0 END AS CustomerFunnel7,");
                strSql.Append(" CASE  WHEN FollowUpMode=1  THEN 1  ELSE 0 END AS TelFollow,	CASE  WHEN FollowUpMode=2 THEN 1  ELSE 0 END AS FaceFollow");
                strSql.Append(" FROM Client_TrailRecord tr WHERE  EXISTS(SELECT CustomerFollowId FROM Client_CustomerFollow WHERE CustomerFollowId= tr.TrailId ");
                strSql.Append(" AND DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,EndTime,CreateDate)<=0))  f GROUP BY CreateUserId) tmpF ON tmpF.CreateUserId = tmpT.CreateUserId)) tt ON u.UserId=tt.CreateUserId where 1=1 {0}) tbl GROUP BY tbl.DepartmentName ");


                string GetReadSql = OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize;
                if (!string.IsNullOrWhiteSpace(GetReadSql))
                {
                    strSql.Replace("{0}", string.Format(" and CreateUserId in({0}) ", GetReadSql));
                }

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
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
        /// 客户跟进报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerFollowData(Pagination pagination, string queryJson)
        {

            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM ( SELECT c.FullName AS CustomerName,u.DepartmentName,c.CustomerId,r.StartTime,r.EndTime,r.FollowUpMode,r.SaleStageName,r.SaleStageId,r.TrackContent,r.Description,r.Contact,r.PostId,r.CreateUserName,r.CreateDate,r.CreateUserId FROM Client_TrailRecord r ");
                strSql.Append(" INNER JOIN Client_Customer c ON r.ObjectId=c.CustomerId AND r.ObjectSort=2 INNER JOIN dbo.v_UserDepartment u ON r.CreateUserId=u.UserId WHERE r.DeleteMark=0 AND r.EnabledMark=1 ");
                strSql.Append(" AND DATEDIFF(DAY,@StartTime,r.StartTime)>=0 and DATEDIFF(DAY,@EndTime,r.StartTime)<=0 ) tbl WHERE 1=1 {0} ");


                string userID = OperatorProvider.Provider.Current().LoginInfo.UserId;

                if (!string.IsNullOrWhiteSpace(userID))
                {
                    strSql.Replace("{0}", string.Format(@" and exists(select * from Base_User where UserId=tbl.CreateUserId and (UserId='{0}' or  ManagerId='{0}'or DepartmentId in
         (                                                                                                      select DepartmentId
                                                                                                                from
       Base_Department
                                                                                                                where  ParentId =
         (select DepartmentId
          from   Base_User
          where  UserId = '{0}'))))", userID));
                }

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@UserName, r.CreateUserName )>0");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                if (!queryParam["UserId"].IsEmpty())
                {
                    strSql.Append(" AND u.UserId=@UserId");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", queryParam["UserId"].ToString()));
                }
                if (!queryParam["DepartmentId"].IsEmpty())
                {
                    strSql.Append(" AND u.DepartmentId=@DepartmentId");
                    parameter.Add(DbParameters.CreateDbParameter("@DepartmentId", queryParam["DepartmentId"].ToString()));
                }
                if (!queryParam["SaleStageId"].IsEmpty())
                {
                    strSql.Append(" AND r.SaleStageId=@SaleStageId");
                    parameter.Add(DbParameters.CreateDbParameter("@SaleStageId", queryParam["SaleStageId"].ToString()));
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
        /// 客户新进报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerNewData(Pagination pagination, string queryJson)
        {
            ;
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM (SELECT c.CreateDate,c.TraceUserId  AS CreateUserId,c.TraceUserName,c.CustomerId,c.FullName AS CustomerName,c.ProvinceId,c.CityId,t.Description,t.Contact FROM Client_Customer  c ");
                strSql.Append(" LEFT JOIN dbo.Client_CustomerFollow f ON c.CustomerId=f.CustomerId ");
                strSql.Append(" LEFT JOIN  Client_TrailRecord t ON f.CustomerFollowId=t.TrailId ");
                strSql.Append(" AND DATEDIFF(DAY,@StartTime,c.CreateDate)>=0 and DATEDIFF(DAY,@EndTime,c.CreateDate)<=0  ) tbl WHERE 1=1 {0}");

                string userID = OperatorProvider.Provider.Current().LoginInfo.UserId;

                if (!string.IsNullOrWhiteSpace(userID))
                {
                    strSql.Replace("{0}", string.Format(@" and exists(select * from Base_User where UserId=tbl.CreateUserId and (UserId='{0}' or  ManagerId='{0}'or DepartmentId in
         (                                                                                                      select DepartmentId
                                                                                                                from
       Base_Department
                                                                                                                where  ParentId =
         (select DepartmentId
          from   Base_User
          where  UserId = '{0}'))))", userID));
                }

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@UserName,c.TraceUserName )>0");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                if (!queryParam["UserId"].IsEmpty())
                {
                    strSql.Append(" AND c.TraceUserId=@UserId");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", queryParam["UserId"].ToString()));
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
        /// 销售合同报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetContractSaleData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM dbo.Client_Contract c WHERE DeleteMark=0 AND EnabledMark=1 AND   DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,@EndTime,CreateDate)<=0 AND ContractType<20 {0}");

                string userID = OperatorProvider.Provider.Current().LoginInfo.UserId;

                if (!string.IsNullOrWhiteSpace(userID))
                {
                    strSql.Replace("{0}", string.Format(@" and exists(select * from Base_User where UserId=c.CreateUserId and (UserId='{0}' or  ManagerId='{0}'or DepartmentId in
         (                                                                                                      select DepartmentId
                                                                                                                from
       Base_Department
                                                                                                                where  ParentId =
         (select DepartmentId
          from   Base_User
          where  UserId = '{0}'))))", userID));
                }


                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@UserName,c.CreateUserName )>0");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                if (!queryParam["UserId"].IsEmpty())
                {
                    strSql.Append(" AND c.CreateUserId=@UserId");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", queryParam["UserId"].ToString()));
                }
                if (!queryParam["ServiceTypeId"].IsEmpty())
                {
                    strSql.Append(" AND EXISTS (SELECT c.ContractId FROM dbo.Client_ContractService s WHERE s.ContractId=c.ContractId AND s.ServiceTypeId=@ServiceTypeId )");
                    parameter.Add(DbParameters.CreateDbParameter("@ServiceTypeId", queryParam["ServiceTypeId"].ToString()));
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
        /// 客服合同报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetContractServiceData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM dbo.Client_Contract c WHERE DeleteMark=0 AND EnabledMark=1 AND   DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,@EndTime,CreateDate)<=0 AND ContractType>20 {0}");

                string userID = OperatorProvider.Provider.Current().LoginInfo.UserId;

                if (!string.IsNullOrWhiteSpace(userID))
                {
                    strSql.Replace("{0}", string.Format(@" and exists(select * from Base_User where UserId=c.CreateUserId and (UserId='{0}' or  ManagerId='{0}'or DepartmentId in
         (                                                                                                      select DepartmentId
                                                                                                                from
       Base_Department
                                                                                                                where  ParentId =
         (select DepartmentId
          from   Base_User
          where  UserId = '{0}'))))", userID));
                }

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@UserName,c.CreateUserName )>0");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                if (!queryParam["UserId"].IsEmpty())
                {
                    strSql.Append(" AND c.CreateUserId=@UserId");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", queryParam["UserId"].ToString()));
                }
                if (!queryParam["ServiceTypeId"].IsEmpty())
                {
                    strSql.Append(" AND EXISTS (SELECT c.ContractId FROM dbo.Client_ContractService s WHERE s.ContractId=c.ContractId AND s.ServiceTypeId=@ServiceTypeId )");
                    parameter.Add(DbParameters.CreateDbParameter("@ServiceTypeId", queryParam["ServiceTypeId"].ToString()));
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
        /// 我创建的客户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMyselfCustomerData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM dbo.Client_Customer c WHERE DeleteMark=0 AND EnabledMark=1 AND   DATEDIFF(DAY,@StartTime,CreateDate)>=0 and DATEDIFF(DAY,@EndTime,CreateDate)<=0 ");

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["UserName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@UserName,CreateUserName )>0");
                    parameter.Add(DbParameters.CreateDbParameter("@UserName", queryParam["UserName"].ToString()));
                }
                strSql.Append(" AND CreateUserId=@UserId");
                if (!queryParam["UserId"].IsEmpty())
                {

                    parameter.Add(DbParameters.CreateDbParameter("@UserId", queryParam["UserId"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", OperatorProvider.Provider.Current().LoginInfo.UserId));
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
        /// 市场部导入客户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMarketCustomerData(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append("SELECT tbl.CustomerId,tbl.EnCode ,tbl.FullName CustomerName,ISNULL(tbl.TraceUserName,'')  AS TraceUserName,ISNULL(flw.SaleStageName,'') AS SaleStageName,tbl.CreateDate   FROM ( SELECT CustomerId, EnCode,FullName,TraceUserName,CreateDate FROM dbo.Client_Customer WHERE CustomerSource='1cf49dad-eaa0-4b7f-9a34-d6b38a044b84' AND CreateUserId='03cad0f7-3636-4edd-a64b-26342c265f53' ) tbl LEFT JOIN Client_CustomerFollow flw ON tbl.CustomerId=flw.CustomerId  where   DATEDIFF(DAY,@StartTime,tbl.CreateDate)>=0 and DATEDIFF(DAY,@EndTime,tbl.CreateDate)<=0");

                #region 高级查询条件
                if (!queryParam["StartTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", queryParam["StartTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@StartTime", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")));
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", queryParam["EndTime"].ToString()));
                }
                else
                {
                    parameter.Add(DbParameters.CreateDbParameter("@EndTime", DateTime.Now.ToString("yyyy-MM-dd")));
                }
                if (!queryParam["CustomerName"].IsEmpty())
                {
                    strSql.Append(" AND CHARINDEX(@CustomerName,FullName )>0");
                    parameter.Add(DbParameters.CreateDbParameter("@CustomerName", queryParam["CustomerName"].ToString()));
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

        #region 提交数据
        /// <summary>
        /// 删除报表模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存报表模板表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="rptTempEntity">报表实体</param>
        /// <param name="moduleEntity">模块实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RptTempEntity rptTempEntity, ModuleEntity moduleEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    rptTempEntity.Modify(keyValue);
                    db.Update(rptTempEntity);
                }
                else
                {
                    rptTempEntity.Create();
                    db.Insert(rptTempEntity);
                    moduleEntity.UrlAddress = " /ReportManage/Report/ReportPreview?keyValue=" + rptTempEntity.TempId;
                    db.Insert(moduleEntity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }



        #endregion

        #region 接口数据
        /// <summary>
        /// 获取CRM数据
        /// </summary>
        /// <param name="EmployeeName">员工姓名</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="DateType">查询类型（0：全部；1：新签；2：新建；3：跟进）</param>
        /// <returns></returns>
        public DataTable GetTableToKPI(string EmployeeName, string StartDate, string EndDate, int DateType)
        {
            var strSql = new StringBuilder();
            switch (DateType)
            {
                case 0:
                    strSql.AppendFormat(@"SELECT   CreateUserName, '新签' AS Type, count(1) AS Counts
                                            FROM     Client_Contract
                                            WHERE    SignType = 1 and CreateDate>='{0}' and CreateDate<='{1}' and CreateUserName='{2}' 
                                            GROUP BY CreateUserName
                                            UNION ALL
                                            SELECT   CreateUserName, '新建' AS Type, count(1) AS Counts
                                            FROM     Client_Customer
                                            WHERE   CreateDate>='{0}' and CreateDate<='{1}' and CreateUserName='{2}' 
                                            GROUP BY CreateUserName
                                            UNION ALL
                                            SELECT   CreateUserName, '跟进' AS Type, count(1) AS Counts
                                            FROM     Client_CustomerFollow
                                            WHERE   CreateDate>='{0}' and CreateDate<='{1}' and CreateUserName='{2}' 
                                            GROUP BY CreateUserName", StartDate, EndDate, EmployeeName);
                    break;
                case 1:
                    strSql.AppendFormat(@"select CreateUserName,'新签' as Type,count(1) as Counts from Client_Contract 
                                          where SignType=1 and CreateDate>='{0}' and CreateDate<='{1}' and CreateUserName='{2}'   
                                          group by CreateUserName", StartDate,  EndDate, EmployeeName);
                    break;
                case 2:
                    strSql.AppendFormat(@"select CreateUserName,'新建' as Type,count(1) as Counts from Client_Customer 
                                          where  CreateDate>='{0}' and CreateDate<='{1}' and CreateUserName='{2}'   
                                          group by CreateUserName", StartDate, EndDate, EmployeeName);
                    break;
                case 3:
                    strSql.AppendFormat(@"select CreateUserName,'跟进' as Type,count(1) as Counts from Client_CustomerFollow 
                                          where  CreateDate>='{0}' and CreateDate<='{1}' and CreateUserName='{2}'   
                                          group by CreateUserName", StartDate, EndDate, EmployeeName);
                    break;
                default:
                    break;
            }

            DataTable dt = db.FindTable(strSql.ToString());
            return dt;
        }
        #endregion





    }
    class FieldList
    {
        public string Field { get; set; }
    }
}
