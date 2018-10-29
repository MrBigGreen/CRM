using Common;
using CRM.DAL.CommonEntity;
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
    /// 创建时间：2015-07-14
    /// 描述说明：呼叫记录信息
    /// </summary>
    public partial class JobInfoRepository : IDisposable
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
        public List<JobInfoEntity> GetData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[13];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@Keyword", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@Keyword", "");
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
            if (queryDic != null && queryDic.ContainsKey("JobTypeCode") && !string.IsNullOrWhiteSpace(queryDic["JobTypeCode"]))
            {
                param[3] = new SqlParameter("@JobTypeCode", queryDic["JobTypeCode"]);
            }
            else
            {
                param[3] = new SqlParameter("@JobTypeCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("WorkAddress") && !string.IsNullOrWhiteSpace(queryDic["WorkAddress"]))
            {
                param[4] = new SqlParameter("@WorkAddress", queryDic["WorkAddress"]);
            }
            else
            {
                param[4] = new SqlParameter("@WorkAddress", "");
            }
            if (queryDic != null && queryDic.ContainsKey("WorkExperenceCode") && !string.IsNullOrWhiteSpace(queryDic["WorkExperenceCode"]))
            {
                param[5] = new SqlParameter("@WorkExperenceCode", queryDic["WorkExperenceCode"]);
            }
            else
            {
                param[5] = new SqlParameter("@WorkExperenceCode", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerOwnership") && !string.IsNullOrWhiteSpace(queryDic["CustomerOwnership"]))
            {
                param[6] = new SqlParameter("@CustomerOwnership", queryDic["CustomerOwnership"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[6] = new SqlParameter("@CustomerOwnership", "");
            }
            if (queryDic != null && queryDic.ContainsKey("PositionOwnership") && !string.IsNullOrWhiteSpace(queryDic["PositionOwnership"]))
            {
                param[7] = new SqlParameter("@PositionOwnership", queryDic["PositionOwnership"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[7] = new SqlParameter("@PositionOwnership", "");
            }
            if (queryDic != null && queryDic.ContainsKey("JobCheckStatusCode") && !string.IsNullOrWhiteSpace(queryDic["JobCheckStatusCode"]))
            {
                param[8] = new SqlParameter("@JobCheckStatusCode", queryDic["JobCheckStatusCode"].Trim().TrimEnd(new char[] { ',' }));
            }
            else
            {
                param[8] = new SqlParameter("@JobCheckStatusCode", "");
            }
            
            #endregion

            param[9] = new SqlParameter("@sys_PageIndex", page);
            param[10] = new SqlParameter("@sys_PageSize", rows);
            param[11] = new SqlParameter("@PCount", SqlDbType.Int);
            param[12] = new SqlParameter("@RCount", SqlDbType.Int);

            param[11].Direction = ParameterDirection.Output;
            param[12].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<JobInfoEntity>("exec [dbo].[P_JobInfoList_S]  @Keyword,@StartDate,@EndDate,@JobTypeCode,@WorkAddress,@WorkExperenceCode,@CustomerOwnership,@PositionOwnership,@JobCheckStatusCode,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[12].Value;

            return data;

        }


        public void Dispose()
        {
        }


    }
}
