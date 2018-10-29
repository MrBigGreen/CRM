using System;
using System.ComponentModel.DataAnnotations.Schema;
using Offertech.Application.Code;

namespace Offertech.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 14:45
    /// 描 述：项目表
    /// </summary>
    public class ProjectEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 项目主键
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTID")]
        public string ProjectId { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTCODE")]
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTNAME")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        /// <returns></returns>
        [Column("CUSTOMERID")]
        public string CustomerId { get; set; }
        /// <summary>
        /// 项目说明
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTDESC")]
        public string ProjectDesc { get; set; }
        /// <summary>
        /// 项目要求
        /// </summary>
        /// <returns></returns>
        [Column("REQUIREMENTS")]
        public string Requirements { get; set; }
        /// <summary>
        /// 项目预算
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTBUDGET")]
        public string ProjectBudget { get; set; }
        /// <summary>
        /// 招聘人数
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTPEOPLEQTY")]
        public int? ProjectPeopleQty { get; set; }
        /// <summary>
        /// 福利待遇
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTBENEFITS")]
        public string ProjectBenefits { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTADDRESS")]
        public string ProjectAddress { get; set; }
        /// <summary>
        /// 评估结果
        /// </summary>
        /// <returns></returns>
        [Column("EVALUATIONRESULT")]
        public string EvaluationResult { get; set; }
        /// <summary>
        /// 评估说明
        /// </summary>
        /// <returns></returns>
        [Column("EVALUATIONDESC")]
        public string EvaluationDesc { get; set; }
        /// <summary>
        /// 评估人
        /// </summary>
        /// <returns></returns>
        [Column("EVALUATIONPERSON")]
        public string EvaluationPerson { get; set; }
        /// <summary>
        /// 评估时间
        /// </summary>
        /// <returns></returns>
        [Column("EVALUATIONTIME")]
        public DateTime? EvaluationTime { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("SORTCODE")]
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ProjectId = Guid.NewGuid().ToString();
            this.EnabledMark = 1;
            this.DeleteMark = 0;
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
            this.ProjectId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        #endregion
    }
}