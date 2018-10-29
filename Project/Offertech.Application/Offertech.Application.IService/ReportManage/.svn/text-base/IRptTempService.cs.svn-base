using Offertech.Application.Entity.AuthorizeManage;
using Offertech.Application.Entity.ReportManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace Offertech.Application.IService.ReportManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public interface IRptTempService
    {
        #region 获取数据
        /// <summary>
        /// 报表模板列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<RptTempEntity> GetList(string queryJson);
        /// <summary>
        /// 报表模板实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RptTempEntity GetEntity(string keyValue);
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="reportId">主键值</param>
        /// <returns></returns>
        string GetReportData(string reportId);
        /// <summary>
        /// 客户汇总报表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetCustomerSummaryData(Pagination pagination, string queryJson);
        /// <summary>
        /// 客户汇总报表-按部门
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetCustomerSummaryByDeptData(Pagination pagination, string queryJson);
        /// <summary>
        /// 客户跟进报表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetCustomerFollowData(Pagination pagination, string queryJson);
        /// <summary>
        /// 客户新进报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetCustomerNewData(Pagination pagination, string queryJson);
        /// <summary>
        /// 销售合同报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetContractSaleData(Pagination pagination, string queryJson);
        /// <summary>
        /// 客服合同报表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetContractServiceData(Pagination pagination, string queryJson);
        /// <summary>
        /// 我创建的客户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetMyselfCustomerData(Pagination pagination, string queryJson);
        /// <summary>
        /// 市场部导入客户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
         DataTable GetMarketCustomerData(Pagination pagination, string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除报表模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存报表模板表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="rptTempEntity">报表实体</param>
        /// <param name="moduleEntity">模块实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, RptTempEntity rptTempEntity, ModuleEntity moduleEntity);
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
        DataTable GetTableToKPI(string EmployeeName, string StartDate, string EndDate, int DateType);
        #endregion

    }
}
