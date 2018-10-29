using Common;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    /// Name:跟进任务接口
    /// Author：Jonny
    /// Date:2016-3-25
    /// </summary>
    [ServiceContract(Namespace = "www.Offer.com")]
    public partial interface ITPEmployeesBLL : IBaseDAL<TP_Employees>
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        [OperationContract]
        List<TPEmployees> GetDataEmployeesesList(int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <param name="EmployessName"></param>
        /// <param name="Gender"></param>
        /// <param name="CardType"></param>
        /// <param name="CardID"></param>
        /// <param name="BankName"></param>
        /// <param name="BankCity"></param>
        /// <param name="BankID"></param>
        /// <param name="Nation"></param>
        /// <param name="ContractType"></param>
        /// <param name="WagesType"></param>
        /// <param name="SocialSecurityType"></param>
        /// <param name="TaxType"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Email"></param>
        /// <param name="HiddPIC"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddEmployeeseInfo(string CompanyName, string EmployessName, string Gender, string CardType, string CardID, string BankName, string BankCity, string BankID, string Nation, string ContractType, string WagesType, string SocialSecurityType, string TaxType, string PhoneNum, string Email, string HiddPIC, string UserID);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="EmpID"></param>
        /// <param name="CompanyName"></param>
        /// <param name="EmployessName"></param>
        /// <param name="Gender"></param>
        /// <param name="CardType"></param>
        /// <param name="CardID"></param>
        /// <param name="BankName"></param>
        /// <param name="BankCity"></param>
        /// <param name="BankID"></param>
        /// <param name="Nation"></param>
        /// <param name="ContractType"></param>
        /// <param name="WagesType"></param>
        /// <param name="SocialSecurityType"></param>
        /// <param name="TaxType"></param>
        /// <param name="PhoneNum"></param>
        /// <param name="Email"></param>
        /// <param name="HiddPIC"></param>
        /// <param name="JobStatus"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateEmployeeseInfo(string CompanyID, string EmpID, string CompanyName, string EmployessName, string Gender, string CardType, string CardID, string BankName, string BankCity, string BankID, string Nation, string ContractType, string WagesType, string SocialSecurityType, string TaxType, string PhoneNum, string Email, string HiddPIC, string JobStatus, string UserID);

        /// <summary>
        /// 离职
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        bool FireEmployeeseInfo(string EmpID, string UserID);

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        [OperationContract]
        TPEmployees GetDataEmployeesesInfo(string EmpID);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="sortOrder"></param>
        /// <param name="sortName"></param>
        /// <param name="parm"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<TPEmployees> ExportInfo(string UserID, string sortOrder, string sortName, string parm, string type);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="importList"></param>
        /// <returns></returns>
        List<TPEmployees> ImportEmployeeses(List<TPEmployees> importList);
        /// <summary>
        /// 离职
        /// </summary>
        /// <param name="importList"></param>
        /// <returns></returns>
        List<TPEmployees> ImportEmployeesesByFire(List<TPEmployees> importList);
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        [OperationContract]
        IQueryable GetCombox(string typeID);

        List<PayCustomer> GetPayCustomerList(int page, int rows, string order, string sort, string search, ref int total);

        List<TPEmployees> GetPayEmployeesList(int page, int rows, string order, string sort, string search, ref int total);

        bool CreateOrEdit(ref ValidationErrors validationErrors, TP_Employees entity);
    }
}
