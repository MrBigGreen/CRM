using Common;
using CRM.BLL;
using CRM.DAL;
using CRM.DAL.CommonEntity;
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
    /// 创建时间：2015-07-01
    /// 描述说明：客户分享-控制器
    /// </summary>
    [SupportFilter]
    public class CustomerShareController : BaseController
    {
        #region 初始化

        IBLL.ITBCustomerShareBLL m_BLL;

        public CustomerShareController()
            : this(new TBCustomerShareBLL())
        { }

        public CustomerShareController(TBCustomerShareBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion


        //
        // GET: /CustomerShare/
        public ActionResult Index(string ID)
        {
            TB_CustomerShare entity = new TB_CustomerShare();
            entity.CustomerID = ID;
            return View(entity);
        }

        public ActionResult ShareIn()
        {
            return View();
        }


        public ActionResult ShareLog()
        {
            return View();
        }
        
        #region 查询客户归属

        public JsonResult GetData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;

            List<TB_CustomerShare> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerShareID,
                    s.CustomerID,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.SysPersonID,
                    s.SysPersonName,
                    s.VersionNo,
                    s.Authority

                }
                   )
            });
        }
        #endregion
        
        #region 客户分享 create by chand 2015-07-01

        public JsonResult Share(string SysPersonIDs, string CustomerID, string ShareRemark)
        {
            string returnValue = "";
            if (CustomerID.Length > 0 && SysPersonIDs.Length > 0)
            {
                string[] ids = SysPersonIDs.Split(new[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                string CreatedBy = GetCurrentPersonID();
                int flag = 0;
                string[] ctms = CustomerID.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < ctms.Length; j++)
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        string[] arr = ids[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (arr.Length > 1)
                        {
                            int authority = 0;
                            int.TryParse(arr[1], out authority);
                            if (authority < 1)
                            {
                                continue;
                            }
                            TB_CustomerShare entity = new TB_CustomerShare();
                            entity.CreatedBy = CreatedBy;
                            entity.CreatedTime = DateTime.Now;
                            entity.CustomerID = ctms[j];
                            entity.CustomerShareID = Result.GetNewId();
                            entity.IsDelete = false;
                            entity.LastUpdatedBy = CreatedBy;
                            entity.LastUpdatedTime = DateTime.Now;
                            entity.SysPersonID = arr[0];
                            entity.Authority = authority;
                            entity.VersionNo = 1;
                            entity.ShareRemark = ShareRemark;
                            if (m_BLL.Create(ref validationErrors, entity))
                            {
                                flag++;
                                //写入日志 
                                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，新增客户分享，分享ID为" + entity.CustomerShareID, "客户管理");
                            }
                        }
                    }
                }

                if (validationErrors != null && validationErrors.Count > 0)
                {
                    validationErrors.All(a =>
                    {
                        returnValue += a.ErrorMessage;
                        return true;
                    });
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，新增客户分享，" + returnValue, "客户管理");//写入日志          
                }
                if (flag == (ids.Length * ctms.Length))
                {
                    return Json("OK");
                }
                else if (flag > 0)
                {
                    return Json(Suggestion.InsertFail + "部分分享成功，" + returnValue); //提示插入失败
                }
                else
                {
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 取消客户分享 create by chand 2015-07-01

        public JsonResult CancelShare(string ID)
        {
            string returnValue = "";
            if (ID.Length > 0)
            {
                TB_CustomerShare entity = m_BLL.GetById(ID);
                if (entity != null)
                {
                    entity.IsDelete = true;
                    entity.LastUpdatedBy = GetCurrentPersonID();
                    entity.LastUpdatedTime = DateTime.Now;
                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        //写入日志 
                        LogClassModels.WriteServiceLog(Suggestion.DeleteSucceed + "，取消客户分享，取消ID为" + ID + ",客户编号：" + entity.CustomerID, "客户管理");
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
                            LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，取消客户分享，" + returnValue, "客户管理");//写入日志          
                        }
                        return Json(Suggestion.DeleteFail + returnValue); //提示插入失败
                    }
                }
            }

            return Json(Suggestion.DeleteFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 改变分享权限

        public JsonResult GetChangeAuthority(string CustomerShareID, int Authority)
        {
            string returnValue = "";
            if (CustomerShareID.Length > 0)
            {
                TB_CustomerShare entity = m_BLL.GetById(CustomerShareID);
                if (entity != null)
                {
                    entity.Authority = Authority;
                    entity.IsDelete = false;
                    entity.LastUpdatedBy = GetCurrentPersonID();
                    entity.LastUpdatedTime = DateTime.Now;
                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        //写入日志 
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，权限修改" + CustomerShareID, "客户管理");
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
                            LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，取消客户分享，" + returnValue, "客户管理");//写入日志          
                        }
                        return Json(Suggestion.DeleteFail + returnValue); //提示插入失败
                    }
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion
        
        #region 查询客户归属记录 create by chand 2016-1-19

        public JsonResult GetCustomerShareData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (!string.IsNullOrWhiteSpace(id))
            {
                search = search + "CustomerID&" + id + '^';
            }
            else
            {
                search = search + "SysPersonIDFrom&" + GetCurrentPersonID() + '^';
            }

            List<CustomerShareEntity> queryData = m_BLL.GetCustomerShareData(search, page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CustomerID,
                    s.CustomerName,
                    s.CustomerShareLogID,
                    s.LastUpdatedTime,
                    s.ShareRemark,
                    s.SysPersonIDFrom,
                    s.SysPersonIDTo,
                }
                   )
            });
        }

        public JsonResult GetShareInData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            search = search + "SysPersonIDTo&" + GetCurrentPersonID() + '^';

            List<CustomerShareEntity> queryData = m_BLL.GetCustomerShareData(search, page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CustomerID,
                    s.CustomerName,
                    s.CustomerShareLogID,
                    s.LastUpdatedTime,
                    s.ShareRemark,
                    s.SysPersonIDFrom,
                    s.SysPersonIDTo
                }
                   )
            });
        }
        #endregion
    }
}