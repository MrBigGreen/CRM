using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Offer.DAL;

namespace Offer.App.Controllers
{
    public class CallLogController : Controller
    {
        private SysEntities db = new SysEntities();

        // GET: /CallLog/
        public ActionResult Index()
        {
            var tb_calllog = db.TB_CallLog.Include(t => t.SysPerson).Include(t => t.TB_PositionRecommend);
            return View(tb_calllog.ToList());
        }

        // GET: /CallLog/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CallLog tb_calllog = db.TB_CallLog.Find(id);
            if (tb_calllog == null)
            {
                return HttpNotFound();
            }
            return View(tb_calllog);
        }

        // GET: /CallLog/Create
        public ActionResult Create()
        {
            ViewBag.SysPersonID = new SelectList(db.SysPerson, "Id", "Name");
            ViewBag.PositionRecommendID = new SelectList(db.TB_PositionRecommend, "PositionRecommendID", "ImportResumeID");
            return View();
        }

        // POST: /CallLog/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CallLogID,SysPersonID,PositionRecommendID,ImportResumeID,CallTaskID,CallTime,IsConnect,IsSound,JobStatusCode,JobIndustryCode,JobIndustryName,JobPostCode,JobPostName,JobAddressCode,JobAddressName,JobSalaryCode,NowCompanyInfo,Remark,CreatedBy,CreatedTime,LastUpdatedTime,LastUpdatedBy,IsDel,IsSendEmail,IsSendMessage")] TB_CallLog tb_calllog)
        {
            if (ModelState.IsValid)
            {
                db.TB_CallLog.Add(tb_calllog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SysPersonID = new SelectList(db.SysPerson, "Id", "Name", tb_calllog.SysPersonID);
            ViewBag.PositionRecommendID = new SelectList(db.TB_PositionRecommend, "PositionRecommendID", "ImportResumeID", tb_calllog.PositionRecommendID);
            return View(tb_calllog);
        }

        // GET: /CallLog/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CallLog tb_calllog = db.TB_CallLog.Find(id);
            if (tb_calllog == null)
            {
                return HttpNotFound();
            }
            ViewBag.SysPersonID = new SelectList(db.SysPerson, "Id", "Name", tb_calllog.SysPersonID);
            ViewBag.PositionRecommendID = new SelectList(db.TB_PositionRecommend, "PositionRecommendID", "ImportResumeID", tb_calllog.PositionRecommendID);
            return View(tb_calllog);
        }

        // POST: /CallLog/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CallLogID,SysPersonID,PositionRecommendID,ImportResumeID,CallTaskID,CallTime,IsConnect,IsSound,JobStatusCode,JobIndustryCode,JobIndustryName,JobPostCode,JobPostName,JobAddressCode,JobAddressName,JobSalaryCode,NowCompanyInfo,Remark,CreatedBy,CreatedTime,LastUpdatedTime,LastUpdatedBy,IsDel,IsSendEmail,IsSendMessage")] TB_CallLog tb_calllog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_calllog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SysPersonID = new SelectList(db.SysPerson, "Id", "Name", tb_calllog.SysPersonID);
            ViewBag.PositionRecommendID = new SelectList(db.TB_PositionRecommend, "PositionRecommendID", "ImportResumeID", tb_calllog.PositionRecommendID);
            return View(tb_calllog);
        }

        // GET: /CallLog/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_CallLog tb_calllog = db.TB_CallLog.Find(id);
            if (tb_calllog == null)
            {
                return HttpNotFound();
            }
            return View(tb_calllog);
        }

        // POST: /CallLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TB_CallLog tb_calllog = db.TB_CallLog.Find(id);
            db.TB_CallLog.Remove(tb_calllog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
