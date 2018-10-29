using CRM.DAL;
using CRM.DAL.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    /// 个人简历接口
    /// </summary>
    [ServiceContract(Namespace = "www.Offer.com")]
    public partial interface IImportResume
    {
        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        [OperationContract]
        List<ResumeInfoModel> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total);
        [OperationContract]
        List<ResumeInfoModel> GetByParamYesterday(string id, int page, int rows, string order, string sort, string search, ref int total);
        [OperationContract]
        List<ResumeInfoModel> GetByParamTodayCalled(string id, int page, int rows, string order, string sort, string search, ref int total);

        #region  简历搜索
        [OperationContract]
        SearchResumeResult SearchResumeListFirst(SearchResumeCondition condition);
        [OperationContract]
        SearchResumeResult SearchResumeList0420(SearchResumeCondition condition);

        [OperationContract]
        List<SearchResumeModel> SearchResumeListAll(SearchResumeCondition condition);


        JobhunterStandardResume GetResumeBaseInfoByResumeID(int resumeID);


        List<TB_ResumeAttachedEntity> GetResumeAttachedByResumeID(int resumeID);
        #endregion

        [OperationContract]
        List<ResumeInfoModel> ExportInfo(string id, string order, string sort, string search);



        [OperationContract]
        bool UpdateInfo(string phoneNum, string messageInfo, string rid, string uid, string cid, string sendType);
        /// <summary>
        /// 职位收到简历列表
        /// </summary>
        /// <param name="iCompanyInterviewJobID">职位编号</param>
        /// <param name="flag">0 委托投递数量  1 非委托投递数量（收到候选人主动投递的简历）</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns></returns>
        List<ResumeInfoModel> GetReceivedByCompanyInterviewJobID(int iCompanyInterviewJobID, int flag, int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 职位推荐简历列表
        /// </summary>
        /// <param name="iCompanyInterviewJobID">职位编号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns></returns>
        List<ResumeInfoModel> GetRecommendByCompanyInterviewJobID(int iCompanyInterviewJobID, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        [OperationContract]
        List<ResumeInfoModel> GetImportResumeByParam(string personID, int page, int rows, string order, string sort, string search, ref int total);

        /// <summary>
        /// 查询的数据
        /// </summary>
        /// <param name="id">额外的参数</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页显示的行数</param>
        /// <param name="order">排序字段</param>
        /// <param name="sort">升序asc（默认）还是降序desc</param>
        /// <param name="search">查询条件</param>
        /// <param name="total">结果集的总数</param>
        /// <returns>结果集</returns>
        [OperationContract]
        List<TB_ImportResume> GetByParam(string id, string order, string sort, string search); /*在6.0版本中 新增*/
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IQueryable<TB_ImportResume> GetAll();
        /// <summary>
        /// 获取在该表中出现的所有外键实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TB_ImportResume> GetRefSysDocument();


        /// <summary>
        /// 根据主键，查看详细信息
        /// </summary>
        /// <param name="id">根据主键</param>
        /// <returns></returns>
        [OperationContract]
        TB_ImportResume GetById(string id);
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个对象</param>
        /// <returns></returns>
        [OperationContract]
        bool Create(ref Common.ValidationErrors validationErrors, TB_ImportResume entity);
        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="id">一条数据的主键</param>
        /// <returns></returns>  
        [OperationContract]
        bool Delete(ref Common.ValidationErrors validationErrors, string id);
        /// <summary>
        /// 删除对象集合
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="deleteCollection">主键的集合</param>
        /// <returns></returns>       
        [OperationContract]
        bool DeleteCollection(ref Common.ValidationErrors validationErrors, string[] deleteCollection);
        /// <summary>
        /// 编辑一个对象
        /// </summary>
        /// <param name="validationErrors">返回的错误信息</param>
        /// <param name="entity">一个对象</param>
        /// <returns></returns>
        [OperationContract]
        bool Edit(ref Common.ValidationErrors validationErrors, TB_ImportResume entity);
        [OperationContract]
        IQueryable GetCombox(int type, string parm);
        [OperationContract]
        bool UpdateEditInfo(string isS, string uPhone, string recDate, string guidState,
                    string isLine, string companyName, string pay, string remark, string addressInfos,
             string hangye, string work, string callDate,
             string rid, string cid, string uid, string hangyeName, string workName, string addressName);
        [OperationContract]
        bool CreateOfferInfoSave(string uid, string phoneNum, string uname, string sex, string birthday,
            string num, string emailinfo, string rid, string addressInfos,
            string hangye, string work, string hangyeName, string workName, string addressName, string remark, string pay);
        [OperationContract]
        SysMessageTemp GetMessageByState(string state);
       // List<CodeTable> GetEdu();

        List<SRResumeVideo> GetMyVCR_ByUserId(string userId);

        //根据用户ID取得个人发布的所有的简历 三期 byniou
        [OperationContract]
        List<GetResume> GetAllResumeByUserID(string userID);
        //根据简历ID得到工作经验列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<SRGetUpdateManagement> GetWorkExperienceByResumeID(string resumeId);

        ////根据简历ID得到实践经验列表 三期 byniou UpdateResume/ShoolIndex
        [OperationContract]
        List<SRLifeExperience> GetLifeExperienceByResumeID(string resumeId);

        ////根据简历ID得到在校职务列表 三期 byniou UpdateResume/ShoolIndex
        [OperationContract]
        List<SRJobSchoolModel> GetJobSchoolByResumeID(string resumeId);

        ////根据简历ID得到获得证书列表 三期 byniou UpdateResume/ShoolIndex
        [OperationContract]
        List<SRCertificateModel> GetCertificateByResumeID(string resumeId);

        ////根据简历ID得到在校学习情况 三期 byniou UpdateResume/ShoolIndex
        [OperationContract]
        List<SRLearnShoolModel> GetLearnShoolByResumeID(string resumeId);

        //根据简历ID得到教育背景列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<StandardResumeEdu> GetEduByResumeID(string resumeId);

        //根据简历ID得到上传附件列表列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<SRAttachmentsResumeDocuments> GetAttachmentsResumeDocumentsByResumeID(string resumeId);

        //根据简历ID得到培训经历列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<SRTrainingExperienceModel> GetTrainingExperienceByResumeID(string resumeId);

        //根据简历ID得到项目经验列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<SRProjectExperienceModel> GetProjectExperienceByResumeID(string resumeId);


        //根据简历ID得到专业技能列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<SRMajorceModel> GetMajorByResumeID(string resumeId);

        //根据简历ID得到专业技能列表 三期 byniou UpdateResume/Index
        [OperationContract]
        List<SRLanguageAbilityModel> GetLanguageAbilityByResumeID(string resumeId);
        //创建视频简历三期 byniou UpdateResume/Index
        [OperationContract]
        int CreateJobhunterResume(OfflineInterviewResume res);

        //根据简历ID得到自我评价 三期 byniou UpdateResume/Index
        [OperationContract]
        SRGetSelfEvaluation GetSelfEvaluationByResumeId(int resumeId);

        //根据简历ID得到其它信息
        [OperationContract]
        SRResumeOtherInfo GetOtherInfoByResumeId(int resumeId);

        //根据简历ID得到求职意向 三期 byniou UpdateResume/Index
        [OperationContract]
        SRJobTargetModel GetJobTargetByResumeId(int resumeId);

        JobhunterStandardResume GetPersonInfoByUserID(string userID);

        SRResumeVideo GetResumeVidelByUserIdAndResumeId(string userID, string resumeID);

        /// <summary>
        ///  获取申请职位发送的信息 
        /// add by chand 2015-04-09
        /// </summary>
        /// <param name="db"></param>
        /// <param name="resumeId"></param>
        /// <returns></returns>
        SendMailCompanyEntity GetInterviewJobAndApplyInfo(int companyinterviewjobid, string applyUserID, int ResumeId);


        /// <summary>
        /// 欧孚简历基本信息同步到智聘
        /// </summary>
        /// <param name="ciaDB"></param>
        /// <param name="SysPersonID"></param>
        /// <param name="ResumeIDList"></param>
        /// <returns></returns>
        int SyncResume(string SysPersonID, List<string> ResumeIDList);
    }
}
