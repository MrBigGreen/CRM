using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    [Serializable]
    public partial class BaseEntity 
    {
        [ScaffoldColumn(true)]
        [Display(Name = "版本", Order = 10)]
        public int VersionNo { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "是否删除", Order = 11)]
        public bool IsDelete { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建时间", Order = 12)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? CreatedTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建人", Order = 13)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "最后修改时间", Order = 14)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? LastUpdatedTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "最后修改者", Order = 15)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string LastUpdatedBy { get; set; }


    }
}
