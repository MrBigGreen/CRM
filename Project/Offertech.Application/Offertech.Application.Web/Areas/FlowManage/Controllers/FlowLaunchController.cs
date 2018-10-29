﻿using Offertech.Application.Busines.FlowManage;
using Offertech.Application.Entity.FlowManage;
using System;
using System.Collections.Generic;
using Offertech.Util;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offertech.Application.Code;
using Offertech.Application.Busines.SystemManage;

namespace Offertech.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.19 14:27
    /// 描 述：流程发起
    /// </summary>
    public class FlowLaunchController : MvcControllerBase
    {
        private WFRuntimeBLL wfProcessBll = new WFRuntimeBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        #region 视图功能
        //
        // GET: /FlowManage/FlowLaunch/
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewIndex()
        {
            return View();
        }
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowProcessNewForm()
        {
            ViewBag.Code = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, SystemInfo.CurrentModuleId);

            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="wfSchemeInfoId">流程模板信息Id</param>
        /// <param name="frmData">表单数据</param>
        /// <param name="type">0发起，3草稿</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult CreateProcess(string wfSchemeInfoId, string wfProcessInstanceJson, string frmData)
        {
            WFProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceJson.ToObject<WFProcessInstanceEntity>();
            wfProcessBll.CreateProcess(wfSchemeInfoId, wfProcessInstanceEntity, frmData);
            string text = "创建成功";
            if (wfProcessInstanceEntity.EnabledMark != 1)
            {
                text = "草稿保存成功";
            }
            return Success(text);
        }

        #endregion
    }
}
