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
    public partial class PayReportRepository : BaseRepository<TP_SalaryDetails>, IDisposable
    {
        public void Dispose()
        {
        }

        public List<PayReportEntity> GetSummaryReportList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            var param = SqlParameters(page, rows, order, sort, search);
            if (param[0].Value.ToString() == "" && param[1].Value.ToString() == "" && param[2].Value.ToString() == "")
            {
                return null;
            }
            var query = db.Database.SqlQuery<PayReportEntity>("exec P_SummaryReport_S  @CombotreeCheck,@StartDate,@EndDate,@sys_PageIndex,@sys_PageSize,@orders,@sorts,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[8].Value);
            return query;
        }

        public List<PayReportEntity> GetSocialSecurityReportList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            var param = SqlParameters(page, rows, order, sort, search);
            if (param[0].Value.ToString() == "" && param[1].Value.ToString() == "" && param[2].Value.ToString() == "")
            {
                return null;
            }
            var query = db.Database.SqlQuery<PayReportEntity>("exec P_SocialSecurityReport_S @CombotreeCheck,@StartDate,@EndDate,@sys_PageIndex,@sys_PageSize,@orders,@sorts,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[8].Value);
            return query;
        }

        public List<PayReportEntity> GetFundsReportList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            var param = SqlParameters(page, rows, order, sort, search);
            if (param[0].Value.ToString() == "" && param[1].Value.ToString() == "" && param[2].Value.ToString() == "")
            {
                return null;
            }
            var query = db.Database.SqlQuery<PayReportEntity>("exec P_FundsReport_S  @CombotreeCheck,@StartDate,@EndDate,@sys_PageIndex,@sys_PageSize,@orders,@sorts,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[8].Value);
            return query;
        }

        private static SqlParameter[] SqlParameters(int page, int rows, string order, string sort, string search)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@CombotreeCheck", SqlDbType.NVarChar, 5000);
            param[0].Value = "";
            if (queryDic != null && queryDic.ContainsKey("CombotreeCheck") &&
                !string.IsNullOrWhiteSpace(queryDic["CombotreeCheck"]))
            {
                var ck = queryDic["CombotreeCheck"].ToString();
                string checkValue = "";

                string[] cks = ck.Split(',');
                if (cks[0] == "0")
                {
                    checkValue = "0";
                }
                else
                {
                    for (int i = 0; i < cks.Length; i++)
                    {
                        checkValue += "'" + cks[i] + "',";
                    }
                    checkValue = checkValue.Substring(0, checkValue.Length - 1);
                }

                param[0] = new SqlParameter("@CombotreeCheck", checkValue);
            }
            param[1] = new SqlParameter("@StartDate", SqlDbType.NVarChar, 10);
            param[1].Value = "";
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                var sd = queryDic["StartDate"].ToString().Replace("-", "").Substring(0, 6);
                param[1] = new SqlParameter("@StartDate", sd);
            }

            param[2] = new SqlParameter("@EndDate", SqlDbType.NVarChar, 10);
            param[2].Value = "";
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                var ed = queryDic["EndDate"].ToString().Replace("-", "").Substring(0, 6);
                param[2] = new SqlParameter("@EndDate", ed);
            }

            param[3] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[3].Value = page;
            param[4] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[4].Value = rows;
            param[5] = new SqlParameter("@orders", SqlDbType.NVarChar, 50);
            param[5].Value = order;
            param[6] = new SqlParameter("@sorts", SqlDbType.NVarChar, 50);
            param[6].Value = sort;
            param[7] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[8] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[7].Direction = ParameterDirection.Output;
            param[8].Direction = ParameterDirection.Output;
            return param;
        }

        public List<PayReportEntity> GetWagesReportBankInfo(DB_CRMEntities db, string id, string checkValue, string date)
        {
            return null;
        }

        public List<PayReportEntity> GetSocialReportData(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            var param = SqlParameters(page, rows, order, sort, search);
            if (param[0].Value.ToString() == "" && param[1].Value.ToString() == "")
            {
                return null;
            }
            var query = db.Database.SqlQuery<PayReportEntity>("exec P_SocialReport_S  @CombotreeCheck,@StartDate,@EndDate,@sys_PageIndex,@sys_PageSize,@orders,@sorts,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[8].Value);
            return query;
        }
    }
}
