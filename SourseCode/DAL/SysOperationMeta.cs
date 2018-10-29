using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysOperationMetadata))]//使用SysOperationMetadata对SysOperation进行数据验证
    public partial class SysOperation 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "菜单")]
        public string SysMenuId { get; set; }
        [Display(Name = "菜单")]
        public string SysMenuIdOld { get; set; }
        
        #endregion

    }
    public class SysOperationMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "名称", Order = 2)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Name { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "方法", Order = 3)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Function { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "图标", Order = 4)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Iconic { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "排序", Order = 5)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Sort { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 6)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "状态", Order = 7)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object State { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 8)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 9)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }


    }
}
 

