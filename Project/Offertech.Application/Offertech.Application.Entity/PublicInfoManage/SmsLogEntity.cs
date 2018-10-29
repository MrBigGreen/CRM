using System;
using Offertech.Application.Code;

namespace Offertech.Application.Entity.PublicInfoManage
{
    ///<summary>
    ///�汾
    ///����ŷ�ڿƼ�
    ///������chand
    ///���ڣ�2016-12-2611:41
    ///���������ż�¼
    ///</summary>
    public class SmsLogEntity : BaseEntity
    {
        #region ʵ���Ա
        ///<summary>
        ///����
        ///</summary>
        ///<returns></returns>
        public Guid MsgId { get; set; }
        ///<summary>
        ///�ֻ���
        ///</summary>
        ///<returns></returns>
        public string MobileNumber { get; set; }
        ///<summary>
        ///��������
        ///</summary>
        ///<returns></returns>
        public string MsgContent { get; set; }
        ///<summary>
        ///����ֵ������֤��
        ///</summary>
        ///<returns></returns>
        public string MsgValue { get; set; }
        ///<summary>
        ///���Ͷ��Ž������
        ///</summary>
        ///<returns></returns>
        public int? SendResultCode { get; set; }
        ///<summary>
        ///���Ͷ��Ž������
        ///</summary>
        ///<returns></returns>
        public string SendResultMsg { get; set; }
        ///<summary>
        ///����ʱ��
        ///</summary>
        ///<returns></returns>
        public DateTime SendTime { get; set; }
        ///<summary>
        ///�Ƿ�ɾ��
        ///</summary>
        ///<returns></returns>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        public string SendUserId { get; set; }
        #endregion

        #region ��չ����
        ///<summary>
        ///��������
        ///</summary>
        public override void Create()
        {
            this.MsgId = Guid.NewGuid();
            this.SendUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
        }
        ///<summary>
        ///�༭����
        ///</summary>
        ///<paramname="keyValue"></param>
        public override void Modify(string keyValue)
        {
            //this.MsgId = keyValue;
        }
        #endregion
    }
}