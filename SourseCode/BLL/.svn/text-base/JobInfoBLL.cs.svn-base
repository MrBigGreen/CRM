using CRM.DAL;
using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-07-14
    /// 描述说明：职位信息-业务逻辑
    /// </summary>
    public class JobInfoBLL : IBLL.IJobInfoBLL, IDisposable
    {

        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        JobInfoRepository repository = new JobInfoRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public JobInfoBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public JobInfoBLL(DB_CRMEntities entities)
        {
            db = entities;
        }

        #endregion

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<JobInfoEntity> GetData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            List<JobInfoEntity> queryData = repository.GetData(db, id, order, sort, search, page, rows, ref   total);
            return queryData;
        }


        public void Dispose()
        {

        }

    }
}
