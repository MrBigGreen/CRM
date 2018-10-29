using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offertech.Util.SmsSevice;

namespace Offertech.Util
{
    public class SmsHelper
    {
        #region 短信平台JM
        /// <summary>
        /// 发送短信【JM短信平台】
        /// </summary>
        /// <param name="smsmodel">短信模板</param>
        /// <returns>
        ///0提交成功；  
        ///101 无此用户
        ///102 密码错
        ///103 提交过快（提交速度超过流速限制）
        ///104 系统忙（因平台侧原因，暂时无法处理提交的短信）
        ///105 敏感短信（短信内容包含敏感词）
        ///106 消息长度错（>536或<=0）
        ///107 包含错误的手机号码
        ///108 手机号码个数错（群发>50000或<=0;单发>200或<=0）
        ///109 无发送额度（该用户可用短信数已使用完）
        ///110 不在发送时间内
        ///111 超出该账户当月发送额度限制
        ///112 无此产品，用户没有订购该产品
        ///113 extno格式错（非数字或者长度不对）
        ///115 自动审核驳回
        ///116 签名不合法，未带签名（用户必须带签名的前提下）
        ///117 IP地址认证错,请求调用的IP地址不是系统登记的IP地址
        ///118 用户没有相应的发送权限
        ///119 用户已过期
        ///  </returns>
        public static string SendSmsByJM(SmsModel smsmodel)
        {
            try
            {
                string _needstatus = string.IsNullOrEmpty(smsmodel.needstatus) ? "false" : smsmodel.needstatus;
                string _product = string.IsNullOrEmpty(smsmodel.product) ? "" : ("&product=" + smsmodel.product);
                string param = "account=" + smsmodel.account + "&pswd=" + smsmodel.pswd + "&mobile=" + smsmodel.mobile + "&msg=" + smsmodel.msg + "&needstatus=" + _needstatus + _product;
                string res = HttpMethods.HttpPost(smsmodel.url, param);
                return res;
                //return res.Split(',')[1].Split('\n')[0];
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 短信平台亿美软通



        public static SDKService sc = new SDKService();
        
        /// <summary>
        /// 发送短信【JM短信平台】
        /// </summary>
        /// <param name="smsmodel">短信模板</param>
        /// <returns>
        ///  </returns>
        public static SmsResult SendSmsByYM(SmsModel smsmodel)
        {
            int result = 0;
            try
            {
                result = sc.sendSMS(smsmodel.account, smsmodel.pswd, string.Empty, new string[] { smsmodel.mobile }, smsmodel.msg,
    string.Empty, "GBK", 5, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            }
            catch (Exception ex)
            {
                result = 99;
            }
            return new SmsResult
            {
                code = result,
                msg = Enum.GetName(typeof(SendSmsResult), result)
            };

        }
        #endregion
    }

    #region 数据传输模型
    /// <summary>
    /// 短息发送接口模型
    /// </summary>
    public class SmsModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pswd { get; set; }
        /// <summary>
        /// 短信接口地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 是否需要状态报告，默认为false
        /// </summary>
        public string needstatus { get; set; }
        /// <summary>
        /// 产品id（可不填）
        /// </summary>
        public string product { get; set; }

    }
    #endregion

    #region 亿美软通



    public enum SendSmsResult
    {
        短信接口错误 = 99,
        成功 = 0,
        系统异常 = 1,
        客户端异常 = -2,
        命令不被支持 = -101,
        RegistryTransInfo删除信息失败 = -102,
        RegistryInfo更新信息失败 = -103,
        请求超过限制 = -104,
        号码注册激活失败 = -110,
        企业注册失败 = -111,
        充值失败 = -113,
        发送短信失败 = -117,
        接收MO失败 = -118,
        接收Report失败 = -119,
        修改密码失败 = -120,
        号码注销激活失败 = -122,
        查询单价失败 = -123,
        查询余额失败 = -124,
        设置MO转发失败 = -125,
        路由信息失败 = -126,
        计费失败0余额 = -127,
        计费失败余额不足 = -128,
        数据操作失败 = -190,
        序列号错误, 序列号不存在内存中, 或尝试攻击的用户 = -1100,
        序列号密码错误 = -1102,
        序列号Key错误 = -1103,
        路由失败请联系系统管理员 = -1104,
        注册号状态异常未用1 = -1105,
        注册号状态异常停用3 = -1107,
        注册号状态异常停止5 = -1108,
        充值卡无效 = -1131,
        充值密码无效 = -1132,
        充值卡绑定异常 = -1133,
        充值状态无效 = -1134,
        充值金额无效 = -1135,
        数据库插入操作失败 = -1901,
        数据库更新操作失败 = -1902,
        数据库删除操作失败 = -1903,
        数据格式错误, 数据超出数据库允许范围 = -9000,
        序列号格式错误 = -9001,
        密码格式错误 = -9002,
        客户端Key格式错误 = -9003,
        设置转发格式错误 = -9004,
        公司地址格式错误 = -9005,
        企业中文名格式错误 = -9006,
        企业中文名简称格式错误 = -9007,
        邮件地址格式错误 = -9008,
        企业英文名格式错误 = -9009,
        企业英文名简称格式错误 = -9010,
        传真格式错误 = -9011,
        联系人格式错误 = -9012,
        联系电话 = -9013,
        邮编格式错误 = -9014,
        新密码格式错误 = -9015,
        发送短信包大小超出范围 = -9016,
        发送短信内容格式错误 = -9017,
        发送短信扩展号格式错误 = -9018,
        发送短信优先级格式错误 = -9019,
        发送短信手机号格式错误 = -9020,
        发送短信定时时间格式错误 = -9021,
        发送短信唯一序列值错误 = -9022,
        充值卡号格式错误 = -9023,
        充值密码格式错误 = -9024,
        客户端请求sdk5超时 = -9025


    }

    public class SmsResult
    {
        public int code { get; set; }
        public string msg { get; set; }
    }
    #endregion
}
