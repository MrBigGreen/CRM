using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysMenuSysRoleSysOperationMetadata))]//使用SysMenuSysRoleSysOperationMetadata对SysMenuSysRoleSysOperation进行数据验证
    public partial class SysMenuSysRoleSysOperation 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "菜单")]
        public string SysMenuIdOld { get; set; }
        
        [Display(Name = "操作")]
        public string SysOperationIdOld { get; set; }
        
        [Display(Name = "角色")]
        public string SysRoleIdOld { get; set; }
        
        #endregion

    }
    public class SysMenuSysRoleSysOperationMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "菜单", Order = 2)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object SysMenuId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "操作", Order = 3)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object SysOperationId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "角色", Order = 4)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object SysRoleId { get; set; }


    }
}
 

