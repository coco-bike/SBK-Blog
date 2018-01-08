using System.Web;
using System.Web.Optimization;

namespace UI
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/jquery").Include(
                        "~/Content/EasyUi/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/Content/easyUiJs").Include(
                        "~/Content/EasyUi/jquery.easyui.min.js",
                        "~/Content/EasyUi/locale/easyui-lang-zh_CN.js")
                );

            bundles.Add(new ScriptBundle("~/Content/utils").Include(
                       "~/Scripts/utils.js"));

            bundles.Add(new ScriptBundle("~/Areas/Menu").Include(
                "~/Areas/Admin/Scripts/Admin-Menu.js"));
     
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/easyUiCss").Include(
                "~/Content/EasyUi/css/icon.css",
                "~/Content/EasyUi/themes/default/easyui.css")
                );

            bundles.Add(new StyleBundle("~/Content/_LayoutCss").Include(
            "~/Areas/Admin/css/Layout.css")
            );

        }
    }
}