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
    /// 创建时间：2015-06-10
    /// 描述说明：客户分享
    /// </summary>
    public partial class TBCustomerShareRepository : BaseRepository<TB_CustomerShare>, IDisposable
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
        public IQueryable<TB_CustomerShare> DaoChuData(DB_CRMEntities db, string id, string order, string sort, string search, params object[] listQuery)
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
                    where += " and (it.[CreatedTime] >=  CAST('" + queryDic["StartDate"] + "' as   System.DateTime) and it.[CreatedTime] <=  CAST('" + queryDic["EndDate"]
                        + "' as   System.DateTime)) ";
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
                 .CreateObjectSet<TB_CustomerShare>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                 .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                 .AsQueryable();
        }

        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerShare GetById(string id)
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
        public TB_CustomerShare GetById(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerShare.SingleOrDefault(s => s.CustomerShareID == id);
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
            TB_CustomerShare deleteItem = GetById(db, id);
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
            IQueryable<TB_CustomerShare> collection = from f in db.TB_CustomerShare
                                                      where deleteCollection.Contains(f.CustomerShareID)
                                                      select f;
            foreach (var deleteItem in collection)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        #endregion

        #region 客户分享检查  create by chand 2015-12-16
        /// <summary>
        /// 客户分享检查
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        ///-1 人员账号不存在或跟进类型不明确
        ///0 不能分享（已有相同跟进的销售）
        ///1 可以分享
        /// </returns>
        public int GetCustomerShareCheck(DB_CRMEntities db, string CustomerID, string SysPersonID)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerID", CustomerID);

            param[1] = new SqlParameter("@SysPersonID", SysPersonID);

            var data = db.Database.SqlQuery<int>("exec [dbo].[P_CustomerShareCheck]  @CustomerID,@SysPersonID", param).ToList();
            if (data != null && data.Count > 0)
            {
                return data[0];
            }
            return -1;
        }

        #endregion


        #region 客户分享  create by chand 2016-1-18
        /// <summary>
        /// 客户分享
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        /// -1 不能分享，一个客户不能分享给多个人
        ///0 分享失败
        ///1 分享成功
        /// </returns>
        public int GetCustomerShare(DB_CRMEntities db, TB_CustomerShare entity)
        {

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CustomerShareID", entity.CustomerShareID);
            param[1] = new SqlParameter("@CustomerID", entity.CustomerID);
            param[2] = new SqlParameter("@SysPersonIDFrom", entity.CreatedBy);
            param[3] = new SqlParameter("@SysPersonIDTo", entity.SysPersonID);
            param[4] = new SqlParameter("@Authority", entity.Authority);
            param[5] = new SqlParameter("@ShareRemark", entity.ShareRemark);

            var data = db.Database.SqlQuery<int>("exec [dbo].[P_CustomerShare_I]  @CustomerShareID,@CustomerID,@SysPersonIDFrom,@SysPersonIDTo,@Authority,@ShareRemark", param).ToList();
            if (data != null && data.Count > 0)
            {
                return data[0];
            }
            return -1;
        }

        #endregion

        #region 客户分享取消  create by chand 2015-12-16
        /// <summary>
        /// 客户分享取消
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        ///0 没有分享记录
        ///1 取消分享成功
        /// </returns>
        public int GetCustomerShareCancel(DB_CRMEntities db, string CustomerID, string SysPersonID)
        {

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerID", CustomerID);

            param[1] = new SqlParameter("@SysPersonID", SysPersonID);

            var data = db.Database.SqlQuery<int>("exec [dbo].[P_CustomerShareCancel_U]  @CustomerID,@SysPersonID", param).ToList();
            if (data != null && data.Count > 0)
            {
                return data[0];
            }
            return -1;
        }

        #endregion

        #region 客户分享记录 create by chand 2016-1-19

        /// <summary>
        /// 客户分享记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerShareEntity> GetCustomerShareData(DB_CRMEntities db, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[10];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置
            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[1] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[1] = new SqlParameter("@CustomerID", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonIDFrom") && !string.IsNullOrWhiteSpace(queryDic["SysPersonIDFrom"]))
            {
                param[2] = new SqlParameter("@SysPersonIDFrom", queryDic["SysPersonIDFrom"]);
            }
            else
            {
                param[2] = new SqlParameter("@SysPersonIDFrom", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonIDTo") && !string.IsNullOrWhiteSpace(queryDic["SysPersonIDTo"]))
            {
                param[3] = new SqlParameter("@SysPersonIDTo", queryDic["SysPersonIDTo"]);
            }
            else
            {
                param[3] = new SqlParameter("@SysPersonIDTo", "");
            }

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[4] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[4] = new SqlParameter("@StartDate", "");
            }
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[5] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[5] = new SqlParameter("@EndDate", "");
            }


            #endregion

            param[6] = new SqlParameter("@sys_PageIndex", page);
            param[7] = new SqlParameter("@sys_PageSize", rows);
            param[8] = new SqlParameter("@PCount", SqlDbType.Int);
            param[9] = new SqlParameter("@RCount", SqlDbType.Int);

            param[8].Direction = ParameterDirection.Output;
            param[9].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerShareEntity>("exec [dbo].[P_CustomerShare_S]  @CustomerName,@CustomerID,@SysPersonIDFrom,@SysPersonIDTo,@StartDate,@EndDate,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[9].Value;

            return data;

        }
        #endregion


        #region 取消所有分享过来的客户  create by chand 2016-05-06


        /// <summary>
        /// 取消所有分享过来的客户
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SysPersonID"></param>
        /// <param name="OperatorID"></param>
        /// <returns></returns>
        public bool GetCustomerReleaseAllShare(DB_CRMEntities db, string SysPersonID, string OperatorID)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SysPersonID", SysPersonID);

            param[1] = new SqlParameter("@OperatorID", OperatorID);

            var data = db.Database.SqlQuery<int>("exec [dbo].[P_CustomerReleaseAllShare_U]  @SysPersonID,@OperatorID", param).ToList();
            if (data != null && data.Count > 0)
            {
                return true;
            }
            return false;
        }

        #endregion


        public void Dispose()
        {
        }
    }
}
