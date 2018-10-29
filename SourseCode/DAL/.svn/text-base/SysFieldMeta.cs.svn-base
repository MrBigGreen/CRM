using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysFieldMetadata))]//使用SysFieldMetadata对SysField进行数据验证
    public partial class SysField 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "父节点")]
        public string ParentIdOld { get; set; }
        
        #endregion

    }
    public class SysFieldMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "名称", Order = 2)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object MyTexts { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "数值", Order = 2)]
            [StringLength(50, ErrorMessage = "长度不可超过50")]
            public object DataValue { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "父节点", Order = 3)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object ParentId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "表名", Order = 4)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object MyTables { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "字段", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object MyColums { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "排序", Order = 6)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Sort { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 7)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 8)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 9)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "编辑时间", Order = 10)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? UpdateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "编辑人", Order = 11)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object UpdatePerson { get; set; }


    }
}
 

