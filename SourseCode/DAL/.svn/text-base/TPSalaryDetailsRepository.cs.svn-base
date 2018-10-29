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
    public partial class TPSalaryDetailsRepository : BaseRepository<TP_SalaryDetails>, IDisposable
    {


        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TP_SalaryDetails GetById(string id)
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
        public TP_SalaryDetails GetById(DB_CRMEntities db, string id)
        {
            return db.TP_SalaryDetails.SingleOrDefault(s => s.SalaryDetailsID == id);
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
            IQueryable<TP_SalaryDetails> collection = from f in db.TP_SalaryDetails
                                                      where f.SalaryDetailsID.Contains(id)
                                                      select f;
            foreach (var deleteItem in collection)
            {
                db.TP_SalaryDetails.Remove(deleteItem);
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
            IQueryable<TP_SalaryDetails> collection = from f in db.TP_SalaryDetails
                                                      where deleteCollection.Contains(f.SalaryDetailsID)
                                                      select f;
            foreach (var deleteItem in collection)
            {
                db.TP_SalaryDetails.Remove(deleteItem);
            }
        }
        #endregion



        #region 获取薪资数据

        /// <summary>
        /// 获取薪资数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<SalaryDetailsEntity> GetSalaryDetailsData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[18];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("EmpID") && !string.IsNullOrWhiteSpace(queryDic["EmpID"]))
            {
                param[0] = new SqlParameter("@EmpID", queryDic["EmpID"]);
            }
            else
            {
                param[0] = new SqlParameter("@EmpID", "");
            }


            if (queryDic != null && queryDic.ContainsKey("EmpName") && !string.IsNullOrWhiteSpace(queryDic["EmpName"]))
            {
                param[1] = new SqlParameter("@EmpName", queryDic["EmpName"]);
            }
            else
            {
                param[1] = new SqlParameter("@EmpName", "");
            }


            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[2] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[2] = new SqlParameter("@CustomerID", "");
            }


            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[3] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[3] = new SqlParameter("@CustomerName", "");
            }



            if (queryDic != null && queryDic.ContainsKey("SalaryMonth") && !string.IsNullOrWhiteSpace(queryDic["SalaryMonth"]))
            {
                param[4] = new SqlParameter("@SalaryMonth", queryDic["SalaryMonth"]);
            }
            else
            {
                param[4] = new SqlParameter("@SalaryMonth", "");
            }

            if (queryDic != null && queryDic.ContainsKey("IDCard") && !string.IsNullOrWhiteSpace(queryDic["IDCard"]))
            {
                param[5] = new SqlParameter("@IDCard", queryDic["IDCard"]);
            }
            else
            {
                param[5] = new SqlParameter("@IDCard", "");
            }

            if (queryDic != null && queryDic.ContainsKey("BankID") && !string.IsNullOrWhiteSpace(queryDic["BankID"]))
            {
                param[6] = new SqlParameter("@BankID", queryDic["BankID"]);
            }
            else
            {
                param[6] = new SqlParameter("@BankID", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ContractSubject") && !string.IsNullOrWhiteSpace(queryDic["ContractSubject"]))
            {
                param[7] = new SqlParameter("@ContractSubject", queryDic["ContractSubject"]);
            }
            else
            {
                param[7] = new SqlParameter("@ContractSubject", "");
            }



            if (queryDic != null && queryDic.ContainsKey("WagesSubject") && !string.IsNullOrWhiteSpace(queryDic["WagesSubject"]))
            {
                param[8] = new SqlParameter("@WagesSubject", queryDic["WagesSubject"]);
            }
            else
            {
                param[8] = new SqlParameter("@WagesSubject", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SocialSecuritySubject") && !string.IsNullOrWhiteSpace(queryDic["SocialSecuritySubject"]))
            {
                param[9] = new SqlParameter("@SocialSecuritySubject", queryDic["SocialSecuritySubject"]);
            }
            else
            {
                param[9] = new SqlParameter("@SocialSecuritySubject", "");
            }

            if (queryDic != null && queryDic.ContainsKey("TaxSubject") && !string.IsNullOrWhiteSpace(queryDic["TaxSubject"]))
            {
                param[10] = new SqlParameter("@TaxSubject", queryDic["TaxSubject"]);
            }
            else
            {
                param[10] = new SqlParameter("@TaxSubject", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SqlWhere") && !string.IsNullOrWhiteSpace(queryDic["SqlWhere"]))
            {
                param[11] = new SqlParameter("@SqlWhere", queryDic["SqlWhere"]);
            }
            else
            {
                param[11] = new SqlParameter("@SqlWhere", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SqlOrder") && !string.IsNullOrWhiteSpace(queryDic["SqlOrder"]))
            {
                param[12] = new SqlParameter("@SqlOrder", queryDic["SqlOrder"]);
            }
            else
            {
                param[12] = new SqlParameter("@SqlOrder", "");
            }


            #endregion
            //操作人
            param[13] = new SqlParameter("@OperatorID", id);


            param[14] = new SqlParameter("@sys_PageIndex", page);
            param[15] = new SqlParameter("@sys_PageSize", rows);
            param[16] = new SqlParameter("@PCount", SqlDbType.Int);
            param[17] = new SqlParameter("@RCount", SqlDbType.Int);

            param[16].Direction = ParameterDirection.Output;
            param[17].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<SalaryDetailsEntity>("exec [dbo].[P_SalaryDetails_S]  @EmpID,@EmpName,@CustomerID,@CustomerName,@SalaryMonth,@IDCard,@BankID,@ContractSubject,@WagesSubject,@SocialSecuritySubject,@TaxSubject,@SqlWhere,@SqlOrder,@OperatorID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            
            total = (int)param[17].Value;

            return data;

        }


        #endregion





        public void Dispose()
        {

        }
    }
}
