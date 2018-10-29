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
    public class CustomerRatingController : BaseController
    {

        #region 初始化

        IBLL.ITBCustomerRatingBLL m_BLL;

        public CustomerRatingController()
            : this(new TBCustomerRatingBLL())
        { }

        public CustomerRatingController(TBCustomerRatingBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion


        /// <summary>
        /// 单个客户的项目列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SingleList(string id)
        {
            TB_Customer entity = new TBCustomerBLL().GetById(id);
            if (entity != null)
            {
                ViewBag.CustomerID = id;
                ViewBag.RatingScore = entity.RatingScore;
            }

            return View();
        }


        public ActionResult Create(string id)
        {
            TB_CustomerRating entity = new TB_CustomerRating();
            entity.CustomerID = id;

            return View(entity);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Create(TB_CustomerRating entity)
        {

            if (entity != null && ModelState.IsValid)
            {
                Account account = GetCurrentAccount();
                entity.CustomerRatingID = Result.GetNewId();
                entity.VersionNo = 1;
                entity.IsDelete = false;
                entity.CreatedTime = DateTime.Now;
                entity.CreatedBy = account.Id;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = account.Id;

                #region 企业信用等级评分标准
                string RatingScore = string.Empty;
                List<string> ItemList = new List<string>();
                ItemList.Add(entity.CompanyNatureCode);
                ItemList.Add(entity.RegisterCapital);
                ItemList.Add(entity.SalesRevenue);
                ItemList.Add(entity.ContractPeriod);
                ItemList.Add(entity.OverdueAdvances);
                if (m_BLL.GetCalcRatingBefore(ItemList, ref RatingScore))
                {
                    entity.RatingScore = RatingScore;
                }
                #endregion


                string returnValue = string.Empty;

                if (m_BLL.Create(ref validationErrors, entity))
                {
                    //写入日志 
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，企业信用评估，评估ID：" + entity.CustomerRatingID, "客户管理");
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，企业信用评估，" + returnValue, "客户管理");
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }




        public ActionResult GetCustomerRatingData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (!string.IsNullOrWhiteSpace(id))
            {
                search = search + "CustomerID&" + id + '^';
            }
            List<CustomerRatingEntity> queryData = m_BLL.GetCustomerRatingData(page, rows, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }


    }
}