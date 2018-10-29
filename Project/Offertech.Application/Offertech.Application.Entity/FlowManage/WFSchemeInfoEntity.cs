using Offertech.Application.Code;
using System;

namespace Offertech.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流模板信息表
    /// </summary>
    public class WFSchemeInfoEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 流程主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 流程编码
        /// </summary>		
        public string SchemeCode { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>		
        public string SchemeName { get; set; }
        /// <summary>
        /// 流程类型
        /// </summary>		
        public string SchemeType { get; set; }
        /// <summary>
        /// 流程内容版本
        /// </summary>
        public string SchemeVersion { get; set; }
        /// <summary>
        /// 流程模板使用者
        /// </summary>
        public string SchemeCanUser { get; set; }
        /// <summary>
        /// 表单类型（0自定义，1系统)
        /// </summary>
        public int? FrmType { get; set; }
        /// <summary>
        /// 权限类型(0所有人,1指定成员)
        /// </summary>
        public int? AuthorizeType { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
            this.DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        #endregion
    }
}

