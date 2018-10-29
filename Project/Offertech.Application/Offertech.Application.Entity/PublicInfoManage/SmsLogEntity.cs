using System;
using Offertech.Application.Code;

namespace Offertech.Application.Entity.PublicInfoManage
{
    ///<summary>
    ///版本
    ///苏州欧孚科技
    ///创建：chand
    ///日期：2016-12-2611:41
    ///描述：短信记录
    ///</summary>
    public class SmsLogEntity : BaseEntity
    {
        #region 实体成员
        ///<summary>
        ///主键
        ///</summary>
        ///<returns></returns>
        public Guid MsgId { get; set; }
        ///<summary>
        ///手机号
        ///</summary>
        ///<returns></returns>
        public string MobileNumber { get; set; }
        ///<summary>
        ///短信内容
        ///</summary>
        ///<returns></returns>
        public string MsgContent { get; set; }
        ///<summary>
        ///短信值，如验证码
        ///</summary>
        ///<returns></returns>
        public string MsgValue { get; set; }
        ///<summary>
        ///发送短信结果编码
        ///</summary>
        ///<returns></returns>
        public int? SendResultCode { get; set; }
        ///<summary>
        ///发送短信结果内容
        ///</summary>
        ///<returns></returns>
        public string SendResultMsg { get; set; }
        ///<summary>
        ///发送时间
        ///</summary>
        ///<returns></returns>
        public DateTime SendTime { get; set; }
        ///<summary>
        ///是否删除
        ///</summary>
        ///<returns></returns>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 发送用户主键
        /// </summary>
        public string SendUserId { get; set; }
        #endregion

        #region 扩展操作
        ///<summary>
        ///新增调用
        ///</summary>
        public override void Create()
        {
            this.MsgId = Guid.NewGuid();
            this.SendUserId = OperatorProvider.Provider.Current().LoginInfo.UserId;
        }
        ///<summary>
        ///编辑调用
        ///</summary>
        ///<paramname="keyValue"></param>
        public override void Modify(string keyValue)
        {
            //this.MsgId = keyValue;
        }
        #endregion
    }
}