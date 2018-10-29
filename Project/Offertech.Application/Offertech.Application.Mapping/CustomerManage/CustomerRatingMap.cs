using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 13:55
    /// �� �����ͻ��ȼ����۱�
    /// </summary>
    public class CustomerRatingMap : EntityTypeConfiguration<CustomerRatingEntity>
    {
        public CustomerRatingMap()
        {
            #region ������
            //��
            this.ToTable("Client_CustomerRating");
            //����
            this.HasKey(t => t.CustomerRatingId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
