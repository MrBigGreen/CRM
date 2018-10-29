using Offertech.Application.Busines;
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Code;
using Offertech.Application.Entity;
using Offertech.Application.Entity.SystemManage;
using Offertech.Util;
using Offertech.Util.Attributes;
using Offertech.Util.Log;
using Offertech.Util.Offices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : Controller
    {
        UserBLL user = new UserBLL();
        DepartmentBLL department = new DepartmentBLL();

        #region 视图功能
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {
            return View();
        }
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminBeyond()
        {
            return View();
        }
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminLTE()
        {
            return View();
        }
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminWindos()
        {
            return View();
        }
        /// <summary>
        /// 我的桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult Desktop()
        {
            Offertech.Application.Busines.FlowManage.WFRuntimeBLL wfProcessBll = new Offertech.Application.Busines.FlowManage.WFRuntimeBLL();
            ViewBag.AgentsCount = wfProcessBll.GetToMeBeforeCount();
            return View();
        }
        /// <summary>
        /// 我的桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminLTEDesktop()
        {
            Offertech.Application.Busines.FlowManage.WFRuntimeBLL wfProcessBll = new Offertech.Application.Busines.FlowManage.WFRuntimeBLL();
            ViewBag.AgentsCount = wfProcessBll.GetToMeBeforeCount();
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 2;
            logEntity.OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.OperateAccount = OperatorProvider.Provider.Current().LoginInfo.Account;
            logEntity.OperateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            logEntity.ModuleId = moduleId;
            logEntity.Module = moduleName;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Content(moduleId);
        }
        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }
        #endregion
    }
}
