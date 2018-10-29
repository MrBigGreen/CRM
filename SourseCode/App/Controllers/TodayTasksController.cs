using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.BLL;
using Models;
using CRM.DAL;
using Common;
using CRM.App.UnCallWebService;
using System.Xml;

namespace CRM.App.Controllers
{
    /// <summary>
    /// 今日工作
    /// </summary>
    [SupportFilter]
    public class TodayTasksController : BaseController
    {
        IBLL.ITBCustomerFollowBLL m_BLL;
        public TodayTasksController()
            : this(new TBCustomerFollowBLL()) { }
        public TodayTasksController(TBCustomerFollowBLL bll)
        {
            m_BLL = bll;
        }


        public UncallAPIPortTypeClient unCallAPI = new UncallAPIPortTypeClient();

        private string CustomerCallTel
        {
            get
            {
                var CustomerCallTel = ControllerContext.HttpContext.Session["CustomerCallTel"] as string;
                if (null == CustomerCallTel)
                {
                    CustomerCallTel = "";
                    ControllerContext.HttpContext.Session["CustomerCallTel"] = CustomerCallTel;
                }
                return CustomerCallTel;
            }
            set
            {
                ControllerContext.HttpContext.Session["CustomerCallTel"] = value;
            }
        }

        #region ==加载、弹框==
        /// <summary>
        /// 我的任务
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Account account = GetCurrentAccount();
            List<SysPerson> list = m_BLL.GetTeamPersons(account.Id);
            ViewBag.TeamPersons = list;
            List<SysField> listF = m_BLL.GetFollowMode();
            ViewBag.FollowMode = listF;
            return View();
        }

        //跟进历史(电话/当面)
        public ActionResult FollowUpIndex()
        {
            Account account = GetCurrentAccount();
            //List<SysPerson> list = m_BLL.GetTeamPersons(account.Id);
            //ViewBag.TeamPersons = list;

            var PersonList = new SysPersonBLL().GetPersonVisibility(account.Id);
            if (PersonList.Count > 0 && PersonList.Count < 50)
            {
                ViewBag.PersonList = new SelectList(PersonList, "SysPersonID", "SysPersonName");
            }


            ViewBag.SysPersonID = account.Id;
            return View();
        }

        ////呼叫历史
        //public ActionResult CallLogIndex()
        //{
        //    Account account = GetCurrentAccount();
        //    ViewBag.MyPhone = account.MobilePhoneNumber;
        //    ViewBag.MyQQ = account.CompanyQQ;

        //    return View();
        //}

        /// <summary>
        /// 查看联系方式
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public ActionResult ShowContactsInfo(string CustomerID, string fid)
        {
            List<TB_CustomerContact> list = m_BLL.GetContactsInfo(CustomerID);
            List<TB_Customer> listF = m_BLL.GetCustomerInfo(CustomerID);
            ViewBag.ContactsInfo = list;
            ViewBag.CustomerInfo = listF;
            ViewBag.fid = fid;
            return View();
        }
        /// <summary>
        /// 播放录音
        /// </summary>
        /// <param name="FileUrl"></param>
        /// <returns></returns>
        public ActionResult ShowCallRecording(string FileUrl)
        {

            ViewBag.FileUrl = FileUrl;
            return View();
        }

        /// <summary>
        /// 拨打电话
        /// </summary>
        /// <param name="tel"></param>
        /// <param name="personTelCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CallTel(string tel, string personTelCode)
        {
            if (string.IsNullOrWhiteSpace(personTelCode))
            {
                SysPerson entity = new SysPersonBLL().GetById(GetCurrentPersonID());
                personTelCode = entity.TelephoneExt;
            }
            bool isCall = false;
            try
            {
                if (tel.Contains('-'))
                {
                    string[] tels = tel.Split('-');
                    tel = tels[0] + tel[1];
                }
                string isCallXML = unCallAPI.OnClickCall(personTelCode, tel, "");
                //写入日志  
                LogClassModels.WriteServiceLog(Suggestion.OperationSucceed + "，呼叫电话：" + personTelCode + ", 结果：" + isCallXML, "呼叫电话");

                XmlDocument dom = new XmlDocument();
                dom.LoadXml(isCallXML);
                XmlElement root = dom.DocumentElement;
                foreach (XmlNode node in root)
                {
                    if (node.Name == "result")
                    {
                        if (node.InnerText == "1")
                        {
                            isCall = true;
                            break;
                        }
                    }
                }
                CustomerCallTel = tel;



            }
            catch (Exception ex)
            {
                //写入日志  
                LogClassModels.WriteServiceLog(Suggestion.OperationSucceed + "，呼叫电话：" + personTelCode + ", 异常：" + ex.Message, "呼叫电话");
            }

            return Json(new { IsSuccess = isCall });
        }



        /// <summary>
        /// 记录跟进信息
        /// </summary>
        /// <returns></returns>
        public ActionResult FollowUpEdit(string CustomerFollowID, string CustomerID, string CityName, string CityCode, string AddressDetails, string ProvinceName, string ProvinceCode, string DistrictName, string DistrictCode, string Fcode)
        {
            ViewBag.CustomerFollowID = CustomerFollowID;
            ViewBag.CustomerID = CustomerID;
            ViewBag.CityName = CityName;
            ViewBag.CityCode = CityCode;
            ViewBag.AddressDetails = AddressDetails;
            ViewBag.ProvinceName = ProvinceName;
            ViewBag.ProvinceCode = ProvinceCode;
            ViewBag.DistrictName = DistrictName;
            ViewBag.DistrictCode = DistrictCode;
            ViewBag.Fcode = Fcode;
            List<SysField> listF = m_BLL.GetFollowMode();
            ViewBag.FollowMode = listF;
            return View();
        }

        #endregion

        #region ==加载列表==

        /// <summary>
        /// 我的任务
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
            string selectParm = Request["state"] == null ? "" : Request["state"].ToString();//查询条件


            List<CustomerFollowModel> queryData = m_BLL.GetCustomerFollowTask(account.Id, account.FollowType, order, sort, search, page, rows, ref total);
            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    CustomerFollowID = s.CustomerFollowID,
                    ReservationDate = s.ReservationDate.Value.ToString("yyyy-MM-dd HH:mm"),
                    CustomerName = s.CustomerName,
                    CityName = s.CityName,
                    CityCode = s.CityCode,
                    CustomerLevel = s.CustomerLevel,
                    MyName = s.MyName,
                    FollowUpCategory = s.FollowUpCategory,
                    FollowUpMode = s.FollowUpMode,
                    CustomerID = s.CustomerID,
                    FollowUpPurpose = s.FollowUpPurpose,
                    AddressDetails = s.AddressDetails,
                    ProvinceCode = s.ProvinceCode,
                    ProvinceName = s.ProvinceName,
                    DistrictCode = s.DistrictCode,
                    DistrictName = s.DistrictName,
                    Telephone = s.Telephone,
                    TelephoneExt = s.TelephoneExt,
                    CustomerFunnel = s.CustomerFunnel,
                    CustomerFunnelName = s.CustomerFunnelName,
                    FollowBack = s.FollowBack,
                    PositionLink = s.PositionLink

                }

                )
            });

        }
        /// <summary>
        /// 电话联系历史记录
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCallHistroy(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;
            //string selectParm = Request["state"] == null ? "" : Request["state"].ToString();//查询条件
            // List<CustomerFollowModel> queryData = m_BLL.GetCallHistroy(account.Id, page, rows, order, sort, selectParm, ref total);

            if (string.IsNullOrWhiteSpace(search) || search.IndexOf("SysPersonID") < 0)
            {
                search += "SysPersonID&" + account.Id + "^";
            }
            search += "FollowUpMode&1506251032299623134259baff7c9^";
            List<CustomerFollowModel> queryData = m_BLL.GetFollowBackHistory(page, rows, search, ref total);


            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    CustomerFollowID = s.CustomerFollowID,
                    FollowUpDate = s.FollowUpDate,
                    CustomerName = s.CustomerName,
                    ContactName = s.ContactName,
                    CityCode = s.CityCode,
                    CityName = s.CityName,
                    CustomerLevel = s.CustomerLevel,
                    SysPersonID = s.SysPersonID,
                    MyName = s.MyName,
                    CallPhone = s.CallPhone,
                    CustomerFunnel = s.CustomerFunnel,
                    ContactState = s.ContactState,
                    CustomerID = s.CustomerID,
                    Opportunities = s.Opportunities,
                    FileUrl = s.FileUrl,
                    s.Remark
                }

                )
            });
        }
        /// <summary>
        /// 当面拜访历史记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMeetingHistroy(int page, int rows, string order, string sort, string search)
        {
            Account account = GetCurrentAccount();
            int total = 0;
            if (string.IsNullOrWhiteSpace(search) || search.IndexOf("SysPersonID") < 0)
            {
                search += "SysPersonID&" + account.Id + "^";
            }
            search += "FollowUpMode&1506251033338479674bf54656731^";
            List<CustomerFollowModel> queryData = m_BLL.GetFollowBackHistory(page, rows, search, ref total);


            return Json(new datagrid
            {
                total = total,
                rows = queryData.Select(s => new
                {
                    CustomerFollowID = s.CustomerFollowID,
                    FollowUpDate = s.FollowUpDate,
                    CustomerName = s.CustomerName,
                    ContactName = s.ContactName,
                    CityCode = s.CityCode,
                    CityName = s.CityName,
                    CustomerLevel = s.CustomerLevel,
                    SysPersonID = s.SysPersonID,
                    MyName = s.MyName,
                    CallPhone = s.CallPhone,
                    CustomerFunnel = s.CustomerFunnel,
                    CustomerID = s.CustomerID,
                    Opportunities = s.Opportunities,
                    s.Remark
                }

                )
            });
        }

        #endregion

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
            var a = Json(jsonInfo, "text/html", JsonRequestBehavior.AllowGet);
            return a;

        }

        #endregion

        #region ==做跟进记录==


        /// <summary>
        /// 填写跟进记录
        /// </summary>
        /// <param name="customerFollowID">任务ID</param>
        /// <param name="isLine">是否联系上</param>
        /// <param name="isNextType">跟进方式</param>
        /// <param name="customerPhone">联系人</param>
        /// <param name="customerLevel">对方级别</param>
        /// <param name="customerFeedback">客户反馈</param>
        /// <param name="customerFunnel">客户进程</param>
        /// <param name="customerOffer">商机判断</param>
        /// <param name="customerHandle">处理方式</param>
        /// <param name="callDate">下次跟进时间</param>
        /// <param name="remark">客户反馈的需求描述记录</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="cityCode">城市Code</param>
        /// <param name="addressDetails">具体地址</param>
        /// <returns></returns>
        //public ActionResult AddFollowUpInfo(string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFeedback, string customerFunnel, string customerOffer, string customerHandle, string callDate, string remark, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string Fcode)
        [HttpPost]
        public ActionResult AddFollowUpInfo(string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFunnel, string customerOffer, string callDate, string remark, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string Fcode)
        {
            string uid = "";
            string lastUid = "";
            XmlDocument dom = new XmlDocument();
            XmlNode root = null;
            if (isLine == "1")
            {
                for (int i = 0; i < 100; i++)
                {
                    string UidXML = unCallAPI.popEvent(Fcode);
                    dom = new XmlDocument();

                    dom.LoadXml(UidXML);
                    root = dom.SelectSingleNode("//result");
                    if (root != null)
                    {
                        var result = root.InnerText;
                        if (result == "1")
                        {
                            root = dom.SelectSingleNode("//popEvent");
                            uid = (root.SelectSingleNode("uid")).InnerText;
                            if (!string.IsNullOrEmpty(uid))
                            {
                                lastUid = uid;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }


                }
            }


            string fileUrl = "";
            if (lastUid != "")
            {
                string UrlXML = unCallAPI.getRecording(lastUid);
                dom = new XmlDocument();
                dom.LoadXml(UrlXML);
                root = dom.SelectSingleNode("//result");

                if (root != null)
                {
                    var result = root.InnerText;
                    if (result == "1")
                    {
                        root = dom.SelectSingleNode("//getRecording");
                        fileUrl = root.InnerText.Split(':')[1];
                    }
                }
            }

            Account account = GetCurrentAccount();
            //bool isUpdate = m_BLL.AddFollowUpInfo(customerFollowID, isLine, isNextType, customerPhone, customerLevel, customerFeedback, customerFunnel, customerOffer, customerHandle, callDate, remark, account.Id, customerID, cityName, cityCode, customerPurpose, addressDetails, provinceName, provinceCode, districtName, districtCode, fileUrl);
            bool isUpdate = m_BLL.AddFollowUpInfo(customerFollowID, isLine, isNextType, customerPhone, customerLevel, customerFunnel, customerOffer, callDate, remark, account.Id, customerID, cityName, cityCode, customerPurpose, addressDetails, provinceName, provinceCode, districtName, districtCode, fileUrl);
            //bool isUpdate = false;
            return Json(new { IsSuccess = isUpdate });
        }



        #endregion

        #region ==导出==
        /// <summary>
        ///  导出Excle /*在6.0版本中 新增*/
        /// </summary>
        /// <param name="param">Flexigrid传过到后台的参数</param>
        /// <returns></returns>      
        [HttpPost]
        public ActionResult Export(string search)
        {
            string cid = "";
            string type = "0";
            string parm = "";
            if (!string.IsNullOrEmpty(search))
            {
                cid = search.Split('^')[0].Split('&')[0].ToString();
                type = search.Split('^')[0].Split('&')[1].ToString();
                parm = search.Split('^')[1].ToString();
            }
            string sortName = "";
            string sortOrder = "";
            string[] titles = new string[] { };
            string[] fields = new string[] { };
            switch (type)
            {
                case "0":
                    titles = new string[] { "日期", "公司名称", "所在城市", "客户级别", "跟进人", "跟进次数", "跟进方式", "跟进目的" };
                    fields = new string[] { "ReservationDate", "CustomerName", "CityName", "CustomerLevel", "MyName", "FollowUpCategory", "FollowUpMode", "FollowUpPurpose" };
                    sortName = "ReservationDate";
                    sortOrder = "desc";
                    break;
                case "1":
                    titles = new string[] { "日期", "公司名称", "联系人", "客户级别", "联系方式", "是否接通", "商机判断", "跟进人" };
                    fields = new string[] { "FollowUpDate", "CustomerName", "ContactName", "CustomerLevel", "CompanyTel", "ContactState", "Opportunities", "MyName" };
                    sortName = "FollowUpDate";
                    sortOrder = "desc";
                    break;
                case "2":
                    titles = new string[] { "日期", "公司名称", "拜访人", "客户级别", "客户进程 ", "商机判断", "跟进人" };
                    fields = new string[] { "FollowUpDate", "CustomerName", "ContactName", "CustomerLevel", "CustomerFunnel", "Opportunities", "MyName" };
                    sortName = "FollowUpDate";
                    sortOrder = "desc";
                    break;
                default:
                    break;
            }

            Account account = GetCurrentAccount();
            List<CustomerFollowModel> queryData = m_BLL.ExportInfo(account.Id, sortOrder, sortName, cid, parm, type);

            return Content(WriteExcle(titles, fields, queryData.ToArray()));
        }
        #endregion



    }
}