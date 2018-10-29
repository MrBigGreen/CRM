using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.IO;

using System.Text.RegularExpressions;
using Common;
using System.Collections;

namespace Models
{
    public abstract class Utils
    {

        #region 读写删cookie

        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="strname">cookie名称</param>
        /// <param name="strvalue">cookie值</param>
        /// <param name="days">保存天数</param>
        public static void WriteCookie(string strname, string strvalue, int days)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
                cookie = new HttpCookie(strname);
            cookie.Value = UrlEncode(strvalue);
            if (days > 0)
                cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="strname"></param>
        /// <param name="mycookie"></param>
        /// <param name="hours"></param>
        public static void WriteCookie(string strname, Common.Account mycookie, int hours)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
                cookie = new HttpCookie(strname);
            cookie.Value = UrlEncode(SerializeObject(mycookie));
            //唯一账户在线 禁止同一帐号同时在线
            SetOnlineInfo(HttpContext.Current, mycookie.Id);

            if (hours > 0)
                cookie.Expires = DateTime.Now.AddHours(hours);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie
        /// </summary>
        /// <param name="strname"></param>
        /// <param name="mycookie"></param>
        /// <param name="days"></param>
        public static void WriteCookie(string strname, object obj, int days)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
            if (cookie == null)
                cookie = new HttpCookie(strname);
            cookie.Value = UrlEncode(SerializeObject(obj));
            if (days > 0)
                cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读取cookie值
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        public static string ReadCookie(string strname)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null)
            {
                return UrlDecode(HttpContext.Current.Request.Cookies[strname].Value.ToString());
            }
            return "";
        }

        /// <summary>
        /// 读取cookie并返回CookieEntity
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        public static Account ReadCookieAsObj(string strname)
        {
            Account cookie = null;
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null)
            {
                return DSerializeToObject<Account>(UrlDecode(HttpContext.Current.Request.Cookies[strname].Value.ToString()));
            }
            return cookie;
        }
        /// <summary>
        /// 读取cookie并返回CookieEntity
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        public static T ReadCookieAsObj<T>(string strname) where T : class
        {
            T t = default(T);

            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null)
            {
                return DSerializeToObject<T>(UrlDecode(HttpContext.Current.Request.Cookies[strname].Value.ToString()));
            }
            return t;
        }

        /// <summary>
        /// 删除cookie
        /// </summary>
        /// <param name="strname"></param>
        public static void DeleteCookie(string strname)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strname] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[strname];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
        #endregion

        #region 唯一账户在线 禁止同一帐号同时在线  create by chand 2018-08-17
        /// <summary>
        /// 设置唯一登录账号
        /// </summary>
        /// <param name="context"></param>
        /// <param name="username"></param>
        private static void SetOnlineInfo(HttpContext context, string username)
        {
            HttpApplication ttt = new HttpApplication();
            Hashtable hOnline = (Hashtable)context.Application["Online"];//读取全局变量
            if (hOnline != null)
            {
                IDictionaryEnumerator idE = hOnline.GetEnumerator();
                string strKey = "";
                while (idE.MoveNext())
                {
                    if (idE.Value != null && idE.Value.ToString().Equals(username))//如果当前用户已经登录，
                    {
                        //already login            
                        strKey = idE.Key.ToString();
                        hOnline[strKey] = "XX";//将当前用户已经在全局变量中的值设置为XX
                        break;
                    }
                }
            }
            else
            {
                hOnline = new Hashtable();
            }

            hOnline[context.Session.SessionID] = username;//初始化当前用户的
            context.Application.Lock();
            context.Application["Online"] = hOnline;
            context.Application.UnLock();
        }

        #endregion

        #region URL处理

        /// <summary>
        /// url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return HttpContext.Current.Server.UrlDecode(str);
        }
        #endregion

        #region 序列化反序列化对象
        /// <summary>
        /// 序列化为字节流
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            string result = string.Empty;
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    byte[] byt = new byte[ms.Length];
                    byt = ms.ToArray();
                    result = Convert.ToBase64String(byt);
                    ms.Flush();
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 反序列化字节流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T DSerializeToObject<T>(string str) where T : class
        {
            T t = default(T);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                byte[] byt = Convert.FromBase64String(str);
                using (MemoryStream ms = new MemoryStream(byt, 0, byt.Length))
                {
                    t = bf.Deserialize(ms) as T;
                }
            }
            catch (Exception ex)
            {

            }
            return t;
        }

        /// <summary>
        /// 对象序列化为json串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjToJsonstr<T>(T t) where T : class
        {
            string jsonResult = string.Empty;
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                dcjs.WriteObject(ms, t);
                jsonResult = Encoding.UTF8.GetString(ms.ToArray());
            }
            return jsonResult;
        }

        /// <summary>
        /// json串反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        public static T JsonstrToObj<T>(string jsonstr) where T : class
        {
            T resultObj = default(T);
            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonstr)))
            {
                resultObj = dcjs.ReadObject(ms) as T;
            }
            return resultObj;
        }

        #endregion

        #region 清除HTML标记
        public static string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        #region 获取当前星期

        /// <summary>
        /// 获取当前星期用中文格式显示
        /// </summary>
        /// <returns></returns>
        public static string GetCurCnWeek()
        {
            string cnWeek = string.Empty;
            string enWeek = DateTime.Now.DayOfWeek.ToString();
            switch (enWeek)
            {
                case "Monday":
                    cnWeek = "星期一";
                    break;
                case "Tuesday":
                    cnWeek = "星期二";
                    break;
                case "Wednesday":
                    cnWeek = "星期三";
                    break;
                case "Thursday":
                    cnWeek = "星期四";
                    break;
                case "Friday":
                    cnWeek = "星期五";
                    break;
                case "Saturday":
                    cnWeek = "星期六";
                    break;
                case "Sunday":
                    cnWeek = "星期日";
                    break;
            }
            return cnWeek;
        }


        #endregion

        #region 过滤 Javascript
        ///
        /// 过滤 Javascript
        ///
        /// "content">
        ///
        public static string FilterScript(string content)
        {
            string commentPattern = @"(?'comment')";
            string embeddedScriptComments =
                @"(\/\*.*?\*\/|\/\/.*?[\n\r])";
            string scriptPattern =
                String.Format(@"(?'script']*>(.*?{0}?)*]*>)",
                embeddedScriptComments);
            string pattern = String.Format(@"(?s)({0}|{1})",
                commentPattern, scriptPattern);
            return StripScriptAttributesFromTags(Regex.Replace(content,
                pattern, string.Empty, RegexOptions.IgnoreCase));
        }

        #endregion

        #region 过滤javascript属性值（如onclick等）
        ///
        /// 过滤javascript属性值（如onclick等）
        ///
        /// "content">
        ///
        private static string StripScriptAttributesFromTags(string content)
        {
            string eventAttribs =
                @"on(blur|c(hange|lick)|dblclick|focus|keypress|" +
                "(key|mouse)(down|up)|(un)?load" +
                "|mouse(move|o(ut|ver))|reset|s(elect|ubmit))";

            string pattern = String.Format(@"(?inx)" +
                @"\<(\w+)\s+((?'attribute'(?'attributeName'{0})\s*=\s*" +
                @"(?'delim'['""]?)(?'attributeValue'[^'"">]+)(\3)" +
                @")|(?'attribute'(?'attributeName'href)\s*=\s*" +
                @"(?'delim'['""]?)(?'attributeValue'javascript[^'"">]+)" +
                @"(\3))|[^>])*\>", eventAttribs);
            Regex re = new Regex(pattern);
            // 使用MatchEvaluator的委托
            return re.Replace(content, new MatchEvaluator(StripAttributesHandler));
        }



        ///
        /// 取得属性值
        ///
        /// "m">
        ///
        private static string StripAttributesHandler(Match m)
        {
            if (m.Groups["attribute"].Success)
            {
                return m.Value.Replace(m.Groups["attribute"].Value, "");
            }
            else
            {
                return m.Value;
            }
        }

        #endregion

        #region 去掉javascript（scr链接方式）
        ///
        /// 去掉javascript（scr链接方式）
        ///
        /// "content">
        ///
        public static string FilterAHrefScript(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @" href[ ^=]*= *[\s\S]*script *:";
            return Regex.Replace(newstr, regexstr, string.Empty,
                RegexOptions.IgnoreCase);
        }

        #endregion

        #region 去掉链接文件
        ///
        /// 去掉链接文件
        ///
        /// "content">
        ///
        public static string FilterSrc(string content)
        {
            string newstr = FilterScript(content);
            string regexstr =
                @" src *= *['""]?[^\.]+\.(js|vbs|asp|aspx|php|jsp)['""]";
            return Regex.Replace(newstr, regexstr, @"",
                RegexOptions.IgnoreCase);
        }

        #endregion

        #region 过滤HTML
        ///
        /// 过滤HTML
        ///
        /// "content">
        ///
        public static string FilterHtml(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @"]*>";
            return Regex.Replace(newstr, regexstr,
                string.Empty, RegexOptions.IgnoreCase);
        }

        #endregion

        #region 过滤 OBJECT
        ///
        /// 过滤 OBJECT
        ///
        /// "content">
        ///
        public static string FilterObject(string content)
        {
            string regexstr = @"(?i)])*>(\w|\W)*])*>";
            return Regex.Replace(content, regexstr,
                string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion


        ///
        /// 过滤以上所有
        ///
        /// "content">
        ///
        public static string FilterAll(string content)
        {
            try
            {
                content = Regex.Replace(content, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"<script>.*?</script>", "", RegexOptions.IgnoreCase);
                content = FilterAHrefScript(content);
                content = FilterSrc(content);
                while (content.IndexOf("<script>") > 0)
                {
                    content = RemoveScript(content);
                }

            }
            catch (Exception)
            {

            }

            return content;
        }

        private static string RemoveScript(string content)
        {
            int i = content.IndexOf("<script>");
            int e = content.IndexOf("</script>");
            if (i > 0 && e > i)
            {
                content = content.Remove(i, e - i + 9);
            }
            return content;
        }


      

    }



}
