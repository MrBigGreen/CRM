using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-04-06 17:24
    /// 描 述：应收账款
    /// </summary>
    public class ReceivableMap : EntityTypeConfiguration<ReceivableEntity>
    {
        public ReceivableMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Receivable");
            //主键
            this.HasKey(t => t.ReceivableId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}