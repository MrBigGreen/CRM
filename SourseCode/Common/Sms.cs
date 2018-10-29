using Common.SendSmsService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Sms
    {
        static SDKService sc = new SDKService();
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="Mobile">手机号</param>
        /// <param name="Msg">短信内容</param>
        /// <returns></returns>
        public static int SendMsg(string Mobile, string Msg)
        {
            int Result = sc.sendSMS(ConfigurationManager.AppSettings["SmsSn"], ConfigurationManager.AppSettings["SmsPwd"], string.Empty, Mobile.Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), Msg,
                string.Empty, "GBK", 5, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            Utility.WriteSMSLog(Mobile, Msg, Result);
            return Result;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="Mobile">手机号</param>
        /// <param name="Msg">短信内容</param>
        /// <returns></returns>
        public static int SendMsgs(string[] Mobile, string Msg)
        {
            int Result = sc.sendSMS(ConfigurationManager.AppSettings["SmsSn"], ConfigurationManager.AppSettings["SmsPwd"],
                string.Empty, Mobile, "【欧孚视聘】" + Msg,
                string.Empty, "GBK", 5, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            return Result;
        }
    }
}
