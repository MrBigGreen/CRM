using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.FlowManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Application.Code;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.11.21 14:27
    /// 描 述：薪酬流程发起
    /// </summary>
    public class FlowPayController : MvcControllerBase
    {
        private WFRuntimeBLL wfProcessBll = new WFRuntimeBLL();
        private SmsLogBLL smsLogBLL = new SmsLogBLL();

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
        /// 发起流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowProcessForm()
        {
            return View();
        }
        /// <summary>
        /// 发起流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowProcessView()
        {
            return View();
        }
        /// <summary>
        /// 审核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerificationFrom(string processInstanceId)
        {
            return View();
        }

        /// <summary>
        /// 查看已办流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProcessLookForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        

        /// <summary>
        /// 已办流程进度查看，根据当前节点的权限查看表单内容
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="isPermission"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetProcessSchemeEntityByNodeId(string keyValue, string nodeId)
        {
            var data = wfProcessBll.GetProcessSchemeEntityByNodeId(keyValue, nodeId);



            return Content(data.ToJson());
        }


        #endregion

        #region 提交审核

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        [HttpGet]
        public ActionResult SendSecurityCode()
        {
            //UserEntity userEntity = new UserBLL().GetEntity(OperatorProvider.Provider.Current().ManagerId);//上级主管
            UserEntity userEntity = new UserBLL().GetEntity("System");//系统管理员

            if (!ValidateUtil.IsValidMobile(userEntity.Mobile))
            {
                throw new Exception("手机格式不正确,请输入正确格式的手机号码。");
            }
            var sms = smsLogBLL.GetLatestCurrent(userEntity.Mobile, 300);
            if (sms != null)
            {
                throw new Exception("验证码已发送，请与您上级主管联系");
            }
            string SecurityCode = CommonHelper.RndNum(6);
            if (!string.IsNullOrEmpty(SecurityCode))
            {
                SmsLogEntity smsLog = new SmsLogEntity();
                smsLog.MobileNumber = userEntity.Mobile;
                smsLog.MsgContent = "验证码 " + SecurityCode + "，社保工资审核，当前审核人员：" + OperatorProvider.Provider.Current().LoginInfo.UserName+ "。/退订短信回TD【博尔捷人才】"; 
                smsLog.MsgValue = SecurityCode;
                // smsLogBLL.SaveBySend(smsLog);
                string url = "http://dxjk.51lanz.com/LANZGateway/DirectSendSMSs.asp";
                string SMSparameters = @"UserID=998695&Account=szbridgehr&Password=503ADDD9B727F6F9F6A37F7469A42CC1593F2B43&Content=" + System.Web.HttpUtility.UrlEncode(smsLog.MsgContent, Encoding.GetEncoding("GB2312")) + "&Phones=" + userEntity.Mobile;

                string targeturl = url.Trim().ToString();
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                string res = HttpSMSPost(hr, SMSparameters);
                Response.Write(res);
                smsLogBLL.SaveSmsLog(smsLog);
            }
            return Success("发送成功。");
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

        #endregion
    }
}
