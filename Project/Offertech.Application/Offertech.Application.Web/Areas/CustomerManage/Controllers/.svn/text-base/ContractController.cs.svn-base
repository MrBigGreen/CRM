using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Busines.CustomerManage;
using Offertech.Util;
using Offertech.Util.WebControl;
using System.Web.Mvc;
using Offertech.Application.Code;
using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Application.Cache;
using System;
using System.Collections.Generic;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.Busines.PublicInfoManage;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Linq;
using Offertech.Util.Extension;
using System.Threading;
using System.Net;
using System.Text;

namespace Offertech.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:51
    /// 描 述：合同管理
    /// </summary>
    public class ContractController : MvcControllerBase
    {
        private ContractBLL contractbll = new ContractBLL();
        private DataItemCache itemCache = new DataItemCache();
        private SmsLogBLL smsLogBLL = new SmsLogBLL();
        private CustomerBLL customerBLL = new CustomerBLL();
        private OrganizeCache organizeCache = new OrganizeCache();


        #region 视图功能

        #region 销售合同

        /// <summary>
        /// 欧孚合同
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OfferForm()
        {
            return View();
        }
        /// <summary>
        /// 欧孚合同列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OfferList()
        {
            return View();
        }
        /// <summary>
        /// 博尔捷合同
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult BridgeForm()
        {
            return View();
        }
        /// <summary>
        /// 博尔捷合同列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult BridgeList()
        {
            return View();
        }

        /// <summary>
        /// 销售合同详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SalesDetail()
        {
            return View();
        }


        #endregion

        #region 客服合同

        /// <summary>
        /// 欧孚合同
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuOfferForm()
        {
            return View();
        }
        /// <summary>
        /// 欧孚合同列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuOfferList()
        {
            return View();
        }
        /// <summary>
        /// 博尔捷合同
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuBridgeForm()
        {
            return View();
        }
        /// <summary>
        /// 博尔捷合同列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuBridgeList()
        {
            return View();
        }

        /// <summary>
        /// 客服合同详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuDetail()
        {
            return View();
        }


        #endregion

        /// <summary>
        /// 所有合同列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllList()
        {
            return View();
        }


        /// <summary>
        /// 导入合同页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ExcelImportForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetPageList(pagination, queryJson).ToAuthorizeData();
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = contractbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = contractbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取欧孚销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetOfferDataBySaleJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetOfferDataBySale(pagination, queryJson);
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
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetBridgeDataBySaleJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetBridgeDataBySale(pagination, queryJson);
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
        /// 获取欧孚客服合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetOfferDataByKeFuJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetOfferDataByKeFu(pagination, queryJson);
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
        /// 获取欧孚客服合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetBridgeDataByKeFuJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetBridgeDataByKeFu(pagination, queryJson);
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
        /// 获取所有合同数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageDataJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = contractbll.GetPageData(pagination, queryJson);
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
            contractbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ContractModel model)
        {
            contractbll.SaveForm(keyValue, model);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存表单新增
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult AddForm(string keyValue, ContractModel model)
        {
            ContractEntity entity = new ContractEntity();
            entity.ContractId = model.ContractId;
            entity.ContractMoney = model.ContractMoney;
            entity.ContractType = model.ContractType;
            entity.CustomerId = model.CustomerId;
            entity.CustomerName = model.CustomerName;
            entity.Deadline = model.Deadline;
            entity.Description = model.Description;
            entity.EffectiveDate = model.EffectiveDate;
            entity.PaymentMethod = model.PaymentMethod;
            entity.PhoneNumber = model.PhoneNumber;
            entity.ProjectLeader = model.ProjectLeader;
            entity.ServiceType = model.ServiceType.TrimEnd(new char[] { ',', ';' });
            entity.SignSubject = model.SignSubject;
            entity.SignSubjectId = model.SignSubjectId;
            entity.SignType = model.SignType;
            entity.ContractCode = model.ContractCode;


            List<ContractServiceEntity> serviceEntityList = new List<ContractServiceEntity>();
            if (!string.IsNullOrWhiteSpace(model.ServiceTypeId))
            {
                var arrIds = model.ServiceTypeId.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                var arrNames = model.ServiceType.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrIds.Length; i++)
                {
                    ContractServiceEntity serviceEntity = new ContractServiceEntity();
                    serviceEntity.ServiceTypeId = arrIds[i];
                    serviceEntity.ServiceType = arrNames[i];
                    serviceEntity.ContractId = model.ContractId;
                    serviceEntity.Create();
                    serviceEntityList.Add(serviceEntity);
                    //合同编码的前置
                    if (string.IsNullOrWhiteSpace(entity.ContractCode))
                    {
                        var dataItem = itemCache.GetDataItem(serviceEntity.ServiceTypeId);
                        if (dataItem != null)
                        {
                            entity.ContractCode = dataItem.ItemValue;
                        }
                    }
                }
            }


            contractbll.AddForm(entity, serviceEntityList);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存表单新增
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult KeFuAddForm(string keyValue, ContractModel model)
        {
            if (string.IsNullOrWhiteSpace(model.VerificationCode))
            {
                return Error("验证码不能为空，请输入正确的验证码");
            }
            var sms = smsLogBLL.GetLatestCurrent(model.PhoneNumber.ToString(), 1200);
            if (sms == null || model.VerificationCode != sms.MsgValue)
            {
                return Error("您输入的验证码不正确");
            }

            ContractEntity entity = new ContractEntity();
            entity.ContractId = model.ContractId;
            entity.ContractMoney = model.ContractMoney;
            entity.ContractType = model.ContractType;
            entity.CustomerId = model.CustomerId;
            entity.CustomerName = model.CustomerName;
            entity.Deadline = model.Deadline;
            entity.Description = model.Description;
            entity.EffectiveDate = model.EffectiveDate;
            entity.PaymentMethod = model.PaymentMethod;
            entity.PhoneNumber = model.PhoneNumber;
            entity.ProjectLeader = model.ProjectLeader;
            entity.ServiceType = model.ServiceType.TrimEnd(new char[] { ',', ';' });
            entity.SignSubject = model.SignSubject;
            entity.SignSubjectId = model.SignSubjectId;
            entity.SignType = model.SignType;
            entity.ContractCode = model.ContractCode;
            entity.CreateUserName = model.CreateUserName;

            List<ContractServiceEntity> serviceEntityList = new List<ContractServiceEntity>();
            if (!string.IsNullOrWhiteSpace(model.ServiceTypeId))
            {
                var arrIds = model.ServiceTypeId.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                var arrNames = model.ServiceType.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arrIds.Length; i++)
                {
                    ContractServiceEntity serviceEntity = new ContractServiceEntity();
                    serviceEntity.ServiceTypeId = arrIds[i];
                    serviceEntity.ServiceType = arrNames[i];
                    serviceEntity.ContractId = model.ContractId;
                    serviceEntity.Create();
                    serviceEntityList.Add(serviceEntity);
                    //合同编码的前置
                    if (string.IsNullOrWhiteSpace(entity.ContractCode))
                    {
                        var dataItem = itemCache.GetDataItem(serviceEntity.ServiceTypeId);
                        if (dataItem != null)
                        {
                            entity.ContractCode = dataItem.ItemValue;
                        }
                    }
                }
            }
            string moduleCode = string.Empty;
            if (entity.ContractType == 21)
            {
                moduleCode = "10007";
            }
            else
            {
                moduleCode = "10008";
            }

            contractbll.AddForm(entity, serviceEntityList, moduleCode, moduleCode);
            return Success("操作成功。");
        }
        #endregion

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        [HttpGet]
        public ActionResult GetSecurityCode(string mobileCode)
        {
            if (!ValidateUtil.IsValidMobile(mobileCode))
            {
                throw new Exception("手机格式不正确,请输入正确格式的手机号码。");
            }
            var sms = smsLogBLL.GetLatestCurrent(mobileCode, 120);
            if (sms != null)
            {
                throw new Exception("验证码已发送，请勿重复获取");
            }
            string SecurityCode = CommonHelper.RndNum(6);
            if (!string.IsNullOrEmpty(SecurityCode))
            {
                SmsLogEntity smsLog = new SmsLogEntity();
                smsLog.MobileNumber = mobileCode;
                smsLog.MsgContent = "验证码 " + SecurityCode + "，客服创建合同，20分钟内验证码有效，切勿将验证码泄露于他人。/退订短信回TD【博尔捷人才】";
                smsLog.MsgValue = SecurityCode;
                //smsLogBLL.SaveBySend(smsLog);
                string url = "http://dxjk.51lanz.com/LANZGateway/DirectSendSMSs.asp";
                string SMSparameters = @"UserID=998695&Account=szbridgehr&Password=503ADDD9B727F6F9F6A37F7469A42CC1593F2B43&Content=" + System.Web.HttpUtility.UrlEncode(smsLog.MsgContent, Encoding.GetEncoding("GB2312")) + "&Phones=" + mobileCode;

                string targeturl = url.Trim().ToString();
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                string res = HttpSMSPost(hr, SMSparameters);
                Response.Write(res);
                smsLogBLL.SaveSmsLog(smsLog);
            }
            return Success("获取成功。");
        }
        public string HttpSMSPost(HttpWebRequest hr, string parameters)
        {
            string strRet = null;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(parameters);

            try
            {
                hr.KeepAlive = false;
                hr.Method = "POST";
                hr.ContentType = "application/x-www-form-urlencoded";
                hr.ContentLength = data.Length;
                Stream newStream = hr.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                HttpWebResponse myResponse = (HttpWebResponse)hr.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                strRet = reader.ReadToEnd();
                reader.Close();
                myResponse.Close();

            }
            catch (Exception ex)
            {
                strRet = ex.ToString();
            }
            return strRet;
        }
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
                                ContractEntity entity = new ContractEntity();
                                List<ContractServiceEntity> serviceEntityList = new List<ContractServiceEntity>();
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
                                            #region 服务类型
                                            if (TitleList[j] == "服务类型")
                                            {
                                                string[] strArr = CellValue.Split(new char[] { ',', ';', '^', '|', '，' }, StringSplitOptions.RemoveEmptyEntries);
                                                if (strArr.Length > 0)
                                                {
                                                    for (int q = 0; q < strArr.Length; q++)
                                                    {
                                                        var type = strArr[q];
                                                        var productInfo = itemCache.GetDataItemList("Client_ProductInfo").FirstOrDefault(s => s.ItemName == type);
                                                        if (productInfo != null)
                                                        {
                                                            ContractServiceEntity serviceEntity = new ContractServiceEntity();
                                                            serviceEntity.ServiceTypeId = productInfo.ItemDetailId;
                                                            serviceEntity.ServiceType = productInfo.ItemName;
                                                            serviceEntity.ContractId = "";
                                                            serviceEntity.Create();
                                                            serviceEntityList.Add(serviceEntity);
                                                            //合同编码的前置
                                                            if (string.IsNullOrWhiteSpace(entity.ContractCode))
                                                            {
                                                                var dataItem = itemCache.GetDataItem(serviceEntity.ServiceTypeId);
                                                                if (dataItem != null)
                                                                {
                                                                    entity.ContractCode = dataItem.ItemValue;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (serviceEntityList.Count < 1)
                                                {
                                                    error = "服务类型数据有误";
                                                    break;
                                                }
                                                var serviceList = serviceEntityList.Select(s => s.ServiceType);
                                                entity.ServiceType = string.Join(",", serviceList);

                                            }
                                            #endregion
                                            error = SetEntityValue(entity, TitleList[j], CellValue.Trim());
                                            if (!string.IsNullOrWhiteSpace(error))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    if (error == string.Empty)
                                    {
                                        string moduleCode = string.Empty;
                                        if (entity.ContractType == 21)
                                        {
                                            moduleCode = "10007";
                                        }
                                        else
                                        {
                                            moduleCode = "10008";
                                        }
                                        contractbll.AddForm(entity, serviceEntityList, "", moduleCode);
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
        public string SetEntityValue(ContractEntity entity, string title, string cellValue)
        {
            string result = string.Empty;
            switch (title)
            {
                case "客户名称":
                    if (cellValue.Length <= 4)
                    {
                        result = "公司名称不完整";
                        break;
                    }
                    System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
                    if (!reg.IsMatch(cellValue))
                    {
                        result = "客户名称只能包含汉字和括弧";
                        break;
                    }
                    var customer = customerBLL.GetEntityByName(cellValue);
                    if (customer == null)
                    {
                        result = "客户不存在";
                        break;
                    }
                    entity.CustomerName = customer.FullName;
                    entity.CustomerId = customer.CustomerId;
                    break;
                case "签约公司":
                    var organize = organizeCache.GetList().FirstOrDefault(t => t.FullName == cellValue); ;
                    if (organize == null)
                    {
                        result = "签约公司不正确";
                        break;
                    }
                    entity.SignSubject = organize.FullName;
                    entity.SignSubjectId = organize.OrganizeId;
                    break;
                case "合同类型":
                    if (cellValue.Contains("欧孚合同"))
                    {
                        entity.ContractType = 21;
                    }
                    else if (cellValue.Contains("博尔捷合同"))
                    {
                        entity.ContractType = 22;
                    }
                    break;
                case "服务类型":
                    break;
                case "合同起始日期":
                case "开始日期":
                case "生效日期":
                    entity.EffectiveDate = cellValue.ToDate();
                    break;
                case "合同终止日期":
                case "结束日期":
                case "截止日期":
                    entity.Deadline = cellValue.ToDate();
                    break;
                case "付款方式":
                    entity.PaymentMethod = cellValue;
                    break;
                case "签约类型":
                    if (cellValue.Contains("新签"))
                    {
                        entity.SignType = 1;
                    }
                    else
                    {
                        entity.SignType = 2;
                    }

                    break;
                case "合同金额":
                    entity.ContractMoney = cellValue;
                    break;
                case "项目负责人":
                    entity.ProjectLeader = cellValue;
                    break;
                case "手机号码":
                    entity.PhoneNumber = cellValue.ToLong();
                    break;
                case "备注":
                    entity.Description = cellValue;
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
            string filename = Server.UrlDecode("合同导入模板.xls");//返回客户端文件名称
            string filepath = this.Server.MapPath("~/Resource/ExcelTemplate/合同导入模板.xls");
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }

        #endregion
    }
}
