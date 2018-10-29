using System;
using Offertech.Application.Code;

namespace Offertech.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// ����ŷ�ڿƼ�
    /// �� ������������Ա
    /// �� �ڣ�2017-04-19 15:42
    /// �� �������������λ��
    /// </summary>
    public class CompanyEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string CompanyId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public int? Category { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string ParentId { get; set; }
        /// <summary>
        /// �����ʺ�
        /// </summary>
        /// <returns></returns>
        public string EnCode { get; set; }
        /// <summary>
        /// �˻�����
        /// </summary>
        /// <returns></returns>
        public string ShortName { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        public string Nature { get; set; }
        /// <summary>
        /// ���ߵ绰
        /// </summary>
        /// <returns></returns>
        public string OuterPhone { get; set; }
        /// <summary>
        /// ���ߵ绰
        /// </summary>
        /// <returns></returns>
        public string InnerPhone { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Fax { get; set; }
        /// <summary>
        /// �ʺŷ������
        /// </summary>
        /// <returns></returns>
        public string Postalcode { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        public string ManagerId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string Manager { get; set; }
        /// <summary>
        /// ʡ����
        /// </summary>
        /// <returns></returns>
        public string ProvinceId { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string CityId { get; set; }
        /// <summary>
        /// ��/������
        /// </summary>
        /// <returns></returns>
        public string CountyId { get; set; }
        /// <summary>
        /// ��ϸ��ַ
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        public string WebAddress { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? FoundedTime { get; set; }
        /// <summary>
        /// ��Ӫ��Χ
        /// </summary>
        /// <returns></returns>
        public string Businessscope { get; set; }
        /// <summary>
        /// ��
        /// </summary>
        /// <returns></returns>
        public int? Layer { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// ��Ч��־
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CompanyId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CompanyId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().LoginInfo.UserName;
        }
        #endregion
    }
}