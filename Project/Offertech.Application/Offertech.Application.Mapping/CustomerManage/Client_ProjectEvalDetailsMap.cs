using Offertech.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-03-09 14:45
    /// �� ������Ŀ��
    /// </summary>
    public class Client_ProjectEvalDetailsMap : EntityTypeConfiguration<Client_ProjectEvalDetailsEntity>
    {
        public Client_ProjectEvalDetailsMap()
        {
            #region ������
            //��
            this.ToTable("Client_ProjectEvalDetails");
            //����
            this.HasKey(t => t.ProjectEvalDetailsId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
