using Offertech.Application.Busines;
using Offertech.Application.Entity.ReportManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Offertech.Application.Busines.ReportManage;
using Offertech.Application.Entity.AuthorizeManage;
using Offertech.Application.Busines.AuthorizeManage;
using Offertech.Application.Code;

namespace Offertech.Application.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表管理
    /// </summary>
    public class CustomerController : MvcControllerBase
    {
        RptTempBLL rptTempBLL = new RptTempBLL();
        ModuleBLL modulebll = new ModuleBLL();

        #region 视图功能
        /// <summary>
        /// 客户汇总报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Summary()
        {
            return View();
        }

        /// <summary>
        /// 客户汇总报表-按部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SummaryDept()
        {
            return View();
        }

        /// <summary>
        /// 客户跟进报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Follow()
        {
            return View();
        }

        /// <summary>
        /// 客户新进报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// 我创建的客户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult MyselfCustomer()
        {
            return View();
        }

        /// <summary>
        /// 市场部导入客户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Market()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取销售报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSummaryJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetCustomerSummaryData(pagination, queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取销售报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSummaryDeptJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetCustomerSummaryByDeptData(pagination, queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取销售跟进报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCustomerFollowJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetCustomerFollowData(pagination, queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取新进客户报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCustomerNewJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetCustomerNewData(pagination, queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 我创建的客户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMyselfCustomerJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetMyselfCustomerData(pagination, queryJson);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 市场部导入客户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMarketCustomerJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetMarketCustomerData(pagination, queryJson);
            return Content(data.ToJson());
        }
        #endregion
    }
}
