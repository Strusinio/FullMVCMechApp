using System.Web;
using System.Web.Optimization;

namespace MechAppProject
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Content/animated-event-calendar").Include(
                        "~/Content/animated-event-calendar/dist/jquery.simple-calendar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/site.css",
                      "~/Content/animated-event-calendar/dist/simple-calendar.css",
                      "~/Content/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/bundles/WorkshopStyles").Include(
                "~/Content/vendor/fontawesome-free/css/all.min.css",
                "~/Content/vendor/datatables/dataTables.bootstrap4.css",
                "~/Content/sb-admin-2.css"));

            bundles.Add(new ScriptBundle("~/bundles/WorkshopScripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/sb-admin-2.js",
                "~/Content/vendor/datatables/jquery.dataTables.js",
                "~/Content/vendor/datatables/dataTables.bootstrap4.js",
                "~/Content/vendor/datatables/datatables-demo.js",
                "~/Content/vendor/jquery-easing/jquery.easing.min.js",
                "~/Content/vendor/chart.js/Chart.min.js",
                "~/Content/js/demo/chart-area-demo.js",
                "~/Content/js/demo/chart-pie-demo.js"));
        }
    }
}
