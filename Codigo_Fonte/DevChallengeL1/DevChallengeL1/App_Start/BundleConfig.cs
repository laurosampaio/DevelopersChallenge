using System.Web.Optimization;

namespace DevChallengeL1
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquerybracket").Include(
                        "~/Scripts/jquery.bracket.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryjson").Include(
                        "~/Scripts/jquery.json-2.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.js"));
            
            

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/respond.js"));




            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery.bracket.css",
                      "~/Content/site.css"));
        }
    }
}
