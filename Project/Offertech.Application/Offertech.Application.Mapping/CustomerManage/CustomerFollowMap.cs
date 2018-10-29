using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-04-05 12:48
    /// 描 述：客户跟进进程
    /// </summary>
    public class CustomerFollowMap : EntityTypeConfiguration<CustomerFollowEntity>
    {
        public CustomerFollowMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_CustomerFollow");
            //主键
            this.HasKey(t => t.CustomerFollowId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
