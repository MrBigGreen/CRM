using System;
using Offertech.Application.Code;
using System.Collections.Generic;

namespace Offertech.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-21 16:10
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 商机跟进主键
        /// </summary>
        /// <returns></returns>
        public string TrailId { get; set; }
        /// <summary>
        /// 对象分类（1-商机、2-客户）
        /// </summary>
        /// <returns></returns>
        public int? ObjectSort { get; set; }
        /// <summary>
        /// 跟进方式（1电话 2 当面 3邮件）
        /// </summary>
        /// <returns></returns>
        public int? FollowUpMode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        /// <returns></returns>
        public string PostId { get; set; }
        /// <summary>
        /// 销售阶段
        /// </summary>
        /// <returns></returns>
        public string SaleStageName { get; set; }
        /// <summary>
        /// 销售阶段
        /// </summary>
        /// <returns></returns>
        public string SaleStageId { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        /// <returns></returns>
        public string ObjectId { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        /// <returns></returns>
        public string ObjectName { get; set; }
        /// <summary>
        /// 跟进类型（1-计划、2-完成）
        /// </summary>
        /// <returns></returns>
        public int? TrailType { get; set; }
        /// <summary>
        /// 跟进内容
        /// </summary>
        /// <returns></returns>
        public string TrackContent { get; set; }
        /// <summary>
        /// 跟进图片
        /// </summary>
        public string FilesPath { get; set; }
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
            this.TrailId = Guid.NewGuid().ToString();
            this.EnabledMark = 1;
            this.DeleteMark = 0;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
            this.ModifyDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.TrailId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        #endregion
    }

    public class TrailRecordModel
    {
        #region 实体成员
        /// <summary>
        /// 商机跟进主键
        /// </summary>
        /// <returns></returns>
        public string TrailId { get; set; }
        /// <summary>
        /// 对象分类（1-商机、2-客户）
        /// </summary>
        /// <returns></returns>
        public int? ObjectSort { get; set; }
        /// <summary>
        /// 跟进方式（1电话 2 当面 3邮件）
        /// </summary>
        /// <returns></returns>
        public int? FollowUpMode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        /// <returns></returns>
        public string PostId { get; set; }
        /// <summary>
        /// 销售阶段
        /// </summary>
        /// <returns></returns>
        public string SaleStageName { get; set; }
        /// <summary>
        /// 销售阶段
        /// </summary>
        /// <returns></returns>
        public string SaleStageId { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>
        /// <returns></returns>
        public string ObjectId { get; set; }
        /// <summary>
        /// 对象名称
        /// </summary>
        /// <returns></returns>
        public string ObjectName { get; set; }
        /// <summary>
        /// 跟进类型（1-计划、2-完成）
        /// </summary>
        /// <returns></returns>
        public int? TrailType { get; set; }
        /// <summary>
        /// 跟进内容
        /// </summary>
        /// <returns></returns>
        public string TrackContent { get; set; }
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
        /// 跟进照片
        /// </summary>
        public string FilesPath { get; set; }

        /// <summary>
        /// 跟进照片
        /// </summary>
        public List<string> FilePathList { get; set; }
        #endregion

    }
}