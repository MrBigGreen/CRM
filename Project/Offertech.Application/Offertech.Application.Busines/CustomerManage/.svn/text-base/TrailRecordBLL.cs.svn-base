using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.CustomerManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace Offertech.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-21 16:10
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordBLL
    {
        private ITrailRecordService service = new TrailRecordService();
        private ITrailRecordExtendService _ExtendService = new TrailRecordExtendService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TrailRecordEntity> GetList(string objectId)
        {
            return service.GetList(objectId);
        }
        /// <summary>
        /// 获取跟进记录
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
        public IEnumerable<TrailRecordEntity> GetPageList(string objectId, Pagination pagination, string queryJson)
        {
            return service.GetPageList(objectId, pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        public IEnumerable<TrailRecordModel> GetLastList(string objectId, int rows = 6)
        {
            return _ExtendService.GetLastList(objectId, rows);
        }
        /// <summary>
        /// 获取带有图片跟进记录
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">条件</param>
        /// <returns></returns>
        public IEnumerable<TrailRecordModel> GetPicPageList(string objectId, Pagination pagination, string queryJson)
        {
            return _ExtendService.GetPageList(objectId, pagination, queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TrailRecordEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">筛选条件</param>
        /// <returns></returns>
        public DataTable GetPlanData(Pagination pagination, string queryJson)
        {
            return service.GetPlanData(pagination, queryJson);
        }
        /// <summary>
        /// 获取跟进历史列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">筛选条件</param>
        /// <returns></returns>
        public DataTable GetHistoryData(Pagination pagination, string queryJson)
        {
            return service.GetHistoryData(pagination, queryJson);
        }
        /// <summary>
        /// 获取跟进计划列表
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        public DataTable GetAppPlanData(string userId, Pagination pagination)
        {
            return service.GetAppPlanData(userId, pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, TrailRecordEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 移动端保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void AppSaveForm(string keyValue, TrailRecordEntity entity)
        {
            try
            {
                service.AppSaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}