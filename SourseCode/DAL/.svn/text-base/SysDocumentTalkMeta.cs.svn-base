using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysDocumentTalkMetadata))]//使用SysDocumentTalkMetadata对SysDocumentTalk进行数据验证
    public partial class SysDocumentTalk 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        [Display(Name = "文档")]
        public string SysDocumentIdOld { get; set; }
        
        #endregion

    }
    public class SysDocumentTalkMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "内容", Order = 2)]
			[StringLength(500, ErrorMessage = "长度不可超过500")]
			public object Content { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "文档", Order = 3)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object SysDocumentId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "差", Order = 4)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Bad { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "赞", Order = 5)]
			[Range(0,2147483646, ErrorMessage="数值超出范围")]
			public int? Good { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 6)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 7)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }


    }
}
 

