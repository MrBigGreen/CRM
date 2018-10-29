using Offertech.Application.Entity.MessageManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.MessageManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：陈彬彬
    /// 日 期：2015.11.26 18:23
    /// 描 述：即时通信用户群组表
    /// </summary>
    public class IMUserGroupMap : EntityTypeConfiguration<IMUserGroupEntity>
    {
        public IMUserGroupMap()
        {
            #region 表、主键
            //表
            this.ToTable("IM_UserGroup");
            //主键
            this.HasKey(t => t.UserGroupId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
