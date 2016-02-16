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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Assets/javascript/lib/toastr/toastr.min.css",
                      "~/Content/reset.css",
                      "~/Content/site.css"));

             // Common
             bundles.Add(new ScriptBundle("~/bundles/site-libs").Include(
                 "~/Scripts/bootstrap.js",
                 "~/Scripts/respond.js",
                "~/Assets/javascript/lib/toastr/toastr.min.js",
                "~/Assets/javascript/lib/handlebars/handlebars.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Assets/javascript/src/common/newsletter-subscriber.js"));

            // DateTime picker
            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                "~/Assets/javascript/lib/moment/min/moment.min.js",
                "~/Assets/javascript/lib/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/Assets/javascript/src/once/custom-datetime-pickers-activator.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Assets/javascript/lib/moment/min/moment.min.js",
                "~/Assets/javascript/lib/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/Assets/javascript/src/once/custom-date-pickers-activator.js"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker-css").Include(
                "~/Assets/javascript/lib/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/trip-detailed").Include(
                "~/Assets/javascript/src/once/trip-detailed-join-request.js"));


            // BundleTable.EnableOptimizations = true;
        }
    }
}
