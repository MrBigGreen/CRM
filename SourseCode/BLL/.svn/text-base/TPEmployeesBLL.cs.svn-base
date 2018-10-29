using Common;
using CRM.DAL;
using CRM.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace CRM.BLL
{
    public partial class TPEmployeesBLL : ITPEmployeesBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;

        protected TPEmployeesRepository repository = new TPEmployeesRepository();
        public TPEmployeesBLL()
        {
            db = new DB_CRMEntities();
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
        public List<TP_Employees> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total)
        {


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
        public List<TP_Employees> GetByParam(string id, string order, string sort, string search)
        {
            return null;
        }
        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="db">数据库上下文</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool Create(ref ValidationErrors validationErrors, DB_CRMEntities db, TP_Employees entity)
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
        public bool Create(ref ValidationErrors validationErrors, TP_Employees entity)
        {
            int personCount = db.TP_Employees.Where(w => w.CustomerID.Equals(entity.CustomerID, StringComparison.CurrentCultureIgnoreCase) && w.IDCard.Equals(entity.IDCard, StringComparison.CurrentCultureIgnoreCase)).Count();
            if (personCount > 0)
            {
                validationErrors.Add("员工已存在");
                return false;
            }

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
        public bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TP_Employees> entitys)
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
        public bool EditCollection(ref ValidationErrors validationErrors, IQueryable<TP_Employees> entitys)
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
        public bool Edit(ref ValidationErrors validationErrors, DB_CRMEntities db, TP_Employees entity)
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

            TP_Employees editEntity = repository.Edit(db, entity);
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
        public bool Edit(ref ValidationErrors validationErrors, TP_Employees entity)
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
        public List<TP_Employees> GetAll()
        {
            return repository.GetAll(db).ToList();
        }

        /// <summary>
        /// 根据主键获取一个客户
        /// </summary>
        /// <param name="id">客户的主键</param>
        /// <returns>一个客户</returns>
        public TP_Employees GetById(string id)
        {
            return repository.GetById(db, id);
        }


        public void Dispose()
        {
        }


        public List<TPEmployees> GetDataEmployeesesList(int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetDataEmployeesesList(db, page, rows, order, sort, search, ref total);
        }

        public bool AddEmployeeseInfo(string CompanyName, string EmployessName, string Gender, string CardType, string CardID, string BankName,
            string BankCity, string BankID, string Nation, string ContractType, string WagesType, string SocialSecurityType, string TaxType, string PhoneNum, string Email, string HiddPIC, string UserID)
        {
            int i = repository.ChangeEmployeeseInfo(db, 1, CompanyName, EmployessName, Gender, CardType, CardID, BankName,
               BankCity, BankID, Nation, ContractType, WagesType, SocialSecurityType, TaxType, PhoneNum, Email, HiddPIC, UserID, "1", "");
            if (i == 1)
            {
                return true;
            }

            return false;
        }

        public bool UpdateEmployeeseInfo(string CompanyID, string EmpID, string CompanyName, string EmployessName, string Gender,
            string CardType, string CardID, string BankName, string BankCity, string BankID, string Nation,
            string ContractType, string WagesType, string SocialSecurityType, string TaxType, string PhoneNum, string Email, string HiddPIC, string JobStatus, string UserID)
        {
            int i = repository.ChangeEmployeeseInfo(db, 2, CompanyName, EmployessName, Gender, CardType, CardID, BankName,
                BankCity, BankID, Nation, ContractType, WagesType, SocialSecurityType, TaxType, PhoneNum, Email, HiddPIC, UserID, JobStatus, EmpID);
            if (i == 1)
            {
                return true;
            }

            return false;
        }


        public bool FireEmployeeseInfo(string EmpID, string UserID)
        {
            int i = repository.FireEmployeeseInfo(db, EmpID, UserID);
            if (i == 1)
            {
                return true;
            }

            return false;
        }


        public TPEmployees GetDataEmployeesesInfo(string EmpID)
        {
            return repository.GetDataEmployeesesInfo(db, EmpID);
        }


        public List<TPEmployees> ExportInfo(string UserID, string sortOrder, string sortName, string parm, string type)
        {

            return repository.GetDataEmployeesesListByExcel(db, UserID, sortOrder, sortName, parm, type);
        }



        public List<TPEmployees> ImportEmployeesesByFire(List<TPEmployees> importList)
        {
            return repository.ImportEmployeesesByFire(db, importList);
        }

        public List<TPEmployees> ImportEmployeeses(List<TPEmployees> importList)
        {
            return repository.ImportEmployeeses(db, importList);
        }

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="type">0：SysField表查询；1:联系人</param>
        /// <param name="parm">参数</param>
        /// <returns></returns>
        public IQueryable GetCombox(string parm)
        {
            IQueryable queryInfo = from m in db.SysField
                                   where m.ParentId == parm
                                   orderby m.Sort
                                   select new { id = m.Id, text = m.MyTexts };
            return queryInfo;
        }

        #region   检查员工信息是否存在 create by chand 2016-04-05






        /// <summary>
        /// 检查员工信息是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public bool GetExists(string CustomerID, string IDCard)
        {
            return repository.GetExists(db, CustomerID, IDCard);
        }


        /// <summary>
        /// 根据客户编号和身份证获取员工信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID">客户编号</param>
        /// <param name="IDCard">身份证编号</param>
        /// <returns></returns>
        public TP_Employees GetEmployee(string CustomerID, string IDCard)
        {
            if (string.IsNullOrWhiteSpace(CustomerID) || string.IsNullOrWhiteSpace(IDCard))
            {
                return null;
            }
            return repository.GetEmployee(db, CustomerID, IDCard);
        }
        #endregion



        public List<PayCustomer> GetPayCustomerList(int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetPayCustomerList(db, page, rows, order, sort, search, ref total);
        }


        /// <summary>
        /// 员工列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<TPEmployees> GetPayEmployeesList(int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetPayEmployeesList(db, page, rows, order, sort, search, ref total);
        }



        /// <summary>
        /// 创建一个客户
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个客户</param>
        /// <returns></returns>
        public bool CreateOrEdit(ref ValidationErrors validationErrors, TP_Employees entity)
        {
            var model = db.TP_Employees.SingleOrDefault(w => w.CustomerID.Equals(entity.CustomerID, StringComparison.CurrentCultureIgnoreCase) && w.IDCard.Equals(entity.IDCard, StringComparison.CurrentCultureIgnoreCase));
            if (model != null)
            {
                model.EmpName = entity.EmpName;
                model.BankType = entity.BankType;
                model.BankCity = entity.BankCity;
                model.BankID = entity.BankID;
                model.PhoneNumber = entity.PhoneNumber;
                model.EmailAddress = entity.EmailAddress;
                model.IDType = entity.IDType;
                model.IDCard = entity.IDCard;
                model.Nation = entity.Nation;
                model.ContractSubject = entity.ContractSubject;
                model.WagesSubject = entity.WagesSubject;
                model.SocialSecuritySubject = entity.SocialSecuritySubject;
                model.TaxSubject = entity.TaxSubject;
                model.SocialInsurID = entity.SocialInsurID;
                model.HousingFundID = entity.HousingFundID;
                model.JobStatus = entity.JobStatus;
                return Edit(ref validationErrors, entity);
            }
            else
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
        }

    }
}
