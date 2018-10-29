using Common;
using CRM.BLL;
using CRM.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using CRM.App.Commons;
using System.ComponentModel.DataAnnotations;
using CRM.IBLL;
using CRM.DAL.CommonEntity;


namespace CRM.App.Controllers
{
    /// <summary>
    /// 名称：薪酬管理
    /// 作者：Jonny
    /// 日期:2016.3.24
    /// </summary>
    public class PayController : BaseController
    {
        private IBLL.ITPEmployeesBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();
        public PayController()
            : this(new TPEmployeesBLL())
        {
        }

        public PayController(TPEmployeesBLL bll)
        {
            m_BLL = bll;
        }


        //
        // GET: /Pay/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeesIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表获取
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }

            List<TPEmployees> queryData = m_BLL.GetDataEmployeesesList(page, rows, order, sort, search, ref total);


            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    EmpID = s.EmpID,
                    EmpName = s.EmpName,
                    CompanyName = s.CustomerName,
                    Birthday = s.Birthday.Value.ToString("yyyy-MM-dd"),
                    BankCity = s.BankCity,
                    BankID = s.BankID,
                    BankType = s.BankType,
                    IDCard = s.IDCard,
                    IDCardPic1 = s.IDCardPic1,
                    IDCardPic2 = s.IDCardPic2,
                    IDCardPic3 = s.IDCardPic3,
                    JobStatus = s.JobStatus == 1 ? "在职" : "离职",
                    CustomerID = s.CustomerID,
                    Gender = s.Gender,
                    PhoneNumber = s.PhoneNumber,
                    ContractSubjectName = s.ContractSubjectName,
                    WagesSubjectName = s.WagesSubjectName,
                    SocialSecuritySubjectName = s.SocialSecuritySubjectName,
                    TaxSubjectName = s.TaxSubjectName
                }

                    )
            });

        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult ShowInfo(string EmpID)
        {
            var model = m_BLL.GetDataEmployeesesInfo(EmpID);
            ViewBag.info = model;
            return View();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult CreateInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddInfo()
        {
            var CompanyName = Request.Form["CompanyName"];
            var EmployessName = Request.Form["EmployessName"];
            var Gender = Request.Form["Gender"];
            var CardType = Request.Form["CardType"];
            var CardID = Request.Form["CardID"];
            var BankName = Request.Form["BankName"];
            var BankCity = Request.Form["BankCity"];
            var BankID = Request.Form["BankID"];
            var Nation = Request.Form["Nation"];
            var PhoneNum = Request.Form["PhoneNum"];
            var Email = Request.Form["Email"];
            var HiddPIC = Request.Form["HiddPIC"];
            var ContractType = Request.Form["ContractType"];
            var WagesType = Request.Form["WagesType"];
            var SocialSecurityType = Request.Form["SocialSecurityType"];
            var TaxType = Request.Form["TaxType"];
            Account account = GetCurrentAccount();
            bool isAdd = m_BLL.AddEmployeeseInfo(CompanyName, EmployessName, Gender, CardType, CardID, BankName,
                BankCity, BankID, Nation, ContractType, WagesType, SocialSecurityType, TaxType, PhoneNum, Email, HiddPIC, account.Id);
            return Json(new { IsSuccess = isAdd });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult UpdateInfo(string EmpID)
        {
            var model = m_BLL.GetDataEmployeesesInfo(EmpID);
            ViewBag.info = model;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateInfo()
        {
            var EmpID = Request.Form["EmpID"];
            // var CompanyID = Request.Form["CompanyID"];
            //var CompanyName = Request.Form["CompanyName"];
            var EmployessName = Request.Form["EmployessName"];
            var Gender = Request.Form["Gender"];
            var CardType = Request.Form["CardType"];
            var CardID = Request.Form["CardID"];
            var BankName = Request.Form["BankName"];
            var BankCity = Request.Form["BankCity"];
            var BankID = Request.Form["BankID"];
            var Nation = Request.Form["Nation"];
            var PhoneNum = Request.Form["PhoneNum"];
            var Email = Request.Form["Email"];
            var HiddPIC = Request.Form["HiddPIC"];
            var JobStatus = Request.Form["JobStatus"];
            var ContractType = Request.Form["ContractType"];
            var WagesType = Request.Form["WagesType"];
            var SocialSecurityType = Request.Form["SocialSecurityType"];
            var TaxType = Request.Form["TaxType"];
            Account account = GetCurrentAccount();
            bool isUpdate = m_BLL.UpdateEmployeeseInfo("", EmpID, "", EmployessName, Gender,
                CardType, CardID, BankName, BankCity, BankID, Nation,
                 ContractType, WagesType, SocialSecurityType, TaxType, PhoneNum, Email, HiddPIC, JobStatus, account.Id);
            return Json(new { IsSuccess = isUpdate });
        }

        /// <summary>
        /// 离职
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FireEmployeese(string parm)
        {
            Account account = GetCurrentAccount();
            bool isFire = m_BLL.FireEmployeeseInfo(parm, account.Id);
            return Json(new { IsSuccess = isFire });
        }

        #region ==导出==

        /// <summary>
        ///  导出Excle  
        /// </summary>
        /// <param name="param"> </param>
        /// <returns></returns>      
        [HttpPost]
        public ActionResult Export(string search)
        {
            string type = "";
            string parm = "";
            if (!string.IsNullOrEmpty(search))
            {
                type = search.Split('$')[0].ToString();
                parm = search.Split('$')[1].ToString();
            }
            string sortName = "";
            string sortOrder = "";
            string[] titles = new string[] { };
            string[] fields = new string[] { };
            titles = new string[] { "员工姓名", "公司名称", "性别", "状态", "证件类型", "证件号", "银行", "银行开户市", "银行卡号", "手机号码", "生日", "邮箱", "国籍" };
            fields = new string[]
            {
                "EmpName", "CompanyName", "GenderName", "JobStatusName", "IDTypeName", "IDCard", "BankTypeName", "BankCity",
                "BankID", "PhoneNumber", "Birthday", "EmailAddress", "Nation"
            };
            sortName = "EmpName";
            sortOrder = "desc";

            Account account = GetCurrentAccount();
            List<TPEmployees> queryData = m_BLL.ExportInfo(account.Id, sortOrder, sortName, parm, type);
            //Salary
            return Content(WriteExcles(titles, fields, queryData.ToArray()));
        }

        #endregion

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="type">0:证件类型；1：银行</param>
        /// <param name="parm"></param>
        /// <returns></returns>
        //[HttpPost]
        public JsonResult GetCombox(string typeID)
        {
            IQueryable jsonInfo = m_BLL.GetCombox(typeID);
            var a = Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);
            return a;

        }

        #region 导入员工信息

        public ActionResult ImportExcel()
        {
            return View();
        }

        public ActionResult DownTemplateFile()
        {
            //Sms.SendMsg("13771942286", "欧孚欧孚真好");
            string filePath = Server.MapPath("~/up/Files/员工信息导入模板.xls");
            DownFile(filePath, "员工信息导入模板.xls");
            return new EmptyResult();

        }

        public ActionResult ImportExcelInfo()
        {
            DataTable data = new DataTable();
            string error = string.Empty;
            HttpPostedFileBase pstFile = Request.Files["file"];
            if (pstFile != null)
            {
                UploadFiles upFiles = new UploadFiles();
                string toFileFullPath = "";
                string serverFileName = "";
                string file = upFiles.FileSaveAs(pstFile, ref toFileFullPath, ref serverFileName);
                if (file != "")
                {

                    IWorkbook workbook = null;
                    FileStream fs = null;
                    string fileName = toFileFullPath;
                    ISheet sheet = null;

                    int startRow = 0;

                    #region 读取Excel 导入到系统中

                    List<TPEmployees> list = new List<TPEmployees>();
                    try
                    {
                        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                            workbook = new XSSFWorkbook(fs);
                        else if (fileName.IndexOf(".xls") > 0) // 2003版本
                            workbook = new HSSFWorkbook(fs);


                        sheet = workbook.GetSheet("Sheet1");
                        if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                        if (sheet != null)
                        {
                            IRow firstRow = sheet.GetRow(0);
                            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                            #region 第一行是否是DataTable的列名

                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue;
                                    if (cellValue != null)
                                    {


                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                    }
                                }
                            }

                            startRow = sheet.FirstRowNum + 1;

                            #endregion

                            #region 导入内容

                            //获取当前用户ID
                            var userID = GetCurrentPersonID();

                            //最后一列的标号
                            int rowCount = sheet.LastRowNum;

                            for (int i = startRow; i <= rowCount; ++i)
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue; //没有数据的行默认是null　　　　　　　

                                if (row.FirstCellNum > 0) continue; //不是从第一个单元格开始
                                //初始客户基本信息
                                TPEmployees entity = new TPEmployees();

                                entity.CreatedBy = userID;
                                entity.LastUpdatedBy = userID;


                                DataRow dataRow = data.NewRow();
                                for (int j = row.FirstCellNum; j < cellCount; ++j)
                                {
                                    if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                        GetEntityValue(j, entity, row);
                                    }
                                }
                                list.Add(entity);

                            }

                            #endregion
                        }
                        var listError = m_BLL.ImportEmployeeses(list);


                        if (listError.Count == 0)
                        {
                            ViewBag.ErrorCount = 0;
                            ViewBag.SuccessCount = list.Count();
                            return View("ExportError", null);
                        }
                        else
                        {
                            string[] titles = new string[] { };
                            string[] fields = new string[] { };
                            titles = new string[]
                            {
                                "员工姓名", "公司名称", "大合同主体","工资发放主体","社保缴纳主体","个税申报主体", "状态", "证件类型", "证件号", "银行", "银行开户市", "银行卡号", "手机号码",  "邮箱", "国籍","错误描述"
                            };
                            fields = new string[]
                            {
                                "EmpName", "CompanyName", "ContractSubjectName","WagesSubjectName","SocialSecuritySubjectName","TaxSubjectName", "JobStatusName", "IDType", "IDCard", "BankType",
                                "BankCity", "BankID", "PhoneNumber",  "EmailAddress", "Nation", "ErrorInfo"
                            };

                            List<TPEmployees> queryData = listError;
                            string url = WriteExcles(titles, fields, queryData.ToArray());
                            ViewBag.Url = url;
                            ViewBag.ErrorCount = listError.Count();
                            return View("ExportError", listError);
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write("导入员工异常1: " + ex.Message);

                    }
                    finally
                    {

                        if (fs != null)
                            fs.Close();
                        fs = null;
                    }

                    #endregion
                }
            }
            ViewBag.ErrorCount = -1;
            return View("ExportError", null);
        }

        private static void GetEntityValue(int j, TPEmployees entity, IRow row)
        {
            switch (j)
            {
                case 0:
                    //员工姓名
                    entity.EmpName = row.GetCell(j).ToString().Trim();
                    entity.EmpID = Result.GetNewId();
                    break;
                case 1:
                    //公司名称
                    entity.CustomerName = row.GetCell(j).ToString().Trim();
                    break;
                case 2:
                    //大合同主体			
                    entity.ContractSubjectName = row.GetCell(j).ToString().Trim();
                    break;
                case 3:
                    //工资发放主体
                    entity.WagesSubjectName = row.GetCell(j).ToString().Trim();
                    break;
                case 4:
                    //社保缴纳主体
                    entity.SocialSecuritySubjectName = row.GetCell(j).ToString().Trim();
                    break;
                case 5:
                    //个税申报主体
                    entity.TaxSubjectName = row.GetCell(j).ToString().Trim();
                    break;
                case 6:
                    //状态
                    entity.JobStatusName = row.GetCell(j).ToString().Trim();
                    break;
                case 7:
                    //证件类型
                    entity.IDType = row.GetCell(j).ToString().Trim();
                    break;
                case 8:
                    //证件号
                    entity.IDCard = row.GetCell(j).ToString().Trim();
                    break;
                case 9:
                    //银行
                    entity.BankType = row.GetCell(j).ToString().Trim();
                    ;
                    break;
                case 10:
                    //银行开户市
                    entity.BankCity = row.GetCell(j).ToString().Trim();
                    break;
                case 11:
                    //银行卡号
                    entity.BankID = row.GetCell(j).ToString().Trim();
                    break;
                case 12:
                    //手机号码
                    entity.PhoneNumber = row.GetCell(j).ToString().Trim();
                    break;
                case 13:
                    //邮箱
                    entity.EmailAddress = row.GetCell(j).ToString().Trim();
                    break;
                case 14:
                    //国籍
                    entity.Nation = row.GetCell(j).ToString().Trim();
                    break;

            }
        }


        public ActionResult ExportError(List<TPEmployees> listError)
        {
            return View();
        }

        #endregion

        #region 导入离职

        public ActionResult FireExcel()
        {
            return View();
        }

        public ActionResult FireExcelInfo()
        {
            DataTable data = new DataTable();
            string error = string.Empty;
            HttpPostedFileBase pstFile = Request.Files["file"];
            if (pstFile != null)
            {
                UploadFiles upFiles = new UploadFiles();
                string toFileFullPath = "";
                string serverFileName = "";
                string file = upFiles.FileSaveAs(pstFile, ref toFileFullPath, ref serverFileName);
                if (file != "")
                {

                    IWorkbook workbook = null;
                    FileStream fs = null;
                    string fileName = toFileFullPath;
                    ISheet sheet = null;

                    int startRow = 0;

                    #region 读取Excel 导入到系统中

                    List<TPEmployees> list = new List<TPEmployees>();
                    try
                    {
                        fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                            workbook = new XSSFWorkbook(fs);
                        else if (fileName.IndexOf(".xls") > 0) // 2003版本
                            workbook = new HSSFWorkbook(fs);


                        sheet = workbook.GetSheet("Sheet1");
                        if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                        if (sheet != null)
                        {
                            IRow firstRow = sheet.GetRow(0);
                            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                            #region 第一行是否是DataTable的列名

                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue;
                                    if (cellValue != null)
                                    {


                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                    }
                                }
                            }

                            startRow = sheet.FirstRowNum + 1;

                            #endregion

                            #region 导入内容

                            //获取当前用户ID
                            var userID = GetCurrentPersonID();

                            //最后一列的标号
                            int rowCount = sheet.LastRowNum;

                            for (int i = startRow; i <= rowCount; ++i)
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue; //没有数据的行默认是null　　　　　　　

                                if (row.FirstCellNum > 0) continue; //不是从第一个单元格开始
                                //初始客户基本信息
                                TPEmployees entity = new TPEmployees();

                                entity.CreatedBy = userID;
                                entity.LastUpdatedBy = userID;


                                DataRow dataRow = data.NewRow();
                                for (int j = row.FirstCellNum; j < cellCount; ++j)
                                {
                                    if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                        GetEntityValue(j, entity, row);
                                    }
                                }
                                list.Add(entity);

                            }

                            #endregion
                        }
                        var listError = m_BLL.ImportEmployeesesByFire(list);

                        if (listError.Count == 0)
                        {
                            ViewBag.ErrorCount = 0;
                            ViewBag.SuccessCount = list.Count();
                            return View("ExportFireError", null);
                        }
                        else
                        {
                            string[] titles = new string[] { };
                            string[] fields = new string[] { };
                            titles = new string[]
                            {
                                "员工姓名", "公司名称", "性别", "状态", "证件类型", "证件号", "银行", "银行开户市", "银行卡号", "手机号码", "生日", "邮箱", "国籍",
                                "错误描述"
                            };
                            fields = new string[]
                            {
                                "EmpName", "CompanyName", "GenderName", "JobStatusName", "IDType", "IDCard", "BankType",
                                "BankCity", "BankID", "PhoneNumber", "Birthday", "EmailAddress", "Nation", "ErrorInfo"
                            };

                            List<TPEmployees> queryData = listError;
                            string url = WriteExcles(titles, fields, queryData.ToArray());
                            ViewBag.Url = url;
                            ViewBag.SuccessCount = list.Count() - listError.Count();
                            ViewBag.ErrorCount = listError.Count();
                            return View("ExportFireError", listError);
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write("离职员工异常1: " + ex.Message);

                    }
                    finally
                    {

                        if (fs != null)
                            fs.Close();
                        fs = null;
                    }

                    #endregion
                }
            }
            ViewBag.ErrorCount = -1;
            return View("ExportError", null);
        }
        public ActionResult ExportFireError(List<TPEmployees> listError)
        {
            return View();
        }

        #endregion






        #region 薪酬管理  create by chand 2016-04-02
        public ActionResult SalaryManage()
        {
            return View();
        }

        /// <summary>
        /// 模板下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DownSalaryTemplate(string type)
        {
            if (type == "KeFu")
            {
                string filePath = Server.MapPath("~/up/Files/薪资导入-客服.xls");
                DownFile(filePath, "薪资导入-客服.xls");
            }
            else if (type == "XinChou")
            {
                string filePath = Server.MapPath("~/up/Files/薪资导入-薪酬.xls");
                DownFile(filePath, "薪资导入-薪酬.xls");
            }
            else if (type == "SheBao")
            {
                string filePath = Server.MapPath("~/up/Files/薪资导入-社保.xls");
                DownFile(filePath, "薪资导入-社保.xls");
            }


            return new EmptyResult();

        }

        public ActionResult ImportSalary()
        {
            return View();
        }







        #region 检查薪资 导入实体

        TB_Customer CustomerEntity = new TB_Customer();
        TBCustomerBLL cBLL = new TBCustomerBLL();
        //获取所有薪资主体
        ISysFieldHander baseDDL = new SysFieldHander();
        List<SysField> _SalarySubjects = null;
        /// <summary>
        /// 银行类型集合
        /// </summary>
        List<SysField> _BankTypes = null;


        #endregion


        /// <summary>
        /// 获取薪资客户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSalaryCustomer()
        {
            TBCustomerBLL bll = new TBCustomerBLL();
            SelectList list = new SelectList(bll.GetSalaryCustomer(), "CustomerID", "CustomerName");

            return Json(list, "text/html", JsonRequestBehavior.AllowGet);
        }

        #region 获取所有薪资月份
        /// <summary>
        /// 获取所有薪资月份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllSalaryMonth()
        {

            //List<SelectListItem> list = new List<SelectListItem>();

            //list.Add(new SelectListItem() { Value = " > ", Text = " > ", Selected = false });
            //list.Add(new SelectListItem() { Value = " < ", Text = " < ", Selected = false });
            //list.Add(new SelectListItem() { Value = " = ", Text = " = ", Selected = false });
            //list.Add(new SelectListItem() { Value = " <> ", Text = " <> ", Selected = false });
            //list.Add(new SelectListItem() { Value = " Like ", Text = " Like ", Selected = false });


            ITPSalaryDetailsBLL m_BLL = new TPSalaryDetailsBLL();
            var list = m_BLL.GetAllSalaryMonth();

            //new SelectList(Enumerable.Range(0, 80).Select(x => new SelectListItem { Value = (DateTime.Now.Year - x).ToString(), Text = (DateTime.Now.Year - x).ToString() })
            //    , "Value", "Text", item.FromYear == null ? string.Empty : item.FromYear)


            SelectList SelectList = new SelectList(list.Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
            return Json(SelectList, "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// 列表获取
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSalaryData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;

            ITPSalaryDetailsBLL m_BLL = new TPSalaryDetailsBLL();
            List<SalaryDetailsEntity> queryData = m_BLL.GetSalaryDetailsData(account.Id, order, sort, search, page, rows, ref total);

            SalaryDetailsEntity totalEntity = new SalaryDetailsEntity();
            totalEntity.EmpName = "合计：";

            #region 最后一行合计

            totalEntity.PayableSalary1 = queryData.Sum(s => s.PayableSalary1);
            totalEntity.IndiPension1 = queryData.Sum(s => s.IndiPension1);
            totalEntity.IndiMedical1 = queryData.Sum(s => s.IndiMedical1);
            totalEntity.IndiJobSecurity1 = queryData.Sum(s => s.IndiJobSecurity1);
            totalEntity.IndiSSTotal1 = queryData.Sum(s => s.IndiSSTotal1);
            totalEntity.IndiFund1 = queryData.Sum(s => s.IndiFund1);
            totalEntity.Tax1 = queryData.Sum(s => s.Tax1);
            totalEntity.AnnualBonus1 = queryData.Sum(s => s.AnnualBonus1);
            totalEntity.AnnualBonusTax1 = queryData.Sum(s => s.AnnualBonusTax1);
            totalEntity.RealSalary1 = queryData.Sum(s => s.RealSalary1);
            totalEntity.CompanyPension1 = queryData.Sum(s => s.CompanyPension1);
            totalEntity.CompanyMedical1 = queryData.Sum(s => s.CompanyMedical1);
            totalEntity.CompanyJobSecurity1 = queryData.Sum(s => s.CompanyJobSecurity1);
            totalEntity.IndusInsurance1 = queryData.Sum(s => s.IndusInsurance1);
            totalEntity.BirthInsurance1 = queryData.Sum(s => s.BirthInsurance1);
            totalEntity.CompanySSTotal1 = queryData.Sum(s => s.CompanySSTotal1);
            totalEntity.PayableSalary2 = queryData.Sum(s => s.PayableSalary2);
            totalEntity.TranSubsidies2 = queryData.Sum(s => s.TranSubsidies2);
            totalEntity.MealAllowance2 = queryData.Sum(s => s.MealAllowance2);
            totalEntity.TravelAllowance2 = queryData.Sum(s => s.TravelAllowance2);
            totalEntity.BackPay2 = queryData.Sum(s => s.BackPay2);
            totalEntity.PayableTotal2 = queryData.Sum(s => s.PayableTotal2);
            totalEntity.IndiFund2 = queryData.Sum(s => s.IndiFund2);
            totalEntity.IndiSSTotal2 = queryData.Sum(s => s.IndiSSTotal2);
            totalEntity.IndiPension2 = queryData.Sum(s => s.IndiPension2);
            totalEntity.IndiMedical2 = queryData.Sum(s => s.IndiMedical2);
            totalEntity.IndiJobSecurity2 = queryData.Sum(s => s.IndiJobSecurity2);
            totalEntity.Other2 = queryData.Sum(s => s.Other2);
            totalEntity.Tax2 = queryData.Sum(s => s.Tax2);
            totalEntity.TaxDeductions2 = queryData.Sum(s => s.TaxDeductions2);
            totalEntity.RealSalary2 = queryData.Sum(s => s.RealSalary2);

            totalEntity.PayBaseBK3 = queryData.Sum(s => s.PayBaseBK3);
            totalEntity.PayBase3 = queryData.Sum(s => s.PayBase3);
            totalEntity.CompanyPension3 = queryData.Sum(s => s.CompanyPension3);
            totalEntity.CompanyMedical3 = queryData.Sum(s => s.CompanyMedical3);
            totalEntity.CompanyJobSecurity3 = queryData.Sum(s => s.CompanyJobSecurity3);
            totalEntity.CompanyInjury3 = queryData.Sum(s => s.CompanyInjury3);
            totalEntity.CompanyBirth3 = queryData.Sum(s => s.CompanyBirth3);
            totalEntity.CompanySSTotal3 = queryData.Sum(s => s.CompanySSTotal3);
            totalEntity.CompanyPensionBK3 = queryData.Sum(s => s.CompanyPensionBK3);
            totalEntity.CompanyMedicalBK3 = queryData.Sum(s => s.CompanyMedicalBK3);
            totalEntity.CompanyJobSecurityBK3 = queryData.Sum(s => s.CompanyJobSecurityBK3);
            totalEntity.IndusInsuranceBK3 = queryData.Sum(s => s.IndusInsuranceBK3);
            totalEntity.BirthInsuranceBK3 = queryData.Sum(s => s.BirthInsuranceBK3);
            totalEntity.CompanySSTotalBK3 = queryData.Sum(s => s.CompanySSTotalBK3);
            totalEntity.IndiPension3 = queryData.Sum(s => s.IndiPension3);
            totalEntity.IndiMedical3 = queryData.Sum(s => s.IndiMedical3);
            totalEntity.IndiJobSecurity3 = queryData.Sum(s => s.IndiJobSecurity3);
            totalEntity.IndiSubtotal3 = queryData.Sum(s => s.IndiSubtotal3);
            totalEntity.IndiPensionBK3 = queryData.Sum(s => s.IndiPensionBK3);
            totalEntity.IndiMedicalBK3 = queryData.Sum(s => s.IndiMedicalBK3);
            totalEntity.IndiJobSecurityBK3 = queryData.Sum(s => s.IndiJobSecurityBK3);
            totalEntity.IndiSubtotalBK3 = queryData.Sum(s => s.IndiSubtotalBK3);
            totalEntity.Total3 = queryData.Sum(s => s.Total3);
            totalEntity.FundAccount3 = queryData.Sum(s => s.FundAccount3);
            totalEntity.IndiFund3 = queryData.Sum(s => s.IndiFund3);
            totalEntity.CompanyFund3 = queryData.Sum(s => s.CompanyFund3);
            totalEntity.FundSubtotal3 = queryData.Sum(s => s.FundSubtotal3);
            totalEntity.PayAmount3 = queryData.Sum(s => s.PayAmount3);
            totalEntity.FundAccountBC3 = queryData.Sum(s => s.FundAccountBC3);
            totalEntity.IndiFundBC3 = queryData.Sum(s => s.IndiFundBC3);
            totalEntity.CompanyFundBC3 = queryData.Sum(s => s.CompanyFundBC3);
            totalEntity.FundSubtotalBC3 = queryData.Sum(s => s.FundSubtotalBC3);


            #endregion

            List<SalaryDetailsEntity> footerList = new List<SalaryDetailsEntity>();
            footerList.Add(totalEntity);
            return Json(new datagrid
            {
                total = total,
                rows = queryData,
                footer = footerList
            });

        }


        /// <summary>
        ///  导出Excle /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="param">Flexigrid传过到后台的参数</param>
        /// <returns></returns>      
        [HttpPost]
        public ActionResult ExportSalaryData(string id, string title, string field, string sortName, string sortOrder, string search)
        {
            string[] titles = title.Split(',');//如果确定显示的名称，可以直接定义
            string[] fields = field.Split(',');
            //List<SysPerson> queryData = m_BLL.GetByParam(id, sortOrder, sortName, search, GetCurrentAccount().SysDepartmentID);


            Account account = GetCurrentAccount();
            int total = 0;

            ITPSalaryDetailsBLL m_BLL = new TPSalaryDetailsBLL();
            List<SalaryDetailsEntity> queryData = m_BLL.GetSalaryDetailsData(account.Id, sortOrder, sortName, search, 1, 50000, ref total);



            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }

        #endregion


        #region 薪资导入  ========================2016-06-30========================

        //客服导入
        public ActionResult ImportKeFu()
        {
            return View();
        }

        //薪酬导入
        public ActionResult ImportXinChou()
        {
            return View();
        }
        //社保导入
        public ActionResult ImportSheBao()
        {
            return View();
        }


        List<TP_SalaryItem> SalaryItemList = new List<TP_SalaryItem>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadSalaryData()
        {


            int iItemClass = 0;
            int SuccessCount = 0;
            int ErrorCount = 0;
            string toFileFullPath = "";
            string serverFileName = "";
            DataTable data = new DataTable();
            string error = string.Empty;
            //读取上传文件
            HttpPostedFileBase pstFile = Request.Files["file"];
            bool Replace = false;
            if (Request.Form["RepeatData"].ToString() == "1")
            {
                Replace = true;
            }

            if (!string.IsNullOrWhiteSpace(Request.Form["ItemClass"]))
            {
                int.TryParse(Request.Form["ItemClass"].ToString(), out iItemClass);
            }

            if (pstFile != null)
            {
                UploadFiles upFiles = new UploadFiles();

                string file = upFiles.FileSaveAs(pstFile, ref toFileFullPath, ref serverFileName);
                if (file != "")
                {

                    IWorkbook workbook = null;
                    FileStream fs = null;
                    string fileName = toFileFullPath;
                    ISheet sheet = null;

                    int startRow = 0;

                    #region 读取Excel 导入到系统中
                    try
                    {
                        fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                        if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                            workbook = new XSSFWorkbook(fs);
                        else if (fileName.IndexOf(".xls") > 0) // 2003版本
                            workbook = new HSSFWorkbook(fs);


                        sheet = workbook.GetSheet("Sheet1");
                        if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                        if (sheet != null)
                        {
                            IRow firstRow = sheet.GetRow(0);
                            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                            //列名的集合
                            List<string> TitleList = new List<string>();
                            //导入薪资Excel的最后一列加上状态
                            ICell resultCell = firstRow.CreateCell(cellCount + 1);
                            resultCell.SetCellValue("导入结果");

                            #region 第一行是否是DataTable的列名

                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue.Trim();
                                    if (cellValue != null)
                                    {
                                        //添加到集合列中
                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                        TitleList.Add(cellValue);
                                    }
                                }
                            }
                            //最后一列显示导入结果状态

                            DataColumn lastColumn = new DataColumn("结果");
                            data.Columns.Add(lastColumn);


                            startRow = sheet.FirstRowNum + 1;

                            #endregion

                            ITPSalaryDetailsBLL salaryBLL = new TPSalaryDetailsBLL();

                            #region 导入内容
                            //获取当前用户ID
                            string currentPerson = GetCurrentPersonID();

                            //获取可以导入的薪资项目
                            ITPSalaryItemBLL itemBll = new TPSalaryItemBLL();
                            SalaryItemList = itemBll.GetSalaryItemByItemClass(iItemClass);


                            //记录薪资项目和对应的值
                            Dictionary<string, string> dicList = new Dictionary<string, string>();


                            //记录最后一次的客户，如果上次导入的客户和本次的客户相同则不需要再查询
                            TB_Customer CustomerEntity = new TB_Customer();


                            #region 循环所有行数
                            //最后一列的标号
                            int rowCount = sheet.LastRowNum;
                            for (int i = startRow; i <= rowCount; ++i)
                            {


                                IRow row = sheet.GetRow(i);
                                if (row == null) continue; //没有数据的行默认是null　　　　　　　

                                if (row.FirstCellNum > 0) continue;//不是从第一个单元格开始

                                DataRow dataRow = data.NewRow();

                                //清除项目
                                dicList.Clear();
                                validationErrors.Clear();
                                error = string.Empty;
                                try
                                {


                                    #region 每行的列

                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                        {
                                            string CellValue = string.Empty;


                                            #region 获取单元格的值
                                            switch (row.GetCell(j).CellType)
                                            {
                                                case CellType.Blank:
                                                case CellType.Error:
                                                case CellType.Unknown:
                                                    break;
                                                case CellType.Boolean:
                                                    CellValue = row.GetCell(j).BooleanCellValue ? "1" : "0";
                                                    break;
                                                case CellType.Formula:
                                                    //单元格函数
                                                    CellValue = row.GetCell(j).CellFormula;

                                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                    {
                                                        DataFormatter formatter = new DataFormatter();
                                                        CellValue = formatter.FormatCellValue(row.GetCell(j));
                                                    }
                                                    else
                                                    {
                                                        CellValue = row.GetCell(j).NumericCellValue.ToString();
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    //数字类型
                                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                    {
                                                        DataFormatter formatter = new DataFormatter();
                                                        CellValue = formatter.FormatCellValue(row.GetCell(j));
                                                    }
                                                    else
                                                    {
                                                        CellValue = row.GetCell(j).NumericCellValue.ToString();
                                                    }
                                                    break;
                                                case CellType.String:
                                                    //字符串
                                                    CellValue = row.GetCell(j).StringCellValue;
                                                    break;

                                                default:
                                                    break;
                                            }

                                            #endregion


                                            dataRow[j] = CellValue;

                                            #region 月份
                                            //设置新建客户值
                                            if (TitleList[j] == "月份")
                                            {
                                                dicList.Add("SalaryMonth", CellValue);
                                            }
                                            #endregion

                                            #region 身份证
                                            else if (TitleList[j] == "身份证")
                                            {
                                                string IDCard = CellValue;
                                                if (string.IsNullOrWhiteSpace(IDCard))
                                                {
                                                    error = "导入失败，证件号码不能为空";
                                                    ErrorCount++;
                                                    break;
                                                }
                                                else
                                                {
                                                    string result = IDCardHelper.Validate(IDCard);
                                                    if (result != string.Empty)
                                                    {
                                                        error = "导入失败，" + result;
                                                        ErrorCount++;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        dicList.Add("IDCard", IDCard);
                                                    }

                                                }

                                            }
                                            #endregion

                                            #region 客户名称
                                            else if (TitleList[j] == "客户名称")
                                            {
                                                if (CustomerEntity != null && CustomerEntity.CustomerName.Equals(CellValue))
                                                {
                                                    dicList.Add("CustomerName", CustomerEntity.CustomerName);
                                                    dicList.Add("CustomerID", CustomerEntity.CustomerID);
                                                }
                                                else
                                                {
                                                    CustomerEntity = cBLL.GetByName(CellValue);
                                                    if (CustomerEntity == null)
                                                    {
                                                        error = "导入失败，客户名称不存在";
                                                        ErrorCount++;
                                                        break;
                                                    }
                                                    dicList.Add("CustomerName", CustomerEntity.CustomerName);
                                                    dicList.Add("CustomerID", CustomerEntity.CustomerID);
                                                }


                                            }
                                            #endregion

                                            else
                                            {
                                                string itemName = TitleList[j];

                                                var item = SalaryItemList.Where(s => s.ItemName == itemName).FirstOrDefault();
                                                if (item != null)
                                                {
                                                    dicList.Add(item.ItemCode, CellValue);
                                                }

                                            }

                                        }
                                    }
                                    #endregion

                                    if (error == string.Empty)
                                    {
                                        if (salaryBLL.InsertSalary(ref validationErrors, dicList, currentPerson, Replace))
                                        {
                                            error = "导入成功";
                                            SuccessCount++;
                                        }
                                        else
                                        {
                                            error = "导入失败";
                                            ErrorCount++;
                                        }

                                        if (validationErrors != null && validationErrors.Count > 0)
                                        {
                                            validationErrors.All(a =>
                                            {
                                                error += a.ErrorMessage;
                                                return true;
                                            });
                                        }

                                    }
                                    else
                                    {
                                        ErrorCount++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error = ex.Message;
                                }
                                dataRow[cellCount] = error;
                                data.Rows.Add(dataRow);

                                ICell cell = row.CreateCell(cellCount + 1);
                                cell.SetCellValue(error);
                            }
                            #endregion

                            #endregion
                        }

                        ViewBag.ResultImportData = data;
                    }
                    catch (Exception ex)
                    {
                        Response.Write("批量导入客户异常1: " + ex.Message);
                        error = "批量导入客户异常: " + ex.Message;
                    }
                    finally
                    {

                        if (fs != null)
                            fs.Close();
                        fs = null;
                    }
                    #endregion

                    try
                    {
                        using (FileStream fileWrite = new FileStream(toFileFullPath, FileMode.Create))
                        {
                            workbook.Write(fileWrite);
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    //DownFile(toFileFullPath, "薪资导入返回结果.xls");
                }
            }
            error = "导入数据不存在，请返回批量导入页面！";
            ViewBag.Error = error;

            ViewBag.Url = serverFileName;
            ViewBag.ErrorCount = ErrorCount;
            ViewBag.SuccessCount = SuccessCount;
            return View("ExportError");
        }



        #endregion

        #region 导入完成通知  ========================2016-07-04========================

        public ActionResult ImportSuccess()
        {
            ITPSalaryDetailsBLL salaryBLL = new TPSalaryDetailsBLL();

            return View();
        }




        #endregion
    }
}