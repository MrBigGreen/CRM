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
    /// 描述说明：客户联系信息实体
    /// </summary>
    [MetadataType(typeof(TB_CustomerContactMetadata))]//使用TB_CustomerContactMetadata对TB_CustomerContact进行数据验证
    public partial class TB_CustomerContact : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体

        #endregion
    }

    public class TB_CustomerContactMetadata 
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object CustomerContactID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "联系人", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ContactName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "部门", Order = 3)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object Department { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "职务", Order = 4)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object Post { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "固定电话", Order = 5)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        //[RegularExpression(@"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$", ErrorMessage = "电话格式有误。")]
        public object CompanyTel { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "前台电话", Order = 6)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        //[RegularExpression(@"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$", ErrorMessage = "电话格式有误。")]
        public object CompanyTel2 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "手机1", Order = 7)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        //[RegularExpression(@"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$", ErrorMessage = "电话格式有误。")]
        public object PhoneNumber1 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "手机2", Order = 8)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        //[RegularExpression(@"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)$", ErrorMessage = "电话格式有误。")]
        public object PhoneNumber2 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "邮箱1", Order = 9)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object Email1 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "邮箱2", Order = 10)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object Email2 { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "QQ号码", Order = 11)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object QQID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "生日", Order = 12)]
        [StringLength(10, ErrorMessage = "长度不可超过10")]
        public object BirthDate { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "感兴趣", Order = 13)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object Interested { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "备注", Order = 14)]
        [StringLength(300, ErrorMessage = "长度不可超过300")]
        public object Remark { get; set; }
    }
}
