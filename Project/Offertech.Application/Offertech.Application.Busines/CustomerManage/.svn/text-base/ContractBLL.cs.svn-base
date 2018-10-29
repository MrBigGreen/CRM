using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.CustomerManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Application.IService.SystemManage;
using Offertech.Application.Service.SystemManage;
using Offertech.Application.Code;

namespace Offertech.Application.Busines.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:51
    /// 描 述：合同管理
    /// </summary>
    public class ContractBLL
    {
        private IContractService service = new ContractService();
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ContractEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ContractEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取合同到期数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ContractEntity> GetDeadlineList(string queryJson)
        {
            return service.GetDeadlineList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ContractEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取欧孚销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetOfferDataBySale(Pagination pagination, string queryJson)
        {
            return service.GetOfferDataBySale(pagination, queryJson);
        }

        /// <summary>
        /// 获取博尔捷销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetBridgeDataBySale(Pagination pagination, string queryJson)
        {
            return service.GetBridgeDataBySale(pagination, queryJson);
        }
        /// <summary>
        /// 获取欧孚客服合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetOfferDataByKeFu(Pagination pagination, string queryJson)
        {
            return service.GetOfferDataByKeFu(pagination, queryJson);
        }

        /// <summary>
        /// 获取欧孚销售合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetBridgeDataByKeFu(Pagination pagination, string queryJson)
        {
            return service.GetBridgeDataByKeFu(pagination, queryJson);
        }
        /// <summary>
        /// 获取合同数据列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageData(Pagination pagination, string queryJson)
        {
            return service.GetPageData(pagination, queryJson);
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
        public void SaveForm(string keyValue, ContractModel entity)
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
        /// 新增合同
        /// </summary>
        /// <param name="entity">合同主体对象</param>
        /// <param name="serviceEntityList">服务类型</param>
        public void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList)
        {
            try
            {
                service.AddForm(entity, serviceEntityList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 新增合同
        /// </summary>
        /// <param name="entity">合同主体对象</param>
        /// <param name="serviceEntityList">服务类型</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="moduleCode">模板编码</param>
        public void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList, string moduleId, string moduleCode)
        {
            try
            {
                service.AddForm(entity, serviceEntityList, moduleId, moduleCode);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
