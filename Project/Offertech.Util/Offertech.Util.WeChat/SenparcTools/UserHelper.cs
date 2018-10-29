using System;
using System.Collections.Generic;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities;

namespace Offertech.Util.WeChat.SenparcTools
{
    public class UserHelper : SenparcSdkBase
    {
        /// <summary>
        /// UserId转OpenID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string ConvertToOpenId(string userId, string agentId = null)
        {
            var result = CommonApi.ConvertToOpenId(GetToken(), userId, agentId);
            return result == null ? "" : result.openid;
        }


        public static string GetAppIdByConvertToOpenId(string userId, string agentId = null)
        {
            var result = CommonApi.ConvertToOpenId(GetToken(), userId, agentId);
            return result == null ? "" : result.appid;
        }

        public static bool AddWxUser(ref string erromsg, string userId, string name, int[] department = null, string position = null, string mobile = null, string email = null, string weixinId = null)
        {
            try
            {
                var result = MailListApi.CreateMember(GetToken(), userId, name, department, position, mobile, email, weixinId);
                erromsg = result.errmsg;
            }
            catch (ErrorJsonResultException ex)
            {
                erromsg = WxReturnCodeMessage.GetReturnMessage((int)ex.JsonResult.errcode);
                return false;
            }

            return true;
        }

        public static bool UpdateWxUser(ref string erromsg, string userId, string name, int[] department = null, string position = null, string mobile = null, string email = null, string weixinId = null)
        {
            try
            {
                var result = MailListApi.UpdateMember(GetToken(), userId, name, department, position, mobile, email, weixinId);
                erromsg = result.errmsg;
            }
            catch (ErrorJsonResultException ex)
            {
                erromsg = WxReturnCodeMessage.GetReturnMessage((int)ex.JsonResult.errcode);
                return false;
            }

            return true;
        }

        public static bool DeleteMember(string userId, ref string erromsg)
        {
            try
            {
                var result = MailListApi.DeleteMember(GetToken(), userId);
            }
            catch (ErrorJsonResultException ex)
            {
                erromsg = WxReturnCodeMessage.GetReturnMessage((int)ex.JsonResult.errcode);
                return false;
            }

            return true;
        }

        public static bool BatchDeleteMember(string[] userIds, ref string erromsg)
        {
            try
            {
                var result = MailListApi.BatchDeleteMember(GetToken(), userIds);
            }
            catch (ErrorJsonResultException ex)
            {
                erromsg = WxReturnCodeMessage.GetReturnMessage((int)ex.JsonResult.errcode);
                return false;
            }

            return true;
        }


        public static GetMemberResult GetMember(string userId, ref string erromsg)
        {
            GetMemberResult result = new GetMemberResult();
            try
            {
                result = MailListApi.GetMember(GetToken(), userId);
            }
            catch (ErrorJsonResultException ex)
            {
                erromsg = WxReturnCodeMessage.GetReturnMessage((int)ex.JsonResult.errcode);
            }
            return result;
        }
    }
}