using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    public partial interface IPayReportBLL
    {
        /// <summary>
        ///  获取下拉框
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        List<ComboxEntity> GetCombox(string parm, string roleID);
        /// <summary>
        ///  获取下拉框
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [OperationContract]
        List<ComboxEntity> GetComboxList(string parm, string roleID);

        /// <summary>
        ///  获取下拉框
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [OperationContract]
        List<ComboxEntity> GetComboxChildList(string parm);
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
        [OperationContract]
        List<PayReportEntity> GetSummaryReportData(string id, int page, int rows, string order, string sort, string search, ref int total);
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
        [OperationContract]
        List<PayReportEntity> GetSocialSecurityReportData(string id, int page, int rows, string order, string sort, string search, ref int total);
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
        [OperationContract]
        List<PayReportEntity> GetFundsReportData(string id, int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 工资审核银行list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="checkValue"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<PayReportEntity> GetWagesReportBankInfo(string id, string checkValue, string date);
        /// <summary>
        /// 明细list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="checkValue"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<PayReportEntity> GetWagesReportInfo(string id, string checkValue, string date);
        /// <summary>
        /// 支付list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="checkValue"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [OperationContract]
        List<PayReportEntity> GetWagesReportPayInfo(string id, string checkValue, string date);
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
        [OperationContract]
        List<PayReportEntity> GetSocialReportData(string id, int page, int rows, string order, string sort, string search, ref int total);

    }
}
