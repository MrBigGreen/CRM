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
    public class ProjectMap : EntityTypeConfiguration<ProjectEntity>
    {
        public ProjectMap()
        {
            #region ������
            //��
            this.ToTable("Client_Project");
            //����
            this.HasKey(t => t.ProjectId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
