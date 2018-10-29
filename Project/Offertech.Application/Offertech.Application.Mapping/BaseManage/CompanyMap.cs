using Offertech.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-04-19 15:42
    /// 描 述：主体机构单位表
    /// </summary>
    public class CompanyMap : EntityTypeConfiguration<CompanyEntity>
    {
        public CompanyMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Company");
            //主键
            this.HasKey(t => t.CompanyId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
