using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL.CommonEntity
{

    public class CompanyJob
    {
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string HRName { get; set; }
        public string JobHunter { get; set; }
        public string QQ { get; set; }
        public string HREmail { get; set; }
        public string JobHunterEmail { get; set; }
        public string Account { get; set; }
        public string InterviweTime { get; set; }
        public int? CompanyID { get; set; }
        public int? CompanyInterviewJobID { get; set; }

    }

    /// <summary>
    /// 类名：职位申请邮件实体
    /// 说明：发送给企业通知的实体内容
    /// 创建人：chand
    /// 创建日期：2015-04-03
    /// </summary>
    public class SendMailCompanyEntity
    {
        private string m_CompanyName;
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }

        private string m_CompanyEmail;

        public string CompanyEmail
        {
            get { return m_CompanyEmail; }
            set { m_CompanyEmail = value; }
        }

        private string m_InterviweJob;
        /// <summary>
        /// 发布的职位
        /// </summary>
        public string JobTitle
        {
            get { return m_InterviweJob; }
            set { m_InterviweJob = value; }
        }

        private string m_ApplyName;
        /// <summary>
        /// 姓名
        /// </summary>
        public string ApplyName
        {
            get { return m_ApplyName; }
            set { m_ApplyName = value; }
        }
        private string m_Gender;
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            get { return m_Gender; }
            set { m_Gender = value; }
        }
        private string m_Education;
        /// <summary>
        /// 学历
        /// </summary>
        public string Education
        {
            get { return m_Education; }
            set { m_Education = value; }
        }
        private string m_WorkExperience;
        /// <summary>
        /// 工作经验
        /// </summary>
        public string WorkExperience
        {
            get { return m_WorkExperience; }
            set { m_WorkExperience = value; }
        }
        private string m_City;
        /// <summary>
        /// 住址
        /// </summary>
        public string City
        {
            get { return m_City; }
            set { m_City = value; }
        }
        private string m_ShoolName;
        /// <summary>
        /// 学校
        /// </summary>
        public string ShoolName
        {
            get { return m_ShoolName; }
            set { m_ShoolName = value; }
        }
        private string m_ResumeVideo;
        /// <summary>
        /// 是否有视频
        /// </summary>
        public string ResumeVideo
        {
            get { return m_ResumeVideo; }
            set { m_ResumeVideo = value; }
        }
        private string m_JobStatus;
        /// <summary>
        /// 求职状态
        /// </summary>
        public string JobStatus
        {
            get { return m_JobStatus; }
            set { m_JobStatus = value; }
        }

        private string m_ApplyEmail;
        /// <summary>
        /// 申请职位人邮箱
        /// </summary>
        public string ApplyEmail
        {
            get { return m_ApplyEmail; }
            set { m_ApplyEmail = value; }
        }
        private string m_HREmailAddress;
        /// <summary>
        /// HR邮箱
        /// </summary>
        public string HREmailAddress
        {
            get { return m_HREmailAddress; }
            set { m_HREmailAddress = value; }
        }
        /// <summary>
        /// 登录后跳转链接
        /// </summary>
        public string ReturnURL { get; set; }
    }
}
