using System.Web;
using System.Web.Optimization;

namespace TripDestination.Web.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/reset.css"));

            // DateTime picker
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Assets/javascript/lib/moment/min/moment.min.js",
                "~/Assets/javascript/lib/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/Assets/javascript/src/once/custom-datetime-pickers-activator.js"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker-css").Include(
                "~/Assets/javascript/lib/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css"));

            // BundleTable.EnableOptimizations = true;
        }
    }
}
