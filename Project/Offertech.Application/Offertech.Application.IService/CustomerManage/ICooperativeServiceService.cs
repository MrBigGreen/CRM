using Offertech.Application.Entity.CustomerManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;

namespace Offertech.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:49
    /// 描 述：客户合作服务类型
    /// </summary>
    public interface ICooperativeServiceService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<CooperativeServiceEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<CooperativeServiceEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        CooperativeServiceEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, CooperativeServiceEntity entity);
        #endregion
    }
}
