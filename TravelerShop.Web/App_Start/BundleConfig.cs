using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace TravelerShop.Web.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));
        }
    }
}