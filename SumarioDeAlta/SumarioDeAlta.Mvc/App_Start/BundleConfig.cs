
using System.Web.Optimization;


namespace SumarioDeAlta.Mvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-1-8-2-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1-9-0-custom.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryGrid").Include(
                "~/Scripts/jquery-jqGrid-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymobile").Include(
                      "~/Scripts/jquery-mobile-1-1-1.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryGridLocale").Include(
                "~/Scripts/i8n/grid.locale-pt-br.js", 
                "~/Scripts/i8n/jquery.ui.datepicker-pt-BR.js"));

            //"~/Content/jquery-mobile-1-1-1.css", Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Forms.css",
                "~/Content/redmond/jquery-ui-1-9-0-custom.css", 
                "~/Content/ui.jqgrid.css",
                "~/Content/workkerReset.css",
                "~/Content/workkerForm-structure.css",
                "~/Content/workkerStyle-structure.css",
                "~/Content/site-structure.css",
                "~/Content/site-style.css"));
        }
    }
}