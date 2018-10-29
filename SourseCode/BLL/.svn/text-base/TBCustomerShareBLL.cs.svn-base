using Common;
using CRM.DAL;
using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.BLL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-29
    /// 描述说明：客户分享-业务逻辑
    /// </summary>
    public partial class TBCustomerShareBLL : IBLL.ITBCustomerShareBLL, IDisposable
    {

        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        TBCustomerShareRepository repository = new TBCustomerShareRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TBCustomerShareBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public TBCustomerShareBLL(DB_CRMEntities entities)
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
        public List<TB_CustomerShare> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<TB_CustomerShare> queryData = repository.DaoChuData(db, id, order, sort, search);
            total = queryData.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    queryData = queryData.Take(rows);
                }
                else
                {
                    queryData = queryData.Skip((page - 1) * rows).Take(rows);
                }
            }
            var list = queryData.ToList();
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item.SysPersonID))
                {
                    item.SysPersonName = (from s in db.SysPerson where s.Id == item.SysPersonID select s.MyName).SingleOrDefault();
                }
            }
            return list;
        }
        /// <summary>
        /// 查询的数据 /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<TB_CustomerShare> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<TB_CustomerShare> queryData = repository.DaoChuData(db, id, order, sort, search);

            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_CustomerShare entity)
        {
            int count = 1;

            repository.Create(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("创建出错了");
            }
            return false;
        }
        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, TB_CustomerShare entity)
        {
            try
            {

                int flag = repository.GetCustomerShare(db, entity);
                if (flag == 1)
                {
                    return true;
                }
                else if (flag == -1)
                {
                    validationErrors.Add("一个客户不能分享给多个人");
                    return false;
                }
                else if (flag == -2)
                {
                    validationErrors.Add("客户不能分享给归属人");
                    return false;
                }
                else if (flag == 0)
                {
                    validationErrors.Add("分享失败");
                    return false;
                }
                else
                {
                    validationErrors.Add("数据异常");
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_CustomerShare> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int flag = 0, count = entitys.Count();
                    if (count > 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            foreach (var entity in entitys)
                            {
                                if (Create(ref validationErrors, db, entity))
                                {
                                    flag++;
                                }
                                else
                                {
                                    Transaction.Current.Rollback();
                                    return false;
                                }
                            }
                            if (count == flag)
                            {
                                transactionScope.Complete();
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }

        /// <summary>
        /// 删除一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个客户的主键</param>
        /// <returns></returns>  
        public bool Delete(ref ValidationErrors validationErrors, string id)
        {
            try
            {
                return repository.Delete(id) == 1;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        /// 删除客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的客户</param>
        /// <returns></returns>    
        public bool DeleteCollection(ref ValidationErrors validationErrors, string[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {

                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        repository.Delete(db, deleteCollection);
                        if (deleteCollection.Length == repository.Save(db))
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TB_CustomerShare> entitys)
        {
            if (entitys != null)
            {
                try
                {
                    int flag = 0, count = entitys.Count();
                    if (count > 0)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            foreach (var entity in entitys)
                            {
                                if (Edit(ref validationErrors, db, entity))
                                {
                                    flag++;
                                }
                                else
                                {
                                    Transaction.Current.Rollback();
                                    return false;
                                }
                            }
                            if (count == flag)
                            {
                                transactionScope.Complete();
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    validationErrors.Add(ex.Message);
                    ExceptionsHander.WriteExceptions(ex);
                }
            }
            return false;
        }
        /// <summary>
        /// 编辑一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_CustomerShare entity)
        {  /*                       
                           * 不操作 原有 现有
                           * 增加   原没 现有
                           * 删除   原有 现没
                           */
            if (entity == null)
            {
                return false;
            }
            int count = 1;

            TB_CustomerShare editEntity = repository.Edit(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, TB_CustomerShare entity)
        {
            //检查用户是否有编辑分享权限
            if (new TBCustomerBLL().GetCustomerAuthority(entity.CustomerID, entity.LastUpdatedBy) != 0)
            {
                validationErrors.Add("您没有编辑客户分享权限");
                return false;
            }

            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Edit(ref validationErrors, db, entity))
                    {
                        transactionScope.Complete();
                        return true;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        public List<TB_CustomerShare> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个客户
        /// </summary>
        /// <param name="id">客户的主键</param>
        /// <returns>一个客户</returns>
        public TB_CustomerShare GetById(string id)
        {
            return repository.GetById(db, id);
        }

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
        public int GetCustomerShareCancel(string CustomerID, string SysPersonID)
        {
            return repository.GetCustomerShareCancel(db, CustomerID, SysPersonID);
        }


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
        public List<CustomerShareEntity> GetCustomerShareData(string search, int page, int rows, ref int total)
        {
            return repository.GetCustomerShareData(db, search, page, rows, ref total);
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
        public bool GetCustomerReleaseAllShare(string SysPersonID, string OperatorID)
        {
            return repository.GetCustomerReleaseAllShare(db, SysPersonID, OperatorID);
        }

        #endregion

        public void Dispose()
        {

        }

    }
}
