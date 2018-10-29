using Common;
using CRM.DAL;
using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{


    /// <summary>
    ///  创建人：chand
    ///  创建时间：2016-03-31
    ///  描述：薪资明细    接口
    /// </summary>
    public interface ITPSalaryDetailsBLL : IBaseDAL<TP_SalaryDetails>
    {
        List<SalaryDetailsEntity> GetSalaryDetailsData(string id, string order, string sort, string search, int page, int rows, ref int total);

        List<int> GetAllSalaryMonth();

        bool InsertSalary(ref ValidationErrors validationErrors, Dictionary<string, string> dicData, string currentPersonID, bool Replace);
    }
}
