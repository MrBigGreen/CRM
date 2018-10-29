using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    [MetadataType(typeof(TB_SalesPerformanceMetadata))]//使用TB_SalesPerformanceMetadata对TB_SalesPerformance进行数据验证
    public partial class TB_SalesPerformance : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        public string SysPersonName { get; set; }
        #endregion

    }
    public class TB_SalesPerformanceMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object SalesPerformanceID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "销售", Order = 1)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SysPersonID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "月份", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(7, ErrorMessage = "长度不可超过7")]
        public object TheMonth { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "周", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object TheWeek { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "区间", Order = 3)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object IntervalName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "类别", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object Category { get; set; }



        [ScaffoldColumn(true)]
        [Display(Name = "客户拜访数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object FollowUpQty { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "合同数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object ContractQty { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "开票金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public object BillingAmount { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "预计金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public object BudgetAmount { get; set; }
        

        [ScaffoldColumn(true)]
        [Display(Name = "重点客户数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object KeyCustomerQty { get; set; }
        

        [ScaffoldColumn(true)]
        [Display(Name = "平均拜访次数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public object FollowUpAvg { get; set; }
    }

    public class SalesPerfModel
    {


        [ScaffoldColumn(true)]
        [Display(Name = "月份", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(10, ErrorMessage = "长度不可超过10")]
        public string TheMonth { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "周", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int TheWeek { get; set; }



        [ScaffoldColumn(true)]
        [Display(Name = "客户拜访数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int FollowUpQty1 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户拜访数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int FollowUpQty2 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "合同数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int ContractQty1 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "合同数量", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int ContractQty2 { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "开票金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public decimal BillingAmount1 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "预算金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public decimal BudgetAmount1 { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "开票金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public decimal BillingAmount2 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "预算金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public decimal BudgetAmount2 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "重点客户数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int KeyCustomerQty1 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "重点客户数", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int KeyCustomerQty2 { get; set; }



    }



    public class SalesPerfReportModel
    {
        public int Category { get; set; }


        public int TheWeek { get; set; }

        public int FollowUpQty { get; set; }

        public int ContractQty { get; set; }

        public int BillingAmount { get; set; }

        public int BudgetAmount { get; set; }

        public int KeyCustomerQty { get; set; }
    }
}
