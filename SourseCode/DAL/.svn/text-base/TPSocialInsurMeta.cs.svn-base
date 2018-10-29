using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    [MetadataType(typeof(TP_SocialInsurMetadata))]//使用TP_SocialInsurMetadata对TP_SocialInsur进行数据验证
    public partial class TP_SocialInsur : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        #endregion

    }
    public class TP_SocialInsurMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SocialInsurID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "社保名称", Order = 1)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SocialInsurName { get; set; }

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
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object DistrictCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "差异范围", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object DiffBegin { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "差异范围", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object DiffEnd { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人养老", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiPension { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "公司养老", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanyPension { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人工伤", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiInjury { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司工伤", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanyInjury { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人医疗", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiMedical { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司医疗", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanyMedical { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人生育", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiBirth { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司生育", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanyBirth { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人补充", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiSupply { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司补充", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanySupply { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "个人大病统筹", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object IndiDBTC { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司大病统筹", Order = 3)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object CompanyDBTC { get; set; }
    }



    public class SocialInsurModel
    {

        public string SocialInsurID { get; set; }
        public string SocialInsurName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public Nullable<decimal> DiffBegin { get; set; }
        public Nullable<decimal> DiffEnd { get; set; }
        public Nullable<decimal> IndiPension { get; set; }
        public Nullable<decimal> CompanyPension { get; set; }
        public Nullable<decimal> IndiInjury { get; set; }
        public Nullable<decimal> CompanyInjury { get; set; }
        public Nullable<decimal> IndiMedical { get; set; }
        public Nullable<decimal> CompanyMedical { get; set; }
        public Nullable<decimal> IndiBirth { get; set; }
        public Nullable<decimal> CompanyBirth { get; set; }
        public Nullable<decimal> IndiSupply { get; set; }
        public Nullable<decimal> CompanySupply { get; set; }
        public Nullable<decimal> IndiDBTC { get; set; }
        public Nullable<decimal> CompanyDBTC { get; set; }

        public DateTime LastUpdatedTime { get; set; }

    }
}
