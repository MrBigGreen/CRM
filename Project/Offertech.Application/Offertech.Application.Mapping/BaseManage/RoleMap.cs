﻿using Offertech.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 苏州欧孚科技
    /// 创建人：chand
    /// 日 期：2015.11.4 14:31
    /// 描 述：角色管理
    /// </summary>
    public class RoleMap : EntityTypeConfiguration<RoleEntity>
    {
        public RoleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Role");
            //主键
            this.HasKey(t => t.RoleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
