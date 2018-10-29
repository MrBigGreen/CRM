using CRM.BLL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.DAL;
using Common;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 通话记录
    /// By:Jonny 2015.10.26
    /// </summary>
    [SupportFilter]
    public class CallRecordingController : BaseController
    {

        IBLL.IUnCallBLL m_BLL;
        public CallRecordingController()
            : this(new UnCallBLL()) { }
        public CallRecordingController(UnCallBLL bll)
        {
            m_BLL = bll;
        }

        #region 记录
        //
        // GET: /CallRecording/
        public ActionResult Index()
        {
            var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID());
            Account account = GetCurrentAccount();
            int isShow = m_BLL.GetUserRole(account.Id);
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            ViewBag.isShow = isShow;
            return View();
        }

        [HttpPost]
        public JsonResult GetData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;
            string selectParm = Request["state"] == null ? "" : Request["state"].ToString();//查询条件
            List<CallRecordingModel> queryData = m_BLL.GetCallRecordingData(account.Id, page, rows, order, sort, selectParm, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    calldate = s.CallDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    src = s.Src,
                    FcodeUserName = s.FcodeUserName,
                    dst = s.Dst,
                    CompanyName = s.CompanyName,
                    billsec = DateTime.Parse("00:00:00").AddSeconds(s.Billsec).ToString("HH:mm:ss"),
                    disposition = s.Disposition == "ANSWERED" ? "已接听" : "未接听",
                    userField = s.UserField == null ? "" : s.UserField.Split(':')[1].ToString(),
                    uniqueid = s.Uniqueid
                }

                )
            });

        }
        /// <summary>
        /// 播放录音
        /// </summary>
        /// <param name="FileUrl"></param>
        /// <returns></returns>
        public ActionResult ShowCallRecording(string FileUrl)
        {

            ViewBag.FileUrl = FileUrl;
            return View();
        }

        /// <summary>
        /// 编辑与查看
        /// </summary>
        /// <param name="cid">uniqueid</param>
        /// <returns></returns>
        public ActionResult EditCommentWindow(string cid, string url, string Fcode)
        {
            ViewBag.Cid = cid;
            ViewBag.Url = url;
            ViewBag.Fcode = Fcode;
            List<SoundCommentModel> list = m_BLL.GetComment(cid);

            string callProcess = "";
            string goodRemark = "";
            string badRemark = "";
            string improveRemark = "";
            string isUpdate = "0";
            if (list != null && list.Count > 0)
            {
                callProcess = list[0].SoundType;
                goodRemark = list[0].SoundGoodRemark;
                badRemark = list[0].SoundBadRemark;
                improveRemark = list[0].SoundImproveRemark;
                isUpdate = "1";
            }
            ViewBag.CallProcess = callProcess;
            ViewBag.GoodRemark = goodRemark;
            ViewBag.BadRemark = badRemark;
            ViewBag.ImproveRemark = improveRemark;
            ViewBag.IsUpdate = isUpdate;
            Account account = GetCurrentAccount();
            int role = m_BLL.GetUserRole(account.Id);
            ViewBag.IsRole = role;
            return View();
        }
        [HttpPost]
        public ActionResult AddCommentInfo(string callProcess, string goodRemark, string badRemark, string improveRemark, string cid, string url, string fcode, string isupdate)
        {

            Account account = GetCurrentAccount();

            bool isUpdate = m_BLL.AddCommentInfo(account.Id, callProcess, goodRemark, badRemark, improveRemark, cid, url, fcode, isupdate);

            return Json(new { IsSuccess = isUpdate });
        }

        #endregion


        #region 报表

        public ActionResult CallRecordingReports()
        {
            var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID());
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }

            return View();
        }


        [HttpPost]
        public JsonResult GetReportsData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;
            string selectParm = Request["state"] == null ? "" : Request["state"].ToString();//查询条件
            List<CallRecordingModel> queryData = m_BLL.GetReportsData(account.Id, page, rows, order, sort, selectParm, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    src = s.Src,
                    Dial = s.Dial,
                    Connect = s.Connect,
                    Valid = s.Valid,
                    FcodeUserName = s.FcodeUserName,
                    Efficiency = s.Efficiency,
                    CallTotal = s.CallTotals
                }

                )
            });

        }

        [HttpPost]
        public void ExportSound(string search)
        {
            //MemoryStream ms = new MemoryStream();
            //byte[] buffer = null;
            //using (ZipFile file = ZipFile.Create(ms))
            //{
            //    file.BeginUpdate();
            //    List<string> list = m_BLL.GetExportSounds(search);
            //    if (list != null)
            //    {
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            if (!string.IsNullOrEmpty(list[i]))
            //            {
            //                file.Add(list[i]);
            //            }

            //        }
            //    }

            //    file.CommitUpdate();
            //    buffer = new byte[ms.Length];
            //    ms.Position = 0;
            //    ms.Read(buffer, 0, buffer.Length);
            //}
            //Response.AddHeader("content-disposition", "attachment;filename=Test.zip");
            //Response.BinaryWrite(buffer);
            //Response.Flush();
            //Response.End();


         
        }

        /// <summary>
        /// 导出报表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Export(string search)
        {
            Account account = GetCurrentAccount();

            List<CallRecordingModel> queryData = m_BLL.GetReportsExcel(account.Id, search);

            string[] titles = new string[] { };
            string[] fields = new string[] { };

            titles = new string[] { "姓名", "号码", "拨出电话数", "接通电话数", "有效电话数", "接通率", "总时长（分钟）" };
            fields = new string[] { "FcodeUserName", "Src", "Dial", "Connect", "Valid", "Efficiency", "CallTotals" };


            return Content(WriteExcles(titles, fields, queryData.ToArray()));
        }


        #endregion



        #region ==下拉框==
        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCombox(string type)
        {
            IQueryable jsonInfo = m_BLL.GetCombox(type);
            var a = Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);
            return a;

        }

        #endregion
    }
}