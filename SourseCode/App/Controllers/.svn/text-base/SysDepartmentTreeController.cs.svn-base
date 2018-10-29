using System;
using System.Linq;
using System.Collections.Generic;
using CRM.DAL;
using CRM.BLL;
using Common;
using System.Web.Mvc;
using CRM.App.Models;
using Models;
using CRM.DAL.CommonEntity;
namespace CRM.App.Controllers
{
    /// <summary>
    /// 部门树形结构
    /// </summary>
    public class SysDepartmentTreeController : BaseController
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

            IBLL.ISysDepartmentBLL db = new SysDepartmentBLL();

            SysDepartmentTreeNodeCollection tree = new SysDepartmentTreeNodeCollection();

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


        #region 部门选择器 create by chand 2015-08-20
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Selected()
        {
            return View();
        }


        /// <summary>
        /// 获取树形部门的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyDeptTree()
        {
            List<SystemTree> listSystemTree = new List<SystemTree>();

            IBLL.ISysDepartmentBLL db = new SysDepartmentBLL();

            SysDepartmentTreeNodeCollection tree = new SysDepartmentTreeNodeCollection();
            var account = GetCurrentAccount();
            SysDepartment dept = db.GetById(account.SysDepartmentID);
            if (dept != null)
            {
                SystemTree myTree = new SystemTree() { id = dept.Id.GetString(), text = dept.Name.GetString() };
                listSystemTree.Add(myTree);
                #region 先获取可见部门
                var trees = db.GetAll().Where(s => s.PositionLevel.Length > dept.PositionLevel.Length && s.PositionLevel.Substring(0, dept.PositionLevel.Length) == dept.PositionLevel && !s.Id.Equals("16022320110263086130e957a24c9")).OrderBy(o => o.PositionLevel);
                if (trees != null)
                {
                    tree.Bind(trees, dept.Id, ref myTree.children);
                    myTree.state = "open";//默认是打开还是关闭
                }
                #endregion
            }

            return Json(listSystemTree, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取树形部门人员的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyDeptByPersonTree()
        {
            List<SystemTree> listSystemTree = new List<SystemTree>();

            IBLL.ISysDepartmentBLL db = new SysDepartmentBLL();

            SysDepartmentTreeNodeCollection tree = new SysDepartmentTreeNodeCollection();
            var account = GetCurrentAccount();
            SysDepartment dept = db.GetById(account.SysDepartmentID);
            if (dept != null)
            {
                SystemTree myTree = new SystemTree() { id = dept.Id.GetString(), text = dept.Name.GetString() };
                listSystemTree.Add(myTree);
                #region 先获取可见部门
                var trees = db.GetAll().Where(s => s.PositionLevel.Length > dept.PositionLevel.Length && s.PositionLevel.Substring(0, dept.PositionLevel.Length) == dept.PositionLevel && !s.Id.Equals("16022320110263086130e957a24c9")).OrderBy(o => o.PositionLevel);
                if (trees != null)
                {
                    tree.Bind(trees, dept.Id, ref myTree.children);
                    myTree.state = "open";//默认是打开还是关闭
                }
                #endregion
                //可见的部门人员
                var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID()).OrderByDescending(m=>m.SysPersonName);
                Bind2(PersonList, ref listSystemTree);
            }

            return Json(listSystemTree, JsonRequestBehavior.AllowGet);
        }

        public void Bind2(IEnumerable<PersonEntity> entitys, ref List<SystemTree> myChildren)
        {
            foreach (var item in entitys)
            {
                Bind3(item, ref myChildren);
            }
        }
        public bool Bind3(PersonEntity entity, ref List<SystemTree> myChildren)
        {
            foreach (var item in myChildren)
            {
                if (item.id == entity.SysDepartmentId)
                {
                    SystemTree myTree = new SystemTree() { id = entity.SysPersonID.GetString(), text = entity.SysPersonName.GetString() };

                    //item.children.Add(myTree);
                    item.children.Insert(0, myTree);
                }
                else
                {
                    Bind3(entity, ref item.children);

                }
            }
            return true;
        }
        #endregion
         

    }
}


