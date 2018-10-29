using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    [MetadataType(typeof(TP_HousingFundMetadata))]//使用TP_HousingFundMetadata对TP_HousingFund进行数据验证
    public partial class TP_HousingFund : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        #endregion

    }
    public class TP_HousingFundMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object HousingFundID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "名称", Order = 1)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object HousingFundName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "省份", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ProvinceCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "城市", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CityCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "区域", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object DistrictCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "差异范围", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object DiffBegin { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "差异范围", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object DiffEnd { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人缴纳比例", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiRatio { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "公司缴纳比例", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanyRatio { get; set; }

    }

    public class HousingFundModel
    {

        public string HousingFundID { get; set; }
        public string HousingFundName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public Nullable<decimal> DiffBegin { get; set; }
        public Nullable<decimal> DiffEnd { get; set; }
        public Nullable<decimal> IndiRatio { get; set; }
        public Nullable<decimal> CompanyRatio { get; set; }

        public DateTime LastUpdatedTime { get; set; }

    }
}
