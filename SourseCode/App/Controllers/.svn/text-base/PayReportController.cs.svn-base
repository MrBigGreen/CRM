using Common;
using CRM.BLL;
using CRM.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 名称：薪酬管理报表
    /// 作者：Jonny
    /// 日期:2016.6.24
    /// </summary>
    public class PayReportController : BaseController
    {
        IBLL.IPayReportBLL m_BLL;
        public PayReportController()
            : this(new PayReportBLL())
        { }
        public PayReportController(PayReportBLL bll)
        {
            m_BLL = bll;
        }

        #region 基础界面
        //
        // GET: /PayReport/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 汇总数据查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SummaryReport()
        {
            return View();
        }
        /// <summary>
        /// 社保信息汇总查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SocialSecurityReport()
        {
            return View();
        }
        /// <summary>
        /// 公积金信息汇总查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FundsReport()
        {
            return View();
        }

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="parm">拓展参数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCombox(string parm)
        {
            Account account = GetCurrentAccount();
            var jsonInfo = m_BLL.GetCombox(parm, account.Id);
            var ckValue = Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);
            return ckValue;

        }
        public JsonResult GetComboxList(string parm)
        {
            Account account = GetCurrentAccount();
            var jsonInfo = m_BLL.GetComboxList(parm, account.Id);
            var ckValue = Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);
            return ckValue;

        }
        public JsonResult GetComboxChildList(string parm)
        {
            Account account = GetCurrentAccount();
            var jsonInfo = m_BLL.GetComboxChildList(parm);
            var ckValue = Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);
            return ckValue;

        }
        /// <summary>
        /// 工资支付审批表
        /// </summary>
        /// <returns></returns>
        public ActionResult WagesReport()
        {
            return View();
        }

        /// <summary>
        /// 社保支付审批表
        /// </summary>
        /// <returns></returns>
        public ActionResult SocialReport()
        {
            return View();
        }
        #endregion


        /// <summary>
        /// 汇总数据查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSummaryReportData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;

            List<PayReportEntity> queryData = m_BLL.GetSummaryReportData(account.Id, page, rows, order, sort, search, ref total);
            if (queryData == null)
                return null;
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    ContractID = s.ContractID,
                    ContractName = s.ContractName,
                    CustomerCounts = s.CustomerCounts,
                    PayPersonsCounts = s.PayPersonsCounts,
                    PayPersonsMoneys = s.PayPersonsMoneys,
                    RealPayPersonsCounts = s.RealPayPersonsCounts,
                    RealPayPersonsMoneys = s.RealPayPersonsMoneys,
                    DifferenceAmount = s.DifferenceAmount,
                    Remark = s.Remark

                }

                )
            });

        }

        /// <summary>
        /// 社保数据查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSocialSecurityReportData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;

            List<PayReportEntity> queryData = m_BLL.GetSocialSecurityReportData(account.Id, page, rows, order, sort, search, ref total);
            if (queryData == null)
                return null;
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    ContractID = s.ContractID,
                    ContractName = s.ContractName,
                    CustomerCounts = s.CustomerCounts,
                    CustomerNums = s.CustomerNums,
                    CustomerAmount = s.CustomerAmount,
                    PersonsNums = s.PersonsNums,
                    PersonsAmount = s.PersonsAmount,
                    RealPersonsNums = s.RealPersonsNums,
                    RealPersonsAmount = s.RealPersonsAmount,
                    RealCompanyAmount = s.RealCompanyAmount,
                    RealTotal = s.RealTotal,
                    SetDifferenceAmount = s.SetDifferenceAmount
                }

                )
            });

        }

        /// <summary>
        /// 公积金数据查询
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFundsReportData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;

            List<PayReportEntity> queryData = m_BLL.GetFundsReportData(account.Id, page, rows, order, sort, search, ref total);
            if (queryData == null)
                return null;
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    ContractID = s.ContractID,
                    ContractName = s.ContractName,
                    CustomerCounts = s.CustomerCounts,
                    CustomerNums = s.CustomerNums,
                    CustomerAmount = s.CustomerAmount,
                    PersonsNums = s.PersonsNums,
                    PersonsAmount = s.PersonsAmount,
                    RealPersonsNums = s.RealPersonsNums,
                    RealPersonsAmount = s.RealPersonsAmount,
                    RealCompanyAmount = s.RealCompanyAmount,
                    RealTotal = s.RealTotal,
                    SetDifferenceAmount = s.SetDifferenceAmount
                }

                )
            });

        }
        //工资局部视图
        public PartialViewResult GetWagesReportPartial(string ckValue, string ckText, string clValue, string clText, string StartDate)
        {
            List<PayReportEntity> listInfo = new List<PayReportEntity>();
            List<PayReportEntity> listPay = new List<PayReportEntity>();
            List<PayReportEntity> listBank = new List<PayReportEntity>();
            //查询数据
            if (!string.IsNullOrEmpty(ckValue) && !string.IsNullOrEmpty(StartDate))
            {
                Account account = GetCurrentAccount();
                
                //明细
                listInfo = m_BLL.GetWagesReportInfo(account.Id, clValue, StartDate);
                //银行
                listBank = m_BLL.GetWagesReportBankInfo(account.Id, clValue, StartDate);
                //支付
                listPay = m_BLL.GetWagesReportPayInfo(account.Id, clValue, StartDate);
            }
            ViewBag.CompanyName = ckText;
            ViewBag.CustomerName = clText;
            ViewBag.Month = DateTime.Parse(StartDate).Month;

            ViewBag.CustomerPayPersonsNums = "";
            ViewBag.CustomerPayPersonsAmount = "";
            ViewBag.PayPersonsSocialSecurityNums = "";
            ViewBag.PayPersonsSocialSecurityAmount = "";
            ViewBag.PayPersonsFundsNums = "";
            ViewBag.PayPersonsFundsAmount = "";
            ViewBag.PayPersonsTaxNums = "";
            ViewBag.PayPersonsTaxAmount = "";
            return PartialView("_GetWagesReportPartial", null);
        }


        /// <summary>
        /// 社保审核
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSocialReportData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;

            List<PayReportEntity> queryData = m_BLL.GetSocialReportData(account.Id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    ContractID = s.ContractID,
                    CustomerCounts = s.CustomerCounts,
                    PayPersonsCounts = s.PayPersonsCounts,
                    CompanyAcceptAmount = s.CompanyAcceptAmount,
                    PersonAcceptAmount = s.PersonAcceptAmount,
                    AcceptAmountTotal = s.AcceptAmountTotal

                }

                )
            });

        }

        ////社保局部视图
        //public PartialViewResult GetSocialReportPartial(string ckValue, string ckText, string StartDate)
        //{
        //    if (!string.IsNullOrEmpty(ckValue) && !string.IsNullOrEmpty(StartDate))
        //    {
        //        return PartialView("_GetSocialReportPartial", null);
        //    }
        //    return PartialView("_GetSocialReportPartial", null);
        //}
    }
}