using Common;
using CRM.App.UnCallWebService;
using CRM.BLL;
using CRM.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace CRM.App.Controllers
{

    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-25
    /// 描述说明：客户跟进设置-控制器
    /// </summary>
    [SupportFilter]
    public class CustomerFollowController : BaseController
    {

        #region 初始化

        IBLL.ITBCustomerFollowBLL m_BLL;

        public CustomerFollowController()
            : this(new TBCustomerFollowBLL())
        { }

        public CustomerFollowController(TBCustomerFollowBLL bll)
        {
            m_BLL = bll;
        }

        ValidationErrors validationErrors = new ValidationErrors();



        #endregion
        public UncallAPIPortTypeClient unCallAPI = new UncallAPIPortTypeClient();
        //
        // GET: /CustomerFollow/
        public ActionResult Index(string ID)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerID = ID;
            return View(entity);
        }


        #region 创建根据任务-跟进设置 create by chand 2015-06-25
        [SupportFilter]
        public ActionResult CreateTask(string ID)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            var info = new TBCustomerBLL().GetById(ID);
            if (info != null)
            {
                entity.ProvinceName = info.ProvinceName;
                entity.ProvinceCode = info.ProvinceCode;
                entity.CityCode = info.CityCode;
                entity.CityName = info.CityName;
                entity.DistrictCode = info.DistrictCode;
                entity.DistrictName = info.DistrictName;
                entity.AddressDetails = info.AddressDetails;
                entity.CustomerID = ID;
            }
            entity.ReservationDate = DateTime.Now.AddMinutes(10);
            return View(entity);
        }



        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult CreateTask(TB_CustomerFollow entity)
        {

            if (entity != null && ModelState.IsValid)
            {
                Account account = GetCurrentAccount();



                if (!m_BLL.IsContact(entity.CustomerID, account.Id, account.FollowType))
                {
                    return Json(Suggestion.InsertFail + "，已有跟进未完成"); //提示输入的数据的格式不对 
                }

                entity.FollowType = account.FollowType;
                DateTime date = DateTime.Now;
                entity.CustomerFollowID = Result.GetNewId();
                //entity.FollowUpDate = entity.ReservationDate;
                entity.ContactState = 0;
                entity.IsFinish = false;
                entity.SysPersonID = account.Id;
                entity.VersionNo = 1;
                entity.IsDelete = false;
                entity.CreatedTime = date;
                entity.CreatedBy = account.Id;
                entity.LastUpdatedTime = date;
                entity.LastUpdatedBy = account.Id;


                string returnValue = string.Empty;

                if (m_BLL.Create(ref validationErrors, entity))
                {
                    //写入日志 
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户跟进设置，客户ID：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "客户管理");
                    return Json(Suggestion.InsertSucceed);
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    //写入日志  
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 批量跟进创建 create by chand 2015-07-20
        [SupportFilter]
        public ActionResult BatchCreate(string ID)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerIDs = ID;
            entity.ReservationDate = DateTime.Now.AddMinutes(10);
            return View(entity);
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult BatchCreate(string ID, TB_CustomerFollow entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string returnValue = string.Empty;
                string[] arr = entity.CustomerIDs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    int flag = 0;
                    //string currentPerson = GetCurrentPersonID();
                    Account account = GetCurrentAccount();
                    DateTime date = DateTime.Now;

                    for (int i = 0; i < arr.Length; i++)
                    {
                        var customer = new TBCustomerBLL().GetById(arr[i]);
                        if (customer == null)
                        {
                            continue;
                        }
                        TB_CustomerFollow info = new TB_CustomerFollow();

                        info.FollowType = account.FollowType;
                        info.CustomerFollowID = Result.GetNewId();
                        info.ReservationDate = entity.ReservationDate;
                        info.FollowUpMode = entity.FollowUpMode;
                        info.FollowUpCategory = entity.FollowUpCategory;
                        info.OtherLevel = entity.OtherLevel;
                        info.FollowUpPurpose = entity.FollowUpPurpose;
                        info.ProvinceName = customer.ProvinceName;
                        info.ProvinceCode = customer.ProvinceCode;
                        info.CityCode = customer.CityCode;
                        info.CityName = customer.CityName;
                        info.DistrictCode = customer.DistrictCode;
                        info.DistrictName = customer.DistrictName;
                        info.AddressDetails = customer.AddressDetails;
                        info.CustomerID = customer.CustomerID;
                        info.FollowUpDate = entity.ReservationDate;
                        info.ContactState = 0;
                        info.IsFinish = false;
                        info.SysPersonID = account.Id;
                        info.VersionNo = 1;
                        info.IsDelete = false;
                        info.CreatedTime = date;
                        info.CreatedBy = account.Id;
                        info.LastUpdatedTime = date;
                        info.LastUpdatedBy = account.Id;


                        if (m_BLL.Create(ref validationErrors, info))
                        {
                            flag++;
                            //写入日志 
                            LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户跟进设置(批量)，客户编号：" + info.CustomerID + "，跟进编号：" + info.CustomerFollowID, "客户管理");
                        }
                        else
                        {
                            if (validationErrors != null && validationErrors.Count > 0)
                            {
                                validationErrors.All(a =>
                                {
                                    returnValue += a.ErrorMessage;
                                    return true;
                                });
                            }
                            //写入日志  
                            LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置(批量)错误：" + returnValue, "客户管理");
                        }
                    }
                    if (flag == arr.Length)
                    {
                        return Json(Suggestion.InsertSucceed);
                    }
                    else if (flag > 0)
                    {
                        return Json(Suggestion.InsertFail + "，批量跟进未完成，部分跟进设置成功！"); //提示输入的数据的格式不对 
                    }
                }
            }
            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 跟进修改 create by chand 2015-07-20

        [SupportFilter]
        public ActionResult Edit(string ID)
        {
            TB_CustomerFollow entity = m_BLL.GetById(ID);
            return View(entity);
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Edit(string ID, TB_CustomerFollow entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();

                entity.VersionNo++;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;

                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，修改客户跟进，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "工作安排"
                        );//写入日志 
                    return Json(Suggestion.UpdateSucceed);
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改客户跟进错误：" + returnValue, "工作安排"
                        );//写入日志                      
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 批量跟进修改 create by chand 2015-07-31

        [SupportFilter]
        public ActionResult BatchEdit(string ID)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerFollowID = ID;
            entity.ReservationDate = DateTime.Now.AddMinutes(10);
            return View(entity);
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult BatchEdit(string ID, TB_CustomerFollow entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string returnValue = string.Empty;
                string[] arr = entity.CustomerFollowID.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    int flag = 0;
                    string currentPerson = GetCurrentPersonID();
                    DateTime date = DateTime.Now;

                    for (int i = 0; i < arr.Length; i++)
                    {
                        var followInfo = m_BLL.GetById(arr[i]);
                        if (followInfo == null)
                        {
                            continue;
                        }
                        followInfo.ReservationDate = entity.ReservationDate;
                        followInfo.FollowUpMode = entity.FollowUpMode;
                        followInfo.LastUpdatedTime = date;
                        followInfo.LastUpdatedBy = currentPerson;

                        if (m_BLL.Edit(ref validationErrors, followInfo))
                        {
                            flag++;
                            //写入日志 
                            LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，修改客户跟进(批量)，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "客户管理");
                        }
                        else
                        {
                            if (validationErrors != null && validationErrors.Count > 0)
                            {
                                validationErrors.All(a =>
                                {
                                    returnValue += a.ErrorMessage;
                                    return true;
                                });
                            }
                            //写入日志  
                            LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改客户跟进(批量)错误：" + returnValue, "客户管理");
                        }
                    }
                    if (flag == arr.Length)
                    {
                        return Json(Suggestion.UpdateSucceed);
                    }
                    else if (flag > 0)
                    {
                        return Json(Suggestion.UpdateFail + "，批量修改跟进未完成，部分跟进设置成功！"); //提示输入的数据的格式不对 
                    }
                }
            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion



        #region 客户跟进记录 crate by chand  2015-06-26

        public JsonResult GetFollowData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;

            List<TB_CustomerFollow> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.AddressDetails,
                    s.CityCode,
                    s.CityName,
                    s.ContactState,
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerFeedback,
                    s.CustomerFollowID,
                    s.CustomerFunnel,
                    s.CustomerContactName,
                    s.CustomerID,
                    s.CustomerLevel,
                    s.CustomerName,
                    s.FollowUpCategory,
                    s.FollowUpDate,
                    s.FollowUpMode,
                    s.FollowUpPurpose,
                    s.IsFinish,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.NextFollowDate,
                    s.NextFollowUpMode,
                    s.Opportunities,
                    s.OtherLevel,
                    s.ProcessMode,
                    s.Remark,
                    s.ReservationDate,
                    s.SysPersonID,
                    s.SysPersonName,
                    s.ContactName

                }
                   )
            });
        }
        #endregion

        #region 跟进检查
        /// <summary>
        /// 是否有问完成的跟进记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult IsContact(string id)
        {

            int flag = 0;
            Account account = GetCurrentAccount();
            if (m_BLL.IsContact(id, account.Id, account.FollowType))
            {
                flag = 1;
            }
            return Json(new { state = flag });
        }

        #endregion


        #region 客户进程历史反馈
        /// <summary>
        /// Change By：Jonny|| Date:2015.10.10 ||Remark:新增传递的参数Fcode用于获取电话录音 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="FollowID"></param>
        /// <param name="Fcode">分机号</param>
        /// <returns></returns>
        public ActionResult FollowBackNew(string CustomerID, string FollowID, string Fcode)
        {

            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerID = CustomerID;
            entity.CustomerFollowID = FollowID;
            entity.Fcode = Fcode;//新增分机号
            //TBCustomerBLL customerBll = new TBCustomerBLL();
            //TB_Customer customer = customerBll.GetById(entity.CustomerID);
            //ViewBag.EmailAddress = customer.EmailAddress;
            //ViewBag.ContactPhone = customer.ContactPhone;
            //ViewBag.ContactPerson = customer.ContactPerson;
            //ViewBag.CustomerName = customer.CustomerName;

            Account account = GetCurrentAccount();
            ViewBag.FollowType = account.FollowType;
            entity.NextFollowDate = null;
            return View(entity);

        }
        [HttpPost]
        [SupportFilter]
        public ActionResult FollowBackNew(TB_CustomerFollow model)
        {
            string returnValue = string.Empty;

            TB_CustomerFollow entity = m_BLL.GetById(model.CustomerFollowID);

            if (entity != null && ModelState.IsValid)
            {

                Account account = GetCurrentAccount();
                entity.FollowType = account.FollowType;

                entity.ContactState = model.ContactState;
                entity.FollowUpMode = model.FollowUpMode;
                entity.CustomerContactName = model.CustomerContactName;
                entity.CallPhone = model.CallPhone;
                entity.OtherLevel = model.OtherLevel;
                entity.ExpectedMoney = model.ExpectedMoney;
                entity.ExpectedDate = model.ExpectedDate;
                entity.Opportunities = model.Opportunities;
                entity.RecommendSolutionID = model.RecommendSolutionID;
                entity.RecommendSolutionIDNew = model.RecommendSolutionIDNew;
                entity.NextFollowUpPurpose = model.NextFollowUpPurpose;
                entity.NextFollowUpMode = model.NextFollowUpMode;
                entity.NextFollowDate = model.NextFollowDate;
                entity.CustomerFunnel = model.CustomerFunnel;
                entity.Remark = model.Remark;
                entity.IsKP = model.IsKP;
                entity.IsFinish = true;
                entity.FollowUpDate = DateTime.Now;//实际跟进日期
                entity.VersionNo++;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = account.Id;

                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    //写入日志  
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，填写客户跟进记录，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "工作安排");

                    #region 修改客户 商机判断，客户进程  chand by chand 2015-07-23

                    TBCustomerBLL customerBll = new TBCustomerBLL();
                    TB_Customer customer = customerBll.GetById(entity.CustomerID);
                    if (customer != null)
                    {
                        if (account.FollowType == 2)
                        {
                            customer.Opportunities2 = entity.Opportunities;//商机判断
                            customer.CustomerFunnel2 = entity.CustomerFunnel;//客户进程
                            customer.FollowUpDate2 = DateTime.Now;//跟进日期
                            if (!string.IsNullOrWhiteSpace(entity.Remark))
                            {
                                customer.FollowBack2 = entity.Remark;//最后一次跟进反馈内容
                            }
                        }
                        else
                        {
                            customer.Opportunities = entity.Opportunities;//商机判断
                            customer.CustomerFunnel = entity.CustomerFunnel;//客户进程
                            customer.FollowUpDate = DateTime.Now;//跟进日期
                            if (!string.IsNullOrWhiteSpace(entity.Remark))
                            {
                                customer.FollowBack = entity.Remark;//最后一次跟进反馈内容
                            }
                        }

                        customer.VersionNo++;
                        customer.LastUpdatedTime = DateTime.Now;
                        customer.LastUpdatedBy = entity.LastUpdatedBy;

                        if (customerBll.Edit(ref validationErrors, customer, false))
                        {
                            //写入日志  
                            LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户跟进反馈，更新客户(" + customer.CustomerName + ")商机判断、客户进程，客户Id：" + customer.CustomerID, "工作安排");
                        }

                    }

                    #endregion

                    //如果客户进程变成“冰冻客户”，客户跟进记录中“下次跟进时间”、“跟进目的”、“跟进方式”不为必填项
                    //if (entity.NextFollowDate != null && !string.IsNullOrWhiteSpace(entity.NextFollowUpPurpose))
                    if (entity.NextFollowDate != null)
                    {
                        #region 下次跟进时间和跟进目的不为空时 创建下次跟进
                        DateTime date = DateTime.Now;
                        TB_CustomerFollow newInfo = new TB_CustomerFollow();
                        newInfo.FollowType = account.FollowType;

                        newInfo.CustomerFollowID = Result.GetNewId();
                        newInfo.FollowUpDate = entity.NextFollowDate;//实际跟进日期
                        newInfo.ReservationDate = (DateTime)entity.NextFollowDate;//预约日期
                        //newInfo.OtherLevel = entity.OtherLevel;//客户级别
                        newInfo.CustomerFunnel = entity.CustomerFunnel;//客户进程
                        newInfo.Opportunities = entity.Opportunities;
                        //下次跟进目的
                        if (string.IsNullOrWhiteSpace(entity.NextFollowUpPurpose))
                        {
                            newInfo.FollowUpPurpose = entity.FollowUpPurpose;
                        }
                        else
                        {
                            newInfo.FollowUpPurpose = entity.NextFollowUpPurpose;
                        }
                        if (string.IsNullOrWhiteSpace(entity.NextFollowUpMode))
                        {
                            newInfo.FollowUpMode = "1506251032299623134259baff7c9";//下次跟进方式
                        }
                        else
                        {
                            newInfo.FollowUpMode = entity.NextFollowUpMode;//下次跟进方式
                        }

                        newInfo.FollowUpCategory = "15062511182740603013696f1bf00";//跟进次数（默认持续跟进）


                        newInfo.ProvinceName = entity.ProvinceName;
                        newInfo.ProvinceCode = entity.ProvinceCode;
                        newInfo.CityCode = entity.CityCode;
                        newInfo.CityName = entity.CityName;
                        newInfo.DistrictCode = entity.DistrictCode;
                        newInfo.DistrictName = entity.DistrictName;
                        newInfo.AddressDetails = entity.AddressDetails;
                        newInfo.CustomerID = entity.CustomerID;


                        newInfo.ContactState = 0;
                        newInfo.IsFinish = false;
                        newInfo.SysPersonID = account.Id;
                        newInfo.VersionNo = 1;
                        newInfo.IsDelete = false;
                        newInfo.CreatedTime = date;
                        newInfo.CreatedBy = account.Id;
                        newInfo.LastUpdatedTime = date;
                        newInfo.LastUpdatedBy = account.Id;


                        if (m_BLL.Create(ref validationErrors, newInfo))
                        {
                            //写入日志 
                            LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户跟进设置，客户编号：" + entity.CustomerID + "，跟进编号：" + newInfo.CustomerFollowID, "客户管理");

                        }
                        else
                        {
                            if (validationErrors != null && validationErrors.Count > 0)
                            {
                                validationErrors.All(a =>
                                {
                                    returnValue += a.ErrorMessage;
                                    return true;
                                });
                            }
                            //写入日志  
                            LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");

                        }
                        #endregion
                    }
                    return Json(Suggestion.UpdateSucceed);
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，填写客户跟进记录错误：" + returnValue, "工作安排"
                        );//写入日志                      
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        public ActionResult FollowBackHistory(string CustomerID, int page, int rows = 15, string search = "")
        {
            List<CRM.DAL.CustomerFollowModel> list = new List<CustomerFollowModel>();
            int total = 0;
            Account account = GetCurrentAccount();
            list = m_BLL.GetFollowBackHistoryByCustomerID(CustomerID, account.FollowType, page, rows, search, ref total);
            return PartialView("FollowBackHistory", list);
        }


        public ActionResult FollowBackHistoryPartial(string CustomerID, int FollowType, int page, int rows = 15, string search = "")
        {
            List<CRM.DAL.CustomerFollowModel> list = new List<CustomerFollowModel>();
            int total = 0;
            list = m_BLL.GetFollowBackHistoryByCustomerID(CustomerID, FollowType, page, rows, search, ref total);
            ViewBag.CustomerID = CustomerID;
            Account account = GetCurrentAccount();
            ViewBag.FollowType = account.FollowType;
            ViewData["FollowType"] = FollowType;
            return View(list);
        }
        public ActionResult FollowBackHistoryPartial2(string CustomerID, int FollowType, int page, int rows = 15, string search = "")
        {
            List<CRM.DAL.CustomerFollowModel> list = new List<CustomerFollowModel>();
            int total = 0;
            list = m_BLL.GetFollowBackHistoryByCustomerID(CustomerID, FollowType, page, rows, search, ref total);
            ViewBag.CustomerID = CustomerID;
            Account account = GetCurrentAccount();
            ViewBag.FollowType = account.FollowType;
            ViewData["FollowType"] = FollowType;
            return View(list);
        }

        #endregion


        #region  获取客户最后一次联系人
        [HttpPost]
        public JsonResult GetLastContactInfo(string id)
        {
            string currentPerson = GetCurrentPersonID();
            TB_CustomerFollow entity = m_BLL.GetLastContactInfo(id, currentPerson);
            if (entity != null)
            {
                return Json(
                new
                {
                    Result = 1,
                    ContactName = entity.CustomerContactName,
                    CallPhone = entity.CallPhone,
                    //CustomerEmail = entity.CustomerEmail,
                    OtherLevel = entity.OtherLevel,
                    IsKP = entity.IsKP,
                    CustomerFunnel = entity.CustomerFunnel

                });
            }
            return Json(
                new
                {
                    Result = 0,

                });
        }


        #endregion


        #region 修改反馈内容 create by chand 2015-12-01

        /// <summary>
        /// 修改反馈内容
        /// </summary>
        /// <param name="followID"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public JsonResult GetUpdateFollowBack(string CustomerFollowID, string Remark)
        {
            int flag = 0;
            string returnValue = string.Empty;
            TB_CustomerFollow entity = m_BLL.GetById(CustomerFollowID);
            if (entity != null)
            {
                string currentPerson = GetCurrentPersonID();
                if (entity.LastUpdatedBy == currentPerson)
                {
                    entity.Remark = Remark;
                    entity.LastUpdatedTime = DateTime.Now;

                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        flag = 1;
                        //写入日志  
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，修改跟进反馈信息，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "工作安排");
                    }
                    else
                    {
                        if (validationErrors != null && validationErrors.Count > 0)
                        {
                            validationErrors.All(a =>
                            {
                                returnValue += a.ErrorMessage;
                                return true;
                            });
                        }
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改跟进反馈信息失败：" + returnValue, "工作安排");//写入日志

                    }
                }
                else
                {
                    returnValue = "您不是跟进人，不能修改跟进反馈内容";
                }
            }
            else
            {
                returnValue = "数据有误，修改失败";
            }
            return Json(
                new
                {
                    Result = flag,
                    Msg = returnValue
                });
        }

        #endregion



        #region 增加客户跟进记录 create by chand 2016-2-22

        public ActionResult FollowBackAdd(string id)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerID = id;
            entity.NextFollowDate = null;
            entity.ExpectedDate = null;
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public ActionResult FollowBackAdd(TB_CustomerFollow model)
        {

            if (model != null && ModelState.IsValid)
            {

                string returnValue = string.Empty;
                validationErrors.Clear();
                Account account = GetCurrentAccount();
                DateTime date = DateTime.Now;
                try
                {
                    TB_CustomerFollow entity = new TB_CustomerFollow();
                    entity.FollowType = account.FollowType;

                    entity.CustomerFollowID = Result.GetNewId();

                    entity.CustomerID = model.CustomerID;
                    entity.IsFinish = true;
                    entity.SysPersonID = account.Id;
                    entity.VersionNo = 1;
                    entity.IsDelete = false;
                    entity.CreatedTime = date;
                    entity.CreatedBy = account.Id;
                    entity.LastUpdatedTime = date;
                    entity.LastUpdatedBy = account.Id;
                    //跟进日期
                    entity.ReservationDate = date;


                    //进一步沟通和挖掘需求
                    entity.FollowUpPurpose = "1506121004119556770670248031f";
                    //跟进方式
                    if (!string.IsNullOrWhiteSpace(model.FollowUpMode))
                    {
                        entity.FollowUpMode = model.FollowUpMode;
                    }
                    else
                    {
                        //电话沟通
                        entity.FollowUpMode = "1506251032299623134259baff7c9";
                    }

                    //持续跟进
                    entity.FollowUpCategory = "15062511182740603013696f1bf00";

                    entity.ContactState = 1;
                    //联系人
                    entity.CustomerContactName = model.CustomerContactName;
                    entity.CallPhone = model.CallPhone;
                    entity.OtherLevel = model.OtherLevel;
                    entity.ExpectedMoney = model.ExpectedMoney;
                    entity.ExpectedDate = model.ExpectedDate;
                    entity.Opportunities = model.Opportunities;
                    entity.RecommendSolutionID = model.RecommendSolutionID;
                    entity.RecommendSolutionIDNew = model.RecommendSolutionIDNew;
                    entity.NextFollowUpPurpose = model.NextFollowUpPurpose;
                    entity.NextFollowUpMode = model.NextFollowUpMode;
                    entity.NextFollowDate = model.NextFollowDate;
                    entity.CustomerFunnel = model.CustomerFunnel;
                    entity.Remark = model.Remark;
                    entity.FollowUpDate = model.FollowUpDate;//实际跟进日期
                    if (entity.FollowUpDate == null)
                    {
                        entity.FollowUpDate = DateTime.Now;//实际跟进日期
                    }

                    if (m_BLL.Create(ref validationErrors, entity))
                    {
                        //写入日志  
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，填写客户跟进记录，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "工作安排");

                        #region 修改客户 商机判断，客户进程  chand by chand 2015-07-23

                        TBCustomerBLL customerBll = new TBCustomerBLL();
                        TB_Customer customer = customerBll.GetById(entity.CustomerID);
                        if (customer != null)
                        {
                            if (account.FollowType == 2)
                            {
                                customer.Opportunities2 = entity.Opportunities;//商机判断
                                customer.CustomerFunnel2 = entity.CustomerFunnel;//客户进程
                                customer.FollowUpDate2 = DateTime.Now;//跟进日期
                                if (!string.IsNullOrWhiteSpace(entity.Remark))
                                {
                                    customer.FollowBack2 = entity.Remark;//最后一次跟进反馈内容
                                }
                            }
                            else
                            {
                                customer.Opportunities = entity.Opportunities;//商机判断
                                customer.CustomerFunnel = entity.CustomerFunnel;//客户进程
                                customer.FollowUpDate = DateTime.Now;//跟进日期
                                if (!string.IsNullOrWhiteSpace(entity.Remark))
                                {
                                    customer.FollowBack = entity.Remark;//最后一次跟进反馈内容
                                }
                            }

                            customer.VersionNo++;
                            customer.LastUpdatedTime = DateTime.Now;
                            customer.LastUpdatedBy = entity.LastUpdatedBy;

                            if (customerBll.Edit(ref validationErrors, customer, false))
                            {
                                //写入日志  
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户跟进反馈，更新客户(" + customer.CustomerName + ")商机判断、客户进程，客户Id：" + customer.CustomerID, "工作安排");
                            }

                        }

                        #endregion

                        if (entity.NextFollowDate != null)
                        {
                            #region 下次跟进时间和跟进目的不为空时 创建下次跟进

                            TB_CustomerFollow newInfo = new TB_CustomerFollow();
                            newInfo.FollowType = account.FollowType;

                            newInfo.CustomerFollowID = Result.GetNewId();
                            newInfo.FollowUpDate = entity.NextFollowDate;//实际跟进日期
                            newInfo.ReservationDate = (DateTime)entity.NextFollowDate;//预约日期
                            newInfo.OtherLevel = entity.OtherLevel;
                            newInfo.CustomerFunnel = entity.CustomerFunnel;//客户进程
                            newInfo.Opportunities = entity.Opportunities;
                            //下次跟进目的
                            if (string.IsNullOrWhiteSpace(entity.NextFollowUpPurpose))
                            {
                                newInfo.FollowUpPurpose = entity.FollowUpPurpose;
                            }
                            else
                            {
                                newInfo.FollowUpPurpose = entity.NextFollowUpPurpose;
                            }
                            if (string.IsNullOrWhiteSpace(entity.NextFollowUpMode))
                            {
                                newInfo.FollowUpMode = "1506251032299623134259baff7c9";//下次跟进方式
                            }
                            else
                            {
                                newInfo.FollowUpMode = entity.NextFollowUpMode;//下次跟进方式
                            }

                            newInfo.FollowUpCategory = "15062511182740603013696f1bf00";//跟进次数（默认持续跟进）


                            newInfo.ProvinceName = entity.ProvinceName;
                            newInfo.ProvinceCode = entity.ProvinceCode;
                            newInfo.CityCode = entity.CityCode;
                            newInfo.CityName = entity.CityName;
                            newInfo.DistrictCode = entity.DistrictCode;
                            newInfo.DistrictName = entity.DistrictName;
                            newInfo.AddressDetails = entity.AddressDetails;
                            newInfo.CustomerID = entity.CustomerID;


                            newInfo.ContactState = 0;
                            newInfo.IsFinish = false;
                            newInfo.SysPersonID = account.Id;
                            newInfo.VersionNo = 1;
                            newInfo.IsDelete = false;
                            newInfo.CreatedTime = date;
                            newInfo.CreatedBy = account.Id;
                            newInfo.LastUpdatedTime = date;
                            newInfo.LastUpdatedBy = account.Id;


                            if (m_BLL.Create(ref validationErrors, newInfo))
                            {
                                //写入日志 
                                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户跟进设置，客户编号：" + entity.CustomerID + "，跟进编号：" + newInfo.CustomerFollowID, "客户管理");

                            }
                            else
                            {
                                if (validationErrors != null && validationErrors.Count > 0)
                                {
                                    validationErrors.All(a =>
                                    {
                                        returnValue += a.ErrorMessage;
                                        return true;
                                    });
                                }
                                //写入日志  
                                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");

                            }
                            #endregion
                        }

                        return Json(Suggestion.InsertSucceed);
                    }
                    else
                    {
                        if (validationErrors != null && validationErrors.Count > 0)
                        {
                            validationErrors.All(a =>
                            {
                                returnValue += a.ErrorMessage;
                                return true;
                            });
                        }
                        //写入日志  
                        LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");
                        return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                    }

                }
                catch (Exception ex)
                {
                    LogClassModels.WriteExceptions(ex);
                }
            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }


        #endregion

        #region 新增跟进记录 create  by chand 2016-09-22

        [SupportFilter]
        public ActionResult Create(string ID)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerID = ID;
            entity.NextFollowDate = null;
            entity.ExpectedDate = null;
            return View("CreateForm", entity);
        }



        [HttpPost]
        [SupportFilter]
        public ActionResult GetCreate(TB_CustomerFollow model)
        {

            if (model != null && ModelState.IsValid)
            {

                string returnValue = string.Empty;
                validationErrors.Clear();
                Account account = GetCurrentAccount();
                DateTime date = DateTime.Now;
                try
                {
                    TB_CustomerFollow entity = new TB_CustomerFollow();
                    entity.FollowType = account.FollowType;

                    entity.CustomerFollowID = Result.GetNewId();

                    entity.CustomerID = model.CustomerID;
                    entity.IsFinish = true;
                    entity.SysPersonID = account.Id;
                    entity.VersionNo = 1;
                    entity.IsDelete = false;
                    entity.CreatedTime = date;
                    entity.CreatedBy = account.Id;
                    entity.LastUpdatedTime = date;
                    entity.LastUpdatedBy = account.Id;
                    //跟进日期
                    entity.ReservationDate = date;


                    //进一步沟通和挖掘需求
                    entity.FollowUpPurpose = "1506121004119556770670248031f";
                    //跟进方式
                    if (!string.IsNullOrWhiteSpace(model.FollowUpMode))
                    {
                        entity.FollowUpMode = model.FollowUpMode;
                    }
                    else
                    {
                        //电话沟通
                        entity.FollowUpMode = "1506251032299623134259baff7c9";
                    }

                    //持续跟进
                    entity.FollowUpCategory = "15062511182740603013696f1bf00";

                    entity.ContactState = 1;
                    //联系人
                    entity.CustomerContactName = model.CustomerContactName;
                    entity.CallPhone = model.CallPhone;
                    entity.OtherLevel = model.OtherLevel;
                    entity.ExpectedMoney = model.ExpectedMoney;
                    entity.ExpectedDate = model.ExpectedDate;
                    entity.Opportunities = model.Opportunities;
                    entity.RecommendSolutionID = model.RecommendSolutionID;
                    entity.RecommendSolutionIDNew = model.RecommendSolutionIDNew;
                    entity.NextFollowUpPurpose = model.NextFollowUpPurpose;
                    entity.NextFollowUpMode = model.NextFollowUpMode;
                    entity.NextFollowDate = model.NextFollowDate;
                    entity.CustomerFunnel = model.CustomerFunnel;
                    entity.Remark = model.Remark;
                    entity.FollowUpDate = model.FollowUpDate;//实际跟进日期

                    entity.GoTime = model.GoTime;//出发时间
                    entity.BackTime = model.BackTime;//回来时间


                    if (entity.FollowUpDate == null)
                    {
                        entity.FollowUpDate = DateTime.Now;//实际跟进日期
                    }

                    if (m_BLL.Create(ref validationErrors, entity))
                    {
                        //写入日志  
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，填写客户跟进记录，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "工作安排");

                        #region 修改客户 商机判断，客户进程  chand by chand 2015-07-23

                        TBCustomerBLL customerBll = new TBCustomerBLL();
                        TB_Customer customer = customerBll.GetById(entity.CustomerID);
                        if (customer != null)
                        {
                            if (account.FollowType == 2)
                            {
                                customer.Opportunities2 = entity.Opportunities;//商机判断
                                customer.CustomerFunnel2 = entity.CustomerFunnel;//客户进程
                                customer.FollowUpDate2 = DateTime.Now;//跟进日期
                                if (!string.IsNullOrWhiteSpace(entity.Remark))
                                {
                                    customer.FollowBack2 = entity.Remark;//最后一次跟进反馈内容
                                }
                            }
                            else
                            {
                                customer.Opportunities = entity.Opportunities;//商机判断
                                customer.CustomerFunnel = entity.CustomerFunnel;//客户进程
                                customer.FollowUpDate = DateTime.Now;//跟进日期
                                if (!string.IsNullOrWhiteSpace(entity.Remark))
                                {
                                    customer.FollowBack = entity.Remark;//最后一次跟进反馈内容
                                }
                            }

                            customer.VersionNo++;
                            customer.LastUpdatedTime = DateTime.Now;
                            customer.LastUpdatedBy = entity.LastUpdatedBy;

                            if (customerBll.Edit(ref validationErrors, customer, false))
                            {
                                //写入日志  
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户跟进反馈，更新客户(" + customer.CustomerName + ")商机判断、客户进程，客户Id：" + customer.CustomerID, "工作安排");
                            }

                        }

                        #endregion

                        if (entity.NextFollowDate != null)
                        {
                            #region 下次跟进时间和跟进目的不为空时 创建下次跟进

                            TB_CustomerFollow newInfo = new TB_CustomerFollow();
                            newInfo.FollowType = account.FollowType;

                            newInfo.CustomerFollowID = Result.GetNewId();
                            newInfo.FollowUpDate = entity.NextFollowDate;//实际跟进日期
                            newInfo.ReservationDate = (DateTime)entity.NextFollowDate;//预约日期
                            newInfo.OtherLevel = entity.OtherLevel;
                            newInfo.CustomerFunnel = entity.CustomerFunnel;//客户进程
                            newInfo.Opportunities = entity.Opportunities;
                            //下次跟进目的
                            if (string.IsNullOrWhiteSpace(entity.NextFollowUpPurpose))
                            {
                                newInfo.FollowUpPurpose = entity.FollowUpPurpose;
                            }
                            else
                            {
                                newInfo.FollowUpPurpose = entity.NextFollowUpPurpose;
                            }
                            if (string.IsNullOrWhiteSpace(entity.NextFollowUpMode))
                            {
                                newInfo.FollowUpMode = "1506251032299623134259baff7c9";//下次跟进方式
                            }
                            else
                            {
                                newInfo.FollowUpMode = entity.NextFollowUpMode;//下次跟进方式
                            }

                            newInfo.FollowUpCategory = "15062511182740603013696f1bf00";//跟进次数（默认持续跟进）


                            newInfo.ProvinceName = entity.ProvinceName;
                            newInfo.ProvinceCode = entity.ProvinceCode;
                            newInfo.CityCode = entity.CityCode;
                            newInfo.CityName = entity.CityName;
                            newInfo.DistrictCode = entity.DistrictCode;
                            newInfo.DistrictName = entity.DistrictName;
                            newInfo.AddressDetails = entity.AddressDetails;
                            newInfo.CustomerID = entity.CustomerID;


                            newInfo.ContactState = 0;
                            newInfo.IsFinish = false;
                            newInfo.SysPersonID = account.Id;
                            newInfo.VersionNo = 1;
                            newInfo.IsDelete = false;
                            newInfo.CreatedTime = date;
                            newInfo.CreatedBy = account.Id;
                            newInfo.LastUpdatedTime = date;
                            newInfo.LastUpdatedBy = account.Id;


                            if (m_BLL.Create(ref validationErrors, newInfo))
                            {
                                //写入日志 
                                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户跟进设置，客户编号：" + entity.CustomerID + "，跟进编号：" + newInfo.CustomerFollowID, "客户管理");

                            }
                            else
                            {
                                if (validationErrors != null && validationErrors.Count > 0)
                                {
                                    validationErrors.All(a =>
                                    {
                                        returnValue += a.ErrorMessage;
                                        return true;
                                    });
                                }
                                //写入日志  
                                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");

                            }
                            #endregion
                        }

                        return Json(Suggestion.InsertSucceed);
                    }
                    else
                    {
                        if (validationErrors != null && validationErrors.Count > 0)
                        {
                            validationErrors.All(a =>
                            {
                                returnValue += a.ErrorMessage;
                                return true;
                            });
                        }
                        //写入日志  
                        LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");
                        return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                    }

                }
                catch (Exception ex)
                {
                    LogClassModels.WriteExceptions(ex);
                }
            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 跟进反馈 create by chand 2016-09-22
        [SupportFilter]
        public ActionResult FollowBack(string CustomerID, string FollowID, string Fcode)
        {
            TB_CustomerFollow entity = new TB_CustomerFollow();
            entity.CustomerID = CustomerID;
            entity.CustomerFollowID = FollowID;
            entity.Fcode = Fcode;

            Account account = GetCurrentAccount();
            ViewBag.FollowType = account.FollowType;
            entity.NextFollowDate = null;
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public ActionResult GetFollowBack(TB_CustomerFollow model)
        {
            string returnValue = string.Empty;

            TB_CustomerFollow entity = m_BLL.GetById(model.CustomerFollowID);

            if (entity != null && ModelState.IsValid)
            {

                Account account = GetCurrentAccount();
                entity.FollowType = account.FollowType;

                entity.ContactState = model.ContactState;
                entity.FollowUpMode = model.FollowUpMode;
                entity.CustomerContactName = model.CustomerContactName;
                entity.CallPhone = model.CallPhone;
                entity.OtherLevel = model.OtherLevel;
                entity.ExpectedMoney = model.ExpectedMoney;
                entity.ExpectedDate = model.ExpectedDate;
                entity.Opportunities = model.Opportunities;
                entity.RecommendSolutionID = model.RecommendSolutionID;
                entity.RecommendSolutionIDNew = model.RecommendSolutionIDNew;
                entity.NextFollowUpPurpose = model.NextFollowUpPurpose;
                entity.NextFollowUpMode = model.NextFollowUpMode;
                entity.NextFollowDate = model.NextFollowDate;
                entity.CustomerFunnel = model.CustomerFunnel;
                entity.Remark = model.Remark;
                entity.IsKP = model.IsKP;
                entity.IsFinish = true;
                entity.FollowUpDate = DateTime.Now;//实际跟进日期
                entity.VersionNo++;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = account.Id;

                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    //写入日志  
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，填写客户跟进记录，客户编号：" + entity.CustomerID + "，跟进编号：" + entity.CustomerFollowID, "工作安排");

                    #region 修改客户 商机判断，客户进程  chand by chand 2015-07-23

                    TBCustomerBLL customerBll = new TBCustomerBLL();
                    TB_Customer customer = customerBll.GetById(entity.CustomerID);
                    if (customer != null)
                    {
                        if (account.FollowType == 2)
                        {
                            customer.Opportunities2 = entity.Opportunities;//商机判断
                            customer.CustomerFunnel2 = entity.CustomerFunnel;//客户进程
                            customer.FollowUpDate2 = DateTime.Now;//跟进日期
                            if (!string.IsNullOrWhiteSpace(entity.Remark))
                            {
                                customer.FollowBack2 = entity.Remark;//最后一次跟进反馈内容
                            }
                        }
                        else
                        {
                            customer.Opportunities = entity.Opportunities;//商机判断
                            customer.CustomerFunnel = entity.CustomerFunnel;//客户进程
                            customer.FollowUpDate = DateTime.Now;//跟进日期
                            if (!string.IsNullOrWhiteSpace(entity.Remark))
                            {
                                customer.FollowBack = entity.Remark;//最后一次跟进反馈内容
                            }
                        }

                        customer.VersionNo++;
                        customer.LastUpdatedTime = DateTime.Now;
                        customer.LastUpdatedBy = entity.LastUpdatedBy;

                        if (customerBll.Edit(ref validationErrors, customer, false))
                        {
                            //写入日志  
                            LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户跟进反馈，更新客户(" + customer.CustomerName + ")商机判断、客户进程，客户Id：" + customer.CustomerID, "工作安排");
                        }

                    }

                    #endregion

                    //如果客户进程变成“冰冻客户”，客户跟进记录中“下次跟进时间”、“跟进目的”、“跟进方式”不为必填项
                    //if (entity.NextFollowDate != null && !string.IsNullOrWhiteSpace(entity.NextFollowUpPurpose))
                    if (entity.NextFollowDate != null)
                    {
                        #region 下次跟进时间和跟进目的不为空时 创建下次跟进
                        DateTime date = DateTime.Now;
                        TB_CustomerFollow newInfo = new TB_CustomerFollow();
                        newInfo.FollowType = account.FollowType;

                        newInfo.CustomerFollowID = Result.GetNewId();
                        newInfo.FollowUpDate = entity.NextFollowDate;//实际跟进日期
                        newInfo.ReservationDate = (DateTime)entity.NextFollowDate;//预约日期
                        //newInfo.OtherLevel = entity.OtherLevel;//客户级别
                        newInfo.CustomerFunnel = entity.CustomerFunnel;//客户进程
                        newInfo.Opportunities = entity.Opportunities;
                        //下次跟进目的
                        if (string.IsNullOrWhiteSpace(entity.NextFollowUpPurpose))
                        {
                            newInfo.FollowUpPurpose = entity.FollowUpPurpose;
                        }
                        else
                        {
                            newInfo.FollowUpPurpose = entity.NextFollowUpPurpose;
                        }
                        if (string.IsNullOrWhiteSpace(entity.NextFollowUpMode))
                        {
                            newInfo.FollowUpMode = "1506251032299623134259baff7c9";//下次跟进方式
                        }
                        else
                        {
                            newInfo.FollowUpMode = entity.NextFollowUpMode;//下次跟进方式
                        }

                        newInfo.FollowUpCategory = "15062511182740603013696f1bf00";//跟进次数（默认持续跟进）


                        newInfo.ProvinceName = entity.ProvinceName;
                        newInfo.ProvinceCode = entity.ProvinceCode;
                        newInfo.CityCode = entity.CityCode;
                        newInfo.CityName = entity.CityName;
                        newInfo.DistrictCode = entity.DistrictCode;
                        newInfo.DistrictName = entity.DistrictName;
                        newInfo.AddressDetails = entity.AddressDetails;
                        newInfo.CustomerID = entity.CustomerID;


                        newInfo.ContactState = 0;
                        newInfo.IsFinish = false;
                        newInfo.SysPersonID = account.Id;
                        newInfo.VersionNo = 1;
                        newInfo.IsDelete = false;
                        newInfo.CreatedTime = date;
                        newInfo.CreatedBy = account.Id;
                        newInfo.LastUpdatedTime = date;
                        newInfo.LastUpdatedBy = account.Id;


                        if (m_BLL.Create(ref validationErrors, newInfo))
                        {
                            //写入日志 
                            LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户跟进设置，客户编号：" + entity.CustomerID + "，跟进编号：" + newInfo.CustomerFollowID, "客户管理");

                        }
                        else
                        {
                            if (validationErrors != null && validationErrors.Count > 0)
                            {
                                validationErrors.All(a =>
                                {
                                    returnValue += a.ErrorMessage;
                                    return true;
                                });
                            }
                            //写入日志  
                            LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户跟进设置错误：" + returnValue, "客户管理");

                        }
                        #endregion
                    }
                    return Json(Suggestion.UpdateSucceed);
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，填写客户跟进记录错误：" + returnValue, "工作安排"
                        );//写入日志                      
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion
    }
}