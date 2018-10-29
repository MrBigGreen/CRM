using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data;
using System.Data.SqlClient;
namespace CRM.DAL
{
    /// <summary>
    /// 通知中心
    /// </summary>
    public partial class SysNoticeRepository : BaseRepository<SysNotice>, IDisposable
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
        public IQueryable<SysNotice> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
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
                     .CreateObjectSet<SysNotice>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable(); 

        }
        /// <summary>
        /// 通过主键id，获取通知中心---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>通知中心</returns>
        public SysNotice GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }                   
        }
        /// <summary>
        /// 通过主键id，获取通知中心---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>通知中心</returns>
        public SysNotice GetById(DB_CRMEntities db, string id)
        {
            var entity = db.SysNotice.SingleOrDefault(s => s.Id == id);
            if (entity != null)
            {
                entity.CreatePersonName = (from s in db.SysPerson where s.Id == entity.CreatePerson select s.MyName).SingleOrDefault();
            }
            return db.SysNotice.SingleOrDefault(s => s.Id == id);
        
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
        /// 删除一个通知中心
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条通知中心的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            SysNotice deleteItem = GetById(db, id);
            if (deleteItem != null)
            { 
                db.SysNotice.Remove(deleteItem);
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
            IQueryable<SysNotice> collection = from f in db.SysNotice
                    where deleteCollection.Contains(f.Id)
                    select f;
            foreach (var deleteItem in collection)
            {
                db.SysNotice.Remove(deleteItem);
            }
        }


        #region 获取通知数据 create by chand 2016-07-06

        /// <summary>
        /// 获取通知数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<SysNoticeEntity> GetNoticeData(DB_CRMEntities db, int page, int rows, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            SqlParameter[] param = new SqlParameter[8];


            if (queryDic != null && queryDic.ContainsKey("IsRead") && !string.IsNullOrWhiteSpace(queryDic["IsRead"]))
            {
                param[0] = new SqlParameter("@IsRead", queryDic["IsRead"]);
            }
            else
            {
                param[0] = new SqlParameter("@IsRead", "");
            }

            if (queryDic != null && queryDic.ContainsKey("Title") && !string.IsNullOrWhiteSpace(queryDic["Title"]))
            {
                param[1] = new SqlParameter("@Title", queryDic["Title"]);
            }
            else
            {
                param[1] = new SqlParameter("@Title", "");
            }

            if (queryDic != null && queryDic.ContainsKey("Message") && !string.IsNullOrWhiteSpace(queryDic["Message"]))
            {
                param[2] = new SqlParameter("@Message", queryDic["Message"]);
            }
            else
            {
                param[2] = new SqlParameter("@Message", "");
            }

            if (queryDic != null && queryDic.ContainsKey("PersonId") && !string.IsNullOrWhiteSpace(queryDic["PersonId"]))
            {
                param[3] = new SqlParameter("@PersonId", queryDic["PersonId"]);
            }
            else
            {
                param[3] = new SqlParameter("@PersonId", "");
            }

            param[4] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[4].Value = page;
            param[5] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[5].Value = rows;
            param[6] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[7] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[6].Direction = ParameterDirection.Output;
            param[7].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<SysNoticeEntity>("exec P_SysNotice_S @IsRead,@Title,@Message,@PersonId,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[7].Value);
            return query;
        }

        #endregion


        public void Dispose()
        {          
        }
    }
}

