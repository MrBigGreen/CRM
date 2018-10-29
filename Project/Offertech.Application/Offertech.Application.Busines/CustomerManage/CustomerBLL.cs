using Offertech.Application.Entity.CustomerManage;
using Offertech.Application.IService.CustomerManage;
using Offertech.Application.Service.CustomerManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace Offertech.Application.Busines.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �����ˣ�chand
    /// �� �ڣ�2016-03-14 09:47
    /// �� �����ͻ���Ϣ
    /// </summary>
    public class CustomerBLL
    {
        private ICustomerService service = new CustomerService();
        /// <summary>
        /// ����key
        /// </summary>
        public string cacheKey = "CustomerCache";

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetMyList()
        {
            return service.GetMyList();
        }

        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public DataTable GetMyData(string queryJson)
        {
            return service.GetMyData(queryJson);
        }
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public DataTable GetMyPageData(Pagination pagination, string queryJson)
        {
            return service.GetMyPageData(pagination, queryJson);
        }

        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetMyPageList(Pagination pagination, string queryJson)
        {
            return service.GetMyPageList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ��ǰ�û����¼��û����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public DataTable GetChildList(Pagination pagination, string queryJson)
        {
            return service.GetChildList(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�������ݵ��б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public DataTable GetPageData(Pagination pagination, string queryJson)
        {
            return service.GetPageData(pagination, queryJson);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// �����ͻ����б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetPublicList(Pagination pagination, string queryJson)
        {
            return service.GetPublicList(pagination, queryJson);
        }
        /// <summary>
        /// �ؼ��������ͻ��б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="keyword">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        public IEnumerable<CustomerEntity> GetSearchList(string keyword)
        {
            return service.GetSearchList(keyword);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        public CustomerEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="fullName">����ֵ</param>
        /// <returns></returns>
        public CustomerEntity GetEntityByName(string fullName)
        {
            return service.GetEntityByName(fullName);
        }

        #region �ƶ���
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerEntity> GetAppMyList(string userId)
        {
            return service.GetAppMyList(userId);
        }
        /// <summary>
        /// ��ȡ��ǰ�û��Ŀͻ��б�
        /// </summary>
        /// <returns></returns>
        public DataTable GetAppMyPageData(string userId, Pagination pagination, string queryJson)
        {
            return service.GetAppMyPageData(userId, pagination, queryJson);
        }
        #endregion
        #endregion

        #region ��֤����
        /// <summary>
        /// �ͻ����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CustomerEntity entity)
        {
            if (entity.FullName.Length <= 4)
            {
                throw new Exception("�ͻ����Ʋ�����");
            }
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
            if (!reg.IsMatch(entity.FullName))
            {
                throw new Exception("�ͻ�����ֻ�ܰ������ֺ�����");
            }
            try
            {
                service.SaveForm(keyValue, entity);
                //������������
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    CustomerLuceneNet.GetInstance().AddCustomer(entity.CustomerId);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity">ʵ��</param>
        /// <param name="moduleId">ģ��</param>
        public void SaveForm(string keyValue, CustomerEntity entity, string moduleId)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                if (entity.FullName.Length <= 4)
                {
                    throw new Exception("�ͻ����Ʋ�����");
                }
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
                if (!reg.IsMatch(entity.FullName))
                {
                    throw new Exception("�ͻ�����ֻ�ܰ������ֺ�����");
                }
                if (!ExistFullName(entity.FullName, keyValue))
                {
                    throw new Exception("�ͻ���Ϣ�Ѵ���");
                }

            }

            try
            {
                service.SaveForm(keyValue, entity, moduleId);
                //������������
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    CustomerLuceneNet.GetInstance().AddCustomer(entity.CustomerId);
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity">ʵ��</param>
        /// <param name="moduleId">ģ��</param>
        public void SaveImportForm(string keyValue, CustomerEntity entity, string moduleId)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                if (entity.FullName.Length <= 3)
                {
                    throw new Exception("�ͻ����Ʋ�����");
                }
                //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
                //if (!reg.IsMatch(entity.FullName))
                //{
                //    throw new Exception("�ͻ�����ֻ�ܰ������ֺ�����");
                //}
                if (!ExistFullName(entity.FullName, keyValue))
                {
                    throw new Exception("�ͻ���Ϣ�Ѵ���");
                }

            }

            try
            {
                service.SaveForm(keyValue, entity, moduleId);
                //������������
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    CustomerLuceneNet.GetInstance().AddCustomer(entity.CustomerId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// �޸Ŀͻ����õȼ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="RatingScore">�ͻ����õȼ�</param>
        public void UpdateRatingScore(string keyValue, string RatingScore)
        {
            try
            {
                service.UpdateRatingScore(keyValue, RatingScore);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// �޸Ŀͻ�״̬
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="State">״̬����-2��˲�ͨ���� -1����ˣ� 0 ���ͷŵĿͻ��� 1���� ��ˣ�2���ͨ����</param>
        public void UpdateState(string keyValue, int State)
        {
            try
            {
                service.UpdateState(keyValue, State);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// �ͷſͻ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        public void GetRelease(string keyValue)
        {
            try
            {
                service.GetRelease(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// ��ȡ�ͻ�
        /// </summary>
        /// <param name="keyValue">����</param>
        public void GetExtract(string keyValue)
        {
            try
            {
                service.GetExtract(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// �޸Ŀͻ�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="FullName">�ͻ�����</param>
        public void UpdateFullName(string keyValue, string FullName)
        {
            try
            {
                service.UpdateFullName(keyValue, FullName);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #region �ƶ���
        /// <summary>
        /// �ƶ��������ͻ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="moduleId">ģ��</param>
        public void AppInsert(CustomerEntity entity, string moduleId)
        {
            if (entity.FullName.Length <= 4)
            {
                throw new Exception("�ͻ����Ʋ�����");
            }
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
            if (!reg.IsMatch(entity.FullName))
            {
                throw new Exception("�ͻ�����ֻ�ܰ������ֺ�����");
            }
            try
            {
                service.AppInsert(entity, moduleId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// �ƶ��˸��¿ͻ�
        /// </summary>
        /// <param name="entity"></param>
        public void AppUpdate(CustomerEntity entity)
        {
            if (entity.FullName.Length <= 4)
            {
                throw new Exception("�ͻ����Ʋ�����");
            }
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^([\u4e00-\u9fa5]*[(��]?[\u4e00-\u9fa5]+[)��]?[\u4e00-\u9fa5]*)$");
            if (!reg.IsMatch(entity.FullName))
            {
                throw new Exception("�ͻ�����ֻ�ܰ������ֺ�����");
            }
            try
            {
                service.AppUpdate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion 
        #endregion

        #region ������ҵ���õȼ�



        /// <summary>
        /// ���ݷ�����ȡ�ȼ�
        /// </summary>
        /// <param name="ratingScore"></param>
        /// <returns></returns>
        public string GetCalcRatingBefore(int ratingScore)
        {

            if (ratingScore >= 90)
            {
                return "A";
            }
            else if (ratingScore >= 80)
            {
                return "B";
            }
            else if (ratingScore >= 70)
            {
                return "C";
            }
            else if (ratingScore >= 60)
            {
                return "D";
            }
            else
            {
                return "E";
            }
        }
        #endregion
    }

}
