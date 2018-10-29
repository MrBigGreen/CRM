using Offertech.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Offertech.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-04-19 15:42
    /// �� �������������λ��
    /// </summary>
    public class CompanyMap : EntityTypeConfiguration<CompanyEntity>
    {
        public CompanyMap()
        {
            #region ������
            //��
            this.ToTable("Base_Company");
            //����
            this.HasKey(t => t.CompanyId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
