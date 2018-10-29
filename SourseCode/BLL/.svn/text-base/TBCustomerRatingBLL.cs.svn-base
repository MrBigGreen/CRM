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

    public class TBCustomerRatingBLL : IBLL.ITBCustomerRatingBLL, IDisposable
    {



        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        TBCustomerRatingRepository repository = new TBCustomerRatingRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TBCustomerRatingBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public TBCustomerRatingBLL(DB_CRMEntities entities)
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
        public List<TB_CustomerRating> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            //IQueryable<TB_CustomerRating> queryData = repository.DaoChuData(db, id, order, sort, search);
            //total = queryData.Count();
            //if (total > 0)
            //{
            //    if (page <= 1)
            //    {
            //        queryData = queryData.Take(rows);
            //    }
            //    else
            //    {
            //        queryData = queryData.Skip((page - 1) * rows).Take(rows);
            //    }

            //}
            //return queryData.ToList();
            return null;
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
        public List<TB_CustomerRating> GetByParam(string id, string order, string sort, string search)
        {
            //IQueryable<TB_CustomerRating> queryData = repository.DaoChuData(db, id, order, sort, search);

            //return queryData.ToList();
            return null;
        }

        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_CustomerRating entity)
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
        public bool Create(ref ValidationErrors validationErrors, TB_CustomerRating entity)
        {

            bool flag = false;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    if (Create(ref validationErrors, db, entity))
                    {
                        if (db.Database.ExecuteSqlCommand("UPDATE  dbo.TB_Customer SET RatingScore='" + entity.RatingScore + "'  WHERE CustomerID='" + entity.CustomerID + "'") > 0)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
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
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_CustomerRating> entitys)
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
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TB_CustomerRating> entitys)
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
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TB_CustomerRating entity)
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

            TB_CustomerRating editEntity = repository.Edit(db, entity);
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
        public bool Edit(ref ValidationErrors validationErrors, TB_CustomerRating entity)
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
        public List<TB_CustomerRating> GetAll()
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
        public TB_CustomerRating GetById(string id)
        {
            return repository.GetById(db, id);
        }



        public void Dispose()
        {

        }


        #region 获取客户信用评级
        /// <summary>
        /// 获取客户信用评级
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerRatingEntity> GetCustomerRatingData(int page, int rows, string search, ref int total)
        {
            return repository.GetCustomerRatingData(db, page, rows, search, ref total);
        }

        #endregion



        public bool GetCalcRatingBefore(TB_Customer entity)
        {
            #region 企业信用等级评分标准-签合同之前
            string RatingScore = string.Empty;
            List<string> ItemList = new List<string>();
            ItemList.Add(entity.CompanyNatureCode);
            ItemList.Add(entity.RegisterCapital);
            ItemList.Add(entity.SalesRevenue);
            ItemList.Add(entity.CompanySize);
            ItemList.Add(entity.VocationCode);

            if (GetCalcRatingBefore(ItemList, ref RatingScore))
            {
                entity.RatingScore = RatingScore;
                return true;
            }
            #endregion
            return false;
        }

        public bool GetCalcRatingBefore(List<string> ItemList, ref string RatingScore)
        {
            if (ItemList == null || ItemList.Count < 1)
            {
                return false;
            }
            int flag = 0;
            int iRatingScore = 0;
            try
            {

                for (int i = 0; i < ItemList.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(ItemList[i]))
                    {
                        return false;
                    }
                    string item = ItemList[i];
                    var fldEntity1 = (from s in db.SysField where s.Id == item select s).SingleOrDefault();
                    if (fldEntity1 != null)
                    {
                        if (int.TryParse(fldEntity1.DataValue, out flag))
                        {
                            iRatingScore += flag;
                        }
                    }
                }
                if (iRatingScore >= 90)
                {
                    RatingScore = "A";
                }
                else if (iRatingScore >= 80)
                {
                    RatingScore = "B";
                }
                else if (iRatingScore >= 70)
                {
                    RatingScore = "C";
                }
                else if (iRatingScore >= 60)
                {
                    RatingScore = "D";
                }
                else
                {
                    RatingScore = "E";
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }


}
