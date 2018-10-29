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
    /// 公积金控制器
    /// 创建人：chand
    /// 创建时间：2016-08-26
    /// 
    /// </summary>
    public class PayHousingFundController : BaseController
    {
      
        #region 初始化

        IBLL.ITPHousingFundBLL m_BLL;

        public PayHousingFundController()
            : this(new TPHousingFundBLL())
        { }

        public PayHousingFundController(TPHousingFundBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion

        public ActionResult List()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            //search += "SysPersonIDDDL_String&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetHousingFundData(page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult GetCreate(TP_HousingFund entity)
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，创建公积金" + entity.HousingFundID, "公积金管理");//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建公积金，" + returnValue, "公积金管理");//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        public ActionResult Edit(string id)
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
        public JsonResult GetEdit(TP_HousingFund model)
        {
            if (model != null && ModelState.IsValid)
            {
                var entity = m_BLL.GetById(model.HousingFundID);
                if (entity != null)
                {

                    entity.IndiRatio = model.IndiRatio;
                    entity.CompanyRatio = model.CompanyRatio;
                    entity.DiffBegin = model.DiffBegin;
                    entity.DiffEnd = model.DiffEnd;
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
                        LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，修改公积金编号：" + entity.HousingFundID, "公积金管理");//写入日志 
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
                        LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建公积金，" + returnValue, "公积金管理");//写入日志                      
                        return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                    }
                }

            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult GetDelete(string ids)
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
                            LogClassModels.WriteServiceLog(Suggestion.DeleteSucceed + "，删除公积金编号：" + entity.HousingFundID, "公积金管理");//写入日志 
                        }
                        else
                        {
                            LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，删除公积金编号：" + entity.HousingFundID, "公积金管理");//写入日志          
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



        public ActionResult View(string id)
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