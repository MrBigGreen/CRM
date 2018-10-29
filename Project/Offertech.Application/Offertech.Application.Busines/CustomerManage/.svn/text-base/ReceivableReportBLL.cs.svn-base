using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.CustomerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-04-19 17:40
    /// 描 述：应收账款报表
    /// </summary>
    public class ReceivableReportBLL
    {
        private IReceivableReportService service = new ReceivableReportService();

        /// <summary>
        /// 获取收款列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ReceivableReportModel> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
    }
}
