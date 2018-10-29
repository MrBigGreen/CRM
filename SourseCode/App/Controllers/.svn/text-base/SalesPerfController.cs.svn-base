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
    /// 销售业绩控制器
    /// 创建人：chand
    /// 创建时间：2016-07-19
    /// </summary>
    public class SalesPerfController : BaseController
    {

        #region 初始化

        IBLL.ITBSalesPerformanceBLL m_BLL;

        public SalesPerfController()
            : this(new TBSalesPerformanceBLL())
        { }

        public SalesPerfController(TBSalesPerformanceBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion


        public ActionResult List()
        {

            return View();
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public ActionResult Create(SalesPerfModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();

                TB_SalesPerformance entity1 = new TB_SalesPerformance();
                entity1.SalesPerformanceID = Result.GetNewId();
                entity1.SysPersonID = currentPerson;
                entity1.TheMonth = model.TheMonth.Substring(0, 7);
                entity1.TheWeek = model.TheWeek;
                entity1.Category = 2;
                entity1.FollowUpQty = model.FollowUpQty1;
                entity1.ContractQty = model.ContractQty1;
                entity1.BillingAmount = model.BillingAmount1;
                entity1.BudgetAmount = model.BudgetAmount1;
                entity1.KeyCustomerQty = model.KeyCustomerQty1;
                if (model.ContractQty1 < 1)
                {
                    entity1.FollowUpAvg = 0;
                }
                else
                {
                    entity1.FollowUpAvg = model.FollowUpQty1 / model.ContractQty1;
                }

                entity1.CreatedTime = DateTime.Now;
                entity1.CreatedBy = currentPerson;
                entity1.LastUpdatedTime = DateTime.Now;
                entity1.LastUpdatedBy = currentPerson;



                TB_SalesPerformance entity2 = new TB_SalesPerformance();
                entity2.SalesPerformanceID = Result.GetNewId();
                entity2.SysPersonID = currentPerson;
                if (model.TheWeek == 4)
                {

                    entity2.TheWeek = 1;
                    var NextMonth = DateTime.Parse(model.TheMonth).AddMonths(1);
                    entity2.TheMonth = NextMonth.ToString("yyyy-MM");

                }
                else
                {
                    entity2.TheWeek = model.TheWeek + 1;
                    entity2.TheMonth = model.TheMonth.Substring(0, 7);
                }

                entity2.Category = 1;
                entity2.FollowUpQty = model.FollowUpQty2;
                entity2.ContractQty = model.ContractQty2;
                entity2.BillingAmount = model.BillingAmount2;
                entity2.BudgetAmount = model.BudgetAmount2;
                entity2.KeyCustomerQty = model.KeyCustomerQty2;
                if (model.ContractQty2 < 1)
                {
                    entity2.FollowUpAvg = 0;
                }
                else
                {
                    entity2.FollowUpAvg = model.FollowUpQty2 / model.ContractQty2;
                }

                entity2.CreatedTime = DateTime.Now;
                entity2.CreatedBy = currentPerson;
                entity2.LastUpdatedTime = DateTime.Now;
                entity2.LastUpdatedBy = currentPerson;

                List<TB_SalesPerformance> list = new List<TB_SalesPerformance>();
                list.Add(entity1);
                list.Add(entity2);
                if (m_BLL.IsExists(currentPerson, entity1.TheMonth, (int)entity1.TheWeek))
                {
                    return Json(Suggestion.InsertFail + "，当前月份的业绩已经添加过"); //提示输入的数据的格式不对 
                }

                string returnValue = string.Empty;
                if (m_BLL.CreateCollection(ref validationErrors, list.AsQueryable()))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，添加" + entity1.TheMonth + "的销售业绩", "销售业绩");//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，添加销售业绩，" + returnValue, "销售业绩"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }




        public ActionResult Edit(string id)
        {
            TB_SalesPerformance entity = m_BLL.GetById(id);
            if (entity == null)
            {
                return Content("内容不存在！");
            }
            return View(entity);
        }



        [HttpPost]
        [SupportFilter]
        public ActionResult Edit(TB_SalesPerformance model)
        {



            TB_SalesPerformance entity1 = m_BLL.GetById(model.SalesPerformanceID);
            if (entity1 != null)
            {
                string currentPerson = GetCurrentPersonID();
                entity1.FollowUpQty = model.FollowUpQty;
                entity1.ContractQty = model.ContractQty;
                entity1.BillingAmount = model.BillingAmount;
                entity1.BudgetAmount = model.BudgetAmount;
                entity1.KeyCustomerQty = model.KeyCustomerQty;
                if (model.ContractQty > 0)
                {
                    entity1.FollowUpAvg = model.FollowUpQty / model.ContractQty;
                }
                

                entity1.LastUpdatedTime = DateTime.Now;
                entity1.LastUpdatedBy = currentPerson;


                string returnValue = string.Empty;
                if (m_BLL.Edit(ref validationErrors, entity1))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，修改" + entity1.TheMonth + " " + entity1.TheWeek + "的销售业绩", "销售业绩");//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改销售业绩，" + returnValue, "销售业绩"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }

            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }




        public ActionResult SummaryReport()
        {
            string CurrentPersonID = GetCurrentPersonID();
            var PersonList = new SysPersonBLL().GetPersonVisibility(CurrentPersonID);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = CurrentPersonID;
            return View(entity);
        }


        [HttpPost]
        public ActionResult GetData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SysPersonIDDDL_String&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }



        #region  获取所有业绩月份

        public ActionResult GetMonth()
        {
            var list = m_BLL.GetMonths();
            SelectList select = new SelectList(list.Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
            return Json(select, "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion
        [HttpPost]
        public ActionResult GetSalesPerfReport(string order, string sort, string search)
        {

            string SearchPersonID = GetCurrentPersonID();
            var queryData = m_BLL.GetSalesPerfReport(SearchPersonID, search);
            return Json(new datagrid
            {
                total = 10,
                rows = queryData
            });
        }





    }
}