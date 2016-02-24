using System.Web;
using System.Web.Optimization;

namespace TripDestination.Web.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);

            BundleTable.EnableOptimizations = true;
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Common
            bundles.Add(new ScriptBundle("~/bundles/site-libs").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
               "~/Assets/javascript/lib/toastr/toastr.min.js",
               "~/Assets/javascript/lib/handlebars/handlebars.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Assets/javascript/src/common/newsletter-subscriber.js",
                "~/Assets/javascript/src/common/notification-signalr.js"));

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
                "~/Assets/javascript/src/once/trip-detailed-join-request.js",
                "~/Assets/javascript/src/once/trip-detailed-approve-join-request.js",
                "~/Assets/javascript/src/once/trip-detailed-like.js"));

            bundles.Add(new ScriptBundle("~/bundles/comments").Include(
                "~/Assets/javascript/src/once/add-comment.js",
                "~/Assets/javascript/src/once/load-more-comments.js"));

            bundles.Add(new ScriptBundle("~/bundles/registration").Include(
                "~/Assets/javascript/src/once/registration.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/Kendo/kendo.all.min.js",
                "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/trip-list").Include(
                "~/Assets/javascript/src/once/trip-list.js"));

            bundles.Add(new ScriptBundle("~/bundles/homepage").Include(
                "~/Assets/javascript/src/once/homepage.js"));

            bundles.Add(new ScriptBundle("~/bundles/notifications").Include(
                "~/Assets/javascript/src/once/notifications.js"));

            bundles.Add(new ScriptBundle("~/bundles/pagination").Include(
                "~/Assets/javascript/src/once/pagination.js",
                "~/Assets/javascript/lib/urijs/src/URI.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/trip-edit").Include(
                "~/Assets/javascript/src/once/trip-edit.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Assets/javascript/lib/toastr/toastr.min.css",
                      "~/Content/reset.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo-css").Include(
                      "~/Content/Kendo/kendo.common.min.css",
                      "~/Content/Kendo/kendo.default.min.css"));
        }
    }
}
