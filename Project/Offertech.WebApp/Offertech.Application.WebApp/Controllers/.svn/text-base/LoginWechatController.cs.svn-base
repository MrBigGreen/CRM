using Offertech.Application.Busines;
using Offertech.Application.Busines.AuthorizeManage;
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Code;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Util;
using Offertech.Util.Attributes;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.WebApp.Controllers
{
    [HandlerLogin(LoginMode.Ignore)]
    public class LoginWechatController : WechatControllerBase
    {
        private AccountBLL accountBLL = new AccountBLL();
        [HandlerLogin(LoginMode.Ignore)]
        public ActionResult Index()
        {
            return View();
        }

        #region 提交数据
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult OutLogin()
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Exit).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Exit);
            logEntity.OperateAccount = OperatorProvider.Provider.Current().LoginInfo.Account;
            logEntity.OperateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "退出系统";
            logEntity.Module = Config.GetValue("SoftName");
            logEntity.WriteLog();
            Session.Abandon();                                          //清除当前会话
            Session.Clear();                                            //清除当前浏览器所有Session
            WebHelper.RemoveCookie("learn_autologin");                  //清除自动登录
            return Content(new AjaxResult { type = ResultType.success, message = "退出系统" }.ToJson());
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <param name="autologin">下次自动登录</param>
        /// <returns></returns>
        //[HttpPost]
        //[HttpGet]
        //[AjaxOnly]
        public ActionResult CheckLogin(string username, string password, string verifycode, int autologin)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = username;
            logEntity.OperateUserId = username;
            logEntity.Module = Config.GetValue("SoftName");

            try
            {
                #region 验证码验证
                if (autologin == 0)
                {
                    verifycode = Md5Helper.MD5(verifycode.ToLower(), 16);
                    if (Session["session_verifycode"].IsEmpty() || verifycode != Session["session_verifycode"].ToString())
                    {
                        throw new Exception("验证码错误，请重新输入");
                    }
                }
                #endregion

                #region 内部账户验证

                UserEntity userEntity;
                Logger.Info("准备登录");
                if (Session["WxUserID"].IsEmpty())
                {
                    Logger.Info("普通登录");
                    userEntity = new UserBLL().CheckLogin(username, password);
                }
                else
                {
                    string WxUserID = Session["WxUserID"].ToString();
                    Logger.Info("微信登录" + WxUserID);
                    userEntity = new UserBLL().CheckLogin(WxUserID, username, password);
                }
                if (userEntity != null)
                {
                    Logger.Info("登录成功1234567");

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
                    LoginUser.ObjectId = new PermissionBLL().GetObjectStr(userEntity.UserId);
                    LoginUser.LogTime = DateTime.Now;
                    LoginUser.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    //判断是否系统管理员
                    if (userEntity.Account == "System")
                    {
                        LoginUser.IsSystem = true;
                    }
                    else
                    {
                        LoginUser.IsSystem = false;
                    }
                    operators.LoginInfo = LoginUser;

                    //写入当前用户数据权限
                    AuthorizeDataModel dataAuthorize = new AuthorizeDataModel();
                    dataAuthorize.ReadAutorize = authorizeBLL.GetDataAuthor(operators);
                    dataAuthorize.ReadAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators);
                    dataAuthorize.WriteAutorize = authorizeBLL.GetDataAuthor(operators, true);
                    dataAuthorize.WriteAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators, true);
                    operators.DataAuthorize = dataAuthorize;
                    OperatorProvider.AppUserId = userEntity.UserId;
                    OperatorProvider.Provider.AddCurrent(operators);
                    //登录限制
                    LoginLimit(username, operators.LoginInfo.IPAddress, operators.LoginInfo.IPAddressName);
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "登录成功";
                    logEntity.WriteLog();
                }
                return Success("登录成功。");
                #endregion
            }
            catch (Exception ex)
            {
                WebHelper.RemoveCookie("learn_autologin");                  //清除自动登录
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        #endregion

        #region 登录限制
        /// <summary>
        /// 登录限制
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="iPAddress">IP</param>
        /// <param name="iPAddressName">IP所在城市</param>
        public void LoginLimit(string account, string iPAddress, string iPAddressName)
        {
            if (account == "System")
            {
                return;
            }
            string platform = Net.Browser;
            accountBLL.LoginLimit(platform, account, iPAddress, iPAddressName);
        }


        #endregion
    }
}