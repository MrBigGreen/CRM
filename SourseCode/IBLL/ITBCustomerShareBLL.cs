using CRM.DAL;
using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{

    public interface ITBCustomerShareBLL : IBaseDAL<TB_CustomerShare>
    {
        int GetCustomerShareCancel(string CustomerID, string SysPersonID);


        List<CustomerShareEntity> GetCustomerShareData(string search, int page, int rows, ref int total);


        bool GetCustomerReleaseAllShare(string SysPersonID, string OperatorID);

    }
}
