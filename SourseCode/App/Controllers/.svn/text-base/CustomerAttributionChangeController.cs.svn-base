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
    /// 创建时间：2015-06-29
    /// 描述说明：客户归属变更-控制器
    /// </summary>
    [SupportFilter]
    public class CustomerAttributionChangeController : BaseController
    {

        #region 初始化

        IBLL.ITBCustomerAttributionChangeBLL m_BLL;

        public CustomerAttributionChangeController()
            : this(new TBCustomerAttributionChangeBLL())
        { }

        public CustomerAttributionChangeController(TBCustomerAttributionChangeBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion

        #region  视图 客户归属记录 create by chand 2015-07-01

        //
        // GET: /CustomerAttributionChange/
        public ActionResult Index(string ID)
        {
            TB_CustomerAttributionChange entity = new TB_CustomerAttributionChange();
            entity.CustomerID = ID;
            return View(entity);
        }
        #endregion


        #region 获取 客户归属数据

        public JsonResult GetData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;

            List<TB_CustomerAttributionChange> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerAttributionChangeID,
                    s.CustomerID,
                    s.EndDate,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.StartDate,
                    s.SysPersonID,
                    s.SysPersonName,
                    s.VersionNo

                }
                   )
            });
        }
        #endregion

        #region 设置 归属 create by chand 2015-06-30

        public JsonResult SetOwnership(string SysPersonID, string CustomerIDs)
        {
            string returnValue = "";
            if (SysPersonID.Length > 0 && CustomerIDs.Length > 0)
            {
                string[] ids = CustomerIDs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string updatedBy = GetCurrentPersonID();
                string CustomerAttributionChangeID = "";
                int flag = 0;
                for (int i = 0; i < ids.Length; i++)
                {
                    CustomerAttributionChangeID = Result.GetNewId();
                    if (m_BLL.UpdateByProc(ref validationErrors, CustomerAttributionChangeID, SysPersonID, ids[i], updatedBy))
                    {
                        flag++;
                        //写入日志 
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，修改客户归属人，客户编号：" + ids[i] + ",归属变更ID为" + CustomerAttributionChangeID, "客户管理");
                    }
                }

                if (validationErrors != null && validationErrors.Count > 0)
                {
                    validationErrors.All(a =>
                    {
                        returnValue += a.ErrorMessage;
                        return true;
                    });
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改客户归属人，" + returnValue, "客户管理");//写入日志          
                }
                if (flag == ids.Length)
                {
                    return Json("OK");
                }
                else if (flag > 0)
                {
                    return Json(Suggestion.UpdateFail + "部分更新成功，" + returnValue); //提示插入失败
                }
                else
                {
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion
    }
}