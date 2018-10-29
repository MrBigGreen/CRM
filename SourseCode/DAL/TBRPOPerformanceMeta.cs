using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{

    [MetadataType(typeof(TB_RPOPerformanceMetadata))]//使用TB_RPOPerformanceMetadata对TB_RPOPerformance进行数据验证
    public partial class TB_RPOPerformance : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        public string SysPersonName { get; set; }
        #endregion

    }
    public class TB_RPOPerformanceMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object RPOPerformanceID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "销售", Order = 1)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SysPersonID { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "统计日期", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public object CurrentDate { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "下载简历数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object DownLoadQty { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "联系候选人数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object ContactPersonQty { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "预约面试数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object AppInterviewQty { get; set; }
        
        [ScaffoldColumn(true)]
        [Display(Name = "实际面试数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object InterviewQty { get; set; }
        

        [ScaffoldColumn(true)]
        [Display(Name = "录用数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object OfferQty { get; set; }
        

        [ScaffoldColumn(true)]
        [Display(Name = "上岗数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object OnBoardQty { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "过试用期数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(-1000, 1000, ErrorMessage = "数值超出范围")]
        public object OverProbationQty { get; set; }
    }


    public class RPOPerformanceEntity
    {
        public string SysPersonName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerProjectID { get; set; }
        public string ProjectName { get; set; }

        public string PersonNames { get; set; }
        public int DownLoadQty { get; set; }
        public int ContactPersonQty { get; set; }
        public int AppInterviewQty { get; set; }
        public int InterviewQty { get; set; }
        public int OfferQty { get; set; }
        public int OnBoardQty { get; set; }
        public int OverProbationQty { get; set; }
    }

}
