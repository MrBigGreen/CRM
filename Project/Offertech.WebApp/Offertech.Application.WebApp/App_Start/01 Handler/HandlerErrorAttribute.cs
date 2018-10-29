using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Code;
using Offertech.Application.Entity.SystemManage;
using Offertech.Util;
using Offertech.Util.Attributes;
using Offertech.Util.Extension;
using Offertech.Util.Log;
using Offertech.Util.WebControl;
using System;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.WebApp
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.11.9 10:45
    /// 描 述：错误日志（Controller发生异常时会执行这里） 
    /// </summary>
    public class HandlerErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 控制器方法中出现异常，会调用该方法捕获异常
        /// </summary>
        /// <param name="context">提供使用</param>
        public override void OnException(ExceptionContext context)
        {
            WriteLog(context);
            base.OnException(context);
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new ContentResult { Content = new AjaxResult { type = ResultType.error, message = context.Exception.Message }.ToJson() };
        }
        /// <summary>
        /// 写入日志（log4net）
        /// </summary>
        /// <param name="context">提供使用</param>
        private void WriteLog(ExceptionContext context)
        {
            if (context == null)
                return;
            if (OperatorProvider.Provider.IsOverdue())
                return;
            var log = LogFactory.GetLogger(context.Controller.ToString());
            Exception Error = context.Exception;
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            logMessage.Url = HttpContext.Current.Request.RawUrl;
            logMessage.Class = context.Controller.ToString();
            logMessage.Ip = Net.Ip;
            logMessage.Host = Net.Host;
            logMessage.Browser = Net.Browser;
            logMessage.UserName = OperatorProvider.Provider.Current().LoginInfo.Account + "（" + OperatorProvider.Provider.Current().LoginInfo.UserName + "）";
            if (Error.InnerException == null)
            {
                logMessage.ExceptionInfo = Error.Message;
            }
            else
            {
                logMessage.ExceptionInfo = Error.InnerException.Message;
            }
            //logMessage.ExceptionSource = Error.Source;
            //logMessage.ExceptionRemark = Error.StackTrace;
            string strMessage = new LogFormat().ExceptionFormat(logMessage);
            log.Error(strMessage);

            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 4;
            logEntity.OperateTypeId = ((int)OperationType.Exception).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Exception);
            logEntity.OperateAccount = logMessage.UserName;
            logEntity.OperateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            logEntity.ExecuteResult = -1;
            logEntity.ExecuteResultJson = strMessage;
            logEntity.WriteLog();
            SendMail(strMessage);

        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        private void SendMail(string body)
        {
            bool ErrorToMail = Config.GetValue("ErrorToMail").ToBool();
            if (ErrorToMail == true)
            {
                string SystemName = Config.GetValue("SystemName");//系统名称
                MailHelper.Send("receivebug@Offertech.cn", SystemName + " - 发生异常", body.Replace("-", ""));
            }
        }
    }
}