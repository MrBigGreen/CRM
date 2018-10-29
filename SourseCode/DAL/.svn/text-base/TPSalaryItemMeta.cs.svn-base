using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{ 

    [MetadataType(typeof(TP_SalaryItemMetadata))]//使用TP_SalaryItemMetadata对TP_SalaryItem进行数据验证
    public partial class TP_SalaryItem : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        
        #endregion

    }
    public class TP_SalaryItemMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object SalaryItemID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "字段", Order = 1)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ItemCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "列名", Order = 2)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ItemName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "数据类型", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object DataType { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所属类别", Order = 4)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object ItemClass { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "排序", Order = 6)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int? Sort { get; set; }
    }

    public class SalaryItemEntity {

        public string SalaryItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> DataType { get; set; }
        public Nullable<int> ItemClass { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<bool> IsDel { get; set; }
    
    }
}
