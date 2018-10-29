using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    
    public partial class SysEmailTempMetadata
    {
			 

			[ScaffoldColumn(true)]
			[Display(Name = "模板名称", Order = 2)]
			[StringLength(200, ErrorMessage = "长度不可超过200")]
            [Required(ErrorMessage = "请填写")]
			public string Mail_name { get; set; }

			[ScaffoldColumn(true)]
			[Display(Name = "邮件主题", Order = 3)]
            [StringLength(200, ErrorMessage = "长度不可超过200")]
            [Required(ErrorMessage = "请填写")]
			public string Subject { get; set; }

			 


    }
}
 

