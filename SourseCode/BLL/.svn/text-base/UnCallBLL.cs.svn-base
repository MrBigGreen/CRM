using CRM.IBLL;
using Common;
using CRM.DAL;
using CRM.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;


namespace CRM.BLL
{
    public class UnCallBLL : IUnCallBLL, IDisposable
    {
        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;

        protected UnCall_DBEntities UnCallDb;

        /// <summary>
        /// 人员的数据库访问对象
        /// </summary>
        UnCallRecordingRepository repository = new UnCallRecordingRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public UnCallBLL()
        {
            db = new DB_CRMEntities();
            UnCallDb = new UnCall_DBEntities();
        }


        /// <summary>
        /// 通话记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CallRecordingModel> GetCallRecordingData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {

            IQueryable<CallRecordingModel> queryData = repository.GetCallRecordingData(id, UnCallDb, db, order, sort, search);
           
            List<CallRecordingModel> rim = new List<CallRecordingModel>();
            if (queryData != null)
            {
                total = queryData.Count();
                if (total > 0)
                {
                    if (page <= 1)
                    {
                        queryData = queryData.Take(rows);
                    }
                    else
                    {
                        queryData = queryData.Skip((page - 1) * rows).Take(rows);
                    }

                }

                List<CallRecordingModel> list = queryData.ToList();

                foreach (var item in list)
                {

                    item.CompanyName = (from e in db.TB_Customer
                                        where e.ContactPhone.Replace("-", "").Equals(item.Dst) || e.ContactTel.Replace("-", "").Equals(item.Dst)
                                        select e.CustomerName).Take(1).SingleOrDefault<string>();

                    item.FcodeUserName = (from e in db.SysPerson
                                          where e.TelephoneExt.Equals(item.Src)
                                          select e.MyName).SingleOrDefault<string>();
                    rim.Add(item);
                }


                ////模糊查询
                //var searchParm = "";

                //if (!string.IsNullOrEmpty(search))
                //{
                //    searchParm = search.Split('^')[0].ToString();
                //}
                //if (!string.IsNullOrEmpty(searchParm))
                //{
                //    string[] sparam = searchParm.Split('$');
                //    string searchName = sparam[4].ToString().Trim();
                //    if (searchName != "")
                //    {
                //        rim = rim.Where(s => s.CompanyName.Contains(searchName) || s.Dst.Contains(searchName)).ToList();
                //    }
                //}
                ////分页
                //if (total > 0)
                //{
                //    if (page <= 1)
                //    {
                //        return rim.Take(rows).ToList();
                //    }
                //    else
                //    {
                //        return rim.Skip((page - 1) * rows).Take(rows).ToList();
                //    }

                //}
            }
            return rim;



        }
        /// <summary>
        /// 获取批注
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<SoundCommentModel> GetComment(string cid)
        {
            IQueryable<SoundCommentModel> queryData = repository.GetComment(db, cid);
            return queryData.ToList();

        }

        public bool AddCommentInfo(string id, string callProcess, string goodRemark, string badRemark,
            string improveRemark, string cid, string url, string fcode, string isupdate)
        {

            int i = repository.AddCommentInfo(db, id, callProcess, goodRemark, badRemark, improveRemark, cid, url, fcode, isupdate);
            ;
            if (i == 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取登录用户的权限（leader Or Sales）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUserRole(string id)
        {
            int i = repository.GetUserRole(db, id);
            return i;
        }

        public List<CallRecordingModel> GetReportsData(string id, int page, int rows, string order, string sort,
            string search, ref int total)
        {
            List<CallRecordingModel> list = repository.GetReportsData(id, UnCallDb, db, search);
            List<CallRecordingModel> rim = new List<CallRecordingModel>();
            if (list != null)
            {
                total = list.Count();
                if (total > 0)
                {
                    if (page <= 1)
                    {
                        list = list.Take(rows).ToList();
                    }
                    else
                    {
                        list = list.Skip((page - 1) * rows).Take(rows).ToList();
                    }

                }


                foreach (var item in list)
                {
                    item.FcodeUserName = (from e in db.SysPerson
                                          where e.TelephoneExt.Equals(item.Src)
                                          select e.MyName).Take(1).SingleOrDefault<string>();

                    item.Efficiency = item.Connect == 0 ? "0" : (decimal.Parse(item.Connect.ToString()) / decimal.Parse(item.Dial.ToString())).ToString("P");
                    item.CallTotals = item.CallTotal == 0.00 ? "0" : (item.CallTotal / 60).ToString("#0.00");
                    rim.Add(item);
                }
            }
            return rim;
        }



        /// <summary>
        /// 导出报表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<CallRecordingModel> GetReportsExcel(string id, string search)
        {
            List<CallRecordingModel> list = repository.GetReportsData(id, UnCallDb, db, search);
            List<CallRecordingModel> rim = new List<CallRecordingModel>();
            if (list != null)
            {

                foreach (var item in list)
                {
                    item.FcodeUserName = (from e in db.SysPerson
                                          where e.TelephoneExt.Equals(item.Src)
                                          select e.MyName).Take(1).SingleOrDefault<string>();

                    item.Efficiency = item.Connect == 0 ? "0" : (decimal.Parse(item.Connect.ToString()) / decimal.Parse(item.Dial.ToString())).ToString("P");
                    item.CallTotals = item.CallTotal == 0.00 ? "0" : (item.CallTotal / 60).ToString("#0.00");
                    rim.Add(item);
                }
            }
            return rim;
        }


        public List<string> GetExportSounds(string search)
        {
            //var urlFile = "http://192.168.1.254/play_files_ec.php?f_path=";

            return null;
        }








        /// <summary>
        /// 下拉框
        /// </summary>
        /// <param name="type">参数</param>
        /// <returns></returns>
        public IQueryable GetCombox(string type)
        {

            var queryInfo = from m in db.SysField
                            where m.ParentId == type
                            orderby m.Sort
                            select new { id = m.Id, text = m.MyTexts };
            return queryInfo;
        }

        public void Dispose()
        {
        }
    }
}
