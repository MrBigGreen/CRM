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
    ///  创建时间：2016-03-31
    ///  描述：薪资明细    接口
    /// </summary>
    public interface ITBCustomerServiceBLL : IBaseDAL<TB_CustomerService>
    {
        bool Create(ref ValidationErrors validationErrors, string CustomerID, string SysPersonIDs);
        List<CustomerServiceEntity> GetCustomerServiceData(string CustomerID);
    }
}
