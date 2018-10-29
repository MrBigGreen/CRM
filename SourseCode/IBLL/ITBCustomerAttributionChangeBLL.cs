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
    /// 创建人：chand
    /// 创建时间：2015-06-29
    /// 描述说明：客户归属变更-接口
    /// </summary>
    public interface ITBCustomerAttributionChangeBLL : IBaseDAL<TB_CustomerAttributionChange>
    {
        bool UpdateByProc(ref ValidationErrors validationErrors, string CustomerAttributionChangeID, string SysPersonID, string CustomerID, string UpdatedBy);
    }
}
