using Offertech.Cache.Factory;
using Offertech.Util;
using System;

namespace Offertech.Application.Code
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.10.10
    /// 描 述：当前操作者回话
    /// </summary>
    public class OperatorProvider : OperatorIProvider
    {
        #region 静态实例
        /// <summary>
        /// 当前提供者
        /// </summary>
        public static OperatorIProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        /// <summary>
        /// 给app调用
        /// </summary>
        public static string AppUserId
        {
            set;
            get;
        }
        #endregion

        /// <summary>
        /// 秘钥
        /// </summary>
        private string LoginUserKey = "Offertech_LoginUserKey_2017_V6.1";
        /// <summary>
        /// 登陆提供者模式:Session、Cookie 
        /// </summary>
        private string LoginProvider = Config.GetValue("LoginProvider");
        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        public virtual void AddCurrent(Operator user)
        {
            try
            {
                if (LoginProvider == "Cookie")
                {
                    WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(user.LoginInfo.ToJson()));
                }
                else if (LoginProvider == "AppClient")
                {
                    CacheFactory.Cache().WriteCache(DESEncrypt.Encrypt(user.LoginInfo.ToJson()), LoginUserKey);
                }
                else
                {
                    WebHelper.WriteSession(LoginUserKey, DESEncrypt.Encrypt(user.LoginInfo.ToJson()));
                }
                //登录
                CacheFactory.Cache().WriteCache(user.LoginInfo.Token, user.LoginInfo.UserId, user.LoginInfo.LogTime.AddHours(12));
                //数据权限保存
                CacheFactory.Cache().WriteCache(user.DataAuthorize, user.LoginInfo.UserId + "DataAuthorize", user.LoginInfo.LogTime.AddHours(12));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public virtual Operator Current()
        {
            try
            {
                Operator user = new Operator();
                user.LoginInfo = new LoginUserModel();
                LoginUserModel userModel = new LoginUserModel();
                if (LoginProvider == "Cookie")
                {
                    userModel = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<LoginUserModel>();
                }
                else if (LoginProvider == "AppClient")
                {
                    userModel = DESEncrypt.Decrypt(CacheFactory.Cache().GetCache<string>(LoginUserKey)).ToObject<LoginUserModel>();
                }
                else
                {
                    userModel = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<LoginUserModel>();
                }
                user.LoginInfo = userModel;
                if (userModel != null)
                {
                    user.DataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(userModel.UserId + "DataAuthorize");
                }


                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除登录信息
        /// </summary>
        public virtual void EmptyCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else if (LoginProvider == "AppClient")
            {
                CacheFactory.Cache().RemoveCache(LoginUserKey);
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOverdue()
        {
            try
            {
                object str = "";
                if (LoginProvider == "Cookie")
                {
                    str = WebHelper.GetCookie(LoginUserKey);
                }
                else if (LoginProvider == "AppClient")
                {
                    str = CacheFactory.Cache().GetCache<object>(LoginUserKey);
                }
                else
                {
                    str = WebHelper.GetSession(LoginUserKey);
                }
                if (str != null && str.ToString() != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public virtual int IsOnLine()
        {
            LoginUserModel user = new LoginUserModel();
            if (LoginProvider == "Cookie")
            {
                user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<LoginUserModel>();
            }
            else if (LoginProvider == "AppClient")
            {
                user = DESEncrypt.Decrypt(CacheFactory.Cache().GetCache<string>(LoginUserKey)).ToObject<LoginUserModel>();
            }
            else
            {
                user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<LoginUserModel>();
            }

            object token = CacheFactory.Cache().GetCache<string>(user.UserId);
            if (token == null)
            {
                return -1;//过期
            }
            if (user.Token == token.ToString())
            {
                return 1;//正常
            }
            else
            {
                return 0;//已登录
            }
        }
    }
}
