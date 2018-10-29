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
    /// 创建时间：2015-07-24<br/>
    /// 描述说明：客户查重验证实体
    /// </summary>
    [MetadataType(typeof(TB_CustomerVerificationMetadata))]//使用TB_CustomerVerificationMetadata对TB_CustomerVerification进行数据验证
    public partial class TB_CustomerVerification : BaseEntity
    {
        #region 自定义属性，即由数据实体扩展的实体

        #endregion
    }

    public class TB_CustomerVerificationMetadata
    {

    }

    /// <summary>
    /// 创建人：chand<br/>
    /// 创建时间：2015-07-24<br/>
    /// 描述说明：自定义客户查重验证实体
    /// </summary>
    public class CustomerRepeatEntity
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string SysPersonID { get; set; }
        public string MyName { get; set; }
        public string EmailAddress { get; set; }
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }

        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string AddressDetails { get; set; }
        public bool IsFrozen { get; set; }

        public int AuditStatus { get; set; }
        public bool IsDelete { get; set; }

    }

}
