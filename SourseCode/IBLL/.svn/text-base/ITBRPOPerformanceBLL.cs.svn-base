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
    public interface ITBRPOPerformanceBLL : IBaseDAL<TB_RPOPerformance>
    {

        List<RPOPerformanceEntity> GetSalesPerfData(int page, int rows, string order, string sort, string search, ref int total);

        List<RPOPerformanceEntity> GetRPOPerfPersonData(int page, int rows, string order, string sort, string search, ref int total);

        bool GetProjectFinish(ref ValidationErrors validationErrors, List<string> prjList, string CurrentPerson);
    }
}
