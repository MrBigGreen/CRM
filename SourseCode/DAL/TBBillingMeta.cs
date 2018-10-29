using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    [MetadataType(typeof(TB_BillingMetadata))]//使用TB_BillingMetadata对TB_Billing进行数据验证
    public partial class TB_Billing : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        public string SysPersonName { get; set; }
        #endregion

    }
    public class TB_BillingMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object BillingID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "销售", Order = 2)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SysPersonID { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "月份", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(10, ErrorMessage = "长度不可超过10")]
        public object TheMonth { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "周", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 5, ErrorMessage = "数值超出范围")]
        public object TheWeek { get; set; }


    }


}
