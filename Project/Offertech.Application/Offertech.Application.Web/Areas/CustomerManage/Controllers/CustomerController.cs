using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Application.Cache;
using Offertech.Application.Code;
using Offertech.Application.Entity.AuthorizeManage;
using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using System.Linq;

namespace Offertech.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class CustomerController : MvcControllerBase
    {
        private CustomerBLL customerbll = new CustomerBLL();
        private CustomerContactBLL customercontactbll = new CustomerContactBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private AreaCache areaCache = new AreaCache();
        private UserCache userCache = new UserCache();
        private CustomerCache customerCache = new CustomerCache();

        #region 视图功能
        /// <summary>
        /// 客户列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 我的客户列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult MyList()
        {
            return View();
        }
        /// <summary>
        /// 客户表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 客户明细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 联系人列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ContactIndex()
        {
            return View();
        }
        /// <summary>
        /// 联系人表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }
        /// <summary>
        /// 公共客户池页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Public()
        {
            return View();
        }
        /// <summary>
        /// 导入客户页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ExcelImportForm()
        {
            return View();
        }
        /// <summary>
        /// 客户改名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ChangeFullName()
        {
            return View();
        }
        /// <summary>
        /// 所有客户查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult AlllList()
        {
            return View();
        }
        /// <summary>
        /// 搜索客户视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// 选择客户视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Selected()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson()
        {
            var data = customerCache.GetList();
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetMyListJson(string queryJson)
        {
            var data = customerbll.GetMyData(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetMyPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = customerbll.GetMyPageData(pagination, queryJson);
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
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = customerbll.GetChildList(pagination, queryJson);
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
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetSelectedPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            DataTable dataTable = null;
            if (OperatorProvider.Provider.Current().LoginInfo.Account.Equals("offercom", StringComparison.CurrentCultureIgnoreCase))
            {
                dataTable = customerbll.GetPageData(pagination, queryJson);
            }
            else
            {
                dataTable = customerbll.GetMyPageData(pagination, queryJson);
            }

            var jsonData = new
            {
                rows = dataTable,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取客户实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetContactListJson(string queryJson)
        {
            var data = customercontactbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="customerId">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetContactByIdJson(string customerId, string keyword = "")
        {
            var data = customercontactbll.GetListByCustomerId(customerId, keyword);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="customerId">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetContactJson(string customerId, string keyword = "")
        {
            var data = customercontactbll.GetContactData(customerId, keyword);
            return ToJsonResult(data);
        }

        /// <summary>
        /// 获取联系人实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetContactFormJson(string keyValue)
        {
            var data = customercontactbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPublicListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = customerbll.GetPublicList(pagination, queryJson);
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
        /// 关键字搜索客户列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSearchJson(string keyword)
        {
            var data = customerbll.GetSearchList(keyword);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSearchIndexJson(string keyword)
        {
            int _pagesize = 10;
            int page = 1;
            int _totalcount = 0;
            List<CustomerEntity> list = CustomerLuceneNet.GetInstance().SearchIndex(keyword, _pagesize, page, out _totalcount);


            return ToJsonResult(list);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = customerbll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除客户数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            customerbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存客户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, CustomerEntity entity)
        {
            string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//客户管理模块
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                entity.TraceUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                entity.TraceUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
                entity.EnabledMark = 1;
                //entity.CustomerSource = "";
                #region 计算企业信用等级

                GetCalcRatingBefore(entity);

                #endregion

            }
            else
            {
                entity.FullName = null;
            }
            customerbll.SaveForm(keyValue, entity, moduleId);

            return Success("操作成功。");
        }
        /// <summary>
        /// 删除联系人数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveContactForm(string keyValue)
        {
            customercontactbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存联系人表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveContactForm(string keyValue, CustomerContactEntity entity)
        {
            customercontactbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 释放客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult GetRelease(string keyValue)
        {
            customerbll.GetRelease(keyValue);
            return Success("释放客户成功。");
        }
        /// <summary>
        /// 提取客户客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult GetExtract(string keyValue)
        {
            customerbll.GetExtract(keyValue);
            return Success("客户提取成功。");
        }


        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="FullName">客户名称</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateFullName(string keyValue, string FullName)
        {
            customerbll.UpdateFullName(keyValue, FullName.Trim());
            return Success("更新成功。");
        }
        #endregion

        #region 计算企业信用等级

        /// <summary>
        /// 计算企业信用等级
        /// </summary>
        /// <param name="entity">客户实体</param>
        private void GetCalcRatingBefore(CustomerEntity entity)
        {
            #region 计算企业信用等级
            if (string.IsNullOrWhiteSpace(entity.RatingScore))
            {
                int sumScore = 0;
                int score = 0;

                if (int.TryParse(dataItemCache.GetDataItemValue(entity.NatureCode), out score))
                {//企业性质
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.RegisterCapital), out score))
                {//注册资金
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.SalesRevenue), out score))
                {//销售收入
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.CustIndustryId), out score))
                {//所属行业
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.CompanySize), out score))
                {//公司规模
                    sumScore += score;
                }
                entity.RatingScore = customerbll.GetCalcRatingBefore(sumScore);
            }
            #endregion
        }

        #endregion

        #region 导入数据

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelImportData()
        {
            int SuccessCount = 0;
            int ErrorCount = 0;
            string virtualPath = "";
            string fullFileName = "";
            string error = string.Empty;
            //读取上传文件
            System.Web.HttpPostedFileBase pstFile = Request.Files["file"];

            if (pstFile != null)
            {
                //上传文件
                if (UploadifyFile(pstFile, ref virtualPath, ref fullFileName))
                {

                    #region 读取Excel内容

                    IWorkbook workbook = null;
                    FileStream fs = null;
                    string fileName = fullFileName;
                    ISheet sheet = null;
                    DataTable data = new DataTable();
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
                        if (sheet == null) //如果没有找到指定的sheetName对应的sheet ，则尝试获取第一个sheet
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

                            #region 导入内容
                            //获取当前用户ID
                            string currentPerson = SystemInfo.CurrentUserId;



                            //记录薪资项目和对应的值
                            Dictionary<string, string> dicList = new Dictionary<string, string>();


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

                                error = string.Empty;
                                CustomerEntity entity = new CustomerEntity();
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
                                            error = SetCustomerValue(entity, TitleList[j], CellValue.Trim());
                                            if (!string.IsNullOrWhiteSpace(error))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    if (error == string.Empty)
                                    {
                                        #region 每周一放

                                        if (entity.CustomerSource == "1cf49dad-eaa0-4b7f-9a34-d6b38a044b84")
                                        {
                                            entity.EnabledMark = 0;
                                        }
                                        #endregion

                                        #region 其他正常导入
                                        else
                                        {
                                            entity.EnabledMark = 1;//
                                            if (string.IsNullOrWhiteSpace(entity.TraceUserId))
                                            {
                                                entity.TraceUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                                                entity.TraceUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
                                            }
                                        }

                                        #endregion

                                        //信用计算
                                        GetCalcRatingBefore(entity);
                                        string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//客户管理模块
                                        customerbll.SaveImportForm("", entity, moduleId);
                                        error = "导入成功";
                                        SuccessCount++;
                                    }
                                    else
                                    {
                                        ErrorCount++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    error = ex.Message;
                                    ErrorCount++;
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
                        using (FileStream fileWrite = new FileStream(fullFileName, FileMode.Create))
                        {
                            workbook.Write(fileWrite);
                        }

                    }
                    catch (Exception ex)
                    {

                    }


                    #endregion

                }
            }
            error = "导入数据不存在，请返回批量导入页面！";
            ViewBag.Error = error;
            //new CustomerLuceneNet().CreateIndex2();
            ViewBag.Url = virtualPath;
            ViewBag.ErrorCount = ErrorCount;
            ViewBag.SuccessCount = SuccessCount;
            return View("ExportError");
        }

        /// <summary>
        /// 设置客户实体数据
        /// </summary>
        /// <param name="entity">客户实体</param>
        /// <param name="title">Excel导入的列名</param>
        /// <param name="cellValue">单元格值</param>
        /// <returns></returns>
        public string SetCustomerValue(CustomerEntity entity, string title, string cellValue)
        {
            string result = string.Empty;
            switch (title)
            {

                case "原主键":
                    entity.ShortName = cellValue;
                    break;
                case "客户名称":
                    cellValue = cellValue.Replace(" ", "");
                    if (cellValue.Length <= 3)
                    {
                        result = "客户名称不完整";
                        break;
                    }
                    //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
                    //if (!reg.IsMatch(cellValue))
                    //{
                    //    result = "客户名称只能包含汉字和括弧";
                    //    break;
                    //}
                    entity.FullName = cellValue;
                    break;
                case "客户级别":

                    var Client_Level = dataItemCache.GetDataItemList("Client_Level").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_Level != null)
                    {
                        entity.CustLevelId = Client_Level.ItemDetailId;
                    }
                    else
                    {
                        result = "客户级别不正确";
                    }
                    break;
                case "企业性质":
                    var Client_NatureCode = dataItemCache.GetDataItemList("Client_NatureCode").FirstOrDefault(s => s.ItemName.Contains(cellValue));
                    if (Client_NatureCode != null)
                    {
                        entity.NatureCode = Client_NatureCode.ItemDetailId;
                    }
                    else if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.NatureCode = dataItemCache.GetDataItemList("Client_NatureCode").First().ItemDetailId;
                    }
                    else
                    {
                        result = "企业性质不正确";
                    }
                    break;
                case "注册资金":
                    var Client_RegisterCapital = dataItemCache.GetDataItemList("Client_RegisterCapital").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_RegisterCapital != null)
                    {
                        entity.RegisterCapital = Client_RegisterCapital.ItemDetailId;
                    }
                    else if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.RegisterCapital = dataItemCache.GetDataItemList("Client_RegisterCapital").First().ItemDetailId;
                    }
                    else
                    {
                        result = "注册资金不正确";
                    }
                    break;
                case "销售收入":
                    var Client_SalesRevenue = dataItemCache.GetDataItemList("Client_SalesRevenue").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_SalesRevenue != null)
                    {
                        entity.SalesRevenue = Client_SalesRevenue.ItemDetailId;
                    }
                    else if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.SalesRevenue = dataItemCache.GetDataItemList("Client_SalesRevenue").First().ItemDetailId;
                    }
                    else
                    {
                        result = "销售收入不正确";
                    }
                    break;
                case "所属行业":

                    var Client_Trade = dataItemCache.GetDataItemList("Client_Trade").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_Trade != null)
                    {
                        entity.CustIndustryId = Client_Trade.ItemDetailId;
                    }
                    else if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.CustIndustryId = dataItemCache.GetDataItemList("Client_Trade").First().ItemDetailId;
                    }
                    else
                    {
                        result = "所属行业不正确";
                    }
                    break;
                case "公司规模":
                    var Client_CompanySize = dataItemCache.GetDataItemList("Client_CompanySize").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_CompanySize != null)
                    {
                        entity.CompanySize = Client_CompanySize.ItemDetailId;
                    }
                    else if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.CompanySize = dataItemCache.GetDataItemList("Client_CompanySize").First().ItemDetailId;
                    }
                    else
                    {
                        result = "公司规模不正确";
                    }
                    break;
                case "客户来源":
                    var Client_CustomerSource = dataItemCache.GetDataItemList("Client_CustomerSource").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_CustomerSource != null)
                    {
                        entity.CustomerSource = Client_CustomerSource.ItemDetailId;
                    }
                    else if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        entity.CustomerSource = dataItemCache.GetDataItemList("Client_CustomerSource").First().ItemDetailId;
                    }
                    break;
                case "联系人":
                    entity.Contact = cellValue;
                    break;
                case "职位":
                    var Client_Post = dataItemCache.GetDataItemList("Client_Post").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_Post != null)
                    {
                        entity.PostId = Client_Post.ItemDetailId;
                    }
                    break;
                case "固定电话":
                    entity.Tel = cellValue;
                    break;
                case "手机号码":
                    entity.Mobile = cellValue;
                    break;
                case "邮箱":
                    entity.Email = cellValue;
                    break;
                case "所在省份":
                case "省份":
                    cellValue = cellValue.TrimEnd('省');
                    var province = areaCache.GetList().FirstOrDefault(s => s.AreaName.Contains(cellValue));
                    if (province != null)
                    {
                        entity.ProvinceId = province.AreaId;
                    }
                    break;
                case "所在城市":
                case "城市":
                    cellValue = cellValue.TrimEnd('市');
                    var city = areaCache.GetList().FirstOrDefault(s => s.AreaName.Contains(cellValue));
                    if (city != null)
                    {
                        entity.CityId = city.AreaId;
                    }
                    break;
                case "详细地址":
                    entity.CompanyAddress = cellValue;
                    break;
                case "官网":
                    entity.CompanySite = cellValue;
                    break;
                case "公司简介":
                    entity.CompanyDesc = cellValue;
                    break;
                case "备注":
                    entity.Description = cellValue;
                    break;
                case "归属人":
                    var user = userCache.GetList().Where(s => s.Account == cellValue).FirstOrDefault();
                    if (user != null)
                    {
                        entity.TraceUserId = user.UserId;
                        entity.TraceUserName = user.RealName;
                    }
                    break;
            }

            return result;
        }



        private Offertech.Application.Busines.PublicInfoManage.FileInfoBLL fileInfoBLL = new Offertech.Application.Busines.PublicInfoManage.FileInfoBLL();


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="Filedata">文件对象</param>
        /// <param name="virtualPath">虚拟路径</param>
        /// <param name="fullFileName">服务器完整路径</param>
        /// <returns></returns>
        [HttpPost]
        public bool UploadifyFile(System.Web.HttpPostedFileBase Filedata, ref string virtualPath, ref string fullFileName)
        {
            try
            {
                Thread.Sleep(500);////延迟500毫秒
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return false;
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                string fileGuid = Guid.NewGuid().ToString();
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);
                fullFileName = this.Server.MapPath(virtualPath);
                //创建文件夹
                string path = Path.GetDirectoryName(fullFileName);
                System.IO.Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    //保存文件
                    Filedata.SaveAs(fullFileName);
                    //文件信息写入数据库
                    FileInfoEntity fileInfoEntity = new FileInfoEntity();
                    fileInfoEntity.Create();
                    fileInfoEntity.FileId = fileGuid;
                    //文件夹Id
                    fileInfoEntity.FolderId = "0";
                    fileInfoEntity.FileName = Filedata.FileName;
                    fileInfoEntity.FilePath = virtualPath;
                    fileInfoEntity.FileSize = filesize.ToString();
                    fileInfoEntity.FileExtensions = FileEextension;
                    fileInfoEntity.FileType = FileEextension.Replace(".", "");
                    fileInfoBLL.SaveForm("", fileInfoEntity);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 导出模板

        public void DownloadFile(string keyValue)
        {
            string filename = Server.UrlDecode("客户导入模板.xls");//返回客户端文件名称
            string filepath = this.Server.MapPath("~/Resource/ExcelTemplate/客户导入模板.xls");
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        #endregion
    }
}
