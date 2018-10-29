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
    /// 菜单
    /// </summary>
    public class SysMenuController : BaseController
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
            List<SysMenu> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    Id = s.Id
                    ,
                    Name = s.Name
                    ,
                    ParentId = s.ParentId
                    ,
                    Url = s.Url
                    ,
                    Iconic = s.Iconic
                    ,
                    Sort = s.Sort
                    ,
                    Remark = s.Remark
                    ,
                    State = s.State
                    ,
                    CreatePerson = s.CreatePerson
                    ,
                    CreateTime = s.CreateTime
                    ,
                    UpdateTime = s.UpdateTime
                    ,
                    UpdatePerson = s.UpdatePerson
                    ,
                    IsLeaf = s.IsLeaf

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
            List<SysMenu> queryData = m_BLL.GetByParam(id, sortOrder, sortName, search);

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
            SysMenu item = m_BLL.GetById(id);
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
        public ActionResult Create(SysMenu entity)
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，菜单的信息的Id为" + entity.Id, "菜单"
                        );//写入日志 
                    App.Codes.MenuCaching.ClearCache();
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，菜单的信息，" + returnValue, "菜单"
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
            SysMenu item = m_BLL.GetById(id);
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
        public ActionResult Edit(string id, SysMenu entity)
        {
            if (entity != null && ModelState.IsValid)
            {   //数据校验

                string currentPerson = GetCurrentPerson();
                entity.UpdateTime = DateTime.Now;
                entity.UpdatePerson = currentPerson;

                string returnValue = string.Empty;
                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，菜单信息的Id为" + id, "菜单"
                        );//写入日志     
                    App.Codes.MenuCaching.ClearCache();
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，菜单信息的Id为" + id + "," + returnValue, "菜单"
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
                    App.Codes.MenuCaching.ClearCache();
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

        IBLL.ISysMenuBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();

        public SysMenuController()
            : this(new SysMenuBLL()) { }

        public SysMenuController(SysMenuBLL bll)
        {
            m_BLL = bll;
        }

        /// <summary>
        /// 获取树形列表的数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllMetadata(string id)
        {
            SysMenuBLL m_BLL = new SysMenuBLL();
            IQueryable<SysMenu> rows = m_BLL.GetAllMetadata(id);
            if (rows.Any())
            {//是否可以省
                return Json(new treegrid
                {
                    rows = rows.Select(s =>
                        new
                        {
                            Id = s.Id
                    ,
                            Name = s.Name
                    ,
                            _parentId = s.ParentId
                    ,
                            state = s.SysMenu1.Any(a => a.ParentId == s.Id) ? "closed" : null
                    ,
                            Url = s.Url
                    ,
                            iconCls = s.Iconic
                    ,
                            Sort = s.Sort
                    ,
                            Remark = s.Remark
                    ,
                            State = s.State
                    ,
                            CreatePerson = s.CreatePerson
                    ,
                            CreateTime = s.CreateTime
                    ,
                            UpdateTime = s.UpdateTime
                    ,
                            UpdatePerson = s.UpdatePerson
                    ,
                            IsLeaf = s.IsLeaf

                        }
                        ).OrderBy(o => o.Sort)
                });
            }
            return Content("[]");
        }


        [HttpPost]
        public ActionResult GetAllMetadata2(string id)
        {
            SysMenuBLL m_BLL = new SysMenuBLL();
            var rows = m_BLL.GetAllMetadata().ToList().Select(s =>
                        new
                        {
                            Id = s.Id,
                            Name = s.Name,
                            _parentId = s.ParentId,
                            isCheck = string.Join(",", s.SysOperation.Select(t => t.Id + "^" + t.Name)),
                            iconCls = s.Iconic,
                            Sort = s.Sort

                        }
                        ).OrderBy(o => o.Sort);

            return Json(new treegrid() { rows = rows });

        }
        /// <summary>
        /// 获取树形列表的数据
        /// </summary>
        /// <returns></returns>

        public ActionResult GetAllMetadata23(string id)
        {
            SysMenuSysRoleSysOperationBLL m_BLL = new SysMenuSysRoleSysOperationBLL();
            var rows = m_BLL.GetByRefSysRoleId(id).Select(s => (s.SysOperationId == null) ? s.SysMenuId : s.SysMenuId + "^" + s.SysOperationId);
            return Json(rows, JsonRequestBehavior.AllowGet);

        }

    }
}


