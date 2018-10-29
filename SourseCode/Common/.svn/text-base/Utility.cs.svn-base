using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using log4net;

namespace Common
{

    /// <summary>
    /// 密码强度
    /// </summary>
    public enum Strength
    {
        /// <summary>
        /// 无效密码
        /// </summary>
        Invalid = 0,
        /// <summary>
        /// 低强度密码
        /// </summary>
        Weak = 1,
        /// <summary>
        /// 中强度密码
        /// </summary>
        Normal = 2,
        /// <summary>
        /// 高强度密码
        /// </summary>
        Strong = 3
    };


    public static class Utility
    {

        private static readonly Random _random =
            new Random((int)(DateTime.Now.Ticks & 0xffffffffL) | (int)(DateTime.Now.Ticks >> 32));

        public static string GetRandom()
        {
            return _random.Next(100000, 999999).ToString();
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Get32MD5(string str)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string MaskPassword(string password, DateTime time, string userID)
        {
            string result = string.Empty;
            var shortDateString = time.ToString("yyyy-MM-dd").Replace("-", "");
            for (int i = 0; i < 4; i++)
            {
                result += password.Substring(i * 2, 2);
                result += shortDateString.Substring(i * 2, 2);
                if (i == 3)
                {
                    result += password.Substring((i + 1) * 2);
                }
            }
            result += userID.Replace("-", "");
            result += time.ToString("HH:mm:ss").Replace(":", "");
            return result;
        }

        public static string MaskJobURL(string jobName, DateTime time, string userID)
        {
            string result = string.Empty;
            var password = Get32MD5(jobName);
            var shortDateString = time.ToString("yyyy-MM-dd").Replace("-", "") +
                                  time.ToString("HH:mm:ss").Replace(":", "");
            for (int i = 0; i < 7; i++)
            {
                result += password.Substring(i * 2, 2);
                result += shortDateString.Substring(i * 2, 2);
                if (i == 6)
                {
                    result += password.Substring((i + 1) * 2);
                }
            }
            result += userID.Replace("-", "");
            return result;
        }

        public static List<string> CrackMaskedPassword(string code)
        {
            string password = string.Empty;
            string userID = string.Empty;
            string datetime = string.Empty;
            var list = new List<string>();
            try
            {
                password = code.Substring(0, 2) + code.Substring(4, 2) + code.Substring(8, 2) + code.Substring(12, 2) +
                           code.Substring(16, 2) + code.Substring(18, 22);
                datetime = code.Substring(2, 2) + code.Substring(6, 2) + "-" + code.Substring(10, 2) + "-" +
                           code.Substring(14, 2) + " " + code.Substring(code.Length - 6, 2) + ":" +
                           code.Substring(code.Length - 4, 2) + ":" + code.Substring(code.Length - 2, 2);
                userID = ConvertStrToGUID(code.Substring(40, 32));

                list.Add(password);
                list.Add(datetime);
                list.Add(userID);
            }
            catch
            {
            }
            return list;
        }

        public static List<string> CrackMaskedEmail(string code)
        {
            string email = string.Empty;
            string userID = string.Empty;
            string datetime = string.Empty;
            var list = new List<string>();
            try
            {
                var length = code.LastIndexOf('|');
                email = code.Substring(0, 2) + code.Substring(4, 2) + code.Substring(8, 2) + code.Substring(12, 2) +
                           code.Substring(16, 2) + code.Substring(18, length - 18);
                datetime = code.Substring(2, 2) + code.Substring(6, 2) + "-" + code.Substring(10, 2) + "-" +
                           code.Substring(14, 2) + " " + code.Substring(code.Length - 6, 2) + ":" +
                           code.Substring(code.Length - 4, 2) + ":" + code.Substring(code.Length - 2, 2);
                userID = ConvertStrToGUID(code.Substring(length + 1, 32));

                list.Add(email);
                list.Add(datetime);
                list.Add(userID);
            }
            catch
            {
            }
            return list;
        }


        public static string ConvertStrToGUID(string str)
        {
            var result = str.Substring(0, 8) + "-" + str.Substring(8, 4) + "-" + str.Substring(12, 4) + "-" +
                         str.Substring(16, 4) + "-" + str.Substring(20, 12);
            return result;
        }


        public static string XMLSerialize<T>(T entity)
        {
            var xs = new XmlSerializer(typeof(T));
            var stream = new MemoryStream();
            xs.Serialize(stream, entity);
            stream.Position = 0;
            return StreamToString(stream);
        }

        public static T XMLDeserialize<T>(string data)
        {
            var xs = new XmlSerializer(typeof(T));
            var p = (T)xs.Deserialize(StringToStream(data));
            return p;
        }

        public static string XMLSerialize<T>(List<T> list)
        {
            var xs = new XmlSerializer(typeof(List<T>));
            var stream = new MemoryStream();
            xs.Serialize(stream, list);
            stream.Position = 0;
            return StreamToString(stream);
        }

        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        #region 操作日志


        #region Log4net记录日志
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteLog(Type t, Exception ex)
        {
            //WriteLog(typeof(FormMain), ex);
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }


        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteLog(Type t, string msg)
        {
            //WriteLog(typeof(FormMain), "测试Log4Net日志是否写入");
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }

        #endregion

        /// <summary>
        /// 发送短信日志
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="msgInfo"></param>
        /// <param name="result"></param>
        public static void WriteSMSLog(string phoneNumber, string msgInfo, int result)
        {
            try
            {
                if (string.IsNullOrEmpty(msgInfo))
                    return;
                var dt = DateTime.Now;
                var time = dt.ToString("[yyyy-MM-dd HH:mm:ss]");
                string path =AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["SmsLog"];
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var logName = dt.ToString("yyyyMMdd") + "_Log.log";
                var logFile = path + "/" + logName;
                string done = "失败";
                if (result == 0)
                {
                    done = "成功";
                }
               
                StringBuilder str = new StringBuilder();
                str.Append("Time:    " + time.ToString() + "\r\n");
                str.Append("Phone:  " + phoneNumber + "\r\n");
                str.Append("State:  " + done + "\r\n");
                str.Append("Message: " + msgInfo + "\r\n");
                str.Append("-----------------------------------------------------------\r\n\r\n");
                StreamWriter sw;
                if (!File.Exists(logFile))
                {
                    sw = File.CreateText(logFile);
                }
                else
                {
                    sw = File.AppendText(logFile);
                }
                sw.WriteLine(str.ToString());
                sw.Close();
            
            }
            catch (Exception ex)
            {
                //WriteErorrLog(ex, ConfigurationManager.AppSettings["EmailErrorLog"]);
            }
        }

        #endregion


        #region 计算密码强度

        /// <summary>
        /// 计算密码强度
        /// </summary>
        /// <param name="password">密码字符串</param>
        /// <returns></returns>
        public static Strength PasswordStrength(string password)
        {
            Strength aa;
            //空字符串强度值为0
            if (password == "" || password == "123456")
            {
                return Strength.Invalid;
            }

            //字符统计
            int iNum = 0, iLtt = 0, iSym = 0;
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9') iNum++;
                else if (c >= 'a' && c <= 'z') iLtt++;
                else if (c >= 'A' && c <= 'Z') iLtt++;
                else iSym++;
            }

            if (iLtt == 0 && iSym == 0) return Strength.Weak; //纯数字密码
            if (iNum == 0 && iLtt == 0) return Strength.Weak; //纯符号密码
            if (iNum == 0 && iSym == 0) return Strength.Weak; //纯字母密码

            if (password.Length <= 6) return Strength.Weak;   //长度不大于6的密码

            if (iLtt == 0) return Strength.Normal; //数字和符号构成的密码
            if (iSym == 0) return Strength.Normal; //数字和字母构成的密码
            if (iNum == 0) return Strength.Normal; //字母和符号构成的密码

            if (password.Length <= 10) return Strength.Normal; //长度不大于10的密码

            return Strength.Strong; //由数字、字母、符号构成的密码
        }
        #endregion

         
    }
}
