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
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 13:51
    /// �� ������ͬ����
    /// </summary>
    public class ContractController : MvcControllerBase
    {
        private ContractBLL contractbll = new ContractBLL();
        private DataItemCache itemCache = new DataItemCache();
        private SmsLogBLL smsLogBLL = new SmsLogBLL();
        private CustomerBLL customerBLL = new CustomerBLL();
        private OrganizeCache organizeCache = new OrganizeCache();


        #region ��ͼ����

        #region ���ۺ�ͬ

        /// <summary>
        /// ŷ�ں�ͬ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OfferForm()
        {
            return View();
        }
        /// <summary>
        /// ŷ�ں�ͬ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult OfferList()
        {
            return View();
        }
        /// <summary>
        /// �����ݺ�ͬ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult BridgeForm()
        {
            return View();
        }
        /// <summary>
        /// �����ݺ�ͬ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult BridgeList()
        {
            return View();
        }

        /// <summary>
        /// ���ۺ�ͬ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SalesDetail()
        {
            return View();
        }


        #endregion

        #region �ͷ���ͬ

        /// <summary>
        /// ŷ�ں�ͬ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuOfferForm()
        {
            return View();
        }
        /// <summary>
        /// ŷ�ں�ͬ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuOfferList()
        {
            return View();
        }
        /// <summary>
        /// �����ݺ�ͬ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuBridgeForm()
        {
            return View();
        }
        /// <summary>
        /// �����ݺ�ͬ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult KeFuBridgeList()
        {
            return View();
        }

        /// <summary>
        /// �ͷ���ͬ����
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
        /// ���к�ͬ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllList()
        {
            return View();
        }


        /// <summary>
        /// �����ͬҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ExcelImportForm()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = contractbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = contractbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// ��ȡŷ�����ۺ�ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ���������ۺ�ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡŷ�ڿͷ���ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡŷ�ڿͷ���ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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
        /// ��ȡ���к�ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
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

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            contractbll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="model">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ContractModel model)
        {
            contractbll.SaveForm(keyValue, model);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="model">ʵ�����</param>
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
                    //��ͬ�����ǰ��
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
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="model">ʵ�����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult KeFuAddForm(string keyValue, ContractModel model)
        {
            if (string.IsNullOrWhiteSpace(model.VerificationCode))
            {
                return Error("��֤�벻��Ϊ�գ���������ȷ����֤��");
            }
            var sms = smsLogBLL.GetLatestCurrent(model.PhoneNumber.ToString(), 1200);
            if (sms == null || model.VerificationCode != sms.MsgValue)
            {
                return Error("���������֤�벻��ȷ");
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
                    //��ͬ�����ǰ��
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
            return Success("�����ɹ���");
        }
        #endregion

        /// <summary>
        /// ��ȡ��֤��
        /// </summary>
        /// <param name="mobileCode">�ֻ�����</param>
        /// <returns>����6λ����֤��</returns>
        [HttpGet]
        public ActionResult GetSecurityCode(string mobileCode)
        {
            if (!ValidateUtil.IsValidMobile(mobileCode))
            {
                throw new Exception("�ֻ���ʽ����ȷ,��������ȷ��ʽ���ֻ����롣");
            }
            var sms = smsLogBLL.GetLatestCurrent(mobileCode, 120);
            if (sms != null)
            {
                throw new Exception("��֤���ѷ��ͣ������ظ���ȡ");
            }
            string SecurityCode = CommonHelper.RndNum(6);
            if (!string.IsNullOrEmpty(SecurityCode))
            {
                SmsLogEntity smsLog = new SmsLogEntity();
                smsLog.MobileNumber = mobileCode;
                smsLog.MsgContent = "��֤�� " + SecurityCode + "���ͷ�������ͬ��20��������֤����Ч��������֤��й¶�����ˡ�/�˶����Ż�TD���������˲š�";
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
            return Success("��ȡ�ɹ���");
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
                                ContractEntity entity = new ContractEntity();
                                List<ContractServiceEntity> serviceEntityList = new List<ContractServiceEntity>();
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
                                            #region ��������
                                            if (TitleList[j] == "��������")
                                            {
                                                string[] strArr = CellValue.Split(new char[] { ',', ';', '^', '|', '��' }, StringSplitOptions.RemoveEmptyEntries);
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
                                                            //��ͬ�����ǰ��
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
                                                    error = "����������������";
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
        public string SetEntityValue(ContractEntity entity, string title, string cellValue)
        {
            string result = string.Empty;
            switch (title)
            {
                case "�ͻ�����":
                    if (cellValue.Length <= 4)
                    {
                        result = "��˾���Ʋ�����";
                        break;
                    }
                    System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
                    if (!reg.IsMatch(cellValue))
                    {
                        result = "�ͻ�����ֻ�ܰ������ֺ�����";
                        break;
                    }
                    var customer = customerBLL.GetEntityByName(cellValue);
                    if (customer == null)
                    {
                        result = "�ͻ�������";
                        break;
                    }
                    entity.CustomerName = customer.FullName;
                    entity.CustomerId = customer.CustomerId;
                    break;
                case "ǩԼ��˾":
                    var organize = organizeCache.GetList().FirstOrDefault(t => t.FullName == cellValue); ;
                    if (organize == null)
                    {
                        result = "ǩԼ��˾����ȷ";
                        break;
                    }
                    entity.SignSubject = organize.FullName;
                    entity.SignSubjectId = organize.OrganizeId;
                    break;
                case "��ͬ����":
                    if (cellValue.Contains("ŷ�ں�ͬ"))
                    {
                        entity.ContractType = 21;
                    }
                    else if (cellValue.Contains("�����ݺ�ͬ"))
                    {
                        entity.ContractType = 22;
                    }
                    break;
                case "��������":
                    break;
                case "��ͬ��ʼ����":
                case "��ʼ����":
                case "��Ч����":
                    entity.EffectiveDate = cellValue.ToDate();
                    break;
                case "��ͬ��ֹ����":
                case "��������":
                case "��ֹ����":
                    entity.Deadline = cellValue.ToDate();
                    break;
                case "���ʽ":
                    entity.PaymentMethod = cellValue;
                    break;
                case "ǩԼ����":
                    if (cellValue.Contains("��ǩ"))
                    {
                        entity.SignType = 1;
                    }
                    else
                    {
                        entity.SignType = 2;
                    }

                    break;
                case "��ͬ���":
                    entity.ContractMoney = cellValue;
                    break;
                case "��Ŀ������":
                    entity.ProjectLeader = cellValue;
                    break;
                case "�ֻ�����":
                    entity.PhoneNumber = cellValue.ToLong();
                    break;
                case "��ע":
                    entity.Description = cellValue;
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
            string filename = Server.UrlDecode("��ͬ����ģ��.xls");//���ؿͻ����ļ�����
            string filepath = this.Server.MapPath("~/Resource/ExcelTemplate/��ͬ����ģ��.xls");
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }

        #endregion
    }
}
