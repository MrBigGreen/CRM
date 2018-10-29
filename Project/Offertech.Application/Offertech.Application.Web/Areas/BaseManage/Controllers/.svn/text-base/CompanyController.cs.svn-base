using Offertech.Application.Entity.BaseManage;
using Offertech.Application.Busines.BaseManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using Offertech.Application.Cache;
using System.Linq;
using Offertech.Util.Extension;

namespace Offertech.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-04-19 15:42
    /// 描 述：主体机构单位表
    /// </summary>
    public class CompanyController : MvcControllerBase
    {
        private CompanyBLL companybll = new CompanyBLL();
        private CompanyCache companyCache = new CompanyCache();

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
        /// 公司数据
        /// </summary>
        /// <param name="enCode">分类</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListJson(string enCode, string keyword)
        {
            var data = companyCache.GetList().ToList();

            var expression = LinqExtensions.True<CompanyEntity>();
            if (!string.IsNullOrWhiteSpace(enCode))
            {
                data = data.Where(t => t.EnCode.ToLower() == enCode.ToLower()).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(t => t.FullName.Contains(keyword)).ToList();
            }

            return ToJsonResult(data);
        }
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var data = companyCache.GetList().ToList();
            var expression = LinqExtensions.True<CompanyEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "CompanyId");
            }
            var treeList = new List<TreeEntity>();
            foreach (CompanyEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.CompanyId) == 0 ? false : true;
                tree.id = item.CompanyId;
                tree.text = item.FullName;
                tree.value = item.CompanyId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string condition, string keyword)
        {
            var data = companybll.GetList().ToList();

            if (!string.IsNullOrEmpty(condition) && !string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
                #region 多条件查询
                switch (condition)
                {
                    case "FullName":    //公司名称
                        data = data.TreeWhere(t => t.FullName.Contains(keyword), "CompanyId");
                        break;
                    case "EnCode":      //外文名称
                        data = data.TreeWhere(t => !string.IsNullOrEmpty(t.EnCode) && t.EnCode.Contains(keyword), "CompanyId");
                        break;
                    case "ShortName":   //中文名称
                        data = data.TreeWhere(t => !string.IsNullOrEmpty(t.ShortName) && t.ShortName.Contains(keyword), "CompanyId");
                        break;
                    case "Manager":     //负责人
                        data = data.TreeWhere(t => !string.IsNullOrEmpty(t.Manager) && t.Manager.Contains(keyword), "CompanyId");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            var treeList = new List<TreeGridEntity>();
            foreach (CompanyEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.CompanyId) == 0 ? false : true;
                tree.id = item.CompanyId;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.entityJson = item.ToJson();
                treeList.Add(tree);
            }
            return Content(treeList.TreeJson());
        }
        /// <summary>
        /// 机构实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = companybll.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="organizeName">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = companybll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="enCode">外文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string EnCode, string keyValue)
        {
            bool IsOk = companybll.ExistEnCode(EnCode, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="shortName">中文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistShortName(string ShortName, string keyValue)
        {
            bool IsOk = companybll.ExistShortName(ShortName, keyValue);
            return Content(IsOk.ToString());
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
            companybll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, CompanyEntity entity)
        {
            companybll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
