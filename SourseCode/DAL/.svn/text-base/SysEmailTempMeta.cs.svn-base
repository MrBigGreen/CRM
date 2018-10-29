using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysEmailTempMetadata))]//使用SysEmailTempMetadata对SysEmailTemp进行数据验证
    public partial class SysEmailTemp 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        #endregion

    }
    public partial class SysEmailTempMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

		 

			[ScaffoldColumn(true)]
			[Display(Name = "邮件内容", Order = 4)]
			public object Content { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "回复邮件地址", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Reply_email { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "是否默认", Order = 6)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object IsDefault { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "邮件类型", Order = 7)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Mail_type { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 8)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "状态", Order = 9)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object State { get; set; }

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
 

