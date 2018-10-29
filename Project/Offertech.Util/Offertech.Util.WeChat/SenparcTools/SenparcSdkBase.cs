using System;
using System.Configuration;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.Helpers;
using Senparc.Weixin.QY.Containers;
using Offertech.Cache.Factory;

namespace Offertech.Util.WeChat.SenparcTools
{
    public class SenparcSdkBase
    {
        public static string CORPID = Util.Config.GetValue("CorpId"); //企业ID
        private static string _CorpSecret = Util.Config.GetValue("CorpSecret");//管理员组ID
        private static string _JsCorpSecret = Util.Config.GetValue("JsCorpSecret");//管理员组ID

        public static string GetToken()
        {
            if (!AccessTokenContainer.CheckRegistered(CORPID))
            {
                AccessTokenContainer.Register(CORPID, _CorpSecret);
            }
            return AccessTokenContainer.GetToken(CORPID, _CorpSecret);
        }

        public static JsSdkUiPackage GetJsSdkUiPackage(string url)
        {
            long timestamp = JSSDKHelper.GetTimestamp();
            string nonceStr = JSSDKHelper.GetNoncestr();

            JsApiTicketResult ticket = new JsApiTicketResult();
            if (CacheFactory.Cache().GetCache<JsApiTicketResult>("jsapi_ticket") == null)
            {
                ticket = CommonApi.GetTicket(CORPID, _JsCorpSecret);
                CacheFactory.Cache().WriteCache(ticket, "jsapi_ticket", System.DateTime.Now.AddSeconds(3600));
            }
            else
            {
                ticket = CacheFactory.Cache().GetCache<JsApiTicketResult>("jsapi_ticket");
            }
            string signature = JSSDKHelper.GetSignature(ticket.ticket, nonceStr, timestamp, url);
            JsSdkUiPackage result = new JsSdkUiPackage(CORPID, timestamp.ToString(), nonceStr, signature);
            return result;
        }
    }
}