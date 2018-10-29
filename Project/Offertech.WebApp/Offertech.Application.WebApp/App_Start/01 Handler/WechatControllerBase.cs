using Offertech.Application.Code;
using Offertech.Util;
using Offertech.Util.Log;
using Offertech.Util.WebControl;
using System.Web.Mvc;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;
using Offertech.Util.WeChat.SenparcTools;
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Entity.BaseManage;
using System;
using Offertech.Application.Busines.AuthorizeManage;

namespace Offertech.Application.WebApp
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.11.9 10:45
    /// 描 述：控制器基类
    /// </summary>

    public abstract class WechatControllerBase : Controller
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
            string UserId = string.Empty;
            string userAgent = Request.UserAgent;
            string jurl = "";
            if (userAgent.ToLower().Contains("micromessenger"))
            { //从微信端访问
                if (Session["WxUserID"] == null || Session["WxUserID"].ToString().Length == 0)
                {
                    #region 获取微信用户信息
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
                    #endregion
                }
                else
                {
                    UserId = Session["WxUserID"] == null ? string.Empty : Session["WxUserID"].ToString();
                }
            }
            if (!IsLogin())
            {

                if (userAgent.ToLower().Contains("micromessenger"))
                {
                    #region 微信登录

                    if (!string.IsNullOrWhiteSpace(UserId))
                    {
                        try
                        {
                            UserEntity userEntity = new UserBLL().WechatLogin(UserId);
                            if (userEntity != null)
                            {
                                #region 记录登录成功信息
                                AuthorizeBLL authorizeBLL = new AuthorizeBLL();
                                Operator operators = new Operator();
                                LoginUserModel LoginUser = new LoginUserModel();
                                LoginUser.UserId = userEntity.UserId;
                                LoginUser.Code = userEntity.EnCode;
                                LoginUser.Account = userEntity.Account;
                                LoginUser.UserName = userEntity.RealName;
                                LoginUser.Password = userEntity.Password;
                                LoginUser.Secretkey = userEntity.Secretkey;
                                LoginUser.CompanyId = userEntity.OrganizeId;
                                LoginUser.DepartmentId = userEntity.DepartmentId;
                                LoginUser.ManagerId = userEntity.ManagerId;
                                LoginUser.Manager = userEntity.Manager;
                                LoginUser.HeadIcon = userEntity.HeadIcon;
                                LoginUser.IPAddress = Net.Ip;
                                LoginUser.IPAddressName = IPLocation.GetLocation(Net.Ip);
                                LoginUser.LogTime = DateTime.Now;
                                LoginUser.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                                operators.LoginInfo = LoginUser;


                                // 写入当前用户数据权限
                                AuthorizeDataModel dataAuthorize = new AuthorizeDataModel();
                                dataAuthorize.ReadAutorize = authorizeBLL.GetDataAuthor(operators);
                                dataAuthorize.ReadAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators);
                                dataAuthorize.WriteAutorize = authorizeBLL.GetDataAuthor(operators, true);
                                dataAuthorize.WriteAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators, true);
                                operators.DataAuthorize = dataAuthorize;
                                OperatorProvider.AppUserId = userEntity.UserId;
                                OperatorProvider.Provider.AddCurrent(operators);
                                Logger.Info("微信登录写入缓存:" + LoginUser.ToJson());
                                #endregion
                            }
                            else
                            {
                                Logger.Info("登录失败1");
                                //跳转到登录页面
                                filterContext.Result = new RedirectResult("~/Login/Index");
                            }

                        }
                        catch (Exception ex)
                        {
                            Logger.Info("登录异常");
                            Logger.Error(ex);
                            //跳转到登录页面
                            filterContext.Result = new RedirectResult("~/Login/Index");
                        }
                    }
                    #endregion
                }
                else
                {
                    Logger.Info("非微信--登录失败1");
                    //跳转到登录页面
                    filterContext.Result = new RedirectResult("~/Login/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }


        /// <summary>
        /// 响应前执行登录验证,查看当前用户是否有效 
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            //登录是否过期
            if (OperatorProvider.Provider.IsOverdue())
            {
                return false;
            }
            //是否已登录
            var OnLine = OperatorProvider.Provider.IsOnLine();
            if (OnLine == -1)
            {
                return false;
            }
            return true;
        }
    }
}