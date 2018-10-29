using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysDocumentMetadata))]//使用SysDocumentMetadata对SysDocument进行数据验证
    public partial class SysDocument 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "人员")]
        public string SysPersonId { get; set; }
        [Display(Name = "人员")]
        public string SysPersonIdOld { get; set; }
        
        [Display(Name = "部门")]
        public string SysDepartmentId { get; set; }
        [Display(Name = "部门")]
        public string SysDepartmentIdOld { get; set; }
        
        #endregion

    }
    public class SysDocumentMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "名称", Order = 2)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Name { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "路径", Order = 3)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Path { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "全路径", Order = 4)]
			[StringLength(500, ErrorMessage = "长度不可超过500")]
			public object FullPath { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "后缀", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Suffix { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "大小", Order = 6)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Size { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 7)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "下载次数", Order = 8)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Download { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 9)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 10)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }


    }
}
 

