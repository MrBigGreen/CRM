using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.OAuth2;

namespace Offertech.Util.WeChat.SenparcTools
{
    public class OAuth2ApiHelper : SenparcSdkBase
    {
        public static string GetCode(string returnUrl)
        {
            return OAuth2Api.GetCode(CORPID, returnUrl, "STATE");
        }

        public static GetUserInfoResult GetUserId(string code)
        {
            return OAuth2Api.GetUserId(GetToken(), code);
        }
    }
}
