using Common;
using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    ///  创建人：chand
    ///  创建时间：2016-07-18
    ///  描述：     接口
    /// </summary>
    public interface ITBSalesPerformanceBLL : IBaseDAL<TB_SalesPerformance>
    {
        List<TB_SalesPerformance> GetByPersonID(string SysPersonID, int page, int rows, ref int total);

        bool IsExists(string SysPersonID, string TheMonth, int TheWeek);

        bool CreateCollection(ref ValidationErrors validationErrors, IQueryable<TB_SalesPerformance> entitys);

        List<string> GetMonths();

        List<TB_SalesPerformance> GetSalesPerfData(int page, int rows, string search, ref int total);


        List<SalesPerfReportModel> GetSalesPerfReport(string SearchPersonID, string search);
    }
}
