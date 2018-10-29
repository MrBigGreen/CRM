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
using System.Drawing;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 短信发送记录
    /// </summary>
    public class SysMessageController : BaseController
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
            List<SysMessage> queryData = m_BLL.GetByParam(id, page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    Id = s.Id
                    ,
                    Content = s.Content
                    ,
                    SysMessageTempId = s.SysMessageTempIdOld
                    ,
                    MessageType = s.MessageType
                    ,
                    Remark = s.Remark
                    ,
                    State = s.State
                    ,
                    ReadTime = s.ReadTime
                    ,
                    CreateTime = s.CreateTime
                    ,
                    CreatePerson = s.CreatePerson

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
            List<SysMessage> queryData = m_BLL.GetByParam(id, sortOrder, sortName, search);

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
            SysMessage item = m_BLL.GetById(id);
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
        public ActionResult Create(SysMessage entity)
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，短信发送记录的信息的Id为" + entity.Id, "短信发送记录"
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，短信发送记录的信息，" + returnValue, "短信发送记录"
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
            SysMessage item = m_BLL.GetById(id);
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
        public ActionResult Edit(string id, SysMessage entity)
        {
            if (entity != null && ModelState.IsValid)
            {   //数据校验

                string currentPerson = GetCurrentPerson();
                //entity.UpdateTime = DateTime.Now;
                //entity.UpdatePerson = currentPerson;

                string returnValue = string.Empty;
                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，短信发送记录信息的Id为" + id, "短信发送记录"
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，短信发送记录信息的Id为" + id + "," + returnValue, "短信发送记录"
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

        IBLL.ISysMessageBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();

        public SysMessageController()
            : this(new SysMessageBLL()) { }

        public SysMessageController(SysMessageBLL bll)
        {
            m_BLL = bll;
        }


        [HttpPost]
        public JsonResult SendCode(string phone)
        {
            if (!string.IsNullOrWhiteSpace(phone))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1\d{10}$"))
                {
                    string returnValue = string.Empty;
                    //生成随6位机数
                    string code = GetByRndNum(6);
                    //获取模板
                    SysMessageTempBLL tempBll = new SysMessageTempBLL();
                    var tempEntity = tempBll.GetById("1609051617517460387ac4255e267");

                    SysMessage entity = new SysMessage();
                    entity.Content = tempEntity.Content.Replace("${code}", code);
                    entity.CreatePerson = GetCurrentPersonID();
                    entity.CreateTime = DateTime.Now;
                    entity.Id = Result.GetNewId();
                    entity.MessageType = tempEntity.MessageType;
                    entity.SysMessageTempId = tempEntity.Id;
                    entity.Remark = phone + "-" + code;
                    if (m_BLL.Create(ref validationErrors, entity))
                    {
                        Sms.SendMsg(phone, entity.Content);
                        LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，短信发送记录的信息的Id为" + entity.Id, "短信发送记录"
                            );//写入日志 
                        return Json("手机验证码发送成功");
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
                        LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，短信发送记录的信息，" + returnValue, "短信发送记录"
                            );//写入日志                      
                        return Json("手机验证码发送失败"); //提示插入失败
                    }

                }
            }
            return Json("手机号码格式有误"); //提示插入失败
        }





        //取随机产生的认证码(数字)
        public string GetByRndNum(int VcodeNum)
        {

            string VNum = "";

            Random rand = new Random();

            for (int i = 0; i < VcodeNum; i++)
            {
                VNum += VcArray[rand.Next(0, 9)];
            }
            Session["__VCode"] = VNum;
            return VNum;
        }
        private static readonly string[] VcArray = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };


    }



}


