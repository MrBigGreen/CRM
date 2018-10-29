using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Linq;
using System.Configuration;
using Common;
namespace CRM.BLL
{
    /// <summary>
    /// 基于system.net.mail发送邮件，支持附件
    /// </summary>
    public class NetSendMail
    {

        delegate void AsyncSendMailDelegate(string toMailAddress, string mailSubject, string body, string replyTo);



        //public static void SendSMTPEMail(string strSmtpServer, int port, string strFrom, string strFromName, string maiFromlAccount, string strFromPass, string strto, string strSubject, string strBody)
        //{
        //    System.Net.Mail.SmtpClient client = new SmtpClient(strSmtpServer);
        //    client.Port = port;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential(maiFromlAccount, strFromPass);
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;

        //    System.Net.Mail.MailMessage message = new MailMessage(maiFromlAccount, strto, strSubject, strBody);
        //    message.BodyEncoding = System.Text.Encoding.UTF8;
        //    message.IsBodyHtml = true;
        //    MailAddress reply = new MailAddress(strFrom, strFromName);
        //    message.ReplyToList.Add(reply);
        //    client.Send(message);
        //}

        /// <summary>
        /// 修改密码发送的邮件通知
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailTo"></param>
        /// <param name="name"></param>
        /// <param name="psw"></param>
        public static void MailSendChangePassword(DB_CRMEntities db, string mailTo, string name, string psw)
        {
            string tempName = "修改密码发送的邮件通知";
            var data = db.SysEmailTemp.Where(w => w.Mail_name == tempName).FirstOrDefault();
            if (data != null)
            {
                SysEmail email = new SysEmail();
                email.CreateTime = DateTime.Now;
                email.Id = Common.Result.GetNewId();
                email.SysMailId = data.Id;
                email.Subject = data.Subject.Replace("{{用户名}}", name).Replace("{{密码}}", psw);
                email.Content = data.Content.Replace("{{用户名}}", name).Replace("{{密码}}", psw);
                email.Mail_type = mailTo;
                email.Reply_email = data.Reply_email;
                db.SysEmail.Add(email);

                SendMail(mailTo, email.Subject, email.Content, email.Reply_email);
                var caller = new AsyncSendMailDelegate(SendMail);
                caller.BeginInvoke(email.Mail_type, email.Subject, email.Content, email.Reply_email, null, null);
            }
        }


        /// <summary>
        /// 发送邮件，并记录到数据库中
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mailAddress"></param>
        /// <param name="mailSubject"></param>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        public static void SendMail(DB_CRMEntities db, string mailAddress, string mailSubject, string body, string replyTo, string CreatePerson)
        {
            try
            {
                SysEmail email = new SysEmail();
                email.CreateTime = DateTime.Now;
                email.Id = Common.Result.GetNewId();
                //email.SysMailId = data.Id;
                email.Subject = mailSubject;
                email.Content = body;
                email.Mail_type = mailAddress;
                email.Reply_email = replyTo;
                email.CreatePerson = CreatePerson;
                db.SysEmail.Add(email);

                var caller = new AsyncSendMailDelegate(SendMail);
                caller.BeginInvoke(mailAddress, mailSubject, body, replyTo, null, null);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(typeof(NetSendMail), ex);

            }
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tempName">邮件模板</param>
        /// <param name="mailAddress">收件地址</param>
        /// <param name="mailSubject">标题</param>
        /// <param name="body">内容</param>
        public static void SendMailTemp(DB_CRMEntities db, string tempName, string mailAddress, string mailSubject, string body)
        {
            try
            {
                var data = db.SysEmailTemp.Where(w => w.Mail_name == tempName).FirstOrDefault();
                SysEmail email = new SysEmail();
                email.CreateTime = DateTime.Now;
                email.Id = Common.Result.GetNewId();
                email.SysMailId = data.Id;
                email.Subject = data.Subject;
                email.Content = data.Content;
                email.Mail_type = mailAddress;
                email.Reply_email = data.Reply_email;
                db.SysEmail.Add(email);

                var caller = new AsyncSendMailDelegate(SendMail);
                caller.BeginInvoke(mailAddress, mailSubject, body, data.Reply_email, null, null);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(typeof(NetSendMail), ex);
            }
        }



        /// <summary>
        /// 直接发送邮件
        /// </summary>
        /// <param name="toMailAddress"></param>
        /// <param name="mailSubject"></param>
        /// <param name="body"></param>
        /// <param name="picResource"></param>
        /// <param name="replyTo">需回复人的邮箱</param>
        public static void SendMail(string toMailAddress, string mailSubject, string body, string replyTo)
        {
            try
            {
                var from = new MailAddress(ConfigurationManager.AppSettings["HostMailAddress"].ToString(),
                    ConfigurationManager.AppSettings["HostMailName"].ToString());
                var mail = new MailMessage();

                var plainTextBody = "如果你邮件客户端不支持HTML格式，或者你切换到“普通文本”视图，将看到此内容";
                mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextBody, null, "text/plain"));

                var htmlBody = AlternateView.CreateAlternateViewFromString(body, null, "text/html");


                #region 显示图片
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("http://([^'\"]+)*([.jpg|.gif|.jpeg|.bmp|.png])");
                System.Text.RegularExpressions.MatchCollection Matches = regex.Matches(body);

                for (int i = 0; i < Matches.Count; i++)
                {
                    try
                    {
                        LinkedResource lrImage = new LinkedResource(Matches[i].Value, "image/gif");
                        lrImage.ContentId = "weblogo" + i.ToString();
                        body = body.Replace(Matches[i].Value, "cid:weblogo" + i.ToString());
                        htmlBody.LinkedResources.Add(lrImage);

                    }
                    catch (Exception ex)
                    {
                        Utility.WriteLog(typeof(NetSendMail), ex);
                    }
                }
                #endregion

                mail.AlternateViews.Add(htmlBody);
                mail.Subject = mailSubject;
                mail.From = from;
                replyTo = replyTo.Trim();
                if (!string.IsNullOrEmpty(replyTo))
                {
                    mail.ReplyToList.Add(replyTo);
                }
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                var address = "";
                var displayName = "";

                if (toMailAddress.IndexOf('<') > 0)
                {
                    displayName = toMailAddress.Substring(0, toMailAddress.IndexOf('<'));
                    address = toMailAddress.Substring(toMailAddress.IndexOf('<') + 1).Replace('>', ' ');
                }
                else
                {
                    displayName = string.Empty;
                    address = toMailAddress.Substring(toMailAddress.IndexOf('<') + 1).Replace('>', ' ');
                }
                mail.To.Add(new MailAddress(address, displayName));

                var client = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["SMTPHost"],
                    Port = 25,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["HostMailUserName"],
                        ConfigurationManager.AppSettings["HostMailPassword"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(typeof(NetSendMail), ex);
            }
        }


    }
}
