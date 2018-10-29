using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysPersonMetadata))]//使用SysPersonMetadata对SysPerson进行数据验证
    public partial class SysPerson
    {

        #region 自定义属性，即由数据实体扩展的实体

        [Display(Name = "组织")]
        public string SysDepartmentIdOld { get; set; }

        [Display(Name = "角色权限")]
        public string SysRoleId { get; set; }

        [Display(Name = "角色")]
        public string SysRoleName { get; set; }


        [Display(Name = "角色")]
        public string SysRoleIdOld { get; set; }

        [Display(Name = "文档管理")]
        public string SysDocumentId { get; set; }
        [Display(Name = "文档管理")]
        public string SysDocumentIdOld { get; set; }

        #endregion

    }
    public class SysPersonMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object Id { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "登录账号", Order = 2)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object Name { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "姓名", Order = 3)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object MyName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "密码", Order = 4)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public object Password { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "确认密码", Order = 5)]
        [Required(ErrorMessage = "请填写")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次密码不一致")]
        public object SurePassword { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "性别", Order = 6)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object Sex { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "部门：", Order = 7)]
        [StringLength(36, ErrorMessage = "长度不可超过36")]
        public object SysDepartmentId { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "职位", Order = 8)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object Position { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "手机号码", Order = 9)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object MobilePhoneNumber { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "企业QQ", Order = 10)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        //[DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber,ErrorMessage="号码格式不正确")]
        public object CompanyQQ { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "座机号码", Order = 11)]
        [StringLength(50, ErrorMessage = "50")]
        //[DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber,ErrorMessage="号码格式不正确")]
        public object Telephone { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "分机号码", Order = 12)]
        public object TelephoneExt { get; set; }


        //[ScaffoldColumn(true)]
        //[Display(Name = "省", Order = 11)]
        //[StringLength(200, ErrorMessage = "长度不可超过200")]
        //public object Province { get; set; }

        //[ScaffoldColumn(true)]
        //[Display(Name = "市", Order = 12)]
        //[StringLength(200, ErrorMessage = "长度不可超过200")]
        //public object City { get; set; }

        //[ScaffoldColumn(true)]
        //[Display(Name = "县", Order = 13)]
        //[StringLength(200, ErrorMessage = "长度不可超过200")]
        //public object Village { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "英文名称", Order = 14)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object EnglishName { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "邮箱", Order = 15)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object EmailAddress { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "备注", Order = 16)]
        [StringLength(4000, ErrorMessage = "长度不可超过4000")]
        public object Remark { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "账号状态", Order = 17)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object State { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建时间", Order = 18)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? CreateTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建人", Order = 19)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object CreatePerson { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "编辑时间", Order = 20)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? UpdateTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "登录次数", Order = 21)]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int? LogonNum { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "本次登录时间", Order = 22)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]

        public DateTime? LogonTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "登录IP", Order = 23)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object LogonIP { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "上次登录时间", Order = 24)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]

        public DateTime? LastLogonTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "上次登录IP", Order = 25)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object LastLogonIP { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "页面皮肤", Order = 26)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object PageStyle { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "编辑人", Order = 27)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object UpdatePerson { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "时间戳", Order = 28)]
        public object Version { get; set; }
        [ScaffoldColumn(true)]
        [Display(Name = "高清头像", Order = 29)]
        public object HDpic { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "客户上限", Order = 30)]
        public object CustomerCeiling { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "进程类型", Order = 31)]
        public object FollowType { get; set; }




        [ScaffoldColumn(true)]
        [Display(Name = "在职状态", Order = 32)]
        public object PositionStatus { get; set; }
    }
}


