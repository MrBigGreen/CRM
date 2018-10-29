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
    public class TBCustomerVerificationRepository : BaseRepository<TB_CustomerVerification>, IDisposable
    {
        #region 检查获取需要验证的客户 create by chand 2015-07-24
        /// <summary>
        /// 检查获取需要验证的客户
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerName"></param>
        /// <param name="result"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerRepeatEntity> GetCustomerRepeat(DB_CRMEntities db, string CustomerNewName, string CustomerID, ref  int result)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[3];

            #region  条件设置

            param[0] = new SqlParameter("@CustomerName", CustomerNewName);

            param[1] = new SqlParameter("@CustomerID", CustomerID);


            #endregion

            param[2] = new SqlParameter("@Result", SqlDbType.Int);

            param[2].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<CustomerRepeatEntity>("exec [dbo].[P_CustomerRepeat_S]  @CustomerName,@CustomerID,@Result output", param).ToList();

            result = (int)param[2].Value;

            return data;

        } 
        #endregion

        #region  进行查重验证 create by chand 2015-07-24
        /// <summary>
        /// 进行查重验证
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerNewName"></param>
        /// <param name="VerificationCode"></param>
        /// <returns></returns>
        public bool GetVerification(DB_CRMEntities db, string CustomerNewName, string VerificationCode)
        {

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(VerificationCode.GetString());
            if (queryDic.Count <= 0)
            {
                return false;
            }
            var list = db.TB_CustomerVerification.Where(s => s.CustomerNewName == CustomerNewName && s.IsDelete == false);
            if (list == null)
            {
                return false;
            }
            if (list.Count() != queryDic.Count)
            {
                return false;
            }
            foreach (var item in list)
            {
                if (!queryDic.ContainsKey(item.CustomerID))
                {
                    return false;
                }
                if (item.VerificationCode != queryDic[item.CustomerID])
                {
                    return false;
                }
            }
            return true;
        }
        
        #endregion


        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerVerification GetById(string id)
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
        public TB_CustomerVerification GetById(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerVerification.SingleOrDefault(s => s.CustomerVerificationID == id);
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
            TB_CustomerVerification deleteItem = GetById(db, id);
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
            IQueryable<TB_CustomerVerification> collection = from f in db.TB_CustomerVerification
                                                             where deleteCollection.Contains(f.CustomerVerificationID)
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
