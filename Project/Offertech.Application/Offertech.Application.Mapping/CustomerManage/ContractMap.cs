using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 13:51
    /// 描 述：合同管理
    /// </summary>
    public class ContractMap : EntityTypeConfiguration<ContractEntity>
    {
        public ContractMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Contract");
            //主键
            this.HasKey(t => t.ContractId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
