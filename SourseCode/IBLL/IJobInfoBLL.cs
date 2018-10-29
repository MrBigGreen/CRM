using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-07-14
    /// 描述说明：职位信息接口
    /// </summary>
    [ServiceContract(Namespace = "www.Offer.com")]
    public partial interface IJobInfoBLL
    {
        [OperationContract]
        List<JobInfoEntity> GetData(string id, int page, int rows, string order, string sort, string search, ref int total);
    }
}
