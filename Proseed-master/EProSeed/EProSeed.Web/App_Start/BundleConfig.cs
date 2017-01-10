using System.Web;
using System.Web.Optimization;

namespace EProSeed.Web
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/bootstrap.js",
                         "~/Scripts/App.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme/js").Include(
                "~/Scripts/ace.min.js",
                "~/Scripts/ace-elements.min.js"
                ));


            bundles.Add(new StyleBundle("~/Theming/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/bootstrap-theme.css"
                ));


            bundles.Add(new StyleBundle("~/Theming/style").Include(
             "~/Content/css/style.css"
                ));




            bundles.Add(new StyleBundle("~/themes/css").Include(
              "~/Content/css/ace_skins_min.css",
              "~/Content/css/ace_min.css"
              ));

          







        }
    }
}