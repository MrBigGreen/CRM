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
    /// 日 期：2016-03-19 14:25
    /// 描 述：客户联系人
    /// </summary>
    public interface ICustomerContactService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<CustomerContactEntity> GetList(string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="customerId">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<CustomerContactEntity> GetListByCustomerId(string customerId, string keyword);
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回数据表</returns>
        DataTable GetContactData(string customerId, string keyword);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        CustomerContactEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, CustomerContactEntity entity);
        /// <summary>
        /// 移动端保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void AppSaveForm(string keyValue, CustomerContactEntity entity);
        #endregion
    }
}