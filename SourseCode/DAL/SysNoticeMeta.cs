using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysNoticeMetadata))]//使用SysNoticeMetadata对SysNotice进行数据验证
    public partial class SysNotice
    {

        #region 自定义属性，即由数据实体扩展的实体

        /// <summary>
        /// 
        /// </summary>
        public string CreatePersonName { get; set; }
        #endregion

    }
    public class SysNoticeMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object Id { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "标题", Order = 2)]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
        public object Title { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "内容", Order = 2)]
        [StringLength(2000, ErrorMessage = "长度不可超过2000")]
        public object Message { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "失效时间", Order = 3)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]

        public DateTime? LostTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "人员", Order = 4)]
        [StringLength(36, ErrorMessage = "长度不可超过36")]
        public object PersonId { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "备注", Order = 5)]
        [StringLength(2000, ErrorMessage = "长度不可超过2000")]
        public object Remark { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "状态", Order = 6)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object State { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建时间", Order = 7)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? CreateTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建人", Order = 8)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object CreatePerson { get; set; }


    }


    public class SysNoticeEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> LostTime { get; set; }
        public string PersonId { get; set; }
        public string Remark { get; set; }
        public bool IsRead { get; set; }
        public string State { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<System.DateTime> ReadTime { get; set; }

        public string CreatePersonName { get; set; }

    }


}


