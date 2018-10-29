using Offertech.Application.Entity.AuthorizeManage;
using Offertech.Application.Entity.ReportManage;
using Offertech.Application.IService.ReportManage;
using Offertech.Application.Service.ReportManage;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace Offertech.Application.Busines.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class RptTempBLL
    {
        private IRptTempService service = new RptTempService();

        #region 获取数据
        /// <summary>
        /// 报表模板列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RptTempEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 报表模板实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RptTempEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="reportId">主键</param>
        /// <returns></returns>
        public string GetReportData(string reportId)
        {
            return service.GetReportData(reportId);
        }
        /// <summary>
        /// 客户汇总报表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerSummaryData(Pagination pagination, string queryJson)
        {
            return service.GetCustomerSummaryData(pagination, queryJson);
        }
        /// <summary>
        /// 客户汇总报表-按部门
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerSummaryByDeptData(Pagination pagination, string queryJson)
        {
            return service.GetCustomerSummaryByDeptData(pagination, queryJson);
        }
        /// <summary>
        /// 客户跟进报表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerFollowData(Pagination pagination, string queryJson)
        {
            return service.GetCustomerFollowData(pagination, queryJson);
        }
        /// <summary>
        /// 客户新进报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCustomerNewData(Pagination pagination, string queryJson)
        {
            return service.GetCustomerNewData(pagination, queryJson);
        }
        /// <summary>
        /// 销售合同报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetContractSaleData(Pagination pagination, string queryJson)
        {
            return service.GetContractSaleData(pagination, queryJson);
        }
        /// <summary>
        /// 客服合同报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetContractServiceData(Pagination pagination, string queryJson)
        {
            return service.GetContractServiceData(pagination, queryJson);
        }
        /// <summary>
        /// 我创建的客户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMyselfCustomerData(Pagination pagination, string queryJson)
        {
            return service.GetMyselfCustomerData(pagination, queryJson);
        }

        /// <summary>
        /// 市场部导入客户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMarketCustomerData(Pagination pagination, string queryJson)
        {
            return service.GetMarketCustomerData(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除报表模板
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
        /// 保存报表模板表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="rptTempEntity">报表实体</param>
        /// <param name="moduleEntity">模块实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RptTempEntity rptTempEntity, ModuleEntity moduleEntity)
        {
            try
            {
                service.SaveForm(keyValue, rptTempEntity, moduleEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 接口数据
        /// <summary>
        /// 获取CRM数据
        /// </summary>
        /// <param name="EmployeeName">员工姓名</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="DateType">查询类型（0：全部；1：新签；2：新建；3：跟进）</param>
        /// <returns></returns>
        public DataTable GetTableToKPI(string EmployeeName, string StartDate, string EndDate, int DateType)
        {
            return service.GetTableToKPI(EmployeeName, StartDate, EndDate, DateType);
        }
        #endregion

    }
}
