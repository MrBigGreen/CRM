using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// 创建人：chand
    /// 创建时间：2015-06-10
    /// 描述说明：客户跟进任务实体
    /// </summary>
    [MetadataType(typeof(TB_CustomerFollowMetadata))]//使用TB_CustomerFollowMetadata对TB_CustomerFollow进行数据验证
    public partial class TB_CustomerFollow : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体
        [ScaffoldColumn(true)]
        [Display(Name = "客户名称")]
        public string CustomerName { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "客户级别（低级、中级、高级）")]
        public string CustomerLevel { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "跟进人")]
        public string SysPersonName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "联系人")]
        public string ContactName { get; set; }

        /// <summary>
        /// 在批量创建跟进的时候使用
        /// </summary>
        [ScaffoldColumn(true)]
        [Display(Name = "客户编号")]
        public string CustomerIDs { get; set; }

        /// <summary>
        /// 填写跟进记录
        /// </summary>
        [ScaffoldColumn(true)]
        [Display(Name = "下次跟进目的")]
        public string NextFollowUpPurpose { get; set; }

        /// <summary>
        /// 可推荐方案
        /// </summary>
        [ScaffoldColumn(true)]
        [Display(Name = "可推荐方案")]
        public string RecommendSolutionID { get; set; }


        /// <summary>
        /// 可推荐方案
        /// </summary>
        [ScaffoldColumn(true)]
        [Display(Name = "可推荐方案")]
        public string RecommendSolutionIDNew { get; set; }


        /// <summary>
        /// 可推荐方案
        /// </summary>
        [ScaffoldColumn(true)]
        [Display(Name = "可推荐方案")]
        public string RecommendSolutionIDOld { get; set; }


        /// <summary>
        /// 分机号
        /// </summary>
        public string Fcode { get; set; }



        /// <summary>
        /// 客户邮箱
        /// </summary>
        public string CustomerEmail { get; set; }

        #endregion
    }

    public class TB_CustomerFollowMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object CustomerFollowID { get; set; }

        /// <summary>
        /// 预约跟进日期
        /// </summary>
        [ScaffoldColumn(true)]
        [Display(Name = "跟进日期", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public object ReservationDate { get; set; }

        [Display(Name = "跟进方式", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object FollowUpMode { get; set; }


        [Display(Name = "跟进次数", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object FollowUpCategory { get; set; }

        [Display(Name = "对方级别", Order = 5)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object OtherLevel { get; set; }

        [Display(Name = "跟进目的", Order = 6)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object FollowUpPurpose { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所在省份", Order = 6)]

        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ProvinceCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所在城市", Order = 6)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CityCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "所在行政区", Order = 6)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object DistrictCode { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "详细地址", Order = 9)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object AddressDetails { get; set; }

        [Display(Name = "联系状态", Order = 10)]
        [Range(0, 10)]
        public object ContactState { get; set; }

        [Display(Name = "实际跟进日期", Order = 11)]
        public object FollowUpDate { get; set; }

        [Display(Name = "客户反馈", Order = 12)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerFeedback { get; set; }

        [Display(Name = "客户漏斗", Order = 12)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerFunnel { get; set; }

        [Display(Name = "处理方式", Order = 13)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ProcessMode { get; set; }

        [Display(Name = "商机判断", Order = 14)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object Opportunities { get; set; }


        [Display(Name = "预计成单金额", Order = 14)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ExpectedMoney { get; set; }

        [Display(Name = "预计成单日期", Order = 14)]
        public object ExpectedDate { get; set; }

        [Display(Name = "下次跟进日期", Order = 15)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public object NextFollowDate { get; set; }

        [Display(Name = "下次跟进方式", Order = 16)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object NextFollowUpMode { get; set; }

        [Display(Name = "客户反馈的需求描述", Order = 17)]
        [StringLength(500, ErrorMessage = "长度不可超过500")]
        public object Remark { get; set; }

    }
}
