using Common;
using CRM.BLL;
using CRM.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 社保维护控制器
    /// 创建人：chand
    /// 创建时间：2016-08-24
    /// </summary>
    public class PaySheBaoController : BaseController
    {

        #region 初始化

        IBLL.ITPSocialInsurBLL m_BLL;

        public PaySheBaoController()
            : this(new TPSocialInsurBLL())
        { }

        public PaySheBaoController(TPSocialInsurBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion

        public ActionResult SheBaoList()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetSheBaoData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            //search += "SysPersonIDDDL_String&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetSocialInsurData(page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }

        public ActionResult SheBaoCreate()
        {
            return View();
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult GetSheBaoCreate(TP_SocialInsur entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();


                entity.IsDelete = false;
                entity.VersionNo = 1;
                entity.CreatedTime = DateTime.Now;
                entity.CreatedBy = currentPerson;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = currentPerson;



                string returnValue = string.Empty;
                if (m_BLL.Create(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，创建社保" + entity.SocialInsurID, "社保管理");//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建社保，" + returnValue, "社保管理");//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        public ActionResult SheBaoEdit(string id)
        {
            var entity = m_BLL.GetById(id);
            if (entity == null)
            {
                return Content("信息未找到");
            }
            return View(entity);
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult GetSheBaoEdit(TP_SocialInsur model)
        {
            if (model != null && ModelState.IsValid)
            {
                var entity = m_BLL.GetById(model.SocialInsurID);
                if (entity != null)
                {

                    entity.CompanyBirth = model.CompanyBirth;
                    entity.CompanyDBTC = model.CompanyDBTC;
                    entity.CompanyInjury = model.CompanyInjury;
                    entity.CompanyMedical = model.CompanyMedical;
                    entity.CompanyPension = model.CompanyPension;
                    entity.CompanySupply = model.CompanySupply;
                    entity.DiffBegin = model.DiffBegin;
                    entity.DiffEnd = model.DiffEnd;
                    entity.IndiBirth = model.IndiBirth;
                    entity.IndiDBTC = model.IndiDBTC;
                    entity.IndiInjury = model.IndiInjury;
                    entity.IndiMedical = model.IndiMedical;
                    entity.IndiPension = model.IndiPension;
                    entity.IndiSupply = model.IndiSupply;
                    entity.SocialInsurName = model.SocialInsurName;
                    entity.CityCode = model.CityCode;
                    entity.DistrictCode = model.DistrictCode;
                    entity.ProvinceCode = model.ProvinceCode;



                    string currentPerson = GetCurrentPersonID();


                    entity.IsDelete = false;
                    entity.VersionNo = entity.VersionNo + 1;
                    entity.LastUpdatedTime = DateTime.Now;
                    entity.LastUpdatedBy = currentPerson;



                    string returnValue = string.Empty;
                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，修改社保编号：" + entity.SocialInsurID, "社保管理");//写入日志 
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
                        LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建社保，" + returnValue, "社保管理");//写入日志                      
                        return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                    }
                }

            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult GetSheBaoDelete(string ids)
        {
            string[] list = ids.GetSplit();
            if (list.Length > 0)
            {
                string currentPerson = GetCurrentPersonID();
                int flag = 0;
                for (int i = 0; i < list.Length; i++)
                {

                    var entity = m_BLL.GetById(list[i]);
                    if (entity != null)
                    {
                        entity.IsDelete = true;
                        entity.VersionNo = entity.VersionNo + 1;
                        entity.LastUpdatedBy = currentPerson;
                        entity.LastUpdatedTime = DateTime.Now;
                        if (m_BLL.Edit(ref validationErrors, entity))
                        {
                            flag++;
                            LogClassModels.WriteServiceLog(Suggestion.DeleteSucceed + "，删除社保编号：" + entity.SocialInsurID, "社保管理");//写入日志 
                        }
                        else
                        {
                            LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，删除社保编号：" + entity.SocialInsurID, "社保管理");//写入日志          
                        }
                    }
                }
                if (flag == list.Length)
                {
                    return Json(Suggestion.DeleteSucceed);
                }
                else
                {
                    string returnValue = string.Empty;
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }
            return Json(Suggestion.DeleteFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 

        }



        public ActionResult SheBaoView(string id)
        {
            var entity = m_BLL.GetById(id);
            if (entity == null)
            {
                return Content("信息未找到");
            }
            return View(entity);
        }

    }
}