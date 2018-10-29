using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CRM.DAL
{
    [MetadataType(typeof(TP_EmployeesMetadata))]//使用TP_EmployeesMetadata对TP_Employees进行数据验证
    public partial class TP_Employees : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体
        public string CustomerName { get; set; }

        #endregion

    }
    public class TP_EmployeesMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object EmpID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "员工姓名", Order = 1)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object EmpName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户编号", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object CustomerID { get; set; }

        [ScaffoldColumn(true)]
        [Range(0, 1, ErrorMessage = "数值超出范围")]
        public object Gender { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "在职状态", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public object JobStatus { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "银行", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object BankType { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "银行开户市", Order = 3)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object BankCity { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "银行卡号", Order = 3)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object BankID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "证件编号", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object IDCard { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "证件类型", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object IDType { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "合同主体", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object ContractSubject { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "工资主体", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object WagesSubject { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "社保主体", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SocialSecuritySubject { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "个税主体", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object TaxSubject { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "社保编号", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SocialInsurID { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "公积金编号", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object HousingFundID { get; set; }
    }



    public partial class TPEmployeesMeta
    {

    }
    public partial class TPEmployees
    {
        public string CustomerName { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string CustomerID { get; set; }
        public Nullable<short> Gender { get; set; }
        public string GenderName { get; set; }
        public short JobStatus { get; set; }
        public string JobStatusName { get; set; }
        public string BankType { get; set; }
        public string BankCity { get; set; }
        public string BankID { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Nation { get; set; }
        public string IDType { get; set; }
        public string IDCard { get; set; }
        public string IDCardPic1 { get; set; }
        public string IDCardPic2 { get; set; }
        public string IDCardPic3 { get; set; }
        public string ContractSubject { get; set; }
        public string WagesSubject { get; set; }
        public string SocialSecuritySubject { get; set; }
        public string TaxSubject { get; set; }

        public string ContractSubjectName { get; set; }
        public string WagesSubjectName { get; set; }
        public string SocialSecuritySubjectName { get; set; }
        public string TaxSubjectName { get; set; }

        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }

        public string ErrorInfo { get; set; }
        public string BankTypeName { get; set; }
        public string IDTypeName { get; set; }

        public string SocialInsurID { get; set; }
        public string SocialInsurName { get; set; }
        public string HousingFundID { get; set; }
        public string HousingFundName { get; set; }
    }


    public class PayCustomer
    {

        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int OnJob { get; set; }
        public int LeaveJob { get; set; }
    }
}
