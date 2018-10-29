using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysAnnouncementMetadata))]//使用SysAnnouncementMetadata对SysAnnouncement进行数据验证
    public partial class SysAnnouncement 
    {
      
        #region 自定义属性，即由数据实体扩展的实体
        
        #endregion

    }
    public class SysAnnouncementMetadata
    {
			[ScaffoldColumn(false)]
			[Display(Name = "主键", Order = 1)]
			public object Id { get; set; }

            [ScaffoldColumn(true)]
            [Display(Name = "标题", Order = 2)]
            [Required(ErrorMessage = "请填写")]
            [StringLength(100, ErrorMessage = "长度不可超过100")]
            public string Title { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "内容", Order = 2)]
			[StringLength(4000, ErrorMessage = "长度不可超过4000")]
			public object Message { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "状态", Order = 3)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object State { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建时间", Order = 4)]
			[DataType(System.ComponentModel.DataAnnotations.DataType.DateTime,ErrorMessage="时间格式不正确")]
			public DateTime? CreateTime { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "创建人", Order = 5)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
			public object CreatePerson { get; set; }


    }
}
 

