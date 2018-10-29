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
    /// 描述说明：客户联系信息-数据访问层
    /// </summary>
    public partial class TBCustomerContactRepository : BaseRepository<TB_CustomerContact>, IDisposable
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
        public IQueryable<TB_CustomerContact> DaoChuData(DB_CRMEntities db, string id, string order, string sort, string search, params object[] listQuery)
        {

            return db.TB_CustomerContact.Where(s => s.CustomerID == id && s.IsDelete == false).AsQueryable();
            //return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
            //         .CreateObjectSet<TB_CustomerContact>().Where(string.IsNullOrEmpty(where) ? "true" : where)
            //         .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
            //         .AsQueryable();

        }

        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerContact GetById(string id)
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
        public TB_CustomerContact GetById(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerContact.SingleOrDefault(s => s.CustomerContactID == id);
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
            TB_CustomerContact deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
            this.Save(db);
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public void Delete(DB_CRMEntities db, string[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<TB_CustomerContact> collection = from f in db.TB_CustomerContact
                                                        where deleteCollection.Contains(f.CustomerContactID)
                                                        select f;
            foreach (var deleteItem in collection)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        #endregion



        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerContactModel> GetCustomerContactData(DB_CRMEntities db, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[6];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("ContactName") && !string.IsNullOrWhiteSpace(queryDic["ContactName"]))
            {
                param[0] = new SqlParameter("@ContactName", queryDic["ContactName"]);
            }
            else
            {
                param[0] = new SqlParameter("@ContactName", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[1] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[1] = new SqlParameter("@CustomerID", "");
            }
            #endregion

            param[2] = new SqlParameter("@sys_PageIndex", page);
            param[3] = new SqlParameter("@sys_PageSize", rows);
            param[4] = new SqlParameter("@PCount", SqlDbType.Int);
            param[5] = new SqlParameter("@RCount", SqlDbType.Int);

            param[4].Direction = ParameterDirection.Output;
            param[5].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerContactModel>("exec [dbo].[P_CustomerContact_S]  @ContactName,@CustomerID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[5].Value;

            return data;

        }


        public void Dispose()
        {
        }
    }
}
