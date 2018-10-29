using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysLogMetadata))]//使用SysLogMetadata对SysLog进行数据验证
    public partial class SysLog 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        #endregion

    }
    public class SysLogMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "人员", Order = 2)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object PersonId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "内容", Order = 3)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Message { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "结果", Order = 4)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Result { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "模块", Order = 5)]
			[StringLength(36, ErrorMessage = "长度不可超过36")]
			public object MenuId { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "IP", Order = 6)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object Ip { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "备注", Order = 7)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Remark { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "状态", Order = 8)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object State { get; set; }

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
 

