using Offertech.Application.Entity.CustomerManage;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.IService.CustomerManage
{
    public interface ITrailRecordExtendService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        IEnumerable<TrailRecordModel> GetLastList(string objectId, int rows);
        /// <summary>
        /// 获取跟进记录
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
         IEnumerable<TrailRecordModel> GetPageList(string objectId, Pagination pagination, string queryJson);
        #endregion
    }
}
