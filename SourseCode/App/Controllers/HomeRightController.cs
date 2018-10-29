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
    public class HomeRightController : BaseController
    {
        IBLL.ITBCustomerBLL m_BLL = new TBCustomerBLL();
        //
        // GET: /Home/

        public ActionResult Index()
        {

            Account account = GetCurrentAccount();
            if (account == null)
            {
                return Content(" <script type='text/javascript'> window.top.location='Account'; </script>");

            }

            ISysAnnouncementBLL announcementBLL = new SysAnnouncementBLL();
            SysAnnouncement announcement = announcementBLL.GetTop(1);
            if (announcement != null)
            {
                ViewBag.Title = announcement.Title;
                ViewBag.Announcement = announcement.Message;
            }
            else
            {
                ViewBag.Title = "暂无公告";
                ViewBag.Announcement = "暂无公告";
            }
            #region 加载我的工作台信息
            //SysDepartmentBLL departBLL = new SysDepartmentBLL();
            //SysDepartment departEntity = departBLL.GetById(account.SysDepartmentID);

            //ViewBag.MyWorking = "";


            #endregion

            //长时间未联系客户数量
            long NoContactQty = m_BLL.GetCustomerNoContactQty(account.Id, "", "");
            if (NoContactQty>0)
                ViewBag.NoContactQty = NoContactQty;
            return View();
        }

        public string GetPositionInfo()
        {


            return "";
        }

        #region 获取客户进程统计数据  create by chand 2015-12-08

        /// <summary>
        /// 获取客户进程统计数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFunnelStatistics()
        {

            string personID = GetCurrentPersonID();
            List<CustomerFunnel> queryData = m_BLL.GetFunnelStatistics(personID, personID, "");
            return Json(
                queryData.Select(s => new
                {
                    value = s.Total,
                    name = s.CustomerFunnelName

                }), JsonRequestBehavior.AllowGet
            );
        }
        #endregion
    }
}

