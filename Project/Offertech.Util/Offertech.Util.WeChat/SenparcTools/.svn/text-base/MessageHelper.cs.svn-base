using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.Mass;
using Senparc.Weixin.QY.Entities;
using System;
using System.Collections.Generic;

namespace Offertech.Util.WeChat.SenparcTools
{
    public class MessageHelper : SenparcSdkBase
    {
        /// <summary>
        /// news消息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MassResult SendNews(string toUser, string toParty, string toTag, string agentId, List<Article> articles,
            int safe = 0, int timeOut = 10000)
        {
            try
            {


                if (toParty != null && toParty.Split(new char[] {'|'}).Length > 100)
                {
                    MassResult result = new MassResult();
                    var list = SplitArray(toParty.Split(new char[] {'|'}), 100);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == 0)
                        {
                            result = MassApi.SendNews(GetToken(), toUser, string.Join("|", list[i]), toTag, agentId,
                                articles);
                        }
                        else
                        {
                            result = MassApi.SendNews(GetToken(), null, string.Join("|", list[i]), null, agentId,
                                articles);
                        }
                    }
                    return result;
                }
                else
                {
                    return MassApi.SendNews(GetToken(), toUser, toParty, toTag, agentId, articles);
                }
            }
            catch (Exception e)
            {
                MassResult result = new MassResult();
                return result;
            }
        }

        public static List<string[]> SplitArray(string[] data, int size)
        {
            List<string[]> list = new List<string[]>();
            for (int i = 0; i < data.Length / size; i++)
            {
                string[] r = new string[size];
                Array.Copy(data, i * size, r, 0, size);
                list.Add(r);
                
            }
            if (data.Length % size != 0)
            {
                string[] r = new string[data.Length % size];
                Array.Copy(data, data.Length - data.Length % size, r, 0, data.Length % size);
                list.Add(r);
            }
            return list;
        }


        /// <summary>
        /// text消息
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="toParty"></param>
        /// <param name="toTag"></param>
        /// <param name="agentId"></param>
        /// <param name="content"></param>
        /// <param name="safe"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MassResult SendText(string toUser, string toParty, string toTag, string agentId, string content,
            int safe = 0, int timeOut = 10000)
        {
            try
            {
                return MassApi.SendText(GetToken(), toUser, toParty, toTag, agentId, content);
            }
            catch (Exception e)
            {
                MassResult result = new MassResult();
                return result;
            }
        }

    }
}