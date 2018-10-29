using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:53
    /// 描 述：客户变更记录
    /// </summary>
    public class OwnershipChangeMap : EntityTypeConfiguration<OwnershipChangeEntity>
    {
        public OwnershipChangeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_OwnershipChange");
            //主键
            this.HasKey(t => t.OwnershipChangeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
