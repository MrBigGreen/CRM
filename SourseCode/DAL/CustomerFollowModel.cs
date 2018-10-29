using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    /// <summary>
    /// Name:跟进任务实体
    /// Author：Jonny
    /// Date:2015-6-11
    public class CustomerFollowModel
    {
        public string CustomerFollowID { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? FollowUpDate { get; set; }

        public DateTime? GoTime { get; set; }

        public DateTime? BackTime { get; set; }
        public string CustomerName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string CustomerLevel { get; set; }
        public string SysPersonID { get; set; }
        public string MyName { get; set; }
        public string FollowUpCategory { get; set; }
        public string FollowUpCategoryID { get; set; }
        public string FollowUpMode { get; set; }
        public string FollowUpModeID { get; set; }
        public string CustomerID { get; set; }
        public string FollowUpPurpose { get; set; }
        public string FollowUpPurposeID { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsFinish { get; set; }
        public string ContactName { get; set; }
        public string CallPhone { get; set; }
        public short? ContactState { get; set; }
        public string Opportunities { get; set; }
        public int? TalkTimeLength { get; set; }
        public string CustomerFunnel { get; set; }
        public string CustomerFunnelName { get; set; }
        public string FollowBack { get; set; }

        public string AddressDetails { get; set; }
        public string OpportunitiesID { get; set; }
        public string ProvinceName { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string Telephone { get; set; }
        public string TelephoneExt { get; set; }
        public string FileUrl { get; set; }

        public string Remark { get; set; }

        public string PositionLink { get; set; }

        public string OtherLevel { get; set; }
    }

    public class CustomerSelect
    {

        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }

    }

    public class HRListEntity
    {
        public string CustomerFollowID { get; set; }
        public string CustomerName { get; set; }
        public string FollowUpPurpose { get; set; }
        public string FollowUpPurposeName { get; set; }
        public string CustomerFunnel { get; set; }
        public string CustomerFunnelName { get; set; }

        public Nullable<System.DateTime> ReservationDate { get; set; }
        public Nullable<System.DateTime> GoTime { get; set; }
        public Nullable<System.DateTime> BackTime { get; set; }
        public string MyName { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> FollowUpDate { get; set; }

     
    }
}
