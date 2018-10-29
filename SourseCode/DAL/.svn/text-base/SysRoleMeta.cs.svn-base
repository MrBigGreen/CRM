using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CRM.DAL
{
    [MetadataType(typeof(SysRoleMetadata))]//使用SysRoleMetadata对SysRole进行数据验证
    public partial class SysRole
    {

        #region 自定义属性，即由数据实体扩展的实体

        [Display(Name = "人员")]
        public string SysPersonId { get; set; }
        [Display(Name = "人员")]
        public string SysPersonIdOld { get; set; }

        #endregion

    }
    public class SysRoleMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object Id { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "名称", Order = 2)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object Name { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "描述", Order = 3)]
        [StringLength(4000, ErrorMessage = "长度不可超过4000")]
        public object Description { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "排序", Order = 4)]
        public object Sort { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "是否显示", Order = 4)]
        public object IsDisplay { get; set; }


        [ScaffoldColumn(true)]
        [Display(Name = "创建时间", Order = 4)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? CreateTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "创建人", Order = 5)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object CreatePerson { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "编辑时间", Order = 6)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.DateTime, ErrorMessage = "时间格式不正确")]
        public DateTime? UpdateTime { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "编辑人", Order = 7)]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public object UpdatePerson { get; set; }


    }
}


