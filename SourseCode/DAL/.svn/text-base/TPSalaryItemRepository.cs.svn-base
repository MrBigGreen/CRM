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
    /// 菜单
    /// </summary>
    public partial class TPSalaryItemRepository : BaseRepository<TP_SalaryItem>, IDisposable
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
        public IQueryable<TP_SalaryItem> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
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
                     .CreateObjectSet<TP_SalaryItem>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }
        /// <summary>
        /// 通过主键id，获取菜单---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>菜单</returns>
        public TP_SalaryItem GetById(string id)
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
        public TP_SalaryItem GetById(DB_CRMEntities db, string id)
        {
            return db.TP_SalaryItem.SingleOrDefault(s => s.SalaryItemID == id);
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
            TP_SalaryItem deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                db.TP_SalaryItem.Remove(deleteItem);
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
            IQueryable<TP_SalaryItem> collection = from f in db.TP_SalaryItem
                                                   where deleteCollection.Contains(f.SalaryItemID)
                                                   select f;
            foreach (var deleteItem in collection)
            {
                db.TP_SalaryItem.Remove(deleteItem);
            }
        }

        public void Dispose()
        {
        }



        #region 薪资项目数据

        /// <summary>
        /// 薪资项目数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<SalaryItemEntity> GetSalaryItemData(DB_CRMEntities db, int page, int rows, string search, ref int total)
        {
            SqlParameter[] param = new SqlParameter[8];

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            if (queryDic != null && queryDic.ContainsKey("ItemCode") && !string.IsNullOrWhiteSpace(queryDic["ItemCode"]))
            {
                param[0] = new SqlParameter("@ItemCode", queryDic["ItemCode"]);
            }
            else
            {
                param[0] = new SqlParameter("@ItemCode", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ItemName") && !string.IsNullOrWhiteSpace(queryDic["ItemName"]))
            {
                param[1] = new SqlParameter("@ItemName", queryDic["ItemName"]);
            }
            else
            {
                param[1] = new SqlParameter("@ItemName", "");
            }

            if (queryDic != null && queryDic.ContainsKey("DataType") && !string.IsNullOrWhiteSpace(queryDic["DataType"]))
            {
                param[2] = new SqlParameter("@DataType", queryDic["DataType"]);
            }
            else
            {
                param[2] = new SqlParameter("@DataType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("ItemClass") && !string.IsNullOrWhiteSpace(queryDic["ItemClass"]))
            {
                param[3] = new SqlParameter("@ItemClass", queryDic["ItemClass"]);
            }
            else
            {
                param[3] = new SqlParameter("@ItemClass", "");
            }


            param[4] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[4].Value = page;
            param[5] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[5].Value = rows;
            param[6] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[7] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[6].Direction = ParameterDirection.Output;
            param[7].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<SalaryItemEntity>("exec P_SalaryItem_S @ItemCode,@ItemName,@DataType,@ItemClass,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[7].Value);
            return query;
        }

        #endregion

        #region 根据类别获取项目
        /// <summary>
        /// 根据类别获取项目
        /// </summary>
        /// <param name="db"></param>
        /// <param name="itemClass"></param>
        /// <returns></returns>
        public List<TP_SalaryItem> GetSalaryItemByItemClass(DB_CRMEntities db, int itemClass)
        {
            return db.TP_SalaryItem.Where(s => s.ItemClass == itemClass).ToList();

        } 
        #endregion

    }



}
