using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-19 14:25
    /// 描 述：客户联系人
    /// </summary>
    public class CustomerContactMap : EntityTypeConfiguration<CustomerContactEntity>
    {
        public CustomerContactMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_CustomerContact");
            //主键
            this.HasKey(t => t.CustomerContactId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}