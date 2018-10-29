using Offertech.Application.Busines;
using Offertech.Application.Busines.AuthorizeManage;
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Code;
using Offertech.Application.Entity;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Util;
using Offertech.Util.Attributes;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Mvc;

namespace Offertech.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统登录
    /// </summary>
    [HandlerLogin(LoginMode.Ignore)]
    public class LoginController : MvcControllerBase
    {
        private SmsLogBLL smsLogBLL = new SmsLogBLL();

        #region 视图功能
        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 手机登录验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckForm()
        {
            return View();
        }

        #endregion

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
        [HttpPost]
        [AjaxOnly]
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

                #region 第三方账户验证
                AccountEntity accountEntity = accountBLL.CheckLogin(username, password);
                if (accountEntity != null)
                {
                    Operator operators = new Operator();
                    LoginUserModel loginUser = new LoginUserModel();
                    loginUser.UserId = accountEntity.AccountId;
                    loginUser.Code = accountEntity.MobileCode;
                    loginUser.Account = accountEntity.MobileCode;
                    loginUser.UserName = accountEntity.FullName;
                    loginUser.Password = accountEntity.Password;
                    loginUser.IPAddress = Net.Ip;
                    loginUser.IPAddressName = IPLocation.GetLocation(Net.Ip);
                    loginUser.LogTime = DateTime.Now;
                    loginUser.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    loginUser.IsSystem = true;
                    operators.LoginInfo = loginUser;
                    OperatorProvider.Provider.AddCurrent(operators);
                    //登录限制
                    LoginLimit(username, operators.LoginInfo.IPAddress, operators.LoginInfo.IPAddressName);
                    return Success("登录成功。");
                }
                #endregion

                #region 内部账户验证
                UserEntity userEntity = new UserBLL().CheckLogin(username, password);
                if (userEntity != null)
                {


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

        #region 注册账户、登录限制
        private AccountBLL accountBLL = new AccountBLL();
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        [HttpGet]
        public ActionResult GetSecurityCode(string mobileCode)
        {
            if (!ValidateUtil.IsValidMobile(mobileCode))
            {
                throw new Exception("手机格式不正确,请输入正确格式的手机号码。");
            }
            var data = accountBLL.GetSecurityCode(mobileCode);
            if (!string.IsNullOrEmpty(data))
            {
                SmsModel smsModel = new SmsModel();
                smsModel.account = Config.GetValue("SMSAccount");
                smsModel.pswd = Config.GetValue("SMSPswd");
                smsModel.url = Config.GetValue("SMSUrl");
                smsModel.mobile = mobileCode;
                smsModel.msg = Config.GetValue("SMSPrefix") + "验证码 " + data + "，(请确保是本人操作且为本人手机，否则请忽略此短信)";
                SmsHelper.SendSmsByJM(smsModel);
            }
            return Success("获取成功。");
        }
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="mobileCode">手机号</param>
        /// <param name="securityCode">短信验证码</param>
        /// <param name="fullName">姓名</param>
        /// <param name="password">密码（md5）</param>
        /// <param name="verifycode">图片验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string mobileCode, string securityCode, string fullName, string password, string verifycode)
        {
            AccountEntity accountEntity = new AccountEntity();
            accountEntity.MobileCode = mobileCode;
            accountEntity.SecurityCode = securityCode;
            accountEntity.FullName = fullName;
            accountEntity.Password = password;
            accountEntity.IPAddress = Net.Ip;
            accountEntity.IPAddressName = IPLocation.GetLocation(accountEntity.IPAddress);
            accountEntity.AmountCount = 30;
            accountBLL.Register(accountEntity);
            return Success("注册成功。");
        }
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


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        [HttpGet]
        public ActionResult GetSecurityCodeByoffercomLogin(string mobileCode)
        {
            if (!ValidateUtil.IsValidMobile(mobileCode))
            {
                throw new Exception("手机格式不正确,请输入正确格式的手机号码。");
            }
            var sms = smsLogBLL.GetLatestCurrent(mobileCode, 120);
            if (sms != null)
            {
                throw new Exception("验证码已发送，请勿重复获取");
            }
            string SecurityCode = CommonHelper.RndNum(6);
            if (!string.IsNullOrEmpty(SecurityCode))
            {
                SmsLogEntity smsLog = new SmsLogEntity();
                smsLog.MobileNumber = mobileCode;
                smsLog.MsgContent = "验证码 " + SecurityCode + "，客服账号登录，20分钟内验证码有效，切勿将验证码泄露于他人。/退订短信回TD【博尔捷人才】";
                smsLog.MsgValue = SecurityCode;
                //  smsLogBLL.SaveBySend(smsLog);
                string url = "http://dxjk.51lanz.com/LANZGateway/DirectSendSMSs.asp";
                string SMSparameters = @"UserID=998695&Account=szbridgehr&Password=503ADDD9B727F6F9F6A37F7469A42CC1593F2B43&Content=" + System.Web.HttpUtility.UrlEncode(smsLog.MsgContent, Encoding.GetEncoding("GB2312")) + "&Phones=" + mobileCode;

                string targeturl = url.Trim().ToString();
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                string res = HttpSMSPost(hr, SMSparameters);
                Response.Write(res);
                smsLogBLL.SaveSmsLog(smsLog);
            }

            return Success("获取成功。");
        }

        public string HttpSMSPost(HttpWebRequest hr, string parameters)
        {
            string strRet = null;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(parameters);

            try
            {
                hr.KeepAlive = false;
                hr.Method = "POST";
                hr.ContentType = "application/x-www-form-urlencoded";
                hr.ContentLength = data.Length;
                Stream newStream = hr.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                HttpWebResponse myResponse = (HttpWebResponse)hr.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                strRet = reader.ReadToEnd();
                reader.Close();
                myResponse.Close();

            }
            catch (Exception ex)
            {
                strRet = ex.ToString();
            }
            return strRet;
        }

        [HttpPost]
        public ActionResult SaveCheck(string PhoneNumber, string VCode)
        {
            var smsEntity = smsLogBLL.GetLatestNews(PhoneNumber, 60);
            if (smsEntity != null)
            {
                if (smsEntity.MsgValue == VCode)
                {
                    return Success("验证成功。", "1");
                }
            }

            return Error("验证失败！");


        }

    }
}
