using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Offertech.Util.WeChat.SenparcTools
{
    /// <summary>
    /// 企业号微信支付接口
    /// </summary>
    public class TenPayHelper : SenparcSdkBase
    {
        /// <summary>
        /// 用于企业向微信用户个人付款
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RedPacketSentResult Transfers(string openId, int amount, string desc)
        {
            string mchid = ConfigurationManager.AppSettings["Mchid"]; //商户号
            string MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];//支付密钥
            string nonce_str = GenNoncestr(16);
            Random r = new Random();
            string partner_trade_no = mchid + DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(0, 9);
            string ip = Util.Net.Ip;
            //获得签名
            string signValue = GetSignature(amount, "NO_CHECK", desc,
                CORPID, mchid, nonce_str, openId, partner_trade_no,
                ip, MerchantKey);

            string post_data = "";
            post_data += "<xml>";
            post_data += "<mch_appid>" + CORPID + "</mch_appid>"; //企业号corpid
            post_data += "<mchid>" + mchid + "</mchid>"; //微信支付分配的商户号
            post_data += "<nonce_str>" + nonce_str + "</nonce_str>"; //随机字符串，不长于32位
            post_data += "<partner_trade_no>" + partner_trade_no + "</partner_trade_no>"; //商户订单号，需保持唯一性
            post_data += "<openid>" + openId + "</openid>"; //商户appid下，某用户的openid
            post_data += "<check_name>NO_CHECK</check_name>"; //NO_CHECK：不校验真实姓名 
                    //FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） 
                    //OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
            //post_data += "<re_user_name></re_user_name>"; //收款用户真实姓名。 如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名
            post_data += "<amount>" + amount + "</amount>"; //企业付款金额，单位为分
            post_data += "<desc>" + desc + "</desc>"; //企业付款操作说明信息。必填。
            post_data += "<spbill_create_ip>" + ip + "</spbill_create_ip>"; //调用接口的机器Ip地址
            post_data += "<sign>" + signValue + "</sign>"; //签名
            post_data += "</xml>";
            string post_url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            string request_str = Web_Post(post_url, post_data, mchid, "~/App_Data/apiclient_cert.p12");
            var result = Parse(request_str);
            return result;
        }


        public static string GenNoncestr(int length)
        {
            Random r = new Random();
            string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string noncestr = "";
            for (int i = 0; i < length; i++)
            {
                noncestr += str[r.Next(str.Length)];
            }
            return noncestr;
        }

        public static string GetSignature(int amount, string check_name, string desc,
            string mch_appid, string mchid, string nonce_str, string openid, string partner_trade_no,
            string spbill_create_ip, string MerchantKey)
        {
            string signature = "";
            string signature_str = "";
            string StringA = "";
            //拼接签名算法
            StringA += "amount=" + amount;
            StringA += "&check_name=" + check_name;
            StringA += "&desc=" + desc;
            StringA += "&mch_appid=" + mch_appid;
            StringA += "&mchid=" + mchid;
            StringA += "&nonce_str=" + nonce_str;
            StringA += "&openid=" + openid;
            StringA += "&partner_trade_no=" + partner_trade_no;
            //StringA += "&re_user_name=" + re_user_name;
            StringA += "&spbill_create_ip=" + spbill_create_ip;
            signature_str = StringA + "&key=" + MerchantKey;
            signature = EncryptHelper.MD5(signature_str).ToUpper();
            return signature;
        }

        public static RedPacketSentResult Parse(string result)
        {
            try
            {
                var xml = XDocument.Parse(result).Root;
                var returnCode = xml.Element("result_code").Value;
                var return_msg = xml.Element("return_msg").Value;
                RedPacketSentError error = new RedPacketSentError();
                if (returnCode.Equals("FAIL", StringComparison.OrdinalIgnoreCase))
                {
                    var balanceNotEnough = string.Equals(xml.Element("err_code").Value, "NOTENOUGH", StringComparison.OrdinalIgnoreCase);
                    error = balanceNotEnough ? RedPacketSentError.BalanceNotEnough : RedPacketSentError.Other;
                }
                if (!string.Equals(returnCode, "SUCCESS", StringComparison.OrdinalIgnoreCase))
                    return new RedPacketSentResult { Succeeded = false, Response = result, Error = error, return_msg = return_msg };

                var resultCode = xml.Element("result_code").Value;
                var billno = xml.Element("payment_no").Value;
                var payment_time = xml.Element("payment_time").Value;
                
                if (!string.Equals(resultCode, "SUCCESS", StringComparison.OrdinalIgnoreCase))
                    return new RedPacketSentResult { Succeeded = false, Response = result, Error = error, return_msg = return_msg };

                return new RedPacketSentResult { Succeeded = true, BillNumber = billno,payment_time=payment_time, Response = result };
            }
            catch (Exception ex)
            {
                //Logger.WriteError("解析微信返回结果时发生错误", ex);
                return new RedPacketSentResult { Succeeded = false, Response = result, Error = RedPacketSentError.InternalError };
            }
        }


        public static string Web_Post(string url, string xmldata, string Mchid, string CertUrl)
        {
            //证书地址,并且双击apiclient_cert.p12安装证书
            string cert = System.Web.HttpContext.Current.Server.MapPath(CertUrl);
            string password = Mchid;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            //****************注意*********************
            //这里添加 证书要注意，在本地调试的时候
            //本地调试用这个
            //X509Certificate cer = new X509Certificate(cert, password);
            //上传服务器上用这个
            X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.ClientCertificates.Add(cer);
            webrequest.Method = "post";
            webrequest.UseDefaultCredentials = true;
            //添加xml数据
            StreamWriter swMessages = new StreamWriter(webrequest.GetRequestStream());
            //写入的流以XMl格式写入
            swMessages.Write(xmldata);
            //关闭写入流
            swMessages.Close();

            HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
            Stream stream = webreponse.GetResponseStream();
            string resp = string.Empty;
            using (StreamReader reader = new StreamReader(stream))
            {
                resp = reader.ReadToEnd();
            }
            return resp;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
    }

    public class RedPacketSentResult
    {
        public bool Succeeded { get; set; }

        public string Response { get; set; }

        public RedPacketSentError Error { get; set; }

        public int Amount { get; set; }

        public string BillNumber { get; set; }

        public string payment_time { get; set; }

        public string return_msg { get; set; }

    }

    public enum RedPacketSentError : byte
    {
        None = 0,
        InternalError = 1,
        BalanceNotEnough = 2,
        Other = 128
    }
}
