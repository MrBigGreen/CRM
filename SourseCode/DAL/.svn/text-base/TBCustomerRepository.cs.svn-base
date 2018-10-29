using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-10
    /// 描述说明：客户信息
    /// </summary>
    public partial class TBCustomerRepository : BaseRepository<TB_Customer>, IDisposable
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerModel> GetData(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[24];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[1] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("VocationCode") && !string.IsNullOrWhiteSpace(queryDic["VocationCode"]))
            {
                param[2] = new SqlParameter("@VocationCode", queryDic["VocationCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@VocationCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("DateType") && !string.IsNullOrWhiteSpace(queryDic["DateType"]))
            {
                param[3] = new SqlParameter("@DateType", queryDic["DateType"]);
            }
            else
            {
                param[3] = new SqlParameter("@DateType", "");
            }
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[4] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[5] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[5] = new SqlParameter("@EndDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerLevel") && !string.IsNullOrWhiteSpace(queryDic["CustomerLevel"]))
            {
                param[6] = new SqlParameter("@CustomerLevel", queryDic["CustomerLevel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CustomerLevel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CompanyNatureCode") && !string.IsNullOrWhiteSpace(queryDic["CompanyNatureCode"]))
            {
                param[7] = new SqlParameter("@CompanyNatureCode", queryDic["CompanyNatureCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@CompanyNatureCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[8] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@CustomerFunnel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("Package") && !string.IsNullOrWhiteSpace(queryDic["Package"]))
            {
                param[9] = new SqlParameter("@Package", queryDic["Package"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[9] = new SqlParameter("@Package", "");
            }
            if (queryDic != null && queryDic.ContainsKey("IsCertification") && !string.IsNullOrWhiteSpace(queryDic["IsCertification"]))
            {
                param[10] = new SqlParameter("@IsCertification", queryDic["IsCertification"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[10] = new SqlParameter("@IsCertification", "");
            }
            if (queryDic != null && queryDic.ContainsKey("ReleaseState") && !string.IsNullOrWhiteSpace(queryDic["ReleaseState"]))
            {
                param[11] = new SqlParameter("@ReleaseState", queryDic["ReleaseState"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[11] = new SqlParameter("@ReleaseState", "");
            }
            if (queryDic != null && queryDic.ContainsKey("NotFollow") && !string.IsNullOrWhiteSpace(queryDic["NotFollow"]))
            {
                param[12] = new SqlParameter("@NotFollow", queryDic["NotFollow"]);
            }
            else
            {
                param[12] = new SqlParameter("@NotFollow", "");
            }
            //分享过来
            if (queryDic != null && queryDic.ContainsKey("GiveMe") && !string.IsNullOrWhiteSpace(queryDic["GiveMe"]))
            {
                param[13] = new SqlParameter("@GiveMe", queryDic["GiveMe"]);
            }
            else
            {
                param[13] = new SqlParameter("@GiveMe", "");
            }
            //分享出去
            if (queryDic != null && queryDic.ContainsKey("ToPerson") && !string.IsNullOrWhiteSpace(queryDic["ToPerson"]))
            {
                param[14] = new SqlParameter("@ToPerson", queryDic["ToPerson"]);
            }
            else
            {
                param[14] = new SqlParameter("@ToPerson", "");
            }
            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[15] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[15] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[16] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[16] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[16] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[17] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[17] = new SqlParameter("@SearchPersonID", "");
            }

            //推荐方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[18] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[18] = new SqlParameter("@RecommendSolutionID", "");
            }
            //未联系客户时长
            if (queryDic != null && queryDic.ContainsKey("NoContact") && !string.IsNullOrWhiteSpace(queryDic["NoContact"]))
            {
                param[19] = new SqlParameter("@NoContact", queryDic["NoContact"]);
            }
            else
            {
                param[19] = new SqlParameter("@NoContact", "");
            }
            #endregion

            param[20] = new SqlParameter("@sys_PageIndex", page);
            param[21] = new SqlParameter("@sys_PageSize", rows);
            param[22] = new SqlParameter("@PCount", SqlDbType.Int);
            param[23] = new SqlParameter("@RCount", SqlDbType.Int);

            param[22].Direction = ParameterDirection.Output;
            param[23].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerModel>("exec [dbo].[P_Customer_S]  @CustomerName,@CityCode,@VocationCode,@DateType,@StartDate,@EndDate,@CustomerLevel,@CompanyNatureCode,@CustomerFunnel,@Package,@IsCertification,@ReleaseState,@NotFollow,@GiveMe,@ToPerson,@FindType,@SysPersonID,@SearchPersonID,@RecommendSolutionID,@NoContact,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[23].Value;

            return data;

        }

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerModel> GetCustomerData_KA(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[24];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[1] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("VocationCode") && !string.IsNullOrWhiteSpace(queryDic["VocationCode"]))
            {
                param[2] = new SqlParameter("@VocationCode", queryDic["VocationCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@VocationCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("DateType") && !string.IsNullOrWhiteSpace(queryDic["DateType"]))
            {
                param[3] = new SqlParameter("@DateType", queryDic["DateType"]);
            }
            else
            {
                param[3] = new SqlParameter("@DateType", "");
            }
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[4] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[5] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[5] = new SqlParameter("@EndDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerLevel") && !string.IsNullOrWhiteSpace(queryDic["CustomerLevel"]))
            {
                param[6] = new SqlParameter("@CustomerLevel", queryDic["CustomerLevel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CustomerLevel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CompanyNatureCode") && !string.IsNullOrWhiteSpace(queryDic["CompanyNatureCode"]))
            {
                param[7] = new SqlParameter("@CompanyNatureCode", queryDic["CompanyNatureCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@CompanyNatureCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[8] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@CustomerFunnel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("Package") && !string.IsNullOrWhiteSpace(queryDic["Package"]))
            {
                param[9] = new SqlParameter("@Package", queryDic["Package"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[9] = new SqlParameter("@Package", "");
            }
            if (queryDic != null && queryDic.ContainsKey("IsCertification") && !string.IsNullOrWhiteSpace(queryDic["IsCertification"]))
            {
                param[10] = new SqlParameter("@IsCertification", queryDic["IsCertification"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[10] = new SqlParameter("@IsCertification", "");
            }
            if (queryDic != null && queryDic.ContainsKey("ReleaseState") && !string.IsNullOrWhiteSpace(queryDic["ReleaseState"]))
            {
                param[11] = new SqlParameter("@ReleaseState", queryDic["ReleaseState"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[11] = new SqlParameter("@ReleaseState", "");
            }
            if (queryDic != null && queryDic.ContainsKey("NotFollow") && !string.IsNullOrWhiteSpace(queryDic["NotFollow"]))
            {
                param[12] = new SqlParameter("@NotFollow", queryDic["NotFollow"]);
            }
            else
            {
                param[12] = new SqlParameter("@NotFollow", "");
            }
            //分享过来
            if (queryDic != null && queryDic.ContainsKey("GiveMe") && !string.IsNullOrWhiteSpace(queryDic["GiveMe"]))
            {
                param[13] = new SqlParameter("@GiveMe", queryDic["GiveMe"]);
            }
            else
            {
                param[13] = new SqlParameter("@GiveMe", "");
            }
            //分享出去
            if (queryDic != null && queryDic.ContainsKey("ToPerson") && !string.IsNullOrWhiteSpace(queryDic["ToPerson"]))
            {
                param[14] = new SqlParameter("@ToPerson", queryDic["ToPerson"]);
            }
            else
            {
                param[14] = new SqlParameter("@ToPerson", "");
            }
            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[15] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[15] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[16] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[16] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[16] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[17] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[17] = new SqlParameter("@SearchPersonID", "");
            }

            //推荐方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[18] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[18] = new SqlParameter("@RecommendSolutionID", "");
            }
            //未联系客户时长
            if (queryDic != null && queryDic.ContainsKey("NoContact") && !string.IsNullOrWhiteSpace(queryDic["NoContact"]))
            {
                param[19] = new SqlParameter("@NoContact", queryDic["NoContact"]);
            }
            else
            {
                param[19] = new SqlParameter("@NoContact", "");
            }
            #endregion

            param[20] = new SqlParameter("@sys_PageIndex", page);
            param[21] = new SqlParameter("@sys_PageSize", rows);
            param[22] = new SqlParameter("@PCount", SqlDbType.Int);
            param[23] = new SqlParameter("@RCount", SqlDbType.Int);

            param[22].Direction = ParameterDirection.Output;
            param[23].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerModel>("exec [dbo].[P_Customer_S_KA]  @CustomerName,@CityCode,@VocationCode,@DateType,@StartDate,@EndDate,@CustomerLevel,@CompanyNatureCode,@CustomerFunnel,@Package,@IsCertification,@ReleaseState,@NotFollow,@GiveMe,@ToPerson,@FindType,@SysPersonID,@SearchPersonID,@RecommendSolutionID,@NoContact,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[23].Value;

            return data;

        }

        /// <summary>
        /// 我的客户列表查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        public List<CustomerModel> GetCustomerData_Self(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[20];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[1] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("VocationCode") && !string.IsNullOrWhiteSpace(queryDic["VocationCode"]))
            {
                param[2] = new SqlParameter("@VocationCode", queryDic["VocationCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@VocationCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("DateType") && !string.IsNullOrWhiteSpace(queryDic["DateType"]))
            {
                param[3] = new SqlParameter("@DateType", queryDic["DateType"]);
            }
            else
            {
                param[3] = new SqlParameter("@DateType", "");
            }
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[4] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[5] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[5] = new SqlParameter("@EndDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerLevel") && !string.IsNullOrWhiteSpace(queryDic["CustomerLevel"]))
            {
                param[6] = new SqlParameter("@CustomerLevel", queryDic["CustomerLevel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CustomerLevel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CompanyNatureCode") && !string.IsNullOrWhiteSpace(queryDic["CompanyNatureCode"]))
            {
                param[7] = new SqlParameter("@CompanyNatureCode", queryDic["CompanyNatureCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@CompanyNatureCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[8] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@CustomerFunnel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("Package") && !string.IsNullOrWhiteSpace(queryDic["Package"]))
            {
                param[9] = new SqlParameter("@Package", queryDic["Package"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[9] = new SqlParameter("@Package", "");
            }


            if (queryDic != null && queryDic.ContainsKey("NotFollow") && !string.IsNullOrWhiteSpace(queryDic["NotFollow"]))
            {
                param[10] = new SqlParameter("@NotFollow", queryDic["NotFollow"]);
            }
            else
            {
                param[10] = new SqlParameter("@NotFollow", "");
            }
            //分享过来
            if (queryDic != null && queryDic.ContainsKey("GiveMe") && !string.IsNullOrWhiteSpace(queryDic["GiveMe"]))
            {
                param[11] = new SqlParameter("@GiveMe", queryDic["GiveMe"]);
            }
            else
            {
                param[11] = new SqlParameter("@GiveMe", "");
            }
            //分享出去
            if (queryDic != null && queryDic.ContainsKey("ToPerson") && !string.IsNullOrWhiteSpace(queryDic["ToPerson"]))
            {
                param[12] = new SqlParameter("@ToPerson", queryDic["ToPerson"]);
            }
            else
            {
                param[12] = new SqlParameter("@ToPerson", "");
            }



            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[13] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[13] = new SqlParameter("@SearchPersonID", "");
            }

            //推荐方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[14] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[14] = new SqlParameter("@RecommendSolutionID", "");
            }
            //未联系客户时长
            if (queryDic != null && queryDic.ContainsKey("NoContact") && !string.IsNullOrWhiteSpace(queryDic["NoContact"]))
            {
                param[15] = new SqlParameter("@NoContact", queryDic["NoContact"]);
            }
            else
            {
                param[15] = new SqlParameter("@NoContact", "");
            }
            #endregion

            param[16] = new SqlParameter("@sys_PageIndex", page);
            param[17] = new SqlParameter("@sys_PageSize", rows);
            param[18] = new SqlParameter("@PCount", SqlDbType.Int);
            param[19] = new SqlParameter("@RCount", SqlDbType.Int);

            param[18].Direction = ParameterDirection.Output;
            param[19].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerModel>("exec [dbo].[P_Customer_S_Self]  @CustomerName,@CityCode,@VocationCode,@DateType,@StartDate,@EndDate,@CustomerLevel,@CompanyNatureCode,@CustomerFunnel,@Package,@NotFollow,@GiveMe,@ToPerson,@SearchPersonID,@RecommendSolutionID,@NoContact,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[19].Value;

            return data;

        }



        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerModel> GetPublicData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[16];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[1] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("VocationCode") && !string.IsNullOrWhiteSpace(queryDic["VocationCode"]))
            {
                param[2] = new SqlParameter("@VocationCode", queryDic["VocationCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@VocationCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[3] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[3] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[4] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@EndDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerLevel") && !string.IsNullOrWhiteSpace(queryDic["CustomerLevel"]))
            {
                param[5] = new SqlParameter("@CustomerLevel", queryDic["CustomerLevel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[5] = new SqlParameter("@CustomerLevel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CompanyNatureCode") && !string.IsNullOrWhiteSpace(queryDic["CompanyNatureCode"]))
            {
                param[6] = new SqlParameter("@CompanyNatureCode", queryDic["CompanyNatureCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CompanyNatureCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[7] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@CustomerFunnel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("Package") && !string.IsNullOrWhiteSpace(queryDic["Package"]))
            {
                param[8] = new SqlParameter("@Package", queryDic["Package"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@Package", "");
            }
            if (queryDic != null && queryDic.ContainsKey("IsCertification") && !string.IsNullOrWhiteSpace(queryDic["IsCertification"]))
            {
                param[9] = new SqlParameter("@IsCertification", queryDic["IsCertification"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[9] = new SqlParameter("@IsCertification", "");
            }
            if (queryDic != null && queryDic.ContainsKey("ReleaseState") && !string.IsNullOrWhiteSpace(queryDic["ReleaseState"]))
            {
                param[10] = new SqlParameter("@ReleaseState", queryDic["ReleaseState"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[10] = new SqlParameter("@ReleaseState", "");
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                param[11] = new SqlParameter("@SysPersonId", id);
            }
            else if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[11] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[11] = new SqlParameter("@SysPersonId", "");
            }

            #endregion

            param[12] = new SqlParameter("@sys_PageIndex", page);
            param[13] = new SqlParameter("@sys_PageSize", rows);
            param[14] = new SqlParameter("@PCount", SqlDbType.Int);
            param[15] = new SqlParameter("@RCount", SqlDbType.Int);

            param[14].Direction = ParameterDirection.Output;
            param[15].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerModel>("exec [dbo].[P_CustomerPublic_s]  @CustomerName,@CityCode,@VocationCode,@StartDate,@EndDate,@CustomerLevel,@CompanyNatureCode,@CustomerFunnel,@Package,@IsCertification,@ReleaseState,@SysPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[15].Value;

            return data;

        }



        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<TB_Customer> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            string where = string.Empty;
            int flagWhere = 0;

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            if (queryDic != null && queryDic.Count > 0)
            {
                foreach (var item in queryDic)
                {
                    if (flagWhere != 0)
                    {
                        where += " and ";
                    }
                    flagWhere++;


                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Time)) //开始时间
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(Start_Time)) + "] >=  CAST('" + item.Value + "' as   System.DateTime)";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Time)) //结束时间+1
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(End_Time)) + "] <  CAST('" + Convert.ToDateTime(item.Value).AddDays(1) + "' as   System.DateTime)";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Int)) //开始数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(Start_Int)) + "] >= " + item.Value.GetInt();
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Int)) //结束数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(End_Int)) + "] <= " + item.Value.GetInt();
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_Int)) //精确查询数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(DDL_Int)) + "] =" + item.Value;
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_String)) //精确查询字符串
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(DDL_String)) + "] = '" + item.Value + "'";
                        continue;
                    }
                    where += "it.[" + item.Key + "] like '%" + item.Value + "%'";//模糊查询
                }
            }
            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                     .CreateObjectSet<TB_Customer>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }



        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_Customer GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_Customer GetById(DB_CRMEntities db, string id)
        {
            return db.TB_Customer.SingleOrDefault(s => s.CustomerID == id);
        }
        #endregion

        #region 通过主键删除对象
        /// <summary>
        /// 确定删除一个对象，调用Save方法
        /// </summary>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>    
        public int Delete(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                this.Delete(db, id);
                return 1;
            }
        }
        /// <summary>
        /// 删除一个合同
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条角色的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            TB_Customer deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public void Delete(DB_CRMEntities db, string[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<TB_Customer> collection = from f in db.TB_Customer
                                                 where deleteCollection.Contains(f.CustomerID)
                                                 select f;
            foreach (var deleteItem in collection)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        #endregion

        #region 审核客户管理

        /// <summary>
        /// 审核客户管理
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerModel> GetCustomerAuditData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[7];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("AuditStatus") && !string.IsNullOrWhiteSpace(queryDic["AuditStatus"]))
            {
                param[1] = new SqlParameter("@AuditStatus", queryDic["AuditStatus"]);
            }
            else
            {
                param[1] = new SqlParameter("@AuditStatus", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[2] = new SqlParameter("@SysPersonID", queryDic["SysPersonID"]);
            }
            else
            {
                param[2] = new SqlParameter("@SysPersonID", "");
            }



            #endregion

            param[3] = new SqlParameter("@sys_PageIndex", page);
            param[4] = new SqlParameter("@sys_PageSize", rows);
            param[5] = new SqlParameter("@PCount", SqlDbType.Int);
            param[6] = new SqlParameter("@RCount", SqlDbType.Int);

            param[5].Direction = ParameterDirection.Output;
            param[6].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerModel>("exec [dbo].[P_CustomerAuditData_s]  @CustomerName,@AuditStatus,@SysPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[6].Value;

            return data;

        }

        #endregion

        #region 获取客户操作权限  create by chand 2015-10-23
        /// <summary>
        /// 获取客户操作权限
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        /// -1没有操作权限 
        /// 0 客户最大操作权限 
        /// 1 只读 
        /// 2 可以编辑权限
        /// </returns>
        public int GetCustomerAuthority(DB_CRMEntities db, string CustomerID, string SysPersonID)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerID", CustomerID);

            param[1] = new SqlParameter("@SysPersonID", SysPersonID);

            var data = db.Database.SqlQuery<int>("exec [dbo].[P_CustomerAuthority_S]  @CustomerID,@SysPersonID", param).ToList();
            if (data != null && data.Count > 0)
            {
                return data[0];
            }
            return -1;
        }

        #endregion

        #region 客户进程统计 create by chand 2015-12-07
        /// <summary>
        /// 客户进程统计
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFunnel> GetFunnelStatistics(DB_CRMEntities db, string SearchPersonID, string id, string search)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[19];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[1] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("VocationCode") && !string.IsNullOrWhiteSpace(queryDic["VocationCode"]))
            {
                param[2] = new SqlParameter("@VocationCode", queryDic["VocationCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@VocationCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("DateType") && !string.IsNullOrWhiteSpace(queryDic["DateType"]))
            {
                param[3] = new SqlParameter("@DateType", queryDic["DateType"]);
            }
            else
            {
                param[3] = new SqlParameter("@DateType", "");
            }
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[4] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[5] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[5] = new SqlParameter("@EndDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerLevel") && !string.IsNullOrWhiteSpace(queryDic["CustomerLevel"]))
            {
                param[6] = new SqlParameter("@CustomerLevel", queryDic["CustomerLevel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CustomerLevel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CompanyNatureCode") && !string.IsNullOrWhiteSpace(queryDic["CompanyNatureCode"]))
            {
                param[7] = new SqlParameter("@CompanyNatureCode", queryDic["CompanyNatureCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@CompanyNatureCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[8] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@CustomerFunnel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("Package") && !string.IsNullOrWhiteSpace(queryDic["Package"]))
            {
                param[9] = new SqlParameter("@Package", queryDic["Package"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[9] = new SqlParameter("@Package", "");
            }
            if (queryDic != null && queryDic.ContainsKey("IsCertification") && !string.IsNullOrWhiteSpace(queryDic["IsCertification"]))
            {
                param[10] = new SqlParameter("@IsCertification", queryDic["IsCertification"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[10] = new SqlParameter("@IsCertification", "");
            }
            if (queryDic != null && queryDic.ContainsKey("ReleaseState") && !string.IsNullOrWhiteSpace(queryDic["ReleaseState"]))
            {
                param[11] = new SqlParameter("@ReleaseState", queryDic["ReleaseState"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[11] = new SqlParameter("@ReleaseState", "");
            }
            if (queryDic != null && queryDic.ContainsKey("NotFollow") && !string.IsNullOrWhiteSpace(queryDic["NotFollow"]))
            {
                param[12] = new SqlParameter("@NotFollow", queryDic["NotFollow"]);
            }
            else
            {
                param[12] = new SqlParameter("@NotFollow", "");
            }
            //分享过来
            if (queryDic != null && queryDic.ContainsKey("GiveMe") && !string.IsNullOrWhiteSpace(queryDic["GiveMe"]))
            {
                param[13] = new SqlParameter("@GiveMe", queryDic["GiveMe"]);
            }
            else
            {
                param[13] = new SqlParameter("@GiveMe", "");
            }
            //分享出去
            if (queryDic != null && queryDic.ContainsKey("ToPerson") && !string.IsNullOrWhiteSpace(queryDic["ToPerson"]))
            {
                param[14] = new SqlParameter("@ToPerson", queryDic["ToPerson"]);
            }
            else
            {
                param[14] = new SqlParameter("@ToPerson", "");
            }
            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[15] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[15] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[16] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[16] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[16] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[17] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[17] = new SqlParameter("@SearchPersonID", "");
            }

            //推荐方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[18] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[18] = new SqlParameter("@RecommendSolutionID", "");
            }
            #endregion

            var data = db.Database.SqlQuery<CustomerFunnel>("exec [dbo].[P_CustomerFunnel_Statistics]  @CustomerName,@CityCode,@VocationCode,@DateType,@StartDate,@EndDate,@CustomerLevel,@CompanyNatureCode,@CustomerFunnel,@Package,@IsCertification,@ReleaseState,@NotFollow,@GiveMe,@ToPerson,@FindType,@SysPersonID,@SearchPersonID,@RecommendSolutionID", param).ToList();


            return data;

        }


        #endregion


        #region 获取客户长时间未联系数量 create by chand 2015-12-08
        /// <summary>
        /// 获取客户长时间未联系数量
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public long GetCustomerNoContactQty(DB_CRMEntities db, string SearchPersonID, string id, string search)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[6];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("NotFollow") && !string.IsNullOrWhiteSpace(queryDic["NotFollow"]))
            {
                param[0] = new SqlParameter("@NotFollow", queryDic["NotFollow"]);
            }
            else
            {
                param[0] = new SqlParameter("@NotFollow", "");
            }
            //分享过来
            if (queryDic != null && queryDic.ContainsKey("GiveMe") && !string.IsNullOrWhiteSpace(queryDic["GiveMe"]))
            {
                param[1] = new SqlParameter("@GiveMe", queryDic["GiveMe"]);
            }
            else
            {
                param[1] = new SqlParameter("@GiveMe", "");
            }
            //分享出去
            if (queryDic != null && queryDic.ContainsKey("ToPerson") && !string.IsNullOrWhiteSpace(queryDic["ToPerson"]))
            {
                param[2] = new SqlParameter("@ToPerson", queryDic["ToPerson"]);
            }
            else
            {
                param[2] = new SqlParameter("@ToPerson", "");
            }
            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[3] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[3] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[4] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[4] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[4] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[5] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[5] = new SqlParameter("@SearchPersonID", "");
            }


            #endregion
            Type t = typeof(int);
            var data = db.Database.SqlQuery(t, "exec [dbo].[P_CustomerNoContactQty_S] @NotFollow,@GiveMe,@ToPerson,@FindType,@SysPersonID,@SearchPersonID", param).Cast<int>().First();


            return data;

        }


        #endregion

        #region 客户释放  create by chand 2015-12-16
        /// <summary>
        /// 客户释放
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        /// 0 释放失败   1 释放成功
        /// </returns>
        public int GetCustomerRelease(DB_CRMEntities db, string CustomerID, string SysPersonID)
        {

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerID", CustomerID);

            param[1] = new SqlParameter("@SysPersonID", SysPersonID);

            param[2] = new SqlParameter("@AttributionChangeID", Result.GetNewId());

            var data = db.Database.SqlQuery<int>("exec [dbo].[P_CustomerRelease_U]  @CustomerID,@SysPersonID,@AttributionChangeID", param).ToList();
            if (data != null && data.Count > 0)
            {
                return data[0];
            }
            return -1;
        }

        #endregion

        #region 客户报表  create  by chand 2016-2-21

        /// <summary>
        /// 客户汇总数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerSummaryModel> GetCustomerSummary(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[11];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }

            //跟进类型
            if (queryDic != null && queryDic.ContainsKey("FollowType") && !string.IsNullOrWhiteSpace(queryDic["FollowType"]))
            {
                param[2] = new SqlParameter("@FollowType", queryDic["FollowType"]);
            }
            else
            {
                param[2] = new SqlParameter("@FollowType", "");
            }
            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[3] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[3] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[4] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[4] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[4] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[5] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[5] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion
            if (string.IsNullOrWhiteSpace(sort))
            {
                param[6] = new SqlParameter("@order", "");
            }
            else
            {
                param[6] = new SqlParameter("@order", sort + " " + order);
            }


            param[7] = new SqlParameter("@sys_PageIndex", page);
            param[8] = new SqlParameter("@sys_PageSize", rows);
            param[9] = new SqlParameter("@PCount", SqlDbType.Int);
            param[10] = new SqlParameter("@RCount", SqlDbType.Int);

            param[9].Direction = ParameterDirection.Output;
            param[10].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerSummaryModel>("exec [dbo].[P_CustomerSummary_Report] @StartDate,@EndDate,@FollowType,@FindType,@SysPersonID,@SearchPersonID,@order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[10].Value;

            return data;

        }

        /// <summary>
        /// 客户部门汇总数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerSummaryModel> GetCustomerSummaryDept(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[7];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }

            #endregion
            if (string.IsNullOrWhiteSpace(sort))
            {
                param[2] = new SqlParameter("@order", "");
            }
            else
            {
                param[2] = new SqlParameter("@order", sort + " " + order);
            }


            param[3] = new SqlParameter("@sys_PageIndex", page);
            param[4] = new SqlParameter("@sys_PageSize", rows);
            param[5] = new SqlParameter("@PCount", SqlDbType.Int);
            param[6] = new SqlParameter("@RCount", SqlDbType.Int);

            param[5].Direction = ParameterDirection.Output;
            param[6].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerSummaryModel>("exec [dbo].[P_CustomerSummaryDept_Report] @StartDate,@EndDate,@order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[6].Value;

            return data;

        }


        /// <summary>
        /// 客户进程数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerFollowReportModel> GetCustomerFollow_Report(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[10];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[2] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[2] = new SqlParameter("@CustomerFunnel", "");
            }

            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[3] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[3] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[4] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[4] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[4] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[5] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[5] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion

            param[6] = new SqlParameter("@sys_PageIndex", page);
            param[7] = new SqlParameter("@sys_PageSize", rows);
            param[8] = new SqlParameter("@PCount", SqlDbType.Int);
            param[9] = new SqlParameter("@RCount", SqlDbType.Int);

            param[8].Direction = ParameterDirection.Output;
            param[9].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerFollowReportModel>("exec [dbo].[P_CustomerFollow_Report] @StartDate,@EndDate,@CustomerFunnel,@FindType,@SysPersonID,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[9].Value;

            return data;

        }
        /// <summary>
        /// 客户拜访数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerVisitReportModel> GetCustomerVisit_Report(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[10];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[2] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[2] = new SqlParameter("@CustomerFunnel", "");
            }

            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[3] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[3] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[4] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[4] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[4] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[5] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[5] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion

            param[6] = new SqlParameter("@sys_PageIndex", page);
            param[7] = new SqlParameter("@sys_PageSize", rows);
            param[8] = new SqlParameter("@PCount", SqlDbType.Int);
            param[9] = new SqlParameter("@RCount", SqlDbType.Int);

            param[8].Direction = ParameterDirection.Output;
            param[9].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerVisitReportModel>("exec [dbo].[P_CustomerVisit_Report] @StartDate,@EndDate,@CustomerFunnel,@FindType,@SysPersonID,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[9].Value;

            return data;

        }

        /// <summary>
        /// 客户拜访数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerNewFollowReportModel> GetCustomerNewFollow_Report(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[9];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }


            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[2] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[2] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[3] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[3] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[3] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[4] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[4] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion

            param[5] = new SqlParameter("@sys_PageIndex", page);
            param[6] = new SqlParameter("@sys_PageSize", rows);
            param[7] = new SqlParameter("@PCount", SqlDbType.Int);
            param[8] = new SqlParameter("@RCount", SqlDbType.Int);

            param[7].Direction = ParameterDirection.Output;
            param[8].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerNewFollowReportModel>("exec [dbo].[P_CustomerNewFollow_Report] @StartDate,@EndDate,@FindType,@SysPersonID,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[8].Value;

            return data;

        }

        /// <summary>
        /// 客户进程
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerProcessReportModel> GetCustomerProcess_Report(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[10];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[2] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[2] = new SqlParameter("@CustomerFunnel", "");
            }

            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[3] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[3] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[4] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[4] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[4] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[5] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[5] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion

            param[6] = new SqlParameter("@sys_PageIndex", page);
            param[7] = new SqlParameter("@sys_PageSize", rows);
            param[8] = new SqlParameter("@PCount", SqlDbType.Int);
            param[9] = new SqlParameter("@RCount", SqlDbType.Int);

            param[8].Direction = ParameterDirection.Output;
            param[9].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerProcessReportModel>("exec [dbo].[P_CustomerProcess_Report] @StartDate,@EndDate,@CustomerFunnel,@FindType,@SysPersonID,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[9].Value;

            return data;

        }



        /// <summary>
        /// 客户进程
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerReleaseReportModel> GetCustomerRelease_Report(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[8];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("Type") && !string.IsNullOrWhiteSpace(queryDic["Type"]))
            {
                param[2] = new SqlParameter("@Type", queryDic["Type"]);
            }
            else
            {
                param[2] = new SqlParameter("@Type", "1");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[3] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[3] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion

            param[4] = new SqlParameter("@sys_PageIndex", page);
            param[5] = new SqlParameter("@sys_PageSize", rows);
            param[6] = new SqlParameter("@PCount", SqlDbType.Int);
            param[7] = new SqlParameter("@RCount", SqlDbType.Int);

            param[6].Direction = ParameterDirection.Output;
            param[7].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerReleaseReportModel>("exec [dbo].[P_CustomerRelease_Report] @StartDate,@EndDate,@Type,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[7].Value;

            return data;

        }




        /// <summary>
        /// 客户提取报表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerExtractReportModel> GetCustomerExtract_Report(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[12];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }


            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[1] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@StartDate", "");
            }


            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[2] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[2] = new SqlParameter("@EndDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ReleaseStartDate") && !string.IsNullOrWhiteSpace(queryDic["ReleaseStartDate"]))
            {
                param[3] = new SqlParameter("@ReleaseStartDate", queryDic["ReleaseStartDate"]);
            }
            else
            {
                param[3] = new SqlParameter("@ReleaseStartDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ReleaseEndDate") && !string.IsNullOrWhiteSpace(queryDic["ReleaseEndDate"]))
            {
                param[4] = new SqlParameter("@ReleaseEndDate", queryDic["ReleaseEndDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@ReleaseEndDate", "");
            }


            if (queryDic != null && queryDic.ContainsKey("SearchType") && !string.IsNullOrWhiteSpace(queryDic["FindMode"]))
            {
                param[5] = new SqlParameter("@FindMode", queryDic["FindMode"]);
            }
            else
            {
                param[5] = new SqlParameter("@FindMode", "0");
            }

            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[6] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[6] = new SqlParameter("@FindType", "0");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[7] = new SqlParameter("@SysPersonID", queryDic["SysPersonID"]);
            }
            else
            {
                param[6] = new SqlParameter("@FindType", "0");
                param[7] = new SqlParameter("@SysPersonID", id);
            }

            #endregion

            param[8] = new SqlParameter("@sys_PageIndex", page);
            param[9] = new SqlParameter("@sys_PageSize", rows);
            param[10] = new SqlParameter("@PCount", SqlDbType.Int);
            param[11] = new SqlParameter("@RCount", SqlDbType.Int);

            param[10].Direction = ParameterDirection.Output;
            param[11].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerExtractReportModel>("exec [dbo].[P_CustomerExtract_Report]  @CustomerName,@StartDate,@EndDate,@ReleaseStartDate,@ReleaseEndDate,@FindMode,@FindType,@SysPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[11].Value;

            return data;

        }




        #endregion

        #region 获取无效客户 create by chand 2016-02-25

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerModel> GetLogoffData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[16];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[1] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("VocationCode") && !string.IsNullOrWhiteSpace(queryDic["VocationCode"]))
            {
                param[2] = new SqlParameter("@VocationCode", queryDic["VocationCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@VocationCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[3] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[3] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[4] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@EndDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerLevel") && !string.IsNullOrWhiteSpace(queryDic["CustomerLevel"]))
            {
                param[5] = new SqlParameter("@CustomerLevel", queryDic["CustomerLevel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[5] = new SqlParameter("@CustomerLevel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CompanyNatureCode") && !string.IsNullOrWhiteSpace(queryDic["CompanyNatureCode"]))
            {
                param[6] = new SqlParameter("@CompanyNatureCode", queryDic["CompanyNatureCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CompanyNatureCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[7] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@CustomerFunnel", "");
            }
            if (queryDic != null && queryDic.ContainsKey("Package") && !string.IsNullOrWhiteSpace(queryDic["Package"]))
            {
                param[8] = new SqlParameter("@Package", queryDic["Package"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@Package", "");
            }
            if (queryDic != null && queryDic.ContainsKey("IsCertification") && !string.IsNullOrWhiteSpace(queryDic["IsCertification"]))
            {
                param[9] = new SqlParameter("@IsCertification", queryDic["IsCertification"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[9] = new SqlParameter("@IsCertification", "");
            }
            if (queryDic != null && queryDic.ContainsKey("ReleaseState") && !string.IsNullOrWhiteSpace(queryDic["ReleaseState"]))
            {
                param[10] = new SqlParameter("@ReleaseState", queryDic["ReleaseState"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[10] = new SqlParameter("@ReleaseState", "");
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                param[11] = new SqlParameter("@SysPersonId", id);
            }
            else if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[11] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[11] = new SqlParameter("@SysPersonId", "");
            }

            #endregion

            param[12] = new SqlParameter("@sys_PageIndex", page);
            param[13] = new SqlParameter("@sys_PageSize", rows);
            param[14] = new SqlParameter("@PCount", SqlDbType.Int);
            param[15] = new SqlParameter("@RCount", SqlDbType.Int);

            param[14].Direction = ParameterDirection.Output;
            param[15].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerModel>("exec [dbo].[P_CustomerLogoff_s]  @CustomerName,@CityCode,@VocationCode,@StartDate,@EndDate,@CustomerLevel,@CompanyNatureCode,@CustomerFunnel,@Package,@IsCertification,@ReleaseState,@SysPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[15].Value;

            return data;

        }

        #endregion


        #region 客户导入汇总报表  create by chand 2016-08-17
        /// <summary>
        /// 客户导入汇总报表
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public List<ImportSummaryReportModel> GetImportSummary_Report(DB_CRMEntities db, string sort, string order)
        {
            SqlParameter[] param = new SqlParameter[1];
            if (string.IsNullOrWhiteSpace(sort))
            {
                param[0] = new SqlParameter("@order", "");
            }
            else
            {
                param[0] = new SqlParameter("@order", sort + " " + order);
            }


            var data = db.Database.SqlQuery<ImportSummaryReportModel>("exec [dbo].[P_ImportSummary_Report] @order", param).ToList();
            return data;
        }
        #endregion

        /// <summary>
        /// 根据客户名称获取
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerName"></param>
        /// <returns></returns>
        public TB_Customer GetByName(DB_CRMEntities db, string CustomerName)
        {
            try
            {

                return db.TB_Customer.SingleOrDefault(s => s.CustomerName == CustomerName);
            }
            catch (Exception ex)
            {

            }
            return null;
        }



        #region 客服自己能看到的数据 create by chand 2016-08-29

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerServiceModel> GetCustomerServiceList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            SqlParameter[] param = new SqlParameter[7];

            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }


            if (queryDic != null && queryDic.ContainsKey("SearchPersonID") && !string.IsNullOrWhiteSpace(queryDic["SearchPersonID"]))
            {
                param[1] = new SqlParameter("@SearchPersonID", queryDic["SearchPersonID"]);
            }
            else
            {
                param[1] = new SqlParameter("@SearchPersonID", "");
            }


            if (string.IsNullOrWhiteSpace(sort))
            {
                param[2] = new SqlParameter("@order", "");
            }
            else
            {
                param[2] = new SqlParameter("@order", sort + " " + order);
            }



            param[3] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[3].Value = page;
            param[4] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[4].Value = rows;
            param[5] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[6] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[5].Direction = ParameterDirection.Output;
            param[6].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<CustomerServiceModel>("exec P_CustomerServiceList_S   @CustomerName,@SearchPersonID,@Order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[6].Value);
            return query;
        }
        #endregion



        #region 客服自己能看到的数据 create by chand 2016-08-29

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerServiceModel> GetCustomerSelectedList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            SqlParameter[] param = new SqlParameter[8];

            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SeachType") && !string.IsNullOrWhiteSpace(queryDic["SeachType"]))
            {
                param[1] = new SqlParameter("@SeachType", queryDic["SeachType"]);
            }
            else
            {
                param[1] = new SqlParameter("@SeachType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SearchPersonID") && !string.IsNullOrWhiteSpace(queryDic["SearchPersonID"]))
            {
                param[2] = new SqlParameter("@SearchPersonID", queryDic["SearchPersonID"]);
            }
            else
            {
                param[2] = new SqlParameter("@SearchPersonID", "");
            }


            if (string.IsNullOrWhiteSpace(sort))
            {
                param[3] = new SqlParameter("@order", "");
            }
            else
            {
                param[3] = new SqlParameter("@order", sort + " " + order);
            }



            param[4] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[4].Value = page;
            param[5] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[5].Value = rows;
            param[6] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[7] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[6].Direction = ParameterDirection.Output;
            param[7].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<CustomerServiceModel>("exec P_CustomerSelected_S   @CustomerName,@SeachType,@SearchPersonID,@Order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[7].Value);
            return query;
        }
        #endregion

        /// <summary>
        /// 获取HR考勤记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="contractType"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<HRListEntity> GetHRListInfo(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, string contractType, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[9];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }
            
            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[2] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[2] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[3] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[3] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[3] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[4] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[4] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion
   
            param[5] = new SqlParameter("@sys_PageIndex", page);
            param[6] = new SqlParameter("@sys_PageSize", rows);
            param[7] = new SqlParameter("@PCount", SqlDbType.Int);
            param[8] = new SqlParameter("@RCount", SqlDbType.Int);

            param[7].Direction = ParameterDirection.Output;
            param[8].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<HRListEntity>("exec [dbo].[P_HRList_S] @StartDate,@EndDate, @FindType,@SysPersonID,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[8].Value;

            return data;

        }




        public void Dispose()
        {
        }

    }
}
