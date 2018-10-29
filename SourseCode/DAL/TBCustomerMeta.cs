using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：chand<br/>
    /// 创建时间：2015-06-10<br/>
    /// 描述说明：客户信息实体
    /// </summary>
    [MetadataType(typeof(TB_CustomerMetadata))]//使用TB_CustomerMetadata对TB_Customer进行数据验证
    public partial class TB_Customer : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        /// <summary>
        /// 客户查重验证码
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// 企业推荐方案
        /// </summary>
        public string RecommendSolutionID { get; set; }
        /// <summary>
        /// 企业推荐方案
        /// </summary>
        public string RecommendSolutionName { get; set; }

        /// <summary>
        /// 联系人职位
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// 联系人部门
        /// </summary>
        public string Department { get; set; }

        #endregion
    }

    public class TB_CustomerMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object CustomerID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户名称", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object CustomerName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "归属人", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SysPersonID { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "企业性质", Order = 3)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CompanyNatureCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "注册资金", Order = 3)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object RegisterCapital { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "销售收入", Order = 3)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SalesRevenue { get; set; }






        [ScaffoldColumn(true)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        [Display(Name = "成立日期", Order = 4)]
        public object EstablishDate { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "所属行业", Order = 5)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object VocationCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所在省份", Order = 6)]
        //[Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ProvinceCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所在城市", Order = 6)]
        //[Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CityCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所在行政区", Order = 6)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object DistrictCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "详细地址", Order = 6)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object AddressDetails { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "客户级别", Order = 7)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerLevel { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司规模", Order = 7)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CompanySize { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户漏斗", Order = 8)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerFunnel { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "联系人", Order = 9)]
        //[Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ContactPerson { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "部门", Order = 10)]
        public object Department { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "职位", Order = 11)]
        public object Post { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "手机号码", Order = 12)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        //[RegularExpression(@"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$", ErrorMessage = "电话格式有误。")]        
        public object ContactPhone { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "固定电话", Order = 13)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        //[RegularExpression(@"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$", ErrorMessage = "电话格式有误。")]
        public object ContactTel { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "邮箱", Order = 14)]
        public object EmailAddress { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "来源说明", Order = 15)]
        [StringLength(500, ErrorMessage = "长度不可超过500")]
        public object PositionLink { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "是否注册", Order = 16)]
        public object IsRegister { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "认证状态", Order = 17)]
        public object IsCertification { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "官网", Order = 18)]
        public object HomePage { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "公司简介", Order = 19)]
        public object Comments { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "职位状态", Order = 20)]
        public object ReleaseState { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "绑定公司", Order = 21)]
        public object CompanyName { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "解除日期", Order = 22)]
        public object RelieveDate { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "创建时间", Order = 23)]
        public object CreatedTime { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "客户来源", Order = 23)]
        public object CustomerSource { get; set; }

    }

    public class CustomerServiceModel
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string OwnershipPerson { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }

        public int AuditStatus { get; set; }

        public bool IsFrozen { get; set; }
    }



}
