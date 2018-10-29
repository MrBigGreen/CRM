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
    /// 描述说明：合同信息
    /// 修改人：Jonny
    /// Date:2015-6-18
    /// </summary>
    public partial class TBContractRepository : BaseRepository<TB_Contract>, IDisposable
    {
        /// <summary>
        /// 模糊查询企业名称
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="db"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="listQuery"></param>
        /// <returns></returns>


        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="db"></param>
        /// <param name="companyName"></param>
        /// <param name="companyCode"></param>
        /// <param name="cityCode"></param>
        /// <param name="cityName"></param>
        /// <param name="packages"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="packageMoney"></param>
        /// <param name="uploadPackage"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int AddPackageInfo(DB_CRMEntities db, string companyName, string companyCode, string cityCode, string cityName, string packages, string beginDate, string endDate, string packageMoney, string uploadPackage, string userID)
        {

            int isAdd = 0;
            string sql = string.Empty;
            string cname = string.Empty;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    #region ==合同编号==

                    if (cityName.Contains("市"))
                    {
                        cname = cityName.Replace("市", "").Trim();
                    }
                    else
                    {
                        cname = cityName;
                    }
                    char[] temp = cname.ToCharArray();
                    string city = "";
                    for (int i = 0; i < temp.Length; i++)
                    {
                        city += GetPYString(temp[i].ToString());
                    }

                    sql = string.Format(@"SELECT ContractCount FROM TB_Contract WHERE YEARS='{0}'", DateTime.Now.Year.ToString());
                    int ccount = db.Database.ExecuteSqlCommand(sql);
                    if (ccount == -1)
                    {
                        ccount = 0;
                    }
                    ccount++;
                    int l = 5 - ccount.ToString().Length;
                    string d = "1";
                    for (int j = 0; j < l; j++)
                    {
                        d += "0";
                    }

                    //编号
                    string contractNo = city + "-" + uploadPackage.Substring(0, 6).Trim() + "-" + packages + "-" + d + ccount.ToString();

                    #endregion
                    sql = string.Format(@"INSERT TB_Contract ( ContractID, CustomerID, ContractMoney, EffectiveDate, Deadline, Package, Annex, IsDelete, VersionNo, CreatedBy, CreatedTime, LastUpdatedBy, LastUpdatedTime,YEARS,ContractCount ) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 0, 0, '{7}', GETDATE(), '{7}', GETDATE(),'{8}',{9})", contractNo, companyCode, packageMoney, beginDate, endDate, packages, uploadPackage, userID, uploadPackage.Substring(0, 4).Trim(), ccount);
                    int a = db.Database.ExecuteSqlCommand(sql);
                    if (a == 1)
                    {
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
        /// 获取合同列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="db"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="listQuery"></param>
        /// <returns></returns>
        public IQueryable<ContractModel> GetContractInfo(string userID, DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            var query = (from f in db.TB_Contract
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.CreatedBy equals s.Id
                         join a in db.SysField on f.Package equals a.DataValue
                         where f.IsDelete == false
                         select new ContractModel
                         {
                             ContractID = f.ContractID,
                             ContractMoney = f.ContractMoney,
                             EffectiveDate = f.EffectiveDate,
                             Deadline = f.Deadline,
                             PackageName = a.MyTexts,
                             CreatedBy = s.MyName,
                             Package = f.Package,
                             CityName = c.CityName,
                             CustomerName = c.CustomerName,
                             CityCode = c.CityCode,
                             VocationCode = c.VocationCode,
                             CreatedTime = f.CreatedTime,
                             CustomerID = c.CustomerID,
                             SysPersonID = s.Id
                         }).Distinct();

            if (!string.IsNullOrEmpty(search))
            {
                string[] sparam = search.Split('$');

                #region ==条件搜索==
                //城市
                string cityCode = sparam[0].ToString().Trim();
                if (!string.IsNullOrEmpty(cityCode))
                {
                    query = query.Where(s => cityCode.Contains(s.CityCode));
                }

                //行业
                string vocationCode = sparam[1].ToString().Trim();
                if (!string.IsNullOrEmpty(vocationCode))
                {
                    query = query.Where(s => vocationCode.Contains(s.VocationCode));
                }
                //跟进人
                string sysPID = sparam[2].ToString().Trim();
                if (!string.IsNullOrEmpty(sysPID))
                {
                    query = query.Where(s => sysPID.Contains(s.SysPersonID));
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }

                //合同类别
                string type = sparam[3].ToString().Trim();
                if (!string.IsNullOrEmpty(type) && type != "0")
                {
                    DateTime? begin = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(-30);
                    DateTime? last = DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(30);
                    DateTime? now = DateTime.Parse(DateTime.Now.ToShortDateString());
                    switch (type)
                    {
                        case "1"://新合同
                            query = query.Where(s => s.EffectiveDate <= now && s.EffectiveDate >= begin);
                            break;
                        case "2"://中期合同
                            query = query.Where(s => s.EffectiveDate <= begin && s.Deadline >= last);
                            break;
                        case "3"://即将到期
                            query = query.Where(s => s.Deadline >= now && s.Deadline <= last);
                            break;
                        case "-1"://过期
                            query = query.Where(s => s.Deadline < now);
                            break;
                        default:
                            break;
                    }


                }
                //购买服务
                string packages = sparam[4].ToString().Trim();
                if (!string.IsNullOrEmpty(packages))
                {
                    var p = packages.Substring(0, packages.Length - 1);
                    query = query.Where(s => p.Contains(s.Package));
                }

                //模糊
                string searchName = sparam[5].ToString().Trim();
                if (searchName != "")
                {
                    query = query.Where(s => s.CustomerName.Contains(searchName));
                }

                #endregion
            }
            else
            {
                query = query.Where(s => s.SysPersonID.Equals(userID));
            }

            query = query.OrderByDescending(s => s.CreatedTime);
            var sql = query.ToString();
            return query.AsQueryable();

        }


        public IQueryable<ContractModel> GetExportInfo(string userID, DB_CRMEntities db, string order, string sort, string cid, string search, params object[] listQuery)
        {
            var query = (from f in db.TB_Contract
                         join c in db.TB_Customer on f.CustomerID equals c.CustomerID
                         join s in db.SysPerson on f.CreatedBy equals s.Id
                         join a in db.SysField on f.Package equals a.DataValue
                         where f.IsDelete == false
                         select new ContractModel
                         {
                             ContractID = f.ContractID,
                             ContractMoney = f.ContractMoney,
                             EffectiveDate = f.EffectiveDate,
                             Deadline = f.Deadline,
                             PackageName = a.MyTexts,
                             CreatedBy = s.MyName,
                             Package = f.Package,
                             CityName = c.CityName,
                             CustomerName = c.CustomerName,
                             CityCode = c.CityCode,
                             VocationCode = c.VocationCode,
                             CreatedTime = f.CreatedTime,
                             CustomerID = c.CustomerID,
                             SysPersonID = s.Id
                         }).Distinct();

            if (!string.IsNullOrEmpty(search))
            {
                string[] sparam = search.Split('$');

                #region ==条件搜索==
                //城市
                string cityCode = sparam[0].ToString().Trim();
                if (!string.IsNullOrEmpty(cityCode))
                {
                    query = query.Where(s => cityCode.Contains(s.CityCode));
                }

                //行业
                string vocationCode = sparam[1].ToString().Trim();
                if (!string.IsNullOrEmpty(vocationCode))
                {
                    query = query.Where(s => vocationCode.Contains(s.VocationCode));
                }
                //跟进人
                string sysPID = sparam[2].ToString().Trim();
                if (!string.IsNullOrEmpty(sysPID))
                {
                    query = query.Where(s => sysPID.Contains(s.SysPersonID));
                }
                else
                {
                    query = query.Where(s => s.SysPersonID.Equals(userID));
                }

                //合同类别
                string type = sparam[3].ToString().Trim();
                if (!string.IsNullOrEmpty(type) && type != "0")
                {
                    DateTime? begin = DateTime.Now.AddDays(-30);
                    DateTime? last = DateTime.Now.AddDays(30);
                    DateTime? now = DateTime.Now;
                    switch (type)
                    {
                        case "1"://新合同
                            query = query.Where(s => s.EffectiveDate <= now && s.EffectiveDate >= begin);
                            break;
                        case "2"://中期合同
                            query = query.Where(s => s.EffectiveDate <= begin && s.Deadline >= last);
                            break;
                        case "3"://即将到期
                            query = query.Where(s => s.Deadline >= now && s.Deadline <= last);
                            break;
                        case "-1"://过期
                            query = query.Where(s => s.Deadline < now);
                            break;
                        default:
                            break;
                    }


                }
                //购买服务
                string packages = sparam[4].ToString().Trim();
                if (!string.IsNullOrEmpty(packages))
                {
                    var p = packages.Substring(0, packages.Length - 1);
                    query = query.Where(s => p.Contains(s.Package));
                }

                //模糊
                string searchName = sparam[5].ToString().Trim();
                if (searchName != "")
                {
                    query = query.Where(s => s.CustomerName.Contains(searchName));
                }

                #endregion
            }
            else
            {
                query = query.Where(s => s.SysPersonID.Equals(userID));
            }
            if (!string.IsNullOrEmpty(cid))
            {
                query = query.Where(s => cid.Contains(s.ContractID));
            }
            query = query.OrderByDescending(s => s.CreatedTime);
            var sql = query.ToString();
            return query.AsQueryable();

        }



        /// <summary>
        /// 汉字转拼音缩写
        /// </summary>/// <param name="str">要转换的汉字字符串</param>
        /// /// <returns>拼音缩写</returns>
        public string GetPYString(string str)
        {
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留           
                    tempStr += c.ToString();
                }
                else
                {//累加拼音声母     
                    tempStr += GetPYChar(c.ToString());
                }
            }
            return tempStr.ToUpper();
        }
        /// <summary>
        /// /// 取单个字符的拼音声母/// Code By MuseStudio@hotmail.com
        /// /// 2004-11-30/// </summary>/// <param name="c">要转换的单个汉字</param>
        /// /// <returns>拼音声母</returns>
        public string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "g";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        }


        #region 通过主键获取一个对象
        /// <summary>
        /// 通过主键id，获取角色---查看详细，首次编辑
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>角色</returns>
        public TB_Contract GetById(string id)
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
        public TB_Contract GetById(DB_CRMEntities db, string id)
        {
            return db.TB_Contract.SingleOrDefault(s => s.ContractID == id);
        }
        #endregion





        /// <summary>
        /// 合同查询
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<ContractEntity> GetContractData(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, string contractType, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[13];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }


            //客户名称
            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[2] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[2] = new SqlParameter("@CustomerName", "");
            }

            //客户编号
            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[3] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[3] = new SqlParameter("@CustomerID", "");
            }

            //客户服务方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[4] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[4] = new SqlParameter("@RecommendSolutionID", "");
            }

            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[5] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[5] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[6] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[6] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[6] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[7] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[7] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion
            param[8] = new SqlParameter("@ContractType", contractType);
            param[9] = new SqlParameter("@sys_PageIndex", page);
            param[10] = new SqlParameter("@sys_PageSize", rows);
            param[11] = new SqlParameter("@PCount", SqlDbType.Int);
            param[12] = new SqlParameter("@RCount", SqlDbType.Int);

            param[11].Direction = ParameterDirection.Output;
            param[12].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<ContractEntity>("exec [dbo].[P_Contract_S] @StartDate,@EndDate,@CustomerName,@CustomerID,@RecommendSolutionID,@FindType,@SysPersonID,@SearchPersonID,@ContractType,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[12].Value;

            return data;

        }




        /// <summary>
        /// 合同查询
        /// </summary>
        /// <param name="DB_CRMEntities">数据访问的上下文</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="listQuery">额外的参数</param>
        /// <returns></returns>      
        public List<ContractEntity> GetContractByServiceData(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[12];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }


            //客户名称
            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[2] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[2] = new SqlParameter("@CustomerName", "");
            }

            //客户编号
            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[3] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[3] = new SqlParameter("@CustomerID", "");
            }


            //客户服务方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[4] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[4] = new SqlParameter("@RecommendSolutionID", "");
            }

            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[5] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[5] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[6] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[6] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[6] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[7] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[7] = new SqlParameter("@SearchPersonID", "");
            }

            #endregion

            param[8] = new SqlParameter("@sys_PageIndex", page);
            param[9] = new SqlParameter("@sys_PageSize", rows);
            param[10] = new SqlParameter("@PCount", SqlDbType.Int);
            param[11] = new SqlParameter("@RCount", SqlDbType.Int);

            param[10].Direction = ParameterDirection.Output;
            param[11].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<ContractEntity>("exec [dbo].[P_ContractByService_S] @StartDate,@EndDate,@CustomerName,@CustomerID,@RecommendSolutionID,@FindType,@SysPersonID,@SearchPersonID,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[11].Value;

            return data;

        }
        /// <summary>
        /// 合同分类
        /// </summary>
        /// <param name="db"></param>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="CompanyType">欧孚或博尔捷</param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<ContractEntity> GetContractByServiceDataByType(DB_CRMEntities db, string SearchPersonID, string id, string order, string sort, string search, string CompanyType, int page, int rows, ref int total)
        {
            string where = string.Empty;

            SqlParameter[] param = new SqlParameter[13];
            Dictionary<string, string> queryDic = ValueConvert.StringToDictionary(search.GetString());

            #region  条件设置

            if (queryDic != null && queryDic.ContainsKey("StartDate") && !string.IsNullOrWhiteSpace(queryDic["StartDate"]))
            {
                param[0] = new SqlParameter("@StartDate", queryDic["StartDate"]);
            }
            else
            {
                param[0] = new SqlParameter("@StartDate", "");
            }

            if (queryDic != null && queryDic.ContainsKey("EndDate") && !string.IsNullOrWhiteSpace(queryDic["EndDate"]))
            {
                param[1] = new SqlParameter("@EndDate", queryDic["EndDate"]);
            }
            else
            {
                param[1] = new SqlParameter("@EndDate", "");
            }


            //客户名称
            if (queryDic != null && queryDic.ContainsKey("CustomerName") && !string.IsNullOrWhiteSpace(queryDic["CustomerName"]))
            {
                param[2] = new SqlParameter("@CustomerName", queryDic["CustomerName"]);
            }
            else
            {
                param[2] = new SqlParameter("@CustomerName", "");
            }

            //客户编号
            if (queryDic != null && queryDic.ContainsKey("CustomerID") && !string.IsNullOrWhiteSpace(queryDic["CustomerID"]))
            {
                param[3] = new SqlParameter("@CustomerID", queryDic["CustomerID"]);
            }
            else
            {
                param[3] = new SqlParameter("@CustomerID", "");
            }


            //客户服务方案
            if (queryDic != null && queryDic.ContainsKey("RecommendSolutionID") && !string.IsNullOrWhiteSpace(queryDic["RecommendSolutionID"]))
            {
                param[4] = new SqlParameter("@RecommendSolutionID", queryDic["RecommendSolutionID"]);
            }
            else
            {
                param[4] = new SqlParameter("@RecommendSolutionID", "");
            }

            //查询类型
            if (queryDic != null && queryDic.ContainsKey("FindType") && !string.IsNullOrWhiteSpace(queryDic["FindType"]))
            {
                param[5] = new SqlParameter("@FindType", queryDic["FindType"]);
            }
            else
            {
                param[5] = new SqlParameter("@FindType", "");
            }

            if (queryDic != null && queryDic.ContainsKey("SysPersonID") && !string.IsNullOrWhiteSpace(queryDic["SysPersonID"]))
            {
                param[6] = new SqlParameter("@SysPersonId", queryDic["SysPersonID"].Trim().TrimEnd(new char[] { ',' }));
            }
            else if (!string.IsNullOrWhiteSpace(id))
            {
                param[6] = new SqlParameter("@SysPersonId", id);
            }
            else
            {
                param[6] = new SqlParameter("@SysPersonId", "");
            }

            if (!string.IsNullOrWhiteSpace(SearchPersonID))
            {
                param[7] = new SqlParameter("@SearchPersonID", SearchPersonID);
            }
            else
            {
                param[7] = new SqlParameter("@SearchPersonID", "");
            }
            if (!string.IsNullOrWhiteSpace(CompanyType))
            {
                param[8] = new SqlParameter("@CompanyType", CompanyType);
            }
            else
            {
                param[8] = new SqlParameter("@CompanyType", "");
            }
            #endregion

            param[9] = new SqlParameter("@sys_PageIndex", page);
            param[10] = new SqlParameter("@sys_PageSize", rows);
            param[11] = new SqlParameter("@PCount", SqlDbType.Int);
            param[12] = new SqlParameter("@RCount", SqlDbType.Int);

            param[11].Direction = ParameterDirection.Output;
            param[12].Direction = ParameterDirection.Output;
            var data = db.Database.SqlQuery<ContractEntity>("exec [dbo].[P_ContractByServiceByNew_S] @StartDate,@EndDate,@CustomerName,@CustomerID,@RecommendSolutionID,@FindType,@SysPersonID,@SearchPersonID,@CompanyType,@sys_PageIndex,@sys_PageSize,@PCount output,@RCount output", param).ToList();

            total = (int)param[12].Value;

            return data;

        }


        #region 获取相同合同编号的最大序列号

        /// <summary>
        /// 获取相同合同编号的最大序列号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="PrefixCode"></param>
        /// <returns></returns>
        public long GetMaxContractNo(DB_CRMEntities db, string PrefixCode)
        {
            long maxNo = 0;
            string sql = string.Format(@"SELECT MAX(ContractNO) AS MaxContractNO FROM TB_Contract WHERE LEFT(ContractNO,LEN('{0}'))='{0}'", PrefixCode);
            string sMaxContractNO = db.Database.SqlQuery<string>(sql).ToList()[0];
            if (!string.IsNullOrWhiteSpace(sMaxContractNO))
            {
                string sMaxNo = sMaxContractNO.Substring(PrefixCode.Length);//.Remove(0, sMaxContractNO.Length);
                long.TryParse(sMaxNo, out maxNo);
            }

            return maxNo;
        }

        #endregion

        public int GetContractNOCount(DB_CRMEntities db, string ContractNO)
        {
            return db.TB_Contract.Where(s => s.ContractNO.Equals(ContractNO)).Count();
        }
        /// <summary>
        /// 根据合同ID，获取数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ContractEntity GetEntity(DB_CRMEntities db, string keyValue)
        {

            var data = db.Database.SqlQuery<ContractEntity>(string.Format(@" SELECT ct.ContractID,ct.CustomerID,ct.ContractMoney,ct.EffectiveDate,ct.Deadline
                   ,ct.Package,ct.Annex,ct.Years,ct.ContractCount,ct.IsDelete,ct.VersionNo,ct.CreatedBy,ct.CreatedTime,ct.LastUpdatedBy,ct.LastUpdatedTime,ct.ContractNO
                    ,ct.SigningCompany,ct.Flag,ct.PhoneNumber,ct.ProjectLeader,ct.ContractType,ct.ContractName,ct.IsNew,ct.PayType,ct.Remark,cst.CustomerName,Solution.RecommendSolutionName 
                    ,p.MyName AS CreatedByName,p.DepartmentName,fd1.MyTexts AS SigningCompanyName  FROM dbo.TB_Contract ct
                  INNER JOIN 
                (SELECT a.ContractID,(SELECT   b1.MyTexts+',' FROM TB_ContractSolution a1 LEFT JOIN SysField b1 ON a1.RecommendSolutionID=b1.Id
                WHERE a1.ContractID=a.ContractID  FOR XML PATH('')) AS RecommendSolutionName FROM TB_ContractSolution a  
                 WHERE  1=1 
                 GROUP by a.ContractID) Solution ON Solution.ContractID=ct.ContractID  
                 LEFT JOIN V_PersonOrDept p ON ct.CreatedBy=p.Id 
                 LEFT JOIN TB_Customer cst ON  ct.CustomerID=cst.CustomerID
                 LEFT JOIN dbo.SysField fd1 ON ct.SigningCompany=fd1.Id WHERE ct.ContractID='{0}'", keyValue)).ToList().FirstOrDefault();
            return data;
        }

        public void Dispose()
        {
        }
    }
}
