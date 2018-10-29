using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-10
    /// 描述说明：客户归属变更记录
    /// </summary>
    public partial class TBCustomerAttributionChangeRepository : BaseRepository<TB_CustomerAttributionChange>, IDisposable
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
        public IQueryable<TB_CustomerAttributionChange> DaoChuData(DB_CRMEntities db, string id, string order, string sort, string search, params object[] listQuery)
        {
            string where = " it.IsDelete=false ";
            if (!string.IsNullOrWhiteSpace(id))
            {
                where += " and it.[CustomerID]='" + id + "'";
            }
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            #region 条件
            if (queryDic != null && queryDic.Count > 0)
            {
                if (queryDic.ContainsKey("StartDate") && queryDic.ContainsKey("EndDate"))
                {
                    where += " and (it.[StartDate] >=  CAST('" + queryDic["StartDate"] + "' as   System.DateTime) and it.[StartDate] <=  CAST('" + queryDic["EndDate"]
                        + "' as   System.DateTime)) or (it.[EndDate] >=  CAST('" + queryDic["StartDate"]
                        + "' as   System.DateTime) and it.[EndDate] <=  CAST('" + queryDic["EndDate"] + "' as   System.DateTime))  ";               
                }
                foreach (var item in queryDic)
                {
                    if (string.IsNullOrWhiteSpace(item.Key) || string.IsNullOrWhiteSpace(item.Value) || queryDic.ContainsKey("StartDate") || queryDic.ContainsKey("EndDate"))
                    {
                        continue;
                    }

                    switch (item.Key)
                    {
                        default:
                            where += "and it.[" + item.Key + "] = '" + item.Value + "'";
                            break;
                    }

                }
            }
            #endregion

            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                 .CreateObjectSet<TB_CustomerAttributionChange>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                 .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                 .AsQueryable();
        }

        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerAttributionChange GetById(string id)
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
        public TB_CustomerAttributionChange GetById(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerAttributionChange.SingleOrDefault(s => s.CustomerAttributionChangeID == id);
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
            TB_CustomerAttributionChange deleteItem = GetById(db, id);
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
            IQueryable<TB_CustomerAttributionChange> collection = from f in db.TB_CustomerAttributionChange
                                                                  where deleteCollection.Contains(f.CustomerAttributionChangeID)
                                                                  select f;
            foreach (var deleteItem in collection)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        #endregion

        #region 客户归属变更(存储过程) create  by chand 2015-07-01
        /// <summary>
        /// 客户归属变更(存储过程)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SysPersonID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="UpdatedBy"></param>
        /// <returns></returns>
        public int UpdateByProc(DB_CRMEntities db, string CustomerAttributionChangeID, string SysPersonID, string CustomerID, string UpdatedBy)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerAttributionChangeID", CustomerAttributionChangeID);
            param[1] = new SqlParameter("@SysPersonID", SysPersonID);
            param[2] = new SqlParameter("@CustomerID", CustomerID);
            param[3] = new SqlParameter("@UpdatedBy", UpdatedBy);
            param[4] = new SqlParameter("@Result", System.Data.SqlDbType.Int);
            param[4].Direction = System.Data.ParameterDirection.Output;

            //System.Data.Entity.TransactionalBehavior.EnsureTransaction sss = new System.Data.Entity.TransactionalBehavior();

            db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.EnsureTransaction, "exec P_CustomerAttributionChange_U @CustomerAttributionChangeID,@SysPersonID,@CustomerID,@UpdatedBy,@Result output", param);
            return (int)param[4].Value;
            //return db.Database.SqlQuery<int>("exec P_CustomerAttributionChange_U @CustomerAttributionChangeID,@SysPersonID,@CustomerID,@UpdatedBy", param);
        }


        #endregion
        public void Dispose()
        {
        }
    }
}
