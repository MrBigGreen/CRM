using Offertech.Application.Entity.CustomerManage;
using Offertech.Data.Repository;
using Offertech.Util.WebControl;
using System.Collections.Generic;

namespace Offertech.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-04-28 16:48
    /// 描 述：现金余额
    /// </summary>
    public interface ICashBalanceService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<CashBalanceEntity> GetList(string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加收支余额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cashBalanceEntity"></param>
        void AddBalance(IRepository db, CashBalanceEntity cashBalanceEntity);
        #endregion
    }
}
