using System.Web;
using System.Web.Optimization;

namespace App
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.7.1.min.js",
                         "~/Res/My97DatePicker/WdatePicker.js",
                        "~/Res/ckeditor/ckeditor.js",
                         "~/Scripts/jquery.cookie.js"

                        ));
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                       "~/Res/easyui/jquery.easyui.min.js",
                         "~/Scripts/jquery.cookie.js",
                        "~/Scripts/JScriptCommon.js",
                      "~/Res/easyui/locale/easyui-lang-zh_CN.js",
                         "~/Res/My97DatePicker/WdatePicker.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.validate.unobtrusive-ajax.min.js"
                //,
                //   "~/Scripts/Theme.js"
                           ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                   "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new StyleBundle("~/Content/css/css").Include("~/Content/css/common.css"));



            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content").Include(
                             "~/Content/default.css"
                         ));//菜单图标
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Res/easyui/themes/icon.css",

                      "~/Content/StyleSheet.css"));
            BundleTable.EnableOptimizations = false;
        }
    }
}
