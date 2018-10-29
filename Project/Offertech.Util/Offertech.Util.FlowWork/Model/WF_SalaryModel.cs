using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Util.FlowWork
{
    public class WF_SalaryModel
    {
        #region 实体成员
        /// <summary>
        /// 薪资审核主键
        /// </summary>
        /// <returns></returns>
        public string WFSalaryId { get; set; }
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
        /// 项目编号
        /// </summary>
        /// <returns></returns>
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <returns></returns>
        public string ProjectName { get; set; }
        /// <summary>
        /// 是否垫款
        /// </summary>
        /// <returns></returns>
        public bool? IsAdvances { get; set; }
        /// <summary>
        /// 是否到账
        /// </summary>
        /// <returns></returns>
        public bool? IsArrival { get; set; }
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
        /// 月份
        /// </summary>
        /// <returns></returns>
        public int? SalaryMonth { get; set; }
        /// <summary>
        /// 审核状态（0:未审核；1：审核通过；2：审核未通过）
        /// </summary>
        /// <returns></returns>
        public int? WFState { get; set; }
        /// <summary>
        /// 更新标记(1：有重复更新   2：有重复增加)
        /// </summary>
        /// <returns></returns>
        public int? UpdateFlag { get; set; }
        /// <summary>
        /// AuditDesc
        /// </summary>
        /// <returns></returns>
        public string AuditDesc { get; set; }
        #endregion
    }
}
