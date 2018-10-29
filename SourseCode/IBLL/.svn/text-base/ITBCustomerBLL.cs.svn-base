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
    /// 客户信息 接口
    /// </summary>
    [ServiceContract(Namespace = "www.Offer.com")]
    public interface ITBCustomerBLL : IBaseDAL<TB_Customer>
    {
        List<CustomerModel> GetData(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        List<CustomerModel> GetCustomerData_KA(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 我的客户列表查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>

        List<CustomerModel> GetCustomerData_Self(string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total);


        List<CustomerModel> GetPublicData(string id, int page, int rows, string order, string sort, string search, ref int total);

        List<CustomerRepeatEntity> GetCustomerRepeat(string CustomerNewName, string CustomerID, ref  int result);

        bool IsCreate(string CurrentPersonID);

        bool SendVerification(ref ValidationErrors validationErrors, string CustomerNewName, string CurrentPersonID);

        bool GetVerification(ref ValidationErrors validationErrors, string CustomerNewName, string VerificationCode);

        List<CustomerModel> GetCustomerAuditData(string id, int page, int rows, string order, string sort, string search, ref int total);


        /// <summary>
        /// 客户审核
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="isPass"></param>
        /// <param name="CustomerID"></param>
        /// <param name="Reason"></param>
        /// <param name="AuditPerson"></param>
        /// <returns></returns>
        bool GetAuditCustomer(ref ValidationErrors validationErrors, bool isPass, string CustomerID, string Reason, string AuditPerson);

        /// <summary>
        /// 获取客户操作权限
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="CustomerID"></param>
        /// <param name="SysPersonID"></param>
        /// <returns>
        /// -1没有操作权限 
        /// 0 客户最大操作权限 
        /// 1 只读 
        /// 2 可以编辑权限
        /// </returns>
        int GetCustomerAuthority(string CustomerID, string SysPersonID);

        /// <summary>
        /// 客户释放
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        bool GetCustomerRelease(ref ValidationErrors validationErrors, string CustomerID, string CurrentPersonID);

        /// <summary>
        /// 客户提取
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        bool GetCustomerExtraction(ref ValidationErrors validationErrors, string CustomerID, string CurrentPersonID);

        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CustomerName"></param>
        /// <param name="CurrentPersonID"></param>
        /// <returns></returns>
        bool GetUpdateCustomerName(ref ValidationErrors validationErrors, string CustomerID, string CustomerName, string CurrentPersonID);


        /// <summary>
        /// 客户进程统计
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        List<CustomerFunnel> GetFunnelStatistics(string SearchPersonID, string id, string search);


        /// <summary>
        /// 获取客户长时间未联系数量
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        long GetCustomerNoContactQty(string SearchPersonID, string id, string search);


        /// <summary>
        /// 客户汇总数据
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerSummaryModel> GetCustomerSummary(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        List<CustomerSummaryModel> GetCustomerSummaryDept(string id, int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 客户进程数据
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerFollowReportModel> GetCustomerFollow_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 客户拜访
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerVisitReportModel> GetCustomerVisit_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 客户拜访
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerNewFollowReportModel> GetCustomerNewFollow_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);



        /// <summary>
        /// 客户进程
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerProcessReportModel> GetCustomerProcess_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 客户合同
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ContractEntity> GetCustomerContract_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 客户释放报表
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerReleaseReportModel> GetCustomerRelease_Report(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);


        /// <summary>
        /// 客户提取报表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerExtractReportModel> GetCustomerExtract_Report(string id, string order, string sort, string search, int page, int rows, ref int total);

        /// <summary>
        /// 获取无效客户
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        List<CustomerModel> GetLogoffData(string id, int page, int rows, string order, string sort, string search, ref int total);


        /// <summary>
        /// 激活客户
        /// </summary>
        /// <param name="validationErrors"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        bool GetActivating(ref ValidationErrors validationErrors, string id);

        /// <summary>
        /// 获取是薪资客户的信息
        /// </summary>
        /// <returns></returns>
        List<TB_Customer> GetSalaryCustomer();

        List<ImportSummaryReportModel> GetImportSummary_Report(string sort, string order);

        List<CustomerServiceModel> GetCustomerServiceList(int page, int rows, string order, string sort, string search, ref int total);


        List<CustomerServiceModel> GetCustomerSelectedList(int page, int rows, string order, string sort, string search, ref int total);


        /// <summary>
        /// 客户合同
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<HRListEntity> GetHRListInfo(string SearchPersonID, string id, int page, int rows, string order, string sort, string search, ref int total);
    }
}
