using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Offertech.Util.WeChat.SenparcTools
{
    public static class EncryptHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str)));
            a = a.Replace("-", "");
            return a;
        }
        /// <summary> 
        /// MD5加密 
        /// </summary> 
        /// <param name="str"></param> 
        /// <returns></returns> 
        public static string MD5DecryptString(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5Source = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Out = md5.ComputeHash(md5Source);
            return Convert.ToBase64String(md5Out);
        }

        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="sInputString">输入字符</param> 
        /// <param name="sKey">Key</param> 
        /// <returns>加密结果</returns> 
        public static string DESEncryptString(string sInputString, string sKey)
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes(sInputString);
                byte[] result;
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey); //密钥 
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey); //初始化向量 
                ICryptoTransform desencrypt = DES.CreateEncryptor(); //加密器对象 
                result = desencrypt.TransformFinalBlock(data, 0, data.Length); //转换指定字节数组的指定区域 
                return BitConverter.ToString(result);
            }
            catch (Exception ex)
            {
                //ex.Message = "DES加密异常"; 
                throw ex;
            }
        }

        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="sInputString">输入字符</param> 
        /// <param name="sKey">Key</param> 
        /// <returns>解密结果</returns> 
        public static string DESDecryptString(string sInputString, string sKey)
        {
            try
            {
                //将字符串转换为字节数组 
                string[] sInput = sInputString.Split("-".ToCharArray());
                byte[] data = new byte[sInput.Length];
                byte[] result;
                for (int i = 0; i < sInput.Length; i++)
                {
                    data[i] = byte.Parse(sInput[i], System.Globalization.NumberStyles.HexNumber);
                }

                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                result = desencrypt.TransformFinalBlock(data, 0, data.Length);
                return Encoding.Default.GetString(result);
            }
            catch (Exception ex)
            {
                //ex.Message = "DES解密异常"; 
                throw ex;
            }
        }

        /// <summary>
        /// 对url数据进行编码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UrlEncode(string data)
        {
            return System.Web.HttpUtility.UrlEncode(data);
        }

        /// <summary>
        /// 对url数据进行解码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string UrlDecode(string data)
        {
            return System.Web.HttpUtility.UrlDecode(data);
        }


        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string publickey, string content)
        {
            //publickey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADecrypt(string privatekey, string content)
        {
            //privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(privatekey);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }
    }
}
