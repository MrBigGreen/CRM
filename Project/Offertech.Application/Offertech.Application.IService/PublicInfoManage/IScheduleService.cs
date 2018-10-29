using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;

namespace Offertech.Application.IService.PublicInfoManage
{
    /// <summary>
    /// �� �� 6.1
    /// ����ŷ�ڿƼ�
    /// �����ˣ�chand
    /// �� �ڣ�2016-04-25 10:45
    /// �� �����ճ̹���
    /// </summary>
    public interface IScheduleService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<ScheduleEntity> GetList(string queryJson);
        /// <summary>
        /// ��ȡ�Լ��б�
        /// </summary>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>�����б�</returns>
        IEnumerable<ScheduleEntity> GetMyList(string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        ScheduleEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, ScheduleEntity entity);
        #endregion
    }
}
