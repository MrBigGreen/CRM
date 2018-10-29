using Common;
using Models;
using Offer.BLL;
using Offer.DAL;
using Offer.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offer.App.Controllers
{
    public class CallTaskController : BaseController
    {
        ICallTask m_BLL { get; set; }
        ICallLog LogBLL { get; set; }
        ValidationErrors validationErrors = new ValidationErrors();

        public CallTaskController() : this(new CallTaskBLL(),new CallLogBLL())
        {
        }

        public CallTaskController(CallTaskBLL
            bll,CallLogBLL logbll)
        {
            m_BLL = bll;
            LogBLL = logbll;
        }

        #region 呼叫闹铃
        //
        // GET: /CallTask/
        public ActionResult Index(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        /// <summary>
        /// 异步加载数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetData(string id, int page, int rows)
        {
            int total = 0;
            var queryData = m_BLL.GetByImportResumeID(id,page,rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    CallTaskID = s.CallTaskID,
                    ImportResumeID = s.ImportResumeID,
                    SysPersonID = s.SysPersonID,
                    CallTime = s.CallTime.ToString(),
                    CallStatus = s.CallStatus,
                    Remark = s.Remark,
                    CreatedTime = s.CreatedTime.ToString(),
                    SysPersonName = s.SysPersonName,
                    ResumeName = s.ResumeName,
                    CallStatusName = s.CallStatusName
                }
                )
            });
        }

        //
        // GET: /CallTask/Create
        public ActionResult AddCall(string Id)
        {
            TB_CallTask call = m_BLL.GetById(Id);
            if (call == null)
            {
                call = new TB_CallTask();
            }
            ViewBag.Id = Id;
            return View(call);
        }

        //
        // POST: /CallTask/Create
        [HttpPost]
        public ActionResult AddCall(string id, string CallTime, string remark, string type)
        {
            string returnValue = string.Empty;
            try
            {
                string currentPerson = GetCurrentPersonID();
                TB_CallTask collection = new TB_CallTask();
                if (type == "edit")
                {
                    collection = m_BLL.GetById(id);
                    collection.Remark = remark;
                    collection.CallTime = CallTime.ToDateTime();
                    collection.LastUpdatedTime = DateTime.Now;
                    collection.LastUpdatedBy = currentPerson;
                    if (m_BLL.Edit(ref validationErrors, collection))
                    {
                        return Json(Suggestion.UpdateSucceed);
                    }
                }
                else {
                    collection.CallTime = CallTime.ToDateTime();
                    collection.SysPersonID = GetCurrentAccount().Id;
                    collection.CallTaskID = Result.GetNewId();
                    collection.IsDel = false;
                    collection.CallStatus = 1;
                    collection.CreatedTime = DateTime.Now;
                    collection.LastUpdatedTime = DateTime.Now;
                    collection.LastUpdatedBy = currentPerson;
                    collection.CreatedBy = currentPerson;
                    collection.Remark = remark;
                    collection.ImportResumeID = id;
                    if (m_BLL.Create(ref validationErrors, collection))
                    {
                        return Json(Suggestion.InsertSucceed);
                    }
                }
               
            }
            catch (Exception ex)
            {
                if (validationErrors != null && validationErrors.Count > 0)
                {
                    validationErrors.All(a =>
                    {
                        returnValue += a.ErrorMessage;
                        return true;
                    });
                }
                return Json(returnValue + ex); //提示插入失败
            }
            return Json(Suggestion.OperationFail);
        }

        //
        // POST: /CallTask/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            string returnValue = string.Empty;
            try
            {
                if (m_BLL.Delete(ref validationErrors, id))
                {
                    return Json(Suggestion.DeleteSucceed);
 
                }

            }
            catch (Exception ex)
            {
                if (validationErrors != null && validationErrors.Count > 0)
                {
                    validationErrors.All(a =>
                    {
                        returnValue += a.ErrorMessage;
                        return true;
                    });
                }
                return Json(returnValue + ex);
            }
            return Json(Suggestion.DeleteFail);
        }

        #endregion

        #region 呼叫记录

        public ActionResult CallLog(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        /// <summary>
        /// 异步加载数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCallLog(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            search += "^ImportResumeIDDDL_String&"+id;
            var queryData = LogBLL.GetByParam(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    CallTaskID = s.CallTaskID,
                    IsSound = s.IsSound == true ? "是":"否",
                    IsConnect =s.IsConnect == true ? "是":"否",
                    SysPersonID = s.SysPersonID,
                    CallTime = s.CallTime.ToString(),
                    JobStatusCode = s.JobStatusCode,
                    JobIndustryCode = s.JobIndustryCode,
                    JobPostCode = s.JobPostCode,
                    JobAddressCode = s.JobAddressCode,
                    JobSalaryCode = s.JobSalaryCode,
                    Remark = s.Remark,
                    CreatedTime = s.CreatedTime.ToString(),
                    CreatedBy = s.CreatedBy,
                    JobAddressName=s.JobAddressName.Replace('|',' '),
                    JobIndustryName=s.JobIndustryName,
                    JobPostName=s.JobPostName,
                    JobStatusName=s.JobStatusName,
                    CreatedByName=s.CreatedByName
                }
                )
            });
        }
        #endregion
    }
}
