using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using CRM.DAL;
using Common;

namespace CRM.BLL
{
    /// <summary>
    /// 通知中心 
    /// </summary>
    public partial class SysNoticeBLL : IBLL.ISysNoticeBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 通知中心的数据库访问对象
        /// </summary>
        SysNoticeRepository repository = new SysNoticeRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public SysNoticeBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public SysNoticeBLL(DB_CRMEntities entities)
        {
            db = entities;
        }
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
        public List<SysNotice> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<SysNotice> queryData = repository.DaoChuData(db, order, sort, search);
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
            return queryData.ToList();
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
        public List<SysNotice> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<SysNotice> queryData = repository.DaoChuData(db, order, sort, search);

            return queryData.ToList();
        }
        /// <summary>
        /// 创建一个通知中心
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个通知中心</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, SysNotice entity)
        {
            try
            {
                repository.Create(entity);
                return true;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        ///  创建通知中心集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">通知中心集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<SysNotice> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int count = entitys.Count();
                    if (count == 1)
                    {
                        return this.Create(ref validationErrors, entitys.FirstOrDefault());
                    }
                    else if (count > 1)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            repository.Create(db, entitys);
                            if (count == repository.Save(db))
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
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        /// 删除一个通知中心
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一通知中心的主键</param>
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
        /// 删除通知中心集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">通知中心的集合</param>
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
        ///  创建通知中心集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">通知中心集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<SysNotice> entitys)
        {
            try
            {
                if (entitys != null)
                {
                    int count = entitys.Count();
                    if (count == 1)
                    {
                        return this.Edit(ref validationErrors, entitys.FirstOrDefault());
                    }
                    else if (count > 1)
                    {
                        using (TransactionScope transactionScope = new TransactionScope())
                        {
                            repository.Edit(db, entitys);
                            if (count == repository.Save(db))
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
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }
        /// <summary>
        /// 编辑一个通知中心
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个通知中心</param>
        /// <returns></returns>
        public bool Edit(ref ValidationErrors validationErrors, SysNotice entity)
        {
            try
            {
                repository.Edit(db, entity);
                repository.Save(db);
                return true;
            }
            catch (Exception ex)
            {
                validationErrors.Add(ex.Message);
                ExceptionsHander.WriteExceptions(ex);
            }
            return false;
        }

        public List<SysNotice> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个通知中心
        /// </summary>
        /// <param name="id">通知中心的主键</param>
        /// <returns>一个通知中心</returns>
        public SysNotice GetById(string id)
        {
            return repository.GetById(db, id);
        }


        #region 创建通知角色下的所有人员

        /// <summary>
        /// 创建通知角色下的所有人员
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="Message"></param>
        /// <param name="RoleID"></param>
        /// <param name="CreatePerson"></param>
        /// <returns></returns>
        public bool CreateByRole(ref ValidationErrors validationErrors, string Title, string Message, string RoleID, string CreatePerson)
        {
            var p = (from m in db.SysRole
                     from f in m.SysPerson
                     where m.Id == RoleID
                     select f).ToList();


            foreach (var item in p)
            {
                SysNotice entity = new SysNotice();
                entity.Id = Result.GetNewId();
                entity.Title = Title;
                entity.Message = Message;
                entity.PersonId = item.Id;
                entity.LostTime = DateTime.Now.AddMonths(1);
                entity.IsRead = false;
                entity.CreateTime = DateTime.Now;
                entity.CreatePerson = CreatePerson;
                entity.ReadTime = null;
                //发送短信通知
                if (!string.IsNullOrWhiteSpace(item.MobilePhoneNumber))
                {
                    Sms.SendMsg(item.MobilePhoneNumber, Message);
                }
                Create(ref validationErrors, entity);

            }

            return true;
        }
        #endregion


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
        public List<SysNoticeEntity> GetNoticeData(int page, int rows, string search, ref int total)
        {
            return repository.GetNoticeData(db, page, rows, search, ref  total);
        }

        #endregion

        public void Dispose()
        {

        }
    }
}

