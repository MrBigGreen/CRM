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
    public class TBCustomerProjectRepository : BaseRepository<TB_CustomerProject>, IDisposable
    {

        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerProject GetById(string id)
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
        public TB_CustomerProject GetById(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerProject.SingleOrDefault(s => s.CustomerProjectID == id);
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
        /// 删除一个
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条角色的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            IQueryable<TB_CustomerProject> collection = from f in db.TB_CustomerProject
                                                        where f.CustomerProjectID.Contains(id)
                                                        select f;
            foreach (var deleteItem in collection)
            {
                db.TB_CustomerProject.Remove(deleteItem);
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
            IQueryable<TB_CustomerProject> collection = from f in db.TB_CustomerProject
                                                        where deleteCollection.Contains(f.CustomerProjectID)
                                                        select f;
            foreach (var deleteItem in collection)
            {
                db.TB_CustomerProject.Remove(deleteItem);
            }
        }
        #endregion



        #region 获取项目评估数据 create by chand 2016-05-11

        /// <summary>
        /// 获取项目评估数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerProjectEntity> GetProjectData(DB_CRMEntities db, int page, int rows, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            SqlParameter[] param = new SqlParameter[13];


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

            if (queryDic != null && queryDic.ContainsKey("ProjectName") && !string.IsNullOrWhiteSpace(queryDic["ProjectName"]))
            {
                param[2] = new SqlParameter("@ProjectName", queryDic["ProjectName"]);
            }
            else
            {
                param[2] = new SqlParameter("@ProjectName", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ProjectDesc") && !string.IsNullOrWhiteSpace(queryDic["ProjectDesc"]))
            {
                param[3] = new SqlParameter("@ProjectDesc", queryDic["ProjectDesc"]);
            }
            else
            {
                param[3] = new SqlParameter("@ProjectDesc", "");
            }
            //评估结果
            if (queryDic != null && queryDic.ContainsKey("EvaluationResult") && !string.IsNullOrWhiteSpace(queryDic["EvaluationResult"]))
            {
                param[4] = new SqlParameter("@EvaluationResult", queryDic["EvaluationResult"]);
            }
            else
            {
                param[4] = new SqlParameter("@EvaluationResult", "");
            }
            //评估人
            if (queryDic != null && queryDic.ContainsKey("EvaluationPerson") && !string.IsNullOrWhiteSpace(queryDic["EvaluationPerson"]))
            {
                param[5] = new SqlParameter("@EvaluationPerson", queryDic["EvaluationPerson"]);
            }
            else
            {
                param[5] = new SqlParameter("@EvaluationPerson", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[6] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[6] = new SqlParameter("@CustomerName", "");
            }
            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[7] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[7] = new SqlParameter("@CustomerID", "");
            }
            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[8] = new SqlParameter("@SysPersonID", queryDic["SysPersonID"]);
            }
            else
            {
                param[8] = new SqlParameter("@SysPersonID", "");
            }
            param[9] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[9].Value = page;
            param[10] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[10].Value = rows;
            param[11] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[12] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[11].Direction = ParameterDirection.Output;
            param[12].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<CustomerProjectEntity>("exec P_CustomerProject_S @StartDate,@EndDate,@ProjectName,@ProjectDesc,@EvaluationResult,@EvaluationPerson,@CustomerName,@CustomerID,@SysPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[12].Value);
            return query;
        }

        #endregion
        public void Dispose()
        {

        }


    }
}
