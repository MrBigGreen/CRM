using Common;
using CRM.BLL;
using CRM.DAL.CommonEntity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App.Controllers
{
    public class JobListController : BaseController
    {
        #region 初始化

        IBLL.IJobInfoBLL m_BLL;

        public JobListController()
            : this(new JobInfoBLL())
        { }

        public JobListController(JobInfoBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion
        //
        // GET: /JobList/
        public ActionResult Index()
        {
            return View();
        }

        #region 获取职位数据 create by chand 2015-07-15

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetAllData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;

            List<JobInfoEntity> queryData = m_BLL.GetData(id, page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CompanyID,
                    s.CustomerID,
                    s.WorkAddress,
                    s.CompanyInterviewJobID,
                    s.CompanyName,
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerBelongingID,
                    s.CustomerBelongingName,
                    s.CustomerLevel,
                    s.CustomerLevelText,
                    s.JobCheckStatusCode,
                    s.JobCheckStatusText,
                    s.JobTitle,
                    s.JobTypeCode,
                    s.JobTypeText,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.ReceiveQty,
                    s.RecommendQty,
                    s.WorkExperenceCode,
                    s.WorkExperenceText

                }
                   )
            });
        }
        #endregion



    }
}