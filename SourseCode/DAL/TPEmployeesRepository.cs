using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：Jonny
    /// 创建时间：2016-3-25
    /// 描述说明：客户跟进任务
    /// </summary>
    public partial class TPEmployeesRepository : BaseRepository<TP_Employees>, IDisposable
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param> 
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<TP_Employees> DaoChuData(DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            string where = string.Empty;
            int flagWhere = 0;

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            if (queryDic != null && queryDic.Count > 0)
            {
                foreach (var item in queryDic)
                {
                    if (flagWhere != 0)
                    {
                        where += " and ";
                    }
                    flagWhere++;



                    if (queryDic.ContainsKey("SysOperation") && !string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key == "SysOperation")
                    {//查询多对多关系的列名
                        where += "EXISTS(select p from it.SysOperation as p where p.Id='" + item.Value + "')";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Time)) //开始时间
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(Start_Time)) + "] >=  CAST('" + item.Value + "' as   System.DateTime)";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Time)) //结束时间+1
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(End_Time)) + "] <  CAST('" + Convert.ToDateTime(item.Value).AddDays(1) + "' as   System.DateTime)";
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(Start_Int)) //开始数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(Start_Int)) + "] >= " + item.Value.GetInt();
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(End_Int)) //结束数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(End_Int)) + "] <= " + item.Value.GetInt();
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_Int)) //精确查询数值
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(DDL_Int)) + "] =" + item.Value;
                        continue;
                    }
                    if (!string.IsNullOrWhiteSpace(item.Key) && !string.IsNullOrWhiteSpace(item.Value) && item.Key.Contains(DDL_String)) //精确查询字符串
                    {
                        where += "it.[" + item.Key.Remove(item.Key.IndexOf(DDL_String)) + "] = '" + item.Value + "'";
                        continue;
                    }
                    where += "it.[" + item.Key + "] like '%" + item.Value + "%'";//模糊查询
                }
            }
            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                     .CreateObjectSet<TP_Employees>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }


        /// <summary>
        /// 通过主键id，获取菜单---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>菜单</returns>
        public TP_Employees GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取菜单---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>菜单</returns>
        public TP_Employees GetById(DB_CRMEntities db, string id)
        {
            return db.TP_Employees.SingleOrDefault(s => s.EmpID == id);
        }

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
                return Save(db);
            }
        }
        /// <summary>
        /// 删除一个菜单
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条菜单的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            TP_Employees deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                db.TP_Employees.Remove(deleteItem);
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
            IQueryable<TP_Employees> collection = from f in db.TP_Employees
                                                  where deleteCollection.Contains(f.EmpID)
                                                  select f;
            foreach (var deleteItem in collection)
            {
                db.TP_Employees.Remove(deleteItem);
            }
        }


        public void Dispose()
        {
        }

        /// <summary>
        /// 操作人员
        /// </summary>
        /// <param name="db"></param>
        /// <param name="OperationType">1:新增；2：修改 </param>
        /// <param name="CompanyName"></param>
        /// <param name="EmployessName"></param>
        /// <param name="Gender"></param>
        /// <param name="CardType"></param>
        /// <param name="CardID"></param>
        /// <param name="BankName"></param>
        /// <param name="BankCity"></param>
        /// <param name="BankID"></param>
        /// <param name="Nation"></param>
        /// <param name="Birthday"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Email"></param>
        /// <param name="HiddPIC"></param>
        /// <param name="UserID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public int ChangeEmployeeseInfo(DB_CRMEntities db, int OperationType, string CompanyID, string EmployessName, string Gender, string CardType,
            string CardID, string BankName, string BankCity, string BankID, string Nation, string ContractType, string WagesType, string SocialSecurityType, string TaxType,
            string PhoneNum, string Email, string HiddPIC, string UserID, string JobStatus, string EmployessID)
        {
            int isAdd = 0;
            string sql = string.Empty;
            var errorInfo = IDCardHelper.Validate(CardID.Trim());
            if (!string.IsNullOrEmpty(errorInfo))
            {
                return -1;
            }
            if (OperationType == 1)
            {

                int personCount = db.TP_Employees.Where(w => w.CustomerID.Equals(CompanyID) && w.IDCard.Equals(CardID)).Count();
                if (personCount > 0)
                {
                    return -1;
                }

            }
            else if (OperationType == 2)
            {
                int personCount = db.TP_Employees.Where(w => !w.EmpID.Equals(EmployessID) && w.IDCard.Equals(CardID)).Count();
                if (personCount > 0)
                {
                    return -1;
                }
            }
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    int a = 0;
                    string p1 = string.Empty, p2 = string.Empty, p3 = string.Empty;

                    if (!string.IsNullOrEmpty(HiddPIC))
                    {
                        string[] pics = HiddPIC.Split(';');
                        p1 = pics[0].ToString();
                        if (pics.Length > 1)
                            p2 = pics[1].ToString();
                        if (pics.Length > 2)
                            p3 = pics[2].ToString();
                    }

                    var Birthday = IDCardHelper.GetBirthday(CardID);
                    Gender = IDCardHelper.GetSexName(CardID) == "男" ? "1" : "0";
                    if (OperationType == 1)
                    {
                        sql = string.Format(@"INSERT INTO TP_Employees ( EmpID, EmpName, CustomerID, Gender, JobStatus, BankType, BankCity, BankID, Birthday, PhoneNumber, EmailAddress, Nation, IDType, IDCard, IDCardPic1, IDCardPic2, IDCardPic3, IsDelete, VersionNo, CreatedBy, CreatedTime, ContractSubject, WagesSubject, SocialSecuritySubject, TaxSubject ) VALUES ( N'{0}', N'{1}', N'{2}', {3}, 1, N'{4}', N'{5}', N'{6}', '{7}', '{8}', '{9}','{10}','{11}', '{12}', '{13}','{14}', '{15}', 0, 0, '{16}', GETDATE(),'{17}','{18}','{19}','{20}')", Result.GetNewId(), EmployessName, CompanyID, Gender, BankName, BankCity, BankID, Birthday, PhoneNum, Email, Nation, CardType, CardID, p1, p2, p3, UserID, ContractType, WagesType, SocialSecurityType, TaxType);
                    }
                    else if (OperationType == 2)
                    {
                        sql = string.Format(@"UPDATE TP_Employees SET EmpName = N'{0}', Gender = {1}, BankType = N'{2}', BankCity = N'{3}', BankID = N'{4}', Birthday = '{5}', PhoneNumber = '{6}', EmailAddress = '{7}', Nation = N'{8}', IDType = N'{9}', IDCard = N'{10}', IDCardPic1 = N'{11}', IDCardPic2 = N'{12}', IDCardPic3 = N'{13}', LastUpdatedBy = N'{14}', LastUpdatedTime = GETDATE(),JobStatus={15}, ContractSubject='{16}', WagesSubject='{17}', SocialSecuritySubject='{18}', TaxSubject='{19}'  WHERE EmpID = N'{20}'", EmployessName, Gender, BankName, BankCity, BankID, Birthday, PhoneNum, Email, Nation, CardType, CardID, p1, p2, p3, UserID, JobStatus, WagesType, SocialSecurityType, TaxType, EmployessID);
                    }


                    a = db.Database.ExecuteSqlCommand(sql);

                    if (a == 1)
                    {
                        transactionScope.Complete();
                        isAdd = 1;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
                catch (Exception)
                {
                    Transaction.Current.Rollback();
                }
            }

            return isAdd;
        }



        /// <summary>
        /// 离职
        /// </summary>
        /// <param name="db"></param>
        /// <param name="EmpID"></param>
        /// <param name="ComID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int FireEmployeeseInfo(DB_CRMEntities db, string EmpID, string UserID)
        {
            int isFire = 0;

            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    int a = 0;
                    string sql = string.Empty;
                    sql = string.Format(@"update TP_Employees SET JobStatus=2,LastUpdatedBy = N'{0}' ,LastUpdatedTime = GETDATE() WHERE EmpID = N'{1}'", UserID, EmpID);

                    a = db.Database.ExecuteSqlCommand(sql);

                    if (a == 1)
                    {
                        transactionScope.Complete();
                        isFire = 1;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
                catch (Exception)
                {
                    Transaction.Current.Rollback();
                }
            }

            return isFire;
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<TPEmployees> GetDataEmployeesesList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            SqlParameter[] param = new SqlParameter[12];

            param[0] = new SqlParameter("@EmployessName", SqlDbType.NVarChar, 100);
            param[0].Value = "";
            if (queryDic != null && queryDic.ContainsKey("EmployessName") && !string.IsNullOrWhiteSpace(queryDic["EmployessName"]))
            {
                param[0] = new SqlParameter("@EmployessName", queryDic["EmployessName"]);
            }
            param[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 100);
            param[1].Value = "";
            if (queryDic != null && queryDic.ContainsKey("CompanyName") && !string.IsNullOrWhiteSpace(queryDic["CompanyName"]))
            {
                param[1] = new SqlParameter("@CompanyName", queryDic["CompanyName"]);
            }

            param[2] = new SqlParameter("@PhoneNum", SqlDbType.NVarChar, 50);
            param[2].Value = "";
            if (queryDic != null && queryDic.ContainsKey("PhoneNum") && !string.IsNullOrWhiteSpace(queryDic["PhoneNum"]))
            {
                param[2] = new SqlParameter("@PhoneNum", queryDic["PhoneNum"]);
            }

            param[3] = new SqlParameter("@CardID", SqlDbType.NVarChar, 50);
            param[3].Value = "";
            if (queryDic != null && queryDic.ContainsKey("CardID") && !string.IsNullOrWhiteSpace(queryDic["CardID"]))
            {
                param[3] = new SqlParameter("@CardID", queryDic["CardID"]);
            }

            param[4] = new SqlParameter("@BankID", SqlDbType.NVarChar, 50);
            param[4].Value = "";
            if (queryDic != null && queryDic.ContainsKey("BankID") && !string.IsNullOrWhiteSpace(queryDic["BankID"]))
            {
                param[4] = new SqlParameter("@BankID", queryDic["BankID"]);
            }

            param[5] = new SqlParameter("@JobStatus", SqlDbType.NVarChar, 50);
            param[5].Value = "";
            if (queryDic != null && queryDic.ContainsKey("JobStatus") && !string.IsNullOrWhiteSpace(queryDic["JobStatus"]))
            {
                param[5] = new SqlParameter("@JobStatus", queryDic["JobStatus"]);
            }
            param[6] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[6].Value = page;
            param[7] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[7].Value = rows;
            param[8] = new SqlParameter("@orders", SqlDbType.NVarChar, 50);
            param[8].Value = order;
            param[9] = new SqlParameter("@sorts", SqlDbType.NVarChar, 50);
            param[9].Value = sort;
            param[10] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[11] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[10].Direction = ParameterDirection.Output;
            param[11].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<TPEmployees>("exec P_EmployeesesList_S  @EmployessName,@CompanyName,@PhoneNum,@CardID,@BankID,@JobStatus,@sys_PageIndex,@sys_PageSize,@orders,@sorts,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[11].Value);
            return query;
        }


        public TPEmployees GetDataEmployeesesInfo(DB_CRMEntities db, string EmpID)
        {


            var query = (from emp in db.TP_Employees
                         join c in db.TB_Customer on emp.CustomerID equals c.CustomerID
                         join s in db.TP_SocialInsur on emp.SocialInsurID equals s.SocialInsurID into emps
                         from temp1 in emps.DefaultIfEmpty()
                         join h in db.TP_HousingFund on emp.HousingFundID equals h.HousingFundID into emph
                         from temp2 in emph.DefaultIfEmpty()
                         where emp.EmpID == EmpID
                         select new TPEmployees
                         {
                             EmpID = emp.EmpID,
                             EmpName = emp.EmpName,
                             CustomerName = c.CustomerName,
                             Birthday = emp.Birthday,
                             BankCity = emp.BankCity,
                             BankID = emp.BankID,
                             BankType = emp.BankType,
                             IDCard = emp.IDCard,
                             IDCardPic1 = emp.IDCardPic1,
                             IDCardPic2 = emp.IDCardPic2,
                             IDCardPic3 = emp.IDCardPic3,
                             JobStatus = emp.JobStatus,
                             CustomerID = emp.CustomerID,
                             IDType = emp.IDType,
                             PhoneNumber = emp.PhoneNumber,
                             EmailAddress = emp.EmailAddress,
                             ContractSubject = emp.ContractSubject,
                             WagesSubject = emp.WagesSubject,
                             SocialSecuritySubject = emp.SocialSecuritySubject,
                             TaxSubject = emp.TaxSubject,
                             Gender = emp.Gender,
                             SocialInsurID = temp1.SocialInsurID,
                             SocialInsurName = temp1.SocialInsurName,
                             HousingFundID = temp2.HousingFundID,
                             HousingFundName = temp2.HousingFundName

                         }).SingleOrDefault();


            return query;
        }

        public List<TPEmployees> GetDataEmployeesesListByExcel(DB_CRMEntities db, string UserID, string sortOrder, string sortName, string parm, string type)
        {
            List<TPEmployees> query = new List<TPEmployees>();
            if (type == "0")
            {
                Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(parm.GetString());
                SqlParameter[] param = new SqlParameter[8];

                param[0] = new SqlParameter("@EmployessName", SqlDbType.NVarChar, 100);
                param[0].Value = "";
                if (queryDic != null && queryDic.ContainsKey("EmployessName") &&
                    !string.IsNullOrWhiteSpace(queryDic["EmployessName"]))
                {
                    param[0] = new SqlParameter("@EmployessName", queryDic["EmployessName"]);
                }
                param[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 100);
                param[1].Value = "";
                if (queryDic != null && queryDic.ContainsKey("CompanyName") &&
                    !string.IsNullOrWhiteSpace(queryDic["CompanyName"]))
                {
                    param[1] = new SqlParameter("@CompanyName", queryDic["CompanyName"]);
                }

                param[2] = new SqlParameter("@PhoneNum", SqlDbType.NVarChar, 50);
                param[2].Value = "";
                if (queryDic != null && queryDic.ContainsKey("PhoneNum") &&
                    !string.IsNullOrWhiteSpace(queryDic["PhoneNum"]))
                {
                    param[2] = new SqlParameter("@PhoneNum", queryDic["PhoneNum"]);
                }

                param[3] = new SqlParameter("@CardID", SqlDbType.NVarChar, 50);
                param[3].Value = "";
                if (queryDic != null && queryDic.ContainsKey("CardID") && !string.IsNullOrWhiteSpace(queryDic["CardID"]))
                {
                    param[3] = new SqlParameter("@CardID", queryDic["CardID"]);
                }

                param[4] = new SqlParameter("@BankID", SqlDbType.NVarChar, 50);
                param[4].Value = "";
                if (queryDic != null && queryDic.ContainsKey("BankID") && !string.IsNullOrWhiteSpace(queryDic["BankID"]))
                {
                    param[4] = new SqlParameter("@BankID", queryDic["BankID"]);
                }

                param[5] = new SqlParameter("@JobStatus", SqlDbType.NVarChar, 50);
                param[5].Value = "";
                if (queryDic != null && queryDic.ContainsKey("JobStatus") &&
                    !string.IsNullOrWhiteSpace(queryDic["JobStatus"]))
                {
                    param[5] = new SqlParameter("@JobStatus", queryDic["JobStatus"]);
                }

                param[6] = new SqlParameter("@orders", SqlDbType.NVarChar, 50);
                param[6].Value = sortName;
                param[7] = new SqlParameter("@sorts", SqlDbType.NVarChar, 50);
                param[7].Value = sortOrder;


                query =
                   db.Database.SqlQuery<TPEmployees>(
                       "exec P_EmployeesesListExport_S   @EmployessName,@CompanyName,@PhoneNum,@CardID,@BankID,@JobStatus,@orders,@sorts",
                       param).ToList();
            }
            else
            {
                string[] empids = parm.Substring(0, parm.Length - 1).Split(';');

                query = (from s in db.TP_Employees
                         join c in db.TB_Customer on s.CustomerID equals c.CustomerID
                         join b in db.SysField on s.BankType equals b.Id
                         join i in db.SysField on s.IDType equals i.Id
                         where empids.Contains(s.EmpID)
                         select new TPEmployees
                         {
                             EmpID = s.EmpID,
                             EmpName = s.EmpName,
                             CustomerName = c.CustomerName,
                             Birthday = s.Birthday,
                             BankCity = s.BankCity,
                             BankID = s.BankID,
                             BankType = s.BankType,
                             IDCard = s.IDCard,
                             IDCardPic1 = s.IDCardPic1,
                             IDCardPic2 = s.IDCardPic2,
                             IDCardPic3 = s.IDCardPic3,
                             JobStatus = s.JobStatus,
                             CustomerID = s.CustomerID,
                             IDType = s.IDType,
                             GenderName = s.Gender == 1 ? "男" : "女",
                             JobStatusName = s.JobStatus == 1 ? "在职" : "离职",
                             BankTypeName = b.MyTexts,
                             IDTypeName = i.MyTexts

                         }).Distinct().ToList();

            }
            return query;
        }

        public List<TPEmployees> ImportEmployeeses(DB_CRMEntities db, List<TPEmployees> importList)
        {
            List<TPEmployees> errorList = new List<TPEmployees>();
            if (importList.Count > 0)
            {
                string cname = string.Empty;
                string companyID = string.Empty;
                string idCardName = string.Empty;
                string idCardNameType = string.Empty;
                string cardTypeID = string.Empty;
                string bankName = string.Empty;
                string bankID = string.Empty;

                //主体 ContractSubject,items.WagesSubject,items.SocialSecuritySubject,items.TaxSubject
                string contractName = string.Empty;
                string contractID = string.Empty;

                string wagesName = string.Empty;
                string wagesID = string.Empty;

                string socialSecurityName = string.Empty;
                string socialSecurityID = string.Empty;

                string taxName = string.Empty;
                string taxID = string.Empty;

                int isAdd = 0;
                string sql = string.Empty;

                foreach (var items in importList)
                {
                    try
                    {
                        idCardName = items.IDType.Trim();
                        bool isTrue = true;
                        if (idCardName == "身份证")
                        {
                            var errorInfo = IDCardHelper.Validate(items.IDCard.Trim());
                            if (!string.IsNullOrEmpty(errorInfo))
                            {
                                items.ErrorInfo = errorInfo;
                                errorList.Add(items);
                                isTrue = false;
                            }
                        }

                        if (isTrue)
                        {
                            var Birthday = IDCardHelper.GetBirthday(items.IDCard.Trim());
                            var Gender = IDCardHelper.GetSexName(items.IDCard.Trim()) == "男" ? "1" : "0";
                            if (idCardNameType != idCardName)
                            {
                                cardTypeID = db.SysField.Where(w => w.MyTexts.Equals(idCardName))
                                            .Select(s => s.Id)
                                            .SingleOrDefault();
                                idCardNameType = idCardName;
                            }
                            if (bankName != items.BankType)
                            {
                                bankID = db.SysField.Where(w => w.MyTexts.Equals(items.BankType))
                                            .Select(s => s.Id)
                                            .SingleOrDefault();
                                bankName = items.BankType;
                            }
                            string companyName = items.CustomerName;
                            if (cname != companyName)
                            {
                                companyID =
                                    db.TB_Customer.Where(w => w.CustomerName.Equals(companyName))
                                        .Select(s => s.CustomerID)
                                        .SingleOrDefault();
                                cname = items.CustomerName;
                            }

                            //主体
                            if (contractName != items.ContractSubjectName && !string.IsNullOrEmpty(items.ContractSubjectName))
                            {
                                contractID = db.SysField.Where(w => w.MyTexts.Equals(items.ContractSubjectName))
                                            .Select(s => s.Id)
                                            .SingleOrDefault();
                                contractName = items.ContractSubjectName;
                            }
                            if (wagesName != items.WagesSubjectName && !string.IsNullOrEmpty(items.WagesSubjectName))
                            {
                                wagesID = db.SysField.Where(w => w.MyTexts.Equals(items.WagesSubjectName))
                                            .Select(s => s.Id)
                                            .SingleOrDefault();
                                wagesName = items.WagesSubjectName;
                            }
                            if (socialSecurityName != items.SocialSecuritySubjectName && !string.IsNullOrEmpty(items.SocialSecuritySubjectName))
                            {
                                socialSecurityID = db.SysField.Where(w => w.MyTexts.Equals(items.SocialSecuritySubjectName))
                                            .Select(s => s.Id)
                                            .SingleOrDefault();
                                socialSecurityName = items.SocialSecuritySubjectName;
                            }
                            if (taxName != items.TaxSubjectName && !string.IsNullOrEmpty(items.TaxSubjectName))
                            {
                                taxID = db.SysField.Where(w => w.MyTexts.Equals(items.TaxSubjectName))
                                            .Select(s => s.Id)
                                            .SingleOrDefault();
                                taxName = items.TaxSubjectName;
                            }


                            if (!string.IsNullOrEmpty(companyID))
                            {
                                int count =
                                    db.TP_Employees.Where(
                                        w => w.CustomerID.Equals(companyID) && w.IDCard.Equals(items.IDCard)).Select(s => s.EmpID).Count();
                                if (count == 0)
                                {

                                    using (TransactionScope transactionScope = new TransactionScope())
                                    {
                                        sql = string.Format(@"INSERT INTO TP_Employees ( EmpID, EmpName, CustomerID, Gender, JobStatus, BankType, BankCity, BankID, Birthday, PhoneNumber, EmailAddress, Nation, IDType, IDCard, IsDelete, VersionNo, CreatedBy, CreatedTime,ContractSubject, WagesSubject, SocialSecuritySubject, TaxSubject) VALUES ( N'{0}', N'{1}', N'{2}', {3}, 1, N'{4}', N'{5}', N'{6}', '{7}', '{8}', '{9}','{10}','{11}', '{12}', 0, 0, '{13}', GETDATE(),'{14}', '{15}', '{16}', '{17}')", items.EmpID, items.EmpName, companyID, Gender, bankID, items.BankCity, items.BankID, Birthday, items.PhoneNumber, items.EmailAddress, items.Nation, cardTypeID, items.IDCard, items.CreatedBy, contractID, wagesID, socialSecurityID, taxID);
                                        isAdd = db.Database.ExecuteSqlCommand(sql);
                                        if (isAdd == 1)
                                        {
                                            transactionScope.Complete();
                                        }
                                        else
                                        {
                                            Transaction.Current.Rollback();
                                            items.ErrorInfo = "保存失败";
                                            errorList.Add(items);
                                        }
                                    }
                                }
                                else
                                {
                                    items.ErrorInfo = "身份证已存在";
                                    errorList.Add(items);
                                }
                            }
                            else
                            {
                                items.ErrorInfo = "公司不存在";
                                errorList.Add(items);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        items.ErrorInfo = ex.Message;
                        errorList.Add(items);
                    }
                }
            }
            return errorList;
        }

        public List<TPEmployees> ImportEmployeesesByFire(DB_CRMEntities db, List<TPEmployees> importList)
        {
            List<TPEmployees> errorList = new List<TPEmployees>();
            if (importList.Count > 0)
            {
                string cname = string.Empty;
                string companyID = string.Empty;
                int isUpdate = 0;
                string sql = string.Empty;
                foreach (var items in importList)
                {
                    try
                    {
                        string companyName = items.CustomerName;
                        if (cname != companyName)
                        {
                            companyID =
                                db.TB_Customer.Where(w => w.CustomerName.Equals(companyName))
                                    .Select(s => s.CustomerID)
                                    .SingleOrDefault();
                            cname = items.CustomerName;
                        }
                        if (!string.IsNullOrEmpty(companyID))
                        {
                            string EmpID =
                                db.TP_Employees.Where(
                                    w => w.CustomerID.Equals(companyID) && w.IDCard.Equals(items.IDCard)).Select(s => s.EmpID).SingleOrDefault();
                            if (!string.IsNullOrEmpty(EmpID))
                            {

                                using (TransactionScope transactionScope = new TransactionScope())
                                {
                                    sql = string.Format(@"UPDATE TP_Employees SET LastUpdatedBy = N'{0}', LastUpdatedTime = GETDATE(),JobStatus=2 WHERE EmpID = N'{1}'", items.CreatedBy, EmpID);
                                    isUpdate = db.Database.ExecuteSqlCommand(sql);
                                    if (isUpdate == 1)
                                    {
                                        transactionScope.Complete();
                                    }
                                    else
                                    {
                                        Transaction.Current.Rollback();
                                        items.ErrorInfo = "修改失败";
                                        errorList.Add(items);
                                    }
                                }
                            }
                            else
                            {
                                items.ErrorInfo = "员工不存在";
                                errorList.Add(items);
                            }
                        }
                        else
                        {
                            items.ErrorInfo = "公司不存在";
                            errorList.Add(items);
                        }

                    }
                    catch (Exception ex)
                    {
                        items.ErrorInfo = ex.Message;
                        errorList.Add(items);
                    }
                }
            }
            return errorList;
        }




        #region 检查员工信息是否存在 create by chand 2016-04-05
        /// <summary>
        /// 检查员工信息是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public bool GetExists(DB_CRMEntities db, string CustomerID, string IDCard)
        {
            var entity = db.TP_Employees.Where(s => s.CustomerID == CustomerID && s.IDCard == IDCard).SingleOrDefault();
            if (entity == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 根据客户编号和身份证获取员工信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID">客户编号</param>
        /// <param name="IDCard">身份证编号</param>
        /// <returns></returns>
        public TP_Employees GetEmployee(DB_CRMEntities db, string CustomerID, string IDCard)
        {

            var entity = db.TP_Employees.Where(s => s.CustomerID == CustomerID && s.IDCard == IDCard).FirstOrDefault();


            return entity;

        }
        #endregion


        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<PayCustomer> GetPayCustomerList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            SqlParameter[] param = new SqlParameter[7];

            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }


            if (queryDic != null && queryDic.ContainsKey("SearchPersonID") && !string.IsNullOrWhiteSpace(queryDic["SearchPersonID"]))
            {
                param[1] = new SqlParameter("@SearchPersonID", queryDic["SearchPersonID"]);
            }
            else
            {
                param[1] = new SqlParameter("@SearchPersonID", "");
            }


            if (string.IsNullOrWhiteSpace(sort))
            {
                param[2] = new SqlParameter("@order", "");
            }
            else
            {
                param[2] = new SqlParameter("@order", sort + " " + order);
            }



            param[3] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[3].Value = page;
            param[4] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[4].Value = rows;
            param[5] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[6] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[5].Direction = ParameterDirection.Output;
            param[6].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<PayCustomer>("exec P_PayCustomerList_S   @CustomerName,@SearchPersonID,@Order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[6].Value);
            return query;
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
        public List<TPEmployees> GetPayEmployeesList(DB_CRMEntities db, int page, int rows, string order, string sort, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            SqlParameter[] param = new SqlParameter[12];

            if (queryDic != null && queryDic.ContainsKey("EmpName") && !string.IsNullOrWhiteSpace(queryDic["EmpName"]))
            {
                param[0] = new SqlParameter("@EmpName", queryDic["EmpName"]);
            }
            else
            {
                param[0] = new SqlParameter("@EmpName", "");
            }


            if (queryDic != null && queryDic.ContainsKey("PhoneNumber") && !string.IsNullOrWhiteSpace(queryDic["PhoneNumber"]))
            {
                param[1] = new SqlParameter("@PhoneNumber", queryDic["PhoneNumber"]);
            }
            else
            {
                param[1] = new SqlParameter("@PhoneNumber", "");
            }

            if (queryDic != null && queryDic.ContainsKey("IDCard") && !string.IsNullOrWhiteSpace(queryDic["IDCard"]))
            {
                param[2] = new SqlParameter("@IDCard", queryDic["IDCard"]);
            }
            else
            {
                param[2] = new SqlParameter("@IDCard", "");
            }


            if (queryDic != null && queryDic.ContainsKey("BankID") && !string.IsNullOrWhiteSpace(queryDic["BankID"]))
            {
                param[3] = new SqlParameter("@BankID", queryDic["BankID"]);
            }
            else
            {
                param[3] = new SqlParameter("@BankID", "");
            }


            if (queryDic != null && queryDic.ContainsKey("JobStatus") && !string.IsNullOrWhiteSpace(queryDic["JobStatus"]))
            {
                param[4] = new SqlParameter("@JobStatus", queryDic["JobStatus"]);
            }
            else
            {
                param[4] = new SqlParameter("@JobStatus", "");
            }

            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[5] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[5] = new SqlParameter("@CustomerID", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SearchPersonID") && !string.IsNullOrWhiteSpace(queryDic["SearchPersonID"]))
            {
                param[6] = new SqlParameter("@SearchPersonID", queryDic["SearchPersonID"]);
            }
            else
            {
                param[6] = new SqlParameter("@SearchPersonID", "");
            }

            if (string.IsNullOrWhiteSpace(sort))
            {
                param[7] = new SqlParameter("@order", "");
            }
            else
            {
                param[7] = new SqlParameter("@order", sort + " " + order);
            }



            param[8] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[8].Value = page;
            param[9] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[9].Value = rows;
            param[10] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[11] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[10].Direction = ParameterDirection.Output;
            param[11].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<TPEmployees>("exec P_PayEmployeesList_S  @EmpName,@PhoneNumber,@IDCard,@BankID,@JobStatus,@CustomerID,@SearchPersonID,@Order,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[11].Value);
            return query;
        }
    }
}
