using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System.Web.Mvc;

namespace Offertech.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 13:53
    /// �� �����ͻ������¼
    /// </summary>
    public class OwnershipChangeController : MvcControllerBase
    {
        private OwnershipChangeBLL ownershipchangebll = new OwnershipChangeBLL();
        private CustomerBLL customerbll = new CustomerBLL();

        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BatchChanaeForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = ownershipchangebll.GetList(queryJson);
            return ToJsonResult(data);
        }


        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = customerbll.GetEntity(keyValue);
            //var data = ownershipchangebll.GetEntity(keyValue);
            return Json(new { entity.TraceUserName, entity.TraceUserId }, JsonRequestBehavior.AllowGet);
            // return ToJsonResult(data);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            ownershipchangebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, OwnershipChangeEntity entity)
        {
            ownershipchangebll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="keyValue">�ͻ����</param>
        /// <param name="newUserId">��</param>
        /// <param name="newUserName"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult ChangeSaveForm(string keyValue, string newUserId, string newUserName)
        {
            ownershipchangebll.SaveChanae(keyValue, newUserId, newUserName);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="userId">�ͻ����</param>
        /// <param name="newUserId">��</param>
        /// <param name="newUserName"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult BatchChanae(string userId, string newUserId, string newUserName)
        {
            string error = ownershipchangebll.BatchChanae(userId, newUserId, newUserName);
            if (string.IsNullOrWhiteSpace(error))
            {
                return Success("�����ɹ���");
            }
            else
            {
                return Error("�����������ʧ��" + error);
            }

        }
        #endregion
    }
}
