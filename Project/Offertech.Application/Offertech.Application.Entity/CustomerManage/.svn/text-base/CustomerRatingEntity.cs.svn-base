using System;
using Offertech.Application.Code;

namespace Offertech.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:55
    /// 描 述：客户等级评价表
    /// </summary>
    public class CustomerRatingEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 归属变更主键
        /// </summary>
        /// <returns></returns>
        public string CustomerRatingId { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 企业性质
        /// </summary>
        /// <returns></returns>
        public string NatureCode { get; set; }
        /// <summary>
        /// 注册资金
        /// </summary>
        /// <returns></returns>
        public string RegisterCapital { get; set; }
        /// <summary>
        /// 销售收入
        /// </summary>
        /// <returns></returns>
        public string SalesRevenue { get; set; }
        /// <summary>
        /// 合同账期
        /// </summary>
        /// <returns></returns>
        public string ContractPeriod { get; set; }
        /// <summary>
        /// 逾期垫款
        /// </summary>
        /// <returns></returns>
        public string OverdueAdvances { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        /// <returns></returns>
        public string RatingScore { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CustomerRatingId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CustomerRatingId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        #endregion
    }
}