using System.Web;
using System.Web.Optimization;

namespace Inventory.LunarMed.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         //"~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery-1.9.1.min.js",
                         "~/Scripts/jquery.flot.js",
                         "~/Scripts/jquery.flot.resize.js",
                         "~/Scripts/jquery.dataTables.js",
                         "~/Scripts/jquery-ui-1.12.1.min.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/respond.js",
                      "~/Content/bootstrap/js/bootstrap.min.js",
                      "~/Scripts/bootstrap-dialog.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/css/bootstrap.min.css",
                      "~/Content/bootstrap/css/bootstrap-responsive.min.css",
                      "~/Content/opensans.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/theme.css",
                      "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/bootstrap-dialog.css"));
        }
    }
}
