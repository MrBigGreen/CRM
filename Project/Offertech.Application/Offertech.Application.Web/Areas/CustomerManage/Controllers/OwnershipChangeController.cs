using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:53
    /// 描 述：客户变更记录
    /// </summary>
    public class OwnershipChangeController : MvcControllerBase
    {
        private OwnershipChangeBLL ownershipchangebll = new OwnershipChangeBLL();
        private CustomerBLL customerbll = new CustomerBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 批量变更归属
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BatchChanaeForm()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(string keyValue, Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = ownershipchangebll.GetPageList(keyValue, pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = ownershipchangebll.GetList(queryJson);
            return ToJsonResult(data);
        }


        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = customerbll.GetEntity(keyValue);
            //var data = ownershipchangebll.GetEntity(keyValue);
            return Json(new { entity.TraceUserName, entity.TraceUserId }, JsonRequestBehavior.AllowGet);
            // return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            ownershipchangebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, OwnershipChangeEntity entity)
        {
            ownershipchangebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存变更归属
        /// </summary>
        /// <param name="keyValue">客户编号</param>
        /// <param name="newUserId">新</param>
        /// <param name="newUserName"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ChangeSaveForm(string keyValue, string newUserId, string newUserName)
        {
            ownershipchangebll.SaveChanae(keyValue, newUserId, newUserName);
            return Success("操作成功。");
        }

        /// <summary>
        /// 批量变更归属
        /// </summary>
        /// <param name="userId">客户编号</param>
        /// <param name="newUserId">新</param>
        /// <param name="newUserName"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchChanae(string userId, string newUserId, string newUserName)
        {
            string error = ownershipchangebll.BatchChanae(userId, newUserId, newUserName);
            if (string.IsNullOrWhiteSpace(error))
            {
                return Success("操作成功。");
            }
            else
            {
                return Error("批量变更归属失败" + error);
            }

        }
        #endregion
    }
}
