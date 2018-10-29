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
    public class SalaryManageController : BaseController
    {

        private IBLL.ITPSalaryItemBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();
        public SalaryManageController()
            : this(new TPSalaryItemBLL())
        {
        }

        public SalaryManageController(TPSalaryItemBLL bll)
        {
            m_BLL = bll;
        }

        [SupportFilter]
        //
        // GET: /SalaryManage/
        public ActionResult ItemList()
        {
            return View();
        }

        public ActionResult GetSalaryItemData(int page, int rows, string order, string sort, string search)
        {
            //Account account = GetCurrentAccount();
            int total = 0;


            List<SalaryItemEntity> queryData = m_BLL.GetSalaryItemData(page, rows, search, ref total);


            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }

        [SupportFilter]
        public ActionResult ItemAdd()
        {
            return View();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult ItemAdd(TP_SalaryItem entity)
        {

            if (entity != null && ModelState.IsValid)
            {
                Account account = GetCurrentAccount();
                entity.SalaryItemID = Result.GetNewId(); 
                entity.VersionNo = 1;
                entity.IsDelete = false;
                entity.CreatedTime = DateTime.Now;
                entity.CreatedBy = account.Id;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = account.Id;


                string returnValue = string.Empty;

                if (m_BLL.Create(ref validationErrors, entity))
                {
                    //写入日志 
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，创建薪资项目：" + entity.ItemName + "，项目ID：" + entity.SalaryItemID , "薪酬管理");
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建薪资项目（" + entity.ItemName + "）：" + returnValue, "薪酬管理");
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        [SupportFilter]
        public ActionResult ItemEdit(string id)
        {
            TP_SalaryItem entity = m_BLL.GetById(id);
            return View(entity); 
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult ItemEdit(string ID, TP_SalaryItem model)
        {
            if (model != null && ModelState.IsValid)
            {
                var entity = m_BLL.GetById(model.SalaryItemID);
                if (entity != null)
                {

                    entity.ItemCode = model.ItemCode;
                    entity.ItemName = model.ItemName;
                    entity.ItemClass = model.ItemClass;
                    entity.DataType = model.DataType;
                    entity.Sort = model.Sort; 

                    string currentPerson = GetCurrentPersonID();

                    entity.VersionNo++;
                    entity.LastUpdatedTime = DateTime.Now;
                    entity.LastUpdatedBy = currentPerson;


                    string returnValue = string.Empty;

                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，薪资项目：" + entity.ItemName + "，项目ID：" + entity.SalaryItemID, "薪酬管理");
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改薪资项目（" + entity.ItemName + "）：" + returnValue, "薪酬管理");
                        return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                    }
                }


            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }



        public ActionResult ItemView()
        {
            return View();
        }

    }
}