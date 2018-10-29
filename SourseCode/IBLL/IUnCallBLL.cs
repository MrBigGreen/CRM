using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CRM.DAL;

namespace CRM.IBLL
{
    public partial interface IUnCallBLL
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
        List<CallRecordingModel> GetCallRecordingData(string id, int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        IQueryable GetCombox(string type);

        /// <summary>
        /// 获取批注
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [OperationContract]
        List<SoundCommentModel> GetComment(string cid);
        /// <summary>
        /// 保存批注
        /// </summary>
        /// <param name="id"></param>
        /// <param name="callProcess"></param>
        /// <param name="goodRemark"></param>
        /// <param name="badRemark"></param>
        /// <param name="improveRemark"></param>
        /// <param name="cid"></param>
        /// <param name="url"></param>
        /// <param name="fcode"></param>
        /// <param name="isupdate"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddCommentInfo(string id, string callProcess, string goodRemark, string badRemark, string improveRemark, string cid, string url, string fcode, string isupdate);

        /// <summary>
        /// 获取登录用户的权限（leader Or Sales）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        int GetUserRole(string id);
        /// <summary>
        /// 获取报表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        [OperationContract]
        List<CallRecordingModel> GetReportsData(string id, int page, int rows, string order, string sort, string search, ref int total);

        [OperationContract]
        List<CallRecordingModel> GetReportsExcel(string id, string search);

        [OperationContract]
        List<string> GetExportSounds(string search);
    }
}
