using Offertech.Application.Busines.ReportManage;
using Offertech.Application.Code;
using Offertech.Util;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2017.4.07 14:27
    /// 描 述：合同报表管理
    /// </summary>
    public class ContractController : MvcControllerBase
    {

        RptTempBLL rptTempBLL = new RptTempBLL();

        #region 视图功能
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 销售合同报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Sale()
        {
            return View();
        }

        /// <summary>
        /// 客服合同报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Service()
        {
            return View();
        }

     

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取销售合同数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetContractSaleJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetContractSaleData(pagination, queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取客服报表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetContractServiceJson(Pagination pagination, string queryJson)
        {
            var data = rptTempBLL.GetContractServiceData(pagination, queryJson);
            return Content(data.ToJson());
        }
      
        #endregion
    }
}
