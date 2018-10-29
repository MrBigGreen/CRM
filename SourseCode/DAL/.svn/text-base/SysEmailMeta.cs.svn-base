using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysEmailMetadata))]//使用SysEmailMetadata对SysEmail进行数据验证
    public partial class SysEmail 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "模板")]
        public string SysMailIdOld { get; set; }
        
        #endregion

    }
    public class SysEmailMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "模板", Order = 2)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object SysMailId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "主题", Order = 3)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Subject { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "内容", Order = 4)]
			public object Content { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "回复邮箱地址", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Reply_email { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "邮件类型", Order = 6)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Mail_type { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 7)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "状态", Order = 8)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object State { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "阅读时间", Order = 9)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]

			public DateTime? ReadTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 10)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 11)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }


    }
}
 

