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
    /// 模块：通话记录
    /// 作者：Jonny
    /// 日期：2015.10.26
    /// </summary>
    public partial class UnCallRecordingRepository : BaseRepository<UnCallMeta>, IDisposable
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
        public IQueryable<CallRecordingModel> GetCallRecordingData(string userID, UnCall_DBEntities UnCallDb, DB_CRMEntities db, string order, string sort, string search, params object[] listQuery)
        {
            var query = (from f in UnCallDb.cdr
                         select new CallRecordingModel
                         {
                             CallDate = f.calldate,
                             Dst = f.dst,
                             Dcontext = f.dcontext,
                             Billsec = f.billsec,
                             Disposition = f.disposition,
                             Uniqueid = f.uniqueid,
                             UserField = f.userfield,
                             Src = f.src

                         }).Distinct();


            var searchParm = "";
            var searchRole = "";
            int canSee = 1;
            if (!string.IsNullOrEmpty(search))
            {
                searchParm = search.Split('^')[0].ToString();
                searchRole = search.Split('^')[1].ToString();
            }

            #region 根据权限查询

            if (!string.IsNullOrEmpty(searchRole))
            {
                string dept = searchRole.Split('&')[0].ToString();
                string personIds = searchRole.Split('&')[1].ToString();
                List<string> FCodeList = new List<string>();
                if (dept == "0"||dept=="")
                {
                    FCodeList = db.SysPerson.Where(s => personIds.Contains(s.Id) && s.TelephoneExt != null).Select(p => p.TelephoneExt).ToList();
                }
                else
                {
                    //按照部门
                    FCodeList = db.SysPerson.Where(s => personIds.Contains(s.SysDepartmentId) && s.TelephoneExt != null).Select(p => p.TelephoneExt).ToList();
                }
                if (FCodeList != null && FCodeList.Count > 0)
                {
                    var FCodes = "";
                    for (int i = 0; i < FCodeList.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(FCodeList[i]))
                        {
                            FCodes += FCodeList[i].Trim().ToString() + ",";

                        }

                    }
                    if (!string.IsNullOrEmpty(FCodes))
                    {
                        FCodes = FCodes.Substring(0, FCodes.Length - 1);
                        query = query.Where(s => FCodes.Contains(s.Src));
                    }

                }
            }
            else
            {
                int count = GetUserRole(db, userID);
                if (count > 0)
                {
                    List<string> FCodeList = new List<string>();
                    string sqlFCode =
                        string.Format(
                            @"select distinct TelephoneExt from SysPerson where SysDepartmentId in(select id from SysDepartment where ParentId=(select SysDepartmentId from SysPerson where id='{0}')) and TelephoneExt is not null", userID);
                    FCodeList = db.Database.SqlQuery<string>(sqlFCode).ToList();


                    var FCodes = "";
                    for (int i = 0; i < FCodeList.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(FCodeList[i]))
                        {
                            FCodes += FCodeList[i].Trim().ToString() + ",";

                        }

                    }

                    if (!string.IsNullOrEmpty(FCodes))
                    {
                        FCodes = FCodes.Substring(0, FCodes.Length - 1);

                        query = query.Where(s => FCodes.Contains(s.Src));

                    }
                    else
                    {
                        canSee = 0;
                    }
                }
                else
                {
                    string sqlFCode =
                      string.Format(
                          @"select distinct TelephoneExt from SysPerson where id='{0}' and  TelephoneExt is not null", userID);
                    var fcode = db.Database.SqlQuery<string>(sqlFCode).SingleOrDefault();
                    if (!string.IsNullOrEmpty(fcode))
                    {
                        query = query.Where(s => s.Src.Equals(fcode));
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            #endregion
            if (!string.IsNullOrEmpty(searchParm))
            {
                string[] sparam = searchParm.Split('$');

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
                        query = query.Where(s => s.CallDate < en && s.CallDate >= be);
                    }
                    else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                    {
                        DateTime? be = DateTime.Parse(begin);
                        query = query.Where(s => s.CallDate >= be);
                    }
                    else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? en = DateTime.Parse(end);
                        query = query.Where(s => s.CallDate <= en);
                    }
                    else
                    {
                        //按当天查询
                        var dateMax = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                        var dateMin = DateTime.Parse(DateTime.Now.ToShortDateString());
                        query = query.Where(s => s.CallDate < dateMax && s.CallDate > dateMin);
                    }
                }


                //通话时长
                string secondType = sparam[1].ToString().Trim();
                if (!string.IsNullOrEmpty(secondType) && secondType != "--请选择--")
                {
                    int seconds = int.Parse(sparam[2].ToString().Trim());
                    switch (secondType)
                    {
                        case "1510261530163672340fc22487c66":
                            query = query.Where(s => s.Billsec >= seconds);
                            break;
                        case "1510261530426497373216300bc78":
                            query = query.Where(s => s.Billsec <= seconds);
                            break;
                        case "151026153056545532175cbe1e6d6":
                            query = query.Where(s => s.Billsec == seconds);
                            break;
                        default:
                            break;
                    }

                }
                //通话状态
                string callType = sparam[3].ToString().Trim();
                if (!string.IsNullOrEmpty(callType) && callType != "--请选择--")
                {
                    switch (callType)
                    {
                        case "1510261531487255166e364dce5d7":
                            query = query.Where(s => s.Disposition.Equals("ANSWERED"));
                            break;
                        case "1510261531345377051e9ac580ca8":
                            query = query.Where(s => !s.Disposition.Equals("ANSWERED"));
                            break;
                        default:
                            break;
                    }
                }
                //模糊查询
                string searchName = sparam[4].ToString().Trim();
                if (searchName != "")
                {
                    bool isChinese = false;
                    for (int i = 0; i < searchName.Length; i++)
                    {
                        if ((int)searchName[i] > 127)
                        {
                            isChinese = true;
                            break;
                            ;
                        }
                    }
                    if (isChinese)
                    {
                        string sqlTel =
                            string.Format(
                                @"select distinct REPLACE(f.CallPhone, '-', '') as callPhone from TB_CustomerFollow f,TB_Customer c where f.CustomerID = c.CustomerID and f.ContactState=1 and c.CustomerName like '%{0}%'",
                                searchName);

                        List<string> teList = new List<string>();

                        teList = db.Database.SqlQuery<string>(sqlTel).ToList();
                        var tCodes = "";
                        for (int i = 0; i < teList.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(teList[i]))
                            {
                                tCodes += teList[i].Trim().ToString() + ",";

                            }

                        }

                        if (!string.IsNullOrEmpty(tCodes))
                        {
                            tCodes = tCodes.Substring(0, tCodes.Length - 1);

                            query = query.Where(s => tCodes.Contains(s.Dst));

                        }
                    }
                    else
                    {
                        query = query.Where(s => s.Dst.Equals(searchName));
                    }


                }

                #endregion

            }
            else
            {
                if (canSee == 0)
                {
                    return null;
                }
                var dateMax = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                var dateMin = DateTime.Parse(DateTime.Now.ToShortDateString());
                query = query.Where(s => s.CallDate < dateMax && s.CallDate > dateMin);
            }


            query = query.OrderBy(s => s.CallDate);
            //   var sql = query.ToString();
            return query.AsQueryable();

        }


        /// <summary>
        /// 获取批注
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public IQueryable<SoundCommentModel> GetComment(DB_CRMEntities db, string cid)
        {
            var query = (from f in db.TB_SoundComment
                         where f.SoundID.Equals(cid)
                         select new SoundCommentModel
                         {
                             CommentID = f.CommentID,
                             TelCode = f.TelCode,
                             SoundID = f.SoundID,
                             SoundURL = f.SoundURL,
                             SoundType = f.SoundType,
                             SoundGoodRemark = f.SoundGoodRemark,
                             SoundBadRemark = f.SoundBadRemark,
                             SoundImproveRemark = f.SoundImproveRemark

                         }).Distinct();
            return query.AsQueryable();

        }

        /// <summary>
        /// 保存批注
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <param name="callProcess"></param>
        /// <param name="goodRemark"></param>
        /// <param name="badRemark"></param>
        /// <param name="improveRemark"></param>
        /// <param name="cid"></param>
        /// <param name="url"></param>
        /// <param name="fcode"></param>
        /// <param name="isupdate"></param>
        /// <returns></returns>
        public int AddCommentInfo(DB_CRMEntities db, string id, string callProcess, string goodRemark, string badRemark, string improveRemark, string cid, string url, string fcode, string isupdate)
        {
            int isAdd = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    int a = 0;
                    string sql = string.Empty;
                    if (isupdate == "1")
                    {
                        sql = string.Format(@"UPDATE TB_SoundComment SET SoundType = '{0}', SoundGoodRemark = '{1}', SoundBadRemark = '{2}', SoundImproveRemark = '{3}', UpdateTime = getdate (), UpdateUserID = '{4}' WHERE SoundID = '{5}'", callProcess, goodRemark, badRemark, improveRemark, id, cid);
                    }
                    else
                    {
                        sql = string.Format(@"INSERT INTO TB_SoundComment ( CommentID, TelCode, SoundID, SoundURL, SoundType, SoundGoodRemark, SoundBadRemark, SoundImproveRemark, CreateTime, CreateUserID, UpdateTime, UpdateUserID ) VALUES ( newid () ,{0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', getdate (), '{7}', getdate (), '{7}' )", int.Parse(fcode), cid, url, callProcess, goodRemark, badRemark, improveRemark, id);
                    }

                    a = db.Database.ExecuteSqlCommand(sql);

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
        /// 获取登录用户的权限（leader Or Sales）
        /// </summary>
        /// <param name="id"></param>
        public int GetUserRole(DB_CRMEntities db, string id)
        {
            var query = (from s in db.SysPerson
                         join d in db.SysDepartment on s.SysDepartmentId equals d.Id
                         join f in db.SysDepartment on d.Id equals f.ParentId
                         where s.Id.Equals(id)
                         select f.Id
                ).Count();
            return query;

        }

        /// <summary>
        /// 呼叫报表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="UnCallDb"></param>
        /// <param name="db"></param>
        /// <param name="search"></param>
        /// <param name="listQuery"></param>
        /// <returns></returns>
        public List<CallRecordingModel> GetReportsData(string userID, UnCall_DBEntities UnCallDb, DB_CRMEntities db, string search, params object[] listQuery)
        {
            string sql = string.Empty;

            var searchParm = "";
            var searchRole = "";
            if (!string.IsNullOrEmpty(search))
            {
                searchParm = search.Split('^')[0].ToString();
                searchRole = search.Split('^')[1].ToString();
            }

            if (!string.IsNullOrEmpty(searchParm))
            {
                string[] sparam = searchParm.Split('$');

                #region ==条件搜索==

                //日期查询
                if (!string.IsNullOrEmpty(sparam[0].ToString().Trim()))
                {
                    string[] times = sparam[0].ToString().Trim().Split(',');
                    string begin = times[0].ToString().Trim();
                    string end = times[1].ToString().Trim();
                    //通话时长
                    string seconds = sparam[1].ToString().Trim();
                    if (string.IsNullOrEmpty(seconds))
                    {
                        seconds = "45";
                    }

                    if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? dateMin = DateTime.Parse(begin);
                        DateTime? dateMax = DateTime.Parse(end).AddDays(1);
                        sql = string.Format(@"SELECT c.src, ( SELECT COUNT(1) FROM cdr b WHERE b.src = c.src AND b.calldate BETWEEN '{0}' AND '{1}'  ) AS Dial, ( SELECT COUNT(1) FROM cdr j WHERE j.src = c.src AND j.disposition = 'ANSWERED' AND j.calldate BETWEEN '{0}' AND '{1}' ) AS Connect, ( SELECT COUNT(1) FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate BETWEEN '{0}' AND '{1}' AND h.billsec >= {2} ) AS Valid, ( SELECT case when SUM(h.billsec) is NULL then 0 ELSE  SUM(h.billsec) END FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate BETWEEN '{0}' AND '{1}' ) AS CallTotal FROM cdr c WHERE LENGTH(c.src) = 3 GROUP BY c.src ORDER BY c.src", dateMin, dateMax, int.Parse(seconds));
                    }
                    else if (!string.IsNullOrEmpty(begin) && string.IsNullOrEmpty(end))
                    {
                        DateTime? dateMax = DateTime.Parse(begin);
                        sql = string.Format(@"SELECT c.src, ( SELECT COUNT(1) FROM cdr b WHERE b.src = c.src AND b.calldate> '{0}'  ) AS Dial, ( SELECT COUNT(1) FROM cdr j WHERE j.src = c.src AND j.disposition = 'ANSWERED' AND j.calldate> '{0}' ) AS Connect, ( SELECT COUNT(1) FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate> '{0}'  AND h.billsec >= {1} ) AS Valid, ( SELECT case when SUM(h.billsec) is NULL then 0 ELSE  SUM(h.billsec) END FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate> '{0}'  ) AS CallTotal FROM cdr c WHERE LENGTH(c.src) = 3 GROUP BY c.src ORDER BY c.src", dateMax, int.Parse(seconds));
                    }
                    else if (string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
                    {
                        DateTime? dateMin = DateTime.Parse(end);
                        sql = string.Format(@"SELECT c.src, ( SELECT COUNT(1) FROM cdr b WHERE b.src = c.src AND b.calldate< '{0}'  ) AS Dial, ( SELECT COUNT(1) FROM cdr j WHERE j.src = c.src AND j.disposition = 'ANSWERED' AND j.calldate< '{0}' ) AS Connect, ( SELECT COUNT(1) FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate< '{0}'  AND h.billsec >= {1} ) AS Valid, ( SELECT case when SUM(h.billsec) is NULL then 0 ELSE  SUM(h.billsec) END FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate< '{0}'  ) AS CallTotal FROM cdr c WHERE LENGTH(c.src) = 3 GROUP BY c.src ORDER BY c.src", dateMin, int.Parse(seconds));
                    }
                    else
                    {
                        //按当天查询
                        var dateMax = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                        var dateMin = DateTime.Parse(DateTime.Now.ToShortDateString());
                        sql = string.Format(@"SELECT c.src, ( SELECT COUNT(1) FROM cdr b WHERE b.src = c.src AND b.calldate BETWEEN '{0}' AND '{1}'  ) AS Dial, ( SELECT COUNT(1) FROM cdr j WHERE j.src = c.src AND j.disposition = 'ANSWERED' AND j.calldate BETWEEN '{0}' AND '{1}' ) AS Connect, ( SELECT COUNT(1) FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate BETWEEN '{0}' AND '{1}' AND h.billsec >= {2} ) AS Valid, ( SELECT case when SUM(h.billsec) is NULL then 0 ELSE  SUM(h.billsec) END FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate BETWEEN '{0}' AND '{1}'  ) AS CallTotal FROM cdr c WHERE LENGTH(c.src) = 3 GROUP BY c.src ORDER BY c.src", dateMin, dateMax, int.Parse(seconds));
                    }
                }

                #endregion

            }
            else
            {
                var dateMax = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString());
                var dateMin = DateTime.Parse(DateTime.Now.ToShortDateString());
                sql = string.Format(@"SELECT c.src, ( SELECT COUNT(1) FROM cdr b WHERE b.src = c.src AND b.calldate BETWEEN '{0}' AND '{1}'  ) AS Dial, ( SELECT COUNT(1) FROM cdr j WHERE j.src = c.src AND j.disposition = 'ANSWERED' AND j.calldate BETWEEN '{0}' AND '{1}' ) AS Connect, ( SELECT COUNT(1) FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate BETWEEN '{0}' AND '{1}' AND h.billsec >= 45 ) AS Valid, ( SELECT case when SUM(h.billsec) is NULL then 0 ELSE  SUM(h.billsec) END FROM cdr h WHERE h.src = c.src AND h.disposition = 'ANSWERED' AND h.calldate BETWEEN '{0}' AND '{1}' ) AS CallTotal FROM cdr c WHERE LENGTH(c.src) = 3 GROUP BY c.src ORDER BY c.src", dateMin, dateMax);
            }
            #region 根据权限查询

            if (!string.IsNullOrEmpty(searchRole))
            {
                string dept = searchRole.Split('&')[0].ToString();
                string personIds = searchRole.Split('&')[1].ToString();
                List<string> FCodeList = new List<string>();
                if (dept == "0" || dept == "")
                {
                    FCodeList = db.SysPerson.Where(s => personIds.Contains(s.Id) && s.State.Equals("开启") && s.TelephoneExt != null).Select(p => p.TelephoneExt).ToList();
                }
                else
                {
                    //按照部门
                    FCodeList = db.SysPerson.Where(s => personIds.Contains(s.SysDepartmentId) && s.State.Equals("开启") && s.TelephoneExt != null).Select(p => p.TelephoneExt).ToList();
                }
                if (FCodeList != null && FCodeList.Count > 0)
                {
                    var FCodes = "";
                    for (int i = 0; i < FCodeList.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(FCodeList[i]))
                        {
                            FCodes += FCodeList[i].Trim().ToString() + ",";

                        }

                    }
                    if (!string.IsNullOrEmpty(FCodes))
                    {
                        FCodes = FCodes.Substring(0, FCodes.Length - 1);

                        var list = UnCallDb.Database.SqlQuery<CallRecordingModel>(sql).Where(s => FCodes.Contains(s.Src)).ToList();
                        return list;
                    }
                }
            }
            else
            {
                int count = GetUserRole(db, userID);
                if (count > 0)
                {
                    List<string> FCodeList = new List<string>();
                    string sqlFCode =
                        string.Format(
                            @"select distinct TelephoneExt from SysPerson where State='开启' and  SysDepartmentId in(select id from SysDepartment where ParentId=(select SysDepartmentId from SysPerson where id='{0}')) and TelephoneExt is not null", userID
                            );
                    FCodeList = db.Database.SqlQuery<string>(sqlFCode).ToList();


                    var FCodes = "";
                    for (int i = 0; i < FCodeList.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(FCodeList[i]))
                        {
                            FCodes += FCodeList[i].Trim().ToString() + ",";

                        }

                    }

                    if (!string.IsNullOrEmpty(FCodes))
                    {
                        FCodes = FCodes.Substring(0, FCodes.Length - 1);

                        var list = UnCallDb.Database.SqlQuery<CallRecordingModel>(sql).Where(s => FCodes.Contains(s.Src)).ToList();
                        return list;
                    }
                }
            }

            #endregion

            return null;
        }







        public void Dispose()
        {
        }
    }
}
