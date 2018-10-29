using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysMenuMetadata))]//使用SysMenuMetadata对SysMenu进行数据验证
    public partial class SysMenu 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "父模块")]
        public string ParentIdOld { get; set; }
        
        [Display(Name = "操作")]
        public string SysOperationId { get; set; }
        [Display(Name = "操作")]
        public string SysOperationIdOld { get; set; }
        
        #endregion

    }
    public class SysMenuMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "名称", Order = 2)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Name { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "父模块", Order = 3)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object ParentId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "网址", Order = 4)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Url { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "图标", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Iconic { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "排序", Order = 6)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Sort { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 7)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "展开或折叠", Order = 8)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object State { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 9)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 10)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "编辑时间", Order = 11)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? UpdateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "编辑人", Order = 12)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object UpdatePerson { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "是否是子节点", Order = 13)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object IsLeaf { get; set; }


    }
}
 

