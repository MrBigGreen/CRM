using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Common;
using System.IO;
using System.Text;
using CRM.DAL;
using CRM.BLL;
 
namespace Models
{
    /// <summary>
    /// 是否记录日志
    /// </summary>
    public enum LogOpration
    {
        /// <summary>
        /// 根据Web.config中的配置
        /// </summary>
        Default,
        /// <summary>
        /// 开启记录日志
        /// </summary>
        Start,
        /// <summary>
        /// 禁止记录日志
        /// </summary>
        Fobid
    }
     
    public class LogClassModels : System.Web.SessionState.IRequiresSessionState
    { 
       
        public static void WriteServiceLog(string message, string logType, LogOpration logOpration = LogOpration.Default)
        {
            try
            {
                 //logOpration设置优先级高于配置节logEnabled
                bool logEnabled = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["LogEnabled"]);
                if (logOpration == LogOpration.Fobid || (logOpration == LogOpration.Default && !logEnabled))
                {
                    return;
                }
                else if (logOpration == LogOpration.Start || (logOpration == LogOpration.Default && logEnabled))
                {
                    //此处实现日志的记录
                    Account  account = AccountModel.GetCurrentAccount();
                    SysLog sysLog = new SysLog();
                    sysLog.Id = Result.GetNewId();
                    sysLog.CreateTime = DateTime.Now;
                    sysLog.Ip = GetIP();
                    sysLog.Message = message;
                    sysLog.PersonId = account.Id;
                    sysLog.CreatePerson = account.PersonName;
                    sysLog.MenuId = logType;//哪个模块生成的日志

                    using (var sysLogRepository = new SysLogBLL())
                    {
                        ValidationErrors validationErrors = new ValidationErrors();
                        sysLogRepository.Create(ref validationErrors, sysLog);
                        return;
                    }
                }
            }
            catch (Exception ep)
            {
                try
                {
                    string path = @"mylog.txt";
                    string txtPath = System.Web.HttpContext.Current.Server.MapPath(path);//获取绝对路径
                    using (StreamWriter sw = new StreamWriter(txtPath, true, Encoding.Default))
                    {
                        sw.WriteLine((ep.Message + "|" + message + "|" + GetIP() + DateTime.Now.ToString()).ToString());
                        sw.Close();
                    }
                    return;
                }
                catch { return; }
            }

        }

        /// <summary>
        /// 将异常信息写入数据库，或者文本文件
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteExceptions(Exception ex)
        {

            SysException sysException = new SysException();
            sysException.CreateTime = DateTime.Now;
            sysException.Remark = ex.StackTrace;
            sysException.Message = ex.Message;
            sysException.LeiXing = "异常";
            sysException.CreatePerson = AccountModel.GetCurrentPerson();
            //sysException.Message = (ex.InnerException == null) ? string.Empty : ex.InnerException.Message;
            sysException.Id = Result.GetNewId();

            using (SysExceptionBLL sysExceptionRepository = new SysExceptionBLL())
            {
                ValidationErrors validationErrors = new ValidationErrors();
                sysExceptionRepository.Create(ref validationErrors, sysException);
                return;
            }

        }


        /// <summary>
        /// 获取ip地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string ip = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current != null)
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
                    {
                        //得到真实的客户端地址
                        ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
                    }
                    else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
                    {

                        //得到服务端的地址要判断  System.Web.HttpContext.Current 为空的情况

                        ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.

                    }
                }
            }
            catch (Exception ep)
            {
                ip = "没有正常获取IP，" + ep.Message;
            }

            return ip;
        }

        public void Dispose()
        {

        }
    }
}
