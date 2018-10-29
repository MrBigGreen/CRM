using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL
{
    public partial class PayReportBLL : IBLL.IPayReportBLL
    {
        #region 初始化

        /// <summary>
        /// 私有的数据访问上下文
        /// </summary>
        protected DB_CRMEntities db;
        /// <summary>
        /// 客户的数据库访问对象
        /// </summary>
        PayReportRepository repository = new PayReportRepository();
        /// <summary>
        /// 构造函数，默认加载数据访问上下文
        /// </summary>
        public PayReportBLL()
        {
            db = new DB_CRMEntities();
        }
        /// <summary>
        /// 已有数据访问上下文的方法中调用
        /// </summary>
        /// <param name="entities">数据访问上下文</param>
        public PayReportBLL(DB_CRMEntities entities)
        {
            db = entities;
        }

        #endregion

        public List<ComboxEntity> GetCombox(string parm,string roleID)
        {
            List<ComboxEntity> list = new List<ComboxEntity>();
            if (!string.IsNullOrEmpty(parm))
            {
                list.Add(new ComboxEntity { id = "0", text = "全部", children = GetComboxList(parm, roleID) });
            }

            return list;
        }

        public List<ComboxEntity> GetComboxList(string parm, string roleID)
        {
            IQueryable<ComboxEntity> queryInfo = null;
            queryInfo = from m in db.SysField
                        join n in db.TP_SalarySubjectSysPerson on m.Id equals n.SalarySubjectID
                        where n.SysPersonID == roleID
                        orderby m.Sort
                        select new ComboxEntity { id = m.Id, text = m.MyTexts };
            List<ComboxEntity> list = queryInfo.ToList();
            return list;
        }

        public List<ComboxEntity> GetComboxChildList(string parm )
        {
            IQueryable<ComboxEntity> queryInfo = null;
            queryInfo = from c in db.TB_Customer
                        join s in db.TP_SalaryDetails on c.CustomerID equals s.CustomerID
                        join e in db.TP_Employees on s.EmpID equals e.EmpID
                        where e.ContractSubject==parm
                        orderby c.CustomerID
                        select new ComboxEntity { id = c.CustomerID, text = c.CustomerName };
            List<ComboxEntity> list = queryInfo.ToList();
            return list;
        }


        public List<PayReportEntity> GetSummaryReportData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetSummaryReportList(db, page, rows, order, sort, search, ref total);
        }

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<PayReportEntity> GetSocialSecurityReportData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetSocialSecurityReportList(db, page, rows, order, sort, search, ref total);
        }

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        public List<PayReportEntity> GetFundsReportData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetFundsReportList(db, page, rows, order, sort, search, ref total);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="checkValue"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<PayReportEntity> GetWagesReportBankInfo(string id, string checkValue, string date)
        {
            return repository.GetWagesReportBankInfo(db, id, checkValue, date);
        }
        public List<PayReportEntity> GetWagesReportInfo(string id, string checkValue, string date)
        {
            return repository.GetWagesReportBankInfo(db, id, checkValue, date);
        }
        public List<PayReportEntity> GetWagesReportPayInfo(string id, string checkValue, string date)
        {
            return repository.GetWagesReportBankInfo(db, id, checkValue, date);
        }
        public List<PayReportEntity> GetSocialReportData(string id, int page, int rows, string order, string sort, string search, ref int total)
        {
            return repository.GetSocialReportData(db, page, rows, order, sort, search, ref total);
        }
    }
}
