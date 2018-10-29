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
    /// 数据字典
    /// </summary>
    public class SysFieldController : BaseController
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
            List<SysField> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    Id = s.Id,
                    MyTexts = s.MyTexts,
                    DataValue = s.DataValue,
                    ParentId = s.ParentId,
                    MyTables = s.MyTables,
                    MyColums = s.MyColums,
                    Sort = s.Sort,
                    Remark = s.Remark,
                    CreateTime = s.CreateTime,
                    CreatePerson = s.CreatePerson,
                    UpdateTime = s.UpdateTime,
                    UpdatePerson = s.UpdatePerson
                }

                    )
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
            List<SysField> queryData = m_BLL.GetByParam(id, sortOrder, sortName, search);

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
            SysField item = m_BLL.GetById(id);
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
        public ActionResult Create(SysField entity)
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，数据字典：“" + entity.MyTexts + "”字典Id为：" + entity.Id, "数据字典"
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，数据字典的信息，" + returnValue, "数据字典"
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
            SysField item = m_BLL.GetById(id);
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
        public ActionResult Edit(string id, SysField entity)
        {
            if (entity != null && ModelState.IsValid)
            {   //数据校验

                string currentPerson = GetCurrentPerson();
                entity.UpdateTime = DateTime.Now;
                entity.UpdatePerson = currentPerson;

                string returnValue = string.Empty;
                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，数据字典：“" + entity.MyTexts + "”字典Id为：" + entity.Id, "数据字典"
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，数据字典信息的Id为" + id + "," + returnValue, "数据字典"
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
                    LogClassModels.WriteServiceLog(Suggestion.DeleteSucceed + "，信息的Id为" + string.Join(",", deleteId), "消息"
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
                    LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，信息的Id为" + string.Join(",", deleteId) + "," + returnValue, "消息"
                        );//删除失败，写入日志
                }
            }
            return Json(returnValue);
        }

        IBLL.ISysFieldBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();

        public SysFieldController()
            : this(new SysFieldBLL()) { }

        public SysFieldController(SysFieldBLL bll)
        {
            m_BLL = bll;
        }

        /// <summary>
        /// 获取树形列表的数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllMetadata()
        {
            SysFieldBLL m_BLL = new SysFieldBLL();
            IQueryable<SysField> rows = m_BLL.GetAllMetadata("");
            if (rows.Any())
            {//是否可以省
                return Json(new treegrid
                {
                    rows = rows.Select(s =>
                        new
                        {
                            Id = s.Id
                    ,
                            MyTexts = s.MyTexts,
                            DataValue = s.DataValue
                    ,
                            _parentId = s.ParentId
                    ,
                            state = s.SysField1.Any(a => a.ParentId == s.Id) ? "closed" : null
                    ,
                            MyTables = s.MyTables
                    ,
                            MyColums = s.MyColums
                    ,
                            Sort = s.Sort
                    ,
                            Remark = s.Remark
                    ,
                            CreateTime = s.CreateTime
                    ,
                            CreatePerson = s.CreatePerson
                    ,
                            UpdateTime = s.UpdateTime
                    ,
                            UpdatePerson = s.UpdatePerson

                        }
                        ).OrderByDescending(o => o.Id)
                });
            }
            return Content("[]");
        }


        /// <summary>
        /// 获取树形列表的数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllMetadataChild(string id)
        {
            SysFieldBLL m_BLL = new SysFieldBLL();
            IQueryable<SysField> rows = m_BLL.GetAllMetadata(id);
            if (rows.Any())
            {//是否可以省
                return Json(new treegrid
                {
                    rows = rows.Select(s =>
                        new
                        {
                            Id = s.Id
                    ,
                            MyTexts = s.MyTexts,
                            DataValue = s.DataValue
                    ,
                            _parentId = s.ParentId
                    ,
                            state = s.SysField1.Any(a => a.ParentId == s.Id) ? "closed" : null
                    ,
                            MyTables = s.MyTables
                    ,
                            MyColums = s.MyColums
                    ,
                            Sort = s.Sort
                    ,
                            Remark = s.Remark
                    ,
                            CreateTime = s.CreateTime
                    ,
                            CreatePerson = s.CreatePerson
                    ,
                            UpdateTime = s.UpdateTime
                    ,
                            UpdatePerson = s.UpdatePerson

                        }
                        ).OrderBy(o => o.Id)
                });
            }
            return Content("[]");
        }





        public ActionResult GetComboBoxList(string ParentID)
        {
            return Json(SysFieldModels.GetSysFieldByParentID(ParentID), "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetComboBoxListByNew(string ParentID,string type)
        {
            return Json(SysFieldModels.GetSysFieldByParentIDByNew(ParentID, type, 1), "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}


