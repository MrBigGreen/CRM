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
    /// 创建时间：2016-03-31
    /// 描述说明：薪酬主体权限-业务逻辑
    /// </summary>
    public partial class TPSalaryDetailsBLL : IBLL.ITPSalaryDetailsBLL, IDisposable
    {

        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        TPSalaryDetailsRepository repository = new TPSalaryDetailsRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public TPSalaryDetailsBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public TPSalaryDetailsBLL(DB_CRMEntities entities)
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
        public List<TP_SalaryDetails> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            //IQueryable<TP_SalaryDetails> queryData = repository.DaoChuData(db, id, order, sort, search);
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
        public List<TP_SalaryDetails> GetByParam(string id, string order, string sort, string search)
        {
            //IQueryable<TP_SalaryDetails> queryData = repository.DaoChuData(db, id, order, sort, search);

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
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TP_SalaryDetails entity)
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
        public bool Create(ref ValidationErrors validationErrors, TP_SalaryDetails entity)
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
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TP_SalaryDetails> entitys)
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
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TP_SalaryDetails> entitys)
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
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TP_SalaryDetails entity)
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

            TP_SalaryDetails editEntity = repository.Edit(db, entity);
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
        public bool Edit(ref ValidationErrors validationErrors, TP_SalaryDetails entity)
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
        public List<TP_SalaryDetails> GetAll()
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
        public TP_SalaryDetails GetById(string id)
        {
            return repository.GetById(db, id);
        }



        #region 添加 导入薪资数据==============create by  chand 2016-07-01==============
        /// <summary>
        /// 导入薪资数据
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="entity"></param>
        /// <param name="Replace">有新数据则替换</param>
        /// <returns></returns>
        public bool InsertSalary(ref ValidationErrors validationErrors, Dictionary<string, string> dicData, string currentPersonID, bool Replace)
        {

            TPEmployeesBLL empBLL = new TPEmployeesBLL();
            if (!dicData.ContainsKey("CustomerID") || !dicData.ContainsKey("IDCard") || !dicData.ContainsKey("SalaryMonth"))
            {
                validationErrors.Add("导入的基本信息有误");
                return false;
            }
            string CustomerID = dicData["CustomerID"];
            string IDCard = dicData["IDCard"];
            int SalaryMonth = 0;
            int.TryParse(dicData["SalaryMonth"], out SalaryMonth);
            if (SalaryMonth < 1)
            {
                validationErrors.Add("导入的月份有误");
                return false;
            }
            //根据客户编号和身份证号码检查员工是否存在
            TP_Employees empEntity = empBLL.GetEmployee(dicData["CustomerID"], dicData["IDCard"]);
            if (empEntity == null)
            {
                validationErrors.Add("员工不存在");
                return false;
            }


            #region 计算社保缴纳是否正确
            if (dicData.ContainsKey("PayBase3"))
            {
                decimal num = 0;
                decimal PayBase3 = 0;

                if (decimal.TryParse(dicData["PayBase3"], out PayBase3))
                {

                    if (dicData.ContainsKey("SocialInsurID"))
                    {
                        string SocialInsurID = dicData["SocialInsurID"];
                        TP_SocialInsur socialEntity = db.TP_SocialInsur.SingleOrDefault(s => s.SocialInsurID == SocialInsurID);
                        if (socialEntity == null)
                        {
                            validationErrors.Add("社保编号不存在");
                            return false;
                        }
                        if (PayBase3 > socialEntity.DiffEnd || PayBase3 < socialEntity.DiffBegin)
                        {
                            validationErrors.Add("缴纳基数不在对应的社保范围内");
                            return false;
                        }

                        #region 社保基数缴纳比较


                        #region 养老
                        //个人养老
                        if (socialEntity.IndiPension > 0)
                        {
                            var IndiPension = PayBase3 * socialEntity.IndiPension / 100;
                            if (dicData.ContainsKey("IndiPension3"))
                            {
                                if (decimal.TryParse(dicData["IndiPension3"], out num))
                                {
                                    if (IndiPension != num)
                                    {
                                        validationErrors.Add("个人养老数据有误");
                                        return false;
                                    }
                                }
                            }
                        }
                        //公司养老
                        if (socialEntity.CompanyPension > 0)
                        {
                            var CompanyPension = PayBase3 * socialEntity.CompanyPension / 100;
                            if (dicData.ContainsKey("CompanyPension3"))
                            {
                                if (decimal.TryParse(dicData["CompanyPension3"], out num))
                                {
                                    if (CompanyPension != num)
                                    {
                                        validationErrors.Add("公司养老数据有误");
                                        return false;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 工伤

                        ////个人工伤
                        //if (socialEntity.IndiInjury > 0)
                        //{
                        //    var IndiInjury = PayBase3 * socialEntity.IndiInjury / 100;
                        //    if (dicData.ContainsKey("IndiPension3"))
                        //    {
                        //        if (decimal.TryParse(dicData["IndiPension3"], out num))
                        //        {
                        //            if (IndiPension != num)
                        //            {
                        //                validationErrors.Add("个人养老数据有误");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //}
                        //公司工伤
                        if (socialEntity.CompanyInjury > 0)
                        {
                            var CompanyInjury = PayBase3 * socialEntity.CompanyInjury / 100;
                            if (dicData.ContainsKey("CompanyInjury3"))
                            {
                                if (decimal.TryParse(dicData["CompanyInjury3"], out num))
                                {
                                    if (CompanyInjury != num)
                                    {
                                        validationErrors.Add("公司工伤数据有误");
                                        return false;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 医疗

                        //个人医疗
                        if (socialEntity.IndiMedical > 0)
                        {
                            var IndiMedical = PayBase3 * socialEntity.IndiMedical / 100;
                            if (dicData.ContainsKey("IndiMedical3"))
                            {
                                if (decimal.TryParse(dicData["IndiMedical3"], out num))
                                {
                                    if (IndiMedical != num)
                                    {
                                        validationErrors.Add("个人医疗数据有误");
                                        return false;
                                    }
                                }
                            }
                        }

                        //公司医疗
                        if (socialEntity.CompanyMedical > 0)
                        {
                            var CompanyMedical = PayBase3 * socialEntity.CompanyMedical / 100;
                            if (dicData.ContainsKey("CompanyMedical3"))
                            {
                                if (decimal.TryParse(dicData["CompanyMedical3"], out num))
                                {
                                    if (CompanyMedical != num)
                                    {
                                        validationErrors.Add("公司医疗数据有误");
                                        return false;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 生育

                        ////个人生育
                        //if (socialEntity.IndiBirth > 0)
                        //{
                        //    var IndiBirth = PayBase3 * socialEntity.IndiBirth / 100;
                        //    if (dicData.ContainsKey("IndiPension3"))
                        //    {
                        //        if (decimal.TryParse(dicData["IndiPension3"], out num))
                        //        {
                        //            if (IndiPension != num)
                        //            {
                        //                validationErrors.Add("个人养老数据有误");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //}

                        //公司生育
                        if (socialEntity.CompanyBirth > 0)
                        {
                            var CompanyBirth = PayBase3 * socialEntity.CompanyBirth / 100;
                            if (dicData.ContainsKey("CompanyBirth3"))
                            {
                                if (decimal.TryParse(dicData["CompanyBirth3"], out num))
                                {
                                    if (CompanyBirth != num)
                                    {
                                        validationErrors.Add("公司生育数据有误");
                                        return false;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 补充

                        ////个人补充
                        //if (socialEntity.IndiSupply > 0)
                        //{
                        //    var IndiSupply = PayBase3 * socialEntity.IndiSupply / 100;
                        //    if (dicData.ContainsKey("IndiPension3"))
                        //    {
                        //        if (decimal.TryParse(dicData["IndiPension3"], out num))
                        //        {
                        //            if (IndiPension != num)
                        //            {
                        //                validationErrors.Add("个人养老数据有误");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //} 



                        ////公司补充
                        //if (socialEntity.CompanySupply > 0)
                        //{
                        //    var CompanySupply = PayBase3 * socialEntity.CompanySupply / 100;
                        //    if (dicData.ContainsKey("IndiPension3"))
                        //    {
                        //        if (decimal.TryParse(dicData["IndiPension3"], out num))
                        //        {
                        //            if (IndiPension != num)
                        //            {
                        //                validationErrors.Add("个人养老数据有误");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //} 

                        #endregion

                        #region 大病统筹

                        ////个人大病统筹
                        //if (socialEntity.IndiDBTC > 0)
                        //{
                        //    var IndiDBTC = PayBase3 * socialEntity.IndiDBTC / 100;
                        //    if (dicData.ContainsKey("IndiPension3"))
                        //    {
                        //        if (decimal.TryParse(dicData["IndiPension3"], out num))
                        //        {
                        //            if (IndiPension != num)
                        //            {
                        //                validationErrors.Add("个人养老数据有误");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //}
                        ////公司大病统筹
                        //if (socialEntity.CompanyDBTC > 0)
                        //{
                        //    var CompanyDBTC = PayBase3 * socialEntity.CompanyDBTC / 100;
                        //    if (dicData.ContainsKey("IndiPension3"))
                        //    {
                        //        if (decimal.TryParse(dicData["IndiPension3"], out num))
                        //        {
                        //            if (IndiPension != num)
                        //            {
                        //                validationErrors.Add("个人养老数据有误");
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //}  
                        #endregion


                        #endregion

                        #region 计算公积金缴纳是否正确

                        if (dicData.ContainsKey("HousingFundID"))
                        {
                            string HousingFundID = dicData["HousingFundID"];
                            TP_HousingFund housingEntity = db.TP_HousingFund.SingleOrDefault(s => s.HousingFundID == HousingFundID);
                            if (housingEntity == null)
                            {
                                validationErrors.Add("公积金编号不存在");
                                return false;
                            }

                            if (PayBase3 > housingEntity.DiffEnd || PayBase3 < housingEntity.DiffBegin)
                            {
                                validationErrors.Add("缴纳基数不在对应的社保范围内");
                                return false;
                            }
                            
                            #region 公积金
                            //个人公积金
                            if (housingEntity.IndiRatio > 0)
                            {
                                var IndiPension = PayBase3 * housingEntity.IndiRatio / 100;
                                if (dicData.ContainsKey("IndiFund3"))
                                {
                                    if (decimal.TryParse(dicData["IndiFund3"], out num))
                                    {
                                        if (IndiPension != num)
                                        {
                                            validationErrors.Add("个人公积金数据有误");
                                            return false;
                                        }
                                    }
                                }
                            }
                            //公司公积金
                            if (housingEntity.CompanyRatio > 0)
                            {
                                var CompanyRatio = PayBase3 * housingEntity.CompanyRatio / 100;
                                if (dicData.ContainsKey("CompanyFund3"))
                                {
                                    if (decimal.TryParse(dicData["CompanyFund3"], out num))
                                    {
                                        if (CompanyRatio != num)
                                        {
                                            validationErrors.Add("公司公积金数据有误");
                                            return false;
                                        }
                                    }
                                }
                            }
                            #endregion
                            
                        }

                        #endregion

                    }

                }


            }

            #endregion





            //检查是否重复
            var SalaryEntity = db.TP_SalaryDetails.Where(s => s.EmpID == empEntity.EmpID && s.SalaryMonth == SalaryMonth && s.CustomerID == CustomerID).FirstOrDefault();
            if (SalaryEntity == null)
            {

                string sql = "INSERT INTO TP_SalaryDetails({0})VALUES({1})";
                foreach (var item in dicData)
                {
                    if (item.Key == "IDCard" || item.Key == "CustomerName")
                    {
                        continue;
                    }
                    sql = string.Format(sql, item.Key + ",{0}", "'" + item.Value + "',{1}");
                }
                sql = string.Format(sql, "IsDelete,VersionNo,CreatedBy,CreatedTime,LastUpdatedBy,LastUpdatedTime,SalaryDetailsID,EmpID", "0,1,'" + currentPersonID + "',getdate(),'" + currentPersonID + "',getdate(),'" + Result.GetNewId() + "','" + empEntity.EmpID + "'");

                #region 执行更新语句
                try
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {

                        if (db.Database.ExecuteSqlCommand(sql) > 0)
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
                #endregion


            }
            #region 更新已有数据
            else
            {
                string sql = "UPDATE TP_SalaryDetails SET {0}  WHERE SalaryDetailsID='" + SalaryEntity.SalaryDetailsID + "'";

                foreach (var item in dicData)
                {
                    if (item.Key == "IDCard" || item.Key == "CustomerName")
                    {
                        continue;
                    }
                    sql = string.Format(sql, item.Key + "='" + item.Value + "',{0}");
                }
                sql = string.Format(sql, "VersionNo=VersionNo+1,LastUpdatedBy='" + currentPersonID + "',LastUpdatedTime=getdate() ");



                #region 执行更新语句
                try
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {

                        if (db.Database.ExecuteSqlCommand(sql) > 0)
                        {
                            transactionScope.Complete();
                            validationErrors.Add("更新数据");
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
                #endregion


            }
            #endregion



            return false;
        }
        #endregion

        #region 获取薪资数据

        /// <summary>
        /// 获取薪资数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<SalaryDetailsEntity> GetSalaryDetailsData(string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            return repository.GetSalaryDetailsData(db, id, order, sort, search, page, rows, ref total);

        }
        #endregion

        #region 获取薪资所有月份

        public List<int> GetAllSalaryMonth()
        {
            return db.Database.SqlQuery<int>("SELECT SalaryMonth FROM  [dbo].[TP_SalaryDetails] GROUP BY SalaryMonth").ToList(); ;


        }
        #endregion

        public void Dispose()
        {

        }



    }
}
