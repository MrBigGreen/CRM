using Common;
using CRM.BLL;
using CRM.DAL;
using CRM.IBLL;
using Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 薪酬人员管理
    /// 创建人：chand
    /// 创建时间：2016-08-22
    /// 
    /// </summary>
    public class PayEmployeeController : BaseController
    {
        private IBLL.ITPEmployeesBLL m_BLL;

        ValidationErrors validationErrors = new ValidationErrors();
        public PayEmployeeController()
            : this(new TPEmployeesBLL())
        {
        }

        public PayEmployeeController(TPEmployeesBLL bll)
        {
            m_BLL = bll;
        }


        #region 客户列表

        public ActionResult CustomerList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCustomerList(string id, int page, int rows, string order, string sort, string search)
        {
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SearchPersonID&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetPayCustomerList(page, rows, order, sort, search, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }

        #endregion

        #region 员工列表


        public ActionResult EmpList(string id)
        {
            var entity = new TBCustomerBLL().GetById(id);
            if (entity == null)
            {
                return Content("信息未找到！");
            }

            ViewBag.CustomerID = id;
            ViewBag.CustomerName = entity.CustomerName;
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
        public JsonResult GetEmployeesData(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }

            List<TPEmployees> queryData = m_BLL.GetPayEmployeesList(page, rows, order, sort, search, ref total);


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
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult EmpCreate(string customerID)
        {
            if (!string.IsNullOrWhiteSpace(customerID))
            {
                var entity = new TBCustomerBLL().GetById(customerID);
                if (entity == null)
                {
                    return Content("信息未找到！");
                }

                ViewBag.CustomerID = entity.CustomerID;
                ViewBag.CustomerName = entity.CustomerName;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddInfo()
        {
            Account account = GetCurrentAccount();
            if (account != null)
            {
                TP_Employees entity = new TP_Employees();
                entity.EmpID = Result.GetNewId();
                entity.CustomerID = Request.Form["CustomerID"];
                entity.CustomerName = Request.Form["CustomerName"];

                entity.EmpName = Request.Form["EmpName"];
                entity.Gender = Request.Form["Gender"].ToString().ToShort();
                entity.IDType = Request.Form["IDType"];
                entity.IDCard = Request.Form["CardID"];
                entity.BankType = Request.Form["BankName"];
                entity.BankCity = Request.Form["BankCity"];
                entity.BankID = Request.Form["BankID"];
                entity.Nation = Request.Form["Nation"];
                entity.PhoneNumber = Request.Form["PhoneNum"];
                entity.EmailAddress = Request.Form["Email"];
                entity.IDCardPic1 = Request.Form["IDCardPic1"];
                entity.IDCardPic2 = Request.Form["IDCardPic2"];
                entity.IDCardPic3 = Request.Form["IDCardPic3"];

                entity.ContractSubject = Request.Form["ContractType"];
                entity.WagesSubject = Request.Form["WagesType"];
                entity.SocialSecuritySubject = Request.Form["SocialSecurityType"];
                entity.TaxSubject = Request.Form["TaxType"];
                entity.SocialInsurID = Request.Form["SocialInsurID"];
                entity.HousingFundID = Request.Form["HousingFundID"];
                entity.JobStatus = 1;
                entity.IsDelete = false;
                entity.VersionNo = 1;
                entity.CreatedBy = account.Id;
                entity.CreatedTime = DateTime.Now;
                entity.LastUpdatedBy = account.Id;
                entity.LastUpdatedTime = DateTime.Now;


                string returnValue = string.Empty;
                if (m_BLL.Create(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，新增员工：" + entity.EmpName + "，所属客户：" + entity.CustomerName, "薪酬管理");//写入日志 
                    return Json(Suggestion.InsertSucceed);
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，新增员工错误，" + returnValue, "薪酬管理");//写入日志
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }
            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult EmpEdit(string EmpID)
        {
            var model = m_BLL.GetDataEmployeesesInfo(EmpID);
            if (model == null)
            {
                return Content("信息不存在！");
            }
            ViewBag.info = model;
            return View();
        }

        [HttpPost]
        public ActionResult EmpEdit()
        {

            Account account = GetCurrentAccount();

            string EmpID = Request.Form["EmpID"];

            TP_Employees entity = m_BLL.GetById(EmpID);
            if (entity != null)
            {
                entity.EmpName = Request.Form["EmpName"];
                entity.Gender = Request.Form["Gender"].ToString().ToShort();
                entity.IDType = Request.Form["IDType"];
                entity.IDCard = Request.Form["CardID"];
                entity.BankType = Request.Form["BankName"];
                entity.BankCity = Request.Form["BankCity"];
                entity.BankID = Request.Form["BankID"];
                entity.Nation = Request.Form["Nation"];
                entity.PhoneNumber = Request.Form["PhoneNum"];
                entity.EmailAddress = Request.Form["Email"];
                entity.IDCardPic1 = Request.Form["IDCardPic1"];
                entity.IDCardPic2 = Request.Form["IDCardPic2"];
                entity.IDCardPic3 = Request.Form["IDCardPic3"];

                entity.ContractSubject = Request.Form["ContractType"];
                entity.WagesSubject = Request.Form["WagesType"];
                entity.SocialSecuritySubject = Request.Form["SocialSecurityType"];
                entity.TaxSubject = Request.Form["TaxType"];
                entity.SocialInsurID = Request.Form["SocialInsurID"];
                entity.HousingFundID = Request.Form["HousingFundID"];
                entity.JobStatus = Request.Form["JobStatus"].ToShort();
                entity.VersionNo = entity.VersionNo + 1;
                entity.LastUpdatedBy = account.Id;
                entity.LastUpdatedTime = DateTime.Now;


                string returnValue = string.Empty;
                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，新增员工：" + entity.EmpName + "，所属客户：" + entity.CustomerName, "薪酬管理");//写入日志 
                    return Json(Suggestion.UpdateSucceed);
                }
                else
                {
                    if (validationErrors != null && validationErrors.Count > 0)
                    {
                        validationErrors.All(a =>
                        {
                            returnValue += a.ErrorMessage;
                            return true;
                        });
                    }
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，新增员工错误，" + returnValue, "薪酬管理");//写入日志
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }


            }
            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }


        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult EmpView(string EmpID)
        {
            var model = m_BLL.GetDataEmployeesesInfo(EmpID);
            ViewBag.info = model;
            return View();
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
        TBCustomerBLL cBLL = new TBCustomerBLL();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadEmpData()
        {


            int SuccessCount = 0;
            int ErrorCount = 0;
            string toFileFullPath = "";
            string serverFileName = "";
            System.Data.DataTable data = new System.Data.DataTable();
            string error = string.Empty;
            //读取上传文件
            HttpPostedFileBase pstFile = Request.Files["file"];

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
                                        System.Data.DataColumn column = new System.Data.DataColumn(cellValue);
                                        data.Columns.Add(column);
                                        TitleList.Add(cellValue);
                                    }
                                }
                            }
                            //最后一列显示导入结果状态

                            System.Data.DataColumn lastColumn = new System.Data.DataColumn("结果");
                            data.Columns.Add(lastColumn);


                            startRow = sheet.FirstRowNum + 1;

                            #endregion




                            #region 导入内容
                            //获取当前用户ID
                            string currentPerson = GetCurrentPersonID();




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

                                TP_Employees entity = new TP_Employees();
                                entity.EmpID = Result.GetNewId();
                                entity.JobStatus = 1;
                                entity.IsDelete = false;
                                entity.VersionNo = 1;
                                entity.CreatedBy = currentPerson;
                                entity.CreatedTime = DateTime.Now;
                                entity.LastUpdatedBy = currentPerson;
                                entity.LastUpdatedTime = DateTime.Now;


                                System.Data.DataRow dataRow = data.NewRow();

                                //清除项目
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
                                                    //CellValue = row.GetCell(j).CellFormula;

                                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                    {
                                                        DataFormatter formatter = new DataFormatter();
                                                        CellValue = formatter.FormatCellValue(row.GetCell(j)).Trim();
                                                    }
                                                    else
                                                    {
                                                        CellValue = row.GetCell(j).NumericCellValue.ToString().Trim();
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    //数字类型
                                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                    {
                                                        DataFormatter formatter = new DataFormatter();
                                                        CellValue = formatter.FormatCellValue(row.GetCell(j)).Trim();
                                                    }
                                                    else
                                                    {
                                                        CellValue = row.GetCell(j).NumericCellValue.ToString().Trim();
                                                    }
                                                    break;
                                                case CellType.String:
                                                    //字符串
                                                    CellValue = row.GetCell(j).StringCellValue.Trim();
                                                    break;

                                                default:
                                                    break;
                                            }

                                            #endregion

                                            #region 证件号
                                            if (TitleList[j] == "证件号")
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
                                                        entity.IDCard = IDCard;
                                                        if (IDCardHelper.GetSexName(IDCard) == "女")
                                                        {
                                                            entity.Gender = 0;
                                                        }
                                                        else
                                                        {
                                                            entity.Gender = 1;
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region 客户名称
                                            else if (TitleList[j] == "客户名称")
                                            {
                                                if (CustomerEntity == null || !CustomerEntity.CustomerName.Equals(CellValue))
                                                {
                                                    CustomerEntity = cBLL.GetByName(CellValue);
                                                    if (CustomerEntity == null)
                                                    {
                                                        error = "导入失败，客户名称不存在";
                                                        ErrorCount++;
                                                        break;
                                                    }
                                                }
                                                entity.CustomerID = CustomerEntity.CustomerID;
                                                entity.CustomerName = CustomerEntity.CustomerName;

                                            }
                                            else
                                            {
                                                error = SetCustomerValue(entity, TitleList[j], CellValue.Trim());
                                                if (error.Length > 0)
                                                {
                                                    break;
                                                }
                                            }
                                            #endregion

                                            dataRow[j] = CellValue;

                                        }
                                    }
                                    #endregion
                                    if (error == string.Empty)
                                    {
                                        if (m_BLL.CreateOrEdit(ref validationErrors, entity))
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

        ISysFieldHander baseDDL = new SysFieldHander();
        List<SysField> _CompanyList = new List<SysField>();
        List<SysField> _BankList = new List<SysField>();
        public string SetCustomerValue(TP_Employees entity, string title, string cellValue)
        {
            string error = string.Empty;
            if (_CompanyList == null || _CompanyList.Count < 1)
            {
                //合同主体公司（我们所有集团下公司）
                _CompanyList = baseDDL.GetSysFieldByParentId("1604121151490315992e569f90a1e");
            }
            if (_BankList == null || _BankList.Count < 1)
            {
                //银行
                _BankList = baseDDL.GetSysFieldByParentId("1604141504524710001e45c171551");
            }
            switch (title)
            {
                case "员工姓名":
                case "姓名":
                    entity.EmpName = cellValue;
                    return string.Empty;
                case "大合同主体":
                    var c = _CompanyList.Where(s => s.MyTexts.Equals(cellValue)).FirstOrDefault();
                    if (c != null)
                    {
                        entity.ContractSubject = c.Id;
                        return string.Empty;
                    }
                    return "大合同主体信息错误";
                case "工资主体":
                case "工资发放主体":
                    var c2 = _CompanyList.Where(s => s.MyTexts.Equals(cellValue)).FirstOrDefault();
                    if (c2 != null)
                    {
                        entity.WagesSubject = c2.Id;
                        return string.Empty;
                    }
                    return "工资主体信息错误";
                case "社保主体":
                case "社保缴纳主体":
                    var c3 = _CompanyList.Where(s => s.MyTexts.Equals(cellValue)).FirstOrDefault();
                    if (c3 != null)
                    {
                        entity.SocialSecuritySubject = c3.Id;
                        return string.Empty;
                    }
                    return "社保主体信息错误";
                case "个税主体":
                case "个税申报主体":
                    var c4 = _CompanyList.Where(s => s.MyTexts.Equals(cellValue)).FirstOrDefault();
                    if (c4 != null)
                    {
                        entity.TaxSubject = c4.Id;
                        return string.Empty;
                    }
                    return "个税主体信息错误";


                case "状态":
                    //客户级别
                    if (cellValue.Contains("离职"))
                    {
                        entity.JobStatus = 2;
                    }
                    else
                    {
                        entity.JobStatus = 1;
                    }
                    return string.Empty;
                case "证件类型":
                    //企业性质
                    if (cellValue.Contains("身份证"))
                    {
                        entity.IDType = "1604141504209501973a1827b0c60";
                    }
                    else if (cellValue.Contains("护照"))
                    {
                        entity.IDType = "160414150439436254674e55dfb94";
                    }
                    else if (cellValue.Contains("居住证"))
                    {
                        entity.IDType = "16041910534040296928596b02dd9";
                    }
                    else if (cellValue.Contains("签证"))
                    {
                        entity.IDType = "160419105359368053972dd2610cf";
                    }
                    else if (cellValue.Contains("军人证"))
                    {
                        entity.IDType = "1604191054168560542f1fc574840";
                    }
                    else if (cellValue.Contains("港澳通行证"))
                    {
                        entity.IDType = "1604191054553364450ca55c2ca80";
                    }
                    else
                    {
                        entity.IDType = "1604141504209501973a1827b0c60";
                    }
                    break;
                case "银行":
                    var bank = _BankList.Where(s => s.MyTexts.Equals(cellValue)).FirstOrDefault();
                    if (bank != null)
                    {
                        entity.BankType = bank.Id;
                        return string.Empty;
                    }
                    return "银行信息错误";
                case "银行开户市":
                    entity.BankCity = cellValue;
                    return string.Empty;
                case "银行卡号":
                    entity.BankID = cellValue;
                    return string.Empty;
                case "手机号码":
                    entity.PhoneNumber = cellValue;
                    return string.Empty;
                case "社保编号":
                    entity.SocialInsurID = cellValue;
                    return string.Empty;
                case "公积金编号":
                    entity.HousingFundID = cellValue;
                    return string.Empty;
                case "邮箱":
                    entity.EmailAddress = cellValue;
                    return string.Empty;
                case "国籍":
                    entity.Nation = cellValue;
                    return string.Empty;
            }
            return string.Empty;
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
            System.Data.DataTable data = new System.Data.DataTable();
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


                                        System.Data.DataColumn column = new System.Data.DataColumn(cellValue);
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


                                System.Data.DataRow dataRow = data.NewRow();
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

        #endregion
    }
}