using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CRM.BLL;
using Common;
using CRM.DAL;
using Models;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using CRM.App.Commons;
using System.ComponentModel.DataAnnotations;
using CRM.IBLL;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-25
    /// 描述说明：客户-控制器
    /// </summary>
    [SupportFilter]
    public class CustomerController : BaseController
    {
        #region 初始化

        IBLL.ITBCustomerBLL m_BLL;

        public CustomerController()
            : this(new TBCustomerBLL())
        { }

        public CustomerController(TBCustomerBLL bll)
        {
            m_BLL = bll;
        }
        ValidationErrors validationErrors = new ValidationErrors();
        ITBCustomerRatingBLL RatingBLL = new TBCustomerRatingBLL();
        string currentPerson = "";


        #endregion

        #region 视图 所有客户、所有权限菜单  create by chand 2015-07-02

        public ActionResult Index()
        {
            var PersonList = new SysPersonBLL().GetPersonVisibility(GetCurrentPersonID());
            if (PersonList.Count > 0 && PersonList.Count < 15)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = GetCurrentPersonID();
            return View(entity);
        }

        #endregion

        #region 视图 自己可见客户 create by chand 2015-07-02

        public ActionResult Self()
        {
            TB_Customer entity = new TB_Customer();
            entity.SysPersonID = GetCurrentPersonID();
            return View(entity);
        }

        #endregion

        #region 视图 公共池客户 create by chand 2015-07-02

        public ActionResult Public()
        {
            return View();
        }


        #endregion

        #region 视图 客户基本信息 create by chand 2015-07-01

        /// <summary>
        /// 查看视图
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult View(string ID)
        {
            TB_Customer entity = m_BLL.GetById(ID);

            return View(entity);
        }


        #endregion

        #region 获取 可见客户数据 create by chand 2015-07-02

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetAllData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerModel> queryData;
            if (account.FollowType == 2)
            {
                queryData = m_BLL.GetCustomerData_KA(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            }
            else
            {
                queryData = m_BLL.GetData(GetCurrentPersonID(), id, page, rows, order, sort, search, ref total);
            }
            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }



        #endregion


        #region 获取 我的客户列表查询 create by chand 2016-03-25

        /// <summary>
        /// 我的客户列表查询
        /// </summary>
        /// <param name="id">归属人ID（SysPersonID）</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult GetCustomerSelfData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            Account account = GetCurrentAccount();
            List<CustomerModel> queryData;

            queryData = m_BLL.GetCustomerData_Self(GetCurrentPersonID(), id, order, sort, search, page, rows, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }



        #endregion

        #region 获取 公共池客户数据 create by chand 2015-07-02

        public JsonResult GetPublicData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            //search += "DateType&2^";
            List<CustomerModel> queryData = m_BLL.GetPublicData("", page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }
        #endregion

        #region 创建客户  create by chand 2015-06-12

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Create()
        {
            TB_Customer entity = new TB_Customer();

            entity.SysPersonID = GetCurrentPersonID();
            return View(entity);
        }



        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Create(TB_Customer entity)
        {


            if (entity != null && ModelState.IsValid)
            {
                entity.CustomerName = entity.CustomerName.Trim();
                string currentPerson = GetCurrentPersonID();
                if (!m_BLL.IsCreate(currentPerson))
                {
                    return Json(new
                    {
                        Result = "0",
                        CustomerID = entity.CustomerID,
                        returnValue = Suggestion.InsertFail + "，您没有创建客户权限或超出创建客户数！"

                    });
                }
                DateTime date = DateTime.Now;
                entity.CustomerID = Result.GetNewId();

                entity.BelongingDate = date;
                entity.RelieveDate = date.AddMonths(6);
                entity.SchoolRecruitingQty = 0;//校招职位数量
                entity.SocialRecruitingQty = 0;//社招职位数量
                entity.IsCertification = false;
                entity.IsRegister = false;
                entity.IsFrozen = false;
                entity.VersionNo = 1;
                entity.IsDelete = false;
                entity.CreatedTime = date;
                entity.CreatedBy = currentPerson;
                entity.LastUpdatedTime = date;
                entity.LastUpdatedBy = currentPerson;
                entity.AuditStatus = 0;
                entity.Source = 1;//客户来源创建

                RatingBLL.GetCalcRatingBefore(entity);

                string returnValue = string.Empty;

                if (m_BLL.Create(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，客户名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理");//写入日志                     
                    return Json(new
                    {
                        Result = "1",
                        CustomerID = entity.CustomerID,
                        returnValue = returnValue

                    });
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
                    LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，客户名称：" + entity.CustomerName + ",错误：" + returnValue, "客户管理");//写入日志                      
                    //return Json(Suggestion.InsertFail + returnValue); //提示插入失败
                    return Json(new
                    {
                        Result = "0",
                        CustomerID = entity.CustomerID,
                        returnValue = returnValue

                    });
                }
            }

            //return Json(Suggestion.InsertFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
            return Json(new
            {
                Result = "0",
                CustomerID = entity.CustomerID,
                returnValue = Suggestion.InsertFail + "，请核对输入的数据的格式"

            });
        }


        #region 查重
        [HttpPost]
        public JsonResult CustomerRepeat(string CustomerName, string CustomerID)
        {
            // -2 自己创建过 -1其它用户已创建过不可以创建  0可以创建    1 需要验证
            int result = 0;
            var q = m_BLL.GetCustomerRepeat(CustomerName.Trim(), CustomerID, ref result);
            if (result == -1)
            {
                string personID = GetCurrentPersonID();
                if (q != null)
                {
                    var aa = (from s in q where s.SysPersonID == personID select s);
                    if (aa != null && aa.Count() > 0)
                    {
                        result = -2;
                    }
                }
            }
            object data = null;
            if (result == 1)
            {
                //需要验证时，返回对应客户
                data = new datagrid
                        {
                            total = 0,
                            rows = q
                        };
            }
            return Json(new
            {
                Result = result,
                Data = data
            });

        }
        [HttpPost]
        public JsonResult SendVerification(string CustomerName)
        {
            string returnValue = "";
            int flag = 0;
            if (m_BLL.SendVerification(ref validationErrors, CustomerName, GetCurrentPersonID()))
            {
                flag = 1;
                //写入日志   
                LogClassModels.WriteServiceLog(Suggestion.SendEmailSucceed + "，发送验证码给相关客户归属人，新建客户名称:" + CustomerName, "客户管理");
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
                //写入日志   
                LogClassModels.WriteServiceLog(Suggestion.SendEmailFail + "，发送验证码给相关客户归属人,新建客户名称：" + CustomerName + ",错误：" + returnValue, "客户管理");
            }

            return Json(new { Result = flag, ErrorMsg = returnValue });
        }
        #endregion

        #endregion

        #region 修改客户  create by chand 2015-06-15

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Edit(string ID)
        {
            TB_Customer entity = m_BLL.GetById(ID);

            return View(entity);
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter]
        public ActionResult Edit(string id, TB_Customer model)
        {
            if (string.IsNullOrWhiteSpace(model.ContactPerson))
            {
                model.ContactPerson = "无";
            }
            if (model != null && ModelState.IsValid)
            {


                TB_Customer entity = m_BLL.GetById(model.CustomerID);


                entity.CompanyName = model.CompanyName;
                entity.CompanyID = model.CompanyID;
                entity.EstablishDate = model.EstablishDate;
                entity.HomePage = model.HomePage;

                entity.VocationName = model.VocationName;
                entity.CustomerLevel = model.CustomerLevel;
                entity.ProvinceCode = model.ProvinceCode;
                entity.ProvinceName = model.ProvinceName;
                entity.CityCode = model.CityCode;
                entity.CityName = model.CityName;
                entity.DistrictCode = model.DistrictCode;
                entity.DistrictName = model.DistrictName;
                entity.AddressDetails = model.AddressDetails;
                entity.ContactPerson = model.ContactPerson;
                entity.ContactPhone = model.ContactPhone;
                entity.ContactTel = model.ContactTel;
                entity.EmailAddress = model.EmailAddress;
                entity.PositionLink = model.PositionLink;
                entity.Comments = model.Comments;
                entity.CustomerSource = model.CustomerSource;//客户来源

                entity.CompanyNatureCode = model.CompanyNatureCode;
                entity.RegisterCapital = model.RegisterCapital;
                entity.SalesRevenue = model.SalesRevenue;
                entity.CompanySize = model.CompanySize;
                entity.VocationCode = model.VocationCode;

                #region 企业信用等级评分标准-签合同之前

                if (string.IsNullOrWhiteSpace(entity.RatingScore))
                {
                    RatingBLL.GetCalcRatingBefore(entity);
                }
                #endregion

                string currentPerson = GetCurrentPersonID();
                entity.VersionNo++;
                entity.LastUpdatedTime = DateTime.Now;
                entity.LastUpdatedBy = currentPerson;

                string returnValue = string.Empty;

                if (m_BLL.Edit(ref validationErrors, entity))
                {
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理"
                        );//写入日志 
                    App.Codes.MenuCaching.ClearCache(id);
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，客户名称：" + entity.CustomerName + ",错误：" + returnValue, "客户管理"
                        );//写入日志                      
                    return Json(Suggestion.UpdateFail + returnValue); //提示插入失败
                }
            }

            return Json(Suggestion.UpdateFail + "，请核对输入的数据的格式"); //提示输入的数据的格式不对 
        }

        #endregion

        #region 客户详细操作 create by chand 2015-06-23

        public ActionResult Details(string ID)
        {
            //TB_Customer entity = m_BLL.GetById(ID);
            ViewBag.ID = ID;
            Account account = GetCurrentAccount();
            ViewBag.FollowType = account.FollowType;
            return View();
        }


        public ActionResult SelfDetails(string ID, string tab = "")
        {
            ViewBag.ID = ID;
            ViewBag.Tab = tab;

            Account account = GetCurrentAccount();
            int dataType = m_BLL.GetCustomerAuthority(ID, account.Id);
            if (dataType == 1 || dataType == -1)
            {
                //只读
                dataType = 0;
            }
            else
            {
                //可编辑
                dataType = 1;
            }
            ViewBag.DataType = dataType;
            ViewBag.FollowType = account.FollowType;
            return View();
        }
        #endregion

        #region 删除 create by chand 2015-06-23
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>   
        [HttpPost]
        public ActionResult Delete(string ID, string CustomerName)
        {
            string returnValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(ID))
            {
                TB_Customer entity = m_BLL.GetById(ID);
                if (entity != null)
                {
                    entity.IsDelete = true;
                    entity.LastUpdatedBy = GetCurrentPersonID();
                    entity.LastUpdatedTime = DateTime.Now;
                    entity.VersionNo++;
                    if (m_BLL.Edit(ref validationErrors, entity))
                    {
                        //写入日志 
                        LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户联系人：" + CustomerName + "，联系人Id：" + ID, "客户管理");
                        App.Codes.MenuCaching.ClearCache(ID);
                        return Json("OK");
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
                        }//删除失败，写入日志
                        LogClassModels.WriteServiceLog(Suggestion.DeleteFail + "，客户联系人：" + CustomerName + "，联系人Id：" + ID, "客户管理");
                    }
                }
            }
            return Json(returnValue);
        }
        #endregion

        #region 城市级联操作 create by chand  2015-07-08
        /// <summary>
        /// 获取所有的省份
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProvince()
        {
            return Json(SysCodeTableModels.GetAllProvince(), "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据省份名称获取对应的城市列表
        /// </summary>
        /// <param name="provinceName">省份名称</param>
        /// <returns></returns>
        public ActionResult GetCitysByProvinceName(string provinceName)
        {
            return Json(SysCodeTableModels.GetSysCodeTableByParentId(provinceName), "text/html", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 根据城市名称获取对应的行政区划类别
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns></returns>
        public ActionResult GetDistrictByCityName(string cityName)
        {
            return Json(SysCodeTableModels.GetSysCodeTableByParentId(cityName), "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 检查是否可以创建客户 create by chand 2015-08-14

        public JsonResult IsCreate()
        {
            var result = 0;
            if (m_BLL.IsCreate(GetCurrentPersonID()))
            {
                result = 1;
            }
            return Json(new
            {
                Result = result
            });
        }

        #endregion

        #region 释放客户 & 提取客户  create by chand 2015-08-17

        /// <summary>
        /// 释放客户
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public JsonResult Release(string IDs)
        {
            string returnValue = string.Empty;
            int Result = 0;
            if (!string.IsNullOrWhiteSpace(IDs))
            {
                string[] arr = IDs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    string CurrentPersonID = GetCurrentPersonID();

                    int flag = 0;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        validationErrors.Clear();
                        TB_Customer entity = m_BLL.GetById(arr[i]);
                        if (entity != null)
                        {
                            //分享客户释放
                            IBLL.ITBCustomerShareBLL m_ShareBLL = new TBCustomerShareBLL();
                            int demo = m_ShareBLL.GetCustomerShareCancel(entity.CustomerID, CurrentPersonID);
                            if (demo == 1)
                            {
                                flag++;
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户释放，取消分享给我的【" + entity.CustomerName + "】客户，客户Id：" + entity.CustomerID, "客户管理");
                            }
                            else
                            {
                                #region 更新客户
                                if (m_BLL.GetCustomerRelease(ref validationErrors, entity.CustomerID, CurrentPersonID))
                                {
                                    flag++;
                                    //写入日志
                                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户释放，名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理");
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
                                    //写入日志
                                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，客户释放，名称：" + entity.CustomerName + "，错误：" + returnValue, "客户管理");
                                }
                                #endregion
                            }
                        }
                    }
                    if (flag == arr.Length)
                    {
                        //所有客户更新成功
                        Result = 1;
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
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(returnValue))
            {
                returnValue = "请核对输入的数据的格式";
            }
            return Json(new
            {
                Result = Result,
                Msg = returnValue

            });
        }


        /// <summary>
        /// 提取客户
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public JsonResult Extraction(string IDs)
        {
            string returnValue = string.Empty;
            int Result = 0;
            if (!string.IsNullOrWhiteSpace(IDs))
            {
                string[] arr = IDs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    string CurrentPersonID = GetCurrentPersonID();

                    int flag = 0;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        validationErrors.Clear();
                        TB_Customer entity = m_BLL.GetById(arr[i]);
                        if (entity != null && entity.IsFrozen)
                        {
                            #region 更新--提取客户
                            if (m_BLL.GetCustomerExtraction(ref validationErrors, entity.CustomerID, CurrentPersonID))
                            {
                                flag++;
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，提取客户，名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理");
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
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，提取客户，名称：" + entity.CustomerName + "，错误：" + returnValue, "客户管理");
                            }
                            #endregion
                        }
                    }
                    if (flag == arr.Length)
                    {
                        //所有客户提取成功
                        Result = 1;
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
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(returnValue) && Result == 0)
            {
                returnValue = "请核对输入的数据的格式";
            }
            return Json(new
            {
                Result = Result,
                Msg = returnValue

            });
        }



        #endregion

        #region 客户审核 create by chand
        public ActionResult AuditManagement()
        {
            return View();
        }

        public ActionResult SelfAudit()
        {
            return View();
        }

        public JsonResult GetAuditAllData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            //search += "DateType&2^";
            if (!string.IsNullOrWhiteSpace(id))
            {
                search += "SysPersonID&" + GetCurrentPersonID() + "^";
            }
            List<CustomerModel> queryData = m_BLL.GetCustomerAuditData("", page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }

        public JsonResult GetAuditCustomer(int isPass, string CustomerID, string Reason)
        {
            int result = 0;
            string msg = string.Empty;
            if (!string.IsNullOrWhiteSpace(CustomerID))
            {
                if (isPass == 0 && string.IsNullOrWhiteSpace(Reason))
                {
                    msg = "拒绝原因不能为空！";
                }
                else
                {
                    if (m_BLL.GetAuditCustomer(ref validationErrors, (isPass == 1), CustomerID, Reason, GetCurrentPersonID()))
                    {
                        result = 1;
                    }
                }
            }
            else
            {
                msg = "客户不存在";
            }
            return Json(new
            {
                Result = result,
                Msg = msg
            });
        }

        #endregion

        #region 客户批量导入

        public ActionResult Import()
        {
            return View();
        }
        /// <summary>
        /// 模板下雪
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DownTemplateFile()
        {
            string filePath = Server.MapPath("~/up/Files/客户导入模板.xls");
            DownFile(filePath, "客户导入模板.xls");
            return new EmptyResult();

        }

        public ActionResult FileUpload()
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
                    //DataTable dt = excelHelper.ExcelToDataTable("Sheet1", true);
                    ISheet sheet = null;

                    int startRow = 0;

                    #region 读取Excel 导入到系统中
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

                                    if (cell.StringCellValue != null)
                                    {
                                        //添加到集合列中
                                        DataColumn column = new DataColumn(cell.StringCellValue.Trim());
                                        data.Columns.Add(column);
                                        TitleList.Add(cell.StringCellValue.Trim());
                                    }
                                }
                            }
                            //最后一列显示导入结果状态
                            //cellCount++;
                            DataColumn lastColumn = new DataColumn("结果");
                            data.Columns.Add(lastColumn);


                            startRow = sheet.FirstRowNum + 1;

                            #endregion

                            #region 导入内容
                            //获取当前用户ID
                            currentPerson = GetCurrentPersonID();

                            //最后一列的标号
                            int rowCount = sheet.LastRowNum;



                            for (int i = startRow; i <= rowCount; ++i)
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue; //没有数据的行默认是null　　　　　　　

                                if (row.FirstCellNum > 0) continue;//不是从第一个单元格开始
                                //初始客户基本信息
                                TB_Customer entity = new TB_Customer();
                                entity.SysPersonID = currentPerson;
                                entity.CreatedBy = currentPerson;
                                entity.LastUpdatedBy = currentPerson;

                                DataRow dataRow = data.NewRow();

                                validationErrors.Clear();

                                try
                                {
                                    error = string.Empty;
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        string CellValue = string.Empty;
                                        if (row.GetCell(j) == null)
                                        {
                                            continue;
                                        }

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
                                                // CellValue = row.GetCell(j).CellFormula;

                                                if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                {
                                                    DataFormatter formatter = new DataFormatter();
                                                    CellValue = formatter.FormatCellValue(row.GetCell(j)).Replace(" ", "");
                                                }
                                                else
                                                {
                                                    CellValue = row.GetCell(j).NumericCellValue.ToString().Replace(" ", "");
                                                }
                                                break;
                                            case CellType.Numeric:
                                                //数字类型
                                                if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                                {
                                                    DataFormatter formatter = new DataFormatter();
                                                    CellValue = formatter.FormatCellValue(row.GetCell(j)).Replace(" ", "");
                                                }
                                                else
                                                {
                                                    CellValue = row.GetCell(j).NumericCellValue.ToString().Replace(" ", "");
                                                }
                                                break;
                                            case CellType.String:
                                                //字符串
                                                CellValue = row.GetCell(j).StringCellValue.Replace(" ", "");
                                                break;

                                            default:
                                                break;
                                        }

                                        #endregion

                                        if (string.IsNullOrWhiteSpace(CellValue))
                                        {
                                            continue;
                                        }
                                        dataRow[j] = CellValue;
                                        if (TitleList[j] == "客户名称")
                                        {
                                            if (CellValue.Length <= 4)
                                            {
                                                error = "公司名称不完整";
                                                break;
                                            }
                                            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
                                            if (!reg.IsMatch(CellValue))
                                            {
                                                error = "客户名称只能包含汉子和括弧";
                                                break;
                                            }

                                            //entity.CustomerName
                                        }

                                        SetCustomerValue(entity, TitleList[j], CellValue);
                                    }
                                    if (error == string.Empty)
                                    {
                                        if (ImportCustomer(ref error, entity))
                                        {
                                            error = "导入成功";
                                        }
                                        else
                                        {
                                            error = "导入失败," + error;
                                        }

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
                        }


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
                    ViewBag.ResultImportData = data;
                    ViewBag.Url = serverFileName;
                    return View("FileUpload", data);
                }
            }
            error = "导入数据不存在，请返回批量导入页面！";
            ViewBag.Error = error;

            //return RedirectToAction("Import");
            //return Content("批量导入客户异常 ！", "text/plain");
            return View("FileUpload", data);
        }
        /// <summary>
        /// 设置客户值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        /// <param name="cellValue"></param>
        public void SetCustomerValue(TB_Customer entity, int index, string cellValue)
        {
            switch (index)
            {
                case 0:
                    entity.CustomerName = cellValue;
                    break;
                case 1:
                    //行业类别
                    entity.VocationName = cellValue;
                    SysCodeTable code = DefaultListProvider.GetDefaultEntity("VocationCategory", cellValue);
                    if (code != null)
                    {
                        entity.VocationName = code.CodeValue;
                        entity.VocationCode = code.CodeID;
                    }

                    break;
                case 2:
                    //企业性质
                    if (cellValue.Contains("国企"))
                    {
                        entity.CompanyNatureCode = "1506121506437425486a569af8cad";
                    }
                    else if (cellValue.Contains("民营"))
                    {
                        entity.CompanyNatureCode = "1506121507123741863ff409ebb37";
                    }
                    else if (cellValue.Contains("外资"))
                    {
                        entity.CompanyNatureCode = "15061215075696073650f0556dc08";
                    }
                    else
                    {
                        entity.CompanyNatureCode = "";
                    }
                    break;
                case 3:
                    //客户级别
                    if (cellValue.Contains("低级"))
                    {
                        entity.CustomerLevel = "15061216134957181292f2b4a5d89";
                    }
                    else if (cellValue.Contains("中级"))
                    {
                        entity.CustomerLevel = "1506121614153832892109e11afed";
                    }
                    else if (cellValue.Contains("高级"))
                    {
                        entity.CustomerLevel = "150612161442547842908ccf39bf9";
                    }
                    else
                    {
                        entity.CustomerLevel = "";
                    }
                    break;
                case 4:
                    //联系人
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.ContactPerson = "无";
                    }
                    else
                    {
                        entity.ContactPerson = cellValue;
                    }
                    break;
                case 5:
                    //职位
                    if (cellValue.Contains("前台"))
                    {
                        entity.Post = "1509070944153467659cb5c9239b9";
                    }
                    else if (cellValue.Contains("专员"))
                    {
                        entity.Post = "150612100604821875264edcfc726";
                    }
                    else if (cellValue.Contains("VP"))
                    {
                        entity.Post = "1506121006236979084c69c8b702b";
                    }
                    else if (cellValue.Contains("主管"))
                    {
                        entity.Post = "15090709445909814067de2bfaba6";
                    }
                    else if (cellValue.Contains("经理"))
                    {
                        entity.Post = "1509070945263028608992a2f8e11";
                    }
                    else if (cellValue.Contains("总监"))
                    {
                        entity.Post = "1509070946055567858bcb43ff919";
                    }
                    else
                    {
                        entity.Post = "1509070944153467659cb5c9239b9";
                    }

                    break;
                case 6:
                    //固定电话
                    entity.ContactTel = cellValue;
                    break;
                case 7:
                    //手机号码
                    entity.ContactPhone = cellValue;
                    break;
                case 8:
                    entity.EmailAddress = cellValue;
                    break;
                case 9:
                    //省份
                    cellValue = cellValue.TrimEnd('省');
                    entity.ProvinceName = cellValue;
                    SysCodeTable Province = DefaultListProvider.GetFirstOrDefault("ProvinceCategory", cellValue);
                    if (Province != null)
                    {
                        entity.ProvinceCode = Province.CodeID;
                        entity.ProvinceName = Province.CodeValue;
                    }

                    break;
                case 10:
                    //城市
                    entity.CityName = cellValue;
                    SysCodeTable city = DefaultListProvider.GetLastOrDefault("ProvinceCategory", cellValue);
                    if (city != null)
                    {
                        entity.CityCode = city.CodeID;
                        entity.CityName = city.CodeValue;
                    }

                    break;
                case 11:
                    entity.AddressDetails = cellValue;
                    break;
                case 12:
                    entity.HomePage = cellValue;
                    break;
                case 13:
                    //客户来源
                    entity.PositionLink = cellValue;
                    break;
                case 14:
                    entity.Comments = cellValue;
                    break;
            }
        }

        public void SetCustomerValue(TB_Customer entity, string title, string cellValue)
        {
            switch (title)
            {
                case "客户名称":
                    entity.CustomerName = cellValue;
                    break;
                case "客户级别":
                    //客户级别
                    if (cellValue.Contains("低级"))
                    {
                        entity.CustomerLevel = "15061216134957181292f2b4a5d89";
                    }
                    else if (cellValue.Contains("中级"))
                    {
                        entity.CustomerLevel = "1506121614153832892109e11afed";
                    }
                    else if (cellValue.Contains("高级"))
                    {
                        entity.CustomerLevel = "150612161442547842908ccf39bf9";
                    }
                    else if (cellValue.Contains("合作"))
                    {
                        entity.CustomerLevel = "15072408403952458222bfb33a190";
                    }
                    else
                    {
                        entity.CustomerLevel = "";
                    }
                    break;
                case "企业性质":
                    //企业性质
                    if (cellValue.Contains("央企") || cellValue.Contains("全球500强"))
                    {
                        entity.CompanyNatureCode = "1607130958132592499779a15d824";
                    }
                    else if (cellValue.Contains("上市公司") || cellValue.Contains("国企") || cellValue.Contains("外资企业"))
                    {
                        entity.CompanyNatureCode = "1607130958132592499779a15d824";
                    }
                    else if (cellValue.Contains("民营集团企业") || cellValue.Contains("风投B轮及之后的公司"))
                    {
                        entity.CompanyNatureCode = "16071309592868206628818c805d1";
                    }
                    else
                    {
                        //不符合以上类型
                        entity.CompanyNatureCode = "160713100012835377731bfd77ea8";
                    }
                    break;
                case "注册资金":
                    if (cellValue.Contains("500万以下"))
                    {
                        entity.RegisterCapital = "160713100924717199476388d1702";
                    }
                    else if (cellValue.Contains("500万以上"))
                    {
                        entity.RegisterCapital = "16071310053643492313f51d8bdbc";
                    }
                    else if (cellValue.Contains("1000万以上"))
                    {
                        entity.RegisterCapital = "16071310045593911897dceeeaa7b";
                    }
                    else if (cellValue.Contains("3000万以上"))
                    {
                        entity.RegisterCapital = "1607131002104036726eb203720aa";
                    }
                    else if (cellValue.Contains("5000万以上"))
                    {
                        entity.RegisterCapital = "1607131001364842730873a740861";
                    }
                    break;
                case "销售收入":
                    if (cellValue.Contains("1千万以下"))
                    {
                        entity.SalesRevenue = "160713111610802847932a8539bb4";
                    }
                    else if (cellValue.Contains("1千万以上"))
                    {
                        entity.SalesRevenue = "1607131115373489937220b74dc76";
                    }
                    else if (cellValue.Contains("2千万以上"))
                    {
                        entity.SalesRevenue = "160713111513833366877223db7d4";
                    }
                    else if (cellValue.Contains("3千万以上"))
                    {
                        entity.SalesRevenue = "160713111457148078465d04008fc";
                    }
                    else if (cellValue.Contains("4千万以上"))
                    {
                        entity.SalesRevenue = "160713111436080015205e3ffc056";
                    }
                    else if (cellValue.Contains("5千万以上"))
                    {
                        entity.SalesRevenue = "16071311130386745126ebc9985c6";
                    }
                    else if (cellValue.Contains("6千万以上"))
                    {
                        entity.SalesRevenue = "160713111240718019903ba69e829";
                    }
                    else if (cellValue.Contains("7千万以上"))
                    {
                        entity.SalesRevenue = "16071311120649117875f02afaee2";
                    }
                    else if (cellValue.Contains("8千万以上"))
                    {
                        entity.SalesRevenue = "1607131111348611728c8e012082f";
                    }
                    else if (cellValue.Contains("9千万以上"))
                    {
                        entity.SalesRevenue = "1607131055148800552fa9e2cc575";
                    }
                    else if (cellValue.Contains("1亿以上"))
                    {
                        entity.SalesRevenue = "160713105110010108797bfbff0d5";
                    }
                    break;
                case "所属行业":
                    if (cellValue.Contains("仓储") || cellValue.Contains("物流") || cellValue.Contains("交通运输"))
                    {
                        entity.VocationCode = "16071310500882245375f079c3bb5";
                    }
                    else if (cellValue.Contains("服装") || cellValue.Contains("制造业"))
                    {
                        entity.VocationCode = "1607131050324964639cc1020d723";
                    }
                    else if (cellValue.Contains("酒店") || cellValue.Contains("餐饮") || cellValue.Contains("旅游业"))
                    {
                        entity.VocationCode = "1607131050502952855326ef80f51";
                    }
                    else if (cellValue.Contains("互联网") || cellValue.Contains("电商"))
                    {
                        entity.VocationCode = "1607131051099629838d20afb3dea";
                    }
                    else if (cellValue.Contains("医疗") || cellValue.Contains("医药行业"))
                    {
                        entity.VocationCode = "16091814542645716315e8d117a45";
                    }
                    break;
                case "公司规模":
                    if (cellValue.Contains("50人以下"))
                    {
                        entity.CompanySize = "16071310480994061708cc2847809";
                    }
                    else if (cellValue.Contains("50-150人"))
                    {
                        entity.CompanySize = "160713104748749432410a3ad3a44";
                    }
                    else if (cellValue.Contains("150-500人"))
                    {
                        entity.CompanySize = "16071310472809369546c30eaecb9";
                    }
                    else if (cellValue.Contains("500人-1000人"))
                    {
                        entity.CompanySize = "16071310470823377671f8ebcd1be";
                    }
                    else if (cellValue.Contains("1000人以上"))
                    {
                        entity.CompanySize = "16071310462852519752cebf53737";
                    }
                    break;
                case "联系人":
                    //联系人
                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.ContactPerson = "无";
                    }
                    else
                    {
                        entity.ContactPerson = cellValue;
                    }
                    break;
                case "职位":
                    //职位
                    if (cellValue.Contains("前台"))
                    {
                        entity.Post = "1509070944153467659cb5c9239b9";
                    }
                    else if (cellValue.Contains("专员"))
                    {
                        entity.Post = "150612100604821875264edcfc726";
                    }
                    else if (cellValue.Contains("VP"))
                    {
                        entity.Post = "1506121006236979084c69c8b702b";
                    }
                    else if (cellValue.Contains("主管"))
                    {
                        entity.Post = "15090709445909814067de2bfaba6";
                    }
                    else if (cellValue.Contains("经理"))
                    {
                        entity.Post = "1509070945263028608992a2f8e11";
                    }
                    else if (cellValue.Contains("总监"))
                    {
                        entity.Post = "1509070946055567858bcb43ff919";
                    }
                    else
                    {
                        entity.Post = "1509070944153467659cb5c9239b9";
                    }

                    break;
                case "固定电话":
                    //固定电话
                    entity.ContactTel = cellValue;
                    break;
                case "手机号码":
                    //手机号码
                    entity.ContactPhone = cellValue;
                    break;
                case "邮箱":
                    entity.EmailAddress = cellValue;
                    break;
                case "所在省份":
                case "省份":
                    //省份
                    cellValue = cellValue.TrimEnd('省');
                    entity.ProvinceName = cellValue;
                    SysCodeTable Province = DefaultListProvider.GetFirstOrDefault("ProvinceCategory", cellValue);
                    if (Province != null)
                    {
                        entity.ProvinceCode = Province.CodeID;
                        entity.ProvinceName = Province.CodeValue;
                    }

                    break;
                case "所在城市":
                case "城市":
                    //城市
                    entity.CityName = cellValue;
                    SysCodeTable city = DefaultListProvider.GetLastOrDefault("ProvinceCategory", cellValue);
                    if (city != null)
                    {
                        entity.CityCode = city.CodeID;
                        entity.CityName = city.CodeValue;
                    }

                    break;
                case "详细地址":
                    entity.AddressDetails = cellValue;
                    break;
                case "官网":
                    entity.HomePage = cellValue;
                    break;
                case "客户来源":
                    if (cellValue.Contains("外部活动"))
                    {
                        entity.CustomerSource = "16081111001246492637d9b402c84";
                    }
                    else if (cellValue.Contains("市场活动"))
                    {
                        entity.CustomerSource = "1608111100334232347fd303afcda";
                    }
                    else if (cellValue.Contains("电话营销"))
                    {
                        entity.CustomerSource = "16081111005628085832ea6709926";
                    }
                    else if (cellValue.Contains("同事") || cellValue.Contains("客户介绍"))
                    {
                        entity.CustomerSource = "1608111101239963895a64938ad4e";
                    }
                    else if (cellValue.Contains("每周一放"))
                    {
                        entity.CustomerSource = "160811110234208344650a0b6ea96";
                    }
                    else if (cellValue.Contains("其他方式"))
                    {
                        entity.CustomerSource = "1608111101339963895a64938a38a";
                    }

                    break;
                case "公司简介":
                    entity.Comments = cellValue;
                    break;
            }
        }




        public bool ImportCustomer(ref string error, TB_Customer entity)
        {

            entity.CustomerName = entity.CustomerName.Trim();
            if (entity.CustomerName.Length < 4)
            {
                error = "客户名称不正确";
                return false;
            }
            if (!m_BLL.IsCreate(currentPerson))
            {
                error = "您没有创建客户权限或超出创建客户数";
                return false;
            }



            entity.CustomerID = Result.GetNewId();
            DateTime date = DateTime.Now;
            entity.BelongingDate = date;
            entity.RelieveDate = date.AddMonths(6);
            entity.SchoolRecruitingQty = 0;//校招职位数量
            entity.SocialRecruitingQty = 0;//社招职位数量
            entity.IsCertification = false;
            entity.IsRegister = false;
            entity.IsFrozen = false;
            entity.VersionNo = 0;
            entity.IsDelete = false;
            entity.CreatedTime = date;
            entity.LastUpdatedTime = date;
            entity.AuditStatus = 0;
            entity.Source = 2;//导入
            //年度第几周
            entity.WeekOfYear = ValueConvert.GetWeekOfYear();
            string returnValue = string.Empty;
            validationErrors.Clear();

            //信用等级计算
            RatingBLL.GetCalcRatingBefore(entity);


            if (m_BLL.Create(ref validationErrors, entity))
            {
                error = "成功";
                LogClassModels.WriteServiceLog(Suggestion.InsertSucceed + "，【导入】客户名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理");//写入日志 
                return true;
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
                error = "错误：" + returnValue;
                LogClassModels.WriteServiceLog(Suggestion.InsertFail + "，【导入】客户名称：" + entity.CustomerName + ",错误：" + returnValue, "客户管理");//写入日志                      
                return false;
            }

        }



        public void SetCustomerKeyValue(Dictionary<int, string> list, int index, string name)
        {
            if (name.Contains("客户名称"))
            {
                list.Add(index, "CustomerName");
                return;
            }
            if (name.Contains("归属人"))
            {
                list.Add(index, "SysPersonID");
                return;
            }
            if (name.Contains("行业类别"))
            {
                list.Add(index, "VocationName");
                return;
            }
            if (name.Contains("所在省份"))
            {
                list.Add(index, "ProvinceName");
                return;
            }
            if (name.Contains("所在城市"))
            {
                list.Add(index, "CityName");
                return;
            }
            if (name.Contains("详细地址"))
            {
                list.Add(index, "AddressDetails");
                return;
            }
            if (name.Contains("联系人"))
            {
                list.Add(index, "ContactPerson");
                return;
            }
            if (name.Contains("手机号码"))
            {
                list.Add(index, "ContactPhone");
                return;
            }
            if (name.Contains("固定电话"))
            {
                list.Add(index, "ContactTel");
                return;
            }
            if (name.Contains("邮箱"))
            {
                list.Add(index, "EmailAddress");
                return;
            }
            if (name.Contains("官网"))
            {
                list.Add(index, "HomePage");
                return;
            }
            if (name.Contains("职位链接"))
            {
                list.Add(index, "PositionLink");
                return;
            }
            if (name.Contains("公司简介"))
            {
                list.Add(index, "Comments");
                return;
            }
        }

        #endregion

        #region 修改客户名称 create by chand 2015-12-02

        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="followID"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public JsonResult GetUpdateCustomerName(string CustomerID, string CustomerName)
        {
            int flag = 0;
            string returnValue = string.Empty;
            string oldName = string.Empty;
            TB_Customer entity = m_BLL.GetById(CustomerID);
            if (entity != null)
            {
                oldName = entity.CustomerName;
                if (m_BLL.GetUpdateCustomerName(ref validationErrors, CustomerID, CustomerName, GetCurrentPersonID()))
                {
                    flag = 1;
                    //写入日志  
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，修改客户名称，原名称“" + oldName + "”修改成“" + CustomerName + "”，客户编号：" + entity.CustomerID, "客户管理");
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
                    LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，修改客户名称失败：" + returnValue, "客户管理");//写入日志

                }

            }
            else
            {
                returnValue = "数据有误，修改失败";
            }
            return Json(
                new
                {
                    Result = flag,
                    Msg = returnValue
                });
        }

        #endregion

        public ActionResult LogoffList()
        {
            return View();
        }

        #region 注销客户 & 激活 create by chand 2016-02-25


        /// <summary>
        /// 注销客户
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public JsonResult GetLogoff(string IDs)
        {
            string returnValue = string.Empty;
            int Result = 0;
            if (!string.IsNullOrWhiteSpace(IDs))
            {
                string[] arr = IDs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    string CurrentPersonID = GetCurrentPersonID();

                    int flag = 0;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        validationErrors.Clear();
                        TB_Customer entity = m_BLL.GetById(arr[i]);
                        if (entity != null)
                        {

                            #region 注销客户
                            if (m_BLL.Delete(ref validationErrors, entity.CustomerID))
                            {
                                flag++;
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户注销，名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理");
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
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，客户注销，名称：" + entity.CustomerName + "，错误：" + returnValue, "客户管理");
                            }
                            #endregion
                        }
                    }
                    if (flag == arr.Length)
                    {
                        //所有客户更新成功
                        Result = 1;
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
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(returnValue))
            {
                returnValue = "请核对输入的数据的格式";
            }
            return Json(new
            {
                Result = Result,
                Msg = returnValue

            });
        }


        /// <summary>
        /// 激活客户
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public JsonResult GetActivating(string IDs)
        {
            string returnValue = string.Empty;
            int Result = 0;
            if (!string.IsNullOrWhiteSpace(IDs))
            {
                string[] arr = IDs.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    string CurrentPersonID = GetCurrentPersonID();

                    int flag = 0;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        validationErrors.Clear();
                        TB_Customer entity = m_BLL.GetById(arr[i]);
                        if (entity != null)
                        {

                            #region 激活客户
                            if (m_BLL.GetActivating(ref validationErrors, arr[i]))
                            {
                                flag++;
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，客户激活，名称：" + entity.CustomerName + "，客户Id：" + entity.CustomerID, "客户管理");
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
                                //写入日志
                                LogClassModels.WriteServiceLog(Suggestion.UpdateFail + "，客户激活，名称：" + entity.CustomerName + "，错误：" + returnValue, "客户管理");
                            }
                            #endregion
                        }
                    }
                    if (flag == arr.Length)
                    {
                        //所有客户更新成功
                        Result = 1;
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
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(returnValue))
            {
                returnValue = "请核对输入的数据的格式";
            }
            return Json(new
            {
                Result = Result,
                Msg = returnValue

            });
        }



        #endregion


        #region 获取 公共池客户数据 create by chand 2015-07-02

        public JsonResult GetLogoffData(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            //search += "DateType&2^";
            List<CustomerModel> queryData = m_BLL.GetLogoffData("", page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    s.BelongingDate,
                    s.CityCode,
                    s.CityName,
                    s.CompanyCertification,
                    s.CompanyID,
                    s.CompanyNatureCode,
                    s.CompanyPreview,
                    s.CompanyStatusCode,
                    s.CreatedBy,
                    s.CreatedTime,
                    s.CustomerFunnel,
                    s.CustomerID,
                    s.CustomerLevel,
                    s.CustomerName,
                    s.EstablishDate,
                    s.HomePage,
                    s.IsCertification,
                    s.IsFrozen,
                    s.IsRegister,
                    s.LastUpdatedBy,
                    s.LastUpdatedTime,
                    s.FollowUpDate,
                    s.Opportunities,
                    s.ReleaseState,
                    s.RelieveDate,
                    s.SchoolRecruitingQty,
                    s.SocialRecruitingQty,
                    s.SysPersonID,
                    s.SysPersonName,
                    s.VocationCode,
                    s.VocationName,
                    s.ContactPerson,
                    s.ContactPhone,
                    s.ContactTel,
                    s.EmailAddress,
                    s.SortColumn,
                    s.GiveMe,
                    s.ToPerson
                }
                   )
            });
        }
        #endregion


        #region 取消所有分享过来的客户 create by chand 2016-05-06

        public JsonResult ReleaseAllShare(string PersonID)
        {
            var account = GetCurrentAccount();
            if (account != null)
            {

                IBLL.ITBCustomerShareBLL m_ShareBLL = new TBCustomerShareBLL();
                if (m_ShareBLL.GetCustomerReleaseAllShare(PersonID, account.Id))
                {
                    //写入日志
                    LogClassModels.WriteServiceLog(Suggestion.UpdateSucceed + "，取消所有分享过来的客户，员工编号：" + PersonID, "客户管理");
                }

            }
            return Json(new
            {
                Result = 1,
                Msg = ""

            });
        }
        #endregion


        #region 薪酬客服可以选择的客户

        public ActionResult ServiceList()
        {
            return View();
        }


        public JsonResult GetServiceList(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SearchPersonID&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetCustomerServiceList(page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }


        #endregion

        #region 通用   选择客户视图
        public ActionResult SelectedList(string type)
        {
            ViewBag.SeachType = type;
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult GetSelectedList(string id, int page, int rows, string order, string sort, string search)
        {

            int total = 0;
            if (string.IsNullOrWhiteSpace(search))
            {
                search = string.Empty;
            }
            search += "SearchPersonID&" + GetCurrentPersonID() + "^";
            var queryData = m_BLL.GetCustomerSelectedList(page, rows, order, sort, search, ref total);

            return Json(new datagrid
            {
                total = total,
                rows = queryData
            });
        }



        #endregion


        public ActionResult BaseInfo(string id)
        {
            var entity = m_BLL.GetById(id);
            if (entity == null)
            {
                return Content("信息不存在！");
            }
            return View(entity);
        }


    }


}
