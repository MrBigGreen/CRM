using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysMessageTempMetadata))]//使用SysMessageTempMetadata对SysMessageTemp进行数据验证
    public partial class SysMessageTemp 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        #endregion

    }
    public partial class SysMessageTempMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "内容", Order = 3)]
            [StringLength(400, ErrorMessage = "长度不可超过400")]
            public string Content { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "是否默认", Order = 4)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object IsDefault { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "类型", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object MessageType { get; set; }

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
 

