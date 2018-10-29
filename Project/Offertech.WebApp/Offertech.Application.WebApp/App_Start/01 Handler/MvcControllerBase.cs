using Offertech.Application.Code;
using Offertech.Util;
using Offertech.Util.Log;
using Offertech.Util.WebControl;
using Offertech.Util.WeChat.SenparcTools;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using System;
using System.Web.Mvc;

namespace Offertech.Application.WebApp
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.11.9 10:45
    /// 描 述：控制器基类
    /// </summary>
    public abstract class MvcControllerBase : Controller
    {
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult ToJsonResult(object data)
        {
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message, resultdata = data }.ToJson(), "text/json");
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { type = ResultType.error, message = message }.ToJson());
        }
        /* 匿名请求：
  * 1. GET方式
  * 2. 入参增加code参数传值：code: '@Request["code"]'
  * 3. ListIntercept添加Controller、Action
  * 
  */
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 获取微信用户信息
            string UserId = string.Empty;
            string userAgent = Request.UserAgent;
            string jurl = "";
            if (userAgent.ToLower().Contains("micromessenger"))
            {  
                //从微信端访问
                if (Session["WxUserID"] == null || Session["WxUserID"].ToString().Length == 0)
                {
                    if (string.IsNullOrWhiteSpace(Request.QueryString["code"]))
                    {
                        jurl = OAuth2ApiHelper.GetCode(Request.Url.AbsoluteUri);
                        filterContext.Result = RedirectPermanent(jurl);
                        Logger.Info("jurl:" + jurl);
                    }
                    else
                    {
                        try
                        {
                            GetUserInfoResult result = OAuth2ApiHelper.GetUserId(Request.QueryString["code"]);
                            UserId = result.UserId ?? "";
                            Logger.Info("GetUserId:" + result.ToJson());
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                        Session["WxUserID"] = UserId;
                    }
                }
                else
                {
                    UserId = Session["WxUserID"] == null ? string.Empty : Session["WxUserID"].ToString();
                }
            }
            #endregion
            base.OnActionExecuting(filterContext);
        }

    }
}
