using Offertech.Application.Code;
using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Application.IService.PublicInfoManage;
using Offertech.Data.Repository;
using Offertech.Util.Extension;
using Offertech.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Offertech.Application.Service.PublicInfoManage
{
    ///<summary>
    ///版本6.1
    ///苏州欧孚科技
    ///创建：chand
    ///日期：2016-12-2611:41
    ///描述：短信记录
    ///</summary>
    public class SmsLogService : RepositoryFactory<SmsLogEntity>, ISmsLogService
    {
        #region 获取数据
        ///<summary>
        ///获取列表
        ///</summary>
        ///<paramname="queryJson">查询参数</param>
        ///<returns>返回列表</returns>
        public IEnumerable<SmsLogEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        ///<summary>
        ///获取实体
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<returns></returns>
        public SmsLogEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        ///<summary>
        ///根据手机号码获取今日最后一条短信
        ///</summary>
        ///<paramname="MobileNumber">手机号码</param>
        ///<returns></returns>
        public SmsLogEntity GetTodayLast(string MobileNumber)
        {
            Pagination pagination = new Util.WebControl.Pagination() { rows = 1, page = 1, sidx = "SendTime", sord = "DESC" };
            var expression = LinqExtensions.True<SmsLogEntity>();
            var today = DateTime.Now.ToDate();
            expression = expression.And(t => t.MobileNumber == MobileNumber && t.SendTime > today && t.IsDeleted == false && t.SendResultCode == 0);
            return this.BaseRepository().FindList(expression, pagination).FirstOrDefault();
        }

        /// <summary>
        /// 获取最新短信
        /// </summary>
        /// <param name="MobileNumber">手机号码</param>
        /// <param name="second">秒内</param>
        /// <returns></returns>
        public SmsLogEntity GetLatestNews(string MobileNumber, int second)
        {
            Pagination pagination = new Util.WebControl.Pagination() { rows = 1, page = 1, sidx = "SendTime", sord = "DESC" };
            var expression = LinqExtensions.True<SmsLogEntity>();
            var today = DateTime.Now.AddSeconds(-second);
            expression = expression.And(t => t.MobileNumber == MobileNumber && t.SendTime > today && t.IsDeleted == false && t.SendResultCode == 0);
            return this.BaseRepository().FindList(expression, pagination).FirstOrDefault();
        }

        /// <summary>
        /// 根据当前用户获取最新短信
        /// </summary>
        /// <param name="MobileNumber">手机号码</param>
        /// <param name="second">秒内</param>
        /// <returns></returns>
        public SmsLogEntity GetLatestCurrent(string MobileNumber, int second)
        {
            Pagination pagination = new Util.WebControl.Pagination() { rows = 1, page = 1, sidx = "SendTime", sord = "DESC" };
            var expression = LinqExtensions.True<SmsLogEntity>();
            var today = DateTime.Now.AddSeconds(-second);
            string userId = OperatorProvider.Provider.Current().LoginInfo.UserId;
            expression = expression.And(t => t.MobileNumber == MobileNumber && t.SendTime > today && t.IsDeleted == false && t.SendResultCode == 0 && t.SendUserId == userId);
            return this.BaseRepository().FindList(expression, pagination).FirstOrDefault();
        }
        #endregion

        #region 提交数据
        ///<summary>
        ///删除数据
        ///</summary>
        ///<paramname="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        ///<summary>
        ///保存表单（新增、修改）
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<paramname="entity">实体对象</param>
        ///<returns></returns>
        public void SaveForm(string keyValue, SmsLogEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
