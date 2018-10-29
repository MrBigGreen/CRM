using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{ 

    [MetadataType(typeof(TB_CustomerServiceMetadata))]//使用TB_CustomerServiceMetadata对TB_CustomerService进行数据验证
    public partial class TB_CustomerService : BaseEntity
    {

        #region 自定义属性，即由数据实体扩展的实体

        #endregion

    }
    public class TB_CustomerServiceMetadata
    {
        [ScaffoldColumn(false)]
        [Display(Name = "主键", Order = 1)]
        public object CustomerID { get; set; }

        [ScaffoldColumn(true)]
        [Display(Name = "字段", Order = 1)]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public object SysPersonID { get; set; }


       
    }

    public class CustomerServiceEntity {

        public string CustomerID { get; set; }
        public string SysPersonID { get; set; }
        public string SysPersonName { get; set; }

    }

}
