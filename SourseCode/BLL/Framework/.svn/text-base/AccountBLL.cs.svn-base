using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.DAL;
using CRM.IBLL;
using Common;

namespace CRM.BLL
{
    public class AccountBLL : IAccountBLL
    {

        /// <summary>
        /// 验证用户名和密码是否正确
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>登录成功后的用户信息</returns>
        public SysPerson ValidateUser(string userName, string password)
        {
            if (String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(password))
                return null;

            //获取用户信息,请确定web.config中的连接字符串正确
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                var person = (from p in db.SysPerson
                              where p.Name == userName
                              && p.Password == password
                              && p.State == "开启"
                              && p.PositionStatus == true
                              select p).FirstOrDefault();
                if (person != null)
                {//登录成功
                    person.LastLogonTime = person.LogonTime;
                    person.LastLogonIP = person.LogonIP;
                    person.LogonNum++;
                    person.LogonTime = DateTime.Now;
                    person.LogonIP = Common.IP.GetIP();
                    db.SaveChanges();
                    return person;
                }

            } return null;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="personName">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>修改密码是否成功</returns>
        public bool ChangePassword(string personName, string oldPassword, string newPassword)
        {
            if (!string.IsNullOrWhiteSpace(personName) && !string.IsNullOrWhiteSpace(oldPassword) && !string.IsNullOrWhiteSpace(newPassword))
            {
                try
                {
                    string oldPasswordEncryptString = EncryptAndDecrypte.EncryptString(oldPassword);
                    string newPasswordEncryptString = EncryptAndDecrypte.EncryptString(newPassword);

                    using (DB_CRMEntities db = new DB_CRMEntities())
                    {
                        var person = db.SysPerson.FirstOrDefault(p => (p.Name == personName) && (p.Password == oldPasswordEncryptString));
                        //  var person = db.SysPerson.Where(p => p.Name == personName && p.Password == oldPasswordEncryptString).FirstOrDefault();
                        person.Password = newPasswordEncryptString;
                        person.SurePassword = newPasswordEncryptString;
                        if (!string.IsNullOrWhiteSpace(person.EmailAddress))
                        {
                            NetSendMail.MailSendChangePassword(db, person.EmailAddress, personName, newPassword);
                            //发送通知的邮件
                        }
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionsHander.WriteExceptions(ex);
                }

            }
            return false;
        }
    }
}

