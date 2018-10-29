using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    /// 客户查重验证 接口
    /// </summary>
    [ServiceContract(Namespace = "http://crm.9191offer.com")]
    public interface ITBCustomerVerificationBLL : IBaseDAL<TB_CustomerVerification>
    {
        List<CustomerRepeatEntity> GetCustomerRepeat(string CustomerName, ref  int result, ref int total);
    }
}
