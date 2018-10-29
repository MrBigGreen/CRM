using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.IService.PublicInfoManage;
using Offertech.Application.Service.PublicInfoManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;
using System;
using Offertech.Util;

namespace Offertech.Application.Busines.PublicInfoManage
{
    ///<summary>
    ///版本6.1
    ///苏州欧孚科技
    ///创建：chand
    ///日期：2016-12-2611:41
    ///描述：短信记录
    ///</summary>
    public class SmsLogBLL
    {
        private ISmsLogService service = new SmsLogService();

        #region 获取数据
        ///<summary>
        ///获取列表
        ///</summary>
        ///<paramname="queryJson">查询参数</param>
        ///<returns>返回列表</returns>
        public IEnumerable<SmsLogEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        ///<summary>
        ///获取实体
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<returns></returns>
        public SmsLogEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        ///<summary>
        ///根据手机号码获取今日最后一条短信
        ///</summary>
        ///<paramname="MobileNumber">手机号码</param>
        ///<returns></returns>
        public SmsLogEntity GetTodayLast(string MobileNumber)
        {
            return service.GetTodayLast(MobileNumber);
        }
        /// <summary>
        /// 获取最新短信
        /// </summary>
        /// <param name="MobileNumber">手机号码</param>
        /// <param name="second">秒内</param>
        /// <returns></returns>
        public SmsLogEntity GetLatestNews(string MobileNumber, int second)
        {
            return service.GetLatestNews(MobileNumber, second);
        }

        /// <summary>
        /// 根据当前用户获取最新短信
        /// </summary>
        /// <param name="MobileNumber">手机号码</param>
        /// <param name="second">秒内</param>
        /// <returns></returns>
        public SmsLogEntity GetLatestCurrent(string MobileNumber, int second)
        {
            return service.GetLatestCurrent(MobileNumber, second);
        }
        #endregion

        #region 提交数据
        ///<summary>
        ///删除数据
        ///</summary>
        ///<paramname="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        ///<summary>
        ///保存表单（新增、修改）
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<paramname="entity">实体对象</param>
        ///<returns></returns>
        public void SaveForm(string keyValue, SmsLogEntity entity)
        {

            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        ///<summary>
        ///短信保存并发送
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<paramname="entity">实体对象</param>
        ///<returns></returns>
        public void SaveBySend(SmsLogEntity entity)
        {
            try
            {
                SmsModel smsModel = new SmsModel();
                smsModel.account = Config.GetValue("SMSAccount");
                smsModel.pswd = Config.GetValue("SMSPswd");
                smsModel.url = Config.GetValue("SMSUrl");
                smsModel.mobile = entity.MobileNumber;
                smsModel.msg = Config.GetValue("SMSPrefix") + entity.MsgContent;
                var result = SmsHelper.SendSmsByYM(smsModel);
                entity.SendResultCode = result.code;
                entity.SendResultMsg = result.msg;
                entity.IsDeleted = false;
                entity.SendTime = DateTime.Now;
                service.SaveForm(string.Empty, entity);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveSmsLog(SmsLogEntity entity)
        {

            try
            {

                entity.SendResultCode = 0;
                entity.SendResultMsg = "成功";
                entity.IsDeleted = false;
                entity.SendTime = DateTime.Now;
                service.SaveForm(string.Empty, entity);

            }
            catch (Exception)
            {
                throw;
            }
        }
        ///<summary>
        ///短信保存并发送
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<paramname="entity">实体对象</param>
        ///<returns></returns>
        public void SaveBySend(string MobileNumber, string MsgContent, string MsgValue)
        {
            try
            {
                SmsLogEntity entity = new SmsLogEntity();
                entity.MobileNumber = MobileNumber;
                entity.MsgContent = MsgContent;
                entity.MsgValue = MsgValue;

                SmsModel smsModel = new SmsModel();
                smsModel.account = Config.GetValue("SMSAccount");
                smsModel.pswd = Config.GetValue("SMSPswd");
                smsModel.url = Config.GetValue("SMSUrl");
                smsModel.mobile = entity.MobileNumber;
                smsModel.msg = Config.GetValue("SMSPrefix") + entity.MsgContent;
                var result = SmsHelper.SendSmsByYM(smsModel);
                entity.SendResultCode = result.code;
                entity.SendResultMsg = result.msg;
                entity.IsDeleted = false;
                entity.SendTime = DateTime.Now;
                service.SaveForm(string.Empty, entity);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
