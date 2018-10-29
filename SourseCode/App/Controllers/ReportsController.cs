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
    /// 创建人：chand
    /// 创建时间：2016-02-21
    /// 描述说明：报表-控制器
    /// </summary>
    [SupportFilter]
    public class ReportsController : BaseController
    {
        #region 初始化

        IBLL.ITBCustomerBLL m_BLL;

        public ReportsController()
            : this(new TBCustomerBLL())
        { }

        public ReportsController(TBCustomerBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();
        string currentPerson = "";


        #endregion

        #region 获取 客户汇总  create by chand 2016-03-02
        /// <summary>
        /// 客户汇总统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerSummary()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }



        /// <summary>
        /// 客户汇总
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerSummaryData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerSummaryModel> queryData;
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            queryData = m_BLL.GetCustomerSummary(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.ContractCount,
                    s.CustomerFunnel1,
                    s.CustomerFunnel2,
                    s.CustomerFunnel3,
                    s.CustomerFunnel4,
                    s.CustomerFunnel5,
                    s.CustomerFunnel6,
                    s.CustomerFunnel7,
                    s.CustomerNewCount,
                    s.CustomerTotal,
                    s.SysPersonName,
                    s.SysPersonID,
                    s.SortColumn,
                    s.SysDepartmentId,
                    s.SysDepartmentName,
                    s.FaceFollow,
                    s.TelFollow


                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerSummaryData(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            List<CustomerSummaryModel> queryData = m_BLL.GetCustomerSummary(GetCurrentPersonID(), id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }

        #endregion


        #region 获取 客户  按部门汇总  create by chand 2016-03-02
        /// <summary>
        /// 客户汇总统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerSummaryDept()
        {

            return View();
        }



        /// <summary>
        /// 客户汇总
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerSummaryDeptData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerSummaryModel> queryData;
            queryData = m_BLL.GetCustomerSummaryDept(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerSummaryDeptData(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            List<CustomerSummaryModel> queryData = m_BLL.GetCustomerSummaryDept(id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }

        #endregion

        #region 获取 客户跟进  create by chand 2016-03-02
        /// <summary>
        /// 客户汇总统计
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerFollow()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }



        /// <summary>
        /// 客户汇总
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerFollowData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerFollowReportModel> queryData;
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            queryData = m_BLL.GetCustomerFollow_Report(account.Id, id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CallPhone,
                    s.CustomerContactName,
                    s.CustomerFunnel,
                    s.CustomerFunnelName,
                    s.CustomerName,
                    s.ExpectedDate,
                    s.ExpectedMoney,
                    s.FollowUpDate,
                    s.FollowUpMode,
                    s.FollowUpModeName,
                    s.IsKP,
                    s.OtherLevel,
                    s.OtherLevelName,
                    s.Remark,
                    s.SysPersonName,
                    s.CustomerID

                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerFollow(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            List<CustomerFollowReportModel> queryData = m_BLL.GetCustomerFollow_Report(GetCurrentPersonID(), id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }


        #endregion




        #region 客户拜访
        /// <summary>
        /// 客户拜访
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerVisit()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }

        /// <summary>
        /// 客户拜访
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerVisitData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;

            List<CustomerVisitReportModel> queryData;
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            queryData = m_BLL.GetCustomerVisit_Report(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CustomerContactName,
                    s.CustomerFunnel,
                    s.CustomerFunnelName,
                    s.CustomerIndustry,
                    s.ExpectedDate,
                    s.ExpectedMoney,
                    s.FollowUpDate,
                    s.FollowUpMode,
                    s.FollowUpModeName,
                    s.IsKP,
                    s.OtherLevel,
                    s.OtherLevelName,
                    s.SysPersonName,
                    s.CustomerName,
                    s.CustomerID,
                    s.RecommendSolution
                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerVisit(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<CustomerVisitReportModel> queryData = m_BLL.GetCustomerVisit_Report(GetCurrentPersonID(), id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }

        #endregion

        #region 客户新进
        /// <summary>
        /// 客户新进
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerNewFollow()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }

        /// <summary>
        /// 客户新进
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerNewFollowData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<CustomerNewFollowReportModel> queryData;
            queryData = m_BLL.GetCustomerNewFollow_Report(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CityName,
                    s.ContactPerson,
                    s.ContactTel,
                    s.CreatedTime,
                    s.CustomerID,
                    s.CustomerName,
                    s.IsKP,
                    s.OtherLevel,
                    s.OtherLevelName,
                    s.RecommendSolutionName,
                    s.SortColumn,
                    s.SysPersonID,
                    s.SysPersonName,
                    s.VocationCode,
                    s.VocationName
                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerNewFollow(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<CustomerNewFollowReportModel> queryData = m_BLL.GetCustomerNewFollow_Report(GetCurrentPersonID(), id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }

        #endregion

        #region 客户进程
        /// <summary>
        /// 客户进程
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerProcess()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }

        /// <summary>
        /// 客户进程
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerProcessData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerProcessReportModel> queryData;
            queryData = m_BLL.GetCustomerProcess_Report(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CustomerFunnelName,
                    s.CustomerName,
                    s.CustomerID,
                    s.CustomerIndustry,
                    s.CustomerContactName,
                    s.ExpectedDate,
                    s.ExpectedMoney,
                    s.IsKP,
                    s.OtherLevel,
                    s.OtherLevelName,
                    s.SysPersonName,
                    s.RecommendSolution
                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerProcess(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<CustomerProcessReportModel> queryData = m_BLL.GetCustomerProcess_Report(GetCurrentPersonID(), id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion

        #region 客户合同
        /// <summary>
        /// 客户合同
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerContract()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }

            return View(new TB_Contract());
        }

        /// <summary>
        /// 客户合同
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerContractData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<ContractEntity> queryData;
            queryData = m_BLL.GetCustomerContract_Report(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerContract(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<ContractEntity> queryData = m_BLL.GetCustomerContract_Report(account.Id, id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion


        #region 获取 客户释放报表  create by chand 2016-03-17
        /// <summary>
        /// 客户释放报表
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerRelease()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }



        /// <summary>
        /// 客户释放报表
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerReleaseData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerReleaseReportModel> queryData;
            queryData = m_BLL.GetCustomerRelease_Report(account.Id, id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.BelongingDate,
                    s.CustomerID,
                    s.CustomerName,
                    s.ReleaseTime,
                    s.ExtractionPersonID,
                    s.ExtractionPersonName,
                    s.SysPersonID,

                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerRelease(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<CustomerReleaseReportModel> queryData = m_BLL.GetCustomerRelease_Report(account.Id, id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion


        #region 客户提取报表

        /// <summary>
        /// 客户提取报表
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerExtract()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }




        /// <summary>
        /// 客户提取报表
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerExtractData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerExtractReportModel> queryData;
            if (account != null)
            {
                id = account.Id;
            }
            queryData = m_BLL.GetCustomerExtract_Report(id, order, sort, search, page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.BelongingDate,
                    s.CallPhone,
                    s.CreatedTime,
                    s.CustomerContactName,
                    s.CustomerFunnelID,
                    s.CustomerFunnelName,
                    s.CustomerID,
                    s.CustomerName,
                    s.ExtractionPersonID,
                    s.ExtractionPersonName,
                    s.FollowType,
                    s.FollowUpDate,
                    s.FollowUpModeID,
                    s.FollowUpModeName,
                    s.IsKP,
                    s.ReleaseTime,
                    s.Remark


                }
                   )
            }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportCustomerExtract(string id, string title, string field, string sortName, string sortOrder, string search)
        {

            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');

            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerExtractReportModel> queryData;
            if (account != null)
            {
                id = account.Id;
            }
            queryData = m_BLL.GetCustomerExtract_Report(id, sortOrder, sortName, search, 1, 50000, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion


        #region 客户导入汇总

        public ActionResult ImportSummary()
        {

            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetImportSummaryData(string id, string order, string sort, string search)
        {

            var queryData = m_BLL.GetImportSummary_Report(sort, order);
            return Json(new datagrid
            {
                total = queryData.Count,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportImportSummary(string id, string title, string field, string sortName, string sortOrder, string search)
        {

            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account != null)
            {
                id = account.Id;
            }

            var queryData = m_BLL.GetImportSummary_Report(sortName, sortOrder);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion


        #region HR报表 by jonny 2017.01.11

        public ActionResult HRIndex()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }

            return View(new TB_Contract());
        }

        /// <summary>
        /// 考勤记录
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetHRListData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<HRListEntity> queryData;
            queryData = m_BLL.GetHRListInfo(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="field"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportHRListData(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            int total = 0;
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            Account account = GetCurrentAccount();
            if (account.FollowType == 2)
            {
                search += "FollowType&2^";
            }
            List<HRListEntity> queryData = m_BLL.GetHRListInfo(account.Id, id, 1, 50000, "", "", search, ref total);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion


    }
}