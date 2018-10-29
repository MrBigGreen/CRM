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
    /// 描述说明：合同信息实体
    /// </summary>
    [MetadataType(typeof(TB_ContractMetadata))]//使用TB_ContractMetadata对TB_Contract进行数据验证
    public partial class TB_Contract : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体

        /// <summary>
        /// 合作服务
        /// </summary>
        public string RecommendSolutionID { get; set; }

        /// <summary>
        /// 合作服务
        /// </summary>
        public string RecommendSolutionIDs { get; set; }

        public string RecommendSolutionNames { get; set; }

        /// <summary>
        /// 相关人员
        /// </summary>
        public string SysPersonID { get; set; }

        public string SysPersonNames { get; set; }


        /// <summary>
        /// 原来相关人员
        /// </summary>
        public string SysPersonIDOld { get; set; }


        public string SigningCompanyName { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string SendCode { get; set; }

         
        #endregion
    }

    public class TB_ContractMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object ContractID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "合同编号", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ContractNO { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户编号", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerID { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "合同金额", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ContractMoney { get; set; }



        [ScaffoldColumn(true)]
        [Display(Name = "生效日期", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public object EffectiveDate { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "截止日期", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public object Deadline { get; set; }

        [ScaffoldColumn(true)]
        [Required(ErrorMessage = "请填写")]
        [Display(Name = "合作服务", Order = 3)]
        public object RecommendSolutionID { get; set; }


        [ScaffoldColumn(true)]
        [Required(ErrorMessage = "请填写")]
        [Display(Name = "签约公司", Order = 5)]
        public object SigningCompany { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "手机号码", Order = 8)]
        public object PhoneNumber { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "验证码", Order = 9)]
        public object SendCode { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "付款方式", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object PayType { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "签约类型", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        public object IsNew { get; set; }



        [ScaffoldColumn(true)]
        [Display(Name = "备注", Order = 3)]
        public object Remark { get; set; }
    }


    public class ContractSolution
    {

        public string RecommendSolutionID { get; set; }

        public string RecommendSolutionName { get; set; }

        public string ContractID { get; set; }

    }
}
