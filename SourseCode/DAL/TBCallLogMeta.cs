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
    /// 描述说明：呼叫记录信息
    /// </summary>
    [MetadataType(typeof(TB_CustomerMetadata))]//使用TB_CallLogMetadata对TB_CallLog进行数据验证
    public partial class TB_CallLog : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体

        #endregion
    }

    public class TB_CallLogMetadata
    {
    }
}
