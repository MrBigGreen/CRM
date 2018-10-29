using Common;
using CRM.BLL;
using CRM.DAL;
using CRM.IBLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App.Controllers
{
    public class CustomerProjectController : BaseController
    {
        #region 初始化

        IBLL.ITBCustomerProjectBLL m_BLL;

        public CustomerProjectController()
            : this(new TBCustomerProjectBLL())
        { }

        public CustomerProjectController(TBCustomerProjectBLL bll)
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
            ViewBag.CustomerID = id;

            return View();
        }


        public ActionResult Create(string id)
        {
            TB_CustomerProject entity = new TB_CustomerProject();
            entity.CustomerID = id;
            entity.ProjectPeopleQty = 1;
            return View(entity);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Create(TB_CustomerProject entity)
        {

            if (entity != null && ModelState.IsValid)
            {
                Account account = GetCurrentAccount();
                entity.CustomerProjectID = Result.GetNewId();
                //待评估
                entity.EvaluationResult = "1605111431371348259613423d3c6";
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，职位名称：" + entity.ProjectName + "，项目ID：" + entity.CustomerProjectID + "，所属客户ID：" + entity.CustomerID, "项目管理");
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建项目（" + entity.ProjectName + "）：" + returnValue, "项目管理");
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }


        #region 项目修改 create by chand 2016-05-11

        [SupportFilter]
        public ActionResult Edit(string ID)
        {
            TB_CustomerProject entity = m_BLL.GetById(ID);
            return View(entity);
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Edit(string ID, TB_CustomerProject model)
        {
            if (model != null && ModelState.IsValid)
            {
                var entity = m_BLL.GetById(model.CustomerProjectID);
                if (entity != null)
                {
                    if (entity.EvaluationResult != "1605111431371348259613423d3c6")
                    {
                        //项目评估完成不能修改
                        return Json(Suggestion.UpdateFail + "，项目已评估完成，不能继续修改"); //提示输入的数据的格式不对 
                    }
                    entity.ProjectName = model.ProjectName;
                    entity.ProjectDesc = model.ProjectDesc;
                    entity.ProjectRequirements = model.ProjectRequirements;
                    entity.ProjectBudget = model.ProjectBudget;
                    entity.ProjectPeopleQty = model.ProjectPeopleQty;
                    entity.ProjectBenefits = model.ProjectBenefits;
                    entity.ProjectAddress = model.ProjectAddress;

                    string currentPerson = GetCurrentPersonID();

                    entity.VersionNo++;
                    entity.LastUpdatedTime = DateTime.Now;
                    entity.LastUpdatedBy = currentPerson;


                    string returnValue = string.Empty;

                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，职位名称：" + entity.ProjectName + "，项目ID：" + entity.CustomerProjectID + "，所属客户ID：" + entity.CustomerID, "项目管理");
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改项目（" + entity.ProjectName + "）：" + returnValue, "项目管理");
                        return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                    }
                }


            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion


        public ActionResult View(string ID)
        {
            TB_CustomerProject entity = m_BLL.GetById(ID);
            return View(entity);
        }

        public ActionResult List()
        {
            return View();
        }

        #region 项目评估 create by chand 2016-05-13

        [SupportFilter]
        public ActionResult Evaluation(string ID)
        {
            TB_CustomerProject entity = m_BLL.GetById(ID);
            return View(entity);
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Evaluation(string ID, TB_CustomerProject model)
        {
            if (model != null && !string.IsNullOrWhiteSpace(model.CustomerProjectID))
            {
                var entity = m_BLL.GetById(model.CustomerProjectID);
                if (entity != null)
                {
                    entity.EvaluationDesc = model.EvaluationDesc;
                    entity.EvaluationResult = model.EvaluationResult;
                    string currentPerson = GetCurrentPersonID();
                    entity.EvaluationPerson = currentPerson;
                    entity.EvaluationTime = DateTime.Now;
                    entity.VersionNo++;
                    entity.LastUpdatedTime = DateTime.Now;
                    entity.LastUpdatedBy = currentPerson;


                    string returnValue = string.Empty;

                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，项目评估：" + entity.ProjectName + "，项目ID：" + entity.CustomerProjectID, "项目管理");
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，项目评估（" + entity.ProjectName + "）：" + returnValue, "项目管理");
                        return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                    }
                }


            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion


        public ActionResult GetProjectData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (!string.IsNullOrWhiteSpace(id))
            {
                search = search + "CustomerID&" + id + '^';
            }
            List<CustomerProjectEntity> queryData = m_BLL.GetProjectData("", page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }

        public ActionResult SelfList()
        {
            return View();
        }

        public ActionResult GetSelfProjectData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (!string.IsNullOrWhiteSpace(id))
            {
                search = search + "CustomerID&" + id + '^';
            }
            search = search + "SysPersonID&" + GetCurrentPersonID() + '^';
            List<CustomerProjectEntity> queryData = m_BLL.GetProjectData("", page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });

        }


        #region 项目分配 create by chand 2015-07-01

        public JsonResult Allocation(string PersonID, string ProjectID)
        {
            string returnValue = "";
            if (ProjectID.Length > 0 && PersonID.Length > 0)
            {
                var personInfo = new SysPersonBLL().GetById(PersonID);
                if (personInfo != null)
                {

                    string CurrentPerson = GetCurrentPersonID();
                    int flag = 0;
                    string[] ctms = ProjectID.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < ctms.Length; j++)
                    {

                        TB_CustomerProject entity = m_BLL.GetById(ctms[j]);
                        if (entity != null)
                        {

                            entity.EvaluationPerson = PersonID;
                            entity.VersionNo++;
                            entity.LastUpdatedBy = CurrentPerson;
                            entity.LastUpdatedTime = DateTime.Now;
                            if (m_BLL.Edit(ref validationErrors, entity))
                            {
                                flag++;
                                //写入日志 
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，项目(" + entity.ProjectName + ")分配给" + personInfo.MyName, "项目管理");
                            }
                            else
                            {
                                LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，项目(" + entity.ProjectName + ")分配给" + personInfo.MyName, "项目管理");
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，项目分配，" + returnValue, "项目管理");//写入日志          
                    }
                    if (flag == ctms.Length)
                    {
                        return Json("OK");
                    }
                    else if (flag > 0)
                    {
                        return Json(Suggestion.UpdateFail + "部分项目分配成功，" + returnValue); //提示插入失败
                    }
                    else
                    {
                        return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                    }
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion



        #region 项目执行 create by chand 2016-07-25

        public JsonResult GetExecute(string PersonID, string ProjectID)
        {
            string returnValue = "";
            if (ProjectID.Length > 0 && PersonID.Length > 0)
            {
                var personInfo = new SysPersonBLL().GetById(PersonID);
                if (personInfo != null)
                {
                    ITBRPOPerformanceBLL RPOBLL = new TBRPOPerformanceBLL();

                    string CurrentPerson = GetCurrentPersonID();
                    int flag = 0;
                    string[] prjs = ProjectID.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < prjs.Length; j++)
                    {
                        var prjEntity = m_BLL.GetById(prjs[j]);
                        if (prjEntity != null)
                        {
                            TB_RPOPerformance entity = new TB_RPOPerformance();
                            entity.RPOPerformanceID = Result.GetNewId();
                            entity.CustomerProjectID = prjs[j];
                            entity.SysPersonID = personInfo.Id;
                            entity.CurrentDate = DateTime.Now;
                            entity.ContactPersonQty = 0;
                            entity.DownLoadQty = 0;
                            entity.InterviewQty = 0;
                            entity.OfferQty = 0;
                            entity.OnBoardQty = 0;
                            entity.AppInterviewQty = 0;
                            entity.OverProbationQty = 0;
                            entity.IsEnd = false;
                            entity.IsDelete = false;
                            entity.VersionNo = 1;
                            entity.CreatedBy = CurrentPerson;
                            entity.LastUpdatedBy = CurrentPerson;
                            entity.CreatedTime = DateTime.Now;
                            entity.LastUpdatedTime = DateTime.Now;

                            if (RPOBLL.Create(ref validationErrors, entity))
                            {
                                //已执行标记
                                prjEntity.EvaluationResult = "160803152105866457578eb28881e";

                                m_BLL.Edit(ref validationErrors, prjEntity);
                                flag++;
                                //写入日志 
                                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，项目执行编号：" + entity.RPOPerformanceID, "项目管理");
                            }
                            else
                            {
                                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，项目执行人：" + personInfo.MyName, "项目管理");
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，项目执行，" + returnValue, "项目管理");//写入日志          
                    }
                    if (flag == prjs.Length)
                    {
                        return Json("OK");
                    }
                    else if (flag > 0)
                    {
                        return Json(Suggestion.InsertSucceed + "部分项目执行成功，" + returnValue); //提示插入失败
                    }
                    else
                    {
                        return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                    }
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

    }
}