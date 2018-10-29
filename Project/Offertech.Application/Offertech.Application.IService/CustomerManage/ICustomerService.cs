using Offertech.Application.Entity.CustomerManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace Offertech.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �����ˣ�chand
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public interface ICustomerService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetList();
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetMyList();
        /// <summary>
        /// ��ȡ�ͻ�
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetList(string userId);
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        DataTable GetMyData(string queryJson);
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        DataTable GetMyPageData(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<CustomerEntity> GetMyPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ��ǰ�û����¼��û����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetChildList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�������ݵ��б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        DataTable GetPageData(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// �����ͻ����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<CustomerEntity> GetPublicList(Pagination pagination, string queryJson);
        /// <summary>
        /// �ؼ��������ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="keyword">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<CustomerEntity> GetSearchList(string keyword);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        CustomerEntity GetEntity(string keyValue);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="fullName">����ֵ</param>
        /// <returns></returns>
        CustomerEntity GetEntityByName(string fullName);

        #region �ƶ���

        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerEntity> GetAppMyList(string userId);


        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        DataTable GetAppMyPageData(string userId, Pagination pagination, string queryJson);
        #endregion
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);


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
        void SaveForm(string keyValue, CustomerEntity entity);

        /// <summary>
        /// �����(����/�޸�)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleId">ģ��</param>
        void SaveForm(string keyValue, CustomerEntity entity, string moduleId);
        /// <summary>
        /// �޸Ŀͻ����õȼ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="RatingScore">�ͻ����õȼ�</param>
        void UpdateRatingScore(string keyValue, string RatingScore);
        /// <summary>
        /// �޸��û�״̬
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="State">״̬����-2��˲�ͨ���� -1����ˣ� 0 ���ͷŵĿͻ��� 1���� ��ˣ�2���ͨ����</param>
        void UpdateState(string keyValue, int State);
        /// <summary>
        /// �ͷſͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        void GetRelease(string keyValue);
        /// <summary>
        /// ��ȡ�ͻ�
        /// </summary>
        /// <param name="keyValue">����</param>
        void GetExtract(string keyValue);
        /// <summary>
        /// �޸Ŀͻ�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="FullName">�ͻ�����</param>
        void UpdateFullName(string keyValue, string FullName);
        #region �ƶ���
        /// <summary>
        /// �ƶ��������ͻ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="moduleId">ģ��</param>
        void AppInsert(CustomerEntity entity, string moduleId);
        /// <summary>
        /// �ƶ��˸��¿ͻ�
        /// </summary>
        /// <param name="entity"></param>
        void AppUpdate(CustomerEntity entity);
        #endregion 
        #endregion
    }
}
