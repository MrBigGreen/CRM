using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:49
    /// 描 述：客户合作服务类型
    /// </summary>
    public class CooperativeServiceMap : EntityTypeConfiguration<CooperativeServiceEntity>
    {
        public CooperativeServiceMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_CooperativeService");
            //主键
            this.HasKey(t => t.CooperativeServiceId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
