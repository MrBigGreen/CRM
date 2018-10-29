using Offertech.Application.Code;
using System;

namespace Offertech.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流模板内容表
    /// </summary>
    public class WFSchemeContentEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键Id
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 流程内容ID
        /// </summary>		
        public string WFSchemeInfoId { get; set; }
        /// <summary>
        /// 流程内容版本
        /// </summary>
        public string SchemeVersion { get; set; }
        /// <summary>
        /// 流程内容
        /// </summary>
        public string SchemeContent { get; set; }
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
