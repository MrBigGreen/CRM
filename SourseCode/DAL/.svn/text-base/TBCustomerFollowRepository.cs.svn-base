using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-10
    /// 描述说明：客户跟进任务
    /// 修改人：Jonny
    /// Date:2015-6-11
    /// </summary>
    public partial class TBCustomerFollowRepository : BaseRepository<TB_CustomerFollow>, IDisposable
    {
        /// <summary>
        /// 我的任务
        /// </summary>
        /// <param name="SysEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<CustomerFollowModel> MyTask(string userID, DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            var query = (from f in db.TB_CustomerFollow
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.SysPersonID equals s.Id
                         join g in db.SysField on f.FollowUpCategory equals g.Id
                         join p in db.SysField on f.FollowUpPurpose equals p.Id
                         join m in db.SysField on f.FollowUpMode equals m.Id
                         join j in db.SysField on c.CustomerFunnel equals j.Id
                         into JoinedFunncel
                         from funncl in JoinedFunncel.DefaultIfEmpty()


                         where f.IsDelete == false && f.ContactState == 0
                         select new CustomerFollowModel
                         {
                             CustomerFollowID = f.CustomerFollowID,
                             ReservationDate = f.ReservationDate,
                             CustomerName = c.CustomerName,
                             CityCode = f.CityCode,
                             CityName = f.CityName,
                             CustomerLevel = f.OtherLevel,
                             SysPersonID = f.SysPersonID,
                             MyName = s.MyName,
                             FollowUpCategory = g.MyTexts,
                             FollowUpCategoryID = g.Id,
                             FollowUpMode = m.MyTexts,
                             FollowUpModeID = m.Id,
                             CustomerID = f.CustomerID,
                             FollowUpPurpose = p.MyTexts,
                             FollowUpPurposeID = p.Id,
                             AddressDetails = f.AddressDetails,
                             ProvinceCode = f.ProvinceCode,
                             ProvinceName = f.ProvinceName,
                             DistrictCode = f.DistrictCode,
                             DistrictName = f.DistrictName,
                             Telephone = s.Telephone,
                             TelephoneExt = s.TelephoneExt,
                             Remark = c.FollowBack,
                             PositionLink = c.PositionLink,
                             CustomerFunnel = f.CustomerFunnel,
                             CustomerFunnelName = funncl.MyTexts
                         }).Distinct();

            if (!string.IsNullOrEmpty(search))
            {
                string[] sparam = search.Split('$');

                #region ==条件搜索==

                //日期查询
                if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                {
                    string[] times = sparam[0].ToString().Trim().Split(',');
                    string begin = times[0].ToString().Trim();
                    string end = times[1].ToString().Trim();

                    if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        DateTime? en = DateTime.Parse(end).AddDays(1);
                        query = query.Where(s => s.ReservationDate < en && s.ReservationDate >= be);
                    }
                    else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        query = query.Where(s => s.ReservationDate >= be);
                    }
                    else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? en = DateTime.Parse(end);
                        query = query.Where(s => s.ReservationDate <= en);
                    }
                }
                //跟进人
                string sysPID = sparam[1].ToString().Trim();
                if (!string.IsNullOrEmpty(sysPID))
                {
                    query = query.Where(s => sysPID.Contains(s.SysPersonID));
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }
                //跟进目的
                string purpose = sparam[2].ToString().Trim();
                if (!string.IsNullOrEmpty(purpose) && purpose != "--请选择--")
                {
                    query = query.Where(s => s.CustomerFunnel.Equals(purpose));
                }
                //跟进方式
                string mode = sparam[3].ToString().Trim();
                if (!string.IsNullOrEmpty(mode) && mode != "0")
                {
                    query = query.Where(s => s.FollowUpModeID.Equals(mode));
                }

                //模糊
                string searchName = sparam[4].ToString().Trim();
                if (searchName != "")
                {
                    query = query.Where(s => s.CustomerName.Contains(searchName));
                }

                #endregion
            }
            else
            {
                var date = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                query = query.Where(s => s.ReservationDate < date && s.SysPersonID.Equals(userID));
            }

            query = query.OrderBy(s => s.ReservationDate);
            var sql = query.ToString();
            return query.AsQueryable();

        }


        /// <summary>
        /// 客户任务查询
        /// </summary>
        /// <param name="SysEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<CustomerFollowModel> GetCustomerFollowTask(DB_CRMEntities db, string SysPersonID,int FollowType, string order, string sort, string search, int page, int rows, ref int total)
        {

            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            SqlParameter[] param = new SqlParameter[10];
            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[0] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[0] = new SqlParameter("@CustomerName", "");
            }
            //
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[1] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@StartDate", "");
            }

            //
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[2] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[2] = new SqlParameter("@EndDate", "");
            }
            //
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[3] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"]);
            }
            else
            {
                param[3] = new SqlParameter("@CustomerFunnel", "");
            }
            //
            if (!string.IsNullOrWhiteSpace(SysPersonID))
            {
                param[4] = new SqlParameter("@SysPersonID", SysPersonID);
            }
            else
            {
                param[4] = new SqlParameter("@SysPersonID", "");
            }

            param[5] = new SqlParameter("@FollowType", FollowType);

            param[6] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[6].Value = page;
            param[7] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[7].Value = rows;
            param[8] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[9] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[8].Direction = ParameterDirection.Output;
            param[9].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<CustomerFollowModel>("exec P_CustomerFollow_S	@CustomerName ,@StartDate,@EndDate,@CustomerFunnel,@SysPersonID,@FollowType,@sys_PageIndex,@sys_PageSize,@PCount OUTPUT,@RCount OUTPUT", param).ToList();
            total = Convert.ToInt32(param[9].Value);
            return query;
        }



        /// <summary>
        /// 跟进记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerFollowID"></param>
        /// <param name="isLine"></param>
        /// <param name="isNextType"></param>
        /// <param name="customerPhone"></param>
        /// <param name="customerLevel"></param>
        /// <param name="customerFeedback"></param>
        /// <param name="customerFunnel"></param>
        /// <param name="customerOffer"></param>
        /// <param name="customerHandle"></param>
        /// <param name="callDate"></param>
        /// <param name="remark"></param>
        /// <param name="userID"></param>
        /// <param name="customerID"></param>
        /// <param name="cityName"></param>
        /// <param name="cityCode"></param>
        /// <param name="customerPurpose"></param>
        /// <param name="addressDetails"></param>
        /// <param name="provinceName"></param>
        /// <param name="provinceCode"></param>
        /// <param name="districtName"></param>
        /// <param name="districtCode"></param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        //public int AddFollowUpInfo(DB_CRMEntities db, string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFeedback, string customerFunnel, string customerOffer, string customerHandle, string callDate, string remark, string userID, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string fileUrl)
        public int AddFollowUpInfo(DB_CRMEntities db, string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFunnel, string customerOffer, string callDate, string remark, string userID, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string fileUrl)
        {
            int a = 0;
            int b = 0;
            int isAdd = 0;
            string sql = string.Empty;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    string followID = Result.GetNewId();

                    if (callDate != "")
                    {
                        //存客户跟进任务表
                        sql = string.Format(@"INSERT INTO TB_CustomerFollow ( CustomerFollowID, CustomerID, ReservationDate, FollowUpMode, FollowUpCategory, OtherLevel, FollowUpPurpose, CityCode, CityName, ContactState, SysPersonID, IsDelete, CreatedTime, CreatedBy, LastUpdatedTime, LastUpdatedBy ,AddressDetails,ProvinceName ,ProvinceCode ,DistrictName ,DistrictCode ) VALUES ( '{0}', '{1}', '{2}', '{3}', '15062511182740603013696f1bf00', '{4}', '{5}', '{6}', '{7}', 0, '{8}', 0, GETDATE(), '{8}', GETDATE(), '{8}' ,'{9}','{10}','{11}','{12}','{13}')", followID, customerID, callDate, isNextType, customerLevel, customerPurpose, cityCode, cityName, userID, addressDetails, provinceName, provinceCode, districtName, districtCode);
                        a = db.Database.ExecuteSqlCommand(sql);
                    }
                    else
                    {
                        a = 1;
                    }
                    string CustomerContactName = customerPhone.Split(',')[0].ToString();
                    string callPhone = customerPhone.Split(',')[1].ToString();
                    //修改客户跟进任务表
                    sql = string.Format(@"UPDATE TB_CustomerFollow SET ContactState ={0}, FollowUpDate =  GETDATE(), CustomerContactName = '{1}',  CustomerFunnel = '{2}',Opportunities = '{3}', NextFollowDate = '{4}', NextFollowUpMode = '{5}', Remark = '{6}', SysPersonID = '{7}', VersionNo = VersionNo + 1, LastUpdatedTime = GETDATE(), LastUpdatedBy = '{7}',CallPhone='{8}',FileUrl='{9}' WHERE CustomerFollowID='{10}' ;", isLine, CustomerContactName, customerFunnel, customerOffer, callDate, isNextType, remark, userID, callPhone, fileUrl, customerFollowID);

                    b = db.Database.ExecuteSqlCommand(sql);

                    if (a == 1 && b == 1)
                    {
                        //已联系
                        if (isLine == "1")
                        {
                            //修改客户 商机判断，客户漏斗  chand by chand 2015-07-10
                            sql = string.Format("update TB_Customer set Opportunities='{0}',CustomerFunnel='{1}', VersionNo = VersionNo + 1, LastUpdatedTime = GETDATE(), LastUpdatedBy = '{2}' where CustomerID='{3}' ", customerOffer, customerFunnel, userID, customerID);
                            db.Database.ExecuteSqlCommand(sql);
                        }
                        transactionScope.Complete();
                        isAdd = 1;
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
                catch (Exception)
                {
                    Transaction.Current.Rollback();
                }
            }

            return isAdd;
        }


        /// <summary>
        ///  电话联系历史
        /// </summary>
        /// <param name="SysEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<CustomerFollowModel> CallHistroy(string userID, DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            var query = (from f in db.TB_CustomerFollow
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.SysPersonID equals s.Id
                         //join t in db.TB_CustomerContact on f.CustomerContactID equals t.CustomerContactID
                         join a in db.SysField on f.OtherLevel equals a.Id
                         into sa
                         from d in sa.DefaultIfEmpty()
                         join b in db.SysField on f.Opportunities equals b.Id
                         into sb
                         from o in sb.DefaultIfEmpty()
                         where f.IsDelete == false && f.ContactState != 0 && f.FollowUpMode == "1506251032299623134259baff7c9"
                         select new CustomerFollowModel
                         {
                             CustomerFollowID = f.CustomerFollowID,
                             FollowUpDate = f.FollowUpDate,
                             CustomerName = c.CustomerName,
                             ContactName = f.CustomerContactName,
                             CityCode = f.CityCode,
                             CityName = f.CityName,
                             CustomerLevel = d.MyTexts,
                             SysPersonID = f.SysPersonID,
                             MyName = s.MyName,
                             CallPhone = f.CallPhone,
                             CustomerID = f.CustomerID,
                             Opportunities = o.MyTexts,
                             OpportunitiesID = o.Id,
                             ContactState = f.ContactState,
                             FileUrl = f.FileUrl
                         }).Distinct();

            if (!string.IsNullOrEmpty(search))
            {
                string[] sparam = search.Split('$');

                #region ==条件搜索==

                //日期查询
                if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                {
                    string[] times = sparam[0].ToString().Trim().Split(',');
                    string begin = times[0].ToString().Trim();
                    string end = times[1].ToString().Trim();

                    if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        DateTime? en = DateTime.Parse(end).AddDays(1);
                        query = query.Where(s => s.FollowUpDate < en && s.FollowUpDate >= be);
                    }
                    else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        query = query.Where(s => s.FollowUpDate >= be);
                    }
                    else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? en = DateTime.Parse(end);
                        query = query.Where(s => s.FollowUpDate <= en);
                    }
                }
                //跟进人
                string sysPID = sparam[1].ToString().Trim();
                if (!string.IsNullOrEmpty(sysPID))
                {
                    query = query.Where(s => sysPID.Contains(s.SysPersonID));
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }
                //商机判断
                string customerOffer = sparam[2].ToString().Trim();
                if (!string.IsNullOrEmpty(customerOffer) && customerOffer != "--请选择--")
                {
                    query = query.Where(s => s.OpportunitiesID.Equals(customerOffer));
                }
                //是否接通
                string isLine = sparam[3].ToString().Trim();
                if (!string.IsNullOrEmpty(isLine) && isLine != "0" && isLine != "-1")
                {
                    var line = short.Parse(isLine);
                    query = query.Where(s => s.ContactState == line);
                }

                string cityCode = sparam[4].ToString().Trim();
                if (!string.IsNullOrEmpty(cityCode))
                {
                    query = query.Where(s => cityCode.Contains(s.CityCode));
                }

                //模糊
                string searchName = sparam[5].ToString().Trim();
                if (searchName != "")
                {
                    query = query.Where(s => s.CustomerName.Contains(searchName) || s.CallPhone.Contains(searchName) || s.ContactName.Contains(searchName));
                }

                #endregion
            }
            else
            {
                query = query.Where(s => s.SysPersonID.Equals(userID));
            }

            query = query.OrderByDescending(s => s.FollowUpDate);
            var sql = query.ToString();
            return query.AsQueryable();

        }

        /// <summary>
        /// 见面约谈历史
        /// </summary>
        /// <param name="SysEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public IQueryable<CustomerFollowModel> MeetingHistroy(string userID, DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            var query = (from f in db.TB_CustomerFollow
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.SysPersonID equals s.Id
                         // join t in db.TB_CustomerContact on f.CustomerContactID equals t.CustomerContactID
                         where f.IsDelete == false && f.ContactState != 0 && f.FollowUpMode == "1506251033338479674bf54656731"
                         select new CustomerFollowModel
                         {
                             CustomerFollowID = f.CustomerFollowID,
                             FollowUpDate = f.FollowUpDate,
                             CustomerName = c.CustomerName,
                             ContactName = f.CustomerContactName,
                             CityCode = f.CityCode,
                             CityName = f.CityName,
                             CustomerLevel = f.OtherLevel,
                             SysPersonID = f.SysPersonID,
                             MyName = s.MyName,
                             CallPhone = f.CallPhone,
                             CustomerFunnel = f.CustomerFunnel,
                             CustomerID = f.CustomerID,
                             Opportunities = f.Opportunities,
                             ContactState = f.ContactState
                         }).Distinct();

            if (!string.IsNullOrEmpty(search))
            {
                string[] sparam = search.Split('$');

                #region ==条件搜索==

                //日期查询
                if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                {
                    string[] times = sparam[0].ToString().Trim().Split(',');
                    string begin = times[0].ToString().Trim();
                    string end = times[1].ToString().Trim();

                    if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        DateTime? en = DateTime.Parse(end).AddDays(1);
                        query = query.Where(s => s.FollowUpDate < en && s.FollowUpDate >= be);
                    }
                    else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        query = query.Where(s => s.FollowUpDate >= be);
                    }
                    else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? en = DateTime.Parse(end);
                        query = query.Where(s => s.FollowUpDate <= en);
                    }
                }
                //跟进人
                string sysPID = sparam[1].ToString().Trim();
                if (!string.IsNullOrEmpty(sysPID))
                {
                    query = query.Where(s => sysPID.Contains(s.SysPersonID));
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }
                //商机判断
                string customerOffer = sparam[2].ToString().Trim();
                if (!string.IsNullOrEmpty(customerOffer) && customerOffer != "--请选择--")
                {
                    query = query.Where(s => s.Opportunities.Equals(customerOffer));
                }
                //是否接通
                string isLine = sparam[3].ToString().Trim();
                if (!string.IsNullOrEmpty(isLine) && isLine != "0" && isLine != "-1")
                {
                    var line = short.Parse(isLine);
                    query = query.Where(s => s.ContactState == line);
                }

                string cityCode = sparam[4].ToString().Trim();
                if (!string.IsNullOrEmpty(cityCode))
                {
                    query = query.Where(s => cityCode.Contains(s.CityCode));
                }

                //模糊
                string searchName = sparam[5].ToString().Trim();
                if (searchName != "")
                {
                    query = query.Where(s => s.CustomerName.Contains(searchName) || s.CallPhone.Contains(searchName) || s.ContactName.Contains(searchName));
                }

                #endregion
            }
            else
            {
                //var date = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                query = query.Where(s => s.SysPersonID.Equals(userID));
            }

            query = query.OrderByDescending(s => s.FollowUpDate);
            var sql = query.ToString();
            return query.AsQueryable();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="db"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="cid"></param>
        /// <param name="search"></param>
        /// <param name="type"></param>
        /// <param name="listQuery"></param>
        /// <returns></returns>
        public IQueryable<CustomerFollowModel> GetExportInfos(string userID, DB_CRMEntities db, string order, string sort, string cid, string search, string type, params object[] listQuery)
        {
            IQueryable<CustomerFollowModel> query;
            if (type == "0")
            {
                query = (from f in db.TB_CustomerFollow
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.SysPersonID equals s.Id
                         join g in db.SysField on f.FollowUpCategory equals g.Id
                         join p in db.SysField on f.FollowUpPurpose equals p.Id
                         join m in db.SysField on f.FollowUpMode equals m.Id
                         where f.IsDelete == false && f.ContactState == 0
                         select new CustomerFollowModel
                         {
                             CustomerFollowID = f.CustomerFollowID,
                             ReservationDate = f.ReservationDate,
                             CustomerName = c.CustomerName,
                             CityCode = f.CityCode,
                             CityName = f.CityName,
                             CustomerLevel = f.OtherLevel,
                             SysPersonID = f.SysPersonID,
                             MyName = s.MyName,
                             FollowUpCategory = g.MyTexts,
                             FollowUpCategoryID = g.Id,
                             FollowUpMode = m.MyTexts,
                             FollowUpModeID = m.Id,
                             CustomerID = f.CustomerID,
                             FollowUpPurpose = p.MyTexts,
                             FollowUpPurposeID = p.Id,
                             AddressDetails = f.AddressDetails
                         }).Distinct();
                if (!string.IsNullOrEmpty(search))
                {
                    string[] sparam = search.Split('$');

                    #region ==条件搜索==

                    //日期查询
                    if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                    {
                        string[] times = sparam[0].ToString().Trim().Split(',');
                        string begin = times[0].ToString().Trim();
                        string end = times[1].ToString().Trim();

                        if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                        {
                            DateTime? be = DateTime.Parse(begin);
                            DateTime? en = DateTime.Parse(end).AddDays(1);
                            query = query.Where(s => s.ReservationDate < en && s.ReservationDate >= be);
                        }
                        else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                        {
                            DateTime? be = DateTime.Parse(begin);
                            query = query.Where(s => s.ReservationDate >= be);
                        }
                        else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                        {
                            DateTime? en = DateTime.Parse(end);
                            query = query.Where(s => s.ReservationDate <= en);
                        }
                    }
                    //跟进人
                    string sysPID = sparam[1].ToString().Trim();
                    if (!string.IsNullOrEmpty(sysPID))
                    {
                        query = query.Where(s => sysPID.Contains(s.SysPersonID));
                    }
                    else
                    {
                        query = query.Where(s => s.SysPersonID.Equals(userID));
                    }
                    //跟进目的
                    string purpose = sparam[2].ToString().Trim();
                    if (!string.IsNullOrEmpty(purpose) && purpose != "--请选择--")
                    {
                        query = query.Where(s => s.FollowUpPurposeID.Equals(purpose));
                    }
                    //跟进方式
                    string mode = sparam[3].ToString().Trim();
                    if (!string.IsNullOrEmpty(mode) && mode != "0")
                    {
                        query = query.Where(s => s.FollowUpModeID.Equals(mode));
                    }

                    //模糊
                    string searchName = sparam[4].ToString().Trim();
                    if (searchName != "")
                    {
                        query = query.Where(s => s.CustomerName.Contains(searchName));
                    }

                    #endregion
                }
                else
                {
                    var date = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                    query = query.Where(s => s.ReservationDate < date && s.SysPersonID.Equals(userID));
                }

            }
            else if (type == "1")
            {
                query = (from f in db.TB_CustomerFollow
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.SysPersonID equals s.Id
                         // join t in db.TB_CustomerContact on f.CustomerContactID equals t.CustomerContactID
                         join d in db.SysField on f.OtherLevel equals d.Id
                         join o in db.SysField on f.Opportunities equals o.Id
                         join a in db.TB_CallLog on f.CustomerID equals a.CustomerID
                         into sa
                         from tab in sa.DefaultIfEmpty()
                         where f.IsDelete == false && f.ContactState != 0 && f.FollowUpMode == "1506251032299623134259baff7c9"
                         select new CustomerFollowModel
                         {
                             CustomerFollowID = f.CustomerFollowID,
                             FollowUpDate = f.FollowUpDate,
                             CustomerName = c.CustomerName,
                             ContactName = f.CustomerContactName,
                             CityCode = f.CityCode,
                             CityName = f.CityName,
                             CustomerLevel = d.MyTexts,
                             SysPersonID = f.SysPersonID,
                             MyName = s.MyName,
                             CallPhone = f.CallPhone,
                             TalkTimeLength = tab.TalkTimeLength,
                             CustomerID = f.CustomerID,
                             Opportunities = o.MyTexts,
                             OpportunitiesID = o.Id,
                             ContactState = f.ContactState
                         }).Distinct();

                if (!string.IsNullOrEmpty(search))
                {
                    string[] sparam = search.Split('$');

                    #region ==条件搜索==

                    //日期查询
                    if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                    {
                        string[] times = sparam[0].ToString().Trim().Split(',');
                        string begin = times[0].ToString().Trim();
                        string end = times[1].ToString().Trim();

                        if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                        {
                            DateTime? be = DateTime.Parse(begin);
                            DateTime? en = DateTime.Parse(end).AddDays(1);
                            query = query.Where(s => s.FollowUpDate < en && s.FollowUpDate >= be);
                        }
                        else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                        {
                            DateTime? be = DateTime.Parse(begin);
                            query = query.Where(s => s.FollowUpDate >= be);
                        }
                        else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                        {
                            DateTime? en = DateTime.Parse(end);
                            query = query.Where(s => s.FollowUpDate <= en);
                        }
                    }
                    //跟进人
                    string sysPID = sparam[1].ToString().Trim();
                    if (!string.IsNullOrEmpty(sysPID))
                    {
                        query = query.Where(s => sysPID.Contains(s.SysPersonID));
                    }
                    else
                    {
                        query = query.Where(s => s.SysPersonID.Equals(userID));
                    }
                    //商机判断
                    string customerOffer = sparam[2].ToString().Trim();
                    if (!string.IsNullOrEmpty(customerOffer) && customerOffer != "--请选择--")
                    {
                        query = query.Where(s => s.OpportunitiesID.Equals(customerOffer));
                    }
                    //是否接通
                    string isLine = sparam[3].ToString().Trim();
                    if (!string.IsNullOrEmpty(isLine) && isLine != "0" && isLine != "-1")
                    {
                        var line = short.Parse(isLine);
                        query = query.Where(s => s.ContactState == line);
                    }

                    string cityCode = sparam[4].ToString().Trim();
                    if (!string.IsNullOrEmpty(cityCode))
                    {
                        query = query.Where(s => cityCode.Contains(s.CityCode));
                    }

                    //模糊
                    string searchName = sparam[5].ToString().Trim();
                    if (searchName != "")
                    {
                        query = query.Where(s => s.CustomerName.Contains(searchName) || s.CallPhone.Contains(searchName) || s.ContactName.Contains(searchName));
                    }

                    #endregion
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }
            }
            else
            {
                query = (from f in db.TB_CustomerFollow
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.SysPersonID equals s.Id
                         // join t in db.TB_CustomerContact on f.CustomerContactID equals t.CustomerContactID
                         where f.IsDelete == false && f.ContactState != 0 && f.FollowUpMode == "1506251033338479674bf54656731"
                         select new CustomerFollowModel
                         {
                             CustomerFollowID = f.CustomerFollowID,
                             FollowUpDate = f.FollowUpDate,
                             CustomerName = c.CustomerName,
                             ContactName = f.CustomerContactName,
                             CityCode = f.CityCode,
                             CityName = f.CityName,
                             CustomerLevel = f.OtherLevel,
                             SysPersonID = f.SysPersonID,
                             MyName = s.MyName,
                             CallPhone = f.CallPhone,
                             CustomerFunnel = f.CustomerFunnel,
                             CustomerID = f.CustomerID,
                             Opportunities = f.Opportunities,
                             ContactState = f.ContactState
                         }).Distinct();

                if (!string.IsNullOrEmpty(search))
                {
                    string[] sparam = search.Split('$');

                    #region ==条件搜索==

                    //日期查询
                    if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                    {
                        string[] times = sparam[0].ToString().Trim().Split(',');
                        string begin = times[0].ToString().Trim();
                        string end = times[1].ToString().Trim();

                        if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                        {
                            DateTime? be = DateTime.Parse(begin);
                            DateTime? en = DateTime.Parse(end).AddDays(1);
                            query = query.Where(s => s.FollowUpDate < en && s.FollowUpDate >= be);
                        }
                        else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                        {
                            DateTime? be = DateTime.Parse(begin);
                            query = query.Where(s => s.FollowUpDate >= be);
                        }
                        else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                        {
                            DateTime? en = DateTime.Parse(end);
                            query = query.Where(s => s.FollowUpDate <= en);
                        }
                    }
                    //跟进人
                    string sysPID = sparam[1].ToString().Trim();
                    if (!string.IsNullOrEmpty(sysPID))
                    {
                        query = query.Where(s => sysPID.Contains(s.SysPersonID));
                    }
                    else
                    {
                        query = query.Where(s => s.SysPersonID.Equals(userID));
                    }
                    //商机判断
                    string customerOffer = sparam[2].ToString().Trim();
                    if (!string.IsNullOrEmpty(customerOffer) && customerOffer != "--请选择--")
                    {
                        query = query.Where(s => s.Opportunities.Equals(customerOffer));
                    }
                    //是否接通
                    string isLine = sparam[3].ToString().Trim();
                    if (!string.IsNullOrEmpty(isLine) && isLine != "0" && isLine != "-1")
                    {
                        var line = short.Parse(isLine);
                        query = query.Where(s => s.ContactState == line);
                    }

                    string cityCode = sparam[4].ToString().Trim();
                    if (!string.IsNullOrEmpty(cityCode))
                    {
                        query = query.Where(s => cityCode.Contains(s.CityCode));
                    }

                    //模糊
                    string searchName = sparam[5].ToString().Trim();
                    if (searchName != "")
                    {
                        query = query.Where(s => s.CustomerName.Contains(searchName) || s.CallPhone.Contains(searchName) || s.ContactName.Contains(searchName));
                    }

                    #endregion
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }
            }
            if (!string.IsNullOrEmpty(cid))
            {
                query = query.Where(s => cid.Contains(s.CustomerFollowID));
            }
            query = query.OrderByDescending(s => s.CustomerFollowID);
            return query.AsQueryable();
        }





        #region 通用查询客户跟进  create by chand 2015-06-29

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id">客户ID</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>
        public IQueryable<TB_CustomerFollow> DaoChuData(DB_CRMEntities db, string id, string order, string sort, string search, params object[] listQuery)
        {
            string where = " it.IsDelete=false ";

            if (!string.IsNullOrWhiteSpace(id))
            {
                where += " and it.CustomerID='" + id + "'";
            }
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());
            if (queryDic != null && queryDic.Count > 0)
            {
                foreach (var item in queryDic)
                {
                    if (string.IsNullOrWhiteSpace(item.Key) || string.IsNullOrWhiteSpace(item.Value))
                    {
                        continue;
                    }
                    switch (item.Key)
                    {
                        case "StartDate":
                            where += " and it.[FollowUpDate] >=  CAST('" + item.Value + "' as   System.DateTime)";
                            break;
                        case "EndDate":
                            where += " and it.[FollowUpDate] <=  CAST('" + item.Value + "' as   System.DateTime)";
                            break;
                        case "ContactState":
                            where += "and it.[" + item.Key + "] = " + item.Value;
                            break;
                        //case "FollowUpMode":
                        //    where += "it.[" + item.Key + "] = " + item.Value;
                        //    break;
                        default:
                            where += "and it.[" + item.Key + "] = '" + item.Value + "'";
                            break;
                    }

                }
            }
            return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext
                     .CreateObjectSet<TB_CustomerFollow>().Where(string.IsNullOrEmpty(where) ? "true" : where)
                     .OrderBy("it.[" + sort.GetString() + "] " + order.GetString())
                     .AsQueryable();

        }

        #endregion


        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerFollow GetById(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                return GetById(db, id);
            }
        }
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_CustomerFollow GetById(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerFollow.SingleOrDefault(s => s.CustomerFollowID == id);
        }
        #endregion

        #region 通过主键删除对象
        /// <summary>
        /// 确定删除一个对象，调用Save方法
        /// </summary>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>    
        public int Delete(string id)
        {
            using (DB_CRMEntities db = new DB_CRMEntities())
            {
                this.Delete(db, id);
                return 1;
            }
        }
        /// <summary>
        /// 删除一个合同
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="id">一条角色的主键</param>
        public void Delete(DB_CRMEntities db, string id)
        {
            TB_CustomerFollow deleteItem = GetById(db, id);
            if (deleteItem != null)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="db">实体数据</param>
        /// <param name="deleteCollection">主键的集合</param>
        public void Delete(DB_CRMEntities db, string[] deleteCollection)
        {
            //数据库设置级联关系，自动删除子表的内容   
            IQueryable<TB_CustomerFollow> collection = from f in db.TB_CustomerFollow
                                                       where deleteCollection.Contains(f.CustomerFollowID)
                                                       select f;
            foreach (var deleteItem in collection)
            {
                deleteItem.IsDelete = true;
                Edit(db, deleteItem);
            }
        }
        #endregion


        #region 跟进历史

        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetFollowBackHistoryByCustomerID(DB_CRMEntities db, string CustomerID, int FollowType, int page, int rows, string search, ref int total)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CustomerID", CustomerID);
            param[1] = new SqlParameter("@FollowType", FollowType);

            param[2] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[2].Value = page;
            param[3] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[3].Value = rows;
            param[4] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[5] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[4].Direction = ParameterDirection.Output;
            param[5].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<CustomerFollowModel>("exec P_FollowBackHistoryByCustomerID_S @CustomerID,@FollowType,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[5].Value);
            return query;
        }

        #endregion

        #region 跟进历史

        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerFollowModel> GetFollowBackHistory(DB_CRMEntities db, int page, int rows, string search, ref int total)
        {
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            SqlParameter[] param = new SqlParameter[13];

            param[0] = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100);
            param[0].Value = "";
            if (queryDic != null && queryDic.ContainsKey("Keyword") && !string.IsNullOrWhiteSpace(queryDic["Keyword"]))
            {
                param[0] = new SqlParameter("@Keyword", queryDic["Keyword"]);
            }

            param[1] = new SqlParameter("@BackRemark", SqlDbType.NVarChar, 100);
            param[1].Value = "";
            if (queryDic != null && queryDic.ContainsKey("BackRemark") && !string.IsNullOrWhiteSpace(queryDic["BackRemark"]))
            {
                param[1] = new SqlParameter("@BackRemark", queryDic["BackRemark"]);
            }

            param[2] = new SqlParameter("@ContactState", SqlDbType.NVarChar, 50);
            param[2].Value = "";
            if (queryDic != null && queryDic.ContainsKey("ContactState") && !string.IsNullOrWhiteSpace(queryDic["ContactState"]))
            {
                param[2] = new SqlParameter("@ContactState", queryDic["ContactState"]);
            }

            param[3] = new SqlParameter("@FollowUpMode", SqlDbType.NVarChar, 50);
            param[3].Value = "";
            if (queryDic != null && queryDic.ContainsKey("FollowUpMode") && !string.IsNullOrWhiteSpace(queryDic["FollowUpMode"]))
            {
                param[3] = new SqlParameter("@FollowUpMode", queryDic["FollowUpMode"]);
            }

            param[4] = new SqlParameter("@CustomerFunnel", SqlDbType.NVarChar, 50);
            param[4].Value = "";
            if (queryDic != null && queryDic.ContainsKey("CustomerFunnel") && !string.IsNullOrWhiteSpace(queryDic["CustomerFunnel"]))
            {
                param[4] = new SqlParameter("@CustomerFunnel", queryDic["CustomerFunnel"]);
            }

            param[5] = new SqlParameter("@CityCode", SqlDbType.NVarChar, 50);
            param[5].Value = "";
            if (queryDic != null && queryDic.ContainsKey("CityCode") && !string.IsNullOrWhiteSpace(queryDic["CityCode"]))
            {
                param[5] = new SqlParameter("@CityCode", queryDic["CityCode"]);
            }

            param[6] = new SqlParameter("@SysPersonID", SqlDbType.NVarChar, 500);
            param[6].Value = "";
            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[6] = new SqlParameter("@SysPersonID", queryDic["SysPersonID"]);
            }

            param[7] = new SqlParameter("@StartDate", SqlDbType.NVarChar, 50);
            param[7].Value = "";
            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[7] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }

            param[8] = new SqlParameter("@EndDate", SqlDbType.NVarChar, 50);
            param[8].Value = "";
            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[8] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }

            param[9] = new SqlParameter("@sys_PageIndex", SqlDbType.Int, 4);
            param[9].Value = page;
            param[10] = new SqlParameter("@sys_PageSize", SqlDbType.Int, 4);
            param[10].Value = rows;
            param[11] = new SqlParameter("@PCount", SqlDbType.Int, 4);
            param[12] = new SqlParameter("@RCount", SqlDbType.Int, 4);

            param[11].Direction = ParameterDirection.Output;
            param[12].Direction = ParameterDirection.Output;

            var query = db.Database.SqlQuery<CustomerFollowModel>("exec P_FollowBackHistory_S @Keyword,@BackRemark,@ContactState,@FollowUpMode,@CustomerFunnel,@CityCode,@SysPersonID,@StartDate,@EndDate,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();
            total = Convert.ToInt32(param[12].Value);
            return query;
        }

        #endregion


        #region 最后一次跟进未反馈记录

        /// <summary>
        /// 最后一次跟进未反馈记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public TB_CustomerFollow GetLastCustomerFollow(DB_CRMEntities db, string id)
        {
            return db.TB_CustomerFollow.Where(s => s.CustomerID == id && s.ContactState == 0).LastOrDefault();
            //return  (from s in db.TB_CustomerFollow where s.CustomerID==id && s.ContactState==0 select s ).LastOrDefault();
        }

        #endregion

        #region 最后一次客户联系人
        /// <summary>
        /// 最后一次客户联系人
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="ContactName"></param>
        /// <param name="ContactPhone"></param>
        public TB_CustomerFollow GetLastContactInfo(DB_CRMEntities db, string CustomerID,string SysPersonID)
        {
            //SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerID", CustomerID);
            //param[1] = new SqlParameter("@ContactName", SqlDbType.NVarChar, 200);
            //param[2] = new SqlParameter("@ContactPhone", SqlDbType.NVarChar, 200);

            //param[1].Direction = ParameterDirection.Output;
            //param[2].Direction = ParameterDirection.Output;
            //var data = db.Database.SqlQuery<object>("exec [dbo].[P_Customer_S_LastContactPerson] @CustomerID,@ContactName output,@ContactPhone output", param).ToList();

            //ContactName = param[1].Value.ToString();
            //ContactPhone = param[2].Value.ToString();

            return db.TB_CustomerFollow.Where(q => q.CustomerID == CustomerID && q.IsDelete == false && q.ContactState != 0 && q.SysPersonID == SysPersonID).OrderByDescending(q => q.FollowUpDate).FirstOrDefault();

        }

        #endregion
        


        public void Dispose()
        {
        }
    }
}
