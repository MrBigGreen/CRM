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
    public interface ITBBillingBLL : IBaseDAL<TB_Billing>
    {
        bool AddOrEdit(ref ValidationErrors validationErrors, TB_BillingDetails entity);
        TB_BillingDetails GetDetailsById(string Id);
        List<TB_BillingDetails> GetDetails(string BillingID, int Category);
        List<string> GetMonths();

        bool IsExists(string TheMonth, int TheWeek);
    }


}
