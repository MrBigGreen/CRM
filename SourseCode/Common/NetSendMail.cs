﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common
{
    /// <summary>
    /// 基于system.net.mail发送邮件，支持附件
    /// </summary>
    public class NetSendMail
    {
        public static void MailSend(string mailFrom, string maiFromlAccount, string mailFromPwd, string mailSmtpServer, IList<string> mailTo, IList<string> mailCC, IList<string> mailBCC, string mailTitle, string mailContent, IList<string> mailAttachments, System.Text.Encoding encoding, bool isBodyHtml)
        {
            MailMessage message = new MailMessage();
            if (mailFrom.Trim() == "")
            {
                throw new Exception("发送邮件不可以为空");
            }
            message.From = new MailAddress(mailFrom);
            if (mailTo.Count <= 0)
            {
                throw new Exception("接收邮件不可以为空");
            }
            foreach (string s in mailTo)
            {
                message.To.Add(new MailAddress(s));
            }
            if (mailCC.Count > 0)
            {
                foreach (string s in mailCC)
                {
                    message.CC.Add(new MailAddress(s));
                }
            }
            if (mailBCC.Count > 0)
            {
                foreach (string s in mailBCC)
                {
                    message.Bcc.Add(new MailAddress(s));
                }
            }
            message.Subject = mailTitle;
            message.Body = mailContent;
            message.BodyEncoding = encoding;   //邮件编码
            message.IsBodyHtml = isBodyHtml;      //内容格式是否是html
            message.Priority = MailPriority.High;  //设置发送的优先集
            //附件
            foreach (string att in mailAttachments)
            {
                message.Attachments.Add(new Attachment(att));
            }
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = mailSmtpServer;
            smtpClient.Credentials = new NetworkCredential(maiFromlAccount, mailFromPwd);
            smtpClient.Timeout = 1000;
            smtpClient.EnableSsl = false;        //不使用ssl连接
            smtpClient.Send(message);
        }

        public static void MailSendText(string mailFrom, string maiFromlAccount, string mailFromPwd, string mailSmtpServer, IList<string> mailTo, IList<string> mailCC, IList<string> mailBCC, string mailTitle, string mailContent)
        {
            List<string> attList = new List<string>();
            MailSend(mailFrom, maiFromlAccount, mailFromPwd, mailSmtpServer, mailTo, mailCC, mailBCC, mailTitle, mailContent, attList, Encoding.UTF8, false);
        }

        public static void MailSendHTML(string mailTo, string mailTitle, string mailContent)
        {
            string mailFrom = "ben@Offer.com",
              maiFromlAccount = "service@Offer.com",
              mailFromPwd = "Offer123",
              mailSmtpServer = "smtp.exmail.qq.com";
            List<string> attList = new List<string>();
            List<string> mailToList = new List<string>() { mailTo }; IList<string> mailCC = new List<string>(); IList<string> mailBCC = new List<string>();
            MailSend(mailFrom, maiFromlAccount, mailFromPwd, mailSmtpServer, mailToList, mailCC, mailBCC, mailTitle, mailContent, attList, Encoding.UTF8, true);
        }
    }
}
