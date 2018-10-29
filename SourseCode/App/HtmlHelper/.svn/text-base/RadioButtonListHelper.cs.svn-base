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
    public static class RadioButtonListHelper
    {
        #region Radio Button

        /// <summary>
        /// Radio列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <param name="isChecked">默认单选状态</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isChecked = false)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return RadioButtonList(htmlHelper, name, selectList, htmlAttributes, isChecked);

        }


        /// <summary>
        /// Radio列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="selectList">集合</param>
        /// <param name="isChecked">默认单选状态</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool isChecked = false)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return RadioButtonList(htmlHelper, name, selectList, new { }, isChecked);

        }

        /// <summary>
        /// Radio列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <param name="isChecked">默认单选状态</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isChecked = false)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            string defaultValue = string.Empty;
            if (htmlHelper.ViewData.Model != null && htmlHelper.ViewData.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    defaultValue = htmlHelper.ViewData.Eval(name).GetString();

                }
               // isChecked = false;

            }


            StringBuilder str = new StringBuilder();
            foreach (SelectListItem item in selectList)
            {
                str.Append("<input ");
                if (item.Value == defaultValue)
                {
                    str.Append("checked='checked' ");

                }
                else if (isChecked)
                {

                    str.Append(" checked=true ");
                    isChecked = false;
                }
                foreach (var bute in HtmlAttributes)
                {
                    str.Append(bute.Key + "=\"" + bute.Value);

                }

                str.Append("\" id=\"" + item.Value + "\" type=\"radio\"  value=\"" + item.Value + "\" name=\"" + name + "\"/>");
                str.Append(item.Text);

            }


            return MvcHtmlString.Create(str.ToString());
        }

        #endregion

        #region CheckBox create by chand 2015-06-18


        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <param name="isChecked">默认单选状态</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isChecked = false)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return CheckBoxList(htmlHelper, name, selectList, htmlAttributes, isChecked);

        }


        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="selectList">集合</param>
        /// <param name="isChecked">默认单选状态</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool isChecked = false)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return CheckBoxList(htmlHelper, name, selectList, new { }, isChecked);

        }


        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <param name="isChecked">默认单选状态</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isChecked = false)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            string defaultValue = string.Empty;
            if (htmlHelper.ViewData.Model != null && htmlHelper.ViewData.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    defaultValue = htmlHelper.ViewData.Eval(name).GetString();

                }
                //isChecked = false;

            }


            StringBuilder str = new StringBuilder();
            foreach (SelectListItem item in selectList)
            {
                str.Append("<label style='margin:0px 10px 0px 0px;'><input ");
                if (item.Value == defaultValue)
                {
                    str.Append("checked='checked' ");

                }
                else if (isChecked)
                {

                    str.Append(" checked=true ");
                    isChecked = false;
                }
                if (HtmlAttributes != null && HtmlAttributes.Count > 0)
                {
                    foreach (var bute in HtmlAttributes)
                    {
                        if (!string.IsNullOrWhiteSpace(bute.Key))
                        {
                            str.Append(bute.Key + "=\"" + bute.Value);
                        }
                    }
                }

                str.Append("\" id=\"" + item.Value + "\" type=\"checkbox\"  value=\"" + item.Value + "\" name=\"" + name + "\"/>");
                str.Append(item.Text);
                str.Append("</label>");
            }


            return MvcHtmlString.Create(str.ToString());
        }

        #endregion

    }
}

