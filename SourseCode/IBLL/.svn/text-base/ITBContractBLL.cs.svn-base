using Common;
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
    /// Name:合同列表接口
    /// Author：Jonny
    /// Date:2015-6-17
    /// </summary>
    [ServiceContract(Namespace = "www.Offer.com")]
    public partial interface ITBContractBLL
    {
        /// <summary>
        /// 获取组员
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [OperationContract]
        List<SysPerson> GetTeamPersons(string parm);
        /// <summary>
        /// 获取合同套餐
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [OperationContract]
        List<SysField> GetPackage(string parm);
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        [OperationContract]
        IQueryable GetCombox(int type, string parm);

        /// <summary>
        /// 合同签订保存
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="companyCode"></param>
        /// <param name="cityCode"></param>
        /// <param name="cityName"></param>
        /// <param name="packages"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="packageMoney"></param>
        /// <param name="uploadPackage"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddPackageInfo(string companyName, string companyCode, string cityCode, string cityName, string packages, string beginDate, string endDate, string packageMoney, string uploadPackage, string userID);
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
        List<ContractModel> GetByParam(string id, int page, int rows, string order, string sort, string search, ref int total);
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="sortOrder"></param>
        /// <param name="sortName"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        List<ContractModel> ExportInfo(string userID, string sortOrder, string sortName, string cid, string search);
        /// <summary>
        /// Check公司是否已经签合同
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckCompanyContract(string companyCode);

        bool Create(ref ValidationErrors validationErrors, TB_Contract entity);

        List<ContractEntity> GetContractData(string SearchPersonID, string id, string order, string sort, string search, int page, int rows,string contractType, ref int total);

        List<ContractEntity> GetContractByServiceData(string SearchPersonID, string id, string order, string sort, string search, int page, int rows, ref int total);


        bool GetMaxContractNOBySolutionID(ref ValidationErrors validationErrors, string SolutionID, ref string MaxContractNO);

        bool SqlCreate(ref ValidationErrors validationErrors, TB_Contract entity);

        TB_Contract GetById(string id);

        int GetContractNOCount(string ContractNO);
        /// <summary>
        /// 客服合同 分欧孚和博尔捷
        /// </summary>
        /// <param name="SearchPersonID"></param>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<ContractEntity> GetContractByServiceDataByType(string SearchPersonID, string id, string order, string sort, string search,string CompanyType, int page, int rows, ref int total);

        /// <summary>
        /// 根据合同ID，获取数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        ContractEntity GetEntity( string keyValue);
    }
}
