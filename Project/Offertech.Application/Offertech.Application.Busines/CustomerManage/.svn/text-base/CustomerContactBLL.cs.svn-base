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
    /// 日 期：2016-03-19 14:25
    /// 描 述：客户联系人
    /// </summary>
    public class CustomerContactBLL
    {
        private ICustomerContactService service = new CustomerContactService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CustomerContactEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="customerId">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CustomerContactEntity> GetListByCustomerId(string customerId, string keyword)
        {
            return service.GetListByCustomerId(customerId, keyword);
        }
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回数据表</returns>
        public DataTable GetContactData(string customerId, string keyword)
        {
            return service.GetContactData(customerId, keyword);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CustomerContactEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        public void SaveForm(string keyValue, CustomerContactEntity entity)
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
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void AppSaveForm(string keyValue, CustomerContactEntity entity)
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