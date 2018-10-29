using System;
using System.Linq;
using System.Collections.Generic;
using CRM.DAL;
using CRM.BLL;
using Common;
using System.Web.Mvc;
using CRM.App.Models;
using Models;
namespace CRM.App.Controllers
{
    /// <summary>
    /// 数据字典树形结构
    /// </summary>
    public class SysFieldTreeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取树形页面的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTree()
        {
            List<SystemTree> listSystemTree = new List<SystemTree>();
            
            IBLL.ISysFieldBLL db = new SysFieldBLL();
         
            SysFieldTreeNodeCollection tree = new SysFieldTreeNodeCollection();

            var trees = db.GetAll().OrderBy(o => o.Id);
            if (trees != null)
            {
                string parentId = Request["parentid"];//父节点编号
                if (string.IsNullOrWhiteSpace(parentId))
                {
                    tree.Bind(trees, null, ref listSystemTree);
                }
                else
                {
                    tree.Bind(trees, parentId, ref listSystemTree);
                }
            }           
            return Json(listSystemTree, JsonRequestBehavior.AllowGet);
        }
    }
}


