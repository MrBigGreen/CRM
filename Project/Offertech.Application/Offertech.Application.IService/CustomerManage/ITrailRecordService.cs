using Offertech.Application.Entity.CustomerManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace Offertech.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-21 16:10
    /// 描 述：跟进记录
    /// </summary>
    public interface ITrailRecordService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        IEnumerable<TrailRecordEntity> GetList(string objectId);
        /// <summary>
        /// 获取跟进记录
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
        IEnumerable<TrailRecordEntity> GetPageList(string objectId, Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        TrailRecordEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">筛选条件</param>
        /// <returns></returns>
        DataTable GetPlanData(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取跟进历史列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">筛选条件</param>
        /// <returns></returns>
        DataTable GetHistoryData(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        DataTable GetAppPlanData(string userId, Pagination pagination);
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
        void SaveForm(string keyValue, TrailRecordEntity entity);
        /// <summary>
        /// 移动端保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void AppSaveForm(string keyValue, TrailRecordEntity entity);
        #endregion
    }
}