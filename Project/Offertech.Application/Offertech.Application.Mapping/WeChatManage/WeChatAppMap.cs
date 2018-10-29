using Offertech.Application.Entity.WeChatManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用
    /// </summary>
    public class WeChatAppMap : EntityTypeConfiguration<WeChatAppEntity>
    {
        public WeChatAppMap()
        {
            #region 表、主键
            //表
            this.ToTable("WeChat_App");
            //主键
            this.HasKey(t => t.AppId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
