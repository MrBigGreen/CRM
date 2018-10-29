using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Application.Entity.CustomerManage.ViewModel
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-29 13:51
    /// 描 述：合同管理
    /// </summary>
    public class ContractModel
    {
        /// <summary>
        /// 合同主键
        /// </summary>
        /// <returns></returns>
        public string ContractId { get; set; }
        /// <summary>
        /// 合同编码
        /// </summary>
        /// <returns></returns>
        public string ContractCode { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 合同类型（11-欧孚销售合同 、 12-博尔捷销售合同 、21-欧孚客服合同、22-博尔捷客服合同）
        /// </summary>
        /// <returns></returns>
        public int? ContractType { get; set; }
        /// <summary>
        /// 签约主体Id
        /// </summary>
        /// <returns></returns>
        public string SignSubjectId { get; set; }
        /// <summary>
        /// 签约主体
        /// </summary>
        /// <returns></returns>
        public string SignSubject { get; set; }
        /// <summary>
        /// 签约类型(1-新签 、 2续签)
        /// </summary>
        /// <returns></returns>
        public int? SignType { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        /// <returns></returns>
        public string ServiceType { get; set; }
        /// <summary>
        /// 服务类型Id
        /// </summary>
        /// <returns></returns>
        public string ServiceTypeId { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        /// <returns></returns>
        public string ContractMoney { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        /// <returns></returns>
        public string PaymentMethod { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        /// <returns></returns>
        public DateTime? EffectiveDate { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        /// <returns></returns>
        public DateTime? Deadline { get; set; }
        /// <summary>
        /// 项目负责人
        /// </summary>
        /// <returns></returns>
        public string ProjectLeader { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        /// <returns></returns>
        public long? PhoneNumber { get; set; }
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
        /// <summary>
        /// 客服创建合同验证码
        /// </summary>
        public string VerificationCode{get;set;}
    }
}
