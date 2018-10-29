using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 13:53
    /// �� �����ͻ������¼
    /// </summary>
    public class OwnershipChangeMap : EntityTypeConfiguration<OwnershipChangeEntity>
    {
        public OwnershipChangeMap()
        {
            #region ������
            //��
            this.ToTable("Client_OwnershipChange");
            //����
            this.HasKey(t => t.OwnershipChangeId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
