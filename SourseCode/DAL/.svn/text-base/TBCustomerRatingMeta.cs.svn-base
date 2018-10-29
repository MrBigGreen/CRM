using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{

    [MetadataType(typeof(TB_CustomerRatingMetadata))]//使用TB_CustomerRatingMetadata对TB_CustomerRating进行数据验证
    public partial class TB_CustomerRating : BaseEntity
    {


        #region 自定义属性，即由数据实体扩展的实体

        #endregion

    }
    public class TB_CustomerRatingMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object CustomerRatingID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户编号", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerID { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "企业性质", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CompanyNatureCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "注册资金", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object RegisterCapital { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "销售收入", Order = 5)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SalesRevenue { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "合同账期", Order = 6)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ContractPeriod { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "逾期垫款", Order = 7)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object OverdueAdvances { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "信用等级", Order = 8)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object RatingScore { get; set; }


    }

    public class CustomerRatingEntity
    {

        public string CustomerRatingID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CompanyNatureCode { get; set; }
        public string CompanyNatureText { get; set; }
        public string RegisterCapital { get; set; }
        public string RegisterCapitalText { get; set; }
        public string SalesRevenue { get; set; }
        public string SalesRevenueText { get; set; }
        public string ContractPeriod { get; set; }
        public string ContractPeriodText { get; set; }
        public string OverdueAdvances { get; set; }
        public string OverdueAdvancesText { get; set; }
        public string RatingScore { get; set; }
        public string RatingScoreText { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

}
