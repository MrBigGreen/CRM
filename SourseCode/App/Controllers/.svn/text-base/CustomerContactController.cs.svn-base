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
    /// 创建人：chand
    /// 创建时间：2015-06-12
    /// 描述说明：客户联系人-控制器
    /// </summary>
    [SupportFilter]
    public class CustomerContactController : BaseController
    {
        #region 初始化
        IBLL.ITBCustomerContactBLL m_BLL;
        IBLL.ITBCustomerBLL customerBLL = new TBCustomerBLL();
        ValidationErrors validationErrors = new ValidationErrors();

        public CustomerContactController()
            : this(new TBCustomerContactBLL()) { }

        public CustomerContactController(TBCustomerContactBLL bll)
        {
            m_BLL = bll;
        }

        #endregion
        //
        // GET: /CustomerContact/
        public ActionResult Index(string ID)
        {
            ViewBag.CustomerID = ID;
            return View();
        }

        //
        // GET: /CustomerContact/
        public ActionResult ViewList(string ID)
        {
            ViewBag.CustomerID = ID;
            return View();
        }


        public ActionResult MinList(string ID)
        {
            ViewBag.CustomerID = ID;
            return View();
        }

        #region 根据客户编号获取联系人  create by chand 2015-06-17

        public JsonResult GetData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search += "CustomerID&" + id + "^";
            }

            List<CustomerContactModel> queryData = m_BLL.GetCustomerContactData(id, order, sort, search, page, rows, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.BirthDate,
                    s.CompanyTel,
                    s.CompanyTel2,
                    s.ContactName,
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerContactID,
                    s.CustomerID,
                    s.Department,
                    s.Email1,
                    s.Email2,
                    s.Interested,
                    s.IsDelete,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.PhoneNumber1,
                    s.PhoneNumber2,
                    s.PostID,
                    s.PostName,
                    s.QQID,
                    s.Remark,
                    s.VersionNo,
                    s.IsKP

                }
                   )
            });
        }
        #endregion

        #region 创建联系人 create by chand 2015-06-12
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Create(string ID)
        {
            TB_CustomerContact entity = new TB_CustomerContact();
            entity.CustomerID = ID;
            return View(entity);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Create(TB_CustomerContact entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();

                DateTime date = DateTime.Now;
                entity.CustomerContactID = Result.GetNewId();

                entity.VersionNo = 1;
                entity.IsDelete = false;
                entity.CreatedTime = date;
                entity.CreatedBy = currentPerson;
                entity.LastUpdatedTime = date;
                entity.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;

                if (m_BLL.Create(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户联系人：" + entity.ContactName + "，联系人Id：" + entity.CustomerID, "客户管理"
                        );//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户联系人：" + entity.ContactName + ",错误：" + returnValue, "客户管理"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 修改联系人 create by chand 2015-06-18
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Edit(string ID)
        {
            TB_CustomerContact entity = m_BLL.GetById(ID);
            return View(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Edit(TB_CustomerContact entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();
                DateTime date = DateTime.Now;
                entity.VersionNo++;
                entity.LastUpdatedTime = date;
                entity.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;

                if (m_BLL.Edit(ref validationErrors, entity))
                {

                    //写入日志 
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户联系人：" + entity.ContactName + "，联系人Id：" + entity.CustomerID, "客户管理");
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，客户联系人：" + entity.ContactName + ",错误：" + returnValue, "客户管理"
                        );//写入日志                      
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 查看联系人 create by chand 2015-06-12
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Details(string ID)
        {
            TB_CustomerContact entity = m_BLL.GetById(ID);
            if (entity == null)
            {
                entity = new TB_CustomerContact();
            }
            return View(entity);
        }


        #endregion

        #region 删除 create by chand 2015-06-18
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>   
        [HttpPost]
        public ActionResult Delete(string ID, string ContactName)
        {
            string returnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(ID))
            {
                if (m_BLL.Delete(ref validationErrors, ID))
                {
                    //写入日志 
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户联系人：" + ContactName + "，联系人Id：" + ID, "客户管理");
                    App.Codes.MenuCaching.ClearCache(ID);
                    return Json("OK");
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
                    }//删除失败，写入日志
                    LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，客户联系人：" + ContactName + "，联系人Id：" + ID, "客户管理");
                }
            }
            return Json(returnValue);
        }
        #endregion


        public ActionResult ListReadOnly(string ID)
        {
            ViewBag.CustomerID = ID;
            return View();
        }

        public JsonResult GetReadOnlyData(string id, int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            if (account.Name != "offercom")
            {
                int dataType = customerBLL.GetCustomerAuthority(id, account.Id);
                if (dataType == -1)
                {
                    return Json(null);
                }
            }
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search += "CustomerID&" + id + "^";
            }

            List<CustomerContactModel> queryData = m_BLL.GetCustomerContactData(id, order, sort, search, page, rows, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.BirthDate,
                    s.CompanyTel,
                    s.CompanyTel2,
                    s.ContactName,
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerContactID,
                    s.CustomerID,
                    s.Department,
                    s.Email1,
                    s.Email2,
                    s.Interested,
                    s.IsDelete,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.PhoneNumber1,
                    s.PhoneNumber2,
                    s.PostID,
                    s.PostName,
                    s.QQID,
                    s.Remark,
                    s.VersionNo,
                    s.IsKP

                }
                   )
            });
        }
    }
}