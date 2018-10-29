using Offertech.Application.Entity.PublicInfoManage;
using Offertech.Util.WebControl;
using System.Collections.Generic;

namespace Offertech.Application.IService.PublicInfoManage
{
    ///<summary>
    ///版本6.1
    ///苏州欧孚科技
    ///创建：chand
    ///日期：2016-12-2611:41
    ///描述：短信记录
    ///</summary>
    public interface ISmsLogService
    {
        #region 获取数据
        ///<summary>
        ///获取列表
        ///</summary>
        ///<paramname="queryJson">查询参数</param>
        ///<returns>返回列表</returns>
        IEnumerable<SmsLogEntity> GetList(string queryJson);
        ///<summary>
        ///获取实体
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<returns></returns>
        SmsLogEntity GetEntity(string keyValue);
        ///<summary>
        ///根据手机号码获取今日最后一条短信
        ///</summary>
        ///<paramname="MobileNumber">手机号码</param>
        ///<returns></returns>
        SmsLogEntity GetTodayLast(string MobileNumber);
        /// <summary>
        /// 获取最新短信
        /// </summary>
        /// <param name="MobileNumber">手机号码</param>
        /// <param name="second">秒内</param>
        /// <returns></returns>
        SmsLogEntity GetLatestNews(string MobileNumber, int second);
        /// <summary>
        /// 根据当前用户获取最新短信
        /// </summary>
        /// <param name="MobileNumber">手机号码</param>
        /// <param name="second">秒内</param>
        /// <returns></returns>
        SmsLogEntity GetLatestCurrent(string MobileNumber, int second);
        #endregion

        #region 提交数据
        ///<summary>
        ///删除数据
        ///</summary>
        ///<paramname="keyValue">主键</param>
        void RemoveForm(string keyValue);
        ///<summary>
        ///保存表单（新增、修改）
        ///</summary>
        ///<paramname="keyValue">主键值</param>
        ///<paramname="entity">实体对象</param>
        ///<returns></returns>
        void SaveForm(string keyValue, SmsLogEntity entity);
        #endregion
    }
}
