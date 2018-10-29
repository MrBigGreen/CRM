using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.EnterpriseServices;
using System.Configuration;
using Models;
using Common;
using CRM.DAL;
using CRM.BLL;
using CRM.App.Models;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 通知中心
    /// </summary>
    public class SysNoticeController : BaseController
    {

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Index()
        {

            return View();
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexSef()
        {

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
        public JsonResult GetData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            search += "PersonId&" + GetCurrentPersonID() + "^";
            List<SysNoticeEntity> queryData = m_BLL.GetNoticeData(page, rows, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
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
        [SupportFilter]
        [HttpPost]
        public JsonResult GetDataByPersonName()
        {
            //var person = Utils.ReadCookieAsObj("SysPerson");

            var search = "PersonIdDDL_String&" + GetCurrentPersonID() + "^";
            //var search = "PersonIdDDL_String&" + "Admin" + "^";
            int total = 10;
            List<SysNotice> queryData = m_BLL.GetByParam(string.Empty, 1, 10, "desc", "CreateTime", search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }
        /// <summary>
        ///  导出Excle /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="param">Flexigrid传过到后台的参数</param>
        /// <returns></returns>      
        [HttpPost]
        public ActionResult Export(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            List<SysNotice> queryData = m_BLL.GetByParam(id, sortOrder, sortName, search);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        /// <summary>
        /// 查看详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Details(string id)
        {
            SysNotice item = m_BLL.GetById(id);
            if (item.ReadTime == null)
            {
                item.ReadTime = DateTime.Now;
                m_BLL.Edit(ref validationErrors, item);
            }
            return View(item);

        }
        /// <summary>
        /// 查看详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult View(string id)
        {
            SysNotice item = m_BLL.GetById(id);
            if (!item.IsRead)
            {
                item.ReadTime = DateTime.Now;
                item.IsRead = true;
                m_BLL.Edit(ref validationErrors, item);
            }
            return View(item);

        }


        /// <summary>
        /// 首次创建
        /// </summary>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Create(string id)
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
        public ActionResult Create(SysNotice entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPerson();
                entity.CreateTime = DateTime.Now;
                entity.CreatePerson = currentPerson;

                entity.Id = Result.GetNewId();
                string returnValue = string.Empty;
                if (m_BLL.Create(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，通知中心的信息的Id为" + entity.Id, "通知中心"
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，通知中心的信息，" + returnValue, "通知中心"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }
        /// <summary>
        /// 首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns> 
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            SysNotice item = m_BLL.GetById(id);
            return View(item);
        }
        /// <summary>
        /// 提交编辑信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="collection">客户端传回的集合</param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Edit(string id, SysNotice entity)
        {
            if (entity != null && ModelState.IsValid)
            {   //数据校验

                string currentPerson = GetCurrentPerson();
                //entity.UpdateTime = DateTime.Now;
                //entity.UpdatePerson = currentPerson;

                string returnValue = string.Empty;
                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，通知中心信息的Id为" + id, "通知中心"
                        );//写入日志                           
                    return Json(Suggestion.UpdateSucceed); //提示更新成功 
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，通知中心信息的Id为" + id + "," + returnValue, "通知中心"
                        );//写入日志                           
                    return Json(Suggestion.UpdateFail + returnValue); //提示更新失败
                }
            }
            return Json(Suggestion.UpdateFail + "请核对输入的数据的格式"); //提示输入的数据的格式不对               

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>   
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            string returnValue = string.Empty;
            string[] deleteId = collection["query"].GetString().Split(',');
            if (deleteId != null && deleteId.Length > 0)
            {
                if (m_BLL.DeleteCollection(ref validationErrors, deleteId))
                {
                    LogClassModels.WriteServiceLog(Suggestion.DeleteSucceed + "，信息的Id为" + string.Join(",", deleteId), "通知中心"
                        );//删除成功，写入日志
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
                    }
                    LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，信息的Id为" + string.Join(",", deleteId) + "," + returnValue, "通知中心"
                        );//删除失败，写入日志
                }
            }
            return Json(returnValue);
        }

        IBLL.ISysNoticeBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();

        public SysNoticeController()
            : this(new SysNoticeBLL()) { }

        public SysNoticeController(SysNoticeBLL bll)
        {
            m_BLL = bll;
        }


        /// <summary>
        /// 创建通知角色下的所有人员
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public JsonResult CreateByRoleKeFu()
        {
            var current = GetCurrentAccount();

            if (m_BLL.CreateByRole(ref validationErrors, "客服数据已导入完成", "客服数据已导入完成，请尽快处理！", "16070416470403927182f6fd2b743", current.Id))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客服数据已导入完成", "通知中心");//写入日志
                return Json(new
                {
                    Result = 1
                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客服数据已导入完成，" + returnValue, "通知中心");//删除失败，写入日志
                return Json(new
                {
                    Result = 0,
                    Msg = returnValue
                });
            }
        }

        /// <summary>
        /// 创建通知角色下的所有人员
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public JsonResult CreateByRoleXinChou()
        {
            var current = GetCurrentAccount();

            if (m_BLL.CreateByRole(ref validationErrors, "薪酬数据已导入完成", "薪酬数据已导入完成，请尽快处理！", "160704164804723129345fa0919bc", current.Id))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，薪酬数据已导入完成", "通知中心");//写入日志
                return Json(new
                {
                    Result = 1
                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，薪酬数据已导入完成，" + returnValue, "通知中心");//删除失败，写入日志
                return Json(new
                {
                    Result = 0,
                    Msg = returnValue
                });
            }
        }

        /// <summary>
        /// 创建通知角色下的所有人员
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public JsonResult CreateByRoleSheBao()
        {
            var current = GetCurrentAccount();

            if (m_BLL.CreateByRole(ref validationErrors, "社保数据已导入完成", "社保数据已导入完成，请尽快处理！", "16070416500582727490ae6bdfc3b", current.Id))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，社保数据已导入完成", "通知中心");//写入日志
                return Json(new
                {
                    Result = 1
                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，社保数据已导入完成 ，" + returnValue, "通知中心");//删除失败，写入日志
                return Json(new
                {
                    Result = 0,
                    Msg = returnValue
                });
            }
        }

        /// <summary>
        /// 创建通知角色下的所有人员
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public JsonResult CreateByRoleCaiWu()
        {
            var current = GetCurrentAccount();

            if (m_BLL.CreateByRole(ref validationErrors, "财务数据已导入完成", "财务数据已导入完成，请尽快处理！", "16070416462857373625e653967d3", current.Id))
            {
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，财务数据已导入完成", "通知中心");//写入日志
                return Json(new
                {
                    Result = 1
                });
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
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，财务数据已导入完成, " + returnValue, "通知中心");//删除失败，写入日志
                return Json(new
                {
                    Result = 0,
                    Msg = returnValue
                });
            }
        }

        [SupportFilter]
        [HttpPost]
        public JsonResult GetNoticeUnreadData()
        {
            var search = "PersonId&" + GetCurrentPersonID() + "^IsRead&0^";
            int total = 10;
            List<SysNoticeEntity> queryData = m_BLL.GetNoticeData(1, 100, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }
    }
}


