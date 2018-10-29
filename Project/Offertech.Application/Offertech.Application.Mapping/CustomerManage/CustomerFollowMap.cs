using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-04-05 12:48
    /// �� �����ͻ���������
    /// </summary>
    public class CustomerFollowMap : EntityTypeConfiguration<CustomerFollowEntity>
    {
        public CustomerFollowMap()
        {
            #region ������
            //��
            this.ToTable("Client_CustomerFollow");
            //����
            this.HasKey(t => t.CustomerFollowId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
