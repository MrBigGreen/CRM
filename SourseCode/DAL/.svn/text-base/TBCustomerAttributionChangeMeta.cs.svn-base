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
    /// 描述说明：客户归属变更记录实体
    /// </summary>
    [MetadataType(typeof(TB_CustomerAttributionChangeMetadata))]//使用TB_CustomerAttributionChangeMetadata对TB_CustomerAttributionChange进行数据验证
    public partial class TB_CustomerAttributionChange : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体
        [ScaffoldColumn(true)]
        [Display(Name = "归属人")]
        public string SysPersonName { get; set; }
        #endregion
    }

    public class TB_CustomerAttributionChangeMetadata
    {
    }
}
