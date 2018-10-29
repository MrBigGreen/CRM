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
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �����ˣ�chand
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public class CustomerController : MvcControllerBase
    {
        private CustomerBLL customerbll = new CustomerBLL();
        private CustomerContactBLL customercontactbll = new CustomerContactBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private AreaCache areaCache = new AreaCache();
        private UserCache userCache = new UserCache();
        private CustomerCache customerCache = new CustomerCache();

        #region ��ͼ����
        /// <summary>
        /// �ͻ��б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// �ҵĿͻ��б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult MyList()
        {
            return View();
        }
        /// <summary>
        /// �ͻ���ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// �ͻ���ϸҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// ��ϵ���б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ContactIndex()
        {
            return View();
        }
        /// <summary>
        /// ��ϵ�˱�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }
        /// <summary>
        /// �����ͻ���ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Public()
        {
            return View();
        }
        /// <summary>
        /// ����ͻ�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ExcelImportForm()
        {
            return View();
        }
        /// <summary>
        /// �ͻ�����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ChangeFullName()
        {
            return View();
        }
        /// <summary>
        /// ���пͻ���ѯ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult AlllList()
        {
            return View();
        }
        /// <summary>
        /// �����ͻ���ͼ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// ѡ��ͻ���ͼ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Selected()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson()
        {
            var data = customerCache.GetList();
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetMyListJson(string queryJson)
        {
            var data = customerbll.GetMyData(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�ͻ�ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ��ϵ���б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetContactListJson(string queryJson)
        {
            var data = customercontactbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ��ϵ���б�
        /// </summary>
        /// <param name="customerId">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetContactByIdJson(string customerId, string keyword = "")
        {
            var data = customercontactbll.GetListByCustomerId(customerId, keyword);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡ��ϵ���б�
        /// </summary>
        /// <param name="customerId">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetContactJson(string customerId, string keyword = "")
        {
            var data = customercontactbll.GetContactData(customerId, keyword);
            return ToJsonResult(data);
        }

        /// <summary>
        /// ��ȡ��ϵ��ʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetContactFormJson(string keyValue)
        {
            var data = customercontactbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡ�ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// �ؼ��������ͻ��б�
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

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="FullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = customerbll.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ���ͻ�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            customerbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ����ͻ������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, CustomerEntity entity)
        {
            string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//�ͻ�����ģ��
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                entity.TraceUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                entity.TraceUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
                entity.EnabledMark = 1;
                //entity.CustomerSource = "";
                #region ������ҵ���õȼ�

                GetCalcRatingBefore(entity);

                #endregion

            }
            else
            {
                entity.FullName = null;
            }
            customerbll.SaveForm(keyValue, entity, moduleId);

            return Success("�����ɹ���");
        }
        /// <summary>
        /// ɾ����ϵ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveContactForm(string keyValue)
        {
            customercontactbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ������ϵ�˱����������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveContactForm(string keyValue, CustomerContactEntity entity)
        {
            customercontactbll.SaveForm(keyValue, entity);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ͷſͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult GetRelease(string keyValue)
        {
            customerbll.GetRelease(keyValue);
            return Success("�ͷſͻ��ɹ���");
        }
        /// <summary>
        /// ��ȡ�ͻ��ͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult GetExtract(string keyValue)
        {
            customerbll.GetExtract(keyValue);
            return Success("�ͻ���ȡ�ɹ���");
        }


        /// <summary>
        /// �޸Ŀͻ�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="FullName">�ͻ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateFullName(string keyValue, string FullName)
        {
            customerbll.UpdateFullName(keyValue, FullName.Trim());
            return Success("���³ɹ���");
        }
        #endregion

        #region ������ҵ���õȼ�

        /// <summary>
        /// ������ҵ���õȼ�
        /// </summary>
        /// <param name="entity">�ͻ�ʵ��</param>
        private void GetCalcRatingBefore(CustomerEntity entity)
        {
            #region ������ҵ���õȼ�
            if (string.IsNullOrWhiteSpace(entity.RatingScore))
            {
                int sumScore = 0;
                int score = 0;

                if (int.TryParse(dataItemCache.GetDataItemValue(entity.NatureCode), out score))
                {//��ҵ����
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.RegisterCapital), out score))
                {//ע���ʽ�
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.SalesRevenue), out score))
                {//��������
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.CustIndustryId), out score))
                {//������ҵ
                    sumScore += score;
                }
                if (int.TryParse(dataItemCache.GetDataItemValue(entity.CompanySize), out score))
                {//��˾��ģ
                    sumScore += score;
                }
                entity.RatingScore = customerbll.GetCalcRatingBefore(sumScore);
            }
            #endregion
        }

        #endregion

        #region ��������

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
            //��ȡ�ϴ��ļ�
            System.Web.HttpPostedFileBase pstFile = Request.Files["file"];

            if (pstFile != null)
            {
                //�ϴ��ļ�
                if (UploadifyFile(pstFile, ref virtualPath, ref fullFileName))
                {

                    #region ��ȡExcel����

                    IWorkbook workbook = null;
                    FileStream fs = null;
                    string fileName = fullFileName;
                    ISheet sheet = null;
                    DataTable data = new DataTable();
                    int startRow = 0;

                    #region ��ȡExcel ���뵽ϵͳ��
                    try
                    {
                        fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                        if (fileName.IndexOf(".xlsx") > 0) // 2007�汾
                            workbook = new XSSFWorkbook(fs);
                        else if (fileName.IndexOf(".xls") > 0) // 2003�汾
                            workbook = new HSSFWorkbook(fs);


                        sheet = workbook.GetSheet("Sheet1");
                        if (sheet == null) //���û���ҵ�ָ����sheetName��Ӧ��sheet �����Ի�ȡ��һ��sheet
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                        if (sheet != null)
                        {
                            IRow firstRow = sheet.GetRow(0);
                            int cellCount = firstRow.LastCellNum; //һ�����һ��cell�ı�� ���ܵ�����

                            //�����ļ���
                            List<string> TitleList = new List<string>();
                            //����н��Excel�����һ�м���״̬
                            ICell resultCell = firstRow.CreateCell(cellCount + 1);
                            resultCell.SetCellValue("������");

                            #region ��һ���Ƿ���DataTable������

                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue.Trim();
                                    if (cellValue != null)
                                    {
                                        //��ӵ���������
                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                        TitleList.Add(cellValue);
                                    }
                                }
                            }
                            //���һ����ʾ������״̬

                            DataColumn lastColumn = new DataColumn("���");
                            data.Columns.Add(lastColumn);


                            startRow = sheet.FirstRowNum + 1;

                            #endregion

                            #region ��������
                            //��ȡ��ǰ�û�ID
                            string currentPerson = SystemInfo.CurrentUserId;



                            //��¼н����Ŀ�Ͷ�Ӧ��ֵ
                            Dictionary<string, string> dicList = new Dictionary<string, string>();


                            #region ѭ����������
                            //���һ�еı��
                            int rowCount = sheet.LastRowNum;
                            for (int i = startRow; i <= rowCount; ++i)
                            {


                                IRow row = sheet.GetRow(i);
                                if (row == null) continue; //û�����ݵ���Ĭ����null��������������

                                if (row.FirstCellNum > 0) continue;//���Ǵӵ�һ����Ԫ��ʼ

                                DataRow dataRow = data.NewRow();

                                //�����Ŀ
                                dicList.Clear();

                                error = string.Empty;
                                CustomerEntity entity = new CustomerEntity();
                                try
                                {
                                    #region ÿ�е���

                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        if (row.GetCell(j) != null) //ͬ��û�����ݵĵ�Ԫ��Ĭ����null
                                        {
                                            string CellValue = string.Empty;

                                            #region ��ȡ��Ԫ���ֵ
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
                                                    //��Ԫ����
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
                                                    //��������
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
                                                    //�ַ���
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
                                        #region ÿ��һ��

                                        if (entity.CustomerSource == "1cf49dad-eaa0-4b7f-9a34-d6b38a044b84")
                                        {
                                            entity.EnabledMark = 0;
                                        }
                                        #endregion

                                        #region ������������
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

                                        //���ü���
                                        GetCalcRatingBefore(entity);
                                        string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//�ͻ�����ģ��
                                        customerbll.SaveImportForm("", entity, moduleId);
                                        error = "����ɹ�";
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
                        Response.Write("��������ͻ��쳣1: " + ex.Message);
                        error = "��������ͻ��쳣: " + ex.Message;
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
            error = "�������ݲ����ڣ��뷵����������ҳ�棡";
            ViewBag.Error = error;
            //new CustomerLuceneNet().CreateIndex2();
            ViewBag.Url = virtualPath;
            ViewBag.ErrorCount = ErrorCount;
            ViewBag.SuccessCount = SuccessCount;
            return View("ExportError");
        }

        /// <summary>
        /// ���ÿͻ�ʵ������
        /// </summary>
        /// <param name="entity">�ͻ�ʵ��</param>
        /// <param name="title">Excel���������</param>
        /// <param name="cellValue">��Ԫ��ֵ</param>
        /// <returns></returns>
        public string SetCustomerValue(CustomerEntity entity, string title, string cellValue)
        {
            string result = string.Empty;
            switch (title)
            {

                case "ԭ����":
                    entity.ShortName = cellValue;
                    break;
                case "�ͻ�����":
                    cellValue = cellValue.Replace(" ", "");
                    if (cellValue.Length <= 3)
                    {
                        result = "�ͻ����Ʋ�����";
                        break;
                    }
                    //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
                    //if (!reg.IsMatch(cellValue))
                    //{
                    //    result = "�ͻ�����ֻ�ܰ������ֺ�����";
                    //    break;
                    //}
                    entity.FullName = cellValue;
                    break;
                case "�ͻ�����":

                    var Client_Level = dataItemCache.GetDataItemList("Client_Level").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_Level != null)
                    {
                        entity.CustLevelId = Client_Level.ItemDetailId;
                    }
                    else
                    {
                        result = "�ͻ�������ȷ";
                    }
                    break;
                case "��ҵ����":
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
                        result = "��ҵ���ʲ���ȷ";
                    }
                    break;
                case "ע���ʽ�":
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
                        result = "ע���ʽ���ȷ";
                    }
                    break;
                case "��������":
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
                        result = "�������벻��ȷ";
                    }
                    break;
                case "������ҵ":

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
                        result = "������ҵ����ȷ";
                    }
                    break;
                case "��˾��ģ":
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
                        result = "��˾��ģ����ȷ";
                    }
                    break;
                case "�ͻ���Դ":
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
                case "��ϵ��":
                    entity.Contact = cellValue;
                    break;
                case "ְλ":
                    var Client_Post = dataItemCache.GetDataItemList("Client_Post").FirstOrDefault(s => s.ItemName == cellValue);
                    if (Client_Post != null)
                    {
                        entity.PostId = Client_Post.ItemDetailId;
                    }
                    break;
                case "�̶��绰":
                    entity.Tel = cellValue;
                    break;
                case "�ֻ�����":
                    entity.Mobile = cellValue;
                    break;
                case "����":
                    entity.Email = cellValue;
                    break;
                case "����ʡ��":
                case "ʡ��":
                    cellValue = cellValue.TrimEnd('ʡ');
                    var province = areaCache.GetList().FirstOrDefault(s => s.AreaName.Contains(cellValue));
                    if (province != null)
                    {
                        entity.ProvinceId = province.AreaId;
                    }
                    break;
                case "���ڳ���":
                case "����":
                    cellValue = cellValue.TrimEnd('��');
                    var city = areaCache.GetList().FirstOrDefault(s => s.AreaName.Contains(cellValue));
                    if (city != null)
                    {
                        entity.CityId = city.AreaId;
                    }
                    break;
                case "��ϸ��ַ":
                    entity.CompanyAddress = cellValue;
                    break;
                case "����":
                    entity.CompanySite = cellValue;
                    break;
                case "��˾���":
                    entity.CompanyDesc = cellValue;
                    break;
                case "��ע":
                    entity.Description = cellValue;
                    break;
                case "������":
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
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="Filedata">�ļ�����</param>
        /// <param name="virtualPath">����·��</param>
        /// <param name="fullFileName">����������·��</param>
        /// <returns></returns>
        [HttpPost]
        public bool UploadifyFile(System.Web.HttpPostedFileBase Filedata, ref string virtualPath, ref string fullFileName)
        {
            try
            {
                Thread.Sleep(500);////�ӳ�500����
                //û���ļ��ϴ���ֱ�ӷ���
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return false;
                }
                //��ȡ�ļ������ļ���(��������·��)
                //�ļ����·����ʽ��/Resource/ResourceFile/{userId}{data}/{guid}.{��׺��}
                string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
                string fileGuid = Guid.NewGuid().ToString();
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);
                fullFileName = this.Server.MapPath(virtualPath);
                //�����ļ���
                string path = Path.GetDirectoryName(fullFileName);
                System.IO.Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    //�����ļ�
                    Filedata.SaveAs(fullFileName);
                    //�ļ���Ϣд�����ݿ�
                    FileInfoEntity fileInfoEntity = new FileInfoEntity();
                    fileInfoEntity.Create();
                    fileInfoEntity.FileId = fileGuid;
                    //�ļ���Id
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

        #region ����ģ��

        public void DownloadFile(string keyValue)
        {
            string filename = Server.UrlDecode("�ͻ�����ģ��.xls");//���ؿͻ����ļ�����
            string filepath = this.Server.MapPath("~/Resource/ExcelTemplate/�ͻ�����ģ��.xls");
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        #endregion
    }
}
