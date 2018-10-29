using Common;
using Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offer.App.Controllers
{
    public class MessageLogController : BaseController
    {
        IMessageLog m_BLL { get; set; }

        ValidationErrors validationErrors = new ValidationErrors();

        public MessageLogController()
            : this(new MessageLogBLL())
        {
        }

        public MessageLogController(MessageLogBLL
            bll)
        {
            m_BLL = bll;
        }


        //
        // GET: /MessageLog/
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
        public JsonResult GetData(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            const int PageSize = 10;
            string selectParm = Request["state"] == null ? "" : Request["state"].ToString();//查询条件
            var queryData = m_BLL.GetByParam(id, page, PageSize, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    MsgID = s.MsgID,
                    MsgSender = s.MsgSender,
                    MsgRecipient = s.MsgRecipient,
                    MsgTitle = s.MsgTitle,
                    MsgContent = s.MsgContent,
                    CreatedTime = s.CreatedTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                }
                )
            });
        }


    }
}