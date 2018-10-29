using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 13:52
    /// �� ������ͬ��������
    /// </summary>
    public class ContractServiceMap : EntityTypeConfiguration<ContractServiceEntity>
    {
        public ContractServiceMap()
        {
            #region ������
            //��
            this.ToTable("Client_ContractService");
            //����
            this.HasKey(t => t.ContractServiceId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
