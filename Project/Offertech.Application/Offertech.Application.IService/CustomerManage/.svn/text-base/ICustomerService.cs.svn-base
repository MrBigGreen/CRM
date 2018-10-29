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
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public interface ICustomerService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetList();
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetMyList();
        /// <summary>
        /// 获取客户
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetList(string userId);
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetMyData(string queryJson);
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetMyPageData(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<CustomerEntity> GetMyPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取当前用户和下级用户的列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetChildList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取所有数据的列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageData(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 公共客户池列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<CustomerEntity> GetPublicList(Pagination pagination, string queryJson);
        /// <summary>
        /// 关键字搜索客户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<CustomerEntity> GetSearchList(string keyword);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        CustomerEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="fullName">主键值</param>
        /// <returns></returns>
        CustomerEntity GetEntityByName(string fullName);

        #region 移动端

        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetAppMyList(string userId);


        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetAppMyPageData(string userId, Pagination pagination, string queryJson);
        #endregion
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);


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
        void SaveForm(string keyValue, CustomerEntity entity);

        /// <summary>
        /// 保存表单(新增/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        void SaveForm(string keyValue, CustomerEntity entity, string moduleId);
        /// <summary>
        /// 修改客户信用等级
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="RatingScore">客户信用等级</param>
        void UpdateRatingScore(string keyValue, string RatingScore);
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：（-2审核不通过， -1带审核， 0 被释放的客户， 1无需 审核，2审核通过）</param>
        void UpdateState(string keyValue, int State);
        /// <summary>
        /// 释放客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void GetRelease(string keyValue);
        /// <summary>
        /// 提取客户
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetExtract(string keyValue);
        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="FullName">客户名称</param>
        void UpdateFullName(string keyValue, string FullName);
        #region 移动端
        /// <summary>
        /// 移动端新增客户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        void AppInsert(CustomerEntity entity, string moduleId);
        /// <summary>
        /// 移动端更新客户
        /// </summary>
        /// <param name="entity"></param>
        void AppUpdate(CustomerEntity entity);
        #endregion 
        #endregion
    }
}
