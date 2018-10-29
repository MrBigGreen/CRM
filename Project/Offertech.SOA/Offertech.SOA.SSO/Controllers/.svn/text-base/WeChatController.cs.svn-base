using Offertech.Application.Busines.AuthorizeManage;
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Cache;
using Offertech.Application.Code;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Cache.Factory;
using Offertech.Util;
using Offertech.Util.Attributes;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Offertech.SOA.SSO.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2017.04.12 13:32
    /// 描 述：微信接口
    /// </summary>
    public class WeChatController : ApiControllerBase
    {
        private TrailRecordBLL trailRecordBLL = new TrailRecordBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private FileInfoBLL fileInfoBLL = new FileInfoBLL();


        //string _AppId = Util.Config.GetValue("CorpId"); //企业ID
        //string _Secret = Util.Config.GetValue("CorpSecret");//管理员组ID

        //public string GetToken()
        //{
        //    if (!Senparc.Weixin.QY.Containers.AccessTokenContainer.CheckRegistered(_AppId))
        //    {
        //        Senparc.Weixin.QY.Containers.AccessTokenContainer.Register(_AppId, _Secret);
        //    }
        //    return Senparc.Weixin.QY.Containers.AccessTokenContainer.GetToken(_AppId, _Secret);
        //}
        //[HttpGet]


        //public HttpResponseMessage Test()
        //{
        //    var code = Senparc.Weixin.QY.AdvancedAPIs.OAuth2Api.GetCode(_AppId, "http://crm.9191offer.com/wechat.html".UrlEncode(), "state");
        //    var userInfo = Senparc.Weixin.QY.AdvancedAPIs.OAuth2Api.GetUserId(GetToken(), code);

        //    return Success("成功-Test", userInfo);
        //    //return Success("成功");
        //}

        ///// <summary>
        ///// 获取用户的OpenId
        ///// </summary>
        ///// <returns></returns>
        //public HttpResponseMessage GetAuthUrl(string returnUrl)
        //{
        //    var state = "JeffreySu-" + DateTime.Now.Millisecond;//随机数，用于识别请求可靠性

        //    var url = OAuthApi.GetAuthorizeUrl(_AppId, returnUrl, state, OAuthScope.snsapi_userinfo);

        //    return Success(url);
        //}
        ///// <summary>
        ///// 公众号
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="state"></param>
        ///// <returns></returns>
        //public HttpResponseMessage GetMPUserInfo(string code, string state)
        //{
        //    if (string.IsNullOrEmpty(code))
        //    {
        //        return Error("您拒绝了授权！");
        //    }

        //    if (!state.Contains("-"))
        //    {
        //        //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，
        //        //建议用完之后就清空，将其一次性使用
        //        //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
        //        return Error("验证失败！请从正规途径进入！");
        //    }

        //    OAuthAccessTokenResult result = null;

        //    //通过，用code换取access_token
        //    try
        //    {
        //        result = OAuthApi.GetAccessToken(_AppId, _Secret, code);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Error(ex.Message);
        //    }
        //    if (result.errcode != ReturnCode.请求成功)
        //    {
        //        return Error("错误：" + result.errmsg);
        //    }
        //    //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
        //    //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
        //    //Session["OAuthAccessTokenStartTime"] = DateTime.Now;
        //    //Session["OAuthAccessToken"] = result;

        //    //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
        //    try
        //    {

        //        OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
        //        return Success("通过", userInfo);
        //    }
        //    catch (ErrorJsonResultException ex)
        //    {
        //        return Error(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="redirectUrl">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        ///// <param name="state">重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值</param>
        ///// <param name="responseType"></param>
        ///// <param name="scope"></param>
        ///// <returns></returns>
        //public HttpResponseMessage GetQYUserInfo(string redirectUrl, string state)
        //{
        //    var code = Senparc.Weixin.QY.AdvancedAPIs.OAuth2Api.GetCode(_AppId, redirectUrl.UrlEncode(), state);
        //    var accessToken = Senparc.Weixin.QY.Containers.AccessTokenContainer.GetToken(_AppId, _Secret);
        //    var userInfo = Senparc.Weixin.QY.AdvancedAPIs.OAuth2Api.GetUserId(accessToken, code);

        //    return Success("成功-GetQYUserInfo", userInfo);
        //}




        public string HelloWorld()
        {
            return "this is WeChatController";
        }

        #region 用户信息


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="ticket">票据</param>
        /// <returns></returns>
        private UserEntity GetCurrent(string ticket)
        {
            return CacheFactory.Cache().GetCache<UserEntity>(ticket);
        }

        private void AddCurrent(UserEntity userEntity)
        {

            Operator operators = new Operator();
            LoginUserModel loginUser = new LoginUserModel();
            loginUser.UserId = userEntity.UserId;
            loginUser.Code = userEntity.EnCode;
            loginUser.Account = userEntity.Account;
            loginUser.UserName = userEntity.RealName;
            loginUser.Password = userEntity.Password;
            loginUser.Secretkey = userEntity.Secretkey;
            loginUser.CompanyId = userEntity.OrganizeId;
            loginUser.DepartmentId = userEntity.DepartmentId;
            loginUser.ManagerId = userEntity.ManagerId;
            loginUser.Manager = userEntity.Manager;
            loginUser.HeadIcon = userEntity.HeadIcon;
            loginUser.IPAddress = Net.Ip;
            loginUser.IPAddressName = IPLocation.GetLocation(Net.Ip);
            loginUser.ObjectId = new PermissionBLL().GetObjectStr(userEntity.UserId);
            loginUser.LogTime = DateTime.Now;
            loginUser.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
            //判断是否系统管理员
            if (userEntity.Account == "System")
            {
                loginUser.IsSystem = true;
            }
            else
            {
                loginUser.IsSystem = false;
            }
            operators.LoginInfo = loginUser;
            OperatorProvider.Provider.AddCurrent(operators);
        }
        #endregion

        #region 登录验证
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="system">系统</param>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Login(string openId, string account, string password)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = account;
            logEntity.OperateUserId = account;
            logEntity.Module = "WeChat";

            try
            {
                //验证账户
                UserEntity userEntity = new UserBLL().CheckLogin(openId, account, password);

                //生成票据
                var ticket = Guid.NewGuid().ToString();
                //写入票据
                CacheFactory.Cache().WriteCache(userEntity, ticket, DateTime.Now.AddHours(8));

                //写入日志
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                return Success("登录成功", ticket);
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="system">系统</param>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckLogin(string openId)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = openId;
            logEntity.OperateUserId = openId;
            logEntity.Module = "WeChat";

            try
            {
                //验证账户
                UserEntity userEntity = new UserBLL().WechatLogin(openId);
                //生成票据
                var ticket = Guid.NewGuid().ToString();
                //写入票据
                CacheFactory.Cache().WriteCache(userEntity, ticket, DateTime.Now.AddHours(8));

                //写入日志
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                return Success("登录成功", ticket);
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 票据验证
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckTicket(string ticket)
        {
            UserEntity userEntity = CacheFactory.Cache().GetCache<UserEntity>(ticket);
            if (userEntity != null)
            {
                return Success("通过", userEntity);
            }
            else
            {
                return Error("票据验证失败");
            }
        }
        #endregion

        #region 任务计划

        /// <summary>
        /// 移动端首页代办任务
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAppPlanData(string ticket)
        {

            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {
                Pagination pagination = new Pagination();
                pagination.page = 1;
                pagination.rows = 6;
                pagination.sidx = "StartTime";
                pagination.sord = "asc";

                var watch = CommonHelper.TimerStart();
                var data = trailRecordBLL.GetAppPlanData(userEntity.UserId, pagination);
                var jsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };
                return Success("通过", jsonData);
            }
            else
            {
                return Error("票据验证失败");
            }

        }

        #endregion

        #region 客户管理
        private CustomerBLL customerBLL = new CustomerBLL();
        private CustomerContactBLL customercontactbll = new CustomerContactBLL();
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public HttpResponseMessage GetMyPageListJson(string ticket, Pagination pagination, string queryJson)
        {

            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {
                var watch = CommonHelper.TimerStart();
                var data = customerBLL.GetAppMyPageData(userEntity.UserId, pagination, queryJson);
                var jsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };
                return Success("通过", jsonData);

            }
            else
            {
                return Error("票据验证失败");
            }
        }

        /// <summary>
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public HttpResponseMessage GetCustomerEntityJson(string ticket, string keyValue)
        {

            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {
                var data = customerBLL.GetEntity(keyValue);
                return Success("通过", data);

            }
            else
            {
                return Error("票据验证失败");
            }
        }

        /// <summary>
        /// 获取客户下的联系人
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="customerId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCustomerContactJson(string ticket, string customerId, string keyword)
        {

            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {
                var watch = CommonHelper.TimerStart();
                var data = customercontactbll.GetListByCustomerId(customerId, keyword);
                var jsonData = new
                {
                    rows = data,
                    costtime = CommonHelper.TimerEnd(watch)
                };
                return Success("通过", jsonData);

            }
            else
            {
                return Error("票据验证失败");
            }
        }

        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveCustomer(string ticket, string keyValue, CustomerEntity entity)
        {
            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {

                try
                {

                    #region 新增客户
                    if (string.IsNullOrWhiteSpace(keyValue))
                    {
                        string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//客户管理模块
                        entity.CreateUserId = userEntity.UserId;
                        entity.CreateUserName = userEntity.RealName;

                        entity.TraceUserId = userEntity.UserId;
                        entity.TraceUserName = userEntity.RealName;
                        entity.EnabledMark = 1;

                        #region 计算企业信用等级

                        GetCalcRatingBefore(entity);

                        #endregion
                        customerBLL.AppInsert(entity, moduleId);
                    }
                    #endregion

                    #region 更新客户信息
                    else
                    {
                        entity.CustomerId = keyValue;
                        entity.ModifyUserId = userEntity.UserId;
                        entity.ModifyUserName = userEntity.RealName;
                        entity.ModifyDate = DateTime.Now;
                        customerBLL.AppUpdate(entity);
                    }

                    #endregion

                    return Success("成功");
                }
                catch (Exception ex)
                {
                    return Error(ex.Message);
                }
            }
            else
            {
                return Error("票据验证失败");
            }
        }
        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveCustomerContact(string ticket, string keyValue, CustomerContactEntity entity)
        {
            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {
                try
                {
                    #region 新增客户
                    if (string.IsNullOrWhiteSpace(keyValue))
                    {
                        entity.CustomerContactId = Guid.NewGuid().ToString();
                        entity.EnabledMark = 1;
                        entity.CreateUserId = userEntity.UserId;
                        entity.CreateUserName = userEntity.RealName;
                        entity.CreateDate = DateTime.Now;
                        customercontactbll.AppSaveForm(keyValue, entity);
                    }
                    #endregion

                    #region 更新客户信息
                    else
                    {
                        entity.CustomerId = keyValue;
                        entity.ModifyUserId = userEntity.UserId;
                        entity.ModifyUserName = userEntity.RealName;
                        entity.ModifyDate = DateTime.Now;
                        customercontactbll.AppSaveForm(keyValue, entity);
                    }

                    #endregion

                    return Success("成功");
                }
                catch (Exception ex)
                {
                    return Error(ex.Message);
                }
            }
            else
            {
                return Error("票据验证失败");
            }
        }

        #region 计算企业信用等级

        /// <summary>
        /// 计算企业信用等级
        /// </summary>
        /// <param name="entity">客户实体</param>
        private void GetCalcRatingBefore(CustomerEntity entity)
        {
            #region 计算企业信用等级
            if (string.IsNullOrWhiteSpace(entity.RatingScore))
            {
                int sumScore = 0;
                int score = 0;

                if (int.TryParse(dataItemCache.GetDataItemValue(entity.NatureCode), out score))
                {//企业性质
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.RegisterCapital), out score))
                {//注册资金
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.SalesRevenue), out score))
                {//销售收入
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.CustIndustryId), out score))
                {//所属行业
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.CompanySize), out score))
                {//公司规模
                    sumScore += score;
                }
                entity.RatingScore = customerBLL.GetCalcRatingBefore(sumScore);
            }
            #endregion
        }

        #endregion

        #endregion

        #region 跟进
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public dynamic GetListJson(string objectId, Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = trailRecordBLL.GetPicPageList(objectId, pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }


        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveTrailRecord(string ticket, string keyValue, TrailRecordModel model)
        {
            UserEntity userEntity = GetCurrent(ticket);
            if (userEntity != null)
            {

                try
                {
                    TrailRecordEntity entity = new TrailRecordEntity();
                    entity.Contact = model.Contact;
                    entity.StartTime = model.StartTime;
                    entity.EndTime = model.EndTime;
                    entity.FollowUpMode = model.FollowUpMode;
                    entity.ObjectId = model.ObjectId;
                    entity.ObjectName = model.ObjectName;
                    entity.ObjectSort = model.ObjectSort;
                    entity.SaleStageName = model.SaleStageName;
                    entity.SaleStageId = model.SaleStageId;
                    entity.TrackContent = model.TrackContent;
                    entity.Description = model.Description;
                    entity.TrailType = model.TrailType;//跟进类型

                    #region 新增客户
                    if (string.IsNullOrWhiteSpace(keyValue))
                    {

                        entity.TrailId = Guid.NewGuid().ToString();
                        entity.EnabledMark = 1;
                        entity.DeleteMark = 0;
                        entity.CreateDate = DateTime.Now;
                        entity.CreateUserId = userEntity.UserId;
                        entity.CreateUserName = userEntity.RealName;
                        entity.ModifyDate = DateTime.Now;

                        if (model.ObjectSort == 2)
                        {
                            entity.ObjectName = customerBLL.GetEntity(model.ObjectId).FullName;
                        }
                    }
                    #endregion

                    #region 更新客户信息
                    else
                    {
                        entity.TrailId = keyValue;
                        entity.ModifyDate = DateTime.Now;
                        entity.ModifyUserId = userEntity.UserId;
                        entity.ModifyUserName = userEntity.RealName;
                    }

                    #endregion

                    trailRecordBLL.AppSaveForm(keyValue, entity);
                    if (!string.IsNullOrWhiteSpace(model.FilesPath))
                    {
                        //相关文件
                        string[] files = model.FilesPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in files)
                        {
                            var fileInfoEntity = fileInfoBLL.GetEntity(item);
                            if (fileInfoEntity != null)
                            {
                                fileInfoEntity.ObjectId = entity.TrailId;
                                fileInfoBLL.SaveForm(fileInfoEntity.FileId, fileInfoEntity);
                            }
                        }
                    }
                    return Success("成功");
                }
                catch (Exception ex)
                {
                    return Error(ex.Message);
                }
            }
            else
            {
                return Error("票据验证失败");
            }
        }




        #endregion



    }
}