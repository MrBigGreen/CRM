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
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRM.App.Controllers
{
    /// <summary>
    /// Name:合同列表
    /// Author：Jonny
    /// Date:2015-6-17
    /// </summary>
    [SupportFilter]
    public class ContractController : BaseController
    {

        IBLL.ITBContractBLL m_BLL;
        CRM.IBLL.ITBCustomerBLL m_CustomerBLL = new TBCustomerBLL();
        ValidationErrors validationErrors = new ValidationErrors();
        public ContractController()
            : this(new TBContractBLL()) { }
        public ContractController(TBContractBLL bll)
        {
            m_BLL = bll;
        }

        //
        // GET: /Contract/
        public ActionResult Index()
        {
            var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID());
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }



            return View();
        }

        public ActionResult OfferIndex()
        {
            var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID());
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            return View();
        }

        public ActionResult BridgeIndex()
        {
            var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID());
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            return View();
        }



        /// <summary>
        /// 新增合同
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateContractInfo()
        {
            return View();
        }

        /// <summary>
        /// 新增合同
        /// </summary>
        /// <returns></returns>
        public ActionResult OfferCreateContractInfo(string customerId)
        {
            TB_Contract entity = new TB_Contract();
            if (!string.IsNullOrWhiteSpace(customerId))
            {
                TB_Customer customer = m_CustomerBLL.GetById(customerId);
                if (customer != null)
                {
                    entity.CustomerID = customerId;
                    entity.CustomerName = customer.CustomerName;
                    return View(entity);
                }
            }
            return View(entity);
        }

        /// <summary>
        /// 新增合同
        /// </summary>
        /// <returns></returns>
        public ActionResult BridgeCreateContractInfo(string customerId)
        {
            TB_Contract entity = new TB_Contract();
            if (!string.IsNullOrWhiteSpace(customerId))
            {
                TB_Customer customer = m_CustomerBLL.GetById(customerId);
                if (customer != null)
                {

                    entity.CustomerID = customerId;
                    entity.CustomerName = customer.CustomerName;
                    return View(entity);
                }
            }
            return View(entity);
        }


        #region ==下拉框==
        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="type">0:省1:市2:区3:薪酬</param>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCombox(int type, string parm)
        {
            IQueryable jsonInfo = m_BLL.GetCombox(type, parm);
            return Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);

        }

        #endregion



        /// <summary>
        /// 合同签订保存
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="companyCode"></param>
        /// <param name="cityCode"></param>
        /// <param name="cityName"></param>
        /// <param name="packages"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="packageMoney"></param>
        /// <param name="uploadPackage"></param>
        /// <returns></returns>
        public ActionResult AddPackageInfo(string companyName, string companyCode, string cityCode, string cityName, string packages, string beginDate, string endDate, string packageMoney, string uploadPackage)
        {
            Account account = GetCurrentAccount();
            bool isUpdate = m_BLL.AddPackageInfo(companyName, companyCode, cityCode, cityName, packages, beginDate, endDate, packageMoney, uploadPackage, account.Id);
            return Json(new { IsSuccess = isUpdate });
        }
        /// <summary>
        /// Check公司是否已经签合同
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public ActionResult CheckCompanyContract(string companyCode)
        {
            bool isUpdate = m_BLL.CheckCompanyContract(companyCode);
            return Json(new { IsSuccess = isUpdate });
        }

        /// <summary>
        /// 合同列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<ContractEntity> queryData;
            queryData = m_BLL.GetContractData(account.Id, id, order, sort, search, page, rows, "", ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// OFFER合同列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult offerList(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<ContractEntity> queryData;
            queryData = m_BLL.GetContractData(account.Id, id, order, sort, search, page, rows, "offer", ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Bridge合同列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BridgeList(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<ContractEntity> queryData;
            queryData = m_BLL.GetContractData(account.Id, id, order, sort, search, page, rows, "Bridge", ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);

        }

        #region ==导出==
        /// <summary>
        ///  导出Excle /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="param">Flexigrid传过到后台的参数</param>
        /// <returns></returns>      
        [HttpPost]
        public ActionResult Export(string search)
        {
            string[] titles = { "合同编号", "公司名称", "所在城市", "合同金额", "购买服务", "开单人", "生效日期", "截止日期" };
            string[] fields = { "ContractID", "CustomerName", "CityName", "ContractMoney", "PackageName", "CreatedBy", "EffectiveDate", "Deadline" };
            string sortName = "ContractID";
            string sortOrder = "desc";
            string cid = "";
            string parm = "";
            if (!string.IsNullOrEmpty(search))
            {
                cid = search.Split('&')[0].ToString();
                parm = search.Split('&')[1].ToString();
            }
            Account account = GetCurrentAccount();
            List<ContractModel> queryData = m_BLL.ExportInfo(account.Id, sortOrder, sortName, cid, parm);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion

        #region  创建合同（新增跟进记录合同阶段）   create by chand 2016-09-20
        //
        [SupportFilter]
        public ActionResult Create(string customerId)
        {
            ViewBag.CustomerID = customerId;
            return View();
        }
        [SupportFilter]
        public ActionResult CreateMultiple(string customerId)
        {
            ViewBag.CustomerID = customerId;
            return View();
        }
        [HttpPost]
        [SupportFilter]
        public ActionResult Create(string id, TB_Contract model)
        {
            if (model != null && ModelState.IsValid)
            {
                string currentPerson = GetCurrentPersonID();

                DateTime date = DateTime.Now;
                model.ContractID = Result.GetNewId();

                model.Flag = 1;
                model.VersionNo = 1;
                model.IsDelete = false;
                model.CreatedTime = date;
                model.CreatedBy = currentPerson;
                model.LastUpdatedTime = date;
                model.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;
                if (model.SysPersonID != model.SysPersonIDOld && !string.IsNullOrWhiteSpace(model.SysPersonID))
                {
                    new TBCustomerServiceBLL().Create(ref validationErrors, model.CustomerID, model.SysPersonID);

                }
                if (m_BLL.Create(ref validationErrors, model))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，创建合同：" + model.ContractNO, "合同管理"
                        );//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建合同：" + model.ContractNO + ",错误：" + returnValue, "合同管理"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 


        }

        #endregion

        #region 获取最大合同编号

        public JsonResult GetMaxContractNO(string SolutionID)
        {
            string MaxContractNO = string.Empty;

            if (m_BLL.GetMaxContractNOBySolutionID(ref validationErrors, SolutionID, ref MaxContractNO))
            {
                return Json(new
                {
                    Result = 1,
                    MaxContractNO = MaxContractNO

                });
            }
            string returnValue = "";

            if (validationErrors != null && validationErrors.Count > 0)
            {
                validationErrors.All(a =>
                {
                    returnValue += a.ErrorMessage;
                    return true;
                });
            }

            return Json(new
            {
                Result = 0,
                Msg = returnValue

            });

        }
        #endregion

        #region  根据客户编号获取客服
        public JsonResult GetCustomerService(string id)
        {
            ITBCustomerServiceBLL s_bll = new TBCustomerServiceBLL();

            var queryData = s_bll.GetCustomerServiceData(id);
            return Json(queryData, JsonRequestBehavior.AllowGet
                );
        }


        #endregion

        #region 获取客户评分等级 create by chand 2016-09-22

        [HttpPost]
        public JsonResult GetRatingScore(string id)
        {
            string flag = "";
            TB_Customer customer = m_CustomerBLL.GetById(id);
            if (customer != null && customer.RatingScore != null)
            {
                flag = customer.RatingScore;
            }

            return Json(new
            {
                Result = flag

            });
        }
        #endregion

        #region 直接创建合同其他合同（）create by chand 2016-09-22

        public ActionResult MyList()
        {
            return View();
        }


        public ActionResult CreateNew()
        {
            string MaxContractNO = string.Empty;
            //猎头服务类型
            m_BLL.GetMaxContractNOBySolutionID(ref validationErrors, "16080414194353397898601b8dbdc", ref MaxContractNO);
            ViewBag.MaxContractNO = MaxContractNO;
            return View();
        }



        [HttpPost]
        public JsonResult GetCreateNew(string CustomerName, string ContractNO)
        {
            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                TB_Contract model = new TB_Contract();
                string currentPerson = GetCurrentPersonID();

                DateTime date = DateTime.Now;
                model.ContractID = Result.GetNewId();
                model.CustomerName = CustomerName;
                model.ContractNO = ContractNO;
                //默认签约公司-欧孚人才服务
                model.SigningCompany = "1604211247433814674ebd69552cf";
                //猎头服务类型
                model.RecommendSolutionIDs = "16080414194353397898601b8dbdc";

                model.VersionNo = 1;
                model.IsDelete = false;
                model.CreatedTime = date;
                model.CreatedBy = currentPerson;
                model.LastUpdatedTime = date;
                model.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;
                if (m_BLL.SqlCreate(ref validationErrors, model))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，创建合同：" + model.ContractNO, "合同管理");//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建合同：" + model.ContractNO + ",错误：" + returnValue, "合同管理"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }




            }
            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }



        #endregion

        #region 客服创建合同  create by chand 2016-09-01

        public ActionResult ServiceList()
        {
            return View();
        }

        /// <summary>
        /// 创建客服合同
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateByService()
        {
            return View();
        }
        [HttpPost]
        [SupportFilter]
        public ActionResult CreateByService(string id, TB_Contract model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (!new SysMessageBLL().IsVerification(model.PhoneNumber.ToString(), model.SendCode))
                {
                    return Json("验证码不正确！"); //提示插入失败

                }
                string currentPerson = GetCurrentPersonID();

                DateTime date = DateTime.Now;
                model.ContractID = Result.GetNewId();
                model.Flag = 2;
                model.VersionNo = 1;
                model.IsDelete = false;
                model.CreatedTime = date;
                //model.CreatedBy = currentPerson;
                model.LastUpdatedTime = date;
                model.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;
                //创建客服
                if (model.SysPersonID != model.SysPersonIDOld && !string.IsNullOrWhiteSpace(model.SysPersonID))
                {
                    new TBCustomerServiceBLL().Create(ref validationErrors, model.CustomerID, model.SysPersonID);

                }
                if (m_BLL.Create(ref validationErrors, model))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，创建合同：" + model.ContractNO, "合同管理"
                        );//写入日志 
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，创建合同：" + model.ContractNO + ",错误：" + returnValue, "合同管理"
                        );//写入日志                      
                    return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                }


            }


            return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 


        }


        /// <summary>
        /// 合同列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetServiceData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<ContractEntity> queryData;
            queryData = m_BLL.GetContractByServiceData(account.Id, id, order, sort, search, page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 客服合同 欧孚
        /// </summary>
        /// <returns></returns>
        public ActionResult OfferServiceList()
        {
            return View();
        }

        /// <summary>
        /// 合同列表 offer
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOfferServiceData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<ContractEntity> queryData;
            queryData = m_BLL.GetContractByServiceDataByType(account.Id, id, order, sort, search, "offer", page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateByServiceOffer()
        {
            return View();
        }
        /// <summary>
        /// 客服合同 博尔捷
        /// </summary>
        /// <returns></returns>
        public ActionResult BridgeServiceList()
        {
            return View();
        }
        /// <summary>
        /// 合同列表 Bridge
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBridgeServiceData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<ContractEntity> queryData;
            queryData = m_BLL.GetContractByServiceDataByType(account.Id, id, order, sort, search, "Bridge", page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateByServiceBridge()
        {
            return View();
        }

        #endregion

        #region 查看合同  create by chand 2016-09-01
        /// <summary>
        /// 查看合同
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewContract(string id)
        {
            var entity = m_BLL.GetEntity(id);
            if (entity == null)
            {
                return Content("信息不存在！");
            }
            return View(entity);
        }

        public ActionResult ViewByService(string id)
        {
            var entity = m_BLL.GetById(id);
            if (entity == null)
            {
                return Content("信息不存在！");
            }
            return View(entity);
        }

        #endregion

        #region 导入 合同
        TBCustomerBLL cBLL = new TBCustomerBLL();
        public ActionResult ImportByService()
        {
            return View();
        }

        /// <summary>
        /// 模板下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DownTemplate(string type)
        {
            string filePath = Server.MapPath("~/up/Files/合同导入模板.xls");
            DownFile(filePath, "合同导入模板.xls");
            return new EmptyResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadContractData()
        {
            int SuccessCount = 0;
            int ErrorCount = 0;
            string toFileFullPath = "";
            string serverFileName = "";
            DataTable data = new DataTable();
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

                            //ITPSalaryDetailsBLL salaryBLL = new TPSalaryDetailsBLL();

                            #region 导入内容
                            //获取当前用户ID
                            string currentPerson = GetCurrentPersonID();


                            //记录薪资项目和对应的值
                            //  Dictionary<string, string> dicList = new Dictionary<string, string>();

                            ISysFieldHander baseDDL = new SysFieldHander();
                            //服务类型
                            var hzfwList = baseDDL.GetSysFieldByParentId("1508071046119479968e30ccf1f9f");
                            //签约公司
                            var qyList = baseDDL.GetSysFieldByParentId("1604121151490315992e569f90a1e");


                            //记录最后一次的客户，如果上次导入的客户和本次的客户相同则不需要再查询
                            TB_Customer CustomerEntity = new TB_Customer();

                            TB_Contract model = null;
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
                                //   dicList.Clear();
                                validationErrors.Clear();
                                error = string.Empty;
                                try
                                {
                                    model = new TB_Contract();

                                    DateTime date = DateTime.Now;
                                    model.ContractID = Result.GetNewId();
                                    model.Flag = 2;
                                    model.VersionNo = 2;
                                    model.IsDelete = false;
                                    model.CreatedTime = date;
                                    model.LastUpdatedTime = date;
                                    model.LastUpdatedBy = currentPerson;

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
                                                        CellValue = formatter.FormatCellValue(row.GetCell(j)).Replace(" ", ""); ;
                                                    }
                                                    else
                                                    {
                                                        CellValue = row.GetCell(j).NumericCellValue.ToString().Replace(" ", ""); ;
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    //数字类型
                                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                    {
                                                        DataFormatter formatter = new DataFormatter();
                                                        CellValue = formatter.FormatCellValue(row.GetCell(j)).Replace(" ", ""); ;
                                                    }
                                                    else
                                                    {
                                                        CellValue = row.GetCell(j).NumericCellValue.ToString().Replace(" ", ""); ;
                                                    }
                                                    break;
                                                case CellType.String:
                                                    //字符串
                                                    CellValue = row.GetCell(j).StringCellValue.Replace(" ", ""); ;
                                                    break;

                                                default:
                                                    break;
                                            }

                                            #endregion

                                            dataRow[j] = CellValue;

                                            #region 客户名称
                                            if (TitleList[j] == "客户名")
                                            {
                                                if (CustomerEntity != null && CustomerEntity.CustomerName.Equals(CellValue))
                                                {
                                                    //dicList.Add("CustomerName", CustomerEntity.CustomerName);
                                                    //dicList.Add("CustomerID", CustomerEntity.CustomerID);
                                                    model.CustomerName = CustomerEntity.CustomerName;
                                                    model.CustomerID = CustomerEntity.CustomerID;
                                                }
                                                else
                                                {
                                                    CustomerEntity = cBLL.GetByName(CellValue);
                                                    if (CustomerEntity == null)
                                                    {
                                                        error = "导入失败，客户名不存在";
                                                        ErrorCount++;
                                                        break;
                                                    }
                                                    model.CustomerName = CustomerEntity.CustomerName;
                                                    model.CustomerID = CustomerEntity.CustomerID;
                                                }


                                            }
                                            #endregion

                                            #region 签约公司
                                            //设置新建客户值
                                            else if (TitleList[j] == "签约公司")
                                            {
                                                CellValue = CellValue.Trim();
                                                var SigningCompanyEntity = qyList.SingleOrDefault(t => t.MyTexts == CellValue);
                                                if (SigningCompanyEntity == null)
                                                {
                                                    error = "导入失败，签约公司不存在";
                                                    ErrorCount++;
                                                    break;
                                                }
                                                model.SigningCompanyName = SigningCompanyEntity.MyTexts;
                                                model.SigningCompany = SigningCompanyEntity.Id;
                                            }
                                            #endregion

                                            #region 服务类型
                                            else if (TitleList[j] == "服务类型")
                                            {
                                                var arr = CellValue.GetSplit();
                                                if (arr.Length < 1)
                                                {
                                                    error = "导入失败，服务类型不能为空";
                                                    ErrorCount++;
                                                    break;
                                                }
                                                foreach (var item in arr)
                                                {
                                                    var solutionEntity = hzfwList.SingleOrDefault(t => t.MyTexts == item);
                                                    if (solutionEntity != null)
                                                    {
                                                        model.RecommendSolutionIDs += solutionEntity.Id + ",";
                                                    }
                                                }
                                                if (string.IsNullOrWhiteSpace(model.RecommendSolutionIDs))
                                                {
                                                    error = "导入失败，服务类型不能为空";
                                                    ErrorCount++;
                                                    break;
                                                }
                                                model.RecommendSolutionID = model.RecommendSolutionIDs;
                                            }
                                            #endregion
                                            #region 合同编号
                                            else if (TitleList[j] == "合同编号")
                                            {
                                                int count = m_BLL.GetContractNOCount(CellValue);
                                                if (count > 0)
                                                {
                                                    error = "合同编号已存在";
                                                    ErrorCount++;
                                                    break;
                                                }
                                                model.ContractNO = CellValue;
                                            }
                                            #endregion
                                            #region 合同金额
                                            else if (TitleList[j] == "合同金额")
                                            {
                                                model.ContractMoney = CellValue;
                                            }
                                            #endregion

                                            #region 合同起始日期
                                            else if (TitleList[j] == "合同起始日期")
                                            {
                                                model.EffectiveDate = CellValue.ToDateTime();
                                            }
                                            #endregion

                                            #region 合同终止日期
                                            else if (TitleList[j] == "合同终止日期")
                                            {
                                                model.Deadline = CellValue.ToDateTime();
                                            }
                                            #endregion

                                            #region 签约人姓名
                                            else if (TitleList[j] == "签约人姓名")
                                            {
                                                model.ProjectLeader = CellValue;
                                                model.CreatedBy = CellValue;
                                            }
                                            #endregion

                                            #region 创建人
                                            else if (TitleList[j] == "创建人")
                                            {
                                                model.CreatedBy = CellValue;
                                            }
                                            #endregion

                                            #region 手机号码
                                            else if (TitleList[j] == "手机号码")
                                            {
                                                model.PhoneNumber = CellValue;
                                            }
                                            #endregion



                                            #region 付款方式
                                            else if (TitleList[j] == "付款方式")
                                            {
                                                model.PayType = CellValue;
                                            }
                                            #endregion

                                            #region 签约类型
                                            else if (TitleList[j] == "签约类型")
                                            {
                                                int? isNew;
                                                if (CellValue == "新签")
                                                {
                                                    isNew = 0;
                                                }
                                                else if (CellValue == "续签")
                                                {
                                                    isNew = 1;
                                                }
                                                else if (CellValue == "其他")
                                                {
                                                    isNew = 2;
                                                }
                                                else
                                                {
                                                    isNew = 3;
                                                }
                                                model.IsNew = isNew;
                                            }
                                            #endregion
                                            #region 备注
                                            else if (TitleList[j] == "备注")
                                            {
                                                model.Remark = CellValue;
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion

                                    if (error == string.Empty)
                                    {
                                        if (string.IsNullOrEmpty(model.ContractMoney))
                                        {
                                            model.ContractMoney = "未知";
                                        }
                                        if (m_BLL.Create(ref validationErrors, model))
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
                        LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，合同导入", "合同管理");//写入日志 
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
                }
            }
            error = "导入数据不存在，请返回批量导入页面！";
            ViewBag.Error = error;

            ViewBag.Url = serverFileName;
            ViewBag.ErrorCount = ErrorCount;
            ViewBag.SuccessCount = SuccessCount;
            return View("ExportError");
        }

        public ActionResult ExportError()
        {
            return View();
        }

        #endregion

    }
}