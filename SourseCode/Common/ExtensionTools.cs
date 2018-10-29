using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Common
{

    [Serializable]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class DescAttribute : Attribute
    {
        /// <summary>
        /// 获得字段或属性的中文解释.
        /// </summary>
        /// <value>字段或属性的中文解释.</value>
        public string Value { get; private set; }
        /// <summary>
        /// 初始化创建一个 <see cref="Desc"/> 类的实例, 用于指示字段或属性的解释说明.
        /// </summary>
        /// <param name="value">字段或属性的解释说明.</param>
        public DescAttribute(string value)
        {
            Value = value;
        }


    }

    public static class EnumExtension
    {
        /// <summary>
        /// 转换枚举文字说明
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static string ToDesc(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());

            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    return ((DescAttribute)attrs[0]).Value;
            }

            return enumeration.ToString();
        }

        /// <summary>
        /// 获取枚举文字说明
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetDesc(this Enum enumeration, int index)
        {
            Type type = enumeration.GetType();
            string showName = enumeration.ToString();
            foreach (var item in Enum.GetValues(type))
            {
                if (item.GetHashCode() == index)
                {
                    string name = Enum.GetName(type, item);
                    object[] attrs = type.GetField(name).GetCustomAttributes(typeof(DescAttribute), false);
                    if (null != attrs && attrs.Length > 0)
                        showName = ((DescAttribute)attrs[0]).Value;
                    break;
                }
            }
            return showName;
        }

        /// <summary>
        /// 将枚举以列表的形式返回
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectList(this Enum enumeration)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            Type type = enumeration.GetType();

            foreach (var item in Enum.GetValues(type))
            {
                string name = Enum.GetName(type, item);
                string showName = string.Empty;
                object[] attrs = type.GetField(name).GetCustomAttributes(typeof(DescAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    showName = ((DescAttribute)attrs[0]).Value;

                SelectListItem listitem = new SelectListItem
                {
                    Text = showName,
                    Value = item.GetHashCode().ToString()
                };
                list.Add(listitem);
            }
            return list;
        }


        public static List<SelectListItem> ToSelectList(this Enum enumeration, string explain)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() {Text = explain, Value = string.Empty}
            };
            Type type = enumeration.GetType();

            foreach (var item in Enum.GetValues(type))
            {
                string name = Enum.GetName(type, item);
                string showName = string.Empty;
                object[] attrs = type.GetField(name).GetCustomAttributes(typeof(DescAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    showName = ((DescAttribute)attrs[0]).Value;

                SelectListItem listitem = new SelectListItem
                {
                    Text = showName,
                    Value = item.GetHashCode().ToString()
                };
                list.Add(listitem);
            }
            return list;
        }


        /// <summary>
        /// 获取枚举类型转化为int值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="thisStatus"></param>
        /// <returns></returns>
        public static int ToInt<TEnum>(this TEnum thisStatus) where TEnum : struct
        {
            return thisStatus.GetHashCode();
        }

        /// <summary>
        /// 将枚举以字典的形式返回
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new KeyValuePair<int, string>(e.GetHashCode(), e.ToString());
            return values.ToDictionary(a => a.Key, b => b.Value);
        }

    }

    public static class StringExtension
    {
        public static int ToInt(this string s)
        {
            int i;
            int.TryParse(s, out i);
            return i;
        }


        public static short ToShort(this string s)
        {
            short i;
            short.TryParse(s, out i);
            return i;
        }
    }

    public static class DateTimeExtension
    {
        public static DateTime ToDateTime(this string s)
        {
            DateTime i;
            DateTime.TryParse(s,out i);
            return i;
        }
    }
}
