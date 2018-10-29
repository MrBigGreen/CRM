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
    public class RPOPerfController : BaseController
    {
        #region 初始化

        IBLL.ITBRPOPerformanceBLL m_BLL;

        public RPOPerfController()
            : this(new TBRPOPerformanceBLL())
        { }

        public RPOPerfController(TBRPOPerformanceBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion

        #region  RPO 项目执行&项目业绩

        public ActionResult Create(string id)
        {
            TB_RPOPerformance entity = new TB_RPOPerformance();
            entity.CustomerProjectID = id;
            entity.CurrentDate = DateTime.Now;


            return View(entity);
        }


        [HttpPost]
        [SupportFilter]
        public ActionResult Create(TB_RPOPerformance model)
        {
            if (model != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();

                TB_RPOPerformance entity1 = new TB_RPOPerformance();
                entity1.RPOPerformanceID = Result.GetNewId();
                entity1.SysPersonID = currentPerson;
                entity1.CustomerProjectID = model.CustomerProjectID;
                entity1.IsEnd = false;

                entity1.CurrentDate = model.CurrentDate;
                entity1.DownLoadQty = model.DownLoadQty;
                entity1.ContactPersonQty = model.ContactPersonQty;
                entity1.AppInterviewQty = model.AppInterviewQty;
                entity1.InterviewQty = model.InterviewQty;
                entity1.OfferQty = model.OfferQty;
                entity1.OnBoardQty = model.OnBoardQty;
                entity1.OverProbationQty = model.OverProbationQty;

                entity1.IsDelete = false;
                entity1.VersionNo = 1;
                entity1.CreatedBy = currentPerson;
                entity1.LastUpdatedBy = currentPerson;
                entity1.CreatedTime = DateTime.Now;
                entity1.LastUpdatedTime = DateTime.Now;


                string returnValue = string.Empty;
                if (m_BLL.Create(ref validationErrors, entity1))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，添加项目执行数，编号：" + entity1.RPOPerformanceID, "项目管理");//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，添加项目执行数，" + returnValue, "项目管理");//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }



        public ActionResult Edit()
        {
            return View();
        }



        public ActionResult List()
        {
            return View();
        }



        public ActionResult EndList()
        {
            return View();
        }


        public ActionResult ExecuteList()
        {
            return View();
        }


        public ActionResult GetData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SearchPersonID&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetSalesPerfData(page, rows, order, sort, search, ref total);

            List<RPOPerformanceEntity> footerList = new List<RPOPerformanceEntity>();
            if (total > 0)
            {
                RPOPerformanceEntity entity = new RPOPerformanceEntity();
                entity.CustomerName = "";
                entity.ProjectName = "合计";
                entity.ContactPersonQty = queryData.Sum(s => s.ContactPersonQty);
                entity.DownLoadQty = queryData.Sum(s => s.DownLoadQty);
                entity.AppInterviewQty = queryData.Sum(s => s.AppInterviewQty);
                entity.InterviewQty = queryData.Sum(s => s.InterviewQty);
                entity.OfferQty = queryData.Sum(s => s.OfferQty);
                entity.OnBoardQty = queryData.Sum(s => s.OnBoardQty);
                entity.OverProbationQty = queryData.Sum(s => s.OverProbationQty);

                footerList.Add(entity);
            }

            return Json(new datagrid
            {
                total = total,
                rows = queryData,
                footer = footerList
            });

        }


        public ActionResult PersonList()
        {
            return View();
        }

        public ActionResult GetRPOPerfPersonData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SearchPersonID&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetRPOPerfPersonData(page, rows, order, sort, search, ref total);

            List<RPOPerformanceEntity> footerList = new List<RPOPerformanceEntity>();
            if (total > 0)
            {
                RPOPerformanceEntity entity = new RPOPerformanceEntity();
                entity.CustomerName = "";
                entity.ProjectName = "合计";
                entity.ContactPersonQty = queryData.Sum(s => s.ContactPersonQty);
                entity.DownLoadQty = queryData.Sum(s => s.DownLoadQty);
                entity.AppInterviewQty = queryData.Sum(s => s.AppInterviewQty);
                entity.InterviewQty = queryData.Sum(s => s.InterviewQty);
                entity.OfferQty = queryData.Sum(s => s.OfferQty);
                entity.OnBoardQty = queryData.Sum(s => s.OnBoardQty);
                entity.OverProbationQty = queryData.Sum(s => s.OverProbationQty);

                footerList.Add(entity);
            }

            return Json(new datagrid
            {
                total = total,
                rows = queryData,
                footer = footerList
            });

        }

        //项目结束数据
        public ActionResult GetEndData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SearchPersonID&" + GetCurrentPersonID() + "^IsEnd&1^";
            var queryData = m_BLL.GetSalesPerfData(page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }


        #region 项目结束 create by chand 2016-07-26

        public JsonResult GetProjectFinish(string ProjectID)
        {
            string returnValue = "";
            if (ProjectID.Length > 0)
            {
                string CurrentPerson = GetCurrentPersonID();
                List<string> prjList = ProjectID.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (m_BLL.GetProjectFinish(ref validationErrors, prjList, CurrentPerson))
                {
                    //写入日志 
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，项目结束：" + ProjectID, "项目管理");

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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，项目结束，" + returnValue, "项目管理");//写入日志          
                    }
                    return Json(Suggestion.UpdateFail + "，项目结束，" + returnValue); //提示输入的数据的格式不对 
                }

            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #endregion


        #region 开票报表



        IBLL.ITBBillingBLL b_BLL = new TBBillingBLL();


        public ActionResult BillingList()
        {
            return View();
        }

        public ActionResult BillingCreate()
        {
            return View();
        }


        public ActionResult BillingEdit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Content("数据不存在");
            }
            var entity = b_BLL.GetById(id);
            if (entity != null)
            {
                return View(entity);
            }
            return Content("数据不存在");
        }


        public ActionResult BillingView(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Content("数据不存在");
            }
            var entity = b_BLL.GetById(id);
            if (entity != null)
            {
                return View(entity);
            }
            return Content("数据不存在");
        }



        [HttpPost]
        [SupportFilter]
        public JsonResult GetBillingCreate(string TheMonth, int TheWeek)
        {
            if (string.IsNullOrWhiteSpace(TheMonth) || TheWeek < 1 || TheWeek > 5)
            {
                return Json(new
                {
                    Result = 0,
                    Msg = "数据格式不正确！"

                });
            }
            DateTime dt = new DateTime();
            if (!DateTime.TryParse(TheMonth, out dt))
            {
                return Json(new
                {
                    Result = 0,
                    Msg = "月份格式不正确！"

                });

            }
            TheMonth = TheMonth.Substring(0, 7);
            if (b_BLL.IsExists(TheMonth, TheWeek))
            {
                return Json(new
                {
                    Result = 0,
                    Msg = "当前月份周次已经添加过，不能重复添加！"

                });
            }
            string currentPerson = GetCurrentPersonID();

            TB_Billing entity = new TB_Billing();
            entity.BillingID = Result.GetNewId();
            entity.SysPersonID = currentPerson;

            entity.TheMonth = TheMonth;
            entity.TheWeek = TheWeek;
            entity.BillingAmountSJ = 0;
            entity.BillingAmountYJ = 0;


            entity.IsDelete = false;
            entity.VersionNo = 1;
            entity.CreatedBy = currentPerson;
            entity.LastUpdatedBy = currentPerson;
            entity.CreatedTime = DateTime.Now;
            entity.LastUpdatedTime = DateTime.Now;


            string returnValue = string.Empty;
            if (b_BLL.Create(ref validationErrors, entity))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，添加开票统计周：" + entity.TheMonth, "项目管理");//写入日志 
                return Json(new
                {
                    Result = 1,
                    Msg = "添加成功！",
                    BillingID = entity.BillingID


                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，添加开票统计周，" + returnValue, "项目管理");//写入日志                      

                return Json(new
                {
                    Result = 0,
                    Msg = "数据出错" + returnValue

                });
            }


        }


        //开票数据
        [HttpPost]
        public ActionResult GetBillingData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            var queryData = b_BLL.GetByParam(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }
        //开票明细
        [HttpPost]
        public ActionResult GetBillingDetails(string id, int Category)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            var queryData = b_BLL.GetDetails(id, Category);

            return Json(new datagrid
            {
                total = queryData.Count,
                rows = queryData
            });

        }
        //修改开票或创建
        [HttpPost]
        [SupportFilter]
        public JsonResult EditDetails(string BillingDetailsID, string BillingID, int Category, string CustomerName, decimal BillingAmount)
        {
            if (string.IsNullOrWhiteSpace(BillingID) || Category < 1 || Category > 2 || string.IsNullOrWhiteSpace(CustomerName))
            {
                return Json(new
                {
                    Result = 0,
                    Msg = "数据格式不正确！"

                });
            }


            string currentPerson = GetCurrentPersonID();

            TB_BillingDetails entity = new TB_BillingDetails();
            entity.BillingID = BillingID;
            entity.BillingDetailsID = BillingDetailsID;
            entity.Category = Category;
            entity.CustomerName = CustomerName;
            entity.BillingAmount = BillingAmount;


            entity.IsDelete = false;
            entity.VersionNo = 1;
            entity.CreatedBy = currentPerson;
            entity.LastUpdatedBy = currentPerson;
            entity.CreatedTime = DateTime.Now;
            entity.LastUpdatedTime = DateTime.Now;


            string returnValue = string.Empty;
            if (b_BLL.AddOrEdit(ref validationErrors, entity))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，添加开票明细：" + entity.CustomerName, "项目管理");//写入日志 
                return Json(new
                {
                    Result = 1,
                    Msg = "添加成功！",
                    BillingID = entity.BillingID


                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，添加开票明细，" + returnValue, "项目管理");//写入日志                      

                return Json(new
                {
                    Result = 0,
                    Msg = "数据出错" + returnValue

                });
            }


        }


        //删除开票信息
        [HttpPost]
        [SupportFilter]
        public JsonResult DeleteDetails(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new
                {
                    Result = 0,
                    Msg = "数据格式不正确！"

                });
            }


            string returnValue = string.Empty;
            var entity = b_BLL.GetDetailsById(id);
            entity.IsDelete = true;

            if (b_BLL.AddOrEdit(ref validationErrors, entity))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，删除开票明细：" + id, "项目管理");//写入日志 
                return Json(new
                {
                    Result = 1,
                    Msg = "删除成功！",
                    BillingID = entity.BillingID


                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，添加开票明细，" + returnValue, "项目管理");//写入日志                      

                return Json(new
                {
                    Result = 0,
                    Msg = "数据出错" + returnValue

                });
            }


        }




        #region  获取所有业绩月份

        public ActionResult GetMonth()
        {
            var list = b_BLL.GetMonths();
            SelectList select = new SelectList(list.Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
            return Json(select, "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion










        #endregion

    }
}