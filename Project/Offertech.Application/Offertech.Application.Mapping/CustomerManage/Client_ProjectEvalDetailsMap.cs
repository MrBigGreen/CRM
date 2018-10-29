using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创 建：超级管理员
    /// 日 期：2017-03-09 14:45
    /// 描 述：项目表
    /// </summary>
    public class Client_ProjectEvalDetailsMap : EntityTypeConfiguration<Client_ProjectEvalDetailsEntity>
    {
        public Client_ProjectEvalDetailsMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_ProjectEvalDetails");
            //主键
            this.HasKey(t => t.ProjectEvalDetailsId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
