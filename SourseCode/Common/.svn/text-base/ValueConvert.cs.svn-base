using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static partial class ValueConvert
    {
        /// <summary>
        /// 将最后一个字符串的路径path替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Path(this string str, string path)
        {
            int index = str.LastIndexOf('\\');
            int indexDian = str.LastIndexOf('.');
            return str.Substring(0, index + 1) + path + str.Substring(indexDian);
        }
        public static List<string> ToList(this string ids)
        {
            List<string> listId = new List<string>();
            if (!string.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<string>(ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                foreach (var item in sort)
                {
                    listId.Add(item);

                }
            }
            return listId;
        }
        /// <summary>
        /// 从^分割的字符串中获取多个Id,先是用 ^ 分割，再使用 & 分割
        /// </summary>
        /// <param name="ids">先是用 ^ 分割，再使用 & 分割</param>
        /// <returns></returns>
        public static List<string> GetIdSort(this string ids)
        {
            List<string> listId = new List<string>();
            if (!string.IsNullOrEmpty(ids))
            {

                var sort = new SortedSet<string>(ids.Split('^')
                    .Where(w => !string.IsNullOrWhiteSpace(w) && w.Contains('&'))
                    .Select(s => s.Substring(0, s.IndexOf('&'))));
                foreach (var item in sort)
                {
                    listId.Add(item);
                }
            }
            return listId;
        }
        /// <summary>
        /// 从，分割的字符串中获取单个Id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string GetId(this string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var sort = new SortedSet<string>(ids.Split('^')
                    .Where(w => !string.IsNullOrWhiteSpace(w) && w.Contains('&'))
                    .Select(s => s.Substring(0, s.IndexOf('&'))));
                foreach (var item in sort)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 将String转换为Dictionary类型，过滤掉为空的值，首先 6 分割，再 7 分割
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, string> StringToDictionary(string value)
        {
            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();
            string[] s = value.Split('^');
            for (int i = 0; i < s.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(s[i]) && !s[i].Contains("undefined") && !s[i].Contains("请选择"))
                {
                    var ss = s[i].Split('&');
                    if ((!string.IsNullOrEmpty(ss[0])) && (!string.IsNullOrEmpty(ss[1])))
                    {
                        queryDictionary.Add(ss[0], ss[1]);
                    }
                }

            }
            return queryDictionary;
        }
        /// <summary>
        /// 得到对象的 Int 类型的值，默认值0
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值0</returns>
        public static int GetInt(this object Value)
        {
            return GetInt(Value, 0);
        }
        /// <summary>
        /// 得到对象的 Int 类型的值，默认值0
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值0</returns>
        public static int GetInt(this object Value, int defaultValue)
        {

            if (Value == null) return defaultValue;
            if (Value is string && Value.GetString().HasValue() == false) return defaultValue;

            if (Value is DBNull) return defaultValue;

            if ((Value is string) == false && (Value is IConvertible) == true)
            {
                return (Value as IConvertible).ToInt32(CultureInfo.CurrentCulture);
            }

            int retVal = defaultValue;
            if (int.TryParse(Value.ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, out retVal))
            {
                return retVal;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 得到对象的 String 类型的值，默认值string.Empty
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值string.Empty</returns>
        public static string GetString(this object Value)
        {
            return GetString(Value, string.Empty);
        }
        /// <summary>
        /// 得到对象的 String 类型的值，默认值string.Empty
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值 。</returns>
        public static string GetString(this object Value, string defaultValue)
        {
            if (Value == null) return defaultValue;
            string retVal = defaultValue;
            try
            {
                var strValue = Value as string;
                if (strValue != null)
                {
                    return strValue;
                }

                char[] chrs = Value as char[];
                if (chrs != null)
                {
                    return new string(chrs);
                }

                retVal = Value.ToString();
            }
            catch
            {
                return defaultValue;
            }
            return retVal;
        }
        /// <summary>
        /// 得到对象的 DateTime 类型的值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回的默认值为DateTime.MinValue </returns>
        public static DateTime GetDateTime(this object Value)
        {
            return GetDateTime(Value, DateTime.MinValue);
        }

        /// <summary>
        /// 得到对象的 DateTime 类型的值，默认值为DateTime.MinValue
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回默认值为DateTime.MinValue</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回的默认值为DateTime.MinValue</returns>
        public static DateTime GetDateTime(this object Value, DateTime defaultValue)
        {
            if (Value == null) return defaultValue;

            if (Value is DBNull) return defaultValue;

            string strValue = Value as string;
            if (strValue == null && (Value is IConvertible))
            {
                return (Value as IConvertible).ToDateTime(CultureInfo.CurrentCulture);
            }
            if (strValue != null)
            {
                strValue = strValue
                    .Replace("年", "-")
                    .Replace("月", "-")
                    .Replace("日", "-")
                    .Replace("点", ":")
                    .Replace("时", ":")
                    .Replace("分", ":")
                    .Replace("秒", ":")
                      ;
            }
            DateTime dt = defaultValue;
            if (DateTime.TryParse(Value.GetString(), out dt))
            {
                return dt;
            }

            return defaultValue;
        }
        /// <summary>
        /// 得到对象的布尔类型的值，默认值false
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值false</returns>
        public static bool GetBool(this object Value)
        {
            return GetBool(Value, false);
        }

        /// <summary>
        /// 得到对象的 Bool 类型的值，默认值false
        /// </summary>
        /// <param name="Value">要转换的值</param>
        /// <param name="defaultValue">如果转换失败，返回的默认值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值false</returns>
        public static bool GetBool(this object Value, bool defaultValue)
        {
            if (Value == null) return defaultValue;
            if (Value is string && Value.GetString().HasValue() == false) return defaultValue;

            if ((Value is string) == false && (Value is IConvertible) == true)
            {
                if (Value is DBNull) return defaultValue;

                try
                {
                    return (Value as IConvertible).ToBoolean(CultureInfo.CurrentCulture);
                }
                catch { }
            }

            if (Value is string)
            {
                if (Value.GetString() == "0") return false;
                if (Value.GetString() == "1") return true;
                if (Value.GetString().ToLower() == "yes") return true;
                if (Value.GetString().ToLower() == "no") return false;
            }
            ///  if (Value.GetInt(0) != 0) return true;
            bool retVal = defaultValue;
            if (bool.TryParse(Value.GetString(), out retVal))
            {
                return retVal;
            }
            else return defaultValue;
        }
        /// <summary>
        /// 检测 GuidValue 是否包含有效的值，默认值Guid.Empty
        /// </summary>
        /// <param name="GuidValue">要转换的值</param>
        /// <returns>如果对象的值可正确返回， 返回对象转换的值 ，否则， 返回默认值Guid.Empty</returns>
        public static Guid GetGuid(string GuidValue)
        {
            try
            {
                return new Guid(GuidValue);
            }
            catch { return Guid.Empty; }
        }
        /// <summary>
        /// 检测 Value 是否包含有效的值，默认值false
        /// </summary>
        /// <param name="Value"> 传入的值</param>
        /// <returns> 包含，返回true，不包含，返回默认值false</returns>
        public static bool HasValue(this string Value)
        {
            if (Value != null)
            {
                return !string.IsNullOrEmpty(Value.ToString());
            }
            else return false;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string[] GetSplit(this string Value)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return new string[] { };
            }
            return Value.Split(new char[] { ',', ';', '^', '|', '，' }, StringSplitOptions.RemoveEmptyEntries);

        }


        public static int GetWeekOfYear()
        {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);

            //二.获取今天是一年当中的第几天
            int currentDay = DateTime.Today.DayOfYear;
            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //    刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        public static int GetWeekOfYear(DateTime dt, CultureInfo ci)
        {

            //调用：
            //int a=WeekOfYear(this.dateTimePicker1.Value, new CultureInfo("zh-CN"));
            return ci.Calendar.GetWeekOfYear(dt, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }


    }
}
