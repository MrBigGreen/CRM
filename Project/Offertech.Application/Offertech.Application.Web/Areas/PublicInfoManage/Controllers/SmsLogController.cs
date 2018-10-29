using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.Busines.PublicInfoManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.PublicInfoManage.Controllers
{
    ///<summary>
    ///版本6.1
    ///苏州欧孚科技
    ///创建：chand
    ///日期：2016-12-2611:41
    ///描述：短信记录
    ///</summary>
    public class SmsLogController : MvcControllerBase
    {
        private SmsLogBLL smslogbll = new SmsLogBLL();

        #region 视图功能
        ///<summary>
        ///列表页面
        ///</summary>
        ///<returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        ///<summary>
        ///表单页面
        ///</summary>
        ///<returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        ///<summary>
        ///获取列表
        ///</summary>
        ///<paramname="queryJson">查询参数</param>
        ///<returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = smslogbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        ///<summary>
        ///获取实体
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = smslogbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        ///<summary>
        ///删除数据
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            smslogbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        ///<summary>
        ///保存表单（新增、修改）
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<paramname="entity">实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, SmsLogEntity entity)
        {
            smslogbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
