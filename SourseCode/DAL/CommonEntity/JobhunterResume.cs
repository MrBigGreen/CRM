using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{
    [Serializable]
    public class JobhunterResume
    {
        [DataMember]
        public int JobhunterResumeID { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string ResumeName { get; set; }

        [DataMember]
        public string ResumeTypeCode { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public DateTime DOB { get; set; }

        [DataMember]
        public string EducationCode { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string GenderCode { get; set; }

        [DataMember]
        public bool IsCreatedbyResume { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string ResumePreview { get; set; }

        [DataMember]
        public int UserInformationID { get; set; }

        [DataMember]
        public bool IsOpen { get; set; }
    }
    [Serializable]
    public class IsHasOfflineInterviewResume
    {
        [DataMember]
        public int Ishas { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public int DayNum { get; set; }
        [DataMember]
        public string JobVideoCheckState { get; set; }
        [DataMember]
        public string ResumeVideoCheckState { get; set; }
    }

    [Serializable]
    public class OfflineInterviewResume
    {
        [DataMember]
        public int JobhunterResumeID { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string ResumeName { get; set; }

        [DataMember]
        public string ResumeTypeCode { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public string ResumePreview { get; set; }

        [DataMember]
        public bool IsOpen { get; set; }

        [DataMember]
        public int ResumeID { get; set; }

        /// <summary>
        /// 视频问题录制时间记录
        /// </summary>
        //[DataMember]
        //public List<VideoQuizDurationEntity> VideoQuizDurationList { get; set; }
    }

    [Serializable]

    public class GetResume
    {
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string ResumeName { get; set; }
        [DataMember]
        public string ResumeType { get; set; }
        [DataMember]
        public string ResumeTypeName { get; set; }
        [DataMember]
        public DateTime LastUpdatedTime { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// VCR名称
        /// </summary>
        [DataMember]
        public string RVName { get; set; }
        /// <summary>
        /// VCR地址
        /// </summary>
        [DataMember]
        public string RVUrl { get; set; }

        [DataMember]
        public string CheckState { get; set; }

        [DataMember]
        public string CheckStateTitle { get; set; }

        [DataMember]
        public string ReasonTitle { get; set; }

        [DataMember]
        public string ReasonContent { get; set; }

        /// <summary>
        /// 视频来源
        /// </summary>
        [DataMember]
        public string VideoSource { get; set; }
        /// <summary>
        /// 视频截图
        /// </summary>
        [DataMember]
        public string VideoSnap { get; set; }

        [DataMember]
        public string JobIndustryCode { get; set; }

        [DataMember]
        public string JobPostCode { get; set; }

    }
    /// <summary>
    /// 用户/简历 基本信息
    /// </summary>
    [Serializable]
    public class JobhunterStandardResume
    {
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public int UserInformationID { get; set; }
        [DataMember]
        public int JtId { get; set; }
        [DataMember]
        public int EvaId { get; set; }
        [DataMember]
        public string ResumeName { get; set; }
        [DataMember]
        public string ResumeTypeCode { get; set; }
        [DataMember]
        public string PublicCode { get; set; }
        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string GenderCode { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string DistrictCode { get; set; }
        //[DataMember]
        //public string YearOfBirthday { get; set; }
        //[DataMember]
        //public string MonthOfBirthday { get; set; }
        //[DataMember]
        //public string DayOfBirthday { get; set; }
        [DataMember]
        public string Birthday { get; set; }
        [DataMember]
        public DateTime BOB { get; set; }
        [DataMember]
        public string WorkYearCode { get; set; }
        [DataMember]
        public string WorkYearValue { get; set; }
        [DataMember]
        public string WorkYear { get; set; }
        [DataMember]
        public string WorkMonth { get; set; }

        [DataMember]
        public int TotalWorkYear { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string CredentialsCode { get; set; }
        [DataMember]
        public string CredentialsValue { get; set; }

        [DataMember]
        public string CredentialsCodeValue { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        [DataMember]
        public string Graduated { get; set; }

        [DataMember]
        public string EducationCode { get; set; }
        [DataMember]
        public string Education { get; set; }

        public string EducationValue { get; set; }
        //所学专业
        [DataMember]
        public string SpecialtyCode { get; set; }

        //政治面貌
        [DataMember]
        public string PoliticalCode { get; set; }
        [DataMember]
        public string PoliticalValue { get; set; }

        [DataMember]
        public string MarrayCode { get; set; }
        [DataMember]
        public string MarrayValue { get; set; }

        [DataMember]
        public string ProvinceCode { get; set; }

        [DataMember]
        public string CityCode { get; set; }

        [DataMember]
        public Address WorkAddress { get; set; }
        [DataMember]
        public string ProvinceA { get; set; }
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string CityA { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string DistrictA { get; set; }
        [DataMember]
        public string AddressDetails { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string UserPreview { get; set; }


        [DataMember]
        public string ContactPhoneNumber1 { get; set; }

        [DataMember]
        public string ContactPhoneNumber2 { get; set; }

        [DataMember]
        public string QQID { get; set; }
        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public string HomePage { get; set; }
        /// <summary>
        /// 自我评价
        /// </summary>
        [DataMember]
        public string EvaluateCon { get; set; }

        /// <summary>
        /// 期望工作性质代码
        /// </summary>
        [DataMember]
        public string JobNatureCode { get; set; }

        /// <summary>
        /// 期望工作性质名称
        /// </summary>
        [DataMember]
        public string JobNatureName { get; set; }
        /// <summary>
        /// 期望从事行业代码
        /// </summary>
        [DataMember]
        public string JobIndustryCode { get; set; }
        /// <summary>
        /// 期望从事行业名称
        /// </summary>
        [DataMember]
        public string JobIndustryName { get; set; }
        /// <summary>
        /// 期望从事职位代码
        /// </summary>
        [DataMember]
        public string JobPostCode { get; set; }
        /// <summary>
        /// 期望从事职位名称
        /// </summary>
        [DataMember]
        public string JobPostName { get; set; }
        /// <summary>
        /// 期望工作地点
        /// </summary>
        [DataMember]
        public string JobAddressCode { get; set; }
        /// <summary>
        /// 期望工作地点
        /// </summary>
        [DataMember]
        public string JobAddressName { get; set; }


        [DataMember]
        public string TargetJobCode { get; set; }
        [DataMember]
        public string TargetJobValue { get; set; }

        [DataMember]
        public string JobSalaryCode { get; set; }
        [DataMember]
        public string JobSalaryValue { get; set; }

        [DataMember]
        public string JobStatusCode { get; set; }
        [DataMember]
        public string JobStatusValue { get; set; }

        [DataMember]
        public DateTime CreatedTime { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        //跟踪客服
        [DataMember]
        public string Staff { get; set; }

        /// <summary>
        /// VCR名称
        /// </summary>
        [DataMember]
        public string RVName { get; set; }
        /// <summary>
        /// 视频来源
        /// </summary>
        [DataMember]
        public string VideoSource { get; set; }
        /// <summary>
        /// VCR地址
        /// </summary>
        [DataMember]
        public string VcrUrl { get; set; }
        [DataMember]
        public string CheckState { get; set; }
        /// <summary>
        /// 视频截图
        /// </summary>
        [DataMember]
        public string VideoSnap { get; set; }
        [DataMember]
        public string PersonalityCode { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        [DataMember]
        public List<StandardResumeEdu> EduList { get; set; }
        /// <summary>
        /// 工作经验
        /// </summary>
        [DataMember]
        public List<SRGetUpdateManagement> WorkExperienceList { get; set; }
        /// <summary>
        /// 简历附件
        /// </summary>
        [DataMember]
        public List<TB_ResumeAttachedEntity> ResumeAttachedList { get; set; }


        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string LastUpdatedBy { get; set; }
        [DataMember]
        public DateTime LastUpdatedTime { get; set; }
        [DataMember]
        public string TransactionID { get; set; }
        [DataMember]
        public int Version { get; set; }
    }
    [Serializable]
    public class Address
    {
        [DataMember]
        public int AddressID { get; set; }

        [DataMember]
        public string ProvinceCode { get; set; }

        [DataMember]
        public string CityCode { get; set; }

        [DataMember]
        public string DistrictCode { get; set; }

        [DataMember]
        public string ProvinceCodeName { get; set; }

        [DataMember]
        public string CityCodeName { get; set; }

        [DataMember]
        public string DistrictCodeName { get; set; }

        [DataMember]
        public string AddressDetails { get; set; }

        [DataMember]
        public string PostCode { get; set; }

        [DataMember]
        public string AddressURL { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public double Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public double Lat { get; set; }
    }

    [Serializable]
    public class StandardResumeEdu
    {
        [DataMember]
        public int EduID { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string IsOverSea { get; set; }
        [DataMember]
        public string ShoolName { get; set; }
        //[DataMember]
        //public string Country {get;set;}
        [DataMember]
        public string Major { get; set; }
        [DataMember]
        public string MajorName { get; set; }
        [DataMember]
        public string Degree { get; set; }
        [DataMember]
        public string DegreeName { get; set; }
        [DataMember]
        public string MajorDescription { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRJobTargetModel
    {
        [DataMember]
        public int JtId { get; set; }
        [DataMember]
        public string JobNature { get; set; }
        [DataMember]
        public string JobIndustry { get; set; }
        [DataMember]
        public string JobIndustryCode { get; set; }
        [DataMember]
        public string JobPost { get; set; }

        [DataMember]
        public string JobPostCode { get; set; }
        [DataMember]
        public string JobAddress { get; set; }

        [DataMember]
        public string JobAddressCode { get; set; }
        [DataMember]
        public string JobSalary { get; set; }
        [DataMember]
        public string JobStatus { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public string JobSalaryName { get; set; }

        [DataMember]
        public string JobStatusName { get; set; }
        [DataMember]
        public string JobNatureName { get; set; }
    }

    [Serializable]

    public class SRTrainingExperienceModel
    {
        [DataMember]
        public int TraID { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string TrainAgency { get; set; }
        [DataMember]
        public string TrainCourse { get; set; }
        [DataMember]
        public string TrainAddress { get; set; }
        [DataMember]
        public string TrainCertificate { get; set; }
        [DataMember]
        public string CourseDescription { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRProjectExperienceModel
    {
        [DataMember]
        public int ProjectEID { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string ProjectDescription { get; set; }
        [DataMember]
        public string MainResponsibility { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRMajorceModel
    {
        [DataMember]
        public int MajorID { get; set; }
        [DataMember]
        public string MajorCode { get; set; }
        [DataMember]
        public string MajorCodeName { get; set; }
        [DataMember]
        public string MajorName { get; set; }
        [DataMember]
        public string UseTime { get; set; }
        [DataMember]
        public string Mastery { get; set; }
        [DataMember]
        public string MasteryName { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRLanguageAbilityModel
    {
        [DataMember]
        public int LanID { get; set; }
        [DataMember]
        public string LanType { get; set; }
        [DataMember]
        public string LanTypeName { get; set; }
        [DataMember]
        public string LanWrite { get; set; }
        [DataMember]
        public string LanWriteName { get; set; }
        [DataMember]
        public string LanReder { get; set; }
        [DataMember]
        public string LanRederName { get; set; }
        [DataMember]
        public string LanName { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class StandardResumeManagement
    {
        [DataMember]
        public int WorkID { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string WorkAddress { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string CompanyType { get; set; }
        [DataMember]
        public string CompanySize { get; set; }
        [DataMember]
        public string Industry { get; set; }
        [DataMember]
        public string PostType { get; set; }
        [DataMember]
        public string PostName { get; set; }
        [DataMember]
        public string Salary { get; set; }
        [DataMember]
        public string LeaveReson { get; set; }
        [DataMember]
        public string WorkDescription { get; set; }
        [DataMember]
        public string MexIsHas { get; set; }
        [DataMember]
        public string MexFromYear { get; set; }
        [DataMember]
        public string Mexfrommonth { get; set; }
        [DataMember]
        public string Mextoyear { get; set; }
        [DataMember]
        public string MexToMonth { get; set; }
        [DataMember]
        public string ReportObject { get; set; }
        [DataMember]
        public string PersonNum { get; set; }
        [DataMember]
        public string Underling { get; set; }
        [DataMember]
        public string JXDescription { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRGetUpdateManagement
    {
        [DataMember]
        public int WorkID { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string WorkAddress { get; set; }
        [DataMember]
        public string WorkAddressCode { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string CompanyType { get; set; }
        [DataMember]
        public string CompanyTypeCode { get; set; }
        [DataMember]
        public string CompanySize { get; set; }
        [DataMember]
        public string CompanySizeCode { get; set; }
        [DataMember]
        public string Industry { get; set; }
        [DataMember]
        public string IndustryCode { get; set; }
        [DataMember]
        public string PostType { get; set; }
        [DataMember]
        public string PostTypeCode { get; set; }
        [DataMember]
        public string PostName { get; set; }
        [DataMember]
        public string Salary { get; set; }
        [DataMember]
        public string SalaryCode { get; set; }
        [DataMember]
        public string LeaveReson { get; set; }
        [DataMember]
        public string WorkDescription { get; set; }
        [DataMember]
        public string MexIsHas { get; set; }
        [DataMember]
        public string MexIsHasCode { get; set; }
        [DataMember]
        public string MexFromYear { get; set; }
        [DataMember]
        public string Mexfrommonth { get; set; }
        [DataMember]
        public string Mextoyear { get; set; }
        [DataMember]
        public string MexToMonth { get; set; }
        [DataMember]
        public string ReportObject { get; set; }
        [DataMember]
        public string ReportObjectCode { get; set; }
        [DataMember]
        public string PersonNum { get; set; }
        [DataMember]
        public string Underling { get; set; }
        [DataMember]
        public string UnderlingCode { get; set; }
        [DataMember]
        public string JXDescription { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public int YearNum { get; set; }
        [DataMember]
        public int MonthNum { get; set; }
        [DataMember]
        public int MexYearNum { get; set; }
        [DataMember]
        public int MexMonthNum { get; set; }
    }

    [Serializable]

    public class SRGetSelfEvaluation
    {
        [DataMember]
        public int EvaId { get; set; }
        [DataMember]
        public string EvaluateCon { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }


    [Serializable]

    public class SRJobSchoolModel
    {
        [DataMember]
        public int JsID { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string JobShoolName { get; set; }
        [DataMember]
        public string JobShoolDes { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRLearnShoolModel
    {
        [DataMember]
        public int LSID { get; set; }
        [DataMember]
        public string Smoneyone { get; set; }
        [DataMember]
        public string SmoneyoneName { get; set; }
        [DataMember]
        public string Smoneytwo { get; set; }
        [DataMember]
        public string SmoneytwoName { get; set; }
        [DataMember]
        public string AwardsName { get; set; }
        [DataMember]
        public string AwardsType { get; set; }
        [DataMember]
        public string AwardsTypeName { get; set; }
        [DataMember]
        public string AwardsDes { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }


    [Serializable]

    public class SRCertificateModel
    {
        [DataMember]
        public int CerID { get; set; }
        [DataMember]
        public string CerName { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }

        [DataMember]
        public string CerDes { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }



    [Serializable]

    public class SRLifeExperience
    {
        [DataMember]
        public int ExpID { get; set; }
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string FromYear { get; set; }
        [DataMember]
        public string FromMonth { get; set; }
        [DataMember]
        public string ToYear { get; set; }
        [DataMember]
        public string ToMonth { get; set; }
        [DataMember]
        public string WorkAddress { get; set; }
        [DataMember]
        public string WorkAddressCode { get; set; }
        [DataMember]
        public string CompanyType { get; set; }
        [DataMember]
        public string CompanyTypeCode { get; set; }
        [DataMember]
        public string CompanySize { get; set; }
        [DataMember]
        public string CompanySizeCode { get; set; }
        [DataMember]
        public string Industry { get; set; }
        [DataMember]
        public string IndustryCode { get; set; }
        [DataMember]
        public string PostType { get; set; }
        [DataMember]
        public string PostTypeCode { get; set; }
        [DataMember]
        public string PostName { get; set; }
        [DataMember]
        public string ExpDes { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    [Serializable]

    public class SRResumeVideo
    {
        [DataMember]
        public int RVID { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public string RVName { get; set; }
        [DataMember]
        public string RVDes { get; set; }
        [DataMember]
        public string RVUrl { get; set; }
        [DataMember]
        public string RVUrl1 { get; set; }
        [DataMember]
        public string RVUrl2 { get; set; }
        [DataMember]
        public string RVUrl3 { get; set; }
        [DataMember]
        public string VideoLabel { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string RVRType { get; set; }
        [DataMember]
        public int Index { get; set; }
        [DataMember]
        public DateTime Lastupdatetime { get; set; }
        [DataMember]
        public int PlayNum { get; set; }

        /// <summary>
        /// 视频截图文件名
        /// </summary>
        [DataMember]
        public string VideoSnap { get; set; }

        /// <summary>
        /// 审核状态编码
        /// </summary>
        [DataMember]
        public string CheckState { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public string CheckStateTitle { get; set; }
        /// <summary>
        /// 未通过原因(简述)
        /// </summary>
        [DataMember]
        public string ReasonTitle { get; set; }
        /// <summary>
        /// 未通过原因(具体)
        /// </summary>
        [DataMember]
        public string ReasonContent { get; set; }

        /// <summary>
        /// 录制视频来源.PC或APP
        /// </summary>
        [DataMember]
        public string VideoSource { get; set; }

        /// <summary>
        /// 视频问题录制时间记录
        /// </summary>
        //[DataMember]
        //public List<VideoQuizDurationEntity> VideoQuizDurationList { get; set; }
    }

    [Serializable]

    public class SRAttachmentsResumeDocuments
    {
        [DataMember]
        public int ARDID { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public int JobhunterDocumentsID { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileSize { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string Doutype { get; set; }
    }

    [Serializable]

    public class SRResumeItem
    {
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public int ItmeID { get; set; }
        [DataMember]
        public int Grinfo { get; set; }
        [DataMember]
        public int Zwpjinfo { get; set; }
        [DataMember]
        public int Qzyxinfo { get; set; }
        [DataMember]
        public int Jybjinfo { get; set; }
        [DataMember]
        public int Yynlinfo { get; set; }
        [DataMember]
        public int Gzjyinfo { get; set; }
        [DataMember]
        public int Pxjlinfo { get; set; }
        [DataMember]
        public int Xmjyinfo { get; set; }
        [DataMember]
        public int Zyjninfo { get; set; }
        [DataMember]
        public int Shsjinfo { get; set; }

        [DataMember]
        public int Xxqkinfo { get; set; }
        [DataMember]
        public int Zxzwinfo { get; set; }
        [DataMember]
        public int Hdzsinfo { get; set; }

        [DataMember]
        public int Mvcrinfo { get; set; }

        [DataMember]
        public int Fjjlinfo { get; set; }
        [DataMember]
        public int OtherInfo { get; set; }
        [DataMember]
        public string UserID { get; set; }

    }

    [Serializable]

    public class SRResumeOtherInfo
    {
        [DataMember]
        public int OtherId { get; set; }
        [DataMember]
        public string OtherCon { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int ResumeID { get; set; }
    }

    #region 简历搜索
    /// <summary>
    /// 搜索条件
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    public class SearchResumeCondition
    {

        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string CityCode { get; set; }
        /// <summary>
        /// 简历编号(多个逗号隔开)
        /// </summary>
        [DataMember]
        public string ResumeID { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [DataMember]
        public string CompanyId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// 求职意向行业代码(多个逗号隔开)
        /// </summary>
        [DataMember]
        public string JobIndustryCode { get; set; }
        public string JobIndustryValue { get; set; }
        /// <summary>
        /// 求职意向职位代码(多个逗号隔开)
        /// </summary>
        [DataMember]
        public string JobPostCode { get; set; }
        public string JobPostValue { get; set; }

        /// <summary>
        /// 性别代码
        /// </summary>
        [DataMember]
        public string GenderCode { get; set; }
        /// <summary>
        /// 最近更新简历天数
        /// </summary>
        [DataMember]
        public string UpdatedTimeCode { get; set; }
        /// <summary>
        /// 学历编码
        /// </summary>
        [DataMember]
        public string EducationCode { get; set; }
        /// <summary>
        /// 居住点城市代码
        /// </summary>
        [DataMember]
        public string JzCityCode { get; set; }
        public string JzCityValue { get; set; }

        /// <summary>
        /// 专业代码
        /// </summary>
        [DataMember]
        public string SpecialtyCode { get; set; }
        public string SpecialtyValue { get; set; }

        /// <summary>
        /// 期望薪资代码
        /// </summary>
        [DataMember]
        public string JobSalaryCode { get; set; }
        /// <summary>
        /// 求职状态代码
        /// </summary>
        [DataMember]
        public string JobStatusCode { get; set; }
        /// <summary>
        /// 户口城市代码
        /// </summary>
        [DataMember]
        public string HjCityCode { get; set; }
        /// <summary>
        /// 起始年龄段
        /// </summary>
        [DataMember]
        public int FromAge { get; set; }
        /// <summary>
        /// 终止年龄段
        /// </summary>
        [DataMember]
        public int ToAge { get; set; }
        /// <summary>
        /// 海外学习经历代码
        /// </summary>
        [DataMember]
        public string IsOverSea { get; set; }
        /// <summary>
        /// 工作年限代码
        /// </summary>
        [DataMember]
        public string WorkYearCode { get; set; }
        /// <summary>
        /// 外语编码(多个逗号隔开)
        /// </summary>
        [DataMember]
        public string LanTypeCode { get; set; }
        /// <summary>
        /// 操作,不为0时返回企业人才库简历
        /// </summary>
        [DataMember]
        public int Action { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 是否有VCR
        /// </summary>
        [DataMember]
        public string IsVcr { get; set; }

        [DataMember]
        public string InterviewLink { get; set; }
        [DataMember]
        public string IsMatch { get; set; }

        [DataMember]
        public int PCount { get; set; }
        [DataMember]
        public int RCount { get; set; }
    }

    [Serializable]

    public class SearchResumeResult
    {
        [DataMember]
        public List<SearchResumeModel> SearchResumeList { get; set; }

        /// <summary>
        /// 总页数输出
        /// </summary>
        [DataMember]
        public int PCount { get; set; }
        /// <summary>
        /// 总记录数输出
        /// </summary>
        [DataMember]
        public int RCount { get; set; }
    }
    /// <summary>
    /// 搜索结果
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    public class SearchResumeModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        public int Index { get; set; }

        /// <summary>
        /// 简历编号
        /// </summary>
        [DataMember]
        public int ResumeID { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string JobAddress { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DataMember]
        public string Email { get; set; }
        /// <summary>
        /// 求职意向行业
        /// </summary>
        [DataMember]
        public string JobIndustry { get; set; }
        /// <summary>
        /// 求职意向职位
        /// </summary>
        [DataMember]
        public string JobPost { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string Gender { get; set; }
        /// <summary>
        /// 最近更新简历时间
        /// </summary>
        [DataMember]
        public DateTime LastUpdatedTime { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [DataMember]
        public string Education { get; set; }
        /// <summary>
        /// 居住地
        /// </summary>
        [DataMember]
        public string JzAddress { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [DataMember]
        public string Specialty { get; set; }
        /// <summary>
        /// 期望薪资
        /// </summary>
        [DataMember]
        public string JobSalary { get; set; }
        /// <summary>
        /// 求职状态
        /// </summary>
        [DataMember]
        public string JobStatus { get; set; }
        /// <summary>
        /// 户口城市
        /// </summary>
        [DataMember]
        public string HjAddress { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public int Age { get; set; }
        /// <summary>
        /// 海外学习经历
        /// </summary>
        [DataMember]
        public string IsOverSea { get; set; }
        /// <summary>
        /// 工作年限
        /// </summary>
        [DataMember]
        public string WorkYear { get; set; }
        /// <summary>
        /// 外语(多个逗号隔开)
        /// </summary>
        [DataMember]
        public string LanTypeName { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public string UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 简历名称
        /// </summary>
        [DataMember]
        public string ResumeName { get; set; }
        /// <summary>
        /// 是否存入企业人才库
        /// </summary>
        [DataMember]
        public int IsTalentTank { get; set; }

        /// <summary>
        /// 视频审核表ID
        /// </summary>
        [DataMember]
        public int CVId { get; set; }
        /// <summary>
        /// 视频报告人格类型编码
        /// </summary>
        [DataMember]
        public string PersonalityCode { get; set; }
        /// <summary>
        /// 投递简历时间
        /// </summary>
        [DataMember]
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 视频分析报告是否和职位相符
        /// </summary>
        [DataMember]
        public int IsMatch { get; set; }


        [DataMember]
        public int IsCv { get; set; }
    }
    #endregion

    #region 视频秀简历搜索
    public class ShowResumeResult
    {
        public List<ShowResumeModel> ShowResumeList { get; set; }

        /// <summary>
        /// 总页数输出
        /// </summary>
        [DataMember]
        public int PCount { get; set; }
        /// <summary>
        /// 总记录数输出
        /// </summary>
        [DataMember]
        public int RCount { get; set; }
    }

    /// <summary>
    /// 搜索结果
    /// </summary>
    [Serializable]

    public class ShowResumeModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        public int Index { get; set; }
        /// <summary>
        /// 简历编号
        /// </summary>
        [DataMember]
        public int ResumeID { get; set; }
        /// <summary>
        /// 简历名称
        /// </summary>
        [DataMember]
        public string ResumeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 自我评价
        /// </summary>
        [DataMember]
        public string EvaluateCon { get; set; }
        /// <summary>
        /// 工作状态
        /// </summary>
        [DataMember]
        public string JobStatus { get; set; }
        /// <summary>
        /// 头像照片
        /// </summary>
        [DataMember]
        public string UserPreview { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        [DataMember]
        public string EnjoyNum { get; set; }
        /// <summary>
        /// 视频URL
        /// </summary>
        [DataMember]
        public string RVUrl { get; set; }
        /// <summary>
        /// 视频截图
        /// </summary>
        [DataMember]
        public string Screenshot { get; set; }
        /// <summary>
        /// 视频Http
        /// </summary>
        [DataMember]
        public string ShowHttp { get; set; }

    }


    [Serializable]

    public class ResumeVideoShow
    {
        [DataMember]
        public int ShowId { get; set; }
        [DataMember]
        public string ShowName { get; set; }
        [DataMember]
        public string RVUrl { get; set; }
        [DataMember]
        public string VideoSnap { get; set; }
        [DataMember]
        public string VideoSnapName { get; set; }
        [DataMember]
        public string ShowDes { get; set; }
        [DataMember]
        public string JobPost { get; set; }
        [DataMember]
        public int CommNum { get; set; }
        [DataMember]
        public int ResumeId { get; set; }

        [DataMember]
        public int EnjoyNum { get; set; }
        [DataMember]
        public bool IsVaild { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string ShowPhoto { get; set; }
        [DataMember]
        public string VideoLabel { get; set; }

        [DataMember]
        public string SessionId { get; set; }

    }

    [Serializable]

    public class ResumeVideoShowComment
    {
        [DataMember]
        public int CommentId { get; set; }
        [DataMember]
        public int ShowId { get; set; }
        [DataMember]
        public int ParentId { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string ToUserId { get; set; }
        [DataMember]
        public string ToNickName { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public string ShowPhoto { get; set; }
        [DataMember]
        public string ShowName { get; set; }

    }
    #endregion

    /// <summary>
    /// 简历 -简历附件
    /// 创建人:chand
    /// 创建时间：2015-04-28
    /// </summary>
    [Serializable]
    public class TB_ResumeAttachedEntity
    {
        /// <summary>
        /// 简历附件主键
        /// </summary>
        [DataMember]
        public int ResumeAttachedID { get; set; }
        /// <summary>
        /// 简历ID
        /// </summary>
        [DataMember]
        public int ResumeID { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        [DataMember]
        public string AttachedName { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        [DataMember]
        public string AttachedType { get; set; }
        /// <summary>
        /// 附件大小
        /// </summary>
        [DataMember]
        public string AttachedSize { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        public string Link { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [DataMember]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedTime { get; set; }
        [DataMember]
        public string LastUpdatedBy { get; set; }
        [DataMember]
        public DateTime LastUpdatedTime { get; set; }
        [DataMember]
        public string TransactionID { get; set; }
        [DataMember]
        public int Version { get; set; }
    }

}
