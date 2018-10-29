using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace Offertech.Application.IService.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:51
    /// 描 述：合同管理
    /// </summary>
    public interface IContractService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<ContractEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<ContractEntity> GetList(string queryJson);
        /// <summary>
        /// 获取合同到期数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<ContractEntity> GetDeadlineList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ContractEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取欧孚销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetOfferDataBySale(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetBridgeDataBySale(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取欧孚客服合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetOfferDataByKeFu(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetBridgeDataByKeFu(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageData(Pagination pagination, string queryJson);
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
        void SaveForm(string keyValue, ContractModel entity);
        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="entity">合同主体对象</param>
        /// <param name="serviceEntityList">服务类型</param>
        void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList);
        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="entity">合同主体对象</param>
        /// <param name="serviceEntityList">服务类型</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="moduleCode">模板编码</param>
        void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList, string moduleId, string moduleCode);
        #endregion
    }
}
