using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.IBLL;
using CRM.BLL;
using CRM.DAL;
using System.Collections;
using CRM.IBLL.Framework;
using CRM.BLL.Framework;
using System.Runtime.Serialization.Json;
using System.IO;

namespace CRM.App.Controllers
{
    [SupportFilter]
    public class HomeController : BaseController
    {


        ValidationErrors validationErrors = new ValidationErrors();


        //
        // GET: /Home/

        public ActionResult Index()
        {
            Account account = GetCurrentAccount();
            if (account == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.LogonNum = account.LogonNum;
                ViewBag.IP = Common.IP.GetIP();
                ViewBag.LastLogonIP = account.LastLogonIP;
                ViewBag.LastLogonTime = account.LastLogonTime;
                ViewBag.PersonName = account.PersonName;

                ViewBag.Strength = account.Strength;

                //IHomeBLL home = new HomeBLL();
                ViewData["Menu"] = App.Codes.MenuCaching.GetMenu(ref account); //home.GetMenuByAccount(ref account);// 获取菜单
                Utils.WriteCookie("account", account, 7);
            }


            return View();
        }
        /// <summary>
        /// 保存皮肤
        /// </summary>
        /// <param name="theme"></param>
        [HttpGet]
        public void SaveTheme(string theme)
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(theme))
            {
                Account account = GetCurrentAccount();
                SysPersonBLL bll = new SysPersonBLL();
                isSuccess = bll.SaveTheme(theme, account.Id);
            }
            if (isSuccess)
            {

            }
        }
        /// <summary>
        /// 获取列表中的按钮导航
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetToolbar(string id)
        {
            if (string.IsNullOrWhiteSpace(id) && id == "undefined")
            {
                return null;
            }
            Account account = GetCurrentAccount();
            if (account == null)
            {
                return Content(" <script type='text/javascript'> window.top.location='Login'; </script>");

            }
            ISysMenuSysRoleSysOperationBLL sro = new SysMenuSysRoleSysOperationBLL();
            List<SysOperation> sysOperations = sro.GetByRefSysMenuIdAndSysRoleId(id, account.RoleIds);
            List<toolbar> toolbars = new List<toolbar>();
            foreach (SysOperation item in sysOperations)
            {
                toolbars.Add(new toolbar() { handler = item.Function, iconCls = item.Iconic, text = item.Name });
            }
            return Json(toolbars, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据父节点获取数据字典,绑定二级下拉框的时候使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSysFieldByParent(string id, string parentid, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            ISysFieldHander baseDDL = new SysFieldHander();
            return Json(new SelectList(baseDDL.GetSysFieldByParent(id, parentid, value), "MyTexts", "MyTexts"), JsonRequestBehavior.AllowGet);

        }

        #region 检查账号是否在别处登录 create by  chand 2015-08-18
        /// <summary>
        /// 检查账号是否在别处登录 create by  chand 2015-08-18
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckOnLine()
        {
            int flag = 1;
            Hashtable hOnline = (Hashtable)HttpContext.Application["Online"];//获取已经存储的application值
            if (hOnline != null)
            {
                Account account = GetCurrentAccount();
                if (!account.Name.Equals("offercom", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        IDictionaryEnumerator idE = hOnline.GetEnumerator();
                        while (idE.MoveNext())
                        {
                            if (idE.Key != null && idE.Key.ToString().Equals(System.Web.HttpContext.Current.Session.SessionID))
                            {
                                //HttpContext.Session.SessionID
                                //already login
                                if (idE.Value != null && "XX".Equals(idE.Value.ToString()))//说明在别处登录
                                {
                                    hOnline.Remove(Session.SessionID);
                                    HttpContext.Application.Lock();
                                    HttpContext.Application["Online"] = hOnline;
                                    HttpContext.Application.UnLock();
                                    flag = 0;
                                    //HttpContext.Response.Write("<script>alert('你的帐号已在别处登陆，你被强迫下线！');window.location.href='login.aspx';</script>");//退出当前到登录页面
                                    //HttpContext.Response.End();
                                    Utils.DeleteCookie("account");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogClassModels.WriteExceptions(ex);
                    }
                }
            }

            return Json(new
            {
                Result = flag,
                SessionID = System.Web.HttpContext.Current.Session.SessionID
            });
        }

        #endregion


        #region 获取列表中的列字段--返回datagrid column

        /// <summary>
        /// 获取列表中的列字段--返回datagrid column
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetColumns(string id)
        {
            if (string.IsNullOrWhiteSpace(id) && id == "undefined")
            {
                return null;
            }
            Account account = GetCurrentAccount();
            if (account == null)
            {
                return Content(" <script type='text/javascript'> window.top.location='Login'; </script>");

            }

            ISysColumnFilterBLL bll = new SysColumnFilterBLL();

            List<CustomColumn> sysColumns = bll.GetCustomColumns(id, account.Id);
            //自己定义显示列的数量
            int CustomShowCount = sysColumns.Where(s => s.CustomShow == 1).Count();

            List<columns> columns = new List<columns>();
            foreach (var item in sysColumns)
            {
                columns col = new columns();
                col.field = item.ColumnName;
                col.title = item.ColumnText;

                col.width = (item.ColumnWidth < 60 ? 95 : item.ColumnWidth);
                if (CustomShowCount > 0)
                {
                    col.hidden = (item.CustomShow == 0);

                }
                else
                {
                    col.hidden = (!item.IsShow);
                }

                columns.Add(col);
            }
            return Json(columns, "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 获取定制列


        /// <summary>
        /// 获取列表中的列字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCustomColumns(string id)
        {
            if (string.IsNullOrWhiteSpace(id) && id == "undefined")
            {
                return null;
            }
            Account account = GetCurrentAccount();
            if (account == null)
            {
                return Content(" <script type='text/javascript'> window.top.location='Login'; </script>");

            }

            ISysColumnFilterBLL bll = new SysColumnFilterBLL();

            List<CustomColumn> sysColumns = bll.GetCustomColumns(id, account.Id);


            return Json(sysColumns, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 保存定制列


        /// <summary>
        /// 保存定制列
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetColumn(string jsonstr)
        {
            int result = 0;
            string msg = string.Empty;

            Account account = GetCurrentAccount();
            if (account == null)
            {
                return Content(" <script type='text/javascript'> window.top.location='Login'; </script>");
            }


            List<SysColumnFilter> entity = Utils.JsonstrToObj<List<SysColumnFilter>>(jsonstr);
            ISysColumnFilterBLL bll = new SysColumnFilterBLL();


            if (bll.Eidt(ref validationErrors, entity, account.Id))
            {
                result = 1;
            }
            else
            {
                if (validationErrors != null && validationErrors.Count > 0)
                {
                    validationErrors.All(a =>
                    {
                        msg += a.ErrorMessage;
                        return true;
                    });
                }
            }

            return Json(
                new
                {
                    Result = result,
                    Msg = msg
                }
                );
        }




        /// <summary>
        /// 保存定制列
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetCustomColumn(string SysMenuID, string sColumnNames)
        {
            int result = 0;
            string msg = string.Empty;
            if (string.IsNullOrWhiteSpace(SysMenuID) || string.IsNullOrWhiteSpace(sColumnNames))
            {
                return Json(
                new
                {
                    Result = result,
                    Msg = "数据不能为空"
                }
                );

            }


            Account account = GetCurrentAccount();
            if (account == null)
            {
                return Content(" <script type='text/javascript'> window.top.location='Login'; </script>");
            }


            ISysColumnFilterBLL bll = new SysColumnFilterBLL();


            if (bll.SetCustomColumn(ref validationErrors, SysMenuID, account.Id, sColumnNames.Split(new char[] { ',', ';', '，' }, StringSplitOptions.RemoveEmptyEntries).ToList()))
            {
                result = 1;
            }

            if (validationErrors != null && validationErrors.Count > 0)
            {
                validationErrors.All(a =>
                {
                    msg += a.ErrorMessage;
                    return true;
                });
            }


            return Json(
                new
                {
                    Result = result,
                    Msg = msg
                }
                );
        }

        #endregion


        #region 根据SysMenuId，获取查询字段

        /// <summary>
        /// 根据SysMenuId，获取查询字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetConditionFields(string id)
        {
            if (string.IsNullOrWhiteSpace(id) && id == "undefined")
            {
                return null;
            }

            ISysColumnFilterBLL bll = new SysColumnFilterBLL();

            List<SysColumnFilter> sysColumns = bll.GetConditionFields(id);

            SelectList list = new SelectList(sysColumns, "ColumnName", "ColumnText");

            return Json(list, "text/html", JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}

