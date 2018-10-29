using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{
    public class ResumeInfoModel
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public string ImportResumeID { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "归属人", Order = 2)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string SysPersonName { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "姓名", Order = 3)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public string ResumeName { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "手机号码", Order = 4)]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
        public string PhoneNumber { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "性别", Order = 6)]
        public int? GenderCode { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "出生日期", Order = 7)]
        public DateTime? BOB { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "邮箱", Order = 8)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string EmailAddress { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "简历来源", Order = 9)]
        public int? ResumeSource { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "工作年限", Order = 10)]
        public string WorkYearCode { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "创建时间", Order = 11)]
        public DateTime? CreatedTime { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "学历", Order = 12)]

        public string EducationName { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "居住地", Order = 13)]
        public string AddressName { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "创建人", Order = 14)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "9191Offer用户", Order = 15)]
        public string OfferUserID { get; set; }

        public bool? IsDel { get; set; }
        public int? CallCount { get; set; }
        public DateTime? CallTime { get; set; }
        public string ProvinceCode { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }
        public int? CVLevel { get; set; }
        public bool? IsSendEmail { get; set; }
        public bool? IsSendMessage { get; set; }
        public int? IsVideo { get; set; }
        public string CallTaskID { get; set; }
        public int? CallStatus { get; set; }

        public string SysPersonID { get; set; }


    }
}
