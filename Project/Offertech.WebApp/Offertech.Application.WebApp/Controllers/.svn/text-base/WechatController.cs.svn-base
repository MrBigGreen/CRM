using Offertech.Application.Busines;
using Offertech.Application.Busines.AuthorizeManage;
using Offertech.Application.Busines.BaseManage;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Application.Busines.ReportManage;
using Offertech.Application.Busines.SystemManage;
using Offertech.Application.Cache;
using Offertech.Application.Code;
using Offertech.Application.Entity;
using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.Entity.SystemManage;
using Offertech.Util;
using Offertech.Util.Attributes;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offertech.Application.WebApp.Controllers
{
    public class WechatController : WechatControllerBase
    {
        private AccountBLL accountBLL = new AccountBLL();
        private CustomerBLL customerBLL = new CustomerBLL();
        private CustomerContactBLL customercontactbll = new CustomerContactBLL();
        private TrailRecordBLL trailRecordBLL = new TrailRecordBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private FileInfoBLL fileInfoBLL = new FileInfoBLL();
        private ContractBLL contractbll = new ContractBLL();
        private AreaCache areaCache = new AreaCache();

        #region 视图功能
        // GET: Wechat
        public ActionResult Index()
        {
            return View();
        }
        [HandlerLogin(LoginMode.Ignore)]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 客户详情
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult CustomerDetail()
        {
            return View();
        }

        /// <summary>
        /// 微信首页
        /// </summary>
        /// <returns></returns>

        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// 我的客户
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult MyCustomers()
        {
            return View();
        }

        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult ProcessHistory()
        {
            return View();
        }

        /// <summary>
        /// 跟进计划
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult RecordAndPlan()
        {
            return View();
        }

        /// <summary>
        /// 首页-跟进计划
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult Mytask()
        {
            return View();
        }

        /// <summary>
        /// 计划详情
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult TaskDetail()
        {
            return View();
        }

        /// <summary>
        ///联系人管理
        /// </summary>
        /// <returns></returns>
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult ContractManager()
        {
            return View();
        }
        // GET: Wechat
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult ContractAdd()
        {
            return View();
        }
        // GET: Wechat
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult CustomerHistory()
        {
            return View();
        }
        // GET: Wechat
        [HandlerLogin(LoginMode.Enforce)]
        public ActionResult EditContacter()
        {
            return View();
        }

        #endregion

        #region 登录
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
        //[HttpPost]
        //[HttpGet]
        //[AjaxOnly]
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

                #region 内部账户验证

                UserEntity userEntity;
                Logger.Info("准备登录");
                if (Session["WxUserID"].IsEmpty())
                {
                    Logger.Info("普通登录");
                    userEntity = new UserBLL().CheckLogin(username, password);
                }
                else
                {
                    string WxUserID = Session["WxUserID"].ToString();
                    Logger.Info("微信登录" + WxUserID);
                    userEntity = new UserBLL().CheckLogin(WxUserID, username, password);
                }
                if (userEntity != null)
                {
                    Logger.Info("登录成功1234567");

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
                    OperatorProvider.AppUserId = userEntity.UserId;
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

        #region 登录限制
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

        #region 任务计划

        /// <summary>
        /// 移动端首页代办任务
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPlanData()
        {
            Pagination pagination = new Pagination();
            pagination.page = 1;
            pagination.rows = 6;
            pagination.sidx = "StartTime";
            pagination.sord = "asc";

            var watch = CommonHelper.TimerStart();
            var data = trailRecordBLL.GetPlanData(pagination, "");
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
        /// <summary>
        /// 代办任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllPlanData(Pagination pagination)
        {

            var watch = CommonHelper.TimerStart();
            var data = trailRecordBLL.GetPlanData(pagination, "");
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
        #endregion

        #region 客户管理

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetMyPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = customerBLL.GetMyPageData(pagination, queryJson);
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

        /// <summary>
        /// 选择使用的客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetCustomerSelectedListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            DataTable dataTable = null;
            if (OperatorProvider.Provider.Current().LoginInfo.Account.Equals("offercom", StringComparison.CurrentCultureIgnoreCase))
            {
                dataTable = customerBLL.GetPageData(pagination, queryJson);
            }
            else
            {
                dataTable = customerBLL.GetMyPageData(pagination, queryJson);
            }

            var jsonData = new
            {
                rows = dataTable,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetCustomerEntityJson(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                var data = customerBLL.GetEntity(keyValue);
                return Success("通过", new
                {
                    data.CustomerId,
                    data.EnCode,
                    data.FullName,
                    data.ShortName,
                    data.CustIndustryId,
                    CustIndustryName = dataItemCache.GetDataItemValue(data.CustIndustryId),
                    data.CustTypeId,
                    CustTypeName = dataItemCache.GetDataItemValue(data.CustTypeId),
                    data.CustLevelId,
                    CustLevelName = dataItemCache.GetDataItemValue(data.CustLevelId),
                    data.CustDegreeId,
                    CustDegreeName = dataItemCache.GetDataItemValue(data.CustDegreeId),
                    data.ProvinceId,
                    ProvinceName = areaCache.GetEntity(data.ProvinceId),
                    data.CityId,
                    CityName = areaCache.GetEntity(data.CityId),
                    data.CountyId,
                    CountyName = areaCache.GetEntity(data.CountyId),
                    data.Contact,
                    data.PostId,
                    data.Mobile,
                    data.CompanyDesc,
                    data.CompanyAddress,
                    data.County,
                    data.EstablishDate,
                    data.NatureCode,
                    data.RegisterCapital,
                    data.SalesRevenue,
                    data.CompanySize


                });
            }
            else
            {
                return Success("", new
                {
                    CustomerId = string.Empty,
                    EnCode = string.Empty,
                    FullName = string.Empty,
                    ShortName = string.Empty,
                    CustIndustryId = string.Empty,
                    CustIndustryName = string.Empty,
                    CustTypeId = string.Empty,
                    CustTypeName = string.Empty,
                    CustLevelId = string.Empty,
                    CustLevelName = string.Empty,
                    CustDegreeId = string.Empty,
                    CustDegreeName = string.Empty,
                    ProvinceId = string.Empty,
                    ProvinceName = new AreaEntity
                    {
                        AreaName = string.Empty
                    },
                    CityId = string.Empty,
                    CityName = new AreaEntity
                    {
                        AreaName = string.Empty
                    },
                    CountyId = string.Empty,
                    CountyName = new AreaEntity
                    {
                        AreaName = string.Empty
                    },
                    Contact = string.Empty,
                    PostId = string.Empty,
                    Mobile = string.Empty,
                    CompanyDesc = string.Empty,
                    CompanyAddress = string.Empty,
                    County = string.Empty,
                    EstablishDate = string.Empty,
                    NatureCode = string.Empty,
                    RegisterCapital = string.Empty,
                    SalesRevenue = string.Empty,
                    CompanySize = string.Empty
                });
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
        public ActionResult GetCustomerContactJson(string customerId, string keyword)
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
        /// <summary>
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetContactEntityJson(string keyValue)
        {
            var data = customercontactbll.GetEntity(keyValue);
            return Success("通过", new
            {
                data.CustomerId,
                data.CustomerContactId,
                data.Contact,
                data.Gender,
                data.Mobile,
                data.Tel,
                data.PostId,
                data.QQ,
                data.Email,
                data.Wechat,
                data.Description,
                data.Hobby,

            });

        }
        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCustomer(string keyValue, CustomerEntity entity)
        {
            try
            {

                #region 新增客户
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//客户管理模块

                    #region 计算企业信用等级

                    GetCalcRatingBefore(entity);

                    #endregion
                    customerBLL.SaveForm("", entity, moduleId);
                }
                #endregion

                #region 更新客户信息
                else
                {
                    customerBLL.SaveForm(keyValue, entity);
                }

                #endregion

                return Success("成功");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

        }
        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCustomerContact(string keyValue, CustomerContactEntity entity)
        {
            try
            {
                #region 新增客户
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    customercontactbll.SaveForm(keyValue, entity);
                }
                #endregion

                #region 更新客户信息
                else
                {
                    customercontactbll.SaveForm(keyValue, entity);
                }

                #endregion

                return Success("成功");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
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
        /// 获取跟进记录
        /// </summary>
        /// <param name="objectId">客户编号</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetListJson(string objectId, Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = trailRecordBLL.GetPageList(objectId, pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(jsonData.ToJson("yyyy-MM-dd HH:mm"));
        }

        /// <summary>
        /// 获取跟进历史
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetHistoryDataJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = trailRecordBLL.GetHistoryData(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(jsonData.ToJson("yyyy-MM-dd HH:mm"));
        }
        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveTrailRecord(string keyValue, TrailRecordModel model)
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
                entity.FilesPath = model.FilesPath;
                entity.ObjectSort = 2;
                #region 获取相关对象的名称
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    if (model.ObjectSort == 2)
                    {
                        entity.ObjectName = customerBLL.GetEntity(model.ObjectId).FullName;
                    }
                }
                #endregion


                trailRecordBLL.SaveForm(keyValue, entity);
                //if (!string.IsNullOrWhiteSpace(model.FilesPath))
                //{
                //    //相关文件
                //    string[] files = model.FilesPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                //    foreach (var item in files)
                //    {
                //        //文件信息写入数据库
                //        FileInfoEntity fileInfoEntity = new FileInfoEntity();
                //        fileInfoEntity.ObjectId = entity.TrailId;
                //        fileInfoEntity.FolderId = "-1";
                //        fileInfoEntity.FileName = Path.GetFileName(item); ;
                //        fileInfoEntity.FilePath = item;
                //        fileInfoEntity.FileSize = "99";
                //        fileInfoEntity.FileExtensions = Path.GetExtension(item);
                //        fileInfoEntity.DeleteMark = 1;
                //        fileInfoEntity.FileType = fileInfoEntity.FileExtensions.Replace(".", "");
                //        fileInfoBLL.SaveForm("", fileInfoEntity);

                //    }
                //}
                return Success("成功");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

        }

        /// <summary>
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetTrailRecordEntityJson(string keyValue)
        {
            var data = trailRecordBLL.GetEntity(keyValue);
            return Success("通过", data);
        }
        #endregion

        #region 合同管理



        /// <summary>
        /// 获取欧孚销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetOfferDataBySaleJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetOfferDataBySale(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(data.ToJson("yyyy-MM-dd"));
        }
        /// <summary>
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetBridgeDataBySaleJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetBridgeDataBySale(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(data.ToJson("yyyy-MM-dd"));
        }

        /// <summary>
        /// 保存表单新增
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveContractForm(string keyValue, ContractModel model)
        {
            ContractEntity entity = new ContractEntity();
            entity.ContractId = model.ContractId;
            entity.ContractMoney = model.ContractMoney;
            entity.ContractType = model.ContractType;
            entity.CustomerId = model.CustomerId;
            entity.CustomerName = model.CustomerName;
            entity.Deadline = model.Deadline;
            entity.Description = model.Description;
            entity.EffectiveDate = model.EffectiveDate;
            entity.PaymentMethod = model.PaymentMethod;
            entity.PhoneNumber = model.PhoneNumber;
            entity.ProjectLeader = model.ProjectLeader;
            entity.ServiceType = model.ServiceType.TrimEnd(new char[] { ',', ';' });
            entity.SignSubject = model.SignSubject;
            entity.SignSubjectId = model.SignSubjectId;
            entity.SignType = model.SignType;
            entity.ContractCode = model.ContractCode;


            List<ContractServiceEntity> serviceEntityList = new List<ContractServiceEntity>();
            if (!string.IsNullOrWhiteSpace(model.ServiceTypeId))
            {
                var arrIds = model.ServiceTypeId.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                var arrNames = model.ServiceType.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrIds.Length; i++)
                {
                    ContractServiceEntity serviceEntity = new ContractServiceEntity();
                    serviceEntity.ServiceTypeId = arrIds[i];
                    serviceEntity.ServiceType = arrNames[i];
                    serviceEntity.ContractId = model.ContractId;
                    serviceEntity.Create();
                    serviceEntityList.Add(serviceEntity);
                    //合同编码的前置
                    if (string.IsNullOrWhiteSpace(entity.ContractCode))
                    {
                        var dataItem = dataItemCache.GetDataItem(serviceEntity.ServiceTypeId);
                        if (dataItem != null)
                        {
                            entity.ContractCode = dataItem.ItemValue;
                        }
                    }
                }
            }


            contractbll.AddForm(entity, serviceEntityList);
            return Success("操作成功。");
        }

        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetContractJson(string keyValue)
        {
            var data = contractbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 文件上传

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="FileName">文件名称包含后缀</param>
        /// <param name="Filedata">Base64文件 字符串</param>
        /// <returns></returns>
        public ActionResult UploadifyFile(string FileName, string Filedata)
        {

            try
            {
                string virtualPath = string.Empty;
                //没有文件上传，直接返回
                if (string.IsNullOrEmpty(Filedata))
                {
                    return Error("文件不能空");
                }
                if (!ValidateUtil.IsBase64String(Filedata))
                {
                    return Error("数据有误，不是Base64字符串");
                }

                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string FilePath = Config.GetValue("FilePath");
                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                string fileGuid = Guid.NewGuid().ToString();
                string FileEextension = Path.GetExtension(FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string userPath = string.Format("{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);

                virtualPath = FilePath + userPath;

                string fullFileName = this.Server.MapPath(virtualPath);
                //创建文件夹
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    byte[] bytes = Convert.FromBase64String(Filedata);
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        if (bytes[i] < 0)
                            bytes[i] = (byte)(bytes[i] + 256);
                    }
                    FileStream fs = new FileStream(fullFileName, FileMode.Create, FileAccess.Write);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Close();
                }
                #region 上传到文件服务器
                try
                {
                    FtpClient ftpClient = new FtpClient(Config.GetValue("ImgFtpUrl"), FilePath,
                        Config.GetValue("ImgFtpUserName"), Config.GetValue("ImgFtpPassword"));

                    //查看文件是否存在
                    if (!ftpClient.DirectoryExist(userId))
                        ftpClient.MakeDir(userId);
                    ftpClient.GotoDirectory(userId, false);
                    //查看文件是否存在
                    if (!ftpClient.DirectoryExist(uploadDate))
                        ftpClient.MakeDir(uploadDate);
                    ftpClient.GotoDirectory(uploadDate, false);

                    ftpClient.Upload(fullFileName);

                    System.IO.File.Delete(fullFileName);
                }
                catch (Exception e)
                {

                }
                #endregion
                return Success(virtualPath);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #endregion

        #region
        /// <summary>
        /// 获取新建，新签，跟进数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCrmData()
        {
            var currentDate = DateTime.Now.Date;
            var fristDayOfWeek = GetFirstDayOfWeek(currentDate);
            var fristDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var userName = OperatorProvider.Provider.Current().LoginInfo.UserName;

            var rptTempBll = new RptTempBLL();

            var currentDateData = new RptTempBLL().GetTableToKPI(userName, currentDate.ToString("yyyy-MM-dd"), currentDate.AddDays(1).ToString("yyyy-MM-dd"), 0);

            var currentWeekData = new RptTempBLL().GetTableToKPI(userName, fristDayOfWeek.ToString("yyyy-MM-dd"), currentDate.AddDays(1).ToString("yyyy-MM-dd"), 0);

            var currentMonthData = new RptTempBLL().GetTableToKPI(userName, fristDayOfMonth.ToString("yyyy-MM-dd"), currentDate.AddDays(1).ToString("yyyy-MM-dd"), 0);

            return Success("成功", new
            {
                currentDateNew = currentDateData.Select("Type = '新建'").Count() > 0 ? currentDateData.Select("Type = '新建'")[0]["Counts"] : 0,
                currentDateFollow = currentDateData.Select("Type = '跟进'").Count() > 0 ? currentDateData.Select("Type = '跟进'")[0]["Counts"] : 0,
                currentDateNewSign = currentDateData.Select("Type = '新签'").Count() > 0 ? currentDateData.Select("Type = '新签'")[0]["Counts"] : 0,
                currentWeekNew = currentWeekData.Select("Type = '新建'").Count() > 0 ? currentWeekData.Select("Type = '新建'")[0]["Counts"] : 0,
                currentWeekFollow = currentWeekData.Select("Type = '跟进'").Count() > 0 ? currentWeekData.Select("Type = '跟进'")[0]["Counts"] : 0,
                currentWeekNewSign = currentWeekData.Select("Type = '新签'").Count() > 0 ? currentWeekData.Select("Type = '新签'")[0]["Counts"] : 0,
                currentMonthNew = currentMonthData.Select("Type = '新建'").Count() > 0 ? currentMonthData.Select("Type = '新建'")[0]["Counts"] : 0,
                currentMonthFollow = currentMonthData.Select("Type = '跟进'").Count() > 0 ? currentMonthData.Select("Type = '跟进'")[0]["Counts"] : 0,
                currentMonthNewSign = currentMonthData.Select("Type = '新签'").Count() > 0 ? currentMonthData.Select("Type = '新签'")[0]["Counts"] : 0
            });
        }

        private DateTime GetFirstDayOfWeek(DateTime date)
        {
            //星期一为第一天  
            var weeknow = Convert.ToInt32(date.DayOfWeek);
            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            var daydiff = (-1) * weeknow;

            return date.AddDays(daydiff);
        }
        #endregion
    }
}