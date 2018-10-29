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
    /// 创建日期：2015-04-24
    /// 类名：企业信息
    /// </summary>
    [MetadataType(typeof(CompanyMetadata))]//使用SysLogMetadata对SysLog进行数据验证
    public partial class Company
    {

        #region 自定义属性，即由数据实体扩展的实体
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNatureCode { get; set; }
        public string CompanyNatureName { get; set; }
        public string VocationCode { get; set; }
        public string VocationCodeName { get; set; }
        public string FoundAccountCode { get; set; }
        public string CompanyScaleCode { get; set; }
        public string CompanyScaleCodeName { get; set; }
        public string JobAddressDetails { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FaxNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EducationCode { get; set; }
        public bool IsCreatedbyResume { get; set; }
        public string DepartmentCode { get; set; }
        #endregion

    }
    public class CompanyMetadata
    {

    }
}
