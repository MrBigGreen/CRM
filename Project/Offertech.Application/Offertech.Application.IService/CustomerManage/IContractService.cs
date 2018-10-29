using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.Entity.CustomerManage.ViewModel;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace Offertech.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 13:51
    /// �� ������ͬ����
    /// </summary>
    public interface IContractService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<ContractEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<ContractEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡ��ͬ��������
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<ContractEntity> GetDeadlineList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        ContractEntity GetEntity(string keyValue);
        /// <summary>
        /// ��ȡŷ�����ۺ�ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetOfferDataBySale(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ���������ۺ�ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetBridgeDataBySale(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡŷ�ڿͷ���ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetOfferDataByKeFu(Pagination pagination, string queryJson);

        /// <summary>
        /// ��ȡ���������ۺ�ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetBridgeDataByKeFu(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ��ͬ�����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetPageData(Pagination pagination, string queryJson);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ContractModel entity);
        /// <summary>
        /// ������ͬ
        /// </summary>
        /// <param name="entity">��ͬ�������</param>
        /// <param name="serviceEntityList">��������</param>
        void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList);
        /// <summary>
        /// ������ͬ
        /// </summary>
        /// <param name="entity">��ͬ�������</param>
        /// <param name="serviceEntityList">��������</param>
        /// <param name="moduleId">ģ��ID</param>
        /// <param name="moduleCode">ģ�����</param>
        void AddForm(ContractEntity entity, List<ContractServiceEntity> serviceEntityList, string moduleId, string moduleCode);
        #endregion
    }
}
