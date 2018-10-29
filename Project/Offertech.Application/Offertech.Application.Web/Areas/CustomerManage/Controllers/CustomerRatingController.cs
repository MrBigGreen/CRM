using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System.Web.Mvc;
using Offertech.Application.Cache;

namespace Offertech.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:55
    /// 描 述：客户等级评价表
    /// </summary>
    public class CustomerRatingController : MvcControllerBase
    {
        private CustomerRatingBLL customerratingbll = new CustomerRatingBLL();
        private CustomerBLL customerbll = new CustomerBLL();
        private DataItemCache dataItemCache = new DataItemCache();

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
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson,string customerId)
        {
            var watch = CommonHelper.TimerStart();
            var data = customerratingbll.GetPageList(pagination, queryJson, customerId);
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
            var data = customerratingbll.GetList(queryJson);
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
            var data = customerratingbll.GetEntity(keyValue);
            return ToJsonResult(data);
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
            customerratingbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, CustomerRatingEntity entity)
        {
            #region 计算企业信用等级
            int sumScore = 0;
            int score = 0;

            if (int.TryParse(dataItemCache.GetDataItemValue(entity.NatureCode), out score))
            {//企业性质
                sumScore += score;
            }
            if (int.TryParse(dataItemCache.GetDataItemValue(entity.RegisterCapital), out score))
            {//注册资金
                sumScore += score;
            }
            if (int.TryParse(dataItemCache.GetDataItemValue(entity.SalesRevenue), out score))
            {//销售收入
                sumScore += score;
            }
            if (int.TryParse(dataItemCache.GetDataItemValue(entity.ContractPeriod), out score))
            {//合同账期
                sumScore += score;
            }
            if (int.TryParse(dataItemCache.GetDataItemValue(entity.OverdueAdvances), out score))
            {//逾期垫款
                sumScore += score;
            }
            entity.RatingScore = customerbll.GetCalcRatingBefore(sumScore);
            customerbll.UpdateRatingScore(entity.CustomerId, entity.RatingScore);
            #endregion


            customerratingbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
