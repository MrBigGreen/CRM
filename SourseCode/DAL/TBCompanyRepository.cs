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
    /// 创建时间：2015-07-08
    /// 描述说明：客户信息
    /// </summary>
    public partial class TBCompanyRepository : BaseRepository<TB_Company>, IDisposable
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
        public List<CompanyModel> GetData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[5];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@CompanyName", queryDic["Keyword"]);
            }
            else
            {
                param[0] = new SqlParameter("@CompanyName", "");
            }

            #endregion

            param[1] = new SqlParameter("@sys_PageIndex", page);
            param[2] = new SqlParameter("@sys_PageSize", rows);
            param[3] = new SqlParameter("@PCount", SqlDbType.Int);
            param[4] = new SqlParameter("@RCount", SqlDbType.Int);

            param[3].Direction = ParameterDirection.Output;
            param[4].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CompanyModel>("exec [dbo].[P_Company_S]  @CompanyName,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[4].Value;

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
        public IQueryable<TB_Company> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
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
                     .CreateObjectSet<TB_Company>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }



        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_Company GetById(int id)
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
        public TB_Company GetById(DB_CRMEntities db, int id)
        {
            return db.TB_Company.SingleOrDefault(s => s.CompanyID == id);
        }
        #endregion

        #region 通过主键删除对象
        /// <summary>
        /// 确定删除一个对象，调用Save方法
        /// </summary>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>    
        public int Delete(int id)
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
        public void Delete(DB_CRMEntities db, int id)
        {
            TB_Company deleteItem = GetById(db, id);
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
        public void Delete(DB_CRMEntities db, int[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<TB_Company> collection = from f in db.TB_Company
                                                where deleteCollection.Contains(f.CompanyID)
                                                select f;
            foreach (var deleteItem in collection)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        #endregion


        public void Dispose()
        {
        }
    }
}
