using Common;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.BLL
{
    public class TBBillingBLL : IBLL.ITBBillingBLL, IDisposable
    {
        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        TBBillingRepository repository = new TBBillingRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TBBillingBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public TBBillingBLL(DB_CRMEntities entities)
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
        public List<TB_Billing> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            IQueryable<TB_Billing> queryData = repository.DaoChuData(db, order, sort, search);
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
        public List<TB_Billing> GetByParam(string id, string order, string sort, string search)
        {
            IQueryable<TB_Billing> queryData = repository.DaoChuData(db, id, order, sort, search);

            return queryData.ToList();

        }

        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_Billing entity)
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
        public bool Create(ref ValidationErrors validationErrors, TB_Billing entity)
        {



            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Create(ref validationErrors, db, entity))
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
        /// <summary>
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_Billing> entitys)
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
        ///  创建客户集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entitys">客户集合</param>
        /// <returns></returns>
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TB_Billing> entitys)
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
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_Billing entity)
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

            TB_Billing editEntity = repository.Edit(db, entity);
            if (count == repository.Save(db))
            {
                return true;
            }
            else
            {
                validationErrors.Add("编辑客户出错了");
            }
            return false;
        }
        /// <summary>
        /// 编辑一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户</param>
        /// <returns>是否编辑成功</returns>
        public bool Edit(ref ValidationErrors validationErrors, TB_Billing entity)
        {
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
        public List<TB_Billing> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 删除一个客户联系人
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一个客户联系人的主键</param>
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
        /// 删除客户联系人集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的客户联系人</param>
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
        /// 根据主键获取一个客户联系人
        /// </summary>
        /// <param name="id">客户联系人的主键</param>
        /// <returns>一个客户联系人</returns>
        public TB_Billing GetById(string id)
        {
            return repository.GetById(db, id);
        }



        public void Dispose()
        {

        }

        #region 增加或编辑开票明细

        public bool AddOrEdit(ref ValidationErrors validationErrors, TB_BillingDetails entity)
        {
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (string.IsNullOrWhiteSpace(entity.BillingDetailsID))
                    {

                        entity.BillingDetailsID = Result.GetNewId();
                        db.Set<TB_BillingDetails>().Add(entity);
                    }
                    else
                    {
                        var model = db.TB_BillingDetails.SingleOrDefault(s => s.BillingDetailsID == entity.BillingDetailsID);
                        if (model != null)
                        {
                            model.CustomerName = entity.CustomerName;
                            model.BillingAmount = entity.BillingAmount;
                            model.VersionNo++;
                            model.LastUpdatedBy = entity.LastUpdatedBy;
                            model.LastUpdatedTime = entity.LastUpdatedTime;
                            model.IsDelete = entity.IsDelete;
                            db.Set<TB_BillingDetails>().Attach(model);

                            db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                        }
                    }
                    int flag = 0;
                    try
                    {
                        flag = db.SaveChanges();

                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("DECLARE @BillingID NVARCHAR(50)");
                        sb.AppendFormat(" SET @BillingID='{0}'", entity.BillingID);
                        sb.AppendFormat(" UPDATE TB_Billing SET BillingAmountSJ=(SELECT ISNULL(SUM(BillingAmount),0) FROM dbo.TB_BillingDetails WHERE BillingID =@BillingID AND IsDelete=0 AND Category=1) WHERE BillingID=@BillingID");
                        sb.AppendFormat(" UPDATE TB_Billing SET BillingAmountYJ=(SELECT ISNULL(SUM(BillingAmount),0) FROM dbo.TB_BillingDetails WHERE BillingID =@BillingID AND IsDelete=0 AND Category=2) WHERE BillingID=@BillingID");

                        flag += db.Database.ExecuteSqlCommand(sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        validationErrors.Add(ex.Message);
                        ExceptionsHander.WriteExceptions(ex);
                    }

                    if (flag == 3)
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


        #endregion

        public TB_BillingDetails GetDetailsById(string Id)
        {

            return db.TB_BillingDetails.SingleOrDefault(s => s.BillingDetailsID == Id);
        }

        /// <summary>
        /// 获取开票明细
        /// </summary>
        /// <param name="BillingID"></param>
        /// <param name="Category"></param>
        /// <returns></returns>
        public List<TB_BillingDetails> GetDetails(string BillingID, int Category)
        {
            return (from s in db.TB_BillingDetails where s.BillingID == BillingID && s.Category == Category && s.IsDelete == false select s).ToList();

        }

        public List<string> GetMonths()
        {
            return (from s in db.TB_Billing orderby s.TheMonth descending select s.TheMonth).Distinct().OrderByDescending(x => x).ToList();
        }
        /// <summary>
        /// 当前的周是否存在
        /// </summary>
        /// <param name="TheMonth"></param>
        /// <param name="TheWeek"></param>
        /// <returns></returns>
        public bool IsExists(string TheMonth, int TheWeek)
        {
            var q = (from s in db.TB_Billing where s.TheMonth == TheMonth && s.TheWeek == TheWeek select s).ToList();
            if (q.Count > 0)
                return true;
            else
                return false;
        }

    }
}
