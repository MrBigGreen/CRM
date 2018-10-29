using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common;
using System.Linq.Expressions;
namespace Models
{
    public static class CheckBoxListHelper
    {
        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="helper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxList(helper, name, selectList, new { });
        }
        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="helper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            HashSet<string> set = new HashSet<string>();
            List<SelectListItem> list = new List<SelectListItem>();
            string selectedValues = Convert.ToString((selectList as SelectList).SelectedValue);

            if (!string.IsNullOrEmpty(selectedValues))
            {
                if (selectedValues.Contains(","))
                {

                    string[] tempStr = selectedValues.Split(',');

                    for (int i = 0; i < tempStr.Length; i++)
                    {
                        set.Add(tempStr[i]);
                    }
                }
                else
                {
                    set.Add(selectedValues);
                }
            }
            foreach (SelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? set.Contains(item.Value) : set.Contains(item.Text);
                list.Add(item);
            }
            selectList = list;
            HtmlAttributes.Add("type", "checkbox");
            HtmlAttributes.Add("id", name);
            HtmlAttributes.Add("name", name);
            //HtmlAttributes.Add("style", "margin:0 0 0 10px;line-height:30px; vertical-align:-8px;border:none;");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (SelectListItem selectItem in selectList)
            {

                IDictionary<string, object> newHtmlAttributes = HtmlAttributes.DeepCopy();
                newHtmlAttributes.Add("value", selectItem.Value);
                if (selectItem.Selected)
                {
                    newHtmlAttributes.Add("checked", "checked");
                }
                TagBuilder tagBuilder = new TagBuilder("input");
                tagBuilder.MergeAttributes<string, object>(newHtmlAttributes);
                string inputAllHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
                stringBuilder.AppendFormat(@"<label style=""margin:0px 10px 0px 0px;""> {0}  {1}</label>",
                   inputAllHtml, selectItem.Text);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        private static IDictionary<string, object> DeepCopy(this IDictionary<string, object> ht)
        {
            Dictionary<string, object> _ht = new Dictionary<string, object>();
            foreach (var p in ht)
            {
                _ht.Add(p.Key, p.Value);
            }
            return _ht;
        }

    }
}

