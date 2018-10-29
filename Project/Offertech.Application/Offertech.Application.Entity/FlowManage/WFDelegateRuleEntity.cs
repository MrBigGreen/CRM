using Offertech.Application.Code;
using System;

namespace Offertech.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流委托规则表
    /// </summary>
    public class WFDelegateRuleEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 被委托人Id
        /// </summary>
        public string ToUserId { get; set; }
        /// <summary>
        /// 被委托人
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 委托开始时间
        /// </summary>		
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 委托结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 委托人Id
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 委托人
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
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
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}
