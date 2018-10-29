using CRM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CRM.IBLL
{
    /// <summary>
    /// Name:跟进任务接口
    /// Author：Jonny
    /// Date:2015-6-11
    /// </summary>
    [ServiceContract(Namespace = "www.Offer.com")]
    public partial interface ITBCustomerFollowBLL : IBaseDAL<TB_CustomerFollow>
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
        List<CustomerFollowModel> GetMyTask(string id, int page, int rows, string order, string sort, string search, ref int total);


        [OperationContract]
        List<CustomerFollowModel> GetCustomerFollowTask(string SysPersonID, int FollowType, string order, string sort, string search, int page, int rows, ref int total);


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
        List<CustomerFollowModel> GetCallHistroy(string id, int page, int rows, string order, string sort, string search, ref int total);
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
        List<CustomerFollowModel> GetMeetingHistroy(string id, int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        [OperationContract]
        IQueryable GetCombox(int type, string parm);
        /// <summary>
        /// 获取组员
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [OperationContract]
        List<SysPerson> GetTeamPersons(string parm);
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        [OperationContract]
        List<TB_CustomerContact> GetContactsInfo(string CustomerID);
        /// <summary>
        /// 获取主联系人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        [OperationContract]
        List<TB_Customer> GetCustomerInfo(string CustomerID);

        /// <summary>
        /// 填写跟进记录
        /// </summary>
        /// <param name="customerFollowID">任务ID</param>
        /// <param name="isLine">是否联系上</param>
        /// <param name="isNextType">跟进方式</param>
        /// <param name="customerPhone">联系人</param>
        /// <param name="customerLevel">对方级别</param>
        /// <param name="customerFeedback">客户反馈</param>
        /// <param name="customerFunnel">客户进程</param>
        /// <param name="customerOffer">商机判断</param>
        /// <param name="customerHandle">处理方式</param>
        /// <param name="callDate">下次跟进时间</param>
        /// <param name="remark">客户反馈的需求描述记录</param>
        /// <param name="userID">用户ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="cityName">城市名称</param>
        /// <param name="cityCode">城市Code</param>
        /// <param name="customerPurpose">跟进目的</param>
        /// <param name="addressDetails">详细地址</param>
        /// <param name="provinceName">省</param>
        /// <param name="provinceCode">省ID</param>
        /// <param name="districtName">区</param>
        /// <param name="districtCode">区ID</param>
        /// <param name="fileUrl">录音文件名</param>
        /// <returns></returns>
        [OperationContract]
        //bool AddFollowUpInfo(string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFeedback, string customerFunnel, string customerOffer, string customerHandle, string callDate, string remark, string userID, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode,string fileUrl);
        bool AddFollowUpInfo(string customerFollowID, string isLine, string isNextType, string customerPhone, string customerLevel, string customerFunnel, string customerOffer, string callDate, string remark, string userID, string customerID, string cityName, string cityCode, string customerPurpose, string addressDetails, string provinceName, string provinceCode, string districtName, string districtCode, string fileUrl);
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="cid">选择的ID</param>
        /// <param name="sortOrder">排序顺序</param>
        /// <param name="sortName">排序字段</param>
        /// <param name="search">条件</param>
        /// <param name="type">类型：0：我的任务；1：电话跟进记录；2：见面约谈记录</param>
        /// <returns></returns>
        [OperationContract]
        List<CustomerFollowModel> ExportInfo(string userID, string sortOrder, string sortName, string cid, string search, string type);

        /// <summary>
        /// 是否联系
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        [OperationContract]
        bool IsContact(string CustomerID, string CurrentPersonID, int FollowType);
        /// <summary>
        /// 获取跟进方式
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<SysField> GetFollowMode();


        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerFollowModel> GetFollowBackHistoryByCustomerID(string id, int FollowType, int page, int rows, string search, ref int total);

        /// <summary>
        /// 跟进历史
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<CustomerFollowModel> GetFollowBackHistory(int page, int rows, string search, ref int total);


        /// <summary>
        /// 最后一次跟进未反馈记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        TB_CustomerFollow GetLastCustomerFollow(string id);

        /// <summary>
        /// 获取客户最后一次联系人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ContactName"></param>
        /// <param name="ContactPhone"></param>
        TB_CustomerFollow GetLastContactInfo(string CustomerID, string SysPersonID);
    }
}
