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
    /// 创建时间：2015-06-19<br/>
    /// 描述说明：自定义客户信息实体
    /// </summary>
    public class CustomerModel
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CompanyNatureCode { get; set; }
        public Nullable<System.DateTime> EstablishDate { get; set; }
        public string VocationCode { get; set; }
        public string VocationName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CustomerLevel { get; set; }
        public string CustomerFunnel { get; set; }
        public Nullable<bool> IsRegister { get; set; }
        public Nullable<bool> IsCertification { get; set; }
        public string ReleaseState { get; set; }
        public string HomePage { get; set; }
        public string Comments { get; set; }
        public string CompanyStatusCode { get; set; }
        public string CompanyPreview { get; set; }
        public string CompanyCertification { get; set; }
        public string SysPersonID { get; set; }
        public Nullable<System.DateTime> BelongingDate { get; set; }
        public Nullable<System.DateTime> RelieveDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> IsFrozen { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }

        public Nullable<System.DateTime> FollowUpDate { get; set; }


        [Display(Name = "归属人")]
        public string SysPersonName { get; set; }

        [Display(Name = "社招职位")]
        public Nullable<int> SocialRecruitingQty { get; set; }

        [Display(Name = "校招职位")]
        public Nullable<int> SchoolRecruitingQty { get; set; }

        [Display(Name = "商机判断")]
        public string Opportunities { get; set; }

        public string ContactPerson { get; set; }


        public string ContactPhone { get; set; }

        public string ContactTel { get; set; }

        public string EmailAddress { get; set; }

        /// 企业推荐方案
        /// </summary>
        public string RecommendSolutionName { get; set; }

        public long SortColumn { get; set; }
        /// <summary>
        ///   分享过来的客户
        /// </summary>
        public int GiveMe { get; set; }

        /// <summary>
        /// 分享出去的客户
        /// </summary>
        public int ToPerson { get; set; }

        /// <summary>
        /// 审核状态（0 无需审核 1 审核通过 2 待审核 3 审核不通过  ）
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditPerson { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public Nullable<System.DateTime> AuditTime { get; set; }
        /// <summary>
        /// 客户来源
        /// </summary>
        public string PositionLink { get; set; }

        /// <summary>
        /// 审核拒绝原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 客户跟进任务数量
        /// </summary>
        public int FollowCount { get; set; }

        public string RatingScore { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomerFunnel
    {
        public string CustomerFunnelName { get; set; }

        public string CustomerFunnelID { get; set; }

        public int Total { get; set; }

    }

}
