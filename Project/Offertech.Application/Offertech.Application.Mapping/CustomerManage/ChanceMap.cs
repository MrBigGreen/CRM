using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2016-03-12 10:50
    /// 描 述：商机信息
    /// </summary>
    public class ChanceMap : EntityTypeConfiguration<ChanceEntity>
    {
        public ChanceMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Chance");
            //主键
            this.HasKey(t => t.ChanceId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
