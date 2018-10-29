using Common;
using CRM.BLL;
using CRM.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-07-08
    /// 描述说明：公司信息-控制器
    /// </summary>
    [SupportFilter]
    public class CompanyController : BaseController
    {

        #region 初始化

        IBLL.ITBCompanyBLL m_BLL;

        public CompanyController()
            : this(new TBCompanyBLL())
        { }

        public CompanyController(TBCompanyBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();



        #endregion



        //
        // GET: /Company/
        public ActionResult Index()
        {
            return View();
        }

        #region 获取 可见客户数据 create by chand 2015-07-02

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetAllData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;

            List<CompanyModel> queryData = m_BLL.GetData(id, page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.CompanyName,
                    s.CreatedTime,
                    s.CompanyID
                }
                   )
            });
        }
        #endregion
    }
}