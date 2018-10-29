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
    ///  
    /// </summary>
    public partial class TPHousingFundRepository : BaseRepository<TP_HousingFund>, IDisposable
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
        public IQueryable<TP_HousingFund> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
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



                    if (queryDic.ContainsKey("SysOperation") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysOperation")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysOperation as p where p.Id='" + item.Value + "')";
                        continue;
                    }
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
                     .CreateObjectSet<TP_HousingFund>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }


        /// <summary>
        /// 通过主键id，获取菜单---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>菜单</returns>
        public TP_HousingFund GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取菜单---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>菜单</returns>
        public TP_HousingFund GetById(DB_CRMEntities db, string id)
        {
            return db.TP_HousingFund.SingleOrDefault(s => s.HousingFundID == id);
        }

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
                return Save(db);
            }
        }
        /// <summary>
        /// 删除一个菜单
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条菜单的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            TP_HousingFund deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                db.TP_HousingFund.Remove(deleteItem);
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
            IQueryable<TP_HousingFund> collection = from f in db.TP_HousingFund
                                                    where deleteCollection.Contains(f.HousingFundID)
                                                    select f;
            foreach (var deleteItem in collection)
            {
                db.TP_HousingFund.Remove(deleteItem);
            }
        }

        public void Dispose()
        {
        }


        /// <summary>
        /// 公积金信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<HousingFundModel> GetHousingFundData(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            SqlParameter[] param = new SqlParameter[10];


            if (queryDic != null && queryDic.ContainsKey("HousingFundName") && !string.IsNullOrWhiteSpace(queryDic["HousingFundName"]))
            {
                param[0] = new SqlParameter("@HousingFundName", queryDic["HousingFundName"]);
            }
            else
            {
                param[0] = new SqlParameter("@HousingFundName", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ProvinceCode") && !string.IsNullOrWhiteSpace(queryDic["ProvinceCode"]))
            {
                param[1] = new SqlParameter("@ProvinceCode", queryDic["ProvinceCode"]);
            }
            else
            {
                param[1] = new SqlParameter("@ProvinceCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[2] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }
            else
            {
                param[2] = new SqlParameter("@CityCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("DistrictCode") && !string.IsNullOrWhiteSpace(queryDic["DistrictCode"]))
            {
                param[3] = new SqlParameter("@DistrictCode", queryDic["DistrictCode"]);
            }
            else
            {
                param[3] = new SqlParameter("@DistrictCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SearchPersonID") && !string.IsNullOrWhiteSpace(queryDic["SearchPersonID"]))
            {
                param[4] = new SqlParameter("@SearchPersonID", queryDic["SearchPersonID"]);
            }
            else
            {
                param[4] = new SqlParameter("@SearchPersonID", "");
            }

            if (string.IsNullOrWhiteSpace(sort))
            {
                param[5] = new SqlParameter("@order", "");
            }
            else
            {
                param[5] = new SqlParameter("@order", sort + " " + order);
            }

            param[6] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[6].Value = page;
            param[7] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[7].Value = rows;
            param[8] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[9] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[8].Direction = ParameterDirection.Output;
            param[9].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<HousingFundModel>("exec P_PayHousingFund_S @HousingFundName,@ProvinceCode,@CityCode,@DistrictCode,@SearchPersonID,@order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[9].Value);
            return query;
        }



    }
}
