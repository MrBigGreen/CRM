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
    /// 创建时间：2015-08-07<br/>
    /// 描述说明：跟进记录可推荐的方案关联表实体
    /// </summary>
    [MetadataType(typeof(TB_CustomerProjectMetadata))]//使用TB_CustomerProjectMetadata对TB_CustomerProject进行数据验证
    public partial class TB_CustomerProject : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体

        #endregion
    }

    public class TB_CustomerProjectMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object CustomerProjectID { get; set; }



        [Display(Name = "职位名称", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object ProjectName { get; set; }


        [Display(Name = "职位描述", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(2000, ErrorMessage = "长度不可超过2000")]
        public object ProjectDesc { get; set; }

        [Display(Name = "职位要求", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(2000, ErrorMessage = "长度不可超过2000")]
        public object ProjectRequirements { get; set; }

        [Display(Name = "薪资范围", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object ProjectBudget { get; set; }

        [Display(Name = "招聘人数", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        public object ProjectPeopleQty { get; set; }

        [Display(Name = "福利待遇", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ProjectBenefits { get; set; }

        [Display(Name = "工作地点", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object ProjectAddress { get; set; }

        [Display(Name = "评估说明", Order = 5)]
        [StringLength(500, ErrorMessage = "长度不可超过500")]
        public object EvaluationDesc { get; set; }


        [Display(Name = "评估结果", Order = 6)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object EvaluationResult { get; set; }


        [Display(Name = "评估人", Order = 5)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object EvaluationPerson { get; set; }

    }


    public class CustomerProjectEntity
    {

        public string CustomerName { get; set; }

        public string CustomerID { get; set; }
        public string CustomerProjectID { get; set; }
        public string ProjectName { get; set; }

        public string ProjectBudget { get; set; }
        public string ProjectDesc { get; set; }

        public string EvaluationDesc { get; set; }

        public string EvaluationResult { get; set; }

        public string EvaluationResultText { get; set; }
        public string EvaluationPerson { get; set; }
        public string EvaluationPersonName { get; set; }

        public Nullable<System.DateTime> EvaluationTime { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
