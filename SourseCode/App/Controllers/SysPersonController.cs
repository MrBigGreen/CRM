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
using CRM.IBLL;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 人员
    /// </summary>
    public class SysPersonController : BaseController
    {

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Index(string Data)
        {
            ViewBag.Data = Data;
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
        public JsonResult GetData(string Data, string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            string SysDepartmentID = "";
            if (string.IsNullOrWhiteSpace(Data))
            {
                SysDepartmentID = GetCurrentAccount().SysDepartmentID;
            }
            search = search + "PositionStatus&true^";

            List<SysPerson> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, SysDepartmentID, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    Id = s.Id
                    ,
                    Name = s.Name
                    ,
                    MyName = s.MyName
                    ,
                    Password = s.Password
                    ,
                    SurePassword = s.SurePassword
                    ,
                    Sex = s.Sex
                    ,
                    SysDepartmentId = s.SysDepartmentIdOld
                    ,
                    Position = s.Position
                    ,
                    MobilePhoneNumber = s.MobilePhoneNumber
                    ,
                    CompanyQQ = s.CompanyQQ
                        //,Province = s.Province
                        //,City = s.City
                        //,Village = s.Village
                        //,Address = s.Address
                    ,
                    EnglishName = s.EnglishName
                    ,
                    EmailAddress = s.EmailAddress,
                    SysRoleName = s.SysRoleName

                    ,
                    Remark = s.Remark
                    ,
                    State = s.State
                    ,
                    CreateTime = s.CreateTime
                    ,
                    CreatePerson = s.CreatePerson
                    ,
                    UpdateTime = s.UpdateTime
                    ,
                    LogonNum = s.LogonNum
                    ,
                    LogonTime = s.LogonTime
                    ,
                    LogonIP = s.LogonIP
                    ,
                    LastLogonTime = s.LastLogonTime
                    ,
                    LastLogonIP = s.LastLogonIP
                    ,
                    PageStyle = s.PageStyle
                    ,
                    UpdatePerson = s.UpdatePerson
                    ,
                    Version = s.Version,
                    PositionStatus = s.PositionStatus


                }

                    )
            });
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult LiZhiList(string Data)
        {
            ViewBag.Data = Data;
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
        public JsonResult GetLiZhiData(string Data, string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            string SysDepartmentID = "";
            if (string.IsNullOrWhiteSpace(Data))
            {
                SysDepartmentID = GetCurrentAccount().SysDepartmentID;
            }
            search = search + "PositionStatus&false^";

            List<SysPerson> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, SysDepartmentID, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    Id = s.Id
                    ,
                    Name = s.Name
                    ,
                    MyName = s.MyName
                    ,
                    Password = s.Password
                    ,
                    SurePassword = s.SurePassword
                    ,
                    Sex = s.Sex
                    ,
                    SysDepartmentId = s.SysDepartmentIdOld
                    ,
                    Position = s.Position
                    ,
                    MobilePhoneNumber = s.MobilePhoneNumber
                    ,
                    CompanyQQ = s.CompanyQQ
                        //,Province = s.Province
                        //,City = s.City
                        //,Village = s.Village
                        //,Address = s.Address
                    ,
                    EnglishName = s.EnglishName
                    ,
                    EmailAddress = s.EmailAddress,
                    SysRoleName = s.SysRoleName

                    ,
                    Remark = s.Remark
                    ,
                    State = s.State
                    ,
                    CreateTime = s.CreateTime
                    ,
                    CreatePerson = s.CreatePerson
                    ,
                    UpdateTime = s.UpdateTime
                    ,
                    LogonNum = s.LogonNum
                    ,
                    LogonTime = s.LogonTime
                    ,
                    LogonIP = s.LogonIP
                    ,
                    LastLogonTime = s.LastLogonTime
                    ,
                    LastLogonIP = s.LastLogonIP
                    ,
                    PageStyle = s.PageStyle
                    ,
                    UpdatePerson = s.UpdatePerson
                    ,
                    Version = s.Version,
                    PositionStatus = s.PositionStatus


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
            List<SysPerson> queryData = m_BLL.GetByParam(id, sortOrder, sortName, search, GetCurrentAccount().SysDepartmentID);

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
            SysPerson item = m_BLL.GetById(id);
            return View(item);

        }

        /// <summary>
        /// 首次创建
        /// </summary>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Create(string id)
        {
            SysPerson p = new SysPerson();
            p.PageStyle = "default";
            p.CustomerCeiling = 800;//创建客户上线
            p.Password = "123456";
            p.SurePassword = "123456";
            p.LogonNum = 0;
            return View(p);
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Create(SysPerson entity)
        {
            if (entity != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPerson();
                entity.CreateTime = DateTime.Now;
                entity.CreatePerson = currentPerson;
                entity.IsDelete = false;
                entity.VersionNo = 1;
                entity.Id = Result.GetNewId();
                entity.InitPwd = true;
                entity.PositionStatus = true;
                string returnValue = string.Empty;

                if (m_BLL.Create(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，人员的信息的Id为" + entity.Id, "人员"
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，人员的信息，" + returnValue, "人员"
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
            SysPerson item = m_BLL.GetById(id);
            ViewBag.OldPic = item.HDpic;
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
        public ActionResult Edit(string id, SysPerson entity)
        {
            if (entity != null && ModelState.IsValid)
            {   //数据校验

                string oldPic = Request.Form["OldPic"];
                if (entity.HDpic != oldPic)//修改头像删除老的头像文件
                {
                    DirFile.DeleteFile(oldPic);
                }
                var current = GetCurrentAccount();
                entity.UpdateTime = DateTime.Now;
                entity.UpdatePerson = current.Name;
                entity.VersionNo++;
                string returnValue = string.Empty;

                if (!entity.PositionStatus)
                {


                    IBLL.ITBCustomerShareBLL m_ShareBLL = new TBCustomerShareBLL();
                    if (m_ShareBLL.GetCustomerReleaseAllShare(entity.Id, current.Id))
                    {
                        //写入日志
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，取消所有分享过来的客户，员工编号：" + entity.Id, "客户管理");
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，取消所有分享过来的客户，员工编号:" + entity.Id + "," + returnValue, "客户管理"
                            );//写入日志                           
                        return Json(Suggestion.UpdateFail + returnValue); //提示更新失败
                    }
                }

                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，人员信息的Id为" + id, "人员"
                        );//写入日志    
                    App.Codes.MenuCaching.ClearCache(id);
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，人员信息的Id为" + id + "," + returnValue, "人员"
                        );//写入日志                           
                    return Json(Suggestion.UpdateFail + returnValue); //提示更新失败
                }
            }
            return Json(Suggestion.UpdateFail + "请核对输入的数据的格式"); //提示输入的数据的格式不对               

        }


        /// <summary>
        /// 首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns> 
        [SupportFilter]
        public ActionResult MyEdit()
        {
            SysPerson item = m_BLL.GetById(GetCurrentPersonID());
            ViewBag.OldPic = item.HDpic;
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
        public ActionResult MyEdit(string id, SysPerson entity)
        {
            if (entity != null && ModelState.IsValid)
            {   //数据校验
                SysPerson model = m_BLL.GetById(entity.Id);
                if (model != null)
                {

                    string oldPic = Request.Form["OldPic"];
                    if (entity.HDpic != oldPic)//修改头像删除老的头像文件
                    {
                        DirFile.DeleteFile(oldPic);
                        model.HDpic = entity.HDpic;
                    }

                    model.MyName = entity.MyName;
                    model.Sex = entity.Sex;
                    model.SysDepartmentId = entity.SysDepartmentId;
                    model.EnglishName = entity.EnglishName;
                    model.CompanyQQ = entity.CompanyQQ;
                    model.MobilePhoneNumber = entity.MobilePhoneNumber;
                    model.Telephone = entity.Telephone;
                    model.TelephoneExt = entity.TelephoneExt;
                    model.EmailAddress = entity.EmailAddress;
                    model.Remark = entity.Remark;

                    string currentPerson = GetCurrentPerson();
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = currentPerson;

                    string returnValue = string.Empty;
                    if (m_BLL.Edit(ref validationErrors, model))
                    {
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，人员信息的Id为" + id, "人员"
                            );//写入日志    
                        App.Codes.MenuCaching.ClearCache(id);
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
                        LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，人员信息的Id为" + id + "," + returnValue, "人员"
                            );//写入日志                           
                        return Json(Suggestion.UpdateFail + returnValue); //提示更新失败
                    }
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
                    App.Codes.MenuCaching.ClearCache(deleteId);
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

        IBLL.ISysPersonBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();

        public SysPersonController()
            : this(new SysPersonBLL()) { }

        public SysPersonController(SysPersonBLL bll)
        {
            m_BLL = bll;
        }

        #region  薪资主体设置 create by chand  2016-04-12

        public ActionResult SalarySubject(string id)
        {
            StringBuilder sb = new StringBuilder();
            ISysFieldHander baseDDL = new SysFieldHander();
            List<SysField> FieldList = baseDDL.GetSysFieldByParentId("1604121151490315992e569f90a1e");
            List<string> list = m_BLL.GetSalarySubjectSysPerson(id);
            for (int i = 0; i < FieldList.Count; i++)
            {
                if (list.Contains(FieldList[i].Id))
                {
                    sb.AppendFormat("<div style=\"margin:10px;\"><label style=\"margin: 0px 10px 0px 0px;\"><input name=\"SalarySubjectID\" id=\"{0}\" type=\"checkbox\" checked='checked'  value=\"{0}\" \"=\"\">{1}</label></div>", FieldList[i].Id, FieldList[i].MyTexts);
                }
                else
                {
                    sb.AppendFormat("<div style=\"margin:10px;\"><label style=\"margin: 0px 10px 0px 0px;\"><input name=\"SalarySubjectID\" id=\"{0}\" type=\"checkbox\" value=\"{0}\" \"=\"\">{1}</label></div>", FieldList[i].Id, FieldList[i].MyTexts);

                }
            }
            ViewBag.SalarySubject = sb.ToString();
            ViewBag.SysPersonID = id;
            return View();
        }




        public ActionResult SetSalarySubjectSysPerson(string id, string SubjectIDs)
        {
            int result = 0;
            string msg = "";

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(SubjectIDs))
            {

                msg = "数据错误";
            }
            else
            {


                ISysFieldHander baseDDL = new SysFieldHander();
                List<string> list = SubjectIDs.Split(new char[] { ',', ';', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (m_BLL.SetSalarySubjectSysPerson(ref validationErrors, id, list))
                {
                    result = 1;
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            msg += a.ErrorMessage;
                            return true;
                        });
                    }

                }

            }

            return Json(
                new
                {
                    Result = result,
                    Msg = msg

                }
                );
        }



        #endregion


        public ActionResult ListPay()
        {

            return View();
        }

    }
}


