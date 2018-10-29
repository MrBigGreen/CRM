using Offertech.Application.Code;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Data;
using Offertech.Data.Repository;
using Offertech.Util;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Offertech.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:44
    /// 描 述：客户分享
    /// </summary>
    public class ShareService : RepositoryFactory<ShareEntity>, IShareService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ShareEntity> GetPageList(string keyValue, Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ShareEntity>();
            var queryParam = queryJson.ToJObject();
            expression = expression.And(s => s.ObjectId == keyValue);

            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ShareEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取分享的数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetToSaleList(Pagination pagination, string queryJson)
        {
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;

            try
            {
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT s.ShareId,c.CustomerId,c.FullName AS CustomerName,s.ToUserID,s.ToUserName,s.Authority,s.Description,s.CreateDate FROM dbo.Client_Share s INNER JOIN  dbo.Client_Customer c ON s.ObjectId=c.CustomerId WHERE s.DeleteMark=0 AND s.EnabledMark=1 AND c.DeleteMark=0 AND c.EnabledMark>0 ");
                //当前用户
                strSql.Append(" AND c.TraceUserId=@UserId ");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));

                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "ToUserName":              //分享销售
                            strSql.Append(" AND CHARINDEX(@keyword,s.ToUserName)>0 ");
                            break;
                        case "FullName":            //客户名称
                            strSql.Append(" AND CHARINDEX(@keyword,c.FullName)>0 ");
                            break;

                        default:
                            break;
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", keyword));
                }

                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ShareEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, ShareEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
