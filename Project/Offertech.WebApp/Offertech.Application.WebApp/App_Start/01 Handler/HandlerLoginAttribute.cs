using System.Web.Mvc;
using Offertech.Application.Code;
using Offertech.Util;
using System.Web;
using Offertech.Util.Log;
using Offertech.Util.WeChat.SenparcTools;

namespace Offertech.Application.WebApp
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.11.9 10:45
    /// 描 述：登录认证（会话验证组件）
    /// </summary>
    public class HandlerLoginAttribute : AuthorizeAttribute
    {
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }


        private LoginMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerLoginAttribute(LoginMode Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录拦截是否忽略
            if (_customMode == LoginMode.Ignore)
            {
                return;
            }
            string userAgent = filterContext.HttpContext.Request.UserAgent;
            Logger.Info("UserAgent：" + userAgent);
            if (!userAgent.ToLower().Contains("micromessenger"))
            {
                //登录是否过期
                if (OperatorProvider.Provider.IsOverdue())
                {
                    Logger.Info("登录已超时");
                    WebHelper.WriteCookie("learun_login_error", "Overdue");//登录已超时,请重新登录
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    return;
                }
                //是否已登录
                var OnLine = OperatorProvider.Provider.IsOnLine();
                if (OnLine == 0)
                {
                    Logger.Info("您的帐号已在其它地方登录");
                    if (!OperatorProvider.Provider.Current().LoginInfo.Account.Equals("offercom", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        WebHelper.WriteCookie("learun_login_error", "OnLine");//您的帐号已在其它地方登录,请重新登录
                        filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    return;
                }
                else if (OnLine == -1)
                {
                    Logger.Info("缓存已超时,请重新登录");
                    WebHelper.WriteCookie("learun_login_error", "-1");//缓存已超时,请重新登录
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    return;
                }
            }
            return;
        }


    }
}