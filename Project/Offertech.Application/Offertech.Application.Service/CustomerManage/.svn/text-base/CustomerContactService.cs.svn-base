using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Data.Repository;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using Offertech.Util;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using Offertech.Data;
using System.Text;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-19 14:25
    /// 描 述：客户联系人
    /// </summary>
    public class CustomerContactService : RepositoryFactory<CustomerContactEntity>, ICustomerContactService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CustomerContactEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<CustomerContactEntity>();
            var queryParam = queryJson.ToJObject();
            //客户Id
            if (!queryParam["customerId"].IsEmpty())
            {
                string CustomerId = queryParam["customerId"].ToString();
                expression = expression.And(t => t.CustomerId == CustomerId);
            }
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Contact":         //联系人
                        expression = expression.And(t => t.Contact.Contains(keyword));
                        break;
                    case "Mobile":          //手机
                        expression = expression.And(t => t.Mobile.Contains(keyword));
                        break;
                    case "Tel":             //电话
                        expression = expression.And(t => t.Tel.Contains(keyword));
                        break;
                    case "QQ":              //QQ
                        expression = expression.And(t => t.QQ.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="customerId">查询参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CustomerContactEntity> GetListByCustomerId(string customerId, string keyword)
        {
            var expression = LinqExtensions.True<CustomerContactEntity>(); ;
            expression = expression.And(t => t.CustomerId == customerId);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                expression = expression.And(t => t.Contact.Contains(keyword));
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.Contact).ToList();
        }
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回数据表</returns>
        public DataTable GetContactData(string customerId, string keyword)
        {
            var parameter = new List<DbParameter>();
            var strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT  * FROM (SELECT Contact,Mobile,Tel,PostId FROM dbo.Client_Customer  WHERE CustomerId=@CustomerId ");
            strSql.Append(" UNION ");
            strSql.Append("SELECT Contact,Mobile,Tel,PostId FROM dbo.Client_CustomerContact  WHERE CustomerId=@CustomerId ) tbl WHERE 1=1");
            parameter.Add(DbParameters.CreateDbParameter("@CustomerId", customerId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(" AND CHARINDEX(@keyword,Contact)>0");
                parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
            }
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CustomerContactEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CustomerContactEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                try
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// 移动端保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void AppSaveForm(string keyValue, CustomerContactEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                this.BaseRepository().Update(entity);
            }
            else
            {
                try
                {
                    this.BaseRepository().Insert(entity);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        }
        #endregion
    }
}