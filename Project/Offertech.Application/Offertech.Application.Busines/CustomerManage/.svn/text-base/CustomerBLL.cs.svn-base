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
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class CustomerBLL
    {
        private ICustomerService service = new CustomerService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "CustomerCache";

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetMyList()
        {
            return service.GetMyList();
        }

        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMyData(string queryJson)
        {
            return service.GetMyData(queryJson);
        }
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMyPageData(Pagination pagination, string queryJson)
        {
            return service.GetMyPageData(pagination, queryJson);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetMyPageList(Pagination pagination, string queryJson)
        {
            return service.GetMyPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取当前用户和下级用户的列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetChildList(Pagination pagination, string queryJson)
        {
            return service.GetChildList(pagination, queryJson);
        }
        /// <summary>
        /// 获取所有数据的列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageData(Pagination pagination, string queryJson)
        {
            return service.GetPageData(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 公共客户池列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetPublicList(Pagination pagination, string queryJson)
        {
            return service.GetPublicList(pagination, queryJson);
        }
        /// <summary>
        /// 关键字搜索客户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CustomerEntity> GetSearchList(string keyword)
        {
            return service.GetSearchList(keyword);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CustomerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="fullName">主键值</param>
        /// <returns></returns>
        public CustomerEntity GetEntityByName(string fullName)
        {
            return service.GetEntityByName(fullName);
        }

        #region 移动端
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetAppMyList(string userId)
        {
            return service.GetAppMyList(userId);
        }
        /// <summary>
        /// 获取当前用户的客户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAppMyPageData(string userId, Pagination pagination, string queryJson)
        {
            return service.GetAppMyPageData(userId, pagination, queryJson);
        }
        #endregion
        #endregion

        #region 验证数据
        /// <summary>
        /// 客户名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
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
        public void SaveForm(string keyValue, CustomerEntity entity)
        {
            if (entity.FullName.Length <= 4)
            {
                throw new Exception("客户名称不完整");
            }
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
            if (!reg.IsMatch(entity.FullName))
            {
                throw new Exception("客户名称只能包含汉字和括弧");
            }
            try
            {
                service.SaveForm(keyValue, entity);
                //创建搜索索引
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    CustomerLuceneNet.GetInstance().AddCustomer(entity.CustomerId);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity">实体</param>
        /// <param name="moduleId">模块</param>
        public void SaveForm(string keyValue, CustomerEntity entity, string moduleId)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                if (entity.FullName.Length <= 4)
                {
                    throw new Exception("客户名称不完整");
                }
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
                if (!reg.IsMatch(entity.FullName))
                {
                    throw new Exception("客户名称只能包含汉字和括弧");
                }
                if (!ExistFullName(entity.FullName, keyValue))
                {
                    throw new Exception("客户信息已存在");
                }

            }

            try
            {
                service.SaveForm(keyValue, entity, moduleId);
                //创建搜索索引
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    CustomerLuceneNet.GetInstance().AddCustomer(entity.CustomerId);
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity">实体</param>
        /// <param name="moduleId">模块</param>
        public void SaveImportForm(string keyValue, CustomerEntity entity, string moduleId)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                if (entity.FullName.Length <= 3)
                {
                    throw new Exception("客户名称不完整");
                }
                //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
                //if (!reg.IsMatch(entity.FullName))
                //{
                //    throw new Exception("客户名称只能包含汉字和括弧");
                //}
                if (!ExistFullName(entity.FullName, keyValue))
                {
                    throw new Exception("客户信息已存在");
                }

            }

            try
            {
                service.SaveForm(keyValue, entity, moduleId);
                //创建搜索索引
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    CustomerLuceneNet.GetInstance().AddCustomer(entity.CustomerId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 修改客户信用等级
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="RatingScore">客户信用等级</param>
        public void UpdateRatingScore(string keyValue, string RatingScore)
        {
            try
            {
                service.UpdateRatingScore(keyValue, RatingScore);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 修改客户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：（-2审核不通过， -1带审核， 0 被释放的客户， 1无需 审核，2审核通过）</param>
        public void UpdateState(string keyValue, int State)
        {
            try
            {
                service.UpdateState(keyValue, State);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 释放客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void GetRelease(string keyValue)
        {
            try
            {
                service.GetRelease(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 提取客户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetExtract(string keyValue)
        {
            try
            {
                service.GetExtract(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 修改客户名称
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="FullName">客户名称</param>
        public void UpdateFullName(string keyValue, string FullName)
        {
            try
            {
                service.UpdateFullName(keyValue, FullName);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #region 移动端
        /// <summary>
        /// 移动端新增客户
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="moduleId">模块</param>
        public void AppInsert(CustomerEntity entity, string moduleId)
        {
            if (entity.FullName.Length <= 4)
            {
                throw new Exception("客户名称不完整");
            }
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
            if (!reg.IsMatch(entity.FullName))
            {
                throw new Exception("客户名称只能包含汉字和括弧");
            }
            try
            {
                service.AppInsert(entity, moduleId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 移动端更新客户
        /// </summary>
        /// <param name="entity"></param>
        public void AppUpdate(CustomerEntity entity)
        {
            if (entity.FullName.Length <= 4)
            {
                throw new Exception("客户名称不完整");
            }
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(（]?[\u4e00-\u9fa5]+[)）]?[\u4e00-\u9fa5]*)$");
            if (!reg.IsMatch(entity.FullName))
            {
                throw new Exception("客户名称只能包含汉字和括弧");
            }
            try
            {
                service.AppUpdate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion 
        #endregion

        #region 计算企业信用等级



        /// <summary>
        /// 根据分数获取等级
        /// </summary>
        /// <param name="ratingScore"></param>
        /// <returns></returns>
        public string GetCalcRatingBefore(int ratingScore)
        {

            if (ratingScore >= 90)
            {
                return "A";
            }
            else if (ratingScore >= 80)
            {
                return "B";
            }
            else if (ratingScore >= 70)
            {
                return "C";
            }
            else if (ratingScore >= 60)
            {
                return "D";
            }
            else
            {
                return "E";
            }
        }
        #endregion
    }

}
